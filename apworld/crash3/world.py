from collections.abc import Mapping
from collections import defaultdict
from typing import Any

# Imports of base Archipelago modules must be absolute.
from worlds.AutoWorld import World, WebWorld

# Imports of your world's files must be relative.
from . import items, locations, regions, rules
from . import options as crash3_options  # rename due to name conflict with World.options
from .items import ITEM_NAME_TO_ID

warpRoomLevelIds = [
    0x18, 0x1F, 0x19, 0x0E, 0x1E,
    0x23, 0x1B, 0x1D, 0x20, 0x11,
    0x17, 0x16, 0x22, 0x0A, 0x21,
    0x24, 0x0F, 0x13, 0x15, 0x0D,
    0x26, 0x1A, 0x0C, 0x12, 0x10,
    0x27, 0x25, 0x23, 0x20, 0x22
]

class Crash3WebWorld(WebWorld):
    game = "Crash Bandicoot: Warped"
    theme = "grass"
    option_groups = crash3_options.option_groups


def setup_groups():
    item_name_groups = {
        "Colored Gems": {"Blue Gem", "Yellow Gem", "Red Gem", "Purple Gem", "Green Gem"},
        "Filler": {"Aku Aku", "Life Bundle", "Wumpa Fruit Bundle"},
        "Traps": {"Big Crash Trap", "Small Crash Trap", "No Lives Trap", "Jetpack Controls Trap", "Loose Live Trap", "Loose Wumpa Trap"},
        "Powerups": {"Body Slam", "Double Jump", "Tornado Spin", "Bazooka", "Crash Dash"},
        "Gimmicks": {"Tiger", "Baby-T", "Jet Sub", "Jet Ski", "Motorbike", "Biplane Crash", "Biplane Coco"}
    }

    location_name_groups = {
        "Toad Village Relics": {"Toad Village: Sapphire Relic", "Toad Village: Gold Relic", "Toad Village: Platinum Relic"},
        "Under Pressure Relics": {"Under Pressure: Sapphire Relic", "Under Pressure: Gold Relic", "Under Pressure: Platinum Relic"},
        "Orient Express Relics": {"Orient Express: Sapphire Relic", "Orient Express: Gold Relic", "Orient Express: Platinum Relic"},
        "Bone Yard Relics": {"Bone Yard: Sapphire Relic", "Bone Yard: Gold Relic", "Bone Yard: Platinum Relic"},
        "Makin' Waves Relics": {"Makin' Waves: Sapphire Relic", "Makin' Waves: Gold Relic", "Makin' Waves: Platinum Relic"},
        "Gee Wiz Relics": {"Gee Wiz: Sapphire Relic", "Gee Wiz: Gold Relic", "Gee Wiz: Platinum Relic"},
        "Hang'em High Relics": {"Hang'em High: Sapphire Relic", "Hang'em High: Gold Relic", "Hang'em High: Platinum Relic"},
        "Hog Ride Relics": {"Hog Ride: Sapphire Relic", "Hog Ride: Gold Relic", "Hog Ride: Platinum Relic"},
        "Tomb Time Relics": {"Tomb Time: Sapphire Relic", "Tomb Time: Gold Relic", "Tomb Time: Platinum Relic"},
        "Midnight Run Relics": {"Midnight Run: Sapphire Relic", "Midnight Run: Gold Relic", "Midnight Run: Platinum Relic"},
        "Dino Might! Relics": {"Dino Might!: Sapphire Relic", "Dino Might!: Gold Relic", "Dino Might!: Platinum Relic"},
        "Deep Trouble Relics": {"Deep Trouble: Sapphire Relic", "Deep Trouble: Gold Relic", "Deep Trouble: Platinum Relic"},
        "High Time Relics": {"High Time: Sapphire Relic", "High Time: Gold Relic", "High Time: Platinum Relic"},
        "Road Crash Relics": {"Road Crash: Sapphire Relic", "Road Crash: Gold Relic", "Road Crash: Platinum Relic"},
        "Double Header Relics": {"Double Header: Sapphire Relic", "Double Header: Gold Relic", "Double Header: Platinum Relic"},
        "Sphynxinator Relics": {"Sphynxinator: Sapphire Relic", "Sphynxinator: Gold Relic", "Sphynxinator: Platinum Relic"},
        "Bye Bye Blimps Relics": {"Bye Bye Blimps: Sapphire Relic", "Bye Bye Blimps: Gold Relic", "Bye Bye Blimps: Platinum Relic"},
        "Tell No Tales Relics": {"Tell No Tales: Sapphire Relic", "Tell No Tales: Gold Relic", "Tell No Tales: Platinum Relic"},
        "Future Frenzy Relics": {"Future Frenzy: Sapphire Relic", "Future Frenzy: Gold Relic", "Future Frenzy: Platinum Relic"},
        "Tomb Wader Relics": {"Tomb Wader: Sapphire Relic", "Tomb Wader: Gold Relic", "Tomb Wader: Platinum Relic"},
        "Gone Tomorrow Relics": {"Gone Tomorrow: Sapphire Relic", "Gone Tomorrow: Gold Relic", "Gone Tomorrow: Platinum Relic"},
        "Orange Asphalt Relics": {"Orange Asphalt: Sapphire Relic", "Orange Asphalt: Gold Relic", "Orange Asphalt: Platinum Relic"},
        "Flaming Passion Relics": {"Flaming Passion: Sapphire Relic", "Flaming Passion: Gold Relic", "Flaming Passion: Platinum Relic"},
        "Mad Bombers Relics": {"Mad Bombers: Sapphire Relic", "Mad Bombers: Gold Relic", "Mad Bombers: Platinum Relic"},
        "Bug Lite Relics": {"Bug Lite: Sapphire Relic", "Bug Lite: Gold Relic", "Bug Lite: Platinum Relic"},
        "Ski Crazed Relics": {"Ski Crazed: Sapphire Relic", "Ski Crazed: Gold Relic", "Ski Crazed: Platinum Relic"},
        "Area 51? Relics": {"Area 51?: Sapphire Relic", "Area 51?: Gold Relic", "Area 51?: Platinum Relic"},
        "Rings of Power Relics": {"Rings of Power: Sapphire Relic", "Rings of Power: Gold Relic", "Rings of Power: Platinum Relic"},
        "Hot Coco Relics": {"Hot Coco: Sapphire Relic", "Hot Coco: Gold Relic", "Hot Coco: Platinum Relic"},
        "Eggipus Rex Relics": {"Eggipus Rex: Sapphire Relic", "Eggipus Rex: Gold Relic", "Eggipus Rex: Platinum Relic"}
    }

    # Stelle sicher, dass defaultdict genutzt wird, damit dynamische Gruppen per .add() ergänzt werden können
    loc_groups_dict = defaultdict(set, location_name_groups)

    for loc in locations.LOCATION_NAME_TO_ID.keys():
        ind = loc.find(":")
        if ind == -1:
            continue

        level_name = loc[:ind]
        loc_groups_dict[level_name].add(loc)

        if "Gem" in loc and "Clear" not in loc:
            loc_groups_dict["All Colored Gems"].add(loc)

        for group in ["Crystal", "Clear Gem", "Relic"]:
            if group in loc:
                loc_groups_dict[f"All {group}s"].add(loc)

    return item_name_groups, loc_groups_dict


