Scriptname BIP_TIF_Return extends TopicInfo Hidden
{The Barrow-Iron Pact - dialogue fragment: return the iron.}

Function Fragment_0(ObjectReference akSpeakerRef)
	(GetOwningQuest() as BIP_MainScript).ReturnIron()
EndFunction
