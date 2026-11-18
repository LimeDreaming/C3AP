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

LEVEL_ITEM_ID ={

    }

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
    "Relic: Toad Village" : 142069,
    "Relic: Under Pressure" : 142070,
    "Relic: Orient Express" : 142071,
    "Relic: Bone Yard" : 142072,
    "Relic: Makin' Waves" : 142073,
    "Relic: Gee Wiz" : 142074,
    "Relic: Hang'em High" : 142075,
    "Relic: Hog Ride" : 142076,
    "Relic: Tomb Time" : 142077,
    "Relic: Midnight Run" : 142078,
    "Relic: Dino Might!" : 142079,
    "Relic: Deep Trouble" : 142080,
    "Relic: High Time" : 142081,
    "Relic: Road Crash" : 142082,
    "Relic: Double Header" : 142083,
    "Relic: Sphynxinator" : 142084,
    "Relic: Bye Bye Blimps" : 142085,
    "Relic: Tell No Tales" : 142086,
    "Relic: Future Frenzy" : 142087,
    "Relic: Tomb Wader" : 142088,
    "Relic: Gone Tomorrow" : 142089,
    "Relic: Orange Asphalt" : 142090,
    "Relic: Flaming Passion" : 142091,
    "Relic: Mad Bombers" : 142092,
    "Relic: Bug Lite" : 142093,
    "Relic: Ski Crazed" : 142094,
    "Relic: Area 51?" : 142095,
    "Relic: Rings of Power" : 142096,
    "Relic: Hot Coco" : 142097,
    "Relic: Eggipus Rex" : 142098,
    "Blue Gem: Tomb Wader": 142064,
    "Red Gem: Deep Trouble": 142065,
    "Green Gem: Flaming Passion": 142066,
    "Yellow Gem: Hang'em High": 142067,
    "Purple Gem: High Time": 142068,
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
    #"Crystal": 142181,
    #"Clear Gem": 142182,
    #"Sapphire Relic": 142183,
    #"Gold Relic": 142184,
    #"Platinum Relic": 142185,
    "Clear Gem: 105%": 142186,
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
    "Relic: Toad Village" : ItemClassification.progression,
    "Relic: Under Pressure" : ItemClassification.progression,
    "Relic: Orient Express" : ItemClassification.progression,
    "Relic: Bone Yard" : ItemClassification.progression,
    "Relic: Makin' Waves" : ItemClassification.progression,
    "Relic: Gee Wiz" : ItemClassification.progression,
    "Relic: Hang'em High" : ItemClassification.progression,
    "Relic: Hog Ride" : ItemClassification.progression,
    "Relic: Tomb Time" : ItemClassification.progression,
    "Relic: Midnight Run" : ItemClassification.progression,
    "Relic: Dino Might!" : ItemClassification.progression,
    "Relic: Deep Trouble" : ItemClassification.progression,
    "Relic: High Time" : ItemClassification.progression,
    "Relic: Road Crash" : ItemClassification.progression,
    "Relic: Double Header" : ItemClassification.progression,
    "Relic: Sphynxinator" : ItemClassification.progression,
    "Relic: Bye Bye Blimps" : ItemClassification.progression,
    "Relic: Tell No Tales" : ItemClassification.progression,
    "Relic: Future Frenzy" : ItemClassification.progression,
    "Relic: Tomb Wader" : ItemClassification.progression,
    "Relic: Gone Tomorrow" : ItemClassification.progression,
    "Relic: Orange Asphalt" : ItemClassification.progression,
    "Relic: Flaming Passion" : ItemClassification.progression,
    "Relic: Mad Bombers" : ItemClassification.progression,
    "Relic: Bug Lite" : ItemClassification.progression,
    "Relic: Ski Crazed" : ItemClassification.progression,
    "Relic: Area 51?" : ItemClassification.progression,
    "Relic: Rings of Power" : ItemClassification.progression,
    "Relic: Hot Coco" : ItemClassification.progression,
    "Relic: Eggipus Rex" : ItemClassification.progression,
    #"Aku Aku" : ItemClassification.filler,
    "Life Bundle" : ItemClassification.filler,
    "Wumpa Fruit Bundle" : ItemClassification.filler,
    "Loose Live Trap" : ItemClassification.trap,
    "Loose Wumpa Trap" : ItemClassification.trap,
    "Big Crash Trap" : ItemClassification.trap,
    "Small Crash Trap" : ItemClassification.trap,
    "No Lives Trap" : ItemClassification.trap,
    #"Jetpack Controls Trap" : ItemClassification.trap,
    "Body Slam" : ItemClassification.progression,
    "Double Jump" : ItemClassification.progression,
    "Tornado Spin" : ItemClassification.progression,
    "Bazooka" : ItemClassification.progression,
    "Crash Dash" : ItemClassification.useful,
    "Tiger" : ItemClassification.progression | ItemClassification.useful,
    "Baby-T" : ItemClassification.progression | ItemClassification.useful,
    "Jet Sub" : ItemClassification.progression | ItemClassification.useful,
    "Jet Ski" : ItemClassification.progression | ItemClassification.useful,
    "Motorbike" : ItemClassification.progression | ItemClassification.useful,
    "Biplane Crash" : ItemClassification.progression | ItemClassification.useful,
    "Biplane Coco" : ItemClassification.progression | ItemClassification.useful,
    "Firefly" : ItemClassification.progression | ItemClassification.useful,
    #"Crystal" : ItemClassification.progression,
    #"Clear Gem" : ItemClassification.useful,
    #"Sapphire Relic" : ItemClassification.useful,
    #"Gold Relic" : ItemClassification.useful,
    #"Platinum Relic" : ItemClassification.useful,
    "Clear Gem: 105%" : ItemClassification.progression | ItemClassification.useful,
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

        itempool += [world.create_item("Relic: " + level_name)]



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

    #if world.options.level_items_as_items:
    for name in ITEM_NAME_TO_ID:
        if name.startswith(("Crystal: ", "Clear Gem: ", "Clear Gem (Box): ")):
            itempool.append(world.create_item(name))

    #if not world.options.level_items_as_items:
    #    for i in range(25):
    #        itempool += [world.create_item("Crystal")]
    #    for i in range(42):
    #        itempool += [world.create_item("Clear Gem")]



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
    #if needed_number_of_filler_items < 0:
    #   raise OptionError(f"Crash 3: There are {-needed_number_of_filler_items} more base items than locations.", )
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