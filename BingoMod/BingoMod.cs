using Modding;
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
        public const int WIDTH = 2;
        public const int HEIGHT = 5;

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

            GUIController.Instance.BuildMenus();
        }

        public void Unload()
        {
            ModHooks.NewGameHook -= this.InitializeBoardAndGUI;

            LoadedInstance = null;
        }

        private void InitializeBoardAndGUI()
        {
            if (BoardData.completed == null || BoardData.Board == null)
            {
                completed = new List<int>(25);
                Board = new int[HEIGHT, WIDTH];
                for (int i = 0; i < HEIGHT; i++)
                {
                    for (int j = 0; j < WIDTH; j++)
                    {
                        // TODO: RANDOM SELECTION
                        Board[i, j] = j + (WIDTH * i);
                        EditObjectiveHook(j + (WIDTH * i));
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

            GUIController.Instance.BuildMenus();
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
            this.Log($"\"{Objectives.OBJECTIVES[id].name}\" completed, {HEIGHT * WIDTH - completed.Count} more objectives to go.");
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
