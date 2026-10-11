using System.Runtime.Intrinsics.Arm;
using static C3AP_Client.Helpers.NativeMethods;
using static C3AP_Client.GameConfig;
using static C3AP_Client.SharedAdresses;
using static C3AP_Client.Form1;

namespace C3AP_Client
{
    public static class LevelData
    {

        public static int getLevelID(IntPtr offsetAddress)
        {
            IntPtr bytesRead;
            IntPtr targetAddress = IntPtr.Add(ramBase, (int)offsetAddress);
            bool success = ReadProcessMemory(targetProcess, targetAddress, byte1Buffer, byte1Buffer.Length, out bytesRead);
            if (success) { return byte1Buffer[0]; }
            return 0;
        }
        public static bool isInBonus()
        {
            IntPtr bytesRead;
            IntPtr targetAddress = IntPtr.Add(ramBase, (int)BonusStateAddress);
            bool success = ReadProcessMemory(targetProcess, targetAddress, byte1Buffer, byte1Buffer.Length, out bytesRead);
            if (success)
            {
                switch (byte1Buffer[0]) 
                {
                    case 1: return true;
                    case 2: return false;
                }
            }
            return false;
        }

        public static bool isInLevel()
        {
            if (SharedAdresses.LevelIDs.ContainsValue(getLevelID((int)LevelIDAddress)) && getLevelID((int)LevelIDAddress) != 2) { return true; }
            return false;
        }
    }
    
    public static class CrashData
    {
        public static int getAnimationID()
        {
            int currentLevelID = LevelData.getLevelID((int)LevelIDAddress);
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
            return 0;
        }

        public static int getLives()
        {
            int currentLevelID = LevelData.getLevelID((int)LevelIDAddress);
            IntPtr bytesRead;
            if (LevelDataAddress.ContainsKey(currentLevelID))
            {
                if (LevelDataAddress.TryGetValue(currentLevelID, out uint address))
                {
                    uint liveAddress = address + LivesIDOffset;
                    IntPtr targetAddress = IntPtr.Add(ramBase, (int)liveAddress);

                    bool success = ReadProcessMemory(targetProcess, targetAddress, byte1Buffer, byte1Buffer.Length, out bytesRead);
                    if (success) { return byte1Buffer[0]; }
                    return currentLevelID;
                }
            }
            return 0;
        }

        public static int getWumpa()
        {
            int currentLevelID = LevelData.getLevelID((int)LevelIDAddress);
            IntPtr bytesRead;
            if (LevelDataAddress.ContainsKey(currentLevelID))
            {
                if (LevelDataAddress.TryGetValue(currentLevelID, out uint address))
                {
                    uint wumpaAddress = address + WumpaOffset;
                    IntPtr targetAddress = IntPtr.Add(ramBase, (int)wumpaAddress);

                    bool success = ReadProcessMemory(targetProcess, targetAddress, byte1Buffer, byte1Buffer.Length, out bytesRead);
                    if (success) { return byte1Buffer[0]; }
                    return currentLevelID;
                }
            }
            return 0;
        }

        public static int getCrates()
        {
            int currentLevelID = LevelData.getLevelID((int)LevelIDAddress);
            IntPtr bytesRead;
            if (LevelDataAddress.ContainsKey(currentLevelID))
            {
                if (LevelDataAddress.TryGetValue(currentLevelID, out uint address))
                {
                    uint cratesAddress = address + CratesOffset;
                    IntPtr targetAddress = IntPtr.Add(ramBase, (int)cratesAddress);

                    bool success = ReadProcessMemory(targetProcess, targetAddress, byte1Buffer, byte1Buffer.Length, out bytesRead);
                    if (success) { return byte1Buffer[0]; }
                    return currentLevelID;
                }
            }
            return 0;
        }
    }

    public static class GameData 
    {
        public static int getGameState(IntPtr offsetAddress)
        {
            int state = 0;
            IntPtr bytesRead;
            IntPtr targetAddress = IntPtr.Add(ramBase, (int)offsetAddress);
            bool success = ReadProcessMemory(targetProcess, targetAddress, byte1Buffer, byte1Buffer.Length, out bytesRead);
            if (success)
            { state = byte1Buffer[0]; }
            return state;
        }
    }
}
