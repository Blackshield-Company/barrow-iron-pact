Scriptname BIP_SeriesScript extends ReferenceAlias
{The Barrow-Iron Pact - soft integration with the rest of the series, on the player alias of BIP_Intro.
Plugins are looked up at runtime (vanilla Papyrus, no SKSE); when one is missing the call returns None.}

GlobalVariable Property BIP_SeriesGMD Auto
{0 = The Gray-Mane Debt not installed, otherwise GMD_State + 1 (3 = helped Eorlund)}
GlobalVariable Property BIP_SeriesRWD Auto

Event OnInit()
	Refresh()
EndEvent

Event OnPlayerLoadGame()
	Refresh()
EndEvent

Function Refresh()
	GlobalVariable gmdState = Game.GetFormFromFile(0x802, "GrayManeDebt.esp") as GlobalVariable ; soft: GMD_State
	If gmdState
		BIP_SeriesGMD.SetValue(gmdState.GetValue() + 1.0)
	Else
		BIP_SeriesGMD.SetValue(0.0)
	EndIf
	GlobalVariable rwdCount = Game.GetFormFromFile(0x81A, "Roadwarden.esp") as GlobalVariable ; soft: RWD_EncounterCount
	If rwdCount
		BIP_SeriesRWD.SetValue(1.0)
	Else
		BIP_SeriesRWD.SetValue(0.0)
	EndIf
	Debug.Trace("BIP_ series check: GMD " + BIP_SeriesGMD.GetValueInt() + ", RWD " + BIP_SeriesRWD.GetValueInt())
EndFunction
