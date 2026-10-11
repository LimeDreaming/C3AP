using Archipelago.MultiClient.Net;
using Archipelago.MultiClient.Net.BounceFeatures.DeathLink;
using Archipelago.MultiClient.Net.Enums;
using Archipelago.MultiClient.Net.Helpers;
using Archipelago.MultiClient.Net.Models;
using Archipelago.MultiClient.Net.Packets;
using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Windows.Forms;
using static C3AP_Client.Checks;
using static C3AP_Client.CrashData;
using static C3AP_Client.CrystalBits;
using static C3AP_Client.CrashDeathLink;
using static C3AP_Client.DuckstationHook;
using static C3AP_Client.GameConfig;
using static C3AP_Client.GameData;
using static C3AP_Client.Helpers.NativeMethods;
using static C3AP_Client.Hook.Hook;
using static C3AP_Client.Items;
using static C3AP_Client.LevelData;
using static C3AP_Client.Powerups;
using static C3AP_Client.SearchReserved;
using static C3AP_Client.SharedAdresses;
using Timer = System.Windows.Forms.Timer;

namespace C3AP_Client
{
    public partial class Form1 : Form
    {
        public static int Goal;
        public static bool isAllUnlocked;
        public static bool isPowerUp;
        public static bool isDeathLink;
        public static DeathLinkService deathLinkService;

        //Game Watch
        public int gameLevelID;
        public int gameLateLevelID;
        public int crashAniID;
        public int crashLives;
        public int crashWumpas;
        public int crashBoxes;

        private bool _dragging = false;
        private Point _start_point = new Point(0, 0);
        Form tracker = new Tracker();
        private bool isTracker = false;
        public static string RoomSeed;

        // Entspricht exakt der m_end_address = 0x200000 (2 MB PS1-RAM) aus der DuckStation-Header-Datei
        const long PS1_RAM_SIZE = 0x200000;
        
        private int selectedBTN = 0;
        private bool isCollapsed = true;
        private Timer menuAni = new Timer();
        private ItemPopup itemPopup = new ItemPopup();

        private long currentGoalSetting;

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

