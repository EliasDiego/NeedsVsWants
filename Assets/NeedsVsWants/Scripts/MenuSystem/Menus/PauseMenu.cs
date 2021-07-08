using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

using NeedsVsWants;
using NeedsVsWants.Player;
using NeedsVsWants.CalendarSystem;

namespace NeedsVsWants.MenuSystem
{
    public class PauseMenu : Menu
    {
        [SerializeField]
        InputActionReference _PauseKey;
        [SerializeField]
        DayProgressor _DayProgressor;

        System.Action<InputAction.CallbackContext> _OnPauseStarted;

        bool _IsOnFocus = true;

        void Awake() 
        {
            _PauseKey.action.actionMap.Enable();

            _PauseKey.action.started += _OnPauseStarted = inputValue =>
            {
                if(!_IsOnFocus)
                    return;

                if(isActive)
                    DisableMenu();

                else
                    EnableMenu();
            };
        }

        void OnDestroy() 
        {
            _PauseKey.action.started -= _OnPauseStarted;    
        }

        protected override void OnDisableMenu()
        {
            transform.SetActiveChildren(false);

            if(_IsOnFocus)
                Time.timeScale = 1; //_DayProgressor.Unpause();
        }

        protected override void OnEnableMenu()
        {
            transform.SetActiveChildren(true);

            if(_IsOnFocus)
                Time.timeScale = 0; //_DayProgressor.Pause();
        }

        async protected override void OnReturn()
        {
            await Task.Delay(1);

            _IsOnFocus = true;
        }

        protected override void OnSwitchFrom()
        {
            _IsOnFocus = false;
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