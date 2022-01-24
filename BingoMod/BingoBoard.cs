using BingoMod.Canvas;
using UnityEngine;

namespace BingoMod
{
    public static class BingoBoard
    {
        private static CanvasPanel panel;

        private static void ToggleButtonGoal(string name)
        {
            int id = int.Parse(name.Substring(11));
            BingoMod.LoadedInstance.Log(id);
            BingoMod.LoadedInstance.SetGoalStatus(id, !BingoMod.LoadedInstance.completed.Contains(id));
        }

        public static void BuildMenu(GameObject canvas)
        {
            BingoMod.LoadedInstance.Log(canvas);
            panel = new CanvasPanel(canvas, GUIController.Instance.images["boardbg"], new Vector2(1270f, 25f), Vector2.zero, new Rect(0f, 0f, GUIController.Instance.images["boardbg"].width, GUIController.Instance.images["boardbg"].height));

            BingoMod.LoadedInstance.Log(panel);

            Rect buttonRect = new Rect(0, 0, GUIController.Instance.images["boardsquare"].width, GUIController.Instance.images["boardsquare"].height);

            //Main buttons
            for (int i = 0; i < BingoMod.HEIGHT; i++)
            {
                for (int j = 0; j < BingoMod.WIDTH; j++)
                {
                    // TODO: Make text not clip off sides
                    panel.AddButton(
                        "Objective: " + BingoMod.LoadedInstance.Board[i, j].ToString(),
                        GUIController.Instance.images["boardsquare" + (BingoMod.LoadedInstance.completed.Contains(BingoMod.LoadedInstance.Board[i, j]) ? "complete" : "")],
                        new Vector2((j % 5) * 50, (i * 65) - 25 ),
                        Vector2.zero,
                        ToggleButtonGoal,
                        buttonRect,
                        GUIController.Instance.trajanBold,
                        Objectives.OBJECTIVES[BingoMod.LoadedInstance.Board[i, j]].name,
                        7
                    );
                }
            }
        }

        public static void Update()
        {
            if (panel == null)
            {
                return;
            }

            if (GUIController.ForceHideUI())
            {
                if (panel.active)
                {
                    panel.SetActive(false, true);
                }

                return;
            } else
            {
                if (!panel.active)
                {
                    panel.SetActive(true, false);
                }
            }
            if (BingoMod.LoadedInstance.uiState == UIState.Transparent)
            {
                panel.SetOpacity(.5f);
            } else if (BingoMod.LoadedInstance.uiState == UIState.Full)
            {
                panel.SetOpacity(1f);
            }
        }

        public static void UpdateId(int id, bool completed)
        {
            string img = completed ? "boardsquarecomplete" : "boardsquare";
            panel.GetButton("Objective: " + id.ToString()).UpdateSprite(GUIController.Instance.images[img], new Rect(0f, 0f, GUIController.Instance.images[img].width, GUIController.Instance.images[img].height));
        }
    }
}