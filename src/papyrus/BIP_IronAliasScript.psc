Scriptname BIP_IronAliasScript extends ReferenceAlias
{The Barrow-Iron Pact - on the barrow-iron cache.}

Event OnContainerChanged(ObjectReference akNewContainer, ObjectReference akOldContainer)
	If akNewContainer == Game.GetPlayer()
		(GetOwningQuest() as BIP_MainScript).IronTaken()
	EndIf
EndEvent
