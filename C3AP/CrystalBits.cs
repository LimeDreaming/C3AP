using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C3AP_Client
{
    public static class CrystalBits
    {
        public static Dictionary<string, int> powerupData = new Dictionary<string, int>
        {
            { "Body Slam", 0 },
            { "Double Jump", 1 },
            { "Death Tornado Spin", 2 },
            { "Fruit Bazooka", 3 },
            { "Speed Shoes", 4 }
        };

        public static Dictionary<string, Tuple<int, int>> crystalData = new Dictionary<string, Tuple<int, int>>()
        {
            //Bits Part 1
            { "Orient Express", new Tuple<int, int>(0, 2) },
            { "Toad Village", new Tuple<int, int>(0, 3) },
            { "Bone Yard", new Tuple<int, int>(0, 4) },
            { "Tell No Tales", new Tuple<int, int>(0, 5) },
            { "Under Pressure", new Tuple<int, int>(0, 6) },
            { "Gee Wiz", new Tuple<int, int>(0, 7) },

            //Bits Part 2
            { "Dino Might!", new Tuple<int, int>(1, 0) },
            { "Midnight Run", new Tuple<int, int>(1, 1) },
            { "Tomb Time", new Tuple<int, int>(1, 2) },
            { "Bye Bye Blimps", new Tuple<int, int>(1, 3) },
            { "Mad Bombers", new Tuple<int, int>(1, 4) },
            { "Hog Ride", new Tuple<int, int>(1, 5) },
            { "Hang'em High", new Tuple<int, int>(1, 6) },
            { "Road Crash", new Tuple<int, int>(1, 7) },

            //Bits Part 3
            { "Tomb Wader", new Tuple<int, int>(2, 0) },
            { "Makin' Waves", new Tuple<int, int>(2, 1) },
            { "High Time", new Tuple<int, int>(2, 2) },
            { "Future Frenzy", new Tuple<int, int>(2, 3) },
            { "Deep Trouble", new Tuple<int, int>(2, 4) },
            { "Double Header", new Tuple<int, int>(2, 5) },
            { "Sphynxinator", new Tuple<int, int>(2, 6) },

            //Bits Part 4
            { "Orange Asphalt", new Tuple<int, int>(3, 0) },
            { "Flaming Passion", new Tuple<int, int>(3, 2) },
            { "Gone Tomorrow", new Tuple<int, int>(3, 3) },
            { "Bug Lite", new Tuple<int, int>(3, 4) }
        };

        public static Dictionary<string, Tuple<int, int>> cleargemBoxData = new Dictionary<string, Tuple<int, int>>()
        {
            //Bits Part 1
            { "Orient Express", new Tuple<int, int>(1, 2) },
            { "Toad Village", new Tuple<int, int>(1, 3) },
            { "Bone Yard", new Tuple<int, int>(1, 4) },
            { "Tell No Tales", new Tuple<int, int>(1, 5) },
            { "Under Pressure", new Tuple<int, int>(1, 6) },
            { "Gee Wiz", new Tuple<int, int>(1, 7) },

            //Bits Part 2
            { "Dino Might!", new Tuple<int, int>(2, 0) },
            { "Midnight Run", new Tuple<int, int>(2, 1) },
            { "Tomb Time", new Tuple<int, int>(2, 2) },
            { "Bye Bye Blimps", new Tuple<int, int>(2, 3) },
            { "Road Crash", new Tuple<int, int>(2, 4) },
            { "Hog Ride", new Tuple<int, int>(2, 5) },
            { "Hang'em High", new Tuple<int, int>(2, 6) },
            { "Mad Bombers", new Tuple<int, int>(2, 7) },

            //Bits Part 4
            { "Tomb Wader", new Tuple<int, int>(3, 0) },
            { "Makin' Waves", new Tuple<int, int>(3, 1) },
            { "High Time", new Tuple<int, int>(3, 2) },
            { "Future Frenzy", new Tuple<int, int>(3, 3) },
            { "Deep Trouble", new Tuple<int, int>(3, 4) },
            { "Double Header", new Tuple<int, int>(3, 5) },
            { "Sphynxinator", new Tuple<int, int>(3, 6) },
            { "Ring of Power", new Tuple<int, int>(3, 7) },

            //Bits Part 5
            { "Orange Asphalt", new Tuple<int, int>(4, 0) },
            { "Ski Crazed", new Tuple<int, int>(4, 1) },
            { "Flaming Passion", new Tuple<int, int>(4, 2) },
            { "Gone Tomorrow", new Tuple<int, int>(4, 3) },
            { "Bug Lite", new Tuple<int, int>(4, 4) },
            { "Area 51?", new Tuple<int, int>(4, 5) },
            { "Eggipus Rex", new Tuple<int, int>(4, 6) },
            { "Hot Coco", new Tuple<int, int>(4, 7) }
        };

        public static Dictionary<string, Tuple<int, int>> cleargemData = new Dictionary<string, Tuple<int, int>>()
        {
            //Bits Part 1
            { "Bone Yard", new Tuple<int, int>(0, 1) },
            { "Tomb Time", new Tuple<int, int>(0, 2) },
            { "Dino Might!", new Tuple<int, int>(0, 3) },
            { "Sphynxinator", new Tuple<int, int>(0, 4) },
            { "Future Frenzy", new Tuple<int, int>(0, 5) },
            { "Gone Tomorrow", new Tuple<int, int>(0, 6) },
            { "Bug Lite", new Tuple<int, int>(0, 7) },

            //Bits Part 2
            { "Area 51?", new Tuple<int, int>(1, 0) },
            { "Rings of Power", new Tuple<int, int>(1, 1) }
        };

        public static Dictionary<string, Tuple<int, int>> colorgemData = new Dictionary<string, Tuple<int, int>>()
        {
            { "Deep Trouble", new Tuple<int, int>(0, 2) }, // RedGem
            { "Flaming Passion", new Tuple<int, int>(0, 3) }, // GreenGem
            { "High Time", new Tuple<int, int>(0, 4) }, // PurpleGem
            { "Tomb Wader", new Tuple<int, int>(0, 5) }, //BlueGem
            { "Hang'em High", new Tuple<int, int>(0, 6) }, //YellowGem
            { "105% Gem", new Tuple<int, int>(0, 7) }, //105% Gem
        };

        public static Dictionary<string, Tuple<int, int>> relicSGData = new Dictionary<string, Tuple<int, int>>()
        {
            //Bits Part 1
            { "Orient Express", new Tuple<int, int>(0, 2) },
            { "Toad Village", new Tuple<int, int>(0, 3) },
            { "Bone Yard", new Tuple<int, int>(0, 4) },
            { "Tell No Tales", new Tuple<int, int>(0, 5) },
            { "Under Pressure", new Tuple<int, int>(0, 6) },
            { "Gee Wiz", new Tuple<int, int>(0, 7) },

            //Bits Part 2
            { "Dino Might!", new Tuple<int, int>(1, 0) },
            { "Midnight Run", new Tuple<int, int>(1, 1) },
            { "Tomb Time", new Tuple<int, int>(1, 2) },
            { "Bye Bye Blimps", new Tuple<int, int>(1, 3) },
            { "Mad Bombers", new Tuple<int, int>(1, 4) },//
            { "Hog Ride", new Tuple<int, int>(1, 5) },
            { "Hang'em High", new Tuple<int, int>(1, 6) },
            { "Road Crash", new Tuple<int, int>(1, 7) },//

            //Bits Part 3
            { "Tomb Wader", new Tuple<int, int>(2, 0) },
            { "Makin' Waves", new Tuple<int, int>(2, 1) },
            { "High Time", new Tuple<int, int>(2, 2) },
            { "Future Frenzy", new Tuple<int, int>(2, 3) },
            { "Deep Trouble", new Tuple<int, int>(2, 4) },
            { "Double Header", new Tuple<int, int>(2, 5) },
            { "Sphynxinator", new Tuple<int, int>(2, 6) },
            { "Ring of Power", new Tuple<int, int>(2, 7) },

            //Bits Part 4
            { "Orange Asphalt", new Tuple<int, int>(3, 0) },
            { "Ski Crazed", new Tuple<int, int>(3, 1) },
            { "Flaming Passion", new Tuple<int, int>(3, 2) },
            { "Gone Tomorrow", new Tuple<int, int>(3, 3) },
            { "Bug Lite", new Tuple<int, int>(3, 4) },
            { "Area 51?", new Tuple<int, int>(3, 0) },
            { "Eggipus Rex", new Tuple<int, int>(3, 0) },
            { "Hot Coco", new Tuple<int, int>(3, 0) }
        };
    }
}