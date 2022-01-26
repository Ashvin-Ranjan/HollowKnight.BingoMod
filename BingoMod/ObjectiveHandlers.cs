using GlobalEnums;

namespace BingoMod
{
    public class ObjectiveHandlers
    {
        public static void SwingNail(AttackDirection _)
        {
            BingoMod.LoadedInstance.SetGoalStatus(Objectives.SWING_NAIL.id);
        }

        // TODO: TEST IF THIS ACTUALLY LIKE, WORKS
        public static int MimicKill(string name, int orig)
        {
            if (name == "killsGrubMimic" && orig == 0)
            {
                BingoMod.LoadedInstance.SetGoalStatus(Objectives.KILL_MIMIC.id);
            }
            return orig;
        }

        public static void EggSacJournal(EnemyDeathEffects enemyDeathEffects, string _, string _1, string _2, string _3)
        {
            BingoMod.LoadedInstance.Log($"{enemyDeathEffects.name} added to journal");
            if (enemyDeathEffects.name.StartsWith("Egg Sac"))
            {
                BingoMod.LoadedInstance.SetGoalStatus(Objectives.RECORD_EGG_SAC.id);
            }
        }

        public static int CheckShellBreak(string name, int orig)
        {
            if (name == "blockerHits" && orig == 0)
            {
                BingoMod.LoadedInstance.SetGoalStatus(Objectives.BREAK_BALDUR_SHELL.id);
            }
            return orig;
        }

        public static bool CheckOroFlower(string name, bool orig)
        {
            if (name == "givenOroFlower" && orig)
            {
                BingoMod.LoadedInstance.SetGoalStatus(Objectives.GIVE_ORO_FLOWER.id);
            }
            return orig;
        }

        // TODO: TEST IF THIS ACTUALLY LIKE, WORKS
        public static bool CheckMenderbugKey(string name, bool orig)
        {
            if (name == "hasMenderKey" && orig)
            {
                BingoMod.LoadedInstance.SetGoalStatus(Objectives.MENDER_HOUSE_KEY.id);
            }
            return orig;
        }

        // TODO: TEST IF THIS ACTUALLY LIKE, WORKS
        public static bool FinalGrubReward(string name, bool orig)
        {
            if (name == "finalGrubRewardCollected" && orig)
            {
                BingoMod.LoadedInstance.SetGoalStatus(Objectives.FINAL_GRUB_REWARD.id);
            }
            return orig;
        }

        public static bool KillMossCharger(string name, bool orig)
        {
            if (name == "killedMegaMossCharger" && orig)
            {
                BingoMod.LoadedInstance.SetGoalStatus(Objectives.PET_MOSS_CHARGER.id);
            }
            return orig;
        }

        public static int KillPacifiedMantis(string name, int orig)
        {
            BingoMod.LoadedInstance.Log(name);
            if (name == "killsMantis" && PlayerData.instance.GetBool("killedMantisLord"))
            {
                BingoMod.LoadedInstance.SetGoalStatus(Objectives.KILL_PACIFIED_MANTIS.id);
            }
            return orig;
        }

        public static bool DestroyedAllTablets(string name, bool orig)
        {
            BingoMod.LoadedInstance.Log(name);
            if (name == "allBelieverTabletsDestroyed" && orig)
            {
                BingoMod.LoadedInstance.SetGoalStatus(Objectives.DESTROY_BACKER_MESSAGES.id);
            }
            return orig;
        }

        public static void MaggotKill(EnemyDeathEffects enemyDeathEffects, string _, string _1, string _2, string _3)
        {
            if (enemyDeathEffects.name.StartsWith("Prayer Slug"))
            {
                BingoMod.LoadedInstance.SetGoalStatus(Objectives.KILL_MAGGOTS.id);
            }
        }

        // TODO: TEST IF THIS ACTUALLY LIKE, WORKS
        public static bool ScammedCheck(string name, bool orig)
        {
            if (name == "bankerTheft" && orig)
            {
                BingoMod.LoadedInstance.SetGoalStatus(Objectives.GET_SCAMMED.id);
            }
            return orig;
        }

        // TODO: TEST IF THIS ACTUALLY LIKE, WORKS
        public static int FountainGeoCheck(string name, int orig)
        {
            if (name == "fountainGeo" && orig == 2999)
            {
                BingoMod.LoadedInstance.SetGoalStatus(Objectives.FOUNTAIN_2999.id);
            }
            return orig;
        }

        // TODO: TEST IF THIS ACTUALLY LIKE, WORKS
        public static int SellAllEggs(string name, int orig)
        {
            if (name == "soldTrinket4" && orig == 4)
            {
                BingoMod.LoadedInstance.SetGoalStatus(Objectives.ALL_ARCANE_EGGS.id);
            }
            return orig;
        }

        // TODO: TEST IF THIS ACTUALLY LIKE, WORKS
        public static bool MaskMakerUnmasked(string name, bool orig)
        {
            if (name == "maskmakerUnmasked1" && orig)
            {
                BingoMod.LoadedInstance.SetGoalStatus(Objectives.UNMASK_MASK_MAKER.id);
            }
            return orig;
        }

        // TODO: TEST IF THIS ACTUALLY LIKE, WORKS
        public static bool CheckFlowerQuest(string name, bool orig)
        {
            if (name == "xunFlowerGiven" && orig)
            {
                BingoMod.LoadedInstance.SetGoalStatus(Objectives.COMPLETE_FLOWER_QUEST.id);
            }
            return orig;
        }

        // TODO: TEST IF THIS ACTUALLY LIKE, WORKS
        public static bool CheckUnbreakableCharm(string name, bool orig)
        {
            if ((name == "fragileGreed_unbreakable" || name == "fragileHeart_unbreakable" || name == "fragileStrength_unbreakable") && orig)
            {
                BingoMod.LoadedInstance.SetGoalStatus(Objectives.FRAGILE_TO_UNBREAKABLE.id);
            }
            return orig;
        }

        // TODO: TEST IF THIS ACTUALLY LIKE, WORKS
        public static void DieAt69Check()
        {
            if (PlayerData.instance.GetInt("geo") == 69) BingoMod.LoadedInstance.SetGoalStatus(Objectives.DIE_AT_69.id);
        }

        // TODO: TEST IF THIS ACTUALLY LIKE, WORKS
        public static int Check10Eggs(string name, int orig)
        {
            if (name == "rancidEggs" && orig >= 10)
            {
                BingoMod.LoadedInstance.SetGoalStatus(Objectives.GET_10_EGGS.id);
            }
            return orig;
        }

        // TODO: TEST IF THIS ACTUALLY LIKE, WORKS
        public static bool KilledNailsmith(string name, bool orig)
        {
            if (name == "nailsmithKilled" && orig)
            {
                BingoMod.LoadedInstance.SetGoalStatus(Objectives.KILL_NAILSMITH.id);
            }
            return orig;
        }

        // TODO: TEST IF THIS ACTUALLY LIKE, WORKS
        public static bool SavedNailsmith(string name, bool orig)
        {
            if (name == "nailsmithSpared" && orig)
            {
                BingoMod.LoadedInstance.SetGoalStatus(Objectives.SAVE_NAILSMITH.id);
            }
            return orig;
        }
    }
}