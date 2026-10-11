Scriptname BIP_RiteAliasScript extends ReferenceAlias
{The Barrow-Iron Pact - on the Warden's burial rite.}

Event OnContainerChanged(ObjectReference akNewContainer, ObjectReference akOldContainer)
	If akNewContainer == Game.GetPlayer()
		(GetOwningQuest() as BIP_MainScript).RiteTaken()
	EndIf
EndEvent

Event OnRead()
	Utility.Wait(0.1)
	(GetOwningQuest() as BIP_MainScript).RiteRead()
EndEvent
