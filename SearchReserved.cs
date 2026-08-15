using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static C3AP_Client.Form1;
using static C3AP_Client.GameConfig;

namespace C3AP_Client
{
    internal class SearchReserved
    {
        /*[DllImport("kernel32.dll", SetLastError = true)]
        static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [Out] byte[] lpBuffer, int nSize, out IntPtr lpNumberOfBytesRead);

        private IntPtr ramBase;
        private byte[] byte1Buffer = new byte[4];
        private byte[] byte4Buffer = new byte[4];

        private byte[] valueBuffer = new byte[1];
        public byte[] crystalBuffer = new byte[4];
        private byte[] cleargemBuffer = new byte[5];
        Process targetProcess;

        public static byte[] getCrystalReserve(IntPtr offsetAddress, Process targetProcess, IntPtr ramBase)
        {

            IntPtr bytesRead;
            //ramBase = FindRamBySignature(GetProcess());
            IntPtr targetAddress = IntPtr.Add(ramBase, (int)offsetAddress);
            bool success = ReadProcessMemory(targetProcess, targetAddress, crystalBuffer, crystalBuffer.Length, out bytesRead);
            if (success) { return crystalBuffer; }
            return crystalBuffer;
        }

        public static byte[] getClearGemReserve(IntPtr offsetAddress)
        {

            IntPtr bytesRead;
            IntPtr targetAddress = IntPtr.Add(ramBase, (int)offsetAddress);
            bool success = ReadProcessMemory(targetProcess, targetAddress, cleargemBuffer, cleargemBuffer.Length, out bytesRead);
            if (success) { return cleargemBuffer; }
            return cleargemBuffer;
        }

        public static byte[] getColoredGemReserve(IntPtr offsetAddress)
        {

            IntPtr bytesRead;
            IntPtr targetAddress = IntPtr.Add(ramBase, (int)offsetAddress);
            bool success = ReadProcessMemory(targetProcess, targetAddress, byte1Buffer, byte1Buffer.Length, out bytesRead);
            if (success) { return byte1Buffer; }
            return byte1Buffer;
        }

        public static byte[] getRelicSGeReserve(IntPtr offsetAddress)
        {

            IntPtr bytesRead;
            IntPtr targetAddress = IntPtr.Add(ramBase, (int)offsetAddress);
            bool success = ReadProcessMemory(targetProcess, targetAddress, byte4Buffer, byte4Buffer.Length, out bytesRead);
            if (success) { return byte4Buffer; }
            return byte4Buffer;
        }
        public static bool isCrystalReserved(string levelName)
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

        public static bool isClearGemBoxReserved(string levelName)
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

        public static bool isClearGemReserved(string levelName)
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

        public static bool isColoredGemReserved(string levelName)
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

        public static bool isRelicSapphireReserved(string levelName)
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

        public static bool isRelicGoldReserved(string levelName)
        {
            byte[] reserverelicS = getRelicSGeReserve((int)RelicsGoldReceivedAddress);
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
        }*/
    }
}
