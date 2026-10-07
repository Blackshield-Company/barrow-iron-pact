Scriptname BIP_TIF_WakeCollect extends TopicInfo Hidden
{The Barrow-Iron Pact - dialogue fragment: collect the awakened iron.}

Function Fragment_0(ObjectReference akSpeakerRef)
	(GetOwningQuest() as BIP_IntroScript).CollectAwakened()
EndFunction
