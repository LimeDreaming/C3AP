from dataclasses import dataclass

from Options import Toggle, DefaultOnToggle, Option, Range, Choice, ItemDict, OptionList, DeathLink, PerGameCommonOptions
from Options import OptionGroup, OptionSet


# In this file, we define the options the player can pick.
# The most common types of options are Toggle, Range and Choice.

# Options will be in the game's template yaml.
# They will be represented by checkboxes, sliders etc. on the game's options page on the website.
# (Note: Options can also be made invisible from either of these places by overriding Option.visibility.
#  APQuest doesn't have an example of this, but this can be used for secret / hidden / advanced options.)

# For further reading on options, you can also read the Options API Document:
# https://github.com/ArchipelagoMW/Archipelago/blob/main/docs/options%20api.md


# The first type of Option we'll discuss is the Toggle.
# A toggle is an option that can either be on or off. This will be represented by a checkbox on the website.
# The default for a toggle is "off".
# If you want a toggle to be on by default, you can use the "DefaultOnToggle" class instead of the "Toggle" class.

class Goal(Choice):
    """
    Choose what type of Goal you want!
    Normal: Collect crystals and defeat the boss.
    100: Complete 100%.
    105: Complete 105% (all secrets).
    """
    display_name = "Goal"

    option_normal = 0
    option_100 = 1
    option_105 = 2

    default = option_normal

class LevelExitLocations(Toggle):
    """
    Add all 27 level exits as checks into the pool
    These are distinct from the secret exit checks
    """
    display_name = "Level Exit Locations"

class TrapChance(Range):

    """
    Chance to replace a filler life/fruit with a trap
    If playing with fruit sanity you should lower this chance because there will be a LOT of traps
    """
    display_name = "Trap Chance"

    range_start = 0
    range_end = 100
    default = 0

class TrapDuration(Range):
    """
    Amount of seconds a trap will be active for
    Receiving a trap when it was already active will reset the duration
    """
    display_name = "Trap Duration"

    range_start = 0
    range_end = 600
    default = 30

class LooseLiveTrapWeight(Range):

    """
    Relative chance for a trap to loose lives
    Chance for a specific trap to be picked is weight / totalWeight
    """
    display_name = "Loose Live Trap Weight"

    range_start = 0
    range_end = 100
    default = 10

class LooseWumpaTrapWeight(Range):

    """
    Relative chance for a trap to loose wumpa fruits
    Chance for a specific trap to be picked is weight / totalWeight
    """
    display_name = "Loose Wumpa Trap Weight"

    range_start = 0
    range_end = 100
    default = 10

class SmallCrashTrapWeight(Range):

    """
    Relative chance for a trap to be a Small Crash Trap
    This makes Crash smol
    (Due to an issue with Polar + small Crash, this trap will be inactive in bear levels)
    Chance for a specific trap to be picked is weight / totalWeight
    """
    display_name = "Small Crash Trap Weight"

    range_start = 0
    range_end = 100
    default = 10

class BigCrashTrapWeight(Range):

    """
    Relative chance for a trap to be a Big Crash Trap
    This makes Crash beeg
    """
    display_name = "Big Crash Trap Weight"

    range_start = 0
    range_end = 100
    default = 10

class NoLivesTrapWeight(Range):
    """
    Relative chance for a trap to be a No Lives Trap
    This temporarily removes all your lives
    """
    display_name = "No Lives Weight"

    range_start = 0
    range_end = 100
    default = 10

class PowerupLock(Toggle):
    """
    This locks the powerups Body Slam, Double Jump, Tornado Spin, Bazooka and Crash Dash behind an item.
    """
    display_name = "Powerup Lock"

    option_disabled = 0
    option_enabled = 1

    default = option_enabled

class GimmickLock(Toggle):
    """
    This serves as a universal toggle to enable/disable locking the Jetpack, Jetboard, Fireflies, and Polar behind an item.
    Each gimmick can be customized in how strict the logic rules are or be disabled completely.
    Disabled: This gimmick will not be locked
    If you don't have X gimmick unlocked, then:
    Basic: An entire level that normally contains X gimmick will be out of logic
    Normal: Anything that is trivial to obtain without X gimmick will be in logic
    (trivial means it is either expected of you from the vanilla game or doesn't require any complex movement)
    Lunatic: Anything that is possible to obtain without X gimmick will be in logic
    """
    display_name = "Gimmick Lock"

class TigerLockLogic(Choice):
    """
    -Note: Playing Jetpack levels without the Jetpack can sometimes crash the game-
    Normal: puts most of the wumpa fruit in logic
    Lunatic: getting through the invisible barrier is in logic
    """
    display_name = "Tiger Lock Logic"

    option_disabled = 0
    option_basic = 1
    option_normal = 2
    option_lunatic = 3

    default = option_basic


class BabyTLockLogic(Choice):
    """
    Normal: a few wumpa fruit are put in logic along with the Air Crash secret exit
    Nothing crazy is possible, so normal is equivalent to lunatic
    """
    display_name = "Baby-T Lock Logic"

    option_disabled = 0
    option_basic = 1
    option_normal = 2
    #option_lunatic = 3

    default = option_basic


