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

        public void NextLevel(int levelIndex)
        {
            SceneManager.LoadScene(levelIndex, LoadSceneMode.Single);
        }
    }
}