        private void Form1_Load(object sender, EventArgs e)
        {
            duckPath.Text = "Duckstation Path: " + Properties.Settings.Default.duckPath;
            gamePath.Text = "Game Path: " + Properties.Settings.Default.gamePath;

            if (Properties.Settings.Default.duckPath != "") { duckFilePath.Text = Properties.Settings.Default.duckPath; }
            if (Properties.Settings.Default.gamePath != "") { gameFilePath.Text = Properties.Settings.Default.gamePath; }
            duckAutoStart.Checked = Properties.Settings.Default.autoStart;

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

            if (isProcessRunning().Length != 0) { isProcess = true; } else { isProcess = false; }

            if (isProcess == false)
            {
                if (Properties.Settings.Default.autoStart == true)
                {
                    if (Properties.Settings.Default.duckPath != "" && Properties.Settings.Default.gamePath != "")
                    {
                        startDucktation(Properties.Settings.Default.duckPath, Properties.Settings.Default.gamePath);
                        timer1.Enabled = true;
                        waitTimer.Start();
                    }
                }
            }
            else
            {
                timer1.Enabled = true;
                waitTimer2.Start();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int levelNumber = getLevelID((int)LevelIDAddress);
            int levelNumberLate = getLevelID((int)LateLevelIDAddress);
            gameLevelID = getLevelID((int)LevelIDAddress);
            gameLateLevelID = getLevelID((int)LateLevelIDAddress);
            crashAniID = getAnimationID();

            string levelName = LevelIDs.FirstOrDefault(x => x.Value == levelNumber).Key;
            string levelNameLate = LevelIDs.FirstOrDefault(x => x.Value == levelNumberLate).Key;

            levelID.Text = "LevelID: " + levelNumber + " [" + levelName + "] is in State: " + getGameState((int)GameStateAddress);
            latelevelId.Text = "LevelID: " + levelNumberLate + " [" + levelNameLate + "] is in State: " + getGameState((int)GameStateAddress);
            aniID.Text = "AnimationsID: " + getAnimationID();
            lives.Text = "Lives: " + getLives();
            updateLiveCounter(getLives(), levelNumber);
            wumpa.Text = "Wumpa Fruits: " + getWumpa();
            updateWumpaCounter(getWumpa(), levelNumber);
            crates.Text = "Crates: " + getCrates();
            updateBoxCounter(getCrates());

            if (isProcess)
            {
                dsstatus.Text = "Duckstation Status: Is Running";
                duck_status.Image = C3AP_Client.Properties.Resources.duckstation_on;
            }
            else
            {
                dsstatus.Text = "Duckstation Status: Is not Running";
                duck_status.Image = C3AP_Client.Properties.Resources.duckstation_off;

            }

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
            label6.Text = "" + targetProcess;
            label5.Text = "" + ramBase;
            timer1.Interval = 100;
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
                    game: "Crash Bandicoot 3: Warped",
                    name: slotName.Text,
                    itemsHandlingFlags: ItemsHandlingFlags.AllItems,
                    tags: new string[] { "AP" },
                    password: password.Text
                    );

                    if (result.Successful)
                    {
                        APManager.Session = session;
                        RoomSeed = session.RoomState.Seed.ToString();
                        ClientMessageBox.Show("Connected to the AP-Server!", "Connected");
                    }
                    else
                    {
                        ClientMessageBox.Show("Connection to the AP-Server faild!", "Failed Connecten");
                    }

                    if (result is LoginSuccessful successful)
                    {
                        if (successful.SlotData.TryGetValue("options", out var optionsObj) && optionsObj != null)
                        {
                            try
                            {
                                if (optionsObj is Newtonsoft.Json.Linq.JObject jObject)
                                {
                                    long goalValue = jObject["goal_option"]?.Value<long>() ?? 0;
                                    int powerUp = jObject["powerup_lock"]?.Value<int>() ?? 0;
                                    int deathLink = jObject["death_link"]?.Value<int>() ?? 0;

                                    isPowerUp = Convert.ToBoolean(powerUp);
                                    isDeathLink = Convert.ToBoolean(deathLink);

                                    currentGoalSetting = goalValue;

                                }
                            }
                            catch (Exception ex)
                            {
                            }
                        }
                    }

                    MessageBox.Show(isPowerUp.ToString());
                    MessageBox.Show(isDeathLink.ToString());
                    LoadItemsFromJson();
                    alreadyReceivedItems();
                    timer2.Enabled = true;

                    if(isDeathLink)
                    {
                        deathLinkService = session.CreateDeathLinkService();
                        deathLinkService.EnableDeathLink();
                        deathLinkService.OnDeathLinkReceived += HandleDeathLink;
                    }

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

                        SaveItemToJson(itemId, slotName.Text, itemName, senderName, locationName, flags);


                        //richTextBox1.AppendText($"Item empfangen: {itemName} (ID: {itemId.ToString()})!");

                        receivedItemsHelper.DequeueItem();
                    };
                }

            }


            //MessageBox.Show("Erfolgreich mit dem AP-Server verbunden!");
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

        public class CrashData
        {
            public long levelID { get; set; }
            public int crashAniID { get; set; }
            public int crashLives { get; set; }
            public int crashWumpas { get; set; }
            public int crashBoxes { get; set; }
        }

        private void SaveCheckToJson(long itemId)
        {
            var itemEntry = new ReceivedItemData
            {
                ItemId = itemId,
            };
            string jsonString = JsonSerializer.Serialize(itemEntry);

            File.AppendAllLines(RoomSeed + "_items.c3apsave", new[] { jsonString });
        }

        public static int getAnimationID()
        {
            int currentLevelID = getLevelID((int)LevelIDAddress);
            IntPtr bytesRead;
            if (LevelDataAddress.ContainsKey(currentLevelID))
            {
                if (LevelDataAddress.TryGetValue(currentLevelID, out uint address))
                {
                    uint aniAddress = address + AnimationIDOffset;
                    IntPtr targetAddress = IntPtr.Add(ramBase, (int)aniAddress);

                    bool success = ReadProcessMemory(targetProcess, targetAddress, byte1Buffer, byte1Buffer.Length, out bytesRead);
                    if (success) { return byte1Buffer[0]; }
                    return currentLevelID;
                }
            }
            return currentLevelID;
        }

        private void SaveItemToJson(long itemId, string playerName, string itemName, string senderName, string locationName, ItemFlags flags)
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

            File.AppendAllLines(RoomSeed + "_apreceiveditems.c3apsave", new[] { jsonString });
        }

        private void LoadItemsFromJson()
        {
            string logFilePath = RoomSeed + "_apreceiveditems.c3apsave";

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
                    MessageBox.Show("Fehler beim Laden eines Log-Eintrags: " + ex.Message);
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

        private void timer2_Tick(object sender, EventArgs e)
        {
            var levelEntry = LevelIDs.FirstOrDefault(x => x.Value == getLevelID((int)LateLevelIDAddress));

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
            checkDeath(isInLevel(), isInBonus(), crashAniID, slotName.Text);
            checkBodySlam();
            checkDoubleJump();
            checkTornadoSpin();
            checkFruitBazooka();
            checkSpeedShoes();

            if (APManager.IsConnected)
            {
                CheckGameMemoryForGoal(currentGoalSetting);
            }

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
                var match = SharedAdresses.CrystalItems.FirstOrDefault(item => item.LevelAPItemId == receivedItemId);
                if (match != null)
                {
                    sendToPopup(receivedItemId, "Crystal", match.LevelName);
                }
                //setGameItems(receivedItemId, (int)CrystalSavedAddress, (int)CyrstalReceivedAddress);
            }
            else if (isClearGemItem)
            {
                var match = SharedAdresses.ClearGemBoxItems.FirstOrDefault(item => item.LevelAPItemId == receivedItemId);
                if (match != null)
                {
                    sendToPopup(receivedItemId, "Clear Gem", match.LevelName);
                }
                //setGameItems(receivedItemId, (int)GemSavedAddress, (int)GemReceivedAddress);
            }
            else if (isColoredGemItem)
            {
                var match = SharedAdresses.ColoredGemItems.FirstOrDefault(item => item.LevelAPItemId == receivedItemId);
                if (match != null)
                {
                    sendToPopup(receivedItemId, "Colored Gem", match.LevelName);
                }
                //setGameItems(receivedItemId, (int)ColoredGemSavedAddress, (int)ColoredGemReceivedAddress);
            }
            else if (isRelicSapphireItem)
            {
                var match = SharedAdresses.RelicSItems.FirstOrDefault(item => item.LevelAPItemId == receivedItemId);
                if (match != null)
                {
                    sendToPopup(receivedItemId, "Sapphire Relic", match.LevelName);
                }
                //setGameItems(receivedItemId, (int)RelicsSapphireSavedAddress, (int)RelicsSapphireReceivedAddress);
            }
            else if (isRelicGoldItem)
            {
                var match = SharedAdresses.RelicGItems.FirstOrDefault(item => item.LevelAPItemId == receivedItemId);
                if (match != null)
                {
                    sendToPopup(receivedItemId, "Gold Relic", match.LevelName);
                }
                //setGameItems(receivedItemId, (int)RelicsGoldSavedAddress, (int)RelicsGoldReceivedAddress);
            }
            else if (isRelicPlatinumItem)
            {
                var match = SharedAdresses.RelicPItems.FirstOrDefault(item => item.LevelAPItemId == receivedItemId);
                if (match != null)
                {
                    sendToPopup(receivedItemId, "Platinium Relic", match.LevelName);
                }
                //setPlatinumRelic(receivedItemId, (int)RelicsSapphireSavedAddress, (int)RelicsSapphireReceivedAddress, (int)RelicsGoldSavedAddress, (int)RelicsGoldReceivedAddress);
            }
        }

        public static class APManager
        {

            public static ArchipelagoSession Session { get; set; }


            public static bool IsConnected => Session != null && Session.Socket.Connected;
        }

        /*private void NumbersOnly(object sender, KeyPressEventArgs e)
        {
            char numbersOnly = e.KeyChar;

            if (!Char.IsDigit(numbersOnly) && numbersOnly != 8)
            {
                e.Handled = true;
            }
        }*/
        private void alreadyReceivedItems()
        {
            if (APManager.IsConnected)
            {
                var receivedItems = APManager.Session.Items.AllItemsReceived;

                foreach (var item in receivedItems)
                {
                    //setNonPlayedItems(item.ItemId);
                }
                //ClientMessageBox.Show("All Items transferd!", "Archipelago Status");
                //MessageBox.Show("Alle empfangenen Items wurden mit dem Spielstand abgeglichen!", "Abgleich beendet");
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
            MessageBox.Show(getAnimationID().ToString());
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
            logBox.AppendText(locationName);

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
            Properties.Settings.Default.autoStart = duckAutoStart.Checked;

            if (duckAutoStart.Checked == true)
            {
                if (Properties.Settings.Default.duckPath == "")
                {
                    searchDucktationFile();
                }
            }

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

                ClientMessageBox.Show("Congraulation for finishing the Game!", "Win!");
            }
        }

        private void timer4_Tick(object sender, EventArgs e)
        {
            if (isProcessRunning().Length != 0)
            {
                isProcess = true;
                targetProcess = GetProcess();

                if (File.Exists(Properties.Settings.Default.gamePath))
                {
                    if (checkGameFileRegion(Properties.Settings.Default.gamePath, out string game))
                    {
                        if (game.Contains("US"))
                        {
                            GameConfig.LoadUSAddresses();
                            regionFlag.Image = C3AP_Client.Properties.Resources.USCA;
                            //timer1.Enabled = true;
                            //waitTimer.Start();
                        }
                        else
                        {
                            GameConfig.LoadEUAddresses();
                            regionFlag.Image = C3AP_Client.Properties.Resources.EU;
                            //timer1.Enabled = true;
                            //waitTimer.Start();
                        }
                    }
                }
                else
                {
                    if (GetRegion(ramBase, targetProcess) == "US") { regionFlag.Image = C3AP_Client.Properties.Resources.USCA; }
                    else { regionFlag.Image = C3AP_Client.Properties.Resources.EU; }
                }

                timer4.Stop();
            }

            //regionLabel.Text = "Game Region: " + GetRegion(ramBase, targetProcess);

        }

        private void waitTimer_Tick(object sender, EventArgs e)
        {
            ramBase = FindRamBySignature(targetProcess);
            waitTimer.Stop();
        }

        private void openDuckFileDialog(object sender, EventArgs e)
        {
            if (searchDucktationFile() == true) { duckFilePath.Text = Properties.Settings.Default.duckPath; }
        }

        private void openGameFileDialog(object sender, EventArgs e)
        {
            if (searchGameFile() == true) { gameFilePath.Text = Properties.Settings.Default.gamePath; }
        }

        private void openTracker(object sender, EventArgs e)
        {

            if (isTracker == false)
            {
                tracker.Show();
                isTracker = true;
            }
            else
            {
                tracker.Hide();
                isTracker = false;
            }

        }

        private void waitTimer2_Tick(object sender, EventArgs e)
        {
            ramBase = FindRamBySignature(targetProcess);
            waitTimer2.Stop();
        }

        private void updateCountp2(object sender, EventArgs e)
        {
            int liveP1 = liveCountp1.Width;
            int liveP1Pos = liveCountp1.Location.X;
            liveCountp2.Location = new Point(liveP1Pos + liveP1, liveCountp2.Location.Y);
        }
        private void updateWumpaCountp2(object sender, EventArgs e)
        {
            int wumpaP1 = wumpaCountp1.Width;
            int wumpaP1Pos = wumpaCountp1.Location.X;
            wumpaCountp2.Location = new Point(wumpaP1Pos + wumpaP1, wumpaCountp2.Location.Y);
        }

        private void updateBoxCountp2(object sender, EventArgs e)
        {
            int boxP1 = boxCountp1.Width;
            int boxP1Pos = boxCountp1.Location.X;
            boxCountp2.Location = new Point(boxP1Pos + boxP1, boxCountp2.Location.Y);

            if(boxCountp2.Image == null) 
            {
                maxboxCount.Location = new Point(boxP1Pos + boxP1, maxboxCount.Location.Y);
            }
        }
        private void updateBoxCountp3(object sender, EventArgs e)
        {
            int boxP2 = boxCountp2.Width;
            int boxP2Pos = boxCountp2.Location.X;
            boxCountp3.Location = new Point(boxP2Pos + boxP2, boxCountp3.Location.Y);

            if (boxCountp3.Image == null)
            {
                maxboxCount.Location = new Point(boxP2Pos + boxP2, maxboxCount.Location.Y);
            }
        }

        private void updateMaxbox(object sender, EventArgs e) 
        {
            int boxP3 = boxCountp3.Width;
            int boxP3Pos = boxCountp3.Location.X;

            maxboxCount.Location = new Point(boxP3Pos + boxP3, maxboxCount.Location.Y);
        }

        private void updateLiveCounter(int lives, int levelId)
        {
            if (LevelIDs.ContainsValue(levelId) || BossLevelIDs.ContainsValue(levelId))
            { 
                if (lives < 10)
                {
                    liveCountp1.Image = (System.Drawing.Image)Properties.Resources.ResourceManager.GetObject("live" + lives);
                    liveCountp2.Image = null;
                }
                else
                {
                    int CountP1 = lives / 10;
                    int CountP2 = lives % 10;

                    liveCountp1.Image = (System.Drawing.Image)Properties.Resources.ResourceManager.GetObject("live" + CountP1);
                    liveCountp2.Image = (System.Drawing.Image)Properties.Resources.ResourceManager.GetObject("live" + CountP2);
                }
            }
            else { liveCountp1.Image = Properties.Resources.livenone; liveCountp2.Image = Properties.Resources.livenone; }
        }

        private void updateWumpaCounter(int wumpa, int levelId)
        {
            if (LevelIDs.ContainsValue(levelId) || BossLevelIDs.ContainsValue(levelId))
            {
                if (wumpa < 10)
                {
                    wumpaCountp1.Image = (System.Drawing.Image)Properties.Resources.ResourceManager.GetObject("wumpa" + wumpa);
                    wumpaCountp2.Image = null;
                }
                else
                {
                    int CountP1 = wumpa / 10;
                    int CountP2 = wumpa % 10;

                    wumpaCountp1.Image = (System.Drawing.Image)Properties.Resources.ResourceManager.GetObject("wumpa" + CountP1);
                    wumpaCountp2.Image = (System.Drawing.Image)Properties.Resources.ResourceManager.GetObject("wumpa" + CountP2);
                }
            }
            else { wumpaCountp1.Image = Properties.Resources.wumpanone; wumpaCountp2.Image = Properties.Resources.wumpanone; }
        }

        private void updateBoxCounter(int box)
        {
            

            if (box < 10)
            {
                boxCountp1.Image = (System.Drawing.Image)Properties.Resources.ResourceManager.GetObject("box" + box);
                boxCountp2.Image = null;
                boxCountp3.Image = null;
            }
            else if (box >= 10 && box < 100)
            {
                int CountP1 = box / 10;
                int CountP2 = box % 10;
                boxCountp1.Image = (System.Drawing.Image)Properties.Resources.ResourceManager.GetObject("box" + CountP1);
                boxCountp2.Image = (System.Drawing.Image)Properties.Resources.ResourceManager.GetObject("box" + CountP2);
                boxCountp3.Image = null;
            }
            else
            {
                int CountP1 = box / 100;
                int CountP2 = (box / 10) % 10;
                int CountP3 = box % 10;

                boxCountp1.Image = (System.Drawing.Image)Properties.Resources.ResourceManager.GetObject("box" + CountP1);
                boxCountp2.Image = (System.Drawing.Image)Properties.Resources.ResourceManager.GetObject("box" + CountP2);
                boxCountp3.Image = (System.Drawing.Image)Properties.Resources.ResourceManager.GetObject("box" + CountP3);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            liveCountp1.Image = Properties.Resources.live2;
        }

        private void HandleDeathLink(DeathLink deathLink)
        {
            CrashDeathLink.onKillCrash(gameLateLevelID);
        }
    }
}
