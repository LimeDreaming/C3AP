from __future__ import annotations

from typing import TYPE_CHECKING

from BaseClasses import Item, ItemClassification
from Options import OptionError

import random

from .locations import levelNameToId

if TYPE_CHECKING:
    from .world import Crash3World


# Every item must have a unique integer ID associated with it.
# We will have a lookup from item name to ID here that, in world.py, we will import and bind to the world class.
# Even if an item doesn't exist on specific options, it must be present in this lookup.
ITEM_NAME_TO_ID = {
    "Crystal: Toad Village": 142000,
    "Crystal: Under Pressure": 142001,
    "Crystal: Orient Express": 142002,
    "Crystal: Bone Yard": 142003,
    "Crystal: Makin' Waves": 142004,
    "Crystal: Gee Wiz": 142005,
    "Crystal: Hang'em High": 142006,
    "Crystal: Hog Ride": 142007,
    "Crystal: Tomb Time": 142008,
    "Crystal: Midnight Run": 142009,
    "Crystal: Dino Might!": 142010,
    "Crystal: Deep Trouble": 142011,
    "Crystal: High Time": 142012,
    "Crystal: Road Crash": 142013,
    "Crystal: Double Header": 142014,
    "Crystal: Sphynxinator": 142015,
    "Crystal: Bye Bye Blimps": 142016,
    "Crystal: Tell No Tales": 142017,
    "Crystal: Future Frenzy": 142018,
    "Crystal: Tomb Wader": 142019,
    "Crystal: Gone Tomorrow": 142020,
    "Crystal: Orange Asphalt": 142021,
    "Crystal: Flaming Passion": 142022,
    "Crystal: Mad Bombers": 142023,
    "Crystal: Bug Lite": 142024,
    "Clear Gem (Box): Toad Village": 142025,
    "Clear Gem (Box): Under Pressure": 142026,
    "Clear Gem (Box): Orient Express": 142027,
    "Clear Gem (Box): Bone Yard": 142028,
    "Clear Gem: Bone Yard": 142029,
    "Clear Gem (Box): Makin' Waves": 142030,
    "Clear Gem (Box): Gee Wiz": 142031,
    "Clear Gem (Box): Hang'em High": 142032,
    "Clear Gem (Box): Hog Ride": 142033,
    "Clear Gem (Box): Tomb Time": 142034,
    "Clear Gem: Tomb Time": 142035,
    "Clear Gem (Box): Midnight Run": 142036,
    "Clear Gem (Box): Dino Might!": 142037,
    "Clear Gem: Dino Might!": 142038,
    "Clear Gem (Box): Deep Trouble": 142039,
    "Clear Gem (Box): High Time": 142040,
    "Clear Gem (Box): Road Crash": 142041,
    "Clear Gem (Box): Double Header": 142042,
    "Clear Gem (Box): Sphynxinator": 142043,
    "Clear Gem: Sphynxinator": 142044,
    "Clear Gem (Box): Bye Bye Blimps": 142045,
    "Clear Gem (Box): Tell No Tales": 142046,
    "Clear Gem (Box): Future Frenzy": 142047,
    "Clear Gem: Future Frenzy": 142048,
    "Clear Gem (Box): Tomb Wader": 142049,
    "Clear Gem (Box): Gone Tomorrow": 142050,
    "Clear Gem: Gone Tomorrow": 142051,
    "Clear Gem (Box): Orange Asphalt": 142052,
    "Clear Gem (Box): Flaming Passion": 142053,
    "Clear Gem (Box): Mad Bombers": 142054,
    "Clear Gem (Box): Bug Lite": 142055,
    "Clear Gem: Bug Lite": 142056,
    "Clear Gem (Box): Ski Crazed": 142057,
    "Clear Gem (Box): Area 51?": 142058,
    "Clear Gem: Area 51?": 142059,
    "Clear Gem (Box): Rings of Power": 142060,
    "Clear Gem: Rings of Power": 142061,
    "Clear Gem (Box): Hot Coco": 142062,
    "Clear Gem (Box): Eggipus Rex": 142063,
    "Blue Gem: Tomb Wader": 142064,
    "Red Gem: Deep Trouble": 142065,
    "Green Gem: Flaming Passion": 142066,
    "Yellow Gem: Hang'em High": 142067,
    "Purple Gem: High Time": 142068,
    "Sapphire Relic: Toad Village" : 142069,
    "Sapphire Relic: Under Pressure" : 142070,
    "Sapphire Relic: Orient Express" : 142071,
    "Sapphire Relic: Bone Yard" : 142072,
    "Sapphire Relic: Makin' Waves" : 142073,
    "Sapphire Relic: Gee Wiz" : 142074,
    "Sapphire Relic: Hang'em High" : 142075,
    "Sapphire Relic: Hog Ride" : 142076,
    "Sapphire Relic: Tomb Time" : 142077,
    "Sapphire Relic: Midnight Run" : 142078,
    "Sapphire Relic: Dino Might!" : 142079,
    "Sapphire Relic: Deep Trouble" : 142080,
    "Sapphire Relic: High Time" : 142081,
    "Sapphire Relic: Road Crash" : 142082,
    "Sapphire Relic: Double Header" : 142083,
    "Sapphire Relic: Sphynxinator" : 142084,
    "Sapphire Relic: Bye Bye Blimps" : 142085,
    "Sapphire Relic: Tell No Tales" : 142086,
    "Sapphire Relic: Future Frenzy" : 142087,
    "Sapphire Relic: Tomb Wader" : 142088,
    "Sapphire Relic: Gone Tomorrow" : 142089,
    "Sapphire Relic: Orange Asphalt" : 142090,
    "Sapphire Relic: Flaming Passion" : 142091,
    "Sapphire Relic: Mad Bombers" : 142092,
    "Sapphire Relic: Bug Lite" : 142093,
    "Sapphire Relic: Ski Crazed" : 142094,
    "Sapphire Relic: Area 51?" : 142095,
    "Sapphire Relic: Rings of Power" : 142096,
    "Sapphire Relic: Hot Coco" : 142097,
    "Sapphire Relic: Eggipus Rex" : 142098,
    "Gold Relic: Toad Village" : 142099,
    "Gold Relic: Under Pressure" : 142100,
    "Gold Relic: Orient Express" : 142101,
    "Gold Relic: Bone Yard" : 142102,
    "Gold Relic: Makin' Waves" : 142103,
    "Gold Relic: Gee Wiz" : 142104,
    "Gold Relic: Hang'em High" : 142105,
    "Gold Relic: Hog Ride" : 142106,
    "Gold Relic: Tomb Time" : 142107,
    "Gold Relic: Midnight Run" : 142108,
    "Gold Relic: Dino Might!" : 142109,
    "Gold Relic: Deep Trouble" : 142110,
    "Gold Relic: High Time" : 142111,
    "Gold Relic: Road Crash" : 142112,
    "Gold Relic: Double Header" : 142113,
    "Gold Relic: Sphynxinator" : 142114,
    "Gold Relic: Bye Bye Blimps" : 142115,
    "Gold Relic: Tell No Tales" : 142116,
    "Gold Relic: Future Frenzy" : 142117,
    "Gold Relic: Tomb Wader" : 142118,
    "Gold Relic: Gone Tomorrow" : 142119,
    "Gold Relic: Orange Asphalt" : 142120,
    "Gold Relic: Flaming Passion" : 142121,
    "Gold Relic: Mad Bombers" : 142122,
    "Gold Relic: Bug Lite" : 142123,
    "Gold Relic: Ski Crazed" : 142124,
    "Gold Relic: Area 51?" : 142125,
    "Gold Relic: Rings of Power" : 142126,
    "Gold Relic: Hot Coco" : 142127,
    "Gold Relic: Eggipus Rex" : 142128,
    "Platinum Relic: Toad Village" : 142129,
    "Platinum Relic: Under Pressure" : 142130,
    "Platinum Relic: Orient Express" : 142131,
    "Platinum Relic: Bone Yard" : 142132,
    "Platinum Relic: Makin' Waves" : 142133,
    "Platinum Relic: Gee Wiz" : 142134,
    "Platinum Relic: Hang'em High" : 142135,
    "Platinum Relic: Hog Ride" : 142136,
    "Platinum Relic: Tomb Time" : 142137,
    "Platinum Relic: Midnight Run" : 142138,
    "Platinum Relic: Dino Might!" : 142139,
    "Platinum Relic: Deep Trouble" : 142140,
    "Platinum Relic: High Time" : 142141,
    "Platinum Relic: Road Crash" : 142142,
    "Platinum Relic: Double Header" : 142143,
    "Platinum Relic: Sphynxinator" : 142144,
    "Platinum Relic: Bye Bye Blimps" : 142145,
    "Platinum Relic: Tell No Tales" : 142146,
    "Platinum Relic: Future Frenzy" : 142147,
    "Platinum Relic: Tomb Wader" : 142148,
    "Platinum Relic: Gone Tomorrow" : 142149,
    "Platinum Relic: Orange Asphalt" : 142150,
    "Platinum Relic: Flaming Passion" : 142151,
    "Platinum Relic: Mad Bombers" : 142152,
    "Platinum Relic: Bug Lite" : 142153,
    "Platinum Relic: Ski Crazed" : 142154,
    "Platinum Relic: Area 51?" : 142155,
    "Platinum Relic: Rings of Power" : 142156,
    "Platinum Relic: Hot Coco" : 142157,
    "Platinum Relic: Eggipus Rex" : 142158,
    #"Aku Aku" : 142159,
    "Life Bundle" : 142160,
    "Wumpa Fruit Bundle" : 142161,
    "Loose Live Trap" : 142162,
    "Loose Wumpa Trap" : 142163,
    "Big Crash Trap" : 142164,
    "Small Crash Trap" : 142165,
    "No Lives Trap" : 142166,
    "Jetpack Controls Trap" : 142167,
    "Body Slam" : 142168,
    "Double Jump" : 142169,
    "Tornado Spin" : 142170,
    "Bazooka" : 142171,
    "Crash Dash" : 142172,
    "Tiger" : 142173,
    "Baby-T": 142174,
    "Jet Sub": 142175,
    "Jet Ski" : 142176,
    "Motorbike" : 142177,
    "Biplane Crash" : 142178,
    "Biplane Coco" : 142179,
    "Firefly": 142180,
    #"Clear Gem: 105%": 142181,
}

