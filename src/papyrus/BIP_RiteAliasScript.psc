Scriptname BIP_RiteAliasScript extends ReferenceAlias
{The Barrow-Iron Pact - on the Warden's burial rite.}

Event OnRead()
	Utility.Wait(0.1)
	(GetOwningQuest() as BIP_MainScript).RiteRead()
EndEvent
