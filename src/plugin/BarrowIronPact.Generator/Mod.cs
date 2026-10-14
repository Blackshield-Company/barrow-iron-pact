using Modkit.Mutagen;
using Mutagen.Bethesda;
using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Skyrim;
using static Wave3Gen.Kit;
using static Wave3Gen.Vanilla;

namespace Wave3Gen;

/// <summary>The Barrow-Iron Pact: everything specific to this mod. Kit/Program/Vanilla/ItemData are shared.</summary>
internal static class ModInfo
{
    public const string PluginName = "BarrowIronPact.esp";
    public const string Description = "The Barrow-Iron Pact: ancient Nord barrow-iron, a Riverwood smith, three choices and awakenable gear.";

    const int APlayer = 0, AKjeld = 1, ACryptLoc = 2, ACryptMarker = 3, AWarden = 4, AIron = 5, ARite = 6, AWardenBoss = 7;
    const int IPlayer = 0, IMarker = 1, IKjeld = 2;

    public static void Build(Kit k)
    {
        var qIntro = k.Id("BIP_Intro");
        var qMain = k.Id("BIP_Main");

        var gState = k.Global("BIP_State", 0);      // 0 new, 1 running, 2 plate, 3 greatsword, 4 returned, 5 awakening, 6 awakened
        var gForm = k.Global("BIP_Form", 0);        // 1 plate, 2 greatsword
        var gMinLevel = k.Global("BIP_MinLevel", 14);
        var gReady = k.Global("BIP_AwakenReady", 0);
        var gGMD = k.Global("BIP_SeriesGMD", 0);    // GMD_State + 1 (3 = helped Eorlund)
        var gRWD = k.Global("BIP_SeriesRWD", 0);    // 1 = Roadwarden installed

        var smiths = k.Faction("BIP_SmithFaction", "Barrow-Smiths");
        var holds = k.List("BIP_CryptHolds", new[] { WhiterunHoldLocation, FalkreathHoldLocation, HjaalmarchHoldLocation, PaleHoldLocation, EastmarchHoldLocation, RiftHoldLocation });

        // --- Barrow-iron gear --------------------------------------------------------------------------
        var ePlate = k.ArmorEnchant("BIP_EnchBarrowPlate", "Barrow's Chill", (EnchResistFrostConstantSelf, 40), (EnchFortifyHeavyArmorConstantSelf, 20));
        var ePlateA = k.ArmorEnchant("BIP_EnchBarrowPlateAwake", "Warden's Vigil",
            (EnchResistFrostConstantSelf, 50), (EnchFortifyHeavyArmorConstantSelf, 25), (EnchFortifyHealthConstantSelf, 50));
        var eSword = k.WeaponEnchant("BIP_EnchBarrowBlade", "Barrow's Bite", 35, (EnchFrostDamageFFContact, 20, 0), (EnchAbsorbStaminaFFContact, 10, 0));
        var eSwordA = k.WeaponEnchant("BIP_EnchBarrowBladeAwake", "Warden's Wrath", 45,
            (EnchFrostDamageFFContact, 30, 0), (EnchAbsorbStaminaFFContact, 15, 0), (EnchTurnUndeadFFContact, 30, 30));
        var eTorc = k.ArmorEnchant("BIP_EnchTorc", "Ancestor's Torc", (EnchFortifyHealthConstantSelf, 30), (EnchResistFrostConstantSelf, 30));
        var plate = k.ArmorLike("Iron Armor", "BIP_BarrowPlate", T.Plate, 38, 1600, ePlate);
        var plateA = k.ArmorLike("Iron Armor", "BIP_BarrowPlateAwake", T.PlateA, 46, 3400, ePlateA);
        var sword = k.WeaponLike("Iron Greatsword", "BIP_BarrowBlade", T.Sword, 20, 1600, eSword, 2500);
        var swordA = k.WeaponLike("Iron Greatsword", "BIP_BarrowBladeAwake", T.SwordA, 24, 3400, eSwordA, 3500);
        var torc = k.Jewelry("BIP_AncestorTorc", T.Torc, false, MeshAmuletSilverEmerald, SilverAmuletEmeraldAA, 1500, eTorc);
        var regardFx = k.ValueEffect("BIP_RegardEffect", T.Regard, ActorValue.DamageResist, T.RegardDesc);
        var regard = k.Ability("BIP_AncestorRegard", T.Regard, T.RegardDesc, (regardFx, 25), (EnchFortifyStaminaConstantSelf, 25));

        var iron = k.Misc("BIP_BarrowIron", T.IronName, MeshSkull, 0, 6);
        var rite = k.Note("BIP_BurialRite", T.RiteName, T.Rite, second: true);

        var kjeld = k.Speaker("BIP_Kjeld", T.KjeldName, T.KjeldShort, EncGuardSonsM05MaleGuard, FineClothesOutfit02, smiths.FormKey, 10, 50, (K(0x013987), 1));
        var warden = k.Enemy("BIP_HollowWarden", T.WardenName, T.WardenShort, LCharDraugrBossNoDragonPriest, DraugrRace);

        var msgNoStart = k.Msg("BIP_MsgNoStart", T.NoStart);
        var msgReady = k.Msg("BIP_MsgReady", T.Ready);

        // --- Intro ----------------------------------------------------------------------------------------
        var intro = k.Quest(qIntro, "BIP_Intro", T.QuestName, true, Quest.TypeEnum.None, 50);
        intro.Aliases.Add(Player(IPlayer));
        intro.Aliases.Add(Forced(IMarker, "Camp", RiverwoodMapMarker));
        intro.Aliases.Add(Created(IKjeld, "Kjeld", kjeld.FormKey, IMarker, false, Level.Medium, QuestAlias.Flag.StoresText));
        intro.NextAliasID = 3;
        intro.VirtualMachineAdapter = Vmad.QuestAdapter(Vmad.Script("BIP_IntroScript",
                Vmad.Obj("BIP_Main", qMain), Vmad.Obj("BIP_State", gState), Vmad.Obj("BIP_Form", gForm), Vmad.Obj("BIP_AwakenReady", gReady),
                Vmad.Obj("BIP_MsgNoStart", msgNoStart), Vmad.Obj("BIP_MsgReady", msgReady), Vmad.Alias("Kjeld", qIntro, IKjeld),
                Vmad.Obj("BIP_BarrowPlate", plate.FormKey), Vmad.Obj("BIP_BarrowPlateAwake", plateA.FormKey),
                Vmad.Obj("BIP_BarrowBlade", sword.FormKey), Vmad.Obj("BIP_BarrowBladeAwake", swordA.FormKey),
                Vmad.Obj("DragonBone", DragonBone), Vmad.Obj("DragonScales", DragonScales)))
            .WithAliasScripts(qIntro, IPlayer, Vmad.Script("BIP_SeriesScript", Vmad.Obj("BIP_SeriesGMD", gGMD), Vmad.Obj("BIP_SeriesRWD", gRWD)));

        // --- Main -------------------------------------------------------------------------------------------
        var main = k.Quest(qMain, "BIP_Main", T.QuestName, false, Quest.TypeEnum.SideQuest, 60);
        Stage(main, 0, null, flags: QuestStage.Flag.StartUpStage);
        Stage(main, 10, T.Log10);
        Stage(main, 20, T.Log20);
        Stage(main, 25, T.Log25);
        Stage(main, 40, T.Log40);
        Stage(main, 50, T.Log50);
        Stage(main, 100, T.Log100, QuestLogEntry.Flag.CompleteQuest);
        Stage(main, 110, T.Log110, QuestLogEntry.Flag.CompleteQuest);
        Objective(main, 10, T.Obj10, AWarden);
        Objective(main, 20, T.Obj20, AKjeld);
        Objective(main, 21, T.Obj21);
        Objective(main, 40, T.Obj40);
        Objective(main, 50, T.Obj50, AKjeld);
        main.Aliases.Add(Player(APlayer));
        main.Aliases.Add(new QuestAlias { ID = AKjeld, Type = QuestAlias.TypeEnum.Reference, Name = "Kjeld", Flags = QuestAlias.Flag.Optional | QuestAlias.Flag.StoresText });
        main.Aliases.Add(Dungeon(ACryptLoc, "CryptLoc", holds.FormKey, LocTypeDraugrCrypt));
        main.Aliases.Add(MarkerOf(ACryptMarker, "CryptMarker", ACryptLoc));
        main.Aliases.Add(Created(AWarden, "Warden", warden.FormKey, ACryptMarker, false, Level.VeryHard, QuestAlias.Flag.StoresText));
        main.Aliases.Add(Created(AIron, "Iron", iron.FormKey, AWarden, true, Level.Easy, QuestAlias.Flag.QuestObject));
        main.Aliases.Add(Created(ARite, "Rite", rite.FormKey, AWarden, true, Level.Easy, 0));
        // 0.1.1: the crypt's boss spot (optional); the script moves the Warden there.
        main.Aliases.Add(new QuestAlias
        {
            ID = AWardenBoss, Type = QuestAlias.TypeEnum.Reference, Name = "WardenBoss",
            Flags = QuestAlias.Flag.Optional | QuestAlias.Flag.AllowDead | QuestAlias.Flag.AllowDisabled,
            Location = new LocationAliasReference { AliasID = ACryptLoc, RefType = N<ILocationReferenceTypeGetter>(RefTypeBoss) },
        });
        main.NextAliasID = (uint)main.Aliases.Count;
        main.VirtualMachineAdapter = Vmad.QuestAdapter(Vmad.Script("BIP_MainScript",
                Vmad.Alias("Warden", qMain, AWarden), Vmad.Alias("WardenBoss", qMain, AWardenBoss), Vmad.Alias("Rite", qMain, ARite),
                Vmad.Obj("BIP_IronItem", iron.FormKey), Vmad.Obj("BIP_RiteItem", rite.FormKey),
                Vmad.Alias("Kjeld", qMain, AKjeld), Vmad.Alias("Iron", qMain, AIron),
                Vmad.Obj("BIP_State", gState), Vmad.Obj("BIP_Form", gForm),
                Vmad.Obj("BIP_BarrowPlate", plate.FormKey), Vmad.Obj("BIP_BarrowBlade", sword.FormKey),
                Vmad.Obj("BIP_AncestorTorc", torc.FormKey), Vmad.Obj("BIP_AncestorRegard", regard)))
            .WithAliasScripts(qMain, AWarden, Vmad.Script("BIP_WardenAliasScript"))
            .WithAliasScripts(qMain, AIron, Vmad.Script("BIP_IronAliasScript"))
            .WithAliasScripts(qMain, ARite, Vmad.Script("BIP_RiteAliasScript"));

        // --- Dialogue -------------------------------------------------------------------------------------------
        var sKjeld = ("MaleGuard", T.KjeldName);
        var isKjeld = Cond.IsID(kjeld.FormKey);
        Condition State(int v) => GlobalIs(gState, CompareOperator.EqualTo, v);
        Condition St(CompareOperator op, int s) => StageIs(qMain, op, s);

        var bIntro = k.Id("BIP_IntroBranch");
        var tStart = k.Topic("BIP_I_Start", qIntro, bIntro, T.TopicStart, sKjeld);
        var tAccept = k.Topic("BIP_I_Accept", qIntro, bIntro, T.TopicAccept, sKjeld);
        var tDecline = k.Topic("BIP_I_Decline", qIntro, bIntro, T.TopicDecline, sKjeld);
        k.Branch(bIntro, "BIP_IntroBranch", qIntro, tStart);
        var offer = new[] { isKjeld, State(0), PlayerLevelAtLeast(gMinLevel), Cond.QuestRunning(qMain, false) };
        k.Info(tStart, "BIP_I_StartGMD", T.StartGMD, offer.Append(GlobalIs(gGMD, CompareOperator.EqualTo, 3)).ToArray(), links: new[] { tAccept, tDecline });
        k.Info(tStart, "BIP_I_StartRWD", T.StartRWD, offer.Append(GlobalIs(gRWD, CompareOperator.EqualTo, 1)).ToArray(), links: new[] { tAccept, tDecline });
        k.Info(tStart, "BIP_I_StartOffer", T.Start, offer, links: new[] { tAccept, tDecline });
        k.Info(tAccept, "BIP_I_AcceptInfo", T.Accept, new[] { isKjeld }, goodbye: true, fragment: "BIP_TIF_Accept");
        k.Info(tDecline, "BIP_I_DeclineInfo", T.Decline, new[] { isKjeld }, goodbye: true);

        // Awakening (after a forged ending), in the always-running quest
        var bWake = k.Id("BIP_WakeBranch");
        var tWake = k.Topic("BIP_I_Wake", qIntro, bWake, T.TopicWake, sKjeld);
        k.Branch(bWake, "BIP_WakeBranch", qIntro, tWake);
        var forged = GlobalIs(gState, CompareOperator.GreaterThanOrEqualTo, 2);
        var notReturned = GlobalIs(gState, CompareOperator.LessThanOrEqualTo, 3);
        k.Info(tWake, "BIP_I_WakeGo", T.Wake, new[] { isKjeld, forged, notReturned, PlayerHas(DragonBone, 1), PlayerHas(DragonScales, 2) }, goodbye: true, fragment: "BIP_TIF_Wake");
        k.Info(tWake, "BIP_I_WakeNeed", T.WakeNeed, new[] { isKjeld, forged, notReturned });
        k.Info(tWake, "BIP_I_WakeWait", T.WakeWait, new[] { isKjeld, State(5), GlobalIs(gReady, CompareOperator.EqualTo, 0) });
        k.Info(tWake, "BIP_I_WakeDone", T.WakeDone, new[] { isKjeld, State(5), GlobalIs(gReady, CompareOperator.EqualTo, 1) }, goodbye: true, fragment: "BIP_TIF_WakeCollect");

        var tStatus = k.TopTopic("BIP_K_Status", qMain, T.TopicStatus, sKjeld);
        k.Info(tStatus, "BIP_K_Status10", T.Status10, new[] { isKjeld, St(CompareOperator.EqualTo, 10) });
        // 0.1.1: unstick for saves where the Warden never had the iron (works on 0.1.0 saves).
        var tRecover = k.TopTopic("BIP_K_Recover", qMain, T.TopicRecover, sKjeld);
        k.Info(tRecover, "BIP_K_RecoverInfo", T.Recover, new[] { isKjeld, St(CompareOperator.EqualTo, 10), PlayerHas(rite.FormKey), PlayerLacks(iron.FormKey) }, goodbye: true, fragment: "BIP_TIF_Recover");
        k.Info(tStatus, "BIP_K_Status40", T.Status40, new[] { isKjeld, St(CompareOperator.EqualTo, 40) });

        var bChoice = k.Id("BIP_ChoiceBranch");
        var tIron = k.Topic("BIP_K_Iron", qMain, bChoice, T.TopicIron, sKjeld);
        var tPlate = k.Topic("BIP_K_Plate", qMain, bChoice, T.TopicPlate, sKjeld);
        var tSword = k.Topic("BIP_K_Sword", qMain, bChoice, T.TopicSword, sKjeld);
        var tReturn = k.Topic("BIP_K_Return", qMain, bChoice, T.TopicReturn, sKjeld);
        k.Branch(bChoice, "BIP_ChoiceBranch", qMain, tIron);
        var atChoice = new[] { isKjeld, St(CompareOperator.GreaterThanOrEqualTo, 20), St(CompareOperator.LessThan, 40) };
        k.Info(tIron, "BIP_K_IronRite", T.IronRite, atChoice.Append(StageIs(qMain, CompareOperator.EqualTo, 25)).ToArray(), links: new[] { tPlate, tSword, tReturn });
        k.Info(tIron, "BIP_K_IronInfo", T.Iron, atChoice, links: new[] { tPlate, tSword });
        k.Info(tPlate, "BIP_K_PlateInfo", T.PlateLine, atChoice, goodbye: true, fragment: "BIP_TIF_Plate");
        k.Info(tSword, "BIP_K_SwordInfo", T.SwordLine, atChoice, goodbye: true, fragment: "BIP_TIF_Sword");
        k.Info(tReturn, "BIP_K_ReturnInfo", T.ReturnLine, atChoice, goodbye: true, fragment: "BIP_TIF_Return");

        var tCollect = k.TopTopic("BIP_K_Collect", qMain, T.TopicCollect, sKjeld);
        k.Info(tCollect, "BIP_K_CollectInfo", T.Collect, new[] { isKjeld, St(CompareOperator.EqualTo, 50) }, goodbye: true, fragment: "BIP_TIF_Collect");
    }
}

