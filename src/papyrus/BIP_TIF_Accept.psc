Scriptname BIP_TIF_Accept extends TopicInfo Hidden
{The Barrow-Iron Pact - dialogue fragment: accept.}

Function Fragment_0(ObjectReference akSpeakerRef)
	(GetOwningQuest() as BIP_IntroScript).Accept()
EndFunction
