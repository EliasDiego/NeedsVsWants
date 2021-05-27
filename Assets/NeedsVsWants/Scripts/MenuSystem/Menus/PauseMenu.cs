using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.InputSystem;

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

        bool _IsPaused = false;

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

        protected override void OnDisableMenu()
        {
            transform.SetActiveChildren(false);

            if(_IsOnFocus)
                _DayProgressor.Unpause();
        }

        protected override void OnEnableMenu()
        {
            transform.SetActiveChildren(true);

            if(_IsOnFocus)
                _DayProgressor.Pause();
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
    }
}