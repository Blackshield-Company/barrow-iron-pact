Scriptname BIP_MainScript extends Quest
{The Barrow-Iron Pact - questline controller.
Stages: 10 take the iron, 20 iron taken, 25 rite read, 40 forging (two days), 50 ready, 100 forged, 110 returned.}

ReferenceAlias Property Kjeld Auto
ReferenceAlias Property Iron Auto
GlobalVariable Property BIP_State Auto
GlobalVariable Property BIP_Form Auto
Armor Property BIP_BarrowPlate Auto
Weapon Property BIP_BarrowBlade Auto
Armor Property BIP_AncestorTorc Auto
Spell Property BIP_AncestorRegard Auto

Bool RiteWasRead = False
Bool PendingStop = False

Function Begin(ObjectReference akKjeld)
	If akKjeld
		Kjeld.ForceRefTo(akKjeld)
	EndIf
	SetStage(10)
	SetObjectiveDisplayed(10)
EndFunction

Function IronTaken()
	If GetStage() != 10
		Return
	EndIf
	SetObjectiveCompleted(10)
	SetStage(20)
	SetObjectiveDisplayed(20)
	If RiteWasRead
		SetStage(25)
	Else
		SetObjectiveDisplayed(21)
	EndIf
EndFunction

Function RiteRead()
	RiteWasRead = True
	If GetStage() == 20
		SetObjectiveCompleted(21)
		SetStage(25)
	EndIf
EndFunction

Function TakeIron()
	ObjectReference item = Iron.GetReference()
	Actor player = Game.GetPlayer()
	If item && player.GetItemCount(item.GetBaseObject()) > 0
		player.RemoveItem(item.GetBaseObject(), 1, True)
	EndIf
	SetObjectiveCompleted(20)
	SetObjectiveDisplayed(21, False)
EndFunction

Function Forge(Int aiForm)
	Int stage = GetStage()
	If stage != 20 && stage != 25
		Return
	EndIf
	TakeIron()
	BIP_Form.SetValue(aiForm)
	SetStage(40)
	SetObjectiveDisplayed(40)
	RegisterForSingleUpdateGameTime(48.0)
EndFunction

Event OnUpdateGameTime()
	If GetStage() == 40
		SetObjectiveCompleted(40)
		SetStage(50)
		SetObjectiveDisplayed(50)
	EndIf
EndEvent

Function Collect()
	If GetStage() != 50
		Return
	EndIf
	If BIP_Form.GetValueInt() == 1
		Game.GetPlayer().AddItem(BIP_BarrowPlate, 1)
		BIP_State.SetValue(2)
	Else
		Game.GetPlayer().AddItem(BIP_BarrowBlade, 1)
		BIP_State.SetValue(3)
	EndIf
	SetObjectiveCompleted(50)
	SetStage(100)
	Finish()
EndFunction

Function ReturnIron()
	If GetStage() != 25
		Return
	EndIf
	TakeIron()
	Actor player = Game.GetPlayer()
	player.AddItem(BIP_AncestorTorc, 1)
	player.AddSpell(BIP_AncestorRegard)
	BIP_State.SetValue(4)
	SetStage(110)
	Finish()
EndFunction

Function Finish()
	PendingStop = True
	RegisterForSingleUpdate(4.0)
EndFunction

Event OnUpdate()
	If PendingStop
		PendingStop = False
		Debug.Trace("BIP_ questline finished at stage " + GetStage() + ", state " + BIP_State.GetValueInt())
		Stop()
	EndIf
EndEvent
