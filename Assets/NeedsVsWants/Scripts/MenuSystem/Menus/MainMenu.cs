using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

using NeedsVsWants.Player;

namespace NeedsVsWants.MenuSystem
{
    public class MainMenu : Menu
    {
        PlayerStat _PlayerStat;

        protected override void Start() 
        {
            base.Start();
        }

        protected override void OnDisableMenu()
        {
            transform.SetActiveChildren(false);
        }

        protected override void OnEnableMenu()
        {
            transform.SetActiveChildren(true);
        }

        protected override void OnReturn()
        {
            
        }
        
        protected override void OnSwitchFrom()
        {
            
        }

        public void NewGame(int levelIndex)
        {
            SceneManager.LoadScene(levelIndex, LoadSceneMode.Single);

            // Create New File
            PlayerStat.CreateNewInstance();
        }

        public void Quit()
        {
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }
    }
}