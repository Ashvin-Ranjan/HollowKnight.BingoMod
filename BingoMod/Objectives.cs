using System.Collections.Generic;

namespace BingoMod
{
    // Bingo Objective class
    public struct BingoObjective
    {
        public int id;
        public string name;
        public int[] incompatible;
    };

    // Contains all Objectives and the list for them
    public class Objectives
    {
        public static BingoObjective CreateObjective(int id, string name, int[] incompatible = null)
        {
            BingoObjective newObjective = new BingoObjective();
            newObjective.id = id;
            newObjective.name = name;
            newObjective.incompatible = incompatible == null ? new int[0] : incompatible;
            return newObjective;
        }

        public static readonly BingoObjective SWING_NAIL              = CreateObjective(0, "Swing your nail");
        public static readonly BingoObjective KILL_MIMIC              = CreateObjective(1, "Kill all grub mimics");
        public static readonly BingoObjective RECORD_EGG_SAC          = CreateObjective(2, "Kill a bluggsac");
        public static readonly BingoObjective BREAK_BALDUR_SHELL      = CreateObjective(3, "Break the Baldur Shell");
        public static readonly BingoObjective GIVE_ORO_FLOWER         = CreateObjective(4, "Give Oro the delicate flower");
        public static readonly BingoObjective MENDER_HOUSE_KEY        = CreateObjective(5, "Get menderbug's house key");
        public static readonly BingoObjective FINAL_GRUB_REWARD       = CreateObjective(6, "Get all grub rewards");
        public static readonly BingoObjective PET_MOSS_CHARGER        = CreateObjective(7, "Pet the Massive Moss Charger");
        public static readonly BingoObjective KILL_PACIFIED_MANTIS    = CreateObjective(8, "Kill a mantis after its been pacified");
        public static readonly BingoObjective DESTROY_BACKER_MESSAGES = CreateObjective(9, "Destroy all beliver tablets");
        public static readonly BingoObjective KILL_MAGGOTS            = CreateObjective(10, "Kill the maggots");
        public static readonly BingoObjective GET_SCAMMED             = CreateObjective(11, "Get scammed");
        public static readonly BingoObjective FOUNTAIN_2999           = CreateObjective(12, "Drop 2999 geo into the fountain");
        public static readonly BingoObjective ALL_ARCANE_EGGS         = CreateObjective(13, "Sell all arcane eggs");
        public static readonly BingoObjective UNMASK_MASK_MAKER       = CreateObjective(14, "Unmask the mask maker");
        public static readonly BingoObjective COMPLETE_FLOWER_QUEST   = CreateObjective(15, "Complete flower quest");
        public static readonly BingoObjective FRAGILE_TO_UNBREAKABLE  = CreateObjective(16, "Get an unbreakable charm");
        public static readonly BingoObjective DIE_AT_69               = CreateObjective(17, "Die with 69 geo");
        public static readonly BingoObjective GET_10_EGGS             = CreateObjective(18, "Get 10 rancid eggs");
        public static readonly BingoObjective KILL_NAILSMITH          = CreateObjective(19, "Kill the nailsmith", new int[] { 20 });
        public static readonly BingoObjective SAVE_NAILSMITH          = CreateObjective(20, "Save the nailsmith", new int[] { 19 });


        public static readonly Dictionary<int, BingoObjective> OBJECTIVES = new Dictionary<int, BingoObjective>()
        {
            {SWING_NAIL.id,              SWING_NAIL},
            {KILL_MIMIC.id,              KILL_MIMIC},
            {RECORD_EGG_SAC.id,          RECORD_EGG_SAC},
            {BREAK_BALDUR_SHELL.id,      BREAK_BALDUR_SHELL},
            {GIVE_ORO_FLOWER.id,         GIVE_ORO_FLOWER},
            {MENDER_HOUSE_KEY.id,        MENDER_HOUSE_KEY},
            {FINAL_GRUB_REWARD.id,       FINAL_GRUB_REWARD},
            {PET_MOSS_CHARGER.id,        PET_MOSS_CHARGER},
            {KILL_PACIFIED_MANTIS.id,    KILL_PACIFIED_MANTIS},
            {DESTROY_BACKER_MESSAGES.id, DESTROY_BACKER_MESSAGES},
            {KILL_MAGGOTS.id,            KILL_MAGGOTS},
            {GET_SCAMMED.id,             GET_SCAMMED},
            {FOUNTAIN_2999.id,           FOUNTAIN_2999},
            {ALL_ARCANE_EGGS.id,         ALL_ARCANE_EGGS},
            {UNMASK_MASK_MAKER.id,       UNMASK_MASK_MAKER},
            {COMPLETE_FLOWER_QUEST.id,   COMPLETE_FLOWER_QUEST},
            {FRAGILE_TO_UNBREAKABLE.id,  FRAGILE_TO_UNBREAKABLE},
            {DIE_AT_69.id,               DIE_AT_69},
            {GET_10_EGGS.id,             GET_10_EGGS},
            {KILL_NAILSMITH.id,          KILL_NAILSMITH},
            {SAVE_NAILSMITH.id,          SAVE_NAILSMITH},
        };
    }
}