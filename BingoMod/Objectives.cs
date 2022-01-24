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

        public static readonly BingoObjective SWING_NAIL = CreateObjective(0, "Swing your nail");
        public static readonly BingoObjective KILL_MIMIC = CreateObjective(1, "Kill all grub mimics");
        public static readonly BingoObjective RECORD_EGG_SAC = CreateObjective(2, "Kill a bluggsac");
        public static readonly BingoObjective BREAK_BALDUR_SHELL = CreateObjective(3, "Break the Baldur Shell");
        public static readonly BingoObjective GIVE_ORO_FLOWER = CreateObjective(4, "Give Oro the delicate flower");
        public static readonly BingoObjective MENDER_HOUSE_KEY = CreateObjective(5, "Get menderbug's house key");
        public static readonly BingoObjective FINAL_GRUB_REWARD = CreateObjective(6, "Get all grub rewards");
        public static readonly BingoObjective PET_MOSS_CHARGER = CreateObjective(7, "Pet the Massive Moss Charger");
        public static readonly BingoObjective KILL_PACIFIED_MANTIS = CreateObjective(8, "Kill a mantis after its been pacified");
        public static readonly BingoObjective DESTROY_BACKER_MESSAGES = CreateObjective(9, "Destroy all beliver tablets");
        public static readonly BingoObjective KILL_MAGGOTS = CreateObjective(10, "Kill the maggots");


        public static readonly Dictionary<int, BingoObjective> OBJECTIVES = new Dictionary<int, BingoObjective>()
        {
            {SWING_NAIL.id, SWING_NAIL},
            {KILL_MIMIC.id, KILL_MIMIC},
            {RECORD_EGG_SAC.id, RECORD_EGG_SAC},
            {BREAK_BALDUR_SHELL.id, BREAK_BALDUR_SHELL},
            {GIVE_ORO_FLOWER.id, GIVE_ORO_FLOWER},
            {MENDER_HOUSE_KEY.id, MENDER_HOUSE_KEY},
            {FINAL_GRUB_REWARD.id, FINAL_GRUB_REWARD},
            {PET_MOSS_CHARGER.id, PET_MOSS_CHARGER},
            {KILL_PACIFIED_MANTIS.id, KILL_PACIFIED_MANTIS},
            {DESTROY_BACKER_MESSAGES.id, DESTROY_BACKER_MESSAGES},
            {KILL_MAGGOTS.id, KILL_MAGGOTS},
        };
    }
}