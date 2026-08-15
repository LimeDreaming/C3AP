using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C3AP_Client
{
    public static class EUAddresses
    {
        public const uint CyrstalReceivedAddress = 0x69189;
        public const uint CrystalSavedAddress = 0x692dd;

        public const uint GemReceivedAddress = 0x69168;
        public const uint GemSavedAddress = 0x69160;

        public const uint RelicsSapphireReceivedAddress = 0x69375;
        public const uint RelicsSapphireSavedAddress = 0x69365;
        public const uint RelicsGoldReceivedAddress = 0x6937d;
        public const uint RelicsGoldSavedAddress = 0x6936d;

        public const uint ColoredGemReceivedAddress = 0x6916f;
        public const int RedGemReceivedBit = 2;
        public const int GreenGemReceivedBit = 4;
        public const int PurpleGemReceivedBit = 8;
        public const int BlueGemReceivedBit = 16;
        public const int Gem105ReceivedBit = 32;

        public const uint ColoredGemSavedAddress = 0x69167;
        public const int RedGemSavedBit = 2;
        public const int GreenGemSavedBit = 4;
        public const int PurpleGemSavedBit = 8;
        public const int BlueGemSavedBit = 16;
        public const int Gem105SavedBit = 32;

        public static uint PowerUpAddress = 0x69328;

        public static uint GameStateAddress = 0x690ed;
        public static uint LevelIDAddress = 0x60c1c;
    }
    public static class USAddresses
    {
        public const uint CyrstalReceivedAddress = 0x68fd9;
        public const uint CrystalSavedAddress = 0x6912d;

        public const uint GemReceivedAddress = 0x68fb8;
        public const uint GemSavedAddress = 0x68fb0;

        
        public const uint RelicsSapphireReceivedAddress = 0x691c5;
        public const uint RelicsSapphireSavedAddress = 0x691b5;
        public const uint RelicsGoldReceivedAddress = 0x691cd;
        public const uint RelicsGoldSavedAddress = 0x691bd;

        public const uint ColoredGemReceivedAddress = 0x68fbf;
        public const int RedGemReceivedBit = 2;
        public const int GreenGemReceivedBit = 4;
        public const int PurpleGemReceivedBit = 8;
        public const int BlueGemReceivedBit = 16;
        public const int Gem105ReceivedBit = 32;

        public const uint ColoredGemSavedAddress = 0x68fb7;
        public const int RedGemSavedBit = 2;
        public const int GreenGemSavedBit = 4;
        public const int PurpleGemSavedBit = 8;
        public const int BlueGemSavedBit = 16;
        public const int Gem105SavedBit = 32;

        public static uint PowerUpAddress = 0x69178;

        public static uint GameStateAddress = 0x68f3d;
        public static uint LevelIDAddress = 0x68ef9;
    }
    public static class GameConfig
    {
        public static uint CyrstalReceivedAddress;
        public static uint CrystalSavedAddress;

        public static uint GemReceivedAddress;
        public static uint GemSavedAddress;

        public static uint RelicsSapphireSavedAddress;
        public static uint RelicsGoldSavedAddress;

        public static uint RelicsSapphireReceivedAddress;
        public static uint RelicsGoldReceivedAddress;

        public static uint ColoredGemReceivedAddress;

        public static uint ColoredGemSavedAddress;

        public static uint PowerUpAddress;

        public static uint GameStateAddress;
        public static uint LevelIDAddress;

        public static void LoadUSAddresses()
        {
            CyrstalReceivedAddress = USAddresses.CyrstalReceivedAddress;
            CrystalSavedAddress = USAddresses.CrystalSavedAddress;

            GemReceivedAddress = USAddresses.GemReceivedAddress;
            GemSavedAddress = USAddresses.GemSavedAddress;

            RelicsSapphireReceivedAddress = USAddresses.RelicsSapphireReceivedAddress;
            RelicsGoldReceivedAddress = USAddresses.RelicsGoldReceivedAddress;

            RelicsSapphireSavedAddress = USAddresses.RelicsSapphireSavedAddress;
            RelicsGoldSavedAddress = USAddresses.RelicsGoldSavedAddress;

            ColoredGemReceivedAddress = USAddresses.ColoredGemReceivedAddress;

            ColoredGemSavedAddress = USAddresses.ColoredGemSavedAddress;

            PowerUpAddress = USAddresses.PowerUpAddress;

            GameStateAddress = USAddresses.GameStateAddress;
            LevelIDAddress = USAddresses.LevelIDAddress;
        }

        public static void LoadEUAddresses()
        {
            //Crystals
            CyrstalReceivedAddress = EUAddresses.CyrstalReceivedAddress;
            CrystalSavedAddress = EUAddresses.CrystalSavedAddress;
            
            //Clear Gems
            GemReceivedAddress = EUAddresses.GemReceivedAddress;
            GemSavedAddress = EUAddresses.GemSavedAddress;

            //Colored Gems
            ColoredGemReceivedAddress = EUAddresses.ColoredGemReceivedAddress;
            ColoredGemSavedAddress = EUAddresses.ColoredGemSavedAddress;

            // Relics
            RelicsSapphireReceivedAddress = EUAddresses.RelicsSapphireReceivedAddress;
            RelicsGoldReceivedAddress = EUAddresses.RelicsGoldReceivedAddress;
            RelicsSapphireSavedAddress = EUAddresses.RelicsSapphireSavedAddress;
            RelicsGoldSavedAddress = EUAddresses.RelicsGoldSavedAddress;

            PowerUpAddress = EUAddresses.PowerUpAddress;

            GameStateAddress = EUAddresses.GameStateAddress;
            LevelIDAddress = EUAddresses.LevelIDAddress;
        }

    }

   
}
