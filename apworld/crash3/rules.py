from __future__ import annotations

from typing import TYPE_CHECKING

from BaseClasses import CollectionState
from worlds.generic.Rules import add_rule, set_rule

from . import locations

from .regions import crystal_counts

if TYPE_CHECKING:
    from .world import Crash3World

levelNameToGimmick = {
    "Turtle Woods": None,
    "Snow Go": None,
    "Hang Eight": "Jetboard",
    "The Pits": None,
    "Crash Dash": None,
    "Ripper Roo": None,
    "Snow Biz": None,
    "Air Crash": "Jetboard",
    "Bear It": "Polar",
    "Crash Crush": None,
    "The Eel Deal": None,
    "Komodo Brothers": None,
    "Plant Food": "Jetboard",
    "Sewer or Later": None,
    "Bear Down": "Polar",
    "Road to Ruin": None,
    "Un-Bearable": "Polar",
    "Tiny Tiger": None,
    "Hangin' Out": None,
    "Diggin' It": None,
    "Cold Hard Crash": None,
    "Ruination": None,
    "Bee-Having": None,
    "Dr. N. Gin": None,
    "Piston it Away": None,
    "Rock It": "Jetpack",
    "Night Fight": "Fireflies",
    "Pack Attack": "Jetpack",
    "Spaced Out": None,
    "Dr. Neo Cortex": "Jetpack",
    "Totally Bear": "Polar",
    "Totally Fly": "Fireflies",
}

def gimmick_option(world: Crash3World, gimmick: str) -> int:
    match gimmick:
        case "Jetpack":
            return int(world.options.jetpack_lock_logic)
        case "Jetboard":
            return int(world.options.jetboard_lock_logic)
        case "Polar":
            return int(world.options.polar_lock_logic)
        case "Fireflies":
            return int(world.options.firefly_lock_logic)
    return 0

def set_all_rules(world: Crash3World) -> None:
    # In order for AP to generate an item layout that is actually possible for the player to complete,
    # we need to define rules for our Entrances and Locations.
    # Note: Regions do not have rules, the Entrances connecting them do!
    # We'll do entrances first, then locations, and then finally we set our victory condition.

    set_all_entrance_rules(world)
    #set_all_location_rules(world)
    set_completion_condition(world)


def set_all_entrance_rules(world: Crash3World) -> None:

    all_crystals = [
        "Crystal: Toad Village", "Crystal: Under Pressure", "Crystal: Orient Express", "Crystal: Bone Yard",
        "Crystal: Makin' Waves",
        "Crystal: Gee Wiz", "Crystal: Hang'em High", "Crystal: Hog Ride", "Crystal: Tomb Time", "Crystal: Midnight Run",
        "Crystal: Dino Might!", "Crystal: Deep Trouble", "Crystal: High Time", "Crystal: Road Crash",
        "Crystal: Double Header",
        "Crystal: Sphynxinator", "Crystal: Bye Bye Blimps", "Crystal: Tell No Tales", "Crystal: Future Frenzy",
        "Crystal: Tomb Wader",
        "Crystal: Gone Tomorrow", "Crystal: Orange Asphalt", "Crystal: Flaming Passion", "Crystal: Mad Bombers",
        "Crystal: Bug Lite"
    ]

    # Hilfsfunktion, die zählt, wie viele dieser Kristalle der Spieler im state hat
    def has_crystals(state, count: int) -> bool:
        return sum(1 for crystal in all_crystals if state.has(crystal, world.player)) >= count

    # Boss-Tore / Warp-Room-Übergänge
    set_rule(world.get_entrance("Tiny Tiger"), lambda state: has_crystals(state, 5))
    set_rule(world.get_entrance("Tiny Tiger to Warp Room 2"),
             lambda state: has_crystals(state, 5) and state.has("Tiny Tiger Defeated", world.player))

    set_rule(world.get_entrance("Dingodile"), lambda state: has_crystals(state, 10))
    set_rule(world.get_entrance("Dingodile to Warp Room 3"),
             lambda state: has_crystals(state, 10) and state.has("Dingodile Defeated", world.player))

    set_rule(world.get_entrance("N. Tropy"), lambda state: has_crystals(state, 15))
    set_rule(world.get_entrance("N. Tropy to Warp Room 4"),
             lambda state: has_crystals(state, 15) and state.has("N. Tropy Defeated", world.player))

    set_rule(world.get_entrance("N. Gin"), lambda state: has_crystals(state, 20))
    set_rule(world.get_entrance("N. Gin to Warp Room 5"),
             lambda state: has_crystals(state, 20) and state.has("N. Gin Defeated", world.player))

    set_rule(world.get_entrance("Dr. Neo Cortex"), lambda state: has_crystals(state, 25))

    # Einheitlich auf "Warp Room 6" geändert (da das Geheimlevel in Warp Room 6 liegt)
    def has_relics(state, count: int) -> bool:
        all_relic_names = [
            # Hier alle deine Saphir-, Gold- und Platin-Relikte einfügen oder dynamisch filtern
            item for item in world.item_name_to_id.keys() if "Relic" in item
        ]
        return sum(1 for relic in all_relic_names if state.has(relic, world.player)) >= count

    # Korrigierte Regeln für die Relic-Tore:
    set_rule(world.get_entrance("Warp Room to Ski Crazed"), lambda state: has_relics(state, 5))
    set_rule(world.get_entrance("Warp Room to Hang'em High"), lambda state: has_relics(state, 10))
    set_rule(world.get_entrance("Warp Room to Area 51?"), lambda state: has_relics(state, 15))
    set_rule(world.get_entrance("Warp Room to Future Frenzy"), lambda state: has_relics(state, 20))
    set_rule(world.get_entrance("Warp Room to Rings of Power"), lambda state: has_relics(state, 25))

    # Conditions can depend on event items.
    # set_rule(right_room_to_final_boss_room, lambda state: state.has("Top Left Room Button Pressed", world.player))

    # Some entrance rules may only apply if the player enabled certain options.
    # In our case, if the hammer option is enabled, we need to add the Hammer requirement to the Entrance from
    # Overworld to the Top Middle Room.
    # if world.options.hammer:
    #     overworld_to_top_middle_room = world.get_entrance("Overworld to Top Middle Room")
    #     set_rule(overworld_to_top_middle_room, lambda state: state.has("Hammer", world.player))





def set_completion_condition(world: Crash3World) -> None:
    # # Finally, we need to set a completion condition for our world, defining what the player needs to win the game.
    # # You can just set a completion condition directly like any other condition, referencing items the player receives:
    # world.multiworld.completion_condition[world.player] = lambda state: state.has_all(("Sword", "Shield"), world.player)
    #
    # # In our case, we went for the Victory event design pattern (see create_events() in locations.py).
    # # So lets undo what we just did, and instead set the completion condition to:
    # world.multiworld.completion_condition[world.player] = lambda state: state.has("Victory", world.player)
    if world.options.goal_option == 0:
        world.multiworld.completion_condition[world.player] = lambda state: state.has("Victory", world.player)
    if world.options.goal_option == 1:
        world.multiworld.completion_condition[world.player] = lambda state: state.has("Victory100", world.player)
    if world.options.goal_option == 2:
        world.multiworld.completion_condition[world.player] = lambda state: state.has("Victory105", world.player)