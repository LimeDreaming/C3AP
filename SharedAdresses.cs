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
        public static uint XPositionOffset = 0x1c;
        public static uint YPositionOffset = 0x1c;
        public static uint ZPositionOffset = 0x1c;
        public static uint PlaneLifeOffset = 0x1c; //Only in Plane Levels
        public static uint WumpaOffset = 0x1c;
        public static uint LivesIDOffset = 0x1c;
        public static uint CratesOffset = 0x1c;

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
        };

        public static Dictionary<string, long> LevelAPCrystalIDs = new Dictionary<string, long>
        {
            {"Toad Village", 142100},
            {"Under Pressure", 142105},
            {"Orient Express", 142110},
            {"Bone Yard", 142115},
            {"Makin' Waves", 142121},

            {"Gee Wiz", 142127},
            {"Hang'em High", 142132},
            {"Hog Ride", 142138},
            {"Tomb Time", 142143},
            {"Midnight Run", 142149},

            {"Dino Might!", 142155},
            {"Deep Trouble", 142161},
            {"High Time", 142167},
            {"Road Crash", 142173},
            {"Double Header", 142179},

            {"Sphynxinator", 142185},
            {"Bye Bye Blimps", 142191},
            {"Tell No Tales", 142196},
            {"Future Frenzy", 142202},
            {"Tomb Wader", 142208},

            {"Gone Tomorrow", 142215},
            {"Orange Asphalt", 142221},
            {"Flaming Passion", 142226},
            {"Mad Bombers", 142232},
            {"Bug Lite", 142237}
        };

        public record APItemData(string LevelName, long LevelAPCheckId, long LevelAPItemId);

        public static readonly List<APItemData> CrystalItems = new()
        {
            new APItemData("Toad Village", 142100, 142000),
            new APItemData("Under Pressure", 142105, 142001),
            new APItemData("Orient Express", 142110, 142002),
            new APItemData("Bone Yard", 142115, 142003),
            new APItemData("Makin' Waves", 142121, 142004),

            new APItemData("Gee Wiz", 142127, 142005),
            new APItemData("Hang'em High", 142132, 142006),
            new APItemData("Hog Ride", 142138, 142007),
            new APItemData("Tomb Time", 142143, 142008),
            new APItemData("Midnight Run", 142149, 142009),

            new APItemData("Dino Might!", 142155, 142010),
            new APItemData("Deep Trouble", 142161, 142011),
            new APItemData("High Time", 142167, 142012),
            new APItemData("Road Crash", 142173, 142013),
            new APItemData("Double Header", 142179, 142014),

            new APItemData("Sphynxinator", 142185, 142015),
            new APItemData("Bye Bye Blimps", 142191, 142016),
            new APItemData("Tell No Tales", 142196, 142017),
            new APItemData("Future Frenzy", 142202, 142018),
            new APItemData("Tomb Wader", 142208, 142019),

            new APItemData("Gone Tomorrow", 142215, 142020),
            new APItemData("Orange Asphalt", 142221, 142021),
            new APItemData("Flaming Passion", 142226, 142022),
            new APItemData("Mad Bombers", 142232, 142023),
            new APItemData("Bug Lite", 142237, 142024),
        };

        // Clear Gem Box Items

        public static readonly List<APItemData> ClearGemBoxItems = new()
        {
            //Clear Gem Box
            new APItemData("Toad Village", 142101, 142025),
            new APItemData("Under Pressure", 142106, 142026),
            new APItemData("Orient Express", 142111, 142027),
            new APItemData("Bone Yard", 142116, 142028),
            new APItemData("Makin' Waves", 142122, 142030),

            new APItemData("Gee Wiz", 142128, 142031),
            new APItemData("Hang'em High", 142133, 142032),
            new APItemData("Hog Ride", 142139, 142033),
            new APItemData("Tomb Time", 142144, 142034),
            new APItemData("Midnight Run", 142150, 142036),

            new APItemData("Dino Might!", 142156, 142037),
            new APItemData("Deep Trouble", 142162, 142039),
            new APItemData("High Time", 142168, 142040),
            new APItemData("Road Crash", 142174, 142041),
            new APItemData("Double Header", 142180, 142042),

            new APItemData("Sphynxinator", 142186, 142043),
            new APItemData("Bye Bye Blimps", 142192, 142045),
            new APItemData("Tell No Tales", 142197, 142046),
            new APItemData("Future Frenzy", 142203, 142047),
            new APItemData("Tomb Wader", 142209, 142049),

            new APItemData("Gone Tomorrow", 142216, 142050),
            new APItemData("Orange Asphalt", 142222, 142052),
            new APItemData("Flaming Passion", 142227, 142053),
            new APItemData("Mad Bombers", 142233, 142054),
            new APItemData("Bug Lite", 142238, 142055),

            new APItemData("Ski Crazed", 142244, 142057),
            new APItemData("Area 51?", 142248, 142058),
            new APItemData("Rings of Power", 142253, 142060),
            new APItemData("Hot Coco", 142258, 142062),
            new APItemData("Eggipus Rex", 142262, 142063),
            
        };

        public static readonly List<APItemData> ClearGemItems = new()
        {
            //Clear Gems
            new APItemData("Bone Yard", 142117, 142029),
            new APItemData("Tomb Time", 142145, 142035),
            new APItemData("Dino Might!", 142157, 142038),
            new APItemData("Sphynxinator", 142187, 142044),
            new APItemData("Future Frenzy", 142204, 142048),
            new APItemData("Gone Tomorrow", 142217, 142051),
            new APItemData("Bug Lite", 142239, 142056),
            new APItemData("Area 51?", 142249, 142059),
            new APItemData("Rings of Power", 142254, 142061),

        };

        public static readonly List<APItemData> ColoredGemItems = new()
        {
            //Clear Gems
            new APItemData("Tomb Wader", 142210, 142064), // Blue Gem
            new APItemData("Deep Trouble", 142163, 142065), // Red Gem
            new APItemData("Flaming Passion", 142228, 142066), // Green Gem
            new APItemData("Hang'em High", 142134, 142067), // Yellow Gem
            new APItemData("High Time", 142169, 142068), // Purple Gem
        };

        public static readonly List<APItemData> RelicSItems = new()
        {
            new APItemData("Toad Village", 142102, 142069),
            new APItemData("Under Pressure", 142107, 142070),
            new APItemData("Orient Express", 142112, 142071),
            new APItemData("Bone Yard", 142118, 142072),
            new APItemData("Makin' Waves", 142123, 142073),

            new APItemData("Gee Wiz", 142129, 142074),
            new APItemData("Hang'em High", 142135, 142075),
            new APItemData("Hog Ride", 142140, 142076),
            new APItemData("Tomb Time", 142146, 142077),
            new APItemData("Midnight Run", 142151, 142078),

            new APItemData("Dino Might!", 142158, 142079),
            new APItemData("Deep Trouble", 142164, 142080),
            new APItemData("High Time", 142170, 142081),
            new APItemData("Road Crash", 142176, 142082),
            new APItemData("Double Header", 142181, 142083),

            new APItemData("Sphynxinator", 142188, 142084),
            new APItemData("Bye Bye Blimps", 142193, 142085),
            new APItemData("Tell No Tales", 142199, 142086),
            new APItemData("Future Frenzy", 142205, 142087),
            new APItemData("Tomb Wader", 142211, 142088),

            new APItemData("Gone Tomorrow", 142218, 142089),
            new APItemData("Orange Asphalt", 142223, 142090),
            new APItemData("Flaming Passion", 142229, 142091),
            new APItemData("Mad Bombers", 142234, 142092),
            new APItemData("Bug Lite", 142240, 142093),

            new APItemData("Ski Crazed", 142245, 142094),
            new APItemData("Area 51?", 142250, 142095),
            new APItemData("Rings of Power", 142255, 142096),
            new APItemData("Hot Coco", 142259, 142097),
            new APItemData("Eggipus Rex", 142263, 142098),
        };

        public static readonly List<APItemData> RelicGItems = new()
        {
            new APItemData("Toad Village", 142103, 142099),
            new APItemData("Under Pressure", 142108, 142100),
            new APItemData("Orient Express", 142113, 142101),
            new APItemData("Bone Yard", 142119, 142102),
            new APItemData("Makin' Waves", 142124, 142103),

            new APItemData("Gee Wiz", 142130, 142104),
            new APItemData("Hang'em High", 142136, 142105),
            new APItemData("Hog Ride", 142141, 142106),
            new APItemData("Tomb Time", 142147, 142107),
            new APItemData("Midnight Run", 142152, 142108),

            new APItemData("Dino Might!", 142159, 142109),
            new APItemData("Deep Trouble", 142165, 142110),
            new APItemData("High Time", 142171, 142111),
            new APItemData("Road Crash", 142177, 142112),
            new APItemData("Double Header", 142182, 142113),

            new APItemData("Sphynxinator", 142189, 142114),
            new APItemData("Bye Bye Blimps", 142194, 142115),
            new APItemData("Tell No Tales", 142200, 142116),
            new APItemData("Future Frenzy", 142206, 142117),
            new APItemData("Tomb Wader", 142212, 142118),

            new APItemData("Gone Tomorrow", 142219, 142119),
            new APItemData("Orange Asphalt", 142224, 142120),
            new APItemData("Flaming Passion", 142230, 142121),
            new APItemData("Mad Bombers", 142235, 142122),
            new APItemData("Bug Lite", 142241, 142123),

            new APItemData("Ski Crazed", 142246, 142124),
            new APItemData("Area 51?", 142251, 142125),
            new APItemData("Rings of Power", 142256, 142126),
            new APItemData("Hot Coco", 142260, 142127),
            new APItemData("Eggipus Rex", 142264, 142128),
        };

        public static readonly List<APItemData> RelicPItems = new()
        {
            new APItemData("Toad Village", 142104, 142129),
            new APItemData("Under Pressure", 142109, 142130),
            new APItemData("Orient Express", 142114, 142131),
            new APItemData("Bone Yard", 142120, 142132),
            new APItemData("Makin' Waves", 142125, 142133),

            new APItemData("Gee Wiz", 142131, 142134),
            new APItemData("Hang'em High", 142137, 142135),
            new APItemData("Hog Ride", 142142, 142136),
            new APItemData("Tomb Time", 142148, 142137),
            new APItemData("Midnight Run", 142153, 142138),

            new APItemData("Dino Might!", 142160, 142139),
            new APItemData("Deep Trouble", 142166, 142140),
            new APItemData("High Time", 142172, 142141),
            new APItemData("Road Crash", 142178, 142142),
            new APItemData("Double Header", 142183, 142143),

            new APItemData("Sphynxinator", 142190, 142144),
            new APItemData("Bye Bye Blimps", 142195, 142145),
            new APItemData("Tell No Tales", 142201, 142146),
            new APItemData("Future Frenzy", 142207, 142147),
            new APItemData("Tomb Wader", 142213, 142148),

            new APItemData("Gone Tomorrow", 142220, 142149),
            new APItemData("Orange Asphalt", 142225, 142150),
            new APItemData("Flaming Passion", 142231, 142151),
            new APItemData("Mad Bombers", 142236, 142152),
            new APItemData("Bug Lite", 142242, 142153),

            new APItemData("Ski Crazed", 142247, 142154),
            new APItemData("Area 51?", 142252, 142155),
            new APItemData("Rings of Power", 142257, 142156),
            new APItemData("Hot Coco", 142261, 142157),
            new APItemData("Eggipus Rex", 142265, 142158),
        };
    }
}