internal static class T
{
    public const string QuestName = "The Barrow-Iron Pact";
    public const string KjeldName = "Kjeld Barrow-Smith", KjeldShort = "Kjeld";
    public const string WardenName = "Hollow Warden", WardenShort = "Warden";

    public const string TopicStart = "You don't look like a Riverwood smith.";
    public static readonly string[] Start =
    {
        "I'm not. I'm the last of the barrow-smiths. My kin forged for the dead, once, in iron that never rusts.",
        "There's a Hollow Warden in an old crypt who still guards a cache of barrow-iron. I'm too old to take it from him.",
        "Bring me the iron and I'll forge you something the world hasn't seen in a thousand years. Will you go?",
    };
    public static readonly string[] StartGMD =
    {
        "Eorlund Gray-Mane wrote to me. Said a stranger saved his forge. Said I'd know you by the look. He was right.",
        "There's a Hollow Warden in an old crypt guarding a cache of barrow-iron, the old metal my kin forged for the dead.",
        "Bring me the iron and I'll forge you something the world hasn't seen in a thousand years. Will you go?",
    };
    public static readonly string[] StartRWD =
    {
        "You've the dust of the roads on you. A roadwarden's walk. Good. The road I need walked ends underground.",
        "There's a Hollow Warden in an old crypt guarding a cache of barrow-iron, the old metal my kin forged for the dead.",
        "Bring me the iron and I'll forge you something the world hasn't seen in a thousand years. Will you go?",
    };
    public const string TopicAccept = "I'll bring you the barrow-iron.";
    public static readonly string[] Accept = { "The Warden sleeps in a crypt I've marked on your map. He'll wake when you touch the iron. Don't let him take you back down with him." };
    public const string TopicDecline = "Leave the dead their iron.";
    public static readonly string[] Decline = { "A fair answer. Maybe the wisest one. Come back if you change your mind." };

