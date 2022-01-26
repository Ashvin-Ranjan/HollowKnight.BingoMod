using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using Modding;

namespace BingoMod
{
	public class GUIController : MonoBehaviour
	{
        public Font trajanBold;
        public Font trajanNormal;
        public Font arial;
        public Dictionary<string, Texture2D> images = new Dictionary<string, Texture2D>();

        public GameObject canvas;
        private static GUIController _instance;

        /// <summary>
        /// If this returns true, all BingoMod UI elements will be hidden.
        /// </summary>
        public static bool ForceHideUI()
        {
            return GameManager.instance.IsNonGameplayScene() || GameManager.instance.sceneName == "Menu_Title" || BingoMod.LoadedInstance.uiState == UIState.Hidden; // Show UI in cutscenes
        }

        public void BuildMenus()
        {
            LoadResources();

            canvas = new GameObject();
            canvas.AddComponent<UnityEngine.Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
            CanvasScaler scaler = canvas.AddComponent<CanvasScaler>();
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(1920f, 1080f);
            canvas.AddComponent<GraphicRaycaster>();

            ModHooks.NewGameHook += () => BingoBoard.BuildMenu(canvas);
            ModHooks.AfterSavegameLoadHook += _ => BingoBoard.BuildMenu(canvas);

            DontDestroyOnLoad(canvas);
        }

        private void LoadResources()
        {
            foreach (Font f in Resources.FindObjectsOfTypeAll<Font>())
            {
                if (f != null && f.name == "TrajanPro-Bold")
                {
                    trajanBold = f;
                }

                if (f != null && f.name == "TrajanPro-Regular")
                {
                    trajanNormal = f;
                }

                //Just in case for some reason the computer doesn't have arial
                if (f != null && f.name == "Perpetua")
                {
                    arial = f;
                }

                foreach (string font in Font.GetOSInstalledFontNames())
                {
                    if (font.ToLower().Contains("arial"))
                    {
                        arial = Font.CreateDynamicFontFromOSFont(font, 13);
                        break;
                    }
                }
            }

            if (trajanBold == null || trajanNormal == null || arial == null) BingoMod.LoadedInstance.LogError("Could not find game fonts");

            string[] resourceNames = Assembly.GetExecutingAssembly().GetManifestResourceNames();

            foreach (string res in resourceNames)
            {
                if (res.StartsWith("BingoMod.Images."))
                {
                    try
                    {
                        Stream imageStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(res);
                        byte[] buffer = new byte[imageStream.Length];
                        imageStream.Read(buffer, 0, buffer.Length);

                        Texture2D tex = new Texture2D(1, 1);
                        tex.LoadImage(buffer.ToArray());

                        string[] split = res.Split('.');
                        string internalName = split[split.Length - 2];
                        images.Add(internalName, tex);

                        BingoMod.LoadedInstance.Log("Loaded image: " + internalName);
                    }
                    catch (Exception e)
                    {
                        // TODO: Stop it from trying to load the images twice
                        BingoMod.LoadedInstance.LogError("Failed to load image: " + res + "\n" + e.ToString());
                    }
                }
            }
        }

        public void Update()
        {
            if (GameManager.instance == null) return;

            BingoBoard.Update();

            if (GameManager.instance.sceneName == "Menu_Title") return;
        }

        public static GUIController Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = UnityEngine.Object.FindObjectOfType<GUIController>();
                    if (_instance == null)
                    {
                        BingoMod.LoadedInstance.LogWarn("[BINGO MOD] Couldn't find GUIController");

                        GameObject GUIObj = new GameObject();
                        _instance = GUIObj.AddComponent<GUIController>();
                        DontDestroyOnLoad(GUIObj);
                    }
                }
                return _instance;
            }
            set
            {
            }
        }
    }

}