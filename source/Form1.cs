using Archipelago.MultiClient.Net;
using Archipelago.MultiClient.Net.Enums;
using Archipelago.MultiClient.Net.Helpers;
using Archipelago.MultiClient.Net.Models;
using Archipelago.MultiClient.Net.Packets;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Windows.Forms;
using static C3AP_Client.CrystalBits;
using static C3AP_Client.GameConfig;
using static C3AP_Client.SearchReserved;
using static C3AP_Client.SharedAdresses;
using static System.Collections.Specialized.BitVector32;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Timer = System.Windows.Forms.Timer;

namespace C3AP_Client
{
    public partial class Form1 : Form
    {
        private bool _dragging = false;
        private Point _start_point = new Point(0, 0);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int nSize, out IntPtr lpNumberOfBytesWritten);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr OpenProcess(uint processAccess, bool bInheritHandle, int processId);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [Out] byte[] lpBuffer, int nSize, out IntPtr lpNumberOfBytesRead);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool CloseHandle(IntPtr hObject);

        [DllImport("kernel32.dll")]
        static extern int VirtualQueryEx(IntPtr hProcess, IntPtr lpAddress, out MEMORY_BASIC_INFORMATION lpBuffer, uint dwLength);

        const uint PROCESS_VM_WRITE = 0x0020;
        const uint PROCESS_VM_OPERATION = 0x0008;
        const uint PROCESS_VM_READ = 0x0010;
        const uint PROCESS_QUERY_INFORMATION = 0x0400;
        const int MEM_COMMIT = 0x1000;
        const int PAGE_READWRITE = 0x04;
        const int PAGE_EXECUTE_READWRITE = 0x40;

        // Entspricht exakt der m_end_address = 0x200000 (2 MB PS1-RAM) aus der DuckStation-Header-Datei
        const long PS1_RAM_SIZE = 0x200000;

        private int selectedBTN = 0;

        private bool isCollapsed = true;
        private Timer menuAni = new Timer();
        private ItemPopup itemPopup = new ItemPopup();

        private byte[] byte1Buffer = new byte[4];
        private byte[] byte4Buffer = new byte[4];

        private byte[] valueBuffer = new byte[1];
        private byte[] crystalBuffer = new byte[4];
        private byte[] cleargemBuffer = new byte[5];

        private bool isProcess;
        private IntPtr targetProcess;
        private IntPtr ramBase;
        private long currentGoalSetting;

        [StructLayout(LayoutKind.Sequential)]
        struct MEMORY_BASIC_INFORMATION
        {
            public IntPtr BaseAddress;
            public IntPtr AllocationBase;
            public uint AllocationProtect;
            public IntPtr RegionSize;
            public uint State;
            public uint Protect;
            public uint Type;
        }

        public Form1()
        {
            InitializeComponent();
            menuAni.Interval = 5;
            menuAni.Tick += new EventHandler(menuAnimation);
        }

        private void menuAnimation(object sender, EventArgs e)
        {
            if (isCollapsed)
            {
                panelSettings.Width += 10;
                if (panelSettings.Width >= 290)
                {
                    panelSettings.Width = 290;
                    isCollapsed = false;
                    menuAni.Stop();
                }
            }
            else
            {
                panelSettings.Width -= 10;
                if (panelSettings.Width <= 0)
                {
                    panelSettings.Width = 0;
                    isCollapsed = true;
                    menuAni.Stop();
                }
            }
        }

        public static IntPtr GetProcess()
        {
            Process[] processes = Process.GetProcessesByName("duckstation-qt-x64-ReleaseLTCG");

            if (processes.Length == 0)
            {
                return IntPtr.Zero;
            }
            Process proc = processes[0];

            if (proc != null)
            {
                IntPtr processHandle = OpenProcess(PROCESS_VM_READ | PROCESS_VM_WRITE | PROCESS_QUERY_INFORMATION | PROCESS_VM_OPERATION, false, proc.Id);
                return processHandle;
            }

            return IntPtr.Zero;
        }

        public static IntPtr FindRamBySignature(IntPtr hProcess)
        {
            // Die funktionierende Byte-Signatur, die den RAM-Anfang direkt lokalisiert
            byte[] signature = new byte[] { 0x63, 0x64, 0x72, 0x6f, 0x6d, 0x3a, 0x5c, 0x53, 0x43 };

            IntPtr currentAddress = IntPtr.Zero;
            MEMORY_BASIC_INFORMATION mbi;

            while (VirtualQueryEx(hProcess, currentAddress, out mbi, (uint)Marshal.SizeOf(typeof(MEMORY_BASIC_INFORMATION))) != 0)
            {
                if (mbi.State == MEM_COMMIT && (mbi.Protect == PAGE_READWRITE || mbi.Protect == PAGE_EXECUTE_READWRITE))
                {
                    long regionSize = mbi.RegionSize.ToInt64();
                    if (regionSize > 4096 && regionSize < 100 * 1024 * 1024)
                    {
                        byte[] buffer = new byte[regionSize];
                        IntPtr bytesRead;

                        if (ReadProcessMemory(hProcess, mbi.BaseAddress, buffer, buffer.Length, out bytesRead))
                        {
                            int bytesReadInt = (int)bytesRead;
                            long baseAddrVal = mbi.BaseAddress.ToInt64();

                            for (int i = 0; i <= bytesReadInt - signature.Length; i++)
                            {
                                bool found = true;
                                for (int j = 0; j < signature.Length; j++)
                                {
                                    if (buffer[i + j] != signature[j])
                                    {
                                        found = false;
                                        break;
                                    }
                                }

                                if (found)
                                {
                                    // Zurückrechnen zur echten PS1-RAM-Basis (Offset 0x69320)
                                    long ramBaseInt = baseAddrVal + i - 0xb8b0;
                                    return new IntPtr(ramBaseInt);
                                }
                            }
                        }
                    }
                }

                long nextAddress = mbi.BaseAddress.ToInt64() + mbi.RegionSize.ToInt64();
                if (nextAddress <= currentAddress.ToInt64()) break;
                currentAddress = new IntPtr(nextAddress);
            }

            return IntPtr.Zero;
        }

        private static string GetRegion(IntPtr ramBase, IntPtr targetProcess)
        {
            IntPtr bytesRead;
            byte[] regionBuffer = new byte[6];
            IntPtr targetAddress = IntPtr.Add(ramBase, 0xB8B9);
            bool success = ReadProcessMemory(targetProcess, targetAddress, regionBuffer, regionBuffer.Length, out bytesRead);
            if (success)
            {
                string region = System.Text.Encoding.ASCII.GetString(regionBuffer);
                if (region == "US_942")
                {
                    GameConfig.LoadUSAddresses();
                    return "US";
                }
                else
                {
                    GameConfig.LoadEUAddresses();
                    return "EU";
                }
            }
            return "Error";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (isProcessRunning().Length != 0) { isProcess = true; }
            targetProcess = GetProcess();
            ramBase = FindRamBySignature(GetProcess());
            regionLabel.Text = "Game Region: " + GetRegion(ramBase, targetProcess);

            if (GetRegion(ramBase, targetProcess) == "US") { regionFlag.Image = C3AP_Client.Properties.Resources.USCA; }
            else { regionFlag.Image = C3AP_Client.Properties.Resources.EU; }

            timer1.Enabled = true;

            hostName.Text = Properties.Settings.Default.hostName;
            slotName.Text = Properties.Settings.Default.slotName;
            password.Text = Properties.Settings.Default.hostPass;

            panel_Log.Location = new Point(0, 0);
            panel_Hints.Location = new Point(0, 0);
            panel_ReceivedItems.Location = new Point(0, 0);
            panel_gamestats.Location = new Point(0, 0);
            panel_Hints.Visible = false;
            panel_ReceivedItems.Visible = false;
            panel_gamestats.Visible = false;

        }

        private Process[] isProcessRunning()
        {
            return Process.GetProcessesByName("duckstation-qt-x64-ReleaseLTCG");

        }

        private int getLevelID(IntPtr offsetAddress)
        {
            IntPtr bytesRead;
            IntPtr targetAddress = IntPtr.Add(ramBase, (int)offsetAddress);
            bool success = ReadProcessMemory(targetProcess, targetAddress, valueBuffer, valueBuffer.Length, out bytesRead);
            if (success) { return valueBuffer[0]; }
            return 0;
        }

        private bool isInLevel()
        {
            if (SharedAdresses.LevelIDs.ContainsValue(getLevelID((int)LevelIDAddress)) && getLevelID((int)LevelIDAddress) != 2) { return true; }
            return false;
        }

        private byte[] getCrystalCheck(IntPtr offsetAddress)
        {

            IntPtr bytesRead;
            IntPtr targetAddress = IntPtr.Add(ramBase, (int)offsetAddress);
            bool success = ReadProcessMemory(targetProcess, targetAddress, crystalBuffer, crystalBuffer.Length, out bytesRead);
            if (success) { return crystalBuffer; }
            return crystalBuffer;
        }

        private byte[] getCrystalReserve(IntPtr offsetAddress)
        {

            IntPtr bytesRead;
            IntPtr targetAddress = IntPtr.Add(ramBase, (int)offsetAddress);
            bool success = ReadProcessMemory(targetProcess, targetAddress, crystalBuffer, crystalBuffer.Length, out bytesRead);
            if (success) { return crystalBuffer; }
            return crystalBuffer;
        }

        private int getGameState(IntPtr offsetAddress) 
        {
            int state = 0;
            IntPtr bytesRead;
            IntPtr targetAddress = IntPtr.Add(ramBase, (int)offsetAddress);
            bool success = ReadProcessMemory(targetProcess, targetAddress, byte1Buffer, byte1Buffer.Length, out bytesRead);
            if (success)
            { state = byte1Buffer[0]; }
            return state;
        }

        private byte[] getClearGemReserve(IntPtr offsetAddress)
        {

            IntPtr bytesRead;
            IntPtr targetAddress = IntPtr.Add(ramBase, (int)offsetAddress);
            bool success = ReadProcessMemory(targetProcess, targetAddress, cleargemBuffer, cleargemBuffer.Length, out bytesRead);
            if (success) { return cleargemBuffer; }
            return cleargemBuffer;
        }

        private byte[] getColoredGemReserve(IntPtr offsetAddress)
        {

            IntPtr bytesRead;
            IntPtr targetAddress = IntPtr.Add(ramBase, (int)offsetAddress);
            bool success = ReadProcessMemory(targetProcess, targetAddress, byte1Buffer, byte1Buffer.Length, out bytesRead);
            if (success) { return byte1Buffer; }
            return byte1Buffer;
        }

        private byte[] getRelicSGeReserve(IntPtr offsetAddress)
        {

            IntPtr bytesRead;
            IntPtr targetAddress = IntPtr.Add(ramBase, (int)offsetAddress);
            bool success = ReadProcessMemory(targetProcess, targetAddress, byte4Buffer, byte4Buffer.Length, out bytesRead);
            if (success) { return byte4Buffer; }
            return byte4Buffer;
        }

        private bool isCrystalSaved(string levelName)
        {
            byte[] savedcrystals = getCrystalReserve((int)CrystalSavedAddress);
            if (levelName != null)
            {
                if (CrystalBits.crystalData.ContainsKey(levelName))
                {

                    var data = CrystalBits.crystalData[levelName];

                    int byteIndex = data.Item1;
                    int bitPosition = data.Item2;

                    return (savedcrystals[byteIndex] & (1 << bitPosition)) != 0;
                }
            }

            return false;
        }
        private bool isCrystalSave(long APItemID)
        {
            byte[] savecyrstal = getCrystalReserve((int)CrystalSavedAddress);
            var match = SharedAdresses.CrystalItems.FirstOrDefault(item => item.LevelAPItemId == APItemID);

            if (match != null)
            {
                string levelName = match.LevelName;
                if (CrystalBits.crystalData.ContainsKey(levelName))
                {
                    var data = CrystalBits.crystalData[levelName];

                    int byteIndex = data.Item1;
                    int bitPosition = data.Item2;

                    return (savecyrstal[byteIndex] & (1 << bitPosition)) != 0;
                }
            }
            return false;
        }

        private bool isClearGemBoxSave(long APItemID)
        {
            byte[] saveclearboxgem = getClearGemReserve((int)GemSavedAddress);
            var match = SharedAdresses.ClearGemBoxItems.FirstOrDefault(item => item.LevelAPItemId == APItemID);

            if (match != null)
            {
                string levelName = match.LevelName;
                if (CrystalBits.cleargemBoxData.ContainsKey(levelName))
                {
                    var data = CrystalBits.cleargemBoxData[levelName];

                    int byteIndex = data.Item1;
                    int bitPosition = data.Item2;

                    return (saveclearboxgem[byteIndex] & (1 << bitPosition)) != 0;
                }
            }
            return false;
        }

        private bool isClearGemSave(long APItemID)
        {
            byte[] savecleargem = getClearGemReserve((int)GemSavedAddress);
            var match = SharedAdresses.ClearGemItems.FirstOrDefault(item => item.LevelAPItemId == APItemID);

            if (match != null)
            {
                string levelName = match.LevelName;
                if (CrystalBits.cleargemData.ContainsKey(levelName))
                {
                    var data = CrystalBits.cleargemData[levelName];

                    int byteIndex = data.Item1;
                    int bitPosition = data.Item2;

                    return (savecleargem[byteIndex] & (1 << bitPosition)) != 0;
                }
            }
            return false;
        }

        private bool isColoredGemSave(long APItemID)
        {
            byte[] savecoloredgem = getColoredGemReserve((int)ColoredGemSavedAddress);
            var match = SharedAdresses.ColoredGemItems.FirstOrDefault(item => item.LevelAPItemId == APItemID);

            if (match != null)
            {
                string levelName = match.LevelName;
                if (CrystalBits.colorgemData.ContainsKey(levelName))
                {
                    var data = CrystalBits.colorgemData[levelName];

                    int byteIndex = data.Item1;
                    int bitPosition = data.Item2;

                    return (savecoloredgem[byteIndex] & (1 << bitPosition)) != 0;
                }
            }
            return false;
        }

        private bool isRelicSapphireSave(long APItemID)
        {
            byte[] saverelicS = getRelicSGeReserve((int)RelicsSapphireSavedAddress);
            var match = SharedAdresses.RelicSItems.FirstOrDefault(item => item.LevelAPItemId == APItemID);

            if (match != null)
            {
                string levelName = match.LevelName;
                if (CrystalBits.relicSGData.ContainsKey(levelName))
                {
                    var data = CrystalBits.relicSGData[levelName];

                    int byteIndex = data.Item1;
                    int bitPosition = data.Item2;

                    return (saverelicS[byteIndex] & (1 << bitPosition)) != 0;
                }
            }
            return false;
        }

        private bool isRelicGoldSave(long APItemID)
        {
            byte[] saverelicS = getRelicSGeReserve((int)RelicsGoldSavedAddress);
            var match = SharedAdresses.RelicGItems.FirstOrDefault(item => item.LevelAPItemId == APItemID);

            if (match != null)
            {
                string levelName = match.LevelName;
                if (CrystalBits.relicSGData.ContainsKey(levelName))
                {
                    var data = CrystalBits.relicSGData[levelName];

                    int byteIndex = data.Item1;
                    int bitPosition = data.Item2;

                    return (saverelicS[byteIndex] & (1 << bitPosition)) != 0;
                }
            }
            return false;
        }

        private bool isCrystalReserved(string levelName)
        {
            byte[] reservecrystals = getCrystalReserve((int)CyrstalReceivedAddress);
            if (levelName != null)
            {
                if (CrystalBits.crystalData.ContainsKey(levelName))
                {
                    var data = CrystalBits.crystalData[levelName];

                    int byteIndex = data.Item1;
                    int bitPosition = data.Item2;

                    return (reservecrystals[byteIndex] & (1 << bitPosition)) != 0;
                }
            }

            return false;
        }

        private bool isClearGemBoxReserved(string levelName)
        {
            byte[] reservecleargem = getClearGemReserve((int)GemReceivedAddress);
            if (levelName != null)
            {
                if (CrystalBits.cleargemBoxData.ContainsKey(levelName))
                {
                    var data = CrystalBits.cleargemBoxData[levelName];

                    int byteIndex = data.Item1;
                    int bitPosition = data.Item2;

                    return (reservecleargem[byteIndex] & (1 << bitPosition)) != 0;
                }
            }

            return false;
        }

        private bool isClearGemReserved(string levelName)
        {
            byte[] reservecleargem = getClearGemReserve((int)GemReceivedAddress);
            if (levelName != null)
            {
                if (CrystalBits.cleargemData.ContainsKey(levelName))
                {
                    var data = CrystalBits.cleargemData[levelName];

                    int byteIndex = data.Item1;
                    int bitPosition = data.Item2;

                    return (reservecleargem[byteIndex] & (1 << bitPosition)) != 0;
                }
            }

            return false;
        }

        private bool isColoredGemReserved(string levelName)
        {
            byte[] reservecleargem = getColoredGemReserve((int)ColoredGemReceivedAddress);
            if (levelName != null)
            {
                if (CrystalBits.colorgemData.ContainsKey(levelName))
                {
                    var data = CrystalBits.colorgemData[levelName];

                    int byteIndex = data.Item1;
                    int bitPosition = data.Item2;

                    return (reservecleargem[byteIndex] & (1 << bitPosition)) != 0;
                }
            }

            return false;
        }

        private bool isRelicSapphireReserved(string levelName)
        {
            byte[] reserverelicS = getRelicSGeReserve((int)RelicsSapphireReceivedAddress);
            if (levelName != null)
            {
                if (CrystalBits.relicSGData.ContainsKey(levelName))
                {
                    var data = CrystalBits.relicSGData[levelName];

                    int byteIndex = data.Item1;
                    int bitPosition = data.Item2;

                    return (reserverelicS[byteIndex] & (1 << bitPosition)) != 0;
                }
            }

            return false;
        }

        private bool isRelicGoldReserved(string levelName)
        {
            byte[] reserverelicG = getRelicSGeReserve((int)RelicsGoldReceivedAddress);
            if (levelName != null)
            {
                if (CrystalBits.relicSGData.ContainsKey(levelName))
                {
                    var data = CrystalBits.relicSGData[levelName];

                    int byteIndex = data.Item1;
                    int bitPosition = data.Item2;

                    return (reserverelicG[byteIndex] & (1 << bitPosition)) != 0;
                }
            }

            return false;
        }

        private bool deleteCrystalReserved(string levelName, IntPtr offsetAddress)
        {
            IntPtr bytesWritten;
            IntPtr targetAddress = IntPtr.Add(ramBase, (int)offsetAddress);

            if (levelName != null && CrystalBits.crystalData.ContainsKey(levelName))
            {
                var data = CrystalBits.crystalData[levelName];
                int byteIndex = data.Item1;
                int bitPosition = data.Item2;

                byte[] reservecrystals = getCrystalReserve(offsetAddress);

                reservecrystals[byteIndex] &= (byte)~(1 << bitPosition);

                bool success = WriteProcessMemory(targetProcess, targetAddress, reservecrystals, reservecrystals.Length, out bytesWritten);

                return success;
            }

            return false;
        }

        private bool deleteClearGemBoxReserved(string levelName, IntPtr offsetAddress)
        {
            IntPtr bytesWritten;
            IntPtr targetAddress = IntPtr.Add(ramBase, (int)offsetAddress);

            if (levelName != null && CrystalBits.cleargemBoxData.ContainsKey(levelName))
            {
                var data = CrystalBits.cleargemBoxData[levelName];
                int byteIndex = data.Item1;
                int bitPosition = data.Item2;

                
                byte[] reservecrystals = getClearGemReserve(offsetAddress);

                
                reservecrystals[byteIndex] &= (byte)~(1 << bitPosition);

                
                bool success = WriteProcessMemory(targetProcess, targetAddress, reservecrystals, reservecrystals.Length, out bytesWritten
                );

                return success;
            }

            return false;
        }

        private bool deleteClearGemReserved(string levelName, IntPtr offsetAddress)
        {
            IntPtr bytesWritten;
            IntPtr targetAddress = IntPtr.Add(ramBase, (int)offsetAddress);

            if (levelName != null && CrystalBits.cleargemData.ContainsKey(levelName))
            {
                var data = CrystalBits.cleargemData[levelName];
                int byteIndex = data.Item1;
                int bitPosition = data.Item2;

                byte[] reservecrystals = getClearGemReserve(offsetAddress);

                reservecrystals[byteIndex] &= (byte)~(1 << bitPosition);


                bool success = WriteProcessMemory(
                    targetProcess,
                    targetAddress,
                    reservecrystals,
                    reservecrystals.Length,
                    out bytesWritten
                );

                return success;
            }

            return false;
        }

        private bool deleteColoredGemReserved(string levelName, IntPtr offsetAddress)
        {
            IntPtr bytesWritten;
            IntPtr targetAddress = IntPtr.Add(ramBase, (int)offsetAddress);

            if (levelName != null && CrystalBits.colorgemData.ContainsKey(levelName))
            {
                var data = CrystalBits.colorgemData[levelName];
                int byteIndex = data.Item1;
                int bitPosition = data.Item2;

                byte[] reservecrystals = getColoredGemReserve(offsetAddress);

                reservecrystals[byteIndex] &= (byte)~(1 << bitPosition);

                bool success = WriteProcessMemory(targetProcess, targetAddress, reservecrystals, reservecrystals.Length, out bytesWritten);

                return success;
            }

            return false;
        }

        private bool deleteRelicSGReserved(string levelName, IntPtr offsetAddress)
        {
            IntPtr bytesWritten;
            IntPtr targetAddress = IntPtr.Add(ramBase, (int)offsetAddress);

            if (levelName != null && CrystalBits.relicSGData.ContainsKey(levelName))
            {
                var data = CrystalBits.relicSGData[levelName];
                int byteIndex = data.Item1;
                int bitPosition = data.Item2;

                byte[] reserverelic = getRelicSGeReserve(offsetAddress);

                reserverelic[byteIndex] &= (byte)~(1 << bitPosition);

                bool success = WriteProcessMemory(targetProcess, targetAddress, reserverelic, reserverelic.Length, out bytesWritten);

                return success;
            }

            return false;
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            if (isProcessRunning().Length != 0) { isProcess = true; } else { isProcess = false; }
                int levelNumber = getLevelID((int)LevelIDAddress);
            int levelNumberLate = getLevelID((int)LateLevelIDAddress);
            string levelName = LevelIDs.FirstOrDefault(x => x.Value == valueBuffer[0]).Key;
            string levelNameLate = LevelIDs.FirstOrDefault(x => x.Value == valueBuffer[0]).Key;
            levelID.Text = "LevelID: " + valueBuffer[0] + " [" + levelName + "] is in State: " + getGameState((int)GameStateAddress);
            latelevelId.Text = "LevelID: " + valueBuffer[0] + " [" + levelNameLate + "] is in State: " + getGameState((int)GameStateAddress);


            if (isProcess)
            {
                dsstatus.Text = "Duckstation Status: Is Running";
                duck_status.Image = C3AP_Client.Properties.Resources.duckstation_on;
            }
            else { dsstatus.Text = "Duckstation Status: Is not Running"; duck_status.Image = C3AP_Client.Properties.Resources.duckstation_off; }


            bool isCollectedR = isCrystalReserved(levelName);

            if (isCollectedR)
            {
                crystalReserve.Text = "Crystal in current level " + levelName + " in reserved.";
            }
            else
            {
                crystalReserve.Text = "Crystal in current level " + levelName + "  is not reserved.";
            }

            bool isCollected = isCrystalSaved(levelName);

            if (isCollected)
            {
                crystal.Text = "Crystal in current level " + levelName + " collected!";
            }
            else
            {
                crystal.Text = "Crystal in current level " + levelName + " missing.";
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(commandBox.Text) && APManager.IsConnected)
            {
                string message = commandBox.Text;

                APManager.Session.Socket.SendPacket(new SayPacket() { Text = message });

                commandBox.Clear();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (hostName.Text != "")
            {
                if (slotName.Text != "")
                {
                    string[] hostAddresse = hostName.Text.Split(':');
                    var session = ArchipelagoSessionFactory.CreateSession(hostAddresse[0], Int32.Parse(hostAddresse[1])); 
                    LoginResult result = session.TryConnectAndLogin(
                    game: "Crash Bandicoot: Warped",      
                    name: slotName.Text,
                    itemsHandlingFlags: ItemsHandlingFlags.AllItems,
                    tags: new string[] { "AP" }
                    );

                    if (result.Successful)
                    {
                        APManager.Session = session;
                    }

                    if (result is LoginSuccessful successful)
                    {
                        // Slot-Daten für das Ziel auslesen
                        if (successful.SlotData.ContainsKey("goal"))
                        {
                            currentGoalSetting = Convert.ToInt64(successful.SlotData["goal"]);
                        }
                    }
                    LoadItemsFromJson();
                    alreadyReceivedItems();
                    timer2.Enabled = true;

                    session.Items.ItemReceived += (receivedItemsHelper) =>
                    {
                        
                        var nextItem = session.Items.AllItemsReceived.Last();
                        long itemId = nextItem.ItemId;
                        ItemFlags flags = nextItem.Flags;

                        string itemName = session.Items.GetItemName(itemId);

                        int senderPlayerId = nextItem.Player;
                        string locationName = nextItem.LocationName;

                        string senderName = session.Players.GetPlayerName(senderPlayerId);
                        //string senderName1 = session.Players.;

                        OnItemReceived(itemId);

                        AppendColoredLog(slotName.Text, itemName, flags, senderName, locationName);

                        AppendReceivedItemBox(itemName);

                        SaveItemToJson(itemId,slotName.Text,itemName,senderName,locationName,flags);


                        //richTextBox1.AppendText($"Item empfangen: {itemName} (ID: {itemId.ToString()})!");

                        receivedItemsHelper.DequeueItem();
                    };
                }

            }


            MessageBox.Show("Erfolgreich mit dem AP-Server verbunden!");
            Console.WriteLine("Erfolgreich mit dem AP-Server verbunden!");

        }
        private void AppendReceivedItemBox(string text)
        {
            if (receivedItemBox.InvokeRequired)
            {
                receivedItemBox.Invoke(new Action<string>(AppendReceivedItemBox), new object[] { text });
                return;
            }

            receivedItemBox.AppendText(text + Environment.NewLine);
        }

        public class ReceivedItemData
        {
            public long ItemId { get; set; }
            public string PlayerName { get; set; }
            public string ItemName { get; set; }
            public string SenderName { get; set; }
            public string LocationName { get; set; }
            public ItemFlags ItemFlags { get; set; }
        }

        private void SaveItemToJson(long itemId,string playerName, string itemName, string senderName, string locationName, ItemFlags flags)
        {
            var itemEntry = new ReceivedItemData
            {
                ItemId = itemId,
                ItemName = itemName,
                ItemFlags = flags,
                PlayerName = playerName,
                SenderName = senderName,
                LocationName = locationName
            };
            string jsonString = JsonSerializer.Serialize(itemEntry);

            File.AppendAllLines("apreceiveditems.c3apsave", new[] { jsonString });
        }

        private void LoadItemsFromJson()
        {
            string logFilePath = "apreceiveditems.c3apsave";

            if (!File.Exists(logFilePath)) return;

            foreach (var line in File.ReadLines(logFilePath))
            {
                try
                {
                    var item = JsonSerializer.Deserialize<ReceivedItemData>(line);

                    if (item != null)
                    {
                        receivedItemBox.AppendText(item.ItemName + Environment.NewLine);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Fehler beim Laden eines Log-Eintrags: " + ex.Message);
                }
            }
        }

        private void richTextBox1_GotFocus(object sender, EventArgs e)
        {
            
            this.ActiveControl = crystal;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //logBox.AppendText("Hello");

            setNonPlayedItems(142023);
            MessageBox.Show(isCrystalSave(142023).ToString());

            //sendToPopup(142067, "Test", "Test");
            //setNonPlayedItems();
            //setGameItems(142001, (int)CrystalSavedAddress, (int)CyrstalReceivedAddress);

            //MessageBox.Show(isInLevel().ToString());

            //setCrystalInGame(142000, (int)GemSavedAddress, (int)GemReceivedAddress);

        }

        private void sendToPopup(long APItemId, string itemName, string itemLocation)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() =>
                {
                    itemPopup.EnqueueItem(APItemId, itemName, itemLocation);
                }));
            }
            else
            {
                itemPopup.EnqueueItem(APItemId, itemName, itemLocation);
            }
        }

        private void sendExitCheck(string levelName) 
        {
            if(getGameState((int)GameStateAddress) == 3) 
            {
                if (APManager.IsConnected)
                {
                    
                    if (SharedAdresses.LevelAPLocationIDs.TryGetValue(levelName, out long locationId))
                    {
                        APManager.Session.Locations.CompleteLocationChecks(new[] { locationId });
                    }
                }
            }
        }
        private void sendCrystalCheck(string levelName)
        {

            if (isCrystalReserved(levelName))
            {
                if (APManager.IsConnected)
                {
                    var foundCrystal = SharedAdresses.CrystalItems.FirstOrDefault(c => c.LevelName == levelName);
                    if (foundCrystal != null)
                    {
                        bool isLocationChecked = APManager.Session.Items.AllItemsReceived.Any(item => item.ItemId == foundCrystal.LevelAPItemId);
                        if (isLocationChecked == false)
                        {
                            deleteCrystalReserved(levelName, (int)CyrstalReceivedAddress);
                        }
                        APManager.Session.Locations.CompleteLocationChecks(new[] { foundCrystal.LevelAPCheckId });
                    }
                }
            }
        }

        private void sendClearGemBoxCheck(string levelName)
        {
            if (isClearGemBoxReserved(levelName))
            {
                if (APManager.IsConnected)
                {
                    var foundCrystal = SharedAdresses.ClearGemBoxItems.FirstOrDefault(c => c.LevelName == levelName);
                    if (foundCrystal != null)
                    {
                        bool isLocationChecked = APManager.Session.Items.AllItemsReceived.Any(item => item.ItemId == foundCrystal.LevelAPItemId);
                        if (isLocationChecked == false)
                        {
                            deleteClearGemBoxReserved(levelName, (int)GemReceivedAddress);
                        }
                        APManager.Session.Locations.CompleteLocationChecks(new[] { foundCrystal.LevelAPCheckId });
                    }
                }
            }
        }

        private void sendClearGemCheck(string levelName)
        {
            if (isClearGemReserved(levelName))
            {
                if (APManager.IsConnected)
                {
                    var foundCrystal = SharedAdresses.ClearGemItems.FirstOrDefault(c => c.LevelName == levelName);
                    if (foundCrystal != null)
                    {
                        bool isLocationChecked = APManager.Session.Items.AllItemsReceived.Any(item => item.ItemId == foundCrystal.LevelAPItemId);
                        if (isLocationChecked == false)
                        {
                            deleteClearGemReserved(levelName, (int)GemReceivedAddress);
                        }
                        APManager.Session.Locations.CompleteLocationChecks(new[] { foundCrystal.LevelAPCheckId });
                    }
                }
            }
        }

        private void sendColoredGemCheck(string levelName)
        {
            if (isColoredGemReserved(levelName))
            {
                if (APManager.IsConnected)
                {
                    var foundCrystal = SharedAdresses.ColoredGemItems.FirstOrDefault(c => c.LevelName == levelName);
                    if (foundCrystal != null)
                    {
                        bool isLocationChecked = APManager.Session.Items.AllItemsReceived.Any(item => item.ItemId == foundCrystal.LevelAPItemId);
                        if (isLocationChecked == false)
                        {
                            deleteColoredGemReserved(levelName, (int)ColoredGemReceivedAddress);
                        }
                        APManager.Session.Locations.CompleteLocationChecks(new[] { foundCrystal.LevelAPCheckId });
                    }
                }
            }
        }

        private void sendRelicSapphireCheck(string levelName)
        {
            if (isRelicSapphireReserved(levelName))
            {
                if (APManager.IsConnected)
                {
                    var foundCrystal = SharedAdresses.RelicSItems.FirstOrDefault(c => c.LevelName == levelName);
                    if (foundCrystal != null)
                    {
                        bool isLocationChecked = APManager.Session.Items.AllItemsReceived.Any(item => item.ItemId == foundCrystal.LevelAPItemId);
                        if (isLocationChecked == false)
                        {
                            deleteRelicSGReserved(levelName, (int)RelicsSapphireReceivedAddress);
                        }
                        APManager.Session.Locations.CompleteLocationChecks(new[] { foundCrystal.LevelAPCheckId });
                    }
                }
            }
        }

        private void sendRelicGoldCheck(string levelName)
        {
            if (isRelicGoldReserved(levelName))
            {
                if (APManager.IsConnected)
                {
                    var foundCrystal = SharedAdresses.RelicGItems.FirstOrDefault(c => c.LevelName == levelName);
                    if (foundCrystal != null)
                    {
                        bool isLocationChecked = APManager.Session.Items.AllItemsReceived.Any(item => item.ItemId == foundCrystal.LevelAPItemId);
                        if (isLocationChecked == false)
                        {
                            deleteRelicSGReserved(levelName, (int)RelicsGoldReceivedAddress);
                        }
                        APManager.Session.Locations.CompleteLocationChecks(new[] { foundCrystal.LevelAPCheckId });
                    }
                }
            }
        }

        private void sendRelicPlatinumCheck(string levelName)
        {
            if (isRelicSapphireReserved(levelName) && isRelicGoldReserved(levelName))
            {
                if (APManager.IsConnected)
                {
                    var foundCrystal = SharedAdresses.RelicPItems.FirstOrDefault(c => c.LevelName == levelName);
                    if (foundCrystal != null)
                    {
                        bool isLocationChecked = APManager.Session.Items.AllItemsReceived.Any(item => item.ItemId == foundCrystal.LevelAPItemId);
                        if (isLocationChecked == false)
                        {
                            deleteRelicSGReserved(levelName, (int)RelicsSapphireReceivedAddress);
                            deleteRelicSGReserved(levelName, (int)RelicsGoldReceivedAddress);
                        }
                        APManager.Session.Locations.CompleteLocationChecks(new[] { foundCrystal.LevelAPCheckId });
                    }
                }
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            var levelEntry = LevelIDs.FirstOrDefault(x => x.Value == valueBuffer[0]);

            if (levelEntry.Equals(default(KeyValuePair<string, int>))) return;

            string levelName = levelEntry.Key;

            sendExitCheck(levelName);
            sendCrystalCheck(levelName);
            sendClearGemBoxCheck(levelName);
            sendClearGemCheck(levelName);
            sendColoredGemCheck(levelName);
            sendRelicSapphireCheck(levelName);
            sendRelicGoldCheck(levelName);
            sendRelicPlatinumCheck(levelName);

            if (APManager.IsConnected)
            {
                CheckGameMemoryForGoal(currentGoalSetting);
            }


        }

        private void setNonPlayedItems(long APItemID)
        {
            if (!APManager.IsConnected) return;

            if (SharedAdresses.CrystalItems.Any(item => item.LevelAPItemId == APItemID))
            {
                if (!isCrystalSave(APItemID))
                {
                    setGameItems(APItemID, (int)CrystalSavedAddress, (int)CyrstalReceivedAddress);
                }
            }

            else if (SharedAdresses.ClearGemBoxItems.Any(item => item.LevelAPItemId == APItemID))
            {
                if (!isClearGemBoxSave(APItemID))
                {
                    setGameItems(APItemID, (int)GemSavedAddress, (int)GemReceivedAddress);
                }
            }

            else if (SharedAdresses.ClearGemItems.Any(item => item.LevelAPItemId == APItemID))
            {
                if (!isClearGemSave(APItemID))
                {
                    setGameItems(APItemID, (int)GemSavedAddress, (int)GemReceivedAddress);
                }
            }
            // 4. Colored Gem prüfen
            else if (SharedAdresses.ColoredGemItems.Any(item => item.LevelAPItemId == APItemID))
            {
                if (!isColoredGemSave(APItemID))
                {
                    setGameItems(APItemID, (int)ColoredGemSavedAddress, (int)ColoredGemReceivedAddress);
                }
            }

            else if (SharedAdresses.RelicSItems.Any(item => item.LevelAPItemId == APItemID))
            {
                if (!isRelicSapphireSave(APItemID))
                {
                    setGameItems(APItemID, (int)RelicsSapphireSavedAddress, (int)RelicsSapphireReceivedAddress);
                }
            }

            else if (SharedAdresses.RelicGItems.Any(item => item.LevelAPItemId == APItemID))
            {
                if (!isRelicGoldSave(APItemID))
                {
                    setGameItems(APItemID, (int)RelicsGoldSavedAddress, (int)RelicsGoldReceivedAddress);
                }
            }

            else if (SharedAdresses.RelicPItems.Any(item => item.LevelAPItemId == APItemID))
            {
                if (!isRelicSapphireSave(APItemID) && !isRelicGoldSave(APItemID))
                {
                    setGameItems(APItemID, (int)RelicsSapphireSavedAddress, (int)RelicsSapphireReceivedAddress);
                    setGameItems(APItemID, (int)RelicsGoldSavedAddress, (int)RelicsGoldReceivedAddress);
                }
            }
        }

        private byte[] get4ByteBuffer(IntPtr offsetAddress)
        {
            IntPtr bytesRead;
            IntPtr targetAddress = IntPtr.Add(ramBase, (int)offsetAddress);
            bool success = ReadProcessMemory(targetProcess, targetAddress, byte4Buffer, byte4Buffer.Length, out bytesRead);
            if (success) { return byte4Buffer; }
            return byte4Buffer;
        }


        private bool setPlatinumRelic(long itemID, IntPtr offsetSSavedAddress, IntPtr offsetSReserveAddress, IntPtr offsetGSavedAddress, IntPtr offsetGReserveAddress)
        {
            IntPtr bytesWritten;
            IntPtr savetargetSAddress = IntPtr.Add(ramBase, (int)offsetSSavedAddress);
            IntPtr targetSAddress = IntPtr.Add(ramBase, (int)offsetSReserveAddress);
            IntPtr savetargetGAddress = IntPtr.Add(ramBase, (int)offsetGSavedAddress);
            IntPtr targetGAddress = IntPtr.Add(ramBase, (int)offsetGReserveAddress);

            string levelNameRelicP = RelicPItems.FirstOrDefault(item => item.LevelAPItemId == itemID)?.LevelName;

            if (levelNameRelicP != null && CrystalBits.relicSGData.ContainsKey(levelNameRelicP))
            {
                var data = CrystalBits.relicSGData[levelNameRelicP];
                int byteIndex = data.Item1;
                int bitPosition = data.Item2;
                sendToPopup(itemID, "Platinium Relic", levelNameRelicP);
                var foundRelicP = SharedAdresses.RelicPItems.FirstOrDefault(c => c.LevelName == levelNameRelicP);
                APManager.Session.Locations.CompleteLocationChecks(new[] { foundRelicP.LevelAPCheckId });

                if (isInLevel())
                {
                    byte[] savedrelicS = get4ByteBuffer(offsetSReserveAddress);
                    savedrelicS[byteIndex] |= (byte)(1 << bitPosition);
                    bool successS = WriteProcessMemory(targetProcess, targetSAddress, savedrelicS, savedrelicS.Length, out bytesWritten);

                    byte[] savedrelicG = get4ByteBuffer(offsetGReserveAddress);
                    savedrelicG[byteIndex] |= (byte)(1 << bitPosition);
                    bool successG = WriteProcessMemory(targetProcess, targetGAddress, savedrelicG, savedrelicG.Length, out bytesWritten);
                    return successG;
                }
                else
                {
                    byte[] savedrelicS = get4ByteBuffer(offsetSSavedAddress);
                    savedrelicS[byteIndex] |= (byte)(1 << bitPosition);
                    bool successS = WriteProcessMemory(targetProcess, savetargetSAddress, savedrelicS, savedrelicS.Length, out bytesWritten);

                    byte[] savedrelicG = get4ByteBuffer(offsetGSavedAddress);
                    savedrelicG[byteIndex] |= (byte)(1 << bitPosition);
                    bool successG = WriteProcessMemory(targetProcess, savetargetGAddress, savedrelicG, savedrelicG.Length, out bytesWritten);
                    return successG;
                }
            }
            return false;
        }

        private bool setGameItems(long itemID, IntPtr offsetSavedAddress, IntPtr offsetReserveAddress)
        {
            IntPtr bytesWritten;
            IntPtr savetargetAddress = IntPtr.Add(ramBase, (int)offsetSavedAddress);
            IntPtr targetAddress = IntPtr.Add(ramBase, (int)offsetReserveAddress);

            string levelNameCrystal = CrystalItems.FirstOrDefault(item => item.LevelAPItemId == itemID)?.LevelName;
            string levelNameClearGemBox = ClearGemBoxItems.FirstOrDefault(item => item.LevelAPItemId == itemID)?.LevelName;
            string levelNameClearGem = ClearGemItems.FirstOrDefault(item => item.LevelAPItemId == itemID)?.LevelName;
            string levelNameColoredGem = ColoredGemItems.FirstOrDefault(item => item.LevelAPItemId == itemID)?.LevelName;
            string levelNameRelicS = RelicSItems.FirstOrDefault(item => item.LevelAPItemId == itemID)?.LevelName;
            string levelNameRelicG = RelicGItems.FirstOrDefault(item => item.LevelAPItemId == itemID)?.LevelName;
            //string levelNameRelicP = RelicGItems.FirstOrDefault(item => item.LevelAPItemId == itemID)?.LevelName;

            if (levelNameCrystal != null && CrystalBits.crystalData.ContainsKey(levelNameCrystal))
            {
                var data = CrystalBits.crystalData[levelNameCrystal];
                int byteIndex = data.Item1;
                int bitPosition = data.Item2;
                var foundCrystal = SharedAdresses.CrystalItems.FirstOrDefault(c => c.LevelName == levelNameCrystal);
                APManager.Session.Locations.CompleteLocationChecks(new[] { foundCrystal.LevelAPCheckId });
                sendToPopup(itemID, "Crystal", levelNameCrystal);

                if (isInLevel())
                {
                    byte[] savedcrystals = get4ByteBuffer(offsetReserveAddress);
                    savedcrystals[byteIndex] |= (byte)(1 << bitPosition);
                    bool success = WriteProcessMemory(targetProcess, targetAddress, savedcrystals, savedcrystals.Length, out bytesWritten);
                    return success;

                }
                else
                {
                    byte[] savedcrystals = get4ByteBuffer(offsetSavedAddress);
                    savedcrystals[byteIndex] |= (byte)(1 << bitPosition);
                    bool success = WriteProcessMemory(targetProcess, savetargetAddress, savedcrystals, savedcrystals.Length, out bytesWritten);
                    return success;
                }

            }

            //Clear Gems Boxes
            else if (levelNameClearGemBox != null && CrystalBits.cleargemBoxData.ContainsKey(levelNameClearGemBox))
            {
                var data = CrystalBits.cleargemBoxData[levelNameClearGemBox];
                int byteIndex = data.Item1;
                int bitPosition = data.Item2;
                sendToPopup(itemID, "Clear Gem (Box)", levelNameClearGemBox);
                var foundClearGemBox = SharedAdresses.ClearGemBoxItems.FirstOrDefault(c => c.LevelName == levelNameClearGemBox);
                APManager.Session.Locations.CompleteLocationChecks(new[] { foundClearGemBox.LevelAPCheckId });

                if (isInLevel())
                {
                    byte[] savedcrystals = getClearGemReserve(offsetReserveAddress);
                    savedcrystals[byteIndex] |= (byte)(1 << bitPosition);
                    bool success = WriteProcessMemory(targetProcess, targetAddress, savedcrystals, savedcrystals.Length, out bytesWritten);
                    return success;
                }
                else
                {
                    byte[] savedcrystals = getClearGemReserve(offsetSavedAddress);
                    savedcrystals[byteIndex] |= (byte)(1 << bitPosition);
                    bool success = WriteProcessMemory(targetProcess, savetargetAddress, savedcrystals, savedcrystals.Length, out bytesWritten);
                    return success;
                }
            }

            //Clear Gems
            else if (levelNameClearGem != null && CrystalBits.cleargemData.ContainsKey(levelNameClearGem))
            {
                var data = CrystalBits.cleargemData[levelNameClearGem];
                int byteIndex = data.Item1;
                int bitPosition = data.Item2;
                sendToPopup(itemID, "Clear Gem", levelNameClearGem);
                var foundClearGem = SharedAdresses.ClearGemItems.FirstOrDefault(c => c.LevelName == levelNameClearGem);
                APManager.Session.Locations.CompleteLocationChecks(new[] { foundClearGem.LevelAPCheckId });

                if (isInLevel())
                {
                    byte[] savedcrystals = getClearGemReserve(offsetReserveAddress);
                    savedcrystals[byteIndex] |= (byte)(1 << bitPosition);
                    bool success = WriteProcessMemory(targetProcess, targetAddress, savedcrystals, savedcrystals.Length, out bytesWritten);
                    return success;
                }
                else
                {
                    byte[] savedcrystals = getClearGemReserve(offsetSavedAddress);
                    savedcrystals[byteIndex] |= (byte)(1 << bitPosition);
                    bool success = WriteProcessMemory(targetProcess, savetargetAddress, savedcrystals, savedcrystals.Length, out bytesWritten);
                    return success;
                }
            }

            //Colored Gems
            else if (levelNameColoredGem != null && CrystalBits.colorgemData.ContainsKey(levelNameColoredGem))
            {
                var data = CrystalBits.colorgemData[levelNameColoredGem];
                int byteIndex = data.Item1;
                int bitPosition = data.Item2;
                sendToPopup(itemID, "Colored Gem", levelNameColoredGem);
                var foundColoredGem = SharedAdresses.ColoredGemItems.FirstOrDefault(c => c.LevelName == levelNameColoredGem);
                APManager.Session.Locations.CompleteLocationChecks(new[] { foundColoredGem.LevelAPCheckId });

                if (isInLevel())
                {
                    byte[] savedcrystals = getColoredGemReserve(offsetReserveAddress);
                    savedcrystals[byteIndex] |= (byte)(1 << bitPosition);
                    bool success = WriteProcessMemory(targetProcess, targetAddress, savedcrystals, savedcrystals.Length, out bytesWritten);
                    return success;
                }
                else
                {
                    byte[] savedcrystals = get4ByteBuffer(offsetSavedAddress);
                    savedcrystals[byteIndex] |= (byte)(1 << bitPosition);
                    bool success = WriteProcessMemory(targetProcess, savetargetAddress, savedcrystals, savedcrystals.Length, out bytesWritten);
                    return success;
                }
            }

            else if (levelNameRelicS != null && CrystalBits.relicSGData.ContainsKey(levelNameRelicS))
            {
                var data = CrystalBits.relicSGData[levelNameRelicS];
                int byteIndex = data.Item1;
                int bitPosition = data.Item2;
                sendToPopup(itemID, "Sapphhire Relic", levelNameRelicS);
                var foundRelicS = SharedAdresses.RelicSItems.FirstOrDefault(c => c.LevelName == levelNameRelicS);
                APManager.Session.Locations.CompleteLocationChecks(new[] { foundRelicS.LevelAPCheckId });

                if (isInLevel())
                {
                    byte[] savedcrystals = get4ByteBuffer(offsetReserveAddress);
                    savedcrystals[byteIndex] |= (byte)(1 << bitPosition);
                    bool success = WriteProcessMemory(targetProcess, targetAddress, savedcrystals, savedcrystals.Length, out bytesWritten);
                    return success;
                }
                else
                {
                    byte[] savedcrystals = get4ByteBuffer(offsetSavedAddress);
                    savedcrystals[byteIndex] |= (byte)(1 << bitPosition);
                    bool success = WriteProcessMemory(targetProcess, savetargetAddress, savedcrystals, savedcrystals.Length, out bytesWritten);
                    return success;
                }
            }

            else if (levelNameRelicG != null && CrystalBits.relicSGData.ContainsKey(levelNameRelicG))
            {
                var data = CrystalBits.relicSGData[levelNameRelicG];
                int byteIndex = data.Item1;
                int bitPosition = data.Item2;
                sendToPopup(itemID, "Gold Relic", levelNameRelicG);
                var foundRelicG = SharedAdresses.RelicGItems.FirstOrDefault(c => c.LevelName == levelNameRelicG);
                APManager.Session.Locations.CompleteLocationChecks(new[] { foundRelicG.LevelAPCheckId });

                if (isInLevel())
                {
                    byte[] savedcrystals = get4ByteBuffer(offsetReserveAddress);
                    savedcrystals[byteIndex] |= (byte)(1 << bitPosition);
                    bool success = WriteProcessMemory(targetProcess, targetAddress, savedcrystals, savedcrystals.Length, out bytesWritten);
                    return success;
                }
                else
                {
                    byte[] savedcrystals = get4ByteBuffer(offsetSavedAddress);
                    savedcrystals[byteIndex] |= (byte)(1 << bitPosition);
                    bool success = WriteProcessMemory(targetProcess, savetargetAddress, savedcrystals, savedcrystals.Length, out bytesWritten);
                    return success;
                }
            }

            return false;
        }

        private void OnItemReceived(long receivedItemId)
        {
            bool isCrystalItem = receivedItemId >= 142000 && receivedItemId <= 142024;

            bool isClearGemItem = receivedItemId >= 142025 && receivedItemId <= 142063;

            bool isColoredGemItem = receivedItemId >= 142064 && receivedItemId <= 142068;

            bool isRelicSapphireItem = receivedItemId >= 142069 && receivedItemId <= 142098;

            bool isRelicGoldItem = receivedItemId >= 142099 && receivedItemId <= 142128;

            bool isRelicPlatinumItem = receivedItemId >= 142129 && receivedItemId <= 142158;

            if (isCrystalItem)
            {
                setGameItems(receivedItemId, (int)CrystalSavedAddress, (int)CyrstalReceivedAddress);
            }
            else if (isClearGemItem)
            {
                setGameItems(receivedItemId, (int)GemSavedAddress, (int)GemReceivedAddress);
            }
            else if (isColoredGemItem)
            {
                setGameItems(receivedItemId, (int)ColoredGemSavedAddress, (int)ColoredGemReceivedAddress);
            }
            else if (isRelicSapphireItem)
            {
                setGameItems(receivedItemId, (int)RelicsSapphireSavedAddress, (int)RelicsSapphireReceivedAddress);
            }
            else if (isRelicGoldItem)
            {
                setGameItems(receivedItemId, (int)RelicsGoldSavedAddress, (int)RelicsGoldReceivedAddress);
            }
            else if (isRelicPlatinumItem)
            {
                setPlatinumRelic(receivedItemId, (int)RelicsSapphireSavedAddress, (int)RelicsSapphireReceivedAddress, (int)RelicsGoldSavedAddress, (int)RelicsGoldReceivedAddress);
            }
        }

        public static class APManager
        {
            
            public static ArchipelagoSession Session { get; set; }

            
            public static bool IsConnected => Session != null && Session.Socket.Connected;
        }

        private void NumbersOnly(object sender, KeyPressEventArgs e)
        {
            char numbersOnly = e.KeyChar;

            if (!Char.IsDigit(numbersOnly) && numbersOnly != 8)
            {
                e.Handled = true;
            }
        }
        private void alreadyReceivedItems()
        {
            if (APManager.IsConnected)
            {
                var receivedItems = APManager.Session.Items.AllItemsReceived;

                foreach (var item in receivedItems)
                {
                    setNonPlayedItems(item.ItemId);
                }
                ClientMessageBox.Show("Alle empfangenen Items wurden abgeglichen!", "Archipelago Status");
                MessageBox.Show("Alle empfangenen Items wurden mit dem Spielstand abgeglichen!", "Abgleich beendet");
            }
        }


        private void btnMenu(object sender, EventArgs e)
        {
            menuAni.Start();
        }

        private void onMouseDown(object sender, MouseEventArgs e)
        {
            _dragging = true;
            _start_point = new Point(e.X, e.Y);
        }
        private void onMouseMove(object sender, MouseEventArgs e)
        {
            if (_dragging)
            {
                Point p = PointToScreen(e.Location);
                Location = new Point(p.X - this._start_point.X, p.Y - this._start_point.Y);
            }
        }
        private void onMouseRelease(object sender, MouseEventArgs e)
        {
            _dragging = false;
        }

        private void btnLog_Click(object sender, EventArgs e)
        {
            switchBTN(0);
        }
        private void btnLogHover_Click(object sender, EventArgs e)
        {
            if (btn_labellog.ForeColor != System.Drawing.Color.White)
            {
                btn_labellog.ForeColor = System.Drawing.Color.Silver;
            }
        }

        private void btnLog_Leave(object sender, EventArgs e)
        {
            if (btn_selectlog.Visible) { btn_labellog.ForeColor = System.Drawing.Color.White; }
            else { btn_labellog.ForeColor = System.Drawing.Color.Gray; }
        }

        private void btnHints_Click(object sender, EventArgs e)
        {
            switchBTN(1);
        }
        private void btnHintsHover_Click(object sender, EventArgs e)
        {
            if (btn_labelhints.ForeColor != System.Drawing.Color.White)
            {
                btn_labelhints.ForeColor = System.Drawing.Color.Silver;
            }
        }

        private void btnHints_Leave(object sender, EventArgs e)
        {
            if (btn_selecthints.Visible) { btn_labelhints.ForeColor = System.Drawing.Color.White; }
            else { btn_labelhints.ForeColor = System.Drawing.Color.Gray; }
        }

        private void btnReceivedItems_Click(object sender, EventArgs e)
        {
            switchBTN(2);
        }
        private void btnReceivedItemsHover_Click(object sender, EventArgs e)
        {
            if (btn_labelreceivedItems.ForeColor != System.Drawing.Color.White)
            {
                btn_labelreceivedItems.ForeColor = System.Drawing.Color.Silver;
            }
        }
        private void btnReceivedItems_Leave(object sender, EventArgs e)
        {
            if (btn_selectrecItems.Visible) { btn_labelreceivedItems.ForeColor = System.Drawing.Color.White; }
            else { btn_labelreceivedItems.ForeColor = System.Drawing.Color.Gray; }
        }

        private void btnGame_Click(object sender, EventArgs e)
        {
            switchBTN(3);
        }

        private void btnGameStats_Hover(object sender, EventArgs e)
        {
            if (btn_labelgame.ForeColor != System.Drawing.Color.White)
            {
                btn_labelgame.ForeColor = System.Drawing.Color.Silver;
            }
        }
        private void btnGameStats_Leave(object sender, EventArgs e)
        {
            if (btn_selectgame.Visible) { btn_labelgame.ForeColor = System.Drawing.Color.White; }
            else { btn_labelgame.ForeColor = System.Drawing.Color.Gray; }
        }

        private void AppendColoredLog(string playerName, string itemName, ItemFlags itemFlags, string senderName, string locationName)
        {
            // 1. Thread-Sicherheit prüfen (Invoke)
            if (logBox.InvokeRequired)
            {
                logBox.Invoke(new Action<string, string, ItemFlags, string, string>(AppendColoredLog), new object[] { playerName, itemName, itemFlags, senderName, locationName });
                return;
            }
            
            if (playerName == senderName) 
            {
                logBox.SelectionColor = System.Drawing.Color.Beige;
                logBox.AppendText(playerName);

                logBox.SelectionColor = logBox.ForeColor;
                logBox.AppendText(" found their ");
            }
            else 
            {
                logBox.SelectionColor = System.Drawing.Color.Orange;
                logBox.AppendText(senderName);

                logBox.SelectionColor = logBox.ForeColor;
                logBox.AppendText(" sent ");
            }

            if (itemFlags.HasFlag(ItemFlags.Advancement))
            {
                logBox.SelectionColor = System.Drawing.Color.FromArgb(175, 153, 239);
            }
            else if (itemFlags.HasFlag(ItemFlags.None))
            {
                logBox.SelectionColor = System.Drawing.Color.FromArgb(0, 238, 238);
            }
            else if (itemFlags.HasFlag(ItemFlags.Trap))
            {
                logBox.SelectionColor = System.Drawing.Color.FromArgb(131, 63, 51);
            }
            else // Filler / Junk
            {
                logBox.SelectionColor = System.Drawing.Color.FromArgb(50, 131, 125);
            }

            logBox.AppendText(itemName);

            logBox.SelectionColor = logBox.ForeColor;
            if (playerName != senderName) { logBox.AppendText(" to "); }
            else { logBox.AppendText(" "); }

            if (playerName != senderName) { logBox.SelectionColor = System.Drawing.Color.Orange; logBox.AppendText(senderName); }
            else { logBox.SelectionColor = System.Drawing.Color.Beige; logBox.AppendText(playerName); }

            logBox.SelectionColor = logBox.ForeColor;
            logBox.AppendText(" (");

            logBox.SelectionColor = System.Drawing.Color.FromArgb(0, 255, 127);
            logBox.AppendText( locationName);

            logBox.SelectionColor = logBox.ForeColor;
            logBox.AppendText(")" + Environment.NewLine);
            logBox.SelectionStart = logBox.Text.Length;
            logBox.ScrollToCaret();
        }

        private void switchBTN(int btn)
        {
            switch (btn)
            {
                case 0:
                    btn_labellog.ForeColor = System.Drawing.Color.White;
                    btn_selectlog.Visible = true;

                    btn_labelhints.ForeColor = System.Drawing.Color.Gray;
                    btn_selecthints.Visible = false;

                    btn_labelreceivedItems.ForeColor = System.Drawing.Color.Gray;
                    btn_selectrecItems.Visible = false;

                    btn_labelgame.ForeColor = System.Drawing.Color.Gray;
                    btn_selectgame.Visible = false;

                    panel_Log.Visible = true;
                    panel_Hints.Visible = false;
                    panel_ReceivedItems.Visible = false;
                    panel_gamestats.Visible = false;
                    break;

                case 1:
                    btn_labellog.ForeColor = System.Drawing.Color.Gray;
                    btn_selectlog.Visible = false;

                    btn_labelhints.ForeColor = System.Drawing.Color.White;
                    btn_selecthints.Visible = true;

                    btn_labelreceivedItems.ForeColor = System.Drawing.Color.Gray;
                    btn_selectrecItems.Visible = false;

                    btn_labelgame.ForeColor = System.Drawing.Color.Gray;
                    btn_selectgame.Visible = false;

                    panel_Log.Visible = false;
                    panel_Hints.Visible = true;
                    panel_ReceivedItems.Visible = false;
                    panel_gamestats.Visible = false;
                    break;

                case 2:
                    btn_labellog.ForeColor = System.Drawing.Color.Gray;
                    btn_selectlog.Visible = false;

                    btn_labelhints.ForeColor = System.Drawing.Color.Gray;
                    btn_selecthints.Visible = false;

                    btn_labelreceivedItems.ForeColor = System.Drawing.Color.White;
                    btn_selectrecItems.Visible = true;

                    btn_labelgame.ForeColor = System.Drawing.Color.Gray;
                    btn_selectgame.Visible = false;

                    panel_Log.Visible = false;
                    panel_Hints.Visible = false;
                    panel_ReceivedItems.Visible = true;
                    panel_gamestats.Visible = false;
                    break;

                case 3:
                    btn_labellog.ForeColor = System.Drawing.Color.Gray;
                    btn_selectlog.Visible = false;

                    btn_labelhints.ForeColor = System.Drawing.Color.Gray;
                    btn_selecthints.Visible = false;

                    btn_labelreceivedItems.ForeColor = System.Drawing.Color.Gray;
                    btn_selectrecItems.Visible = false;

                    btn_labelgame.ForeColor = System.Drawing.Color.White;
                    btn_selectgame.Visible = true;

                    panel_Log.Visible = false;
                    panel_Hints.Visible = false;
                    panel_ReceivedItems.Visible = false;
                    panel_gamestats.Visible = true;
                    break;
            }
        }

        private void closeApplication(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void minApplication(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
            {
                WindowState = FormWindowState.Minimized;
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.hostName = hostName.Text;
            Properties.Settings.Default.slotName = slotName.Text;
            Properties.Settings.Default.hostPass = password.Text;

            Properties.Settings.Default.Save();
        }

        private bool is100PercentCompleted()
        {
            // Beispiel: Du zählst die gesammelten Items aus deinen Speicher-Methoden oder Variablen
            int totalCrystals = GetCollectedCrystalsCount(); // Sollte 25 sein
            int totalGems = GetCollectedGemsCount();         // Mindestens 42 für 100%
            int totalRelics = GetCollectedRelicsCount();     // Mindestens 28 für 100%

            // Die Bedingung für das wahre Ende / 100% laut Text:
            if (totalCrystals >= 25 && totalGems >= 42 && totalRelics >= 28)
            {
                return true;
            }

            return false;
        }

        private int GetCollectedCrystalsCount()
        {
            int count = 0;

            // Gehe jedes Item in deiner Crystal-Liste durch
            foreach (var item in SharedAdresses.CrystalItems)
            {
                // Prüfe mit deiner bereits existierenden Methode, ob das Crystal da ist
                if (isCrystalSave(item.LevelAPItemId))
                {
                    count++;
                }
            }

            return count;
        }

        private int GetCollectedGemsCount()
        {
            int count = 0;

            foreach (var item in SharedAdresses.ClearGemBoxItems)
            {

                if (isClearGemBoxSave(item.LevelAPItemId))
                {
                    count++;
                }
            }

            foreach (var item in SharedAdresses.ClearGemItems)
            {

                if (isClearGemSave(item.LevelAPItemId))
                {
                    count++;
                }
            }

            foreach (var item in SharedAdresses.ColoredGemItems)
            {

                if (isColoredGemSave(item.LevelAPItemId))
                {
                    count++;
                }
            }

            return count;
        }

        private int GetCollectedRelicsCount()
        {
            int count = 0;

            // Wir holen uns alle Level-Namen aus der Saphir-Liste (oder einer anderen)
            for (int i = 0; i < SharedAdresses.RelicSItems.Count; i++)
            {
                string levelName = SharedAdresses.RelicSItems[i].LevelName;

                // Die jeweiligen APItemIDs für dieses Level in den 3 Listen holen
                long sId = SharedAdresses.RelicSItems[i].LevelAPItemId;
                long gId = SharedAdresses.RelicGItems[i].LevelAPItemId;
                long pId = SharedAdresses.RelicPItems[i].LevelAPItemId;

                // Prüfen, ob für dieses Level IRGENDEINE Relic-Medaille im Speicher gespeichert/gesichert ist
                bool hasSapphire = isRelicSapphireSave(sId); // Deine Methode zum Prüfen von Saphir
                bool hasGold = isRelicGoldSave(gId);       // Deine Methode zum Prüfen von Gold
                bool hasPlatinum = isRelicGoldSave(gId) && isRelicSapphireSave(sId);   // Deine Methode zum Prüfen von Platin

                // Wenn mindestens eine Medaille da ist, zählt dieses Level als geschafft (1 Relic)
                if (hasSapphire || hasGold || hasPlatinum)
                {
                    count++;
                }
            }

            return count;
        }

        private bool is105PercentCompleted()
        {
            byte[] saveBytes = getColoredGemReserve((int)ColoredGemSavedAddress);

            
            if (saveBytes != null && saveBytes.Length > 0)
            {
                int byteIndex = 0;
                int bitPosition = 7;

                return (saveBytes[byteIndex] & (1 << bitPosition)) != 0;
            }

            return false;
        }

        private bool goalAlreadySent = false;

        private void CheckGameMemoryForGoal(long currentGoalSetting)
        {
            if (!APManager.IsConnected || goalAlreadySent) return;

            // Lese deine aktuellen Speicherwerte aus dem Spiel aus
            int currentLevelID = getLevelID((int)LateLevelIDAddress); // Beispiel-Funktion für deine Level-Adresse
            int currentGameState = getGameState((int)GameStateAddress);// Beispiel-Funktion für den GameState

            // Optional: Hier kannst du auch deine Prozent-Zahl aus dem Speicher lesen (z.B. für 100% / 105%)
            // float gamePercentage = GetGamePercentage(); 

            bool reachedGoal = false;

            // 1. Normales Goal: LevelID 7 und GameState 3 (z.B. Cortex besiegt)
            if (currentGoalSetting == 0)
            {
                if (currentLevelID == 7 && currentGameState == 3)
                {
                    reachedGoal = true;
                }
            }

            else if (currentGoalSetting == 1)
            {
                if (is100PercentCompleted() && currentLevelID == 7 && currentGameState == 3)
                {
                    reachedGoal = true;
                }
            }
            // 3. 105% Goal: (Beispiel: Alles komplett)
            else if (currentGoalSetting == 2)
            {
                if (is105PercentCompleted())
                {
                    reachedGoal = true;
                }
            }

            // Wenn das Ziel durch die Speicherwerte erfüllt wurde -> An Archipelago senden!
            if (reachedGoal)
            {

                APManager.Session.SetGoalAchieved();
                //APManager.Session.Socket.Send(Newtonsoft.Json.JsonConvert.SerializeObject(statusPacket));
                goalAlreadySent = true;

                ClientMessageBox.Show("Glückwunsch! Ziel im Spiel erreicht und an Archipelago gesendet!", "Gewonnen!");
            }
        }
    }
}
