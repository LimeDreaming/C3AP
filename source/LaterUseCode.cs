using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

namespace C3AP_Client
{
    internal class LaterUseCode
    {
        /*
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

        //Only Commented may for later use

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
        }*/

        /*private bool setGameItems(long itemID, IntPtr offsetSavedAddress, IntPtr offsetReserveAddress)
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
        }*/
    }
}
