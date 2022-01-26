using Modding;
using System;
using System.Reflection;
using System.Collections.Generic;

namespace BingoMod
{
    public enum UIState
    {
        Hidden,
        Transparent,
        Full
    }
    public class BingoMod : Mod, IMenuMod, ILocalSettings<BoardSaveData>
    {
        public bool ToggleButtonInsideMenu => false;
        public const int WIDTH = 5;
        public const int HEIGHT = 4;

        public static BingoMod LoadedInstance { get; set; }

        public BoardSaveData BoardData = new BoardSaveData();
        public void OnLoadLocal(BoardSaveData s) => this.BoardData = s;
        public BoardSaveData OnSaveLocal() => this.BoardData;

        // Bingo Board
        public int[,] Board { get; set; }

        public List<int> completed;

        public UIState uiState = UIState.Full;

        public override string GetVersion() => Assembly.GetExecutingAssembly().GetName().Version.ToString();

        public override void Initialize()
        {
            if (BingoMod.LoadedInstance != null) return;
            BingoMod.LoadedInstance = this;

            this.Log("Initializing Mod");

            ModHooks.NewGameHook += this.InitializeBoardAndGUI;
            ModHooks.AfterSavegameLoadHook += OnLoadInitializeBoardAndGUI;

            GUIController.Instance.BuildMenus();
        }

        public void Unload()
        {
            ModHooks.NewGameHook -= this.InitializeBoardAndGUI;
            ModHooks.AfterSavegameLoadHook -= OnLoadInitializeBoardAndGUI;

            LoadedInstance = null;
        }

        private void OnLoadInitializeBoardAndGUI(SaveGameData _)
        {
            this.InitializeBoardAndGUI();
        }

        private void InitializeBoardAndGUI()
        {
            if (BoardData.completed == null || BoardData.Board == null)
            {
                // Make the version as random as possiable
                int seed = Environment.TickCount * (Environment.Version.Minor + 1) * (Environment.MachineName.Length + Environment.OSVersion.Version.Minor + 1);
                Random rand = new Random(seed);
                Log($"Board generated with seed: {seed}");
                List<int> objectivesLeft = new List<int>(Objectives.OBJECTIVES.Keys);
                completed = new List<int>(25);
                Board = new int[HEIGHT, WIDTH];
                for (int i = 0; i < HEIGHT; i++)
                {
                    for (int j = 0; j < WIDTH; j++)
                    {
                        int objective = 0;
                        if (objectivesLeft.Count == 0)
                        {
                            LogError("Objectives ran out! Defaulting to 0");
                        } else
                        {
                            int index = rand.Next(objectivesLeft.Count);
                            objective = objectivesLeft[index];
                            objectivesLeft.RemoveAt(index);
                            foreach (int exclude in Objectives.OBJECTIVES[objective].incompatible)
                            {
                                objectivesLeft.Remove(exclude);
                            }
                        }
                        Board[i, j] = objective;
                        EditObjectiveHook(objective);
                    }
                }
                BoardData.completed = completed;
                BoardData.Board = Board;
            } else
            {
                completed = BoardData.completed;
                Board = BoardData.Board;
            }

            var result = "{";
            for (int i = 0; i < Board.GetLength(0); i++)
            {
                if (i > 0) result += ", ";
                result += '{';
                for (int j = 0; j < Board.GetLength(1); j++)
                {
                    if (j > 0) result += ", ";
                    result += Board[i, j];
                }
                result += '}';
            }
            result += "}";


            this.Log("Board initialized!");
            this.Log($"Initialized as {result}");
        }

        public void SetGoalStatus(int id, bool complete = true)
        {
            if (complete)
            {
                completed.Add(id);
            } else
            {
                completed.Remove(id);
            }
            EditObjectiveHook(id, !complete);
            BoardData.completed = completed;
            BoardData.Board = Board;
            BingoBoard.UpdateId(id, complete);
            this.Log($"\"{Objectives.OBJECTIVES[id].name}\" {(complete ? "" : "un")}completed, {HEIGHT * WIDTH - completed.Count} more objectives to go.");
            if (HEIGHT * WIDTH == completed.Count) this.Log("Board complete");
        }