    public const string TopicStatus = "About the barrow-iron...";
    public const string TopicRecover = "The Warden had no iron on him. Only this burial rite.";
    public static readonly string[] Recover = { "No iron? Let me see that rite. Ha. It says where they laid it, under his bier. Here, I'd wager this is it. Bring it back when you're ready." };
    public static readonly string[] Status10 = { "The crypt is marked on your map. The Warden won't give it up. Nobody's ever asked him nicely." };
    public static readonly string[] Status40 = { "Two days at the forge. Barrow-iron fights the hammer." };

    public const string TopicIron = "I have the barrow-iron.";
    public static readonly string[] Iron =
    {
        "Gods, look at it. Not a fleck of rust after a thousand years.",
        "Plate or a greatsword? Barrow-iron remembers its first shape. Choose well; I can only forge it once.",
    };
    public static readonly string[] IronRite =
    {
        "Gods, look at it. And you read the rite, I can see it in your face. The iron was a pact. Payment to the dead for their watch.",
        "I can forge it into plate, or a greatsword. Or... you could let me make a torc of a sliver, and send the rest back to the Warden's kin.",
    };
    public const string TopicPlate = "Forge me plate.";
    public static readonly string[] PlateLine = { "Plate it is. Come back in two days." };
    public const string TopicSword = "Forge me a greatsword.";
    public static readonly string[] SwordLine = { "A greatsword. The old way. Come back in two days." };
    public const string TopicReturn = "Honor the pact. Return the iron to the dead.";
    public static readonly string[] ReturnLine =
    {
        "...My grandmother would have said the same. I'll take it back myself, and keep a sliver for you. Here. The ancestors will remember you.",
    };

