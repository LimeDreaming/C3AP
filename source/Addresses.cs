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
        public const uint CrystalMirrorAddress = 0x692e1;

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

        public static uint PauseStateAddress = 0x609e0;
        public static uint BonusStateAddress = 0x60c5c; // Still need to be proofed that is the Bonus Level State

        public static uint GameStateAddress = 0x690ed;
        public static uint LevelIDAddress = 0x60c1c;
        public static uint LateLevelIDAddress = 0x690a9;

        public static Dictionary<int, uint> LevelDataAddress = new Dictionary<int, uint>
        {
            {0x02, 0xb5db4},
            {0x0b, 0xad298},
            {0x0e, 0xad08c},
            {0x0a, 0xad2e8},
            {0x0c, 0xad514},
            {0x19, 0xacf60},

            {0x0f, 0xad488},
            {0x16, 0xad36c},
            {0x15, 0xad5e0},
            {0x12, 0xad494},
            {0x11, 0xad318},

            {0x10, 0xad654},
            {0x1c, 0xad22c},
            {0x1a, 0xad310},
            {0x14, 0xad7bc},
            {0x1d, 0xad32c},

            {0x1e, 0xad7bc},
            {0x13, 0xbcbe0},
            {0x0d, 0xacfac},
            {0x1b, 0xad4a0},
            {0x18, 0xad66c},

            {0x23, 0xad410},
            {0x20, 0xadaf4},
            {0x22, 0xad3d8},
            {0x17, 0xbcc68},
            {0x24, 0xad600},

            {0x21, 0xace78},
            {0x25, 0xad980},
            {0x1f, 0xbccb4},
            {0x27, 0xace90},
            {0x26, 0xacee0},

            //Bosses
            {0x06, 0xbcc04},
            {0x03, 0xbcf60},
            {0x04, 0xacee0},
            {0x05, 0xbce90},
            {0x07, 0xacef0},
        };

        public static Dictionary<int, string> DeathAnimationIDs = new Dictionary<int, string>
        {
            {0x12 ,"Eingesaugt"},
            {0x13 ,"death by fugu"},
            {0x14 ,"exploded"},
            {0x27 ,""},
            {0x2c ,"crashed the plane"},
            {0x42 ,"fall from the jetski"},
            {0x44 ,""},
            {0x47 ,"has drown"},
            {0x4e ,"got hit by a scorpion"},
            {0x4f ,"got a nice cut!"},
            {0x50 ,"sees only dark!"},
            {0x51 ,"would be put into a bottle!"},
            {0x75 ,"fall deep into a hole"},
            {0x76 ,"got scared to death"},
            {0x77 ,"nitro crates aren't toys!"},
            {0x79 ,"got electrified"},
            {0x7a ,"that was hot!"},
            {0x7b ,"exploded by TNT/Nitro crate"},
            {0x7c ,"catched by enemy"},
            {0x7d ,"cuted in half!"},
            {0x7e ,"goats are not your friends?"},
            {0x7f ,"got date!?"},
            {0x80 ,"tried to break the 4th wall"},
            {0x81 ,"is searching for a princess!"},
            {0x82 ,"is part of the eco system!"},
            {0x83 ,"got smashed by a big dino!"},
            {0x84 ,"pancaked themself!"},
            {0x85 ,"pancaked themself!"},
            {0x86 ,"noticed that spikes are scarp!"},
            {0x87 ,"got hit by a monkey"},
            {0x88 ,"yes coffins are heavy!"},
            {0x89 ,"got hugged by a mumie"},
            {0x8a ,"was paralyzed by arrows!"},
            {0x8b ,"got pulverized by time!"},
            {0x8c ,"got pulverized!"},
            {0x8d ,"thought beam me up!"},
            {0x8e ,"lost air like a ballon!"},
            {0x90 ,"got smashed by Tiny Tiger"},
            {0x9f ,"got stoped by enemy"},
            {0x0a ,"hit the wall with their tiger!"}
        };
    }
    public static class USAddresses
    {
        public const uint CyrstalReceivedAddress = 0x68fd9;
        public const uint CrystalSavedAddress = 0x6912d;
        public const uint CrystalMirrorAddress = 0x69231;

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

        public static uint PauseStateAddress = 0x6087c;
        public static uint BonusStateAddress = 0x60afc; // Still need to be proofed that is the Bonus Level State

        public static uint GameStateAddress = 0x68f3d;
        public static uint LevelIDAddress = 0x68ef9;
        public static uint LateLevelIDAddress = 0x68ef9;

        public static Dictionary<int, uint> LevelDataAddress = new Dictionary<int, uint>
        {
            {0x02, 0xb5dd0},
            {0x0b, 0xad0ec},
            {0x0e, 0xacee4},
            {0x0a, 0xad13c},
            {0x0c, 0xad368},
            {0x19, 0xacdb8},

            {0x0f, 0xad2dc},
            {0x16, 0xad1c4},
            {0x15, 0xad418},
            {0x12, 0xad2e0},
            {0x11, 0xad170},

            {0x10, 0xad4a8},
            {0x1c, 0xad07c},
            {0x1a, 0xad168},
            {0x14, 0xad610},
            {0x1d, 0xad180},

            {0x1e, 0xad5e8},
            {0x13, 0xbca24},
            {0x0d, 0xace00},
            {0x1b, 0xad2f4},
            {0x18, 0xad4c4},

            {0x23, 0xad264},
            {0x20, 0xad948},
            {0x22, 0xad238},
            {0x17, 0xbcaac},
            {0x24, 0xad454},

            {0x21, 0xace0d},
            {0x25, 0xad919},
            {0x1f, 0xbcc39},
            {0x27, 0xace25},
            {0x26, 0xace75},

            //Bosses
            {0x06, 0xbca5c},
            {0x03, 0xbccc8},
            {0x04, 0xacc7c},
            {0x05, 0xbcc44},
            {0x07, 0xacc0c},
        };

        public static Dictionary<int, string> DeathAnimationIDs = new Dictionary<int, string>
        {
            {0x11 ,"Eingesaugt"},
            {0x12 ,"death by fugu"},
            {0x13 ,"exploded"},
            {0x25 ,""},
            {0x2a ,"crashed the plane"},
            {0x40 ,"fall from the jetski"},
            {0x42 ,""},
            {0x45 ,"has drown"},
            {0x4b ,"got hit by a scorpion"},
            {0x4c ,"got a nice cut!"},
            {0x4d ,"sees only dark!"},
            {0x4e ,"would be put into a bottle!"},
            {0x72 ,"fall deep into a hole"},
            {0x73 ,"got scared to death"},
            {0x74 ,"nitro crates aren't toys!"},
            {0x76 ,"got electrified"},
            {0x77 ,"that was hot!"},
            {0x78 ,"exploded by TNT/Nitro crate"},
            {0x79 ,"catched by enemy"},
            {0x7a ,"cuted in half!"},
            {0x7b ,"goats are not your friends?"},
            {0x7c ,"got date!?"},
            {0x7d ,"tried to break the 4th wall"},
            {0x7e ,"is searching for a princess!"},
            {0x7f ,"is part of the eco system!"},
            {0x80 ,"got smashed by a big dino!"},
            {0x81 ,"pancaked themself!"},
            {0x82 ,"pancaked themself!"},
            {0x83 ,"noticed that spikes are scarp!"},
            {0x84 ,"got hit by a monkey"},
            {0x85 ,"yes coffins are heavy!"},
            {0x86 ,"got hugged by a mumie"},
            {0x87 ,"was paralyzed by arrows!"},
            {0x88 ,"got pulverized by time!"},
            {0x89 ,"got pulverized!"},
            {0x8a ,"thought beam me up!"},
            {0x8b ,"lost air like a ballon!"},
            {0x8d ,"got smashed by Tiny Tiger"},
            {0x9c ,"got stoped by enemy"},
            {0x9d ,"hit the wall with their tiger!"}
        };
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

        public static uint PauseStateAddress;
        public static uint BonusStateAddress;

        public static uint GameStateAddress;
        public static uint LevelIDAddress;
        public static uint LateLevelIDAddress;

        public static Dictionary<int, uint> LevelDataAddress;

        public static Dictionary<int, string> DeathAnimationIDs;

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

            PauseStateAddress = USAddresses.PauseStateAddress;
            BonusStateAddress = USAddresses.BonusStateAddress;

            GameStateAddress = USAddresses.GameStateAddress;
            LevelIDAddress = USAddresses.LevelIDAddress;
            LateLevelIDAddress = USAddresses.LateLevelIDAddress;

            LevelDataAddress = USAddresses.LevelDataAddress;
            DeathAnimationIDs = USAddresses.DeathAnimationIDs;
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

            PauseStateAddress = EUAddresses.PauseStateAddress;
            BonusStateAddress = EUAddresses.BonusStateAddress; 

            GameStateAddress = EUAddresses.GameStateAddress;
            LevelIDAddress = EUAddresses.LevelIDAddress;
            LateLevelIDAddress = EUAddresses.LateLevelIDAddress;

            LevelDataAddress = EUAddresses.LevelDataAddress;
            DeathAnimationIDs = EUAddresses.DeathAnimationIDs;
        }

    }

   
}
