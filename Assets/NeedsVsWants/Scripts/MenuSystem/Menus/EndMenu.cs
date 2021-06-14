using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

using NeedsVsWants.Player;

namespace NeedsVsWants.MenuSystem
{
    public class EndMenu : Menu
    {
        protected override void OnDisableMenu() { }

        protected override void OnEnableMenu() { }

        protected override void OnReturn() { }

        protected override void OnSwitchFrom() { }

        public void Restart(int sceneBuildIndex)
        {
            SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Single);

            // Create New File
            PlayerStat.CreateNewInstance();
        }

        public void ReturnToMainMenu(int sceneBuildIndex)
        {
            SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Single);
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