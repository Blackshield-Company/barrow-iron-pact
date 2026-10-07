Scriptname BIP_TIF_Sword extends TopicInfo Hidden
{The Barrow-Iron Pact - dialogue fragment: forge a greatsword.}

Function Fragment_0(ObjectReference akSpeakerRef)
	(GetOwningQuest() as BIP_MainScript).Forge(2)
EndFunction
