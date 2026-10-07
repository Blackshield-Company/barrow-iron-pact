# Changelog

All notable changes to this project are documented here. Versions follow [Semantic Versioning](https://semver.org/).

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
