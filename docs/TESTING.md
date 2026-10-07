# In-game test checklist

The build verifies the plugin, scripts, dialogue structure, voice files and SEQ automatically; none of that proves the quest plays well. Turn on **dialogue subtitles** and Papyrus logging:

```ini
; Skyrim.ini (or SkyrimCustom.ini), [Papyrus]
bEnableLogging=1
bEnableTrace=1
bLoadDebugInformation=1
```

Every log line from this mod starts with `BIP_`. On each load you should see `BIP_ series check: ...` with the detected series mods. `help BIP_ 4` lists the records (ESL-flagged, FormIDs look like `FExxx8xx`).

## Opening

- [ ] Kjeld at the Riverwood map marker; opening at level 14+; GMD/Roadwarden variants.
- [ ] Accept: stage 10.

## Crypt

- [ ] Hollow Warden at the crypt's map marker; loot the iron: stage 20; read the rite: stage 25 (order doesn't matter).

## Endings (save at stage 20/25)

- [ ] Plate or greatsword: iron removed, stage 40; wait 48h: stage 50; collect: item, quest completes.
- [ ] Return (only after reading the rite): torc + Ancestor's Regard, quest completes.
- [ ] Awakening: dragon bone + 2 scales; wait 24h, message; collect: item swapped and re-equipped.

## Series integration

- [ ] Without any other series mod: the plain opening is offered, the log shows all integration values 0, nothing errors.
- [ ] With each listed mod installed, its variant appears (see README, Series integration).

## What to report

Your game version, load order, the `BIP_` lines from Papyrus.0.log, and which step went wrong.
