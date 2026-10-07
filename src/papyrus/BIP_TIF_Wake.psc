Scriptname BIP_TIF_Wake extends TopicInfo Hidden
{The Barrow-Iron Pact - dialogue fragment: start the awakening.}

Function Fragment_0(ObjectReference akSpeakerRef)
	(GetOwningQuest() as BIP_IntroScript).StartAwakening()
EndFunction
