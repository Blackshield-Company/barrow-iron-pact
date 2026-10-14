# Changelog

All notable changes to this project are documented here. Versions follow [Semantic Versioning](https://semver.org/).

## [0.1.3] - 2026-10-14

Still early beta, not yet playtested in game. Save-compatible with 0.1.2 (no records added or removed, no form IDs changed).

### Fixed
- Kjeld's status line after you accept could show a raw location tag (`<Alias=CryptLoc>`). It no longer uses a tag; the crypt is still named in the journal and objective.

### Changed
- The build check now fails on any `<Alias=...>` tag in dialogue (responses, prompts, topic text) or quest text (name, journal, objectives) that is not a filled, non-optional alias of the quest that owns the text, and on alias-created notes whose tags would not resolve.

## [0.1.2] - 2026-10-13

Still early beta, not yet playtested in game.

### Fixed
- The quest giver's accept line showed a raw location alias.

### Added
- A build check for cross-quest alias tokens.

## [0.1.1] - 2026-10-11

Still early beta, not yet playtested in game.

### Fixed
- The barrow-iron could be missing from the Hollow Warden, so the quest never moved past "take the iron". The quest now gives the iron and the burial rite to the Warden by script when it starts, and checks again when he loads and when he dies. If you already carry the iron, the quest catches up.
- Picking up the burial rite when the Warden is dead and the iron is gone now gives you the iron.
- Stuck saves (0.1.0): with the burial rite in your inventory, tell Kjeld "The Warden had no iron on him." He hands it over and the quest continues.

### Changed
- The Hollow Warden now waits at the crypt's boss spot instead of the outdoor map marker (crypts without one keep the marker).

## [0.1.0] - 2026-10-07

Early beta. First public release. It passes every automated check in the build (Papyrus compile, plugin and dialogue validation, round trip, voice file and SEQ checks, FOMOD schema, 15-plugin series compatibility check) but has not been playtested in-game yet.

### Added
- "You don't look like a Riverwood smith." Talk to Kjeld near Riverwood (level 14+).
- The Hollow Warden (a draugr boss) waits at a Nordic crypt in Whiterun, Falkreath, Hjaalmarch, the Pale, Eastmarch or the Rift. He carries the barrow-iron and a burial rite.
- Read the rite (optional) to learn the iron was a pact with the dead. Only then can you choose to return it.
- Bring the iron to Kjeld and choose. Forging takes two days.
- Awakening (optional, later): a dragon bone and two dragon scales; a day later the awakened version.
- Choice: Plate: Barrow-Iron Plate (frost resistance 40%, heavy armor +20); awakens into plate with +50 health as well.
- Choice: Greatsword: Barrow-Iron Greatsword (frost + absorb stamina); awakens into a blade that also turns undead.
- Choice: Honor the pact: Ancestor's Torc (health +30, frost resistance 30%) and Ancestor's Regard, a permanent ability (+25 armor, +25 stamina).
- Soft integration with The Gray-Mane Debt (runtime lookup, not a master).
- Soft integration with Roadwarden (runtime lookup, not a master).
- Unvoiced dialogue with 26 silent voice files; `SEQ/BarrowIronPact.seq` for the start-game-enabled dialogue quest.
- FOMOD installer with optional Papyrus sources.
