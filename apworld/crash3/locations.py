from __future__ import annotations

from typing import TYPE_CHECKING

from BaseClasses import ItemClassification, Location, LocationProgressType

from . import items

if TYPE_CHECKING:
    from .world import Crash3World

levelNameToId = {
    "Toad Village": 0x0b,
    "Under Pressure": 0x0e,
    "Orient Express": 0x0a,
    "Bone Yard": 0x0c,
    "Makin' Waves": 0x19,

    "Tiny Tiger": 0x06,

    "Gee Wiz": 0x0f,
    "Hang'em High": 0x16,
    "Hog Ride": 0x15,
    "Tomb Time": 0x12,
    "Midnight Run": 0x11,

    "Dingodile": 0x03,

    "Dino Might!": 0x10,
    "Deep Trouble": 0x1c,
    "High Time": 0x1a,
    "Road Crash": 0x14,
    "Double Header": 0x1d,

    "N. Tropy": 0x04,

    "Sphynxinator": 0x1e,
    "Bye Bye Blimps": 0x13,
    "Tell No Tales": 0x0d,
    "Future Frenzy": 0x1b,
    "Tomb Wader": 0x18,

    "N. Gin": 0x05,

    "Gone Tomorrow": 0x23,
    "Orange Asphalt": 0x20,
    "Flaming Passion": 0x22,
    "Mad Bombers": 0x17,
    "Bug Lite": 0x24,

    "Dr. Neo Cortex": 0x07,

    "Ski Crazed": 0x21,
    "Area 51?": 0x25,
    "Rings of Power": 0x1f,
    
    "Hot Coco": 0x27,
    "Eggipus Rex": 0x26,
}

# gets the name of the level with that level ID. array where the index is the level ID you want. level IDs cannot go above 0x3F (63).
levelIdToName = [None] * 64
# fills array based on map defined right above this!
for name in levelNameToId:
    levelIdToName[levelNameToId[name]] = name


