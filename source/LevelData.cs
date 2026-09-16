using System.Runtime.Intrinsics.Arm;
using static C3AP_Client.Helpers.NativeMethods;
using static C3AP_Client.GameConfig;
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

        public static bool isInLevel()
        {
            if (SharedAdresses.LevelIDs.ContainsValue(getLevelID((int)LevelIDAddress)) && getLevelID((int)LevelIDAddress) != 2) { return true; }
            return false;
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
