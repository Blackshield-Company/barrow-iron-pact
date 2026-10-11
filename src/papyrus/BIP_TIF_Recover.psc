Scriptname BIP_TIF_Recover extends TopicInfo Hidden
{The Barrow-Iron Pact - dialogue fragment (0.1.1): the Warden had no iron; Kjeld hands it over.}

Function Fragment_0(ObjectReference akSpeakerRef)
	(GetOwningQuest() as BIP_MainScript).RecoverIron()
EndFunction
