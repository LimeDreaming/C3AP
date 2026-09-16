using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static C3AP_Client.GameConfig;
using static C3AP_Client.GameData;
using static C3AP_Client.Items;
using static C3AP_Client.Form1;

namespace C3AP_Client
{
    public static class Checks
    {
        public static void sendExitCheck(string levelName)
        {
            if (getGameState((int)GameStateAddress) == 3)
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
        public static void sendCrystalCheck(string levelName)
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

        public static void sendClearGemBoxCheck(string levelName)
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

        public static void sendClearGemCheck(string levelName)
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

        public static void sendColoredGemCheck(string levelName)
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

        public static void sendForceRelicSapphireCheck(string levelName)
        {
            if (APManager.IsConnected)
            {
                var foundCrystal = SharedAdresses.RelicSItems.FirstOrDefault(c => c.LevelName == levelName);
                if (foundCrystal != null)
                {
                    APManager.Session.Locations.CompleteLocationChecks(new[] { foundCrystal.LevelAPCheckId });
                }
            }
        }

        public static void sendRelicSapphireCheck(string levelName)
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

        public static void sendRelicGoldCheck(string levelName)
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
                        sendForceRelicSapphireCheck(levelName);
                        APManager.Session.Locations.CompleteLocationChecks(new[] { foundCrystal.LevelAPCheckId });
                    }
                }
            }
        }

        public static void sendRelicPlatinumCheck(string levelName)
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
    }
}
