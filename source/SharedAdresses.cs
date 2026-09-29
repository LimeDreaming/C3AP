using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C3AP_Client
{
    public static class SharedAdresses
    {
        //Offset for Crash Data
        public static uint AnimationIDOffset = 0x1c;
        public static uint XPositionOffset = 0x60;
        public static uint YPositionOffset = 0x64;
        public static uint ZPositionOffset = 0x68;
        public static uint PlaneLifeOffset = 0xe5; //Only in Plane Levels
        public static uint WumpaOffset = 0x141;
        public static uint LivesIDOffset = 0x145;
        public static uint CratesOffset = 0x169;


        public static Dictionary<string, int> GameStateID = new Dictionary<string, int>
        {
            {"ERC State", 0x00}, // ERC = Entering Stage/Respawning/Cutscene
            {"Normal Gameplay", 0x01},
            {"Game Over", 0x02},
            {"Complete Level", 0x03},
            {"Exit Level", 0x04},
            {"Exit Game Over", 0x06},
        };

        public static Dictionary<string, int> OutLevelIDs = new Dictionary<string, int>
        {
            {"Intro", 0x28},
            {"Incomplete Ending", 0x29},
            {"100% Ending", 0x2a},
            {"Enemy Talk", 0x3a},
            {"Game Over", 0x3b},
            {"Starttitles", 0x3c},
        };
        public static Dictionary<string, int> BossLevelIDs = new Dictionary<string, int>
        {
            {"Tiny Tiger", 0x06},
            {"Dingodile", 0x03},
            {"N. Tropy", 0x04},
            {"N. Gin", 0x05},
            {"N. Cortex", 0x07},
        };
        public static Dictionary<string, int> LevelIDs = new Dictionary<string, int>
        {
            {"Warp Room", 0x02},
            {"Toad Village", 0x0b},
            {"Under Pressure", 0x0e},
            {"Orient Express", 0x0a},
            {"Bone Yard", 0x0c},
            {"Makin' Waves", 0x19},

            {"Gee Wiz", 0x0f},
            {"Hang'em High", 0x16},
            {"Hog Ride", 0x15},
            {"Tomb Time", 0x12},
            {"Midnight Run", 0x11},
            
            {"Dino Might!", 0x10},
            {"Deep Trouble", 0x1c},
            {"High Time", 0x1a},
            {"Road Crash", 0x14},
            {"Double Header", 0x1d},
            
            {"Sphynxinator", 0x1e},
            {"Bye Bye Blimps", 0x13},
            {"Tell No Tales", 0x0d},
            {"Future Frenzy", 0x1b},
            {"Tomb Wader", 0x18},
            
            {"Gone Tomorrow", 0x23},
            {"Orange Asphalt", 0x20},
            {"Flaming Passion", 0x22},
            {"Mad Bombers", 0x17},
            {"Bug Lite", 0x24},
            
            {"Ski Crazed", 0x21},
            {"Area 51?", 0x25},
            {"Rings Of Power", 0x1f},
            {"Hot Coco", 0x27},
            {"Eggipus Rex", 0x26},

            //Bosses
            {"Tiny Tiger", 0x06},
            {"Dingodile", 0x03},
            {"N. Tropy", 0x04},
            {"N. Gin", 0x05},
            {"N. Cortex", 0x07},
        };

        public static Dictionary<string, long> LevelAPLocationIDs = new Dictionary<string, long>
        {
            //Levels
            {"Toad Village", 142266},
            {"Under Pressure", 142267},
            {"Orient Express", 142268},
            {"Bone Yard", 142269},
            {"Makin' Waves", 142270},

            {"Gee Wiz", 142271},
            {"Hang'em High", 142272},
            {"Hog Ride", 142273},
            {"Tomb Time", 142274},
            {"Midnight Run", 142275},

            {"Dino Might!", 142276},
            {"Deep Trouble", 142277},
            {"High Time", 142278},
            {"Road Crash", 142279},
            {"Double Header", 142280},

            {"Sphynxinator", 142281},
            {"Bye Bye Blimps", 142282},
            {"Tell No Tales", 142283},
            {"Future Frenzy", 142284},
            {"Tomb Wader", 142285},

            {"Gone Tomorrow", 142286},
            {"Orange Asphalt", 142287},
            {"Flaming Passion", 142288},
            {"Mad Bombers", 142289},
            {"Bug Lite", 142290},

            {"Ski Crazed", 142291},
            {"Area 51?", 142292},
            {"Rings of Power", 142293},
            {"Hot Coco", 142294},
            {"Eggipus Rex", 142295},

            //Bosses
            {"Tiny Tiger",142126 },
            {"Dingodile",142154 },
            {"N. Tropy",142184 },
            {"N. Gin",142214 },
            //{"N. Cortex",1420 },
        };

        public record APItemData(string LevelName, long LevelAPCheckId, long LevelAPItemId, int Level);

        public static readonly List<APItemData> CrystalItems = new()
        {
            new APItemData("Toad Village", 142100, 142000, 1),
            new APItemData("Under Pressure", 142105, 142001, 2),
            new APItemData("Orient Express", 142110, 142002, 3),
            new APItemData("Bone Yard", 142115, 142003, 4),
            new APItemData("Makin' Waves", 142121, 142004, 5),

            new APItemData("Gee Wiz", 142127, 142005, 6),
            new APItemData("Hang'em High", 142132, 142006, 7),
            new APItemData("Hog Ride", 142138, 142007, 8),
            new APItemData("Tomb Time", 142143, 142008, 9),
            new APItemData("Midnight Run", 142149, 142009, 10),

            new APItemData("Dino Might!", 142155, 142010, 11),
            new APItemData("Deep Trouble", 142161, 142011, 12),
            new APItemData("High Time", 142167, 142012, 13),
            new APItemData("Road Crash", 142173, 142013, 14),
            new APItemData("Double Header", 142179, 142014, 15),

            new APItemData("Sphynxinator", 142185, 142015, 16),
            new APItemData("Bye Bye Blimps", 142191, 142016, 17),
            new APItemData("Tell No Tales", 142196, 142017, 18),
            new APItemData("Future Frenzy", 142202, 142018, 19),
            new APItemData("Tomb Wader", 142208, 142019, 20),

            new APItemData("Gone Tomorrow", 142215, 142020, 21),
            new APItemData("Orange Asphalt", 142221, 142021, 22),
            new APItemData("Flaming Passion", 142226, 142022, 23),
            new APItemData("Mad Bombers", 142232, 142023, 24),
            new APItemData("Bug Lite", 142237, 142024, 25),
        };

        // Clear Gem Box Items

        public static readonly List<APItemData> ClearGemBoxItems = new()
        {
            //Clear Gem Box
            new APItemData("Toad Village", 142101, 142025, 1),
            new APItemData("Under Pressure", 142106, 142026, 2),
            new APItemData("Orient Express", 142111, 142027, 3),
            new APItemData("Bone Yard", 142116, 142028, 4),
            new APItemData("Makin' Waves", 142122, 142030, 5),

            new APItemData("Gee Wiz", 142128, 142031, 6),
            new APItemData("Hang'em High", 142133, 142032, 7),
            new APItemData("Hog Ride", 142139, 142033, 8),
            new APItemData("Tomb Time", 142144, 142034, 9),
            new APItemData("Midnight Run", 142150, 142036, 10),

            new APItemData("Dino Might!", 142156, 142037, 11),
            new APItemData("Deep Trouble", 142162, 142039, 12),
            new APItemData("High Time", 142168, 142040, 13),
            new APItemData("Road Crash", 142174, 142041, 14),
            new APItemData("Double Header", 142180, 142042, 15),

            new APItemData("Sphynxinator", 142186, 142043, 16),
            new APItemData("Bye Bye Blimps", 142192, 142045, 17),
            new APItemData("Tell No Tales", 142197, 142046, 18),
            new APItemData("Future Frenzy", 142203, 142047, 19),
            new APItemData("Tomb Wader", 142209, 142049, 20),

            new APItemData("Gone Tomorrow", 142216, 142050, 21),
            new APItemData("Orange Asphalt", 142222, 142052, 22),
            new APItemData("Flaming Passion", 142227, 142053, 23),
            new APItemData("Mad Bombers", 142233, 142054, 24),
            new APItemData("Bug Lite", 142238, 142055, 25),

            new APItemData("Ski Crazed", 142244, 142057, 26),
            new APItemData("Area 51?", 142248, 142058, 28),
            new APItemData("Rings of Power", 142253, 142060, 30),
            new APItemData("Hot Coco", 142258, 142062, 31),
            new APItemData("Eggipus Rex", 142262, 142063, 32),
            
        };

        public static readonly List<APItemData> ClearGemItems = new()
        {
            //Clear Gems
            new APItemData("Bone Yard", 142117, 142029, 4),
            new APItemData("Tomb Time", 142145, 142035, 9),
            new APItemData("Dino Might!", 142157, 142038, 11),
            new APItemData("Sphynxinator", 142187, 142044, 16),
            new APItemData("Future Frenzy", 142204, 142048, 19),
            new APItemData("Gone Tomorrow", 142217, 142051, 21),
            new APItemData("Bug Lite", 142239, 142056, 25),
            new APItemData("Area 51?", 142249, 142059, 28),
            new APItemData("Rings of Power", 142254, 142061, 30),

        };

        public static readonly List<APItemData> ColoredGemItems = new()
        {
            //Clear Gems
            new APItemData("Tomb Wader", 142210, 142064, 20), // Blue Gem
            new APItemData("Deep Trouble", 142163, 142065, 12), // Red Gem
            new APItemData("Flaming Passion", 142228, 142066, 23), // Green Gem
            new APItemData("Hang'em High", 142134, 142067, 7), // Yellow Gem
            new APItemData("High Time", 142169, 142068, 13), // Purple Gem
        };

        public static readonly List<APItemData> RelicSItems = new()
        {
            new APItemData("Toad Village", 142102, 142069, 1),
            new APItemData("Under Pressure", 142107, 142070, 2),
            new APItemData("Orient Express", 142112, 142071, 3),
            new APItemData("Bone Yard", 142118, 142072, 4),
            new APItemData("Makin' Waves", 142123, 142073, 5),

            new APItemData("Gee Wiz", 142129, 142074, 6),
            new APItemData("Hang'em High", 142135, 142075, 7),
            new APItemData("Hog Ride", 142140, 142076, 8),
            new APItemData("Tomb Time", 142146, 142077, 9),
            new APItemData("Midnight Run", 142151, 142078, 10),

            new APItemData("Dino Might!", 142158, 142079, 11),
            new APItemData("Deep Trouble", 142164, 142080, 12),
            new APItemData("High Time", 142170, 142081, 13),
            new APItemData("Road Crash", 142176, 142082, 14),
            new APItemData("Double Header", 142181, 142083, 15),

            new APItemData("Sphynxinator", 142188, 142084, 16),
            new APItemData("Bye Bye Blimps", 142193, 142085, 17),
            new APItemData("Tell No Tales", 142199, 142086, 18),
            new APItemData("Future Frenzy", 142205, 142087, 19),
            new APItemData("Tomb Wader", 142211, 142088, 20),

            new APItemData("Gone Tomorrow", 142218, 142089, 21),
            new APItemData("Orange Asphalt", 142223, 142090, 22),
            new APItemData("Flaming Passion", 142229, 142091, 23),
            new APItemData("Mad Bombers", 142234, 142092, 24),
            new APItemData("Bug Lite", 142240, 142093, 25),

            new APItemData("Ski Crazed", 142245, 142094, 26),
            new APItemData("Area 51?", 142250, 142095, 28),
            new APItemData("Rings of Power", 142255, 142096, 30),
            new APItemData("Hot Coco", 142259, 142097, 31),
            new APItemData("Eggipus Rex", 142263, 142098, 32),
        };

        public static readonly List<APItemData> RelicGItems = new()
        {
            new APItemData("Toad Village", 142103, 142099, 1),
            new APItemData("Under Pressure", 142108, 142100, 2),
            new APItemData("Orient Express", 142113, 142101, 3),
            new APItemData("Bone Yard", 142119, 142102, 4),
            new APItemData("Makin' Waves", 142124, 142103, 5),

            new APItemData("Gee Wiz", 142130, 142104, 6),
            new APItemData("Hang'em High", 142136, 142105, 7),
            new APItemData("Hog Ride", 142141, 142106, 8),
            new APItemData("Tomb Time", 142147, 142107, 9),
            new APItemData("Midnight Run", 142152, 142108, 10),

            new APItemData("Dino Might!", 142159, 142109, 11),
            new APItemData("Deep Trouble", 142165, 142110, 12),
            new APItemData("High Time", 142171, 142111, 13),
            new APItemData("Road Crash", 142177, 142112, 14),
            new APItemData("Double Header", 142182, 142113, 15),

            new APItemData("Sphynxinator", 142189, 142114, 16),
            new APItemData("Bye Bye Blimps", 142194, 142115, 17),
            new APItemData("Tell No Tales", 142200, 142116, 18),
            new APItemData("Future Frenzy", 142206, 142117, 19),
            new APItemData("Tomb Wader", 142212, 142118, 20),

            new APItemData("Gone Tomorrow", 142219, 142119, 21),
            new APItemData("Orange Asphalt", 142224, 142120, 22),
            new APItemData("Flaming Passion", 142230, 142121, 23),
            new APItemData("Mad Bombers", 142235, 142122, 24),
            new APItemData("Bug Lite", 142241, 142123, 25),

            new APItemData("Ski Crazed", 142246, 142124, 26),
            new APItemData("Area 51?", 142251, 142125, 28),
            new APItemData("Rings of Power", 142256, 142126, 30),
            new APItemData("Hot Coco", 142260, 142127, 31),
            new APItemData("Eggipus Rex", 142264, 142128, 32),
        };

        public static readonly List<APItemData> RelicPItems = new()
        {
            new APItemData("Toad Village", 142104, 142129, 1),
            new APItemData("Under Pressure", 142109, 142130, 2),
            new APItemData("Orient Express", 142114, 142131, 3),
            new APItemData("Bone Yard", 142120, 142132, 4),
            new APItemData("Makin' Waves", 142125, 142133, 5),

            new APItemData("Gee Wiz", 142131, 142134, 6),
            new APItemData("Hang'em High", 142137, 142135, 7),
            new APItemData("Hog Ride", 142142, 142136, 8),
            new APItemData("Tomb Time", 142148, 142137, 9),
            new APItemData("Midnight Run", 142153, 142138, 10),

            new APItemData("Dino Might!", 142160, 142139, 11),
            new APItemData("Deep Trouble", 142166, 142140, 12),
            new APItemData("High Time", 142172, 142141, 13),
            new APItemData("Road Crash", 142178, 142142, 14),
            new APItemData("Double Header", 142183, 142143, 15),

            new APItemData("Sphynxinator", 142190, 142144, 16),
            new APItemData("Bye Bye Blimps", 142195, 142145, 17),
            new APItemData("Tell No Tales", 142201, 142146, 18),
            new APItemData("Future Frenzy", 142207, 142147, 19),
            new APItemData("Tomb Wader", 142213, 142148, 20),

            new APItemData("Gone Tomorrow", 142220, 142149, 21),
            new APItemData("Orange Asphalt", 142225, 142150, 22),
            new APItemData("Flaming Passion", 142231, 142151, 23),
            new APItemData("Mad Bombers", 142236, 142152, 24),
            new APItemData("Bug Lite", 142242, 142153, 25),

            new APItemData("Ski Crazed", 142247, 142154, 26),
            new APItemData("Area 51?", 142252, 142155, 28),
            new APItemData("Rings of Power", 142257, 142156, 30),
            new APItemData("Hot Coco", 142261, 142157, 31),
            new APItemData("Eggipus Rex", 142265, 142158, 32),
        };

        // Death ID's
        public static Dictionary<int, string> DeathAnimationIDsEU = new Dictionary<int, string>
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

        public static Dictionary<int, string> DeathAnimationIDsUS = new Dictionary<int, string>
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
}
