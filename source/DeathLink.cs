using Archipelago.MultiClient.Net.BounceFeatures.DeathLink;
using static C3AP_Client.Helpers.NativeMethods;
using static C3AP_Client.Form1;
using static C3AP_Client.GameConfig;
using static C3AP_Client.SharedAdresses;

namespace C3AP_Client
{
    internal class CrashDeathLink
    {
        public static void onKillCrash(int levelID) 
        {
            int state = 0;
            IntPtr bytesWritten;

            if (LevelDataAddress.ContainsKey(levelID))
            {
                if (LevelDataAddress.TryGetValue(levelID, out uint address))
                {
                    byte[] zeroByte = new byte[] {0,0,0,0};
                    uint xPOSAddress = address + XPositionOffset;
                    IntPtr xPOStargetAddress = IntPtr.Add(ramBase, (int)xPOSAddress);

                    uint yPOSAddress = address + YPositionOffset;
                    IntPtr yPOStargetAddress = IntPtr.Add(ramBase, (int)yPOSAddress);

                    uint zPOSAddress = address + ZPositionOffset;
                    IntPtr zPOStargetAddress = IntPtr.Add(ramBase, (int)zPOSAddress);

                    bool successX = WriteProcessMemory(targetProcess, xPOStargetAddress, zeroByte, zeroByte.Length, out bytesWritten);
                    bool successY = WriteProcessMemory(targetProcess, yPOStargetAddress, zeroByte, zeroByte.Length, out bytesWritten);
                    bool successZ = WriteProcessMemory(targetProcess, zPOStargetAddress, zeroByte, zeroByte.Length, out bytesWritten);
                    if (successZ) { //MessageBox.Show("Killed Crash");
                                 }
                }
            }
        }

        public static void checkDeath(bool isLevel, bool isBonus, int aniID, string playerName) 
        {
            bool isRespawn = false;
            bool triggerOnce = false;

            if (isRespawn == true && aniID == 1) { isRespawn = false; triggerOnce = false; }

            if (isLevel == true && isBonus == false && isRespawn == false) 
            {
                
                if (DeathAnimationIDs.ContainsKey(aniID) && triggerOnce == false) { sendDeathLink(playerName); triggerOnce = true; }
            }
            if (isLevel == true && isBonus == true)
            {
                if (DeathAnimationIDs.ContainsKey(aniID)) {  }
                isRespawn = true;
            }
        }

        public static void sendDeathLink(string playerName)
        {
            if (!APManager.IsConnected) return;
            if (deathLinkService == null) return;

            deathLinkService.SendDeathLink(new DeathLink(playerName));
        }

    }
}
