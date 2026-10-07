Scriptname BIP_TIF_Plate extends TopicInfo Hidden
{The Barrow-Iron Pact - dialogue fragment: forge plate.}

Function Fragment_0(ObjectReference akSpeakerRef)
	(GetOwningQuest() as BIP_MainScript).Forge(1)
EndFunction