class JetSubLockLogic(Choice):
    """
    -Note: Playing Polar levels without Polar can sometimes crash the game-
    Normal: Bear It wumpa fruit and Un-Bearable wumpa fruit up to the Polar section are in logic
    Lunatic: Everything except the Totally Bear box gem is in logic
    """
    display_name = "Jet Sub Lock Logic"

    option_disabled = 0
    option_basic = 1
    option_normal = 2
    option_lunatic = 3

    default = option_basic

class JetSkiLockLogic(Choice):
    """
    -Note: Playing Polar levels without Polar can sometimes crash the game-
    Normal: Bear It wumpa fruit and Un-Bearable wumpa fruit up to the Polar section are in logic
    Lunatic: Everything except the Totally Bear box gem is in logic
    """
    display_name = "Jet Ski Lock Logic"

    option_disabled = 0
    option_basic = 1
    option_normal = 2
    option_lunatic = 3

    default = option_basic

class MotorbikeLockLogic(Choice):
    """
    -Note: Playing Polar levels without Polar can sometimes crash the game-
    Normal: Bear It wumpa fruit and Un-Bearable wumpa fruit up to the Polar section are in logic
    Lunatic: Everything except the Totally Bear box gem is in logic
    """
    display_name = "Motorbike Lock Logic"

    option_disabled = 0
    option_basic = 1
    option_normal = 2
    option_lunatic = 3

    default = option_basic

class BiplaneCrashLockLogic(Choice):
    """
    -Note: Playing Polar levels without Polar can sometimes crash the game-
    Normal: Bear It wumpa fruit and Un-Bearable wumpa fruit up to the Polar section are in logic
    Lunatic: Everything except the Totally Bear box gem is in logic
    """
    display_name = "Biplane Crash Lock Logic"

    option_disabled = 0
    option_basic = 1
    option_normal = 2
    option_lunatic = 3

    default = option_basic

class BiplaneCocoLockLogic(Choice):
    """
    -Note: Playing Polar levels without Polar can sometimes crash the game-
    Normal: Bear It wumpa fruit and Un-Bearable wumpa fruit up to the Polar section are in logic
    Lunatic: Everything except the Totally Bear box gem is in logic
    """
    display_name = "Biplane Coco Lock Logic"

    option_disabled = 0
    option_basic = 1
    option_normal = 2
    option_lunatic = 3

    default = option_basic

class FireflyLockLogic(Choice):
    """
    Normal: 3 wumpa fruit are available (nothing in the dark is in logic)
    Lunatic: Everything (except very missable and un-fun wumpa fruit) is in logic
    """
    display_name = "Firefly Lock Logic"

    option_disabled = 0
    option_basic = 1
    option_normal = 2
    option_lunatic = 3

    default = option_basic

# # We must now define a dataclass inheriting from PerGameCommonOptions that we put all our options in.
# # This is in the format "option_name_in_snake_case: OptionClassName".
@dataclass
class Crash3Options(PerGameCommonOptions):

    level_exit_locations: LevelExitLocations
    death_link: DeathLink
    goal_option: Goal
    trap_chance: TrapChance
    trap_duration: TrapDuration
    loose_live_weight: LooseLiveTrapWeight
    loose_wumpa_weight: LooseWumpaTrapWeight
    small_crash_weight: SmallCrashTrapWeight
    # small_crash_size: SmallCrashSize
    big_crash_weight: BigCrashTrapWeight
    # big_crash_size: BigCrashSize
    no_lives_weight: NoLivesTrapWeight

    powerup_lock: PowerupLock
    gimmick_lock: GimmickLock
    tiger_lock_logic: TigerLockLogic
    baby_t_lock_logic: BabyTLockLogic
    jet_sub_lock_logic: JetSubLockLogic
    jet_ski_lock_logic: JetSkiLockLogic
    motorbike_lock_logic: MotorbikeLockLogic
    biplane_crash_lock_logic: BiplaneCrashLockLogic
    biplane_coco_lock_logic: BiplaneCocoLockLogic
    firefly_lock_logic: FireflyLockLogic

#
# # If we want to group our options by similar type, we can do so as well. This looks nice on the website.
option_groups = [
    OptionGroup(
        "Trap Options",
        [TrapChance, TrapDuration, LooseLiveTrapWeight, LooseWumpaTrapWeight, SmallCrashTrapWeight, BigCrashTrapWeight, NoLivesTrapWeight],
    ),
    OptionGroup(
        "Powerup Lock",
        [PowerupLock],
    ),
    OptionGroup(
        "Gimmick Lock",
        [GimmickLock, TigerLockLogic, BabyTLockLogic, JetSubLockLogic, JetSkiLockLogic, MotorbikeLockLogic, BiplaneCrashLockLogic, BiplaneCocoLockLogic, FireflyLockLogic],
    ),
]

# Finally, we can define some option presets if we want the player to be able to quickly choose a specific "mode".
# option_presets = {
#     "boring": {
#         "hard_mode": False,
#         "hammer": False,
#         "extra_starting_chest": False,
#         "start_with_one_confetti_cannon": False,
#         "trap_chance": 0,
#         "confetti_explosiveness": ConfettiExplosiveness.range_start,
#         "player_sprite": PlayerSprite.option_human,
#     },
#     "the true way to play": {
#         "hard_mode": True,
#         "hammer": True,
#         "extra_starting_chest": True,
#         "start_with_one_confetti_cannon": True,
#         "trap_chance": 50,
#         "confetti_explosiveness": ConfettiExplosiveness.range_end,
#         "player_sprite": PlayerSprite.option_duck,
#     },
# }