using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static C3AP_Client.SharedAdresses;

namespace C3AP_Client
{
    internal class Gimmicks
    {
        public static Dictionary<string, long> TigerLevel = new Dictionary<string, long>
        {
            {"Orient Express", 142268},
            {"Midnight Run", 142275},
        };
        public static Dictionary<string, long> SubJetLevel = new Dictionary<string, long>
        {
            {"Under Pressure", 142267},
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
    }
}
