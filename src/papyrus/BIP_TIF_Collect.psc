Scriptname BIP_TIF_Collect extends TopicInfo Hidden
{The Barrow-Iron Pact - dialogue fragment: collect.}

Function Fragment_0(ObjectReference akSpeakerRef)
	(GetOwningQuest() as BIP_MainScript).Collect()
EndFunction