class Crash3World(World):
    """
    Crash Bandicoot 3: Warped
    """

    game = "Crash Bandicoot: Warped"
    web = Crash3WebWorld()

    options_dataclass = crash3_options.Crash3Options
    options: crash3_options.Crash3Options

    # Locations und Items initialisieren
    location_name_to_id = locations.LOCATION_NAME_TO_ID
    item_name_to_id = items.ITEM_NAME_TO_ID
    item_name_groups, location_name_groups = setup_groups()

    warp_room: list[int] = warpRoomLevelIds
    secret_warp_room_entrance_ids = []

    origin_region_name = "Warp Room 1"

    def create_regions(self) -> None:
        regions.create_and_connect_regions(self)
        locations.create_all_locations(self)

    def set_rules(self) -> None:
        rules.set_all_rules(self)

    def create_items(self) -> None:
        items.create_all_items(self)
        self.item_name_groups["Crystal"] = {name for name in self.item_name_to_id if name.startswith("Crystal:")}
        self.item_name_groups["Clear Gem"] = {name for name in self.item_name_to_id if name.startswith("Clear Gem")}

    def create_item(self, name: str) -> items.Crash3Item:
        return items.create_item_with_correct_classification(self, name)

    def get_filler_item_name(self) -> str:
        return items.get_random_filler_item_name(self)

    def fill_slot_data(self) -> Mapping[str, Any]:
        return {
            "options": self.options.as_dict(
                "level_exit_locations",
                "trap_chance", "death_link", "powerup_lock", "gimmick_lock"
            ),
            "warp_room_destinations": self.warp_room,
            "secret_warp_room_entrances": self.secret_warp_room_entrance_ids,
            "seed": self.multiworld.seed_name,
            "slot": self.multiworld.player_name[self.player],
        }

    def interpret_slot_data(self, slot_data: dict[str, Any]) -> dict[str, Any]:
        return slot_data