LOCATION_NAME_TO_ID = {
    "Toad Village: Crystal": 142100,
    "Toad Village: Clear Gem (Box Gem)": 142101,
    "Toad Village: Sapphire Relic": 142102,
    "Toad Village: Gold Relic": 142103,
    "Toad Village: Platinum Relic": 142104,

    "Under Pressure: Crystal": 142105,
    "Under Pressure: Clear Gem (Box Gem)": 142106,
    "Under Pressure: Sapphire Relic": 142107,
    "Under Pressure: Gold Relic": 142108,
    "Under Pressure: Platinum Relic": 142109,

    "Orient Express: Crystal": 142110,
    "Orient Express: Clear Gem (Box Gem)": 142111,
    "Orient Express: Sapphire Relic": 142112,
    "Orient Express: Gold Relic": 142113,
    "Orient Express: Platinum Relic": 142114,

    "Bone Yard: Crystal": 142115,
    "Bone Yard: Clear Gem (Box Gem)": 142116,
    "Bone Yard: Clear Gem": 142117,
    "Bone Yard: Sapphire Relic": 142118,
    "Bone Yard: Gold Relic": 142119,
    "Bone Yard: Platinum Relic": 142120,

    "Makin' Waves: Crystal": 142121,
    "Makin' Waves: Clear Gem (Box Gem)": 142122,
    "Makin' Waves: Sapphire Relic": 142123,
    "Makin' Waves: Gold Relic": 142124,
    "Makin' Waves: Platinum Relic": 142125,

    "Tiny Tiger Defeated": 142126,

    "Gee Wiz: Crystal": 142127,
    "Gee Wiz: Clear Gem (Box Gem)": 142128,
    "Gee Wiz: Sapphire Relic": 142129,
    "Gee Wiz: Gold Relic": 142130,
    "Gee Wiz: Platinum Relic": 142131,

    "Hang'em High: Crystal": 142132,
    "Hang'em High: Clear Gem (Box Gem)": 142133,
    "Hang'em High: Yellow Gem": 142134,
    "Hang'em High: Sapphire Relic": 142135,
    "Hang'em High: Gold Relic": 142136,
    "Hang'em High: Platinum Relic": 142137,

    "Hog Ride: Crystal": 142138,
    "Hog Ride: Clear Gem (Box Gem)": 142139,
    "Hog Ride: Sapphire Relic": 142140,
    "Hog Ride: Gold Relic": 142141,
    "Hog Ride: Platinum Relic": 142142,

    "Tomb Time: Crystal": 142143,
    "Tomb Time: Clear Gem (Box Gem)": 142144,
    "Tomb Time: Clear Gem": 142145,
    "Tomb Time: Sapphire Relic": 142146,
    "Tomb Time: Gold Relic": 142147,
    "Tomb Time: Platinum Relic": 142148,

    "Midnight Run: Crystal": 142149,
    "Midnight Run: Clear Gem (Box Gem)": 142150,
    "Midnight Run: Sapphire Relic": 142151,
    "Midnight Run: Gold Relic": 142152,
    "Midnight Run: Platinum Relic": 142153,

    "Dingodile Defeated": 142154,

    "Dino Might!: Crystal": 142155,
    "Dino Might!: Clear Gem (Box Gem)": 142156,
    "Dino Might!: Clear Gem": 142157,
    "Dino Might!: Sapphire Relic": 142158,
    "Dino Might!: Gold Relic": 142159,
    "Dino Might!: Platinum Relic": 142160,

    "Deep Trouble: Crystal": 142161,
    "Deep Trouble: Clear Gem (Box Gem)": 142162,
    "Deep Trouble: Red Gem": 142163,
    "Deep Trouble: Sapphire Relic": 142164,
    "Deep Trouble: Gold Relic": 142165,
    "Deep Trouble: Platinum Relic": 142166,

    "High Time: Crystal": 142167,
    "High Time: Clear Gem (Box Gem)": 142168,
    "High Time: Purple Gem": 142169,
    "High Time: Sapphire Relic": 142170,
    "High Time: Gold Relic": 142171,
    "High Time: Platinum Relic": 142172,

    "Road Crash: Crystal": 142173,
    "Road Crash: Clear Gem (Box Gem)": 142174,
    #"Road Crash: Clear Gem": 142175,
    "Road Crash: Sapphire Relic": 142176,
    "Road Crash: Gold Relic": 142177,
    "Road Crash: Platinum Relic": 142178,

    "Double Header: Crystal": 142179,
    "Double Header: Clear Gem (Box Gem)": 142180,
    "Double Header: Sapphire Relic": 142181,
    "Double Header: Gold Relic": 142182,
    "Double Header: Platinum Relic": 142183,

    "N. Tropy Defeated": 142184,

    "Sphynxinator: Crystal": 142185,
    "Sphynxinator: Clear Gem (Box Gem)": 142186,
    "Sphynxinator: Clear Gem": 142187,
    "Sphynxinator: Sapphire Relic": 142188,
    "Sphynxinator: Gold Relic": 142189,
    "Sphynxinator: Platinum Relic": 142190,

    "Bye Bye Blimps: Crystal": 142191,
    "Bye Bye Blimps: Clear Gem (Box Gem)": 142192,
    "Bye Bye Blimps: Sapphire Relic": 142193,
    "Bye Bye Blimps: Gold Relic": 142194,
    "Bye Bye Blimps: Platinum Relic": 142195,

    "Tell No Tales: Crystal": 142196,
    "Tell No Tales: Clear Gem (Box Gem)": 142197,
    #"Tell No Tales: Purple Gem": 142198,
    "Tell No Tales: Sapphire Relic": 142199,
    "Tell No Tales: Gold Relic": 142200,
    "Tell No Tales: Platinum Relic": 142201,

    "Future Frenzy: Crystal": 142202,
    "Future Frenzy: Clear Gem (Box Gem)": 142203,
    "Future Frenzy: Clear Gem": 142204,
    "Future Frenzy: Sapphire Relic": 142205,
    "Future Frenzy: Gold Relic": 142206,
    "Future Frenzy: Platinum Relic": 142207,

    "Tomb Wader: Crystal": 142208,
    "Tomb Wader: Clear Gem (Box Gem)": 142209,
    "Tomb Wader: Blue Gem": 142210,
    "Tomb Wader: Sapphire Relic": 142211,
    "Tomb Wader: Gold Relic": 142212,
    "Tomb Wader: Platinum Relic": 142213,

    "N. Gin Defeated": 142214,

    "Gone Tomorrow: Crystal": 142215,
    "Gone Tomorrow: Clear Gem (Box Gem)": 142216,
    "Gone Tomorrow: Clear Gem": 142217,
    "Gone Tomorrow: Sapphire Relic": 142218,
    "Gone Tomorrow: Gold Relic": 142219,
    "Gone Tomorrow: Platinum Relic": 142220,

    "Orange Asphalt: Crystal": 142221,
    "Orange Asphalt: Clear Gem (Box Gem)": 142222,
    "Orange Asphalt: Sapphire Relic": 142223,
    "Orange Asphalt: Gold Relic": 142224,
    "Orange Asphalt: Platinum Relic": 142225,

    "Flaming Passion: Crystal": 142226,
    "Flaming Passion: Clear Gem (Box Gem)": 142227,
    "Flaming Passion: Green Gem": 142228,
    "Flaming Passion: Sapphire Relic": 142229,
    "Flaming Passion: Gold Relic": 142230,
    "Flaming Passion: Platinum Relic": 142231,

    "Mad Bombers: Crystal": 142232,
    "Mad Bombers: Clear Gem (Box Gem)": 142233,
    "Mad Bombers: Sapphire Relic": 142234,
    "Mad Bombers: Gold Relic": 142235,
    "Mad Bombers: Platinum Relic": 142236,

    "Bug Lite: Crystal": 142237,
    "Bug Lite: Clear Gem (Box Gem)": 142238,
    "Bug Lite: Clear Gem": 142239,
    "Bug Lite: Sapphire Relic": 142240,
    "Bug Lite: Gold Relic": 142241,
    "Bug Lite: Platinum Relic": 142242,

    # Extra Levels + Secret Levels
    "Ski Crazed: Clear Gem (Box Gem)": 142244,
    "Ski Crazed: Sapphire Relic": 142245,
    "Ski Crazed: Gold Relic": 142246,
    "Ski Crazed: Platinum Relic": 142247,

    "Area 51?: Clear Gem (Box Gem)": 142248,
    "Area 51?: Clear Gem": 142249,
    "Area 51?: Sapphire Relic": 142250,
    "Area 51?: Gold Relic": 142251,
    "Area 51?: Platinum Relic": 142252,

    "Rings of Power: Clear Gem (Box Gem)": 142253,
    "Rings of Power: Clear Gem": 142254,
    "Rings of Power: Sapphire Relic": 142255,
    "Rings of Power: Gold Relic": 142256,
    "Rings of Power: Platinum Relic": 142257,

    "Hot Coco: Clear Gem (Box Gem)": 142258,
    "Hot Coco: Sapphire Relic": 142259,
    "Hot Coco: Gold Relic": 142260,
    "Hot Coco: Platinum Relic": 142261,

    "Eggipus Rex: Clear Gem (Box Gem)": 142262,
    "Eggipus Rex: Sapphire Relic": 142263,
    "Eggipus Rex: Gold Relic": 142264,
    "Eggipus Rex: Platinum Relic": 142265,

    # Area 1
    "Toad Village: Regular Exit": 142266,
    "Under Pressure: Regular Exit": 142267,
    "Orient Express: Regular Exit": 142268,
    "Bone Yard: Regular Exit": 142269,
    "Makin' Waves: Regular Exit": 142270,

    # Area 2
    "Gee Wiz: Regular Exit": 142271,
    "Hang'em High: Regular Exit": 142272,
    "Hog Ride: Regular Exit": 142273,
    "Tomb Time: Regular Exit": 142274,
    "Midnight Run: Regular Exit": 142275,

    # Area 3
    "Dino Might!: Regular Exit": 142276,
    "Deep Trouble: Regular Exit": 142277,
    "High Time: Regular Exit": 142278,
    "Road Crash: Regular Exit": 142279,
    "Double Header: Regular Exit": 142280,

    # Area 4
    "Sphynxinator: Regular Exit": 142281,
    "Bye Bye Blimps: Regular Exit": 142282,
    "Tell No Tales: Regular Exit": 142283,
    "Future Frenzy: Regular Exit": 142284,
    "Tomb Wader: Regular Exit": 142285,

    # Area 5
    "Gone Tomorrow: Regular Exit": 142286,
    "Orange Asphalt: Regular Exit": 142287,
    "Flaming Passion: Regular Exit": 142288,
    "Mad Bombers: Regular Exit": 142289,
    "Bug Lite: Regular Exit": 142290,

    # Area 6
    "Ski Crazed: Regular Exit": 142291,
    "Area 51?: Regular Exit": 142293,
    "Rings of Power: Regular Exit": 142295,

    # Secret Levels
    "Hot Coco: Secret Exit": 142296,
    "Eggipus Rex: Secret Exit": 142297,
    "Test1": 142298,
    "Test2": 142299,
    "Test3": 142300,
}