# Items should have a defined default classification.
# In our case, we will make a dictionary from item name to classification.
DEFAULT_ITEM_CLASSIFICATIONS = {
    "Crystal: Toad Village": ItemClassification.progression,
    "Crystal: Under Pressure": ItemClassification.progression,
    "Crystal: Orient Express": ItemClassification.progression,
    "Crystal: Bone Yard": ItemClassification.progression,
    "Crystal: Makin' Waves": ItemClassification.progression,
    "Crystal: Gee Wiz": ItemClassification.progression,
    "Crystal: Hang'em High": ItemClassification.progression,
    "Crystal: Hog Ride": ItemClassification.progression,
    "Crystal: Tomb Time": ItemClassification.progression,
    "Crystal: Midnight Run": ItemClassification.progression,
    "Crystal: Dino Might!": ItemClassification.progression,
    "Crystal: Deep Trouble": ItemClassification.progression,
    "Crystal: High Time": ItemClassification.progression,
    "Crystal: Road Crash": ItemClassification.progression,
    "Crystal: Double Header": ItemClassification.progression,
    "Crystal: Sphynxinator": ItemClassification.progression,
    "Crystal: Bye Bye Blimps": ItemClassification.progression,
    "Crystal: Tell No Tales": ItemClassification.progression,
    "Crystal: Future Frenzy": ItemClassification.progression,
    "Crystal: Tomb Wader": ItemClassification.progression,
    "Crystal: Gone Tomorrow": ItemClassification.progression,
    "Crystal: Orange Asphalt": ItemClassification.progression,
    "Crystal: Flaming Passion": ItemClassification.progression,
    "Crystal: Mad Bombers": ItemClassification.progression,
    "Crystal: Bug Lite": ItemClassification.progression,
    "Clear Gem (Box): Toad Village": ItemClassification.progression,
    "Clear Gem (Box): Under Pressure": ItemClassification.progression,
    "Clear Gem (Box): Orient Express": ItemClassification.progression,
    "Clear Gem (Box): Bone Yard": ItemClassification.progression,
    "Clear Gem: Bone Yard": ItemClassification.progression,
    "Clear Gem (Box): Makin' Waves": ItemClassification.progression,
    "Clear Gem (Box): Gee Wiz": ItemClassification.progression,
    "Clear Gem (Box): Hang'em High": ItemClassification.progression,
    "Clear Gem (Box): Hog Ride": ItemClassification.progression,
    "Clear Gem (Box): Tomb Time": ItemClassification.progression,
    "Clear Gem: Tomb Time": ItemClassification.progression,
    "Clear Gem (Box): Midnight Run": ItemClassification.progression,
    "Clear Gem (Box): Dino Might!": ItemClassification.progression,
    "Clear Gem: Dino Might!": ItemClassification.progression,
    "Clear Gem (Box): Deep Trouble": ItemClassification.progression,
    "Clear Gem (Box): High Time": ItemClassification.progression,
    "Clear Gem (Box): Road Crash": ItemClassification.progression,
    "Clear Gem (Box): Double Header": ItemClassification.progression,
    "Clear Gem (Box): Sphynxinator": ItemClassification.progression,
    "Clear Gem: Sphynxinator": ItemClassification.progression,
    "Clear Gem (Box): Bye Bye Blimps": ItemClassification.progression,
    "Clear Gem (Box): Tell No Tales": ItemClassification.progression,
    "Clear Gem (Box): Future Frenzy": ItemClassification.progression,
    "Clear Gem: Future Frenzy": ItemClassification.progression,
    "Clear Gem (Box): Tomb Wader": ItemClassification.progression,
    "Clear Gem (Box): Gone Tomorrow": ItemClassification.progression,
    "Clear Gem: Gone Tomorrow": ItemClassification.progression,
    "Clear Gem (Box): Orange Asphalt": ItemClassification.progression,
    "Clear Gem (Box): Flaming Passion": ItemClassification.progression,
    "Clear Gem (Box): Mad Bombers": ItemClassification.progression,
    "Clear Gem (Box): Bug Lite": ItemClassification.progression,
    "Clear Gem: Bug Lite": ItemClassification.progression,
    "Clear Gem (Box): Ski Crazed": ItemClassification.progression,
    "Clear Gem (Box): Area 51?": ItemClassification.progression,
    "Clear Gem: Area 51?": ItemClassification.progression,
    "Clear Gem (Box): Rings of Power": ItemClassification.progression,
    "Clear Gem: Rings of Power": ItemClassification.progression,
    "Clear Gem (Box): Hot Coco": ItemClassification.progression,
    "Clear Gem (Box): Eggipus Rex": ItemClassification.progression,
    "Blue Gem: Tomb Wader": ItemClassification.progression,
    "Red Gem: Deep Trouble": ItemClassification.progression,
    "Green Gem: Flaming Passion": ItemClassification.progression,
    "Yellow Gem: Hang'em High": ItemClassification.progression,
    "Purple Gem: High Time": ItemClassification.progression,
    "Sapphire Relic: Toad Village" : ItemClassification.progression,
    "Sapphire Relic: Under Pressure" : ItemClassification.progression,
    "Sapphire Relic: Orient Express" : ItemClassification.progression,
    "Sapphire Relic: Bone Yard" : ItemClassification.progression,
    "Sapphire Relic: Makin' Waves" : ItemClassification.progression,
    "Sapphire Relic: Gee Wiz" : ItemClassification.progression,
    "Sapphire Relic: Hang'em High" : ItemClassification.progression,
    "Sapphire Relic: Hog Ride" : ItemClassification.progression,
    "Sapphire Relic: Tomb Time" : ItemClassification.progression,
    "Sapphire Relic: Midnight Run" : ItemClassification.progression,
    "Sapphire Relic: Dino Might!" : ItemClassification.progression,
    "Sapphire Relic: Deep Trouble" : ItemClassification.progression,
    "Sapphire Relic: High Time" : ItemClassification.progression,
    "Sapphire Relic: Road Crash" : ItemClassification.progression,
    "Sapphire Relic: Double Header" : ItemClassification.progression,
    "Sapphire Relic: Sphynxinator" : ItemClassification.progression,
    "Sapphire Relic: Bye Bye Blimps" : ItemClassification.progression,
    "Sapphire Relic: Tell No Tales" : ItemClassification.progression,
    "Sapphire Relic: Future Frenzy" : ItemClassification.progression,
    "Sapphire Relic: Tomb Wader" : ItemClassification.progression,
    "Sapphire Relic: Gone Tomorrow" : ItemClassification.progression,
    "Sapphire Relic: Orange Asphalt" : ItemClassification.progression,
    "Sapphire Relic: Flaming Passion" : ItemClassification.progression,
    "Sapphire Relic: Mad Bombers" : ItemClassification.progression,
    "Sapphire Relic: Bug Lite" : ItemClassification.progression,
    "Sapphire Relic: Ski Crazed" : ItemClassification.progression,
    "Sapphire Relic: Area 51?" : ItemClassification.progression,
    "Sapphire Relic: Rings of Power" : ItemClassification.progression,
    "Sapphire Relic: Hot Coco" : ItemClassification.progression,
    "Sapphire Relic: Eggipus Rex" : ItemClassification.progression,
    "Gold Relic: Toad Village" : ItemClassification.useful,
    "Gold Relic: Under Pressure" : ItemClassification.useful,
    "Gold Relic: Orient Express" : ItemClassification.useful,
    "Gold Relic: Bone Yard" : ItemClassification.useful,
    "Gold Relic: Makin' Waves" : ItemClassification.useful,
    "Gold Relic: Gee Wiz" : ItemClassification.useful,
    "Gold Relic: Hang'em High" : ItemClassification.useful,
    "Gold Relic: Hog Ride" : ItemClassification.useful,
    "Gold Relic: Tomb Time" : ItemClassification.useful,
    "Gold Relic: Midnight Run" : ItemClassification.useful,
    "Gold Relic: Dino Might!" : ItemClassification.useful,
    "Gold Relic: Deep Trouble" : ItemClassification.useful,
    "Gold Relic: High Time" : ItemClassification.useful,
    "Gold Relic: Road Crash" : ItemClassification.useful,
    "Gold Relic: Double Header" : ItemClassification.useful,
    "Gold Relic: Sphynxinator" : ItemClassification.useful,
    "Gold Relic: Bye Bye Blimps" : ItemClassification.useful,
    "Gold Relic: Tell No Tales" : ItemClassification.useful,
    "Gold Relic: Future Frenzy" : ItemClassification.useful,
    "Gold Relic: Tomb Wader" : ItemClassification.useful,
    "Gold Relic: Gone Tomorrow" : ItemClassification.useful,
    "Gold Relic: Orange Asphalt" : ItemClassification.useful,
    "Gold Relic: Flaming Passion" : ItemClassification.useful,
    "Gold Relic: Mad Bombers" : ItemClassification.useful,
    "Gold Relic: Bug Lite" : ItemClassification.useful,
    "Gold Relic: Ski Crazed" : ItemClassification.useful,
    "Gold Relic: Area 51?" : ItemClassification.useful,
    "Gold Relic: Rings of Power" : ItemClassification.useful,
    "Gold Relic: Hot Coco" : ItemClassification.useful,
    "Gold Relic: Eggipus Rex" : ItemClassification.useful,
    "Platinum Relic: Toad Village" : ItemClassification.useful,
    "Platinum Relic: Under Pressure" : ItemClassification.useful,
    "Platinum Relic: Orient Express" : ItemClassification.useful,
    "Platinum Relic: Bone Yard" : ItemClassification.useful,
    "Platinum Relic: Makin' Waves" : ItemClassification.useful,
    "Platinum Relic: Gee Wiz" : ItemClassification.useful,
    "Platinum Relic: Hang'em High" : ItemClassification.useful,
    "Platinum Relic: Hog Ride" : ItemClassification.useful,
    "Platinum Relic: Tomb Time" : ItemClassification.useful,
    "Platinum Relic: Midnight Run" : ItemClassification.useful,
    "Platinum Relic: Dino Might!" : ItemClassification.useful,
    "Platinum Relic: Deep Trouble" : ItemClassification.useful,
    "Platinum Relic: High Time" : ItemClassification.useful,
    "Platinum Relic: Road Crash" : ItemClassification.useful,
    "Platinum Relic: Double Header" : ItemClassification.useful,
    "Platinum Relic: Sphynxinator" : ItemClassification.useful,
    "Platinum Relic: Bye Bye Blimps" : ItemClassification.useful,
    "Platinum Relic: Tell No Tales" : ItemClassification.useful,
    "Platinum Relic: Future Frenzy" : ItemClassification.useful,
    "Platinum Relic: Tomb Wader" : ItemClassification.useful,
    "Platinum Relic: Gone Tomorrow" : ItemClassification.useful,
    "Platinum Relic: Orange Asphalt" : ItemClassification.useful,
    "Platinum Relic: Flaming Passion" : ItemClassification.useful,
    "Platinum Relic: Mad Bombers" : ItemClassification.useful,
    "Platinum Relic: Bug Lite" : ItemClassification.useful,
    "Platinum Relic: Ski Crazed" : ItemClassification.useful,
    "Platinum Relic: Area 51?" : ItemClassification.useful,
    "Platinum Relic: Rings of Power" : ItemClassification.useful,
    "Platinum Relic: Hot Coco" : ItemClassification.useful,
    "Platinum Relic: Eggipus Rex" : ItemClassification.useful,
    #"Aku Aku" : ItemClassification.filler,
    "Life Bundle" : ItemClassification.filler,
    "Wumpa Fruit Bundle" : ItemClassification.filler,
    "Loose Live Trap" : ItemClassification.trap,
    "Loose Wumpa Trap" : ItemClassification.trap,
    "Big Crash Trap" : ItemClassification.trap,
    "Small Crash Trap" : ItemClassification.trap,
    "No Lives Trap" : ItemClassification.trap,
    #"Jetpack Controls Trap" : ItemClassification.trap,
    "Body Slam" : ItemClassification.useful,
    "Double Jump" : ItemClassification.progression,
    "Tornado Spin" : ItemClassification.useful,
    "Bazooka" : ItemClassification.useful,
    "Crash Dash" : ItemClassification.useful,
    "Tiger" : ItemClassification.progression | ItemClassification.useful,
    "Baby-T" : ItemClassification.progression | ItemClassification.useful,
    "Jet Sub" : ItemClassification.progression | ItemClassification.useful,
    "Jet Ski" : ItemClassification.progression | ItemClassification.useful,
    "Motorbike" : ItemClassification.progression | ItemClassification.useful,
    "Biplane Crash" : ItemClassification.progression | ItemClassification.useful,
    "Biplane Coco" : ItemClassification.progression | ItemClassification.useful,
    "Firefly" : ItemClassification.progression | ItemClassification.useful,
    #"Clear Gem: 105%" : ItemClassification.progression | ItemClassification.useful,
}


