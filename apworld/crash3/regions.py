from __future__ import annotations

from typing import TYPE_CHECKING

from BaseClasses import Entrance, Region

from .locations import levelIdToName, levelNameToId, Crash3Location

if TYPE_CHECKING:
    from .world import Crash3World

# A region is a container for locations ("checks"), which connects to other regions via "Entrance" objects.
# Many games will model their Regions after physical in-game places, but you can also have more abstract regions.
# For a location to be in logic, its containing region must be reachable.
# The Entrances connecting regions can have rules - more on that in rules.py.
# This makes regions especially useful for traversal logic ("Can the player reach this part of the map?")

# Every location must be inside a region, and you must have at least one region.
# This is why we create regions first, and then later we create the locations (in locations.py).

crystal_counts = []

def create_and_connect_regions(world: Crash3World) -> None:
    create_all_regions(world)
    connect_regions(world)


def create_all_regions(world: Crash3World) -> None:
    regions = []

    # 1. Start-Region (Menu) mit deinen Test-Checks erstellen
    menu_region = Region("Menu", world.player, world.multiworld)
    menu_region.add_locations({
        "Test1": 142298,
        "Test2": 142299,
        "Test3": 142300,
    }, location_type=Crash3Location)
    regions.append(menu_region)

    # 2. Warp-Räume 1 bis 6 erstellen
    for i in range(6):
        regions.append(Region("Warp Room " + str(i + 1), world.player, world.multiworld))

    # 3. Alle Level & Bosse aus deinem Dictionary erstellen
    for name in levelNameToId:
        regions.append(Region(name, world.player, world.multiworld))

    regions.append(Region("Ski Crazed Hidden", world.player, world.multiworld))
    regions.append(Region("Hang'em High Hidden", world.player, world.multiworld))
    regions.append(Region("Area 51? Hidden", world.player, world.multiworld))
    regions.append(Region("Future Frenzy Hidden", world.player, world.multiworld))
    regions.append(Region("Rings of Power Hidden", world.player, world.multiworld))
    # 4. Ziel-Regionen für den Sieg erstellen
    regions.append(Region("100% Complete", world.player, world.multiworld))
    regions.append(Region("105% Complete", world.player, world.multiworld))

    # Alle Regionen auf einmal in Archipelago registrieren
    world.multiworld.regions += regions


def connect_regions(world: Crash3World) -> None:
    # 1. Verbindung vom Menu zum ersten Warp Room
    menu = world.get_region("Menu")
    warp_1 = world.get_region("Warp Room 1")
    menu.connect(warp_1, "Menu to Warp Room 1")

    # 2. Warp-Räume nacheinander verbinden (Warp Room 1 bis 5)
    for i in range(1, 5):
        world.get_region("Warp Room " + str(i)).connect(
            world.get_region("Warp Room " + str(i + 1)),
            "Warp Room " + str(i) + " to Warp Room " + str(i + 1)
        )

    # 3. Jeden Warp-Raum mit seinen 5 Leveln und seinem Boss verbinden
    boss_names = ["Tiny Tiger", "Dingodile", "N. Tropy", "N. Gin", "Dr. Neo Cortex"]

    for i in range(6):
        warp_room = world.get_region("Warp Room " + str(i + 1))

        # Die 5 regulären Level des Warp-Raums
        for j in range(5):
            level_id = world.warp_room[i * 5 + j]
            level_name = levelIdToName[level_id]
            level = world.get_region(level_name)
            warp_room.connect(level, "Warp Room " + str(i + 1) + " to " + level_name)

        # Den Boss hinzufügen (Warp Room 6 hat keinen Boss)
        if i < 5:
            boss_name = boss_names[i]
            boss_region = world.get_region(boss_name)

            # WICHTIG: Hier vergeben wir den exakten Namen "Tiny Tiger", "Dingodile" etc.
            # damit world.get_entrance("Tiny Tiger") in rules.py es findet!
            warp_room.connect(boss_region, boss_name)
            warp_room.connect(boss_region, boss_name + " to Warp Room " + str(i + 2))

        ski_crazed = world.get_region("Ski Crazed Hidden")
        warp_room.connect(ski_crazed, "Warp Room to Ski Crazed")
        hang_high = world.get_region("Hang'em High Hidden")
        warp_room.connect(hang_high, "Warp Room to Hang'em High")
        area51 = world.get_region("Area 51? Hidden")
        warp_room.connect(area51, "Warp Room to Area 51?")
        future_frenzy = world.get_region("Future Frenzy Hidden")
        warp_room.connect(future_frenzy, "Warp Room to Future Frenzy")
        rings_power = world.get_region("Rings of Power Hidden")
        warp_room.connect(rings_power, "Warp Room to Rings of Power")


    # 4. Endbedingungen mit Dr. Neo Cortex verbinden
    cortex = world.get_region("Dr. Neo Cortex")
    cortex.connect(world.get_region("100% Complete"), "Beat Cortex 100%")
    cortex.connect(world.get_region("105% Complete"), "Beat Cortex 105%")
    # Connect crystal count regions in a chain

    # You can then connect the Entrance to the target region.
    # overworld_to_bottom_right_room.connect(bottom_right_room)

    # An even easier way is to use the region.connect helper.
    # overworld.connect(right_room, "Overworld to Right Room")
    # right_room.connect(final_boss_room, "Right Room to Final Boss Room")

    # The region.connect helper even allows adding a rule immediately.
    # We'll talk more about rule creation in the set_all_rules() function in rules.py.
    # overworld.connect(top_left_room, "Overworld to Top Left Room", lambda state: state.has("Key", world.player))

    # Some Entrances may only exist if the player enables certain options.
    # In our case, the Hammer locks the top middle chest in its own room if the hammer option is enabled.
    # In this case, we previously created an extra "Top Middle Room" region that we now need to connect to Overworld.
    # if world.options.hammer:
    #     top_middle_room = world.get_region("Top Middle Room")
    #     overworld.connect(top_middle_room, "Overworld to Top Middle Room")