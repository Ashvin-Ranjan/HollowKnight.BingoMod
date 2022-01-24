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
            if (name == "killsMantis" && PlayerData.instance.GetBool("killedMantisLord"))
            {
                BingoMod.LoadedInstance.SetGoalStatus(Objectives.KILL_PACIFIED_MANTIS.id);
            }
            return orig;
        }

        // TODO: TEST IF THIS ACTUALLY LIKE, WORKS
        public static bool DestroyedAllTablets(string name, bool orig)
        {
            if (name == "allBelieverTabletsDestroyed" && orig)
            {
                BingoMod.LoadedInstance.SetGoalStatus(Objectives.DESTROY_BACKER_MESSAGES.id);
            }
            return orig;
        }
    }
}