# Each Item instance must correctly report the "game" it belongs to.
# To make this simple, it is common practice to subclass the basic Item class and override the "game" field.
class Crash3Item(Item):
    game = "Crash Bandicoot 3: Warped"

def receive_wumpa_bundle(state, player):
    # Generiere eine zufällige Anzahl an Wumpas zwischen 5 und 10
    wumpa_amount = random.randint(5, 10)

    # Hier übergibst du den Wert an das Spiel/den Speicher
    # Z. B. spieler_wumpas[player] += wumpa_amount

    #print(f"Player got {wumpa_amount} Wumpa-Fruits!")

def receive_live_bundle(state, player):
    # Generiere eine zufällige Anzahl an Wumpas zwischen 5 und 10
    live_amount = random.randint(1, 5)

    # Hier übergibst du den Wert an das Spiel/den Speicher
    # Z. B. spieler_wumpas[player] += wumpa_amount

    #print(f"Player got {live_amount} Lives!")

# Ontop of our regular itempool, our world must be able to create arbitrary amounts of filler as requested by core.
# To do this, it must define a function called world.get_filler_item_name(), which we will define in world.py later.
# For now, let's make a function that returns the name of a random filler item here in items.py.
def get_random_filler_item_name(world: Crash3World) -> str:
    # ENTFERNE HIER: world.create_filler()

    if world.random.randint(0, 99) < world.options.trap_chance:
        total_weight = (world.options.small_crash_weight +
                        world.options.big_crash_weight +
                        world.options.no_lives_weight +
                        world.options.jetpack_controls_weight)
        if total_weight > 0:
            random_value = world.random.randint(1, total_weight)
            random_value -= world.options.loose_live_weight
            if random_value <= 0:
                return "Loose Live Trap"
            random_value -= world.options.loose_wumpa_weight
            if random_value <= 0:
                return "Loose Wumpa Trap"
            random_value -= world.options.small_crash_weight
            if random_value <= 0:
                return "Small Crash Trap"
            random_value -= world.options.big_crash_weight
            if random_value <= 0:
                return "Big Crash Trap"

            return "No Lives Trap"

    # Falls kein Trap gewählt wurde oder Gewicht 0 ist, gib ein Standard-Filler-Item zurück (z.B. Wumpa Fruit Bundle):
    return "Wumpa Fruit Bundle"