        public static void EditObjectiveHook(int id, bool add = true)
        {
            switch (id)
            {
                // SWING_NAIL
                case 0:
                    if (add)
                        ModHooks.AttackHook += ObjectiveHandlers.SwingNail;
                    else
                        ModHooks.AttackHook -= ObjectiveHandlers.SwingNail;
                    break;

                // KILL_MIMIC
                case 1:
                    if (add)
                        ModHooks.SetPlayerIntHook += ObjectiveHandlers.MimicKill;
                    else
                        ModHooks.SetPlayerIntHook -= ObjectiveHandlers.MimicKill;
                    break;

                // RECORD_EGG_SAC
                case 2:
                    if (add)
                        ModHooks.RecordKillForJournalHook += ObjectiveHandlers.EggSacJournal;
                    else
                        ModHooks.RecordKillForJournalHook -= ObjectiveHandlers.EggSacJournal;
                    break;

                // BREAK_BALDUR_SHELL
                case 3:
                    if (add)
                        ModHooks.SetPlayerIntHook += ObjectiveHandlers.CheckShellBreak;
                    else
                        ModHooks.SetPlayerIntHook -= ObjectiveHandlers.CheckShellBreak;
                    break;

                // GIVE_ORO_FLOWER
                case 4:
                    if (add)
                        ModHooks.SetPlayerBoolHook += ObjectiveHandlers.CheckOroFlower;
                    else
                        ModHooks.SetPlayerBoolHook -= ObjectiveHandlers.CheckOroFlower;
                    break;

                // MENDER_HOUSE_KEY
                case 5:
                    if (add)
                        ModHooks.SetPlayerBoolHook += ObjectiveHandlers.CheckMenderbugKey;
                    else
                        ModHooks.SetPlayerBoolHook -= ObjectiveHandlers.CheckMenderbugKey;
                    break;

                // FINAL_GRUB_REWARD
                case 6:
                    if (add)
                        ModHooks.SetPlayerBoolHook += ObjectiveHandlers.FinalGrubReward;
                    else
                        ModHooks.SetPlayerBoolHook -= ObjectiveHandlers.FinalGrubReward;
                    break;

                // PET_MOSS_CHARGER
                case 7:
                    if (add)
                        ModHooks.SetPlayerBoolHook += ObjectiveHandlers.KillMossCharger;
                    else
                        ModHooks.SetPlayerBoolHook -= ObjectiveHandlers.KillMossCharger;
                    break;

                // KILL_PACIFIED_MANTIS
                case 8:
                    if (add)
                        ModHooks.SetPlayerIntHook += ObjectiveHandlers.KillPacifiedMantis;
                    else
                        ModHooks.SetPlayerIntHook -= ObjectiveHandlers.KillPacifiedMantis;
                    break;

                // DESTROY_BACKER_MESSAGES
                case 9:
                    if (add)
                        ModHooks.SetPlayerBoolHook += ObjectiveHandlers.DestroyedAllTablets;
                    else
                        ModHooks.SetPlayerBoolHook -= ObjectiveHandlers.DestroyedAllTablets;
                    break;

                // KILL_MAGGOTS
                case 10:
                    if (add)
                        ModHooks.RecordKillForJournalHook += ObjectiveHandlers.MaggotKill;
                    else
                        ModHooks.RecordKillForJournalHook -= ObjectiveHandlers.MaggotKill;
                    break;

                // GET_SCAMMED
                case 11:
                    if (add)
                        ModHooks.SetPlayerBoolHook += ObjectiveHandlers.ScammedCheck;
                    else
                        ModHooks.SetPlayerBoolHook -= ObjectiveHandlers.ScammedCheck;
                    break;

                // FOUNTAIN_2999
                case 12:
                    if (add)
                        ModHooks.SetPlayerIntHook += ObjectiveHandlers.FountainGeoCheck;
                    else
                        ModHooks.SetPlayerIntHook -= ObjectiveHandlers.FountainGeoCheck;
                    break;

                // ALL_ARCANE_EGGS
                case 13:
                    if (add)
                        ModHooks.SetPlayerIntHook += ObjectiveHandlers.SellAllEggs;
                    else
                        ModHooks.SetPlayerIntHook -= ObjectiveHandlers.SellAllEggs;
                    break;

                // UNMASK_MASK_MAKER
                case 14:
                    if (add)
                        ModHooks.SetPlayerBoolHook += ObjectiveHandlers.MaskMakerUnmasked;
                    else
                        ModHooks.SetPlayerBoolHook -= ObjectiveHandlers.MaskMakerUnmasked;
                    break;

                // COMPLETE_FLOWER_QUEST
                case 15:
                    if (add)
                        ModHooks.SetPlayerBoolHook += ObjectiveHandlers.CheckFlowerQuest;
                    else
                        ModHooks.SetPlayerBoolHook -= ObjectiveHandlers.CheckFlowerQuest;
                    break;

                // FRAGILE_TO_UNBREAKABLE
                case 16:
                    if (add)
                        ModHooks.SetPlayerBoolHook += ObjectiveHandlers.CheckUnbreakableCharm;
                    else
                        ModHooks.SetPlayerBoolHook -= ObjectiveHandlers.CheckUnbreakableCharm;
                    break;

                // DIE_AT_69
                case 17:
                    if (add)
                        ModHooks.BeforePlayerDeadHook += ObjectiveHandlers.DieAt69Check;
                    else
                        ModHooks.BeforePlayerDeadHook -= ObjectiveHandlers.DieAt69Check;
                    break;

                // GET_10_EGGS
                case 18:
                    if (add)
                        ModHooks.SetPlayerIntHook += ObjectiveHandlers.Check10Eggs;
                    else
                        ModHooks.SetPlayerIntHook -= ObjectiveHandlers.Check10Eggs;
                    break;

                // KILL_NAILSMITH
                case 19:
                    if (add)
                        ModHooks.SetPlayerBoolHook += ObjectiveHandlers.KilledNailsmith;
                    else
                        ModHooks.SetPlayerBoolHook -= ObjectiveHandlers.KilledNailsmith;
                    break;

                // SAVE_NAILSMITH
                case 20:
                    if (add)
                        ModHooks.SetPlayerBoolHook += ObjectiveHandlers.SavedNailsmith;
                    else
                        ModHooks.SetPlayerBoolHook -= ObjectiveHandlers.SavedNailsmith;
                    break;

                default:
                    BingoMod.LoadedInstance.LogError($"Internal Error: Unable to find objective {id}.");
                    break;
            }
        }
        public List<IMenuMod.MenuEntry> GetMenuData(IMenuMod.MenuEntry? toggleButtonEntry)
        {
            return new List<IMenuMod.MenuEntry>
            {
                new IMenuMod.MenuEntry {
                    Name = "Board Display",
                    Values = new string[] {
                        "Opaque",
                        "Transparent",
                        "Hidden"
                    },
                    // opt will be the index of the option that has been chosen
                    Saver = opt => this.uiState = opt switch {
                        0 => UIState.Full,
                        1 => UIState.Transparent,
                        _ => UIState.Hidden
                    },
                    Loader = () => this.uiState switch
                    {
                        UIState.Full => 0,
                        UIState.Transparent => 1,
                        _ => 2,
                    }
                }
            };
        }
    }

    public class BoardSaveData
    {
        // Bingo Board
        public int[,] Board { get; set; }

        public List<int> completed;
    }
}
