# Nexus Mods page: The Barrow-Iron Pact

Early beta: v0.1.2 passed every automated build check but hasn't been playtested in-game. Exact upload-form values are in [NEXUS_FIELDS.md](NEXUS_FIELDS.md).

## Page settings

- **Game:** Skyrim Special Edition
- **Mod name:** The Barrow-Iron Pact
- **Version:** 0.1.2 (early beta)
- **Category:** Quests and Adventures
- **Suggested tags:** Quests, Lore-Friendly, Dialogue, Choices and Consequences, Armour, Weapons, ESL, No SKSE, Scripted
- **Requirements:** none.
- **Permissions (suggested):** source on GitHub under MIT; allow modification with credit, no re-uploads of the archive.
- **File to upload:** `BarrowIronPact-0.1.2.zip` (FOMOD), main file, named "The Barrow-Iron Pact".

## Description (paste into the BBCode editor)

Same text as `nexus/bip-description.txt` in the workspace:

```bbcode
[center][color=#e06c3c][b]EARLY RELEASE (v0.1.2)[/b]: new mod, not yet widely playtested. Please report bugs in the Bugs tab with your Papyrus log lines (grep BIP_). Source: [url=https://github.com/synthalorian/barrow-iron-pact]GitHub[/url][/color][/center]

[center][size=6][b]THE BARROW-IRON PACT[/b][/size]
[size=3][i]Iron that never rusts, a warden who never sleeps, and a pact older than Skyrim's kings.[/i][/size][/center]

[line]

[size=4][b]Summary[/b][/size]
[b]Status: Early beta (v0.1.2).[/b] Every automated build check passes, but it hasn't had wide in-game playtesting yet. It's playable now. Bug reports help shape 0.1.x.
An ancient-armor reforging quest. [b]Kjeld Barrow-Smith[/b], last of the smiths who forged for the dead, waits near [b]Riverwood[/b]. A Hollow Warden still guards a cache of barrow-iron in an old crypt.

[size=4][b]The quest[/b][/size]
[list]
[*][b]"You don't look like a Riverwood smith."[/b] Talk to Kjeld near Riverwood (level 14+).
[*][b]The Hollow Warden[/b] (a draugr boss) waits at a Nordic crypt in Whiterun, Falkreath, Hjaalmarch, the Pale, Eastmarch or the Rift. He carries the barrow-iron and a [b]burial rite[/b].
[*][b]Read the rite[/b] (optional) to learn the iron was a pact with the dead. Only then can you choose to return it.
[*][b]Bring the iron to Kjeld[/b] and choose. Forging takes two days.
[*][b]Awakening (optional, later)[/b]: a dragon bone and two dragon scales; a day later the awakened version.
[/list]

[size=4][b]Choices and rewards[/b][/size]
[list]
[*][b]Plate[/b] ("Forge me plate."): [b]Barrow-Iron Plate[/b] (frost resistance 40%, heavy armor +20); awakens into plate with +50 health as well
[*][b]Greatsword[/b] ("Forge me a greatsword."): [b]Barrow-Iron Greatsword[/b] (frost + absorb stamina); awakens into a blade that also turns undead
[*][b]Honor the pact[/b] ("Honor the pact. Return the iron to the dead." (needs the rite read)): [b]Ancestor's Torc[/b] (health +30, frost resistance 30%) and [b]Ancestor's Regard[/b], a permanent ability (+25 armor, +25 stamina)
[/list]

[size=4][b]Enhanced items[/b][/size]
[list]
[*]Barrow-Iron Plate / Greatsword, each awakenable
[*]Ancestor's Torc, Ancestor's Regard (ability)
[*]Barrow-Iron Skull Plate, Warden's Burial Rite
[/list]

[size=4][b]Part of the series[/b][/size]
Notices the other synthalorian mods when they're installed and never requires them. Each one is looked up at runtime with vanilla Papyrus (no SKSE, no masters):
[list]
[*][b]The Gray-Mane Debt[/b]: If you helped Eorlund, Kjeld got his letter (different opening).
[*][b]Roadwarden[/b]: Kjeld sees the road dust on you (different opening).
[/list]

[size=4][b]Unvoiced dialogue that you can actually read[/b][/size]
The new lines are unvoiced and shown as subtitles. Every line ships with a silent voice file sized to its length, so subtitles stay on screen. [b]No Fuz Ro D'oh needed[/b]. Turn on dialogue subtitles.

[size=4][b]Requirements[/b][/size]
[list]
[*]Skyrim Special Edition or Anniversary Edition (1.5.97 or 1.6.x). Works on Steam under Proton on Linux.
[*]Nothing else. No SKSE, no SkyUI, no DLC, no Creation Club content.
[/list]

[size=4][b]Installation[/b][/size]
Install with [b]Mod Organizer 2[/b] or [b]Vortex[/b] (FOMOD installer) and enable [b]BarrowIronPact.esp[/b]. It's ESL-flagged. Safe to add mid-playthrough.

[size=4][b]Compatibility[/b][/size]
[list]
[*][b]No edits to vanilla records[/b], no cells, no navmesh. Skyrim.esm is the only master.
[*]New NPCs appear at vanilla map markers; mods that rebuild those spots may leave them standing somewhere odd.
[*]Compatible with all fifteen mods of the series (same author), checked by loading every plugin together: unique FormIDs, EditorIDs, scripts, globals and files, zero overrides.
[/list]

[size=4][b]What got scaled down / known issues[/b][/size]
[list]
[*]The barrow-iron is shown with the vanilla skull mesh (the iron hammered over a skull); there's no new ingot model.
[*]The barrow-iron gear reuses the vanilla iron cuirass and iron greatsword meshes.
[*]Returning the iron happens off-screen through Kjeld; you don't walk it back into the crypt.
[*]Kjeld is a new NPC at the Riverwood map marker; no cell edits.
[*]Not playtested in-game.
[*]Needs a free Nordic crypt with a map marker in the listed holds.
[*]The Hollow Warden comes from the draugr boss list at 'very hard' level: tough at level 14.
[*]Settings are console globals: [code]set BIP_MinLevel to 14[/code]
[/list]

[size=4][b]Uninstalling[/b][/size]
As with any scripted mod, finish the quest before removing it.

[size=4][b]Source[/b][/size]
Open source (MIT): [url=https://github.com/synthalorian/barrow-iron-pact]github.com/synthalorian/barrow-iron-pact[/url]

[size=4][b]Credits[/b][/size]
[list]
[*][url=https://github.com/Mutagen-Modding/Mutagen]Mutagen[/url] by Noggog and contributors.
[*][url=https://github.com/russo-2025/papyrus-compiler]papyrus-compiler[/url] by russo-2025.
[*][url=https://github.com/GandaG/fomod-schema]FOMOD schema[/url] by GandaG.
[*]Bethesda Game Studios for Skyrim.
[/list]

```
