using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static C3AP_Client.Form1;
using static C3AP_Client.SearchReserved;
using static C3AP_Client.SharedAdresses;
using static C3AP_Client.GameConfig;

namespace C3AP_Client
{
    internal class SendChecks
    {
        /*
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
        }*/

    }
}
