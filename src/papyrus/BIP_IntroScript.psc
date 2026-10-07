Scriptname BIP_IntroScript extends Quest
{The Barrow-Iron Pact - always-running quest: Kjeld near Riverwood, his opening topic, and the awakening upgrade.}

Quest Property BIP_Main Auto
GlobalVariable Property BIP_State Auto
{0 = not started, 1 = running, 2 = plate, 3 = greatsword, 4 = iron returned, 5 = awakening, 6 = awakened}
GlobalVariable Property BIP_Form Auto
GlobalVariable Property BIP_AwakenReady Auto
Message Property BIP_MsgNoStart Auto
Message Property BIP_MsgReady Auto
ReferenceAlias Property Kjeld Auto
Armor Property BIP_BarrowPlate Auto
Armor Property BIP_BarrowPlateAwake Auto
Weapon Property BIP_BarrowBlade Auto
Weapon Property BIP_BarrowBladeAwake Auto
MiscObject Property DragonBone Auto
MiscObject Property DragonScales Auto

Function Accept()
	If BIP_State.GetValueInt() != 0 || BIP_Main.IsRunning()
		Return
	EndIf
	If BIP_Main.Start()
		BIP_State.SetValue(1)
		(BIP_Main as BIP_MainScript).Begin(Kjeld.GetReference())
		Debug.Trace("BIP_ questline started")
	Else
		Debug.Trace("BIP_ BIP_Main failed to start (no free Nordic crypt)")
		BIP_MsgNoStart.Show()
	EndIf
EndFunction

Function StartAwakening()
	Int st = BIP_State.GetValueInt()
	If st != 2 && st != 3
		Return
	EndIf
	Actor player = Game.GetPlayer()
	player.RemoveItem(DragonBone, 1)
	player.RemoveItem(DragonScales, 2)
	BIP_State.SetValue(5)
	BIP_AwakenReady.SetValue(0)
	RegisterForSingleUpdateGameTime(24.0)
EndFunction

Event OnUpdateGameTime()
	If BIP_State.GetValueInt() == 5
		BIP_AwakenReady.SetValue(1)
		BIP_MsgReady.Show()
	EndIf
EndEvent

Function CollectAwakened()
	If BIP_State.GetValueInt() != 5 || BIP_AwakenReady.GetValueInt() != 1
		Return
	EndIf
	Actor player = Game.GetPlayer()
	If BIP_Form.GetValueInt() == 1
		Bool worn = player.IsEquipped(BIP_BarrowPlate)
		If player.GetItemCount(BIP_BarrowPlate) > 0
			player.RemoveItem(BIP_BarrowPlate, 1, True)
		EndIf
		player.AddItem(BIP_BarrowPlateAwake, 1)
		If worn
			player.EquipItem(BIP_BarrowPlateAwake, False, True)
		EndIf
	Else
		Bool held = player.IsEquipped(BIP_BarrowBlade)
		If player.GetItemCount(BIP_BarrowBlade) > 0
			player.RemoveItem(BIP_BarrowBlade, 1, True)
		EndIf
		player.AddItem(BIP_BarrowBladeAwake, 1)
		If held
			player.EquipItem(BIP_BarrowBladeAwake, False, True)
		EndIf
	EndIf
	BIP_State.SetValue(6)
	Debug.Trace("BIP_ barrow-iron awakened")
EndFunction