    public const string TopicCollect = "Is it finished?";
    public static readonly string[] Collect =
    {
        "Finished. Feel the chill on it? That's the barrow, still in the metal.",
        "If you ever find a dragon's bone and two of its scales, bring them to me. The iron can be woken.",
    };

    public const string TopicWake = "About awakening the barrow-iron...";
    public static readonly string[] Wake = { "A dragon's bone and two scales. That'll wake it. Leave it with me a day." };
    public static readonly string[] WakeNeed = { "A dragon's bone and two of its scales. Nothing else burns hot enough to wake barrow-iron." };
    public static readonly string[] WakeWait = { "It's still singing on the anvil. Tomorrow." };
    public static readonly string[] WakeDone = { "Awake. Listen. Hear it hum? The Warden's watch is yours now." };

    public const string Log10 = "Kjeld Barrow-Smith, last of the barrow-smiths, asked me to take ancient barrow-iron from the Hollow Warden at <Alias=CryptLoc>.";
    public const string Log20 = "I took the barrow-iron from the Hollow Warden. I should bring it to Kjeld near Riverwood.";
    public const string Log25 = "The Warden carried a burial rite. The barrow-iron was payment to the dead, part of an old pact.";
    public const string Log40 = "Kjeld is forging the barrow-iron. He needs two days.";
    public const string Log50 = "The barrow-iron should be forged.";
    public const string Log100 = "Kjeld forged the barrow-iron for me. He says a dragon's bone and two of its scales could awaken it.";
    public const string Log110 = "I honored the pact. Kjeld returned the barrow-iron to the dead and made me a torc from a sliver of it.";

