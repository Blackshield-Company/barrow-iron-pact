Scriptname BIP_WardenAliasScript extends ReferenceAlias
{The Barrow-Iron Pact - on the Hollow Warden. 0.1.1: re-checks that he carries the iron and the rite.}

Event OnLoad()
	(GetOwningQuest() as BIP_MainScript).EnsureItems()
EndEvent

Event OnDeath(Actor akKiller)
	(GetOwningQuest() as BIP_MainScript).EnsureItems()
EndEvent
