using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static C3AP_Client.Form1;
using static C3AP_Client.GameConfig;
using static C3AP_Client.Helpers.NativeMethods;

namespace C3AP_Client
{
    public static class Powerups
    {
        public static void checkBodySlam() 
        {
            if (APManager.IsConnected) 
            {
                bool isLocationChecked = APManager.Session.Items.AllItemsReceived.Any(item => item.ItemId == 142168);

                if (!isLocationChecked)
                {
                    deletePowerUp(0, ((int)PowerUpAddress));
                }
            }   
        }

        public static void checkDoubleJump()
        {
            if (APManager.IsConnected)
            {
                bool isLocationChecked = APManager.Session.Items.AllItemsReceived.Any(item => item.ItemId == 142169);

                if (!isLocationChecked)
                {
                    deletePowerUp(1, ((int)PowerUpAddress));
                }
            }
        }

        public static void checkTornadoSpin()
        {
            if (APManager.IsConnected)
            {
                bool isLocationChecked = APManager.Session.Items.AllItemsReceived.Any(item => item.ItemId == 142170);

                if (!isLocationChecked)
                {
                    deletePowerUp(2, ((int)PowerUpAddress));
                }
            }
        }

        public static void checkFruitBazooka()
        {
            if (APManager.IsConnected)
            {
                bool isLocationChecked = APManager.Session.Items.AllItemsReceived.Any(item => item.ItemId == 142171);

                if (!isLocationChecked)
                {
                    deletePowerUp(3, ((int)PowerUpAddress));
                }
            }
        }

        public static void checkSpeedShoes()
        {
            if (APManager.IsConnected)
            {
                bool isLocationChecked = APManager.Session.Items.AllItemsReceived.Any(item => item.ItemId == 142172);

                if (!isLocationChecked)
                {
                    deletePowerUp(4, ((int)PowerUpAddress));
                }
            }
        }

        public static bool deletePowerUp(int bitPostion, IntPtr offsetAddress)
        {
            IntPtr bytesWritten;
            IntPtr targetAddress = IntPtr.Add(ramBase, (int)offsetAddress);

            byte[] powerupslam = getPowerUp(offsetAddress);

            powerupslam[0] = (byte)(powerupslam[0] & ~(1 << bitPostion));

            bool successslam = WriteProcessMemory(targetProcess, targetAddress, powerupslam, powerupslam.Length, out bytesWritten);

            return successslam;

        }
        public static byte[] getPowerUp(IntPtr offsetAddress)
        {

            IntPtr bytesRead;
            IntPtr targetAddress = IntPtr.Add(ramBase, (int)offsetAddress);
            bool success = ReadProcessMemory(targetProcess, targetAddress, byte1Buffer, byte1Buffer.Length, out bytesRead);
            if (success) { return byte1Buffer; }
            return byte1Buffer;
        }
    }
}