# for easier lookup during location group setup
warp_1 = dict.fromkeys(["Toad Village", "Under Pressure", "Orient Express", "Bone Yard", "Makin' Waves"], "Warp 1")
warp_2 = dict.fromkeys(["Gee Wiz", "Hang'em High", "Hog Ride", "Tomb Time", "Midnight Run"], "Warp 2")
warp_3 = dict.fromkeys(["Dino Might!", "Deep Trouble", "High Time", "Road Crash", "Double Header"], "Warp 3")
warp_4 = dict.fromkeys(["Sphynxinator", "Bye Bye Blimps", "Tell No Tales", "Future Frenzy", "Tomb Wader"], "Warp 4")
warp_5 = dict.fromkeys(["Gone Tomorrow", "Orange Asphalt", "Flaming Passion", "Mad Bombers", "Bug Lite"], "Warp 5")
warp_6 = dict.fromkeys(["Ski Crazed", "Area 51?", "Rings of Power"], "Warp 6")
warp_s = dict.fromkeys(["Hot Coco", "Eggipus Rex"], "Secret Levels")
level_lookup = {**warp_1, **warp_2, **warp_3, **warp_4, **warp_5, **warp_6, **warp_s}


class Crash3Location(Location):
    game = "Crash Bandicoot 3: Warped"


def get_location_names_with_ids(location_names: list[str]) -> dict[str, int | None]:
    return {location_name: LOCATION_NAME_TO_ID[location_name] for location_name in location_names}


def create_all_locations(world: Crash3World) -> None:
    create_regular_locations(world)
    create_events(world)


def create_regular_locations(world: Crash3World) -> None:
    locations = LOCATION_NAME_TO_ID.keys()
    for name in levelNameToId:
        region = world.get_region(name)
        for location in locations:
            if name in location:
                if not world.options.level_exit_locations and "Regular Exit" in location:
                    continue
                
                new_location = Crash3Location(
                    world.player, 
                    location, 
                    world.location_name_to_id[location], 
                    region
                )
                region.locations.append(new_location)


def create_events(world: Crash3World) -> None:
    world.get_region("Dr. Neo Cortex").add_event(
        "Dr. Neo Cortex Defeated", "Victory", location_type=Crash3Location, item_type=items.Crash3Item
    )

    world.get_region("100% Complete").add_event(
        "Game Completed", "Victory100", location_type=Crash3Location, item_type=items.Crash3Item
    )

    world.get_region("105% Complete").add_event(
        "Game Over Completed", "Victory105", location_type=Crash3Location, item_type=items.Crash3Item
    )