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
ReferenceAlias Property Warden Auto
ReferenceAlias Property WardenBoss Auto
{Optional: the crypt's boss spot.}
ReferenceAlias Property Rite Auto
Form Property BIP_IronItem Auto
Form Property BIP_RiteItem Auto

Bool RiteWasRead = False
Bool PendingStop = False

Function Begin(ObjectReference akKjeld)
	If akKjeld
		Kjeld.ForceRefTo(akKjeld)
	EndIf
	SetStage(10)
	PlaceAtBoss(Warden, WardenBoss)
	EnsureItems()
	SetObjectiveDisplayed(10)
EndFunction

; 0.1.1: at quest start, and when the Warden loads or dies.
Function EnsureItems()
	If GetStage() != 10
		Return
	EndIf
	If Game.GetPlayer().GetItemCount(BIP_IronItem) > 0
		IronTaken()
		Return
	EndIf
	EnsureOn(Warden, Iron, BIP_IronItem)
	EnsureOn(Warden, Rite, BIP_RiteItem)
EndFunction

; 0.1.1: the rite was picked up. If the Warden is dead and the iron is gone, hand it over.
Function RiteTaken()
	EnsureItems()
	If GetStage() == 10 && Lost(Warden, BIP_IronItem)
		RecoverIron()
	EndIf
EndFunction

; 0.1.1: Kjeld's "the Warden had no iron" dialogue.
Function RecoverIron()
	If GetStage() != 10
		Return
	EndIf
	GiveToPlayer(Iron, BIP_IronItem)
	IronTaken()
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

; ---------------------------------------------------------------------------
; 0.1.1: alias-created quest items could be missing from the NPC that should carry them
; (same bug as Champion's Reforge 0.1.0 / The Gray-Mane Debt 0.1.0). These re-add them by script.
; ---------------------------------------------------------------------------

; Make sure akHolder carries akBase (unless the player already has it).
Function EnsureOn(ReferenceAlias akHolder, ReferenceAlias akItem, Form akBase)
	Actor h = akHolder.GetReference() as Actor
	If !h || !akBase || Game.GetPlayer().GetItemCount(akBase) > 0 || h.GetItemCount(akBase) > 0
		Return
	EndIf
	ObjectReference r = akItem.GetReference()
	If !r
		r = h.PlaceAtMe(akBase, 1, True)
		akItem.ForceRefTo(r)
	EndIf
	h.AddItem(r, 1, True)
	Debug.Trace("BIP_ EnsureOn: re-added " + akBase + " to " + h)
EndFunction

; Hand akBase straight to the player (unstick path). The item alias's own pickup event then advances the quest.
Function GiveToPlayer(ReferenceAlias akItem, Form akBase)
	Actor player = Game.GetPlayer()
	If !akBase || player.GetItemCount(akBase) > 0
		Return
	EndIf
	ObjectReference r = akItem.GetReference()
	If !r
		r = player.PlaceAtMe(akBase, 1, True)
		akItem.ForceRefTo(r)
	EndIf
	player.AddItem(r, 1)
	Debug.Trace("BIP_ GiveToPlayer: " + akBase)
EndFunction

; True when the carrier is dead or gone and no longer has the item.
Bool Function Lost(ReferenceAlias akHolder, Form akBase)
	Actor h = akHolder.GetReference() as Actor
	Return !h || (h.IsDead() && h.GetItemCount(akBase) == 0)
EndFunction

; Move a spawned NPC from the outdoor map marker to the location's boss spot, if it has one.
Function PlaceAtBoss(ReferenceAlias akActor, ReferenceAlias akSpot)
	Actor a = akActor.GetReference() as Actor
	ObjectReference spot = akSpot.GetReference()
	If a && spot && !a.IsDead()
		a.MoveTo(spot)
		Debug.Trace("BIP_ moved " + a + " to the boss spot")
	ElseIf a
		Debug.Trace("BIP_ no boss spot for " + a + "; stays at the map marker")
	EndIf
EndFunction
