# Crash Banddicoot: Warped for Archipelago

Welcome to the first pre-alpha test of the Crash Bandicoot Archipelago client!

This is an early test run to check the core functionality and connection. Feedback, bug reports, and suggestions for improvement are very welcome!

If you run into any issues or want to help out, feel free to write to us via the Archipelago server in the Crash Bandicoot 3 channel!

# Instructions:

[Click Here](https://github.com/ArsonAssassin/Archipelago.Core/wiki/How-to-start-playing-a-game-using-this-library) for
general instructions.

## Playing a Game with Crash Bandicoot: Warped

### Required Software

Important: As the mandatory client runs only on Windows, no other systems are supported.

- [Duckstation](https://www.duckstation.org) - Detailed installation instructions for Duckstation can be found at the above link.
- Archipelago version 0.6.7 or later.
- The [Crash 3 Archipelago Client and .apworld](https://github.com/LimeDreaming/C3AP/releases/)
- A legal Crash Bandicoot: Warped USA (NTSC) or Europe (PAL) ROM.  We cannot help with this step.
### Create a Config (.yaml) File

#### What is a config file and why do I need one?

See the guide on setting up a basic YAML at the Archipelago setup guide: [Basic Multiworld Setup Guide](https://archipelago.gg/tutorial/Archipelago/setup_en)

This also includes instructions on generating and hosting the file.  The "On your local installation" instructions
are particularly important.

#### Where do I get a config file?

Run `ArchipelagoLauncher.exe` and generate template files.  Copy `Crash 3.yaml`, fill it out, and place
it in the `players` folder.

### Generate and host your world

Run `ArchipelagoGenerate.exe` to build a world from the YAML files in your `players` folder.  This places
a `.zip` file in the `output` folder.

You may upload this to [the Archipelago website](https://archipelago.gg/uploads) or host the game locally with
`ArchipelagoHost.exe`.

### Setting Up Crash Bandicoot: Warped for Archipelago

1. Download the C3AP-Client-PreAlpha-v0.1.zip and crash3.apworld from the GitHub page linked above.
2. Double click the apworld to install to your Archipelago installation.
3. Open Duckstation and load into Crash Bandicoot: Warped.
4. In Duckstation, navigate to Settings > Game Properties > Console and select "Interpreter" under "Execution Mode".
5. Make sure that Runahead (inside of Emulation Settings) is disabled
6. Start a new game (the client will restore your progress if you are continuing an existing seed).
7. Unpack the C3AP-Client-PreAlpha-v0.1.zip somewhere you like.
8. Open the Unpacked folder and open the C3AP Client.exe [Please after you open Duckstation and started the game].
9. In the top left of the C3AP Client, click the "burger" menu to open the settings page.
10. Enter your host, slot, and optionally your password.
11. Click Connect and optional you can click Save Information to save your settings for the connection.
12. Start playing!

## What works:

  Collecting checks via Crystals, Gems, and Relics.

  Checks completed via Regular Exits and Bosses.

## What is NOT working / Not implemented yet:

  Power-up items are not yet included in the game.

  Several options from the .apworld configuration are not functional yet (specifically Gimmicks, Power-ups, DeadLink, and Traps are currently unavailable).

## Known Issues & What needs to be tested:

  Boss Unlock: If you have collected 5 or more crystals, you currently need to enter a level briefly and then exit back out to unlock the boss button.

  Goals: We still need to verify and test whether the game goals are functioning correctly.

Have fun testing and helping out!
