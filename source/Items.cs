using static C3AP_Client.Helpers.NativeMethods;
using static C3AP_Client.GameConfig;
using static C3AP_Client.Form1;
using static C3AP_Client.Hook.Hook;

namespace C3AP_Client
{
    public static class Items
    {
        public static byte[] getCrystalReserve(IntPtr offsetAddress)
        {

            IntPtr bytesRead;
            IntPtr targetAddress = IntPtr.Add(ramBase, (int)offsetAddress);
            bool success = ReadProcessMemory(targetProcess, targetAddress, byte4Buffer, byte4Buffer.Length, out bytesRead);
            if (success) { return byte4Buffer; }
            return byte4Buffer;
        }
        public static byte[] getClearGemReserve(IntPtr offsetAddress)
        {

            IntPtr bytesRead;
            IntPtr targetAddress = IntPtr.Add(ramBase, (int)offsetAddress);
            bool success = ReadProcessMemory(targetProcess, targetAddress, byte5Buffer, byte5Buffer.Length, out bytesRead);
            if (success) { return byte5Buffer; }
            return byte5Buffer;
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

        public static bool isCrystalSaved(string levelName)
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
        public static bool isCrystalSave(long APItemID)
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

        public static bool isClearGemBoxSave(long APItemID)
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

        public static bool isClearGemSave(long APItemID)
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

        public static bool isColoredGemSave(long APItemID)
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

        public static bool isRelicSapphireSave(long APItemID)
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

        public static bool isRelicGoldSave(long APItemID)
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


        //Delete Area Start
        public static bool deleteCrystalReserved(string levelName, IntPtr offsetAddress)
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

        public static bool deleteClearGemBoxReserved(string levelName, IntPtr offsetAddress)
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

        public static bool deleteClearGemReserved(string levelName, IntPtr offsetAddress)
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

        public static bool deleteColoredGemReserved(string levelName, IntPtr offsetAddress)
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

        public static bool deleteRelicSGReserved(string levelName, IntPtr offsetAddress)
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

        //Delete Area End
    }
}