def create_item_with_correct_classification(world: Crash3World, name: str) -> Crash3Item:
    # Our world class must have a create_item() function that can create any of our items by name at any time.
    # So, we make this helper function that creates the item by name with the correct classification.
    # Note: This function's content could just be the contents of world.create_item in world.py directly,
    # but it seemed nicer to have it in its own function over here in items.py.
    classification = DEFAULT_ITEM_CLASSIFICATIONS[name]

    # It is perfectly normal and valid for an item's classification to differ based on the player's options.
    # In our case, Health Upgrades are only relevant to logic (and thus labeled as "progression") in hard mode.
    # if name == "Health Upgrade" and world.options.hard_mode:
    #     classification = ItemClassification.progression

    return Crash3Item(name, classification, ITEM_NAME_TO_ID[name], world.player)


# With those two helper functions defined, let's now get to actually creating and submitting our itempool.
def create_all_items(world: Crash3World) -> None:
    # This is the function in which we will create all the items that this world submits to the multiworld item pool.
    # There must be exactly as many items as there are locations.
    # In our case, there are either six or seven locations.
    # We must make sure that when there are six locations, there are six items,
    # and when there are seven locations, there are seven items.

    # Creating items should generally be done via the world's create_item method.
    # First, we create a list containing all the items that always exist.


    itempool: list[Item] = [
        world.create_item("Blue Gem: Tomb Wader"),
        world.create_item("Red Gem: Deep Trouble"),
        world.create_item("Green Gem: Flaming Passion"),
        world.create_item("Yellow Gem: Hang'em High"),
        world.create_item("Purple Gem: High Time"),
    ]

    itempool += [world.create_item("Crystal: Toad Village")]
    itempool += [world.create_item("Crystal: Under Pressure")]
    itempool += [world.create_item("Crystal: Orient Express")]
    itempool += [world.create_item("Crystal: Bone Yard")]
    itempool += [world.create_item("Crystal: Makin' Waves")]
    itempool += [world.create_item("Crystal: Gee Wiz")]
    itempool += [world.create_item("Crystal: Hang'em High")]
    itempool += [world.create_item("Crystal: Hog Ride")]
    itempool += [world.create_item("Crystal: Tomb Time")]
    itempool += [world.create_item("Crystal: Midnight Run")]
    itempool += [world.create_item("Crystal: Dino Might!")]
    itempool += [world.create_item("Crystal: Deep Trouble")]
    itempool += [world.create_item("Crystal: High Time")]
    itempool += [world.create_item("Crystal: Road Crash")]
    itempool += [world.create_item("Crystal: Double Header")]
    itempool += [world.create_item("Crystal: Sphynxinator")]
    itempool += [world.create_item("Crystal: Bye Bye Blimps")]
    itempool += [world.create_item("Crystal: Tell No Tales")]
    itempool += [world.create_item("Crystal: Future Frenzy")]
    itempool += [world.create_item("Crystal: Tomb Wader")]
    itempool += [world.create_item("Crystal: Gone Tomorrow")]
    itempool += [world.create_item("Crystal: Orange Asphalt")]
    itempool += [world.create_item("Crystal: Flaming Passion")]
    itempool += [world.create_item("Crystal: Mad Bombers")]
    itempool += [world.create_item("Crystal: Bug Lite")]
    itempool += [world.create_item("Clear Gem (Box): Toad Village")]
    itempool += [world.create_item("Clear Gem (Box): Under Pressure")]
    itempool += [world.create_item("Clear Gem (Box): Orient Express")]
    itempool += [world.create_item("Clear Gem (Box): Bone Yard")]
    itempool += [world.create_item("Clear Gem: Bone Yard")]
    itempool += [world.create_item("Clear Gem (Box): Makin' Waves")]
    itempool += [world.create_item("Clear Gem (Box): Gee Wiz")]
    itempool += [world.create_item("Clear Gem (Box): Hang'em High")]
    itempool += [world.create_item("Clear Gem (Box): Hog Ride")]
    itempool += [world.create_item("Clear Gem (Box): Tomb Time")]
    itempool += [world.create_item("Clear Gem: Tomb Time")]
    itempool += [world.create_item("Clear Gem (Box): Midnight Run")]
    itempool += [world.create_item("Clear Gem (Box): Dino Might!")]
    itempool += [world.create_item("Clear Gem: Dino Might!")]
    itempool += [world.create_item("Clear Gem (Box): Deep Trouble")]
    itempool += [world.create_item("Clear Gem (Box): High Time")]
    itempool += [world.create_item("Clear Gem (Box): Road Crash")]
    itempool += [world.create_item("Clear Gem (Box): Double Header")]
    itempool += [world.create_item("Clear Gem (Box): Sphynxinator")]
    itempool += [world.create_item("Clear Gem: Sphynxinator")]
    itempool += [world.create_item("Clear Gem (Box): Bye Bye Blimps")]
    itempool += [world.create_item("Clear Gem (Box): Tell No Tales")]
    itempool += [world.create_item("Clear Gem (Box): Future Frenzy")]
    itempool += [world.create_item("Clear Gem: Future Frenzy")]
    itempool += [world.create_item("Clear Gem (Box): Tomb Wader")]
    itempool += [world.create_item("Clear Gem (Box): Gone Tomorrow")]
    itempool += [world.create_item("Clear Gem: Gone Tomorrow")]
    itempool += [world.create_item("Clear Gem (Box): Orange Asphalt")]
    itempool += [world.create_item("Clear Gem (Box): Flaming Passion")]
    itempool += [world.create_item("Clear Gem (Box): Mad Bombers")]
    itempool += [world.create_item("Clear Gem (Box): Bug Lite")]
    itempool += [world.create_item("Clear Gem: Bug Lite")]
    itempool += [world.create_item("Clear Gem (Box): Ski Crazed")]
    itempool += [world.create_item("Clear Gem (Box): Area 51?")]
    itempool += [world.create_item("Clear Gem: Area 51?")]
    itempool += [world.create_item("Clear Gem (Box): Rings of Power")]
    itempool += [world.create_item("Clear Gem: Rings of Power")]
    itempool += [world.create_item("Clear Gem (Box): Hot Coco")]
    itempool += [world.create_item("Clear Gem (Box): Eggipus Rex")]
    #itempool += [world.create_item("Clear Gem: 105%")]

    boss_levels = {
        "Tiny Tiger",
        "Dingodile",
        "N. Tropy",
        "N. Gin",
        "Dr. Neo Cortex"
    }

    for level_name in levelNameToId.keys():
        # Überspringe das Level, wenn es ein Boss-Level ist
        if level_name in boss_levels:
            continue

        itempool += [world.create_item("Sapphire Relic: " + level_name)]
        itempool += [world.create_item("Gold Relic: " + level_name)]
        itempool += [world.create_item("Platinum Relic: " + level_name)]

    #itempool += [world.create_item("Aku Aku")]

    #if world.options.powerup_lock:
    #    if world.options.body_slam_lock !=0:
    #        itempool += [world.create_item("Body Slam")]
    #    if world.options.double_jump_lock !=0:
    #        itempool += [world.create_item("Double Jump")]
    #    if world.options.tornado_spin_lock !=0:
    #        itempool += [world.create_item("Tornado Spin")]
    #    if world.options.bazooka_lock !=0:
    #        itempool += [world.create_item("Bazooka")]
    #    if world.options.crash_dash_lock !=0:
    #        itempool += [world.create_item("Crash Dash")]

    if world.options.powerup_lock !=0:
        itempool += [world.create_item("Body Slam")]
        itempool += [world.create_item("Double Jump")]
        itempool += [world.create_item("Tornado Spin")]
        itempool += [world.create_item("Bazooka")]
        itempool += [world.create_item("Crash Dash")]

    if world.options.gimmick_lock:
        if world.options.tiger_lock_logic != 0:
            itempool += [world.create_item("Tiger")]
        if world.options.baby_t_lock_logic != 0:
            itempool += [world.create_item("Baby-T")]
        if world.options.jet_sub_lock_logic != 0:
            itempool += [world.create_item("Jet Sub")]
        if world.options.jet_ski_lock_logic != 0:
            itempool += [world.create_item("Jet Ski")]
        if world.options.motorbike_lock_logic != 0:
            itempool += [world.create_item("Motorbike")]
        if world.options.biplane_crash_lock_logic != 0:
            itempool += [world.create_item("Biplane Crash")]
        if world.options.biplane_coco_lock_logic != 0:
            itempool += [world.create_item("Biplane Coco")]
        if world.options.firefly_lock_logic != 0:
            itempool += [world.create_item("Firefly")]

    # Some items may only exist if the player enables certain options.
    # In our case, If the hammer option is enabled, the sixth item is the Hammer.
    # Otherwise, we add a filler Confetti Cannon.
    # if world.options.hammer:
    #     # Once again, it is important to stress that even though the Hammer doesn't always exist,
    #     # it must be present in the worlds item_name_to_id.
    #     # Whether it is actually in the itempool is determined purely by whether we create and add the item here.
    #     itempool.append(world.create_item("Hammer"))

    # Archipelago requires that each world submits as many locations as it submits items.
    # This is where we can use our filler and trap items.
    # APQuest has two of these: The Confetti Cannon and the Math Trap.
    # (Unfortunately, Archipelago is a bit ambiguous about its terminology here:
    #  "filler" is an ItemClassification separate from "trap", but in a lot of its functions,
    #  Archipelago will use "filler" to just mean "an additional item created to fill out the itempool".
    #  "Filler" in this sense can technically have any ItemClassification,
    #  but most commonly ItemClassification.filler or ItemClassification.trap.
    #  Starting here, the word "filler" will be used to collectively refer to APQuest's Confetti Cannon and Math Trap,
    #  which are ItemClassification.filler and ItemClassification.trap respectively.)
    # Creating filler items works the same as any other item. But there is a question:
    # How many filler items do we actually need to create?
    # In regions.py, we created either six or seven locations depending on the "extra_starting_chest" option.
    # In this function, we have created five or six items depending on whether the "hammer" option is enabled.
    # We *could* have a really complicated if-else tree checking the options again, but there is a better way.
    # We can compare the size of our itempool so far to the number of locations in our world.

    # The length of our itempool is easy to determine, since we have it as a list.
    number_of_items = len(itempool)

    # The number of locations is also easy to determine, but we have to be careful.
    # Just calling len(world.get_locations()) would report an incorrect number, because of our *event locations*.
    # What we actually want is the number of *unfilled* locations. Luckily, there is a helper method for this:
    number_of_unfilled_locations = len(world.multiworld.get_unfilled_locations(world.player))


    # Now, we just subtract the number of items from the number of locations to get the number of empty item slots.
    needed_number_of_filler_items = number_of_unfilled_locations - number_of_items
    if needed_number_of_filler_items < 0:
        raise OptionError(f"Crash 3: There are {-needed_number_of_filler_items} more base items than locations.", )
    # Finally, we create that many filler items and add them to the itempool.
    # To create our filler, we could just use world.create_item("Confetti Cannon").
    # But there is an alternative that works even better for most worlds, including APQuest.
    # As discussed above, our world must have a get_filler_item_name() function defined,
    # which must return the name of an infinitely repeatable filler item.
    # Defining this function enables the use of a helper function called world.create_filler().
    # You can just use this function directly to create as many filler items as you need to complete your itempool.
    itempool += [world.create_filler() for _ in range(needed_number_of_filler_items)]

    #itempool[3].deprioritized

    # But... is that the right option for your game? Let's explore that.
    # For some games, the concepts of "regular itempool filler" and "additionally created filler" are different.
    # These games might want / require specific amounts of specific filler items in their regular pool.
    # To achieve this, they will have to intentionally create the correct quantities using world.create_item().
    # They may still use world.create_filler() to fill up the rest of their itempool with "repeatable filler",
    # after creating their "specific quantity" filler and still having room left over.

    # But there are many other games which *only* have infinitely repeatable filler items.
    # They don't care about specific amounts of specific filler items, instead only caring about the proportions.
    # In this case, world.create_filler() can just be used for the entire filler itempool.
    # APQuest is one of these games:
    # Regardless of whether it's filler for the regular itempool or additional filler for item links / etc.,
    # we always just want a Confetti Cannon or a Math Trap depending on the "trap_chance" option.
    # We defined this behavior in our get_random_filler_item_name() function, which in world.py,
    # we'll bind to world.get_filler_item_name(). So, we can just use world.create_filler() for all of our filler.

    # Anyway. With our world's itempool finalized, we now need to submit it to the multiworld itempool.
    # This is how the generator actually knows about the existence of our items.
    world.multiworld.itempool += itempool

    # Sometimes, you might want the player to start with certain items already in their inventory.
    # These items are called "precollected items".
    # They will be sent as soon as they connect for the first time (depending on your client's item handling flag).
    # Players can add precollected items themselves via the generic "start_inventory" option.
    # If you want to add your own precollected items, you can do so via world.push_precollected().
    # if world.options.start_with_one_confetti_cannon:
    #     # We're adding a filler item, but you can also add progression items to the player's precollected inventory.
    #     starting_confetti_cannon = world.create_item("Confetti Cannon")
    #     world.push_precollected(starting_confetti_cannon)

    warp1_locations = [
    "Toad Village: Crystal",
    "Under Pressure: Crystal",
    "Orient Express: Crystal",
    "Bone Yard: Crystal",
    "Makin' Waves: Crystal"
    ]

    warp1_items = [
        "Crystal: Toad Village",
        "Crystal: Under Pressure",
        "Crystal: Orient Express",
        "Crystal: Bone Yard",
        "Crystal: Makin' Waves"
    ]

    shuffled_items = warp1_items.copy()
    world.random.shuffle(shuffled_items)

    for loc_name, item_name in zip(warp1_locations, shuffled_items):
        item_to_lock = world.create_item(item_name)

        world.multiworld.get_location(loc_name, world.player).place_locked_item(item_to_lock)

    warp2_locations = [
        "Gee Wiz: Crystal",
        "Hang'em High: Crystal",
        "Hog Ride: Crystal",
        "Tomb Time: Crystal",
        "Midnight Run: Crystal"
    ]

    warp2_items = [
        "Crystal: Gee Wiz",
        "Crystal: Hang'em High",
        "Crystal: Hog Ride",
        "Crystal: Tomb Time",
        "Crystal: Midnight Run"
    ]

    shuffled_items = warp2_items.copy()
    world.random.shuffle(shuffled_items)

    for loc_name, item_name in zip(warp2_locations, shuffled_items):
        item_to_lock = world.create_item(item_name)

        world.multiworld.get_location(loc_name, world.player).place_locked_item(item_to_lock)

    warp3_locations = [
        "Dino Might!: Crystal",
        "Deep Trouble: Crystal",
        "High Time: Crystal",
        "Road Crash: Crystal",
        "Double Header: Crystal"
    ]

    warp3_items = [
        "Crystal: Dino Might!",
        "Crystal: Deep Trouble",
        "Crystal: High Time",
        "Crystal: Road Crash",
        "Crystal: Double Header"
    ]

    shuffled_items = warp3_items.copy()
    world.random.shuffle(shuffled_items)

    for loc_name, item_name in zip(warp3_locations, shuffled_items):
        item_to_lock = world.create_item(item_name)

        world.multiworld.get_location(loc_name, world.player).place_locked_item(item_to_lock)

    warp4_locations = [
        "Sphynxinator: Crystal",
        "Bye Bye Blimps: Crystal",
        "Tell No Tales: Crystal",
        "Future Frenzy: Crystal",
        "Tomb Wader: Crystal"
    ]

    warp4_items = [
        "Crystal: Sphynxinator",
        "Crystal: Bye Bye Blimps",
        "Crystal: Tell No Tales",
        "Crystal: Future Frenzy",
        "Crystal: Tomb Wader"
    ]

    shuffled_items = warp4_items.copy()
    world.random.shuffle(shuffled_items)

    for loc_name, item_name in zip(warp4_locations, shuffled_items):
        item_to_lock = world.create_item(item_name)

        world.multiworld.get_location(loc_name, world.player).place_locked_item(item_to_lock)

    warp5_locations = [
        "Gone Tomorrow: Crystal",
        "Orange Asphalt: Crystal",
        "Flaming Passion: Crystal",
        "Mad Bombers: Crystal",
        "Bug Lite: Crystal"
    ]

    warp5_items = [
        "Crystal: Gone Tomorrow",
        "Crystal: Orange Asphalt",
        "Crystal: Flaming Passion",
        "Crystal: Mad Bombers",
        "Crystal: Bug Lite"
    ]

    shuffled_items = warp5_items.copy()
    world.random.shuffle(shuffled_items)

    for loc_name, item_name in zip(warp5_locations, shuffled_items):
        item_to_lock = world.create_item(item_name)

        world.multiworld.get_location(loc_name, world.player).place_locked_item(item_to_lock)