    public const string Obj10 = "Take the barrow-iron from the Hollow Warden at <Alias=CryptLoc>";
    public const string Obj20 = "Bring the barrow-iron to Kjeld near Riverwood";
    public const string Obj21 = "(Optional) Read the Warden's burial rite";
    public const string Obj40 = "Wait two days for Kjeld to forge the barrow-iron";
    public const string Obj50 = "Collect the barrow-iron from Kjeld";

    public const string IronName = "Barrow-Iron Skull Plate"; // vanilla skull mesh: the iron was hammered over the skull of the first warden
    public const string RiteName = "Warden's Burial Rite";
    public const string Rite = "Let the iron lie with the watcher.\nLet the watcher keep the door.\nThe iron is the pact; the pact is the watch.\nTake the iron and the watch is broken; return the iron and the watch is kept.\n\n(Carved by many hands, in old Nordic.)";
    public const string Plate = "Barrow-Iron Plate";
    public const string PlateA = "Awakened Barrow-Iron Plate";
    public const string Sword = "Barrow-Iron Greatsword";
    public const string SwordA = "Awakened Barrow-Iron Greatsword";
    public const string Torc = "Ancestor's Torc";
    public const string Regard = "Ancestor's Regard";
    public const string RegardDesc = "You honored the barrow-pact. Armor rating and Stamina are increased.";
    public const string NoStart = "Kjeld shakes his head. \"The barrows are restless. Another day.\"\n\n(The questline could not start: a free Nordic crypt is needed. Nothing has changed; try again later.)";
    public const string Ready = "Kjeld should have finished awakening the barrow-iron.";
}
