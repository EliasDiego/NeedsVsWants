using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

using NeedsVsWants.Patterns;
using NeedsVsWants.PhoneSystem;
using NeedsVsWants.CalendarSystem;

namespace NeedsVsWants.MenuSystem
{
    public class Phone : SimpleSingleton<Phone>
    {
        [SerializeField]
        InputActionReference _Point;
        [SerializeField]
        InputActionReference _CameraZoom;
        [SerializeField]
        HomeScreenMenu _HomeScreenMenu;
        [Header("Disable")]
        [SerializeField]
        Button[] _DisableButtons;
        [SerializeField]
        DayProgressor _DayProgressor;

        MenuGroup _CurrentMenuGroup;

        RectTransform _RectTransform;

        protected override void Awake() 
        {
            base.Awake();

            _RectTransform = transform as RectTransform;
        }

        void Update() 
        {
            Bounds menuBounds = new Bounds((Vector2)_RectTransform.position, _RectTransform.rect.size);

            if(menuBounds.Contains(_Point.action.ReadValue<Vector2>()))
                _CameraZoom.action.actionMap.Disable();

            else
                _CameraZoom.action.actionMap.Enable();
        }

        public void ReturnCurrentApp()
        {
            if(_CurrentMenuGroup)
            {
                if(!_CurrentMenuGroup.currentMenu.returnMenu)
                {
                    _CurrentMenuGroup.DisableGroup();

                    _CurrentMenuGroup = null;

                    _HomeScreenMenu.EnableMenu();
                }

                else
                    _CurrentMenuGroup.Return();
            }
        }

        public void ReturnHomeScreen()
        {
            if(_CurrentMenuGroup)
            {
                _CurrentMenuGroup.DisableGroup();

                _CurrentMenuGroup = null;

                _HomeScreenMenu.EnableMenu();
            }
        }

        public void ShowBackgroundApps()
        {

        }

        public void SetCurrentMenuGroup(MenuGroup menuGroup) => _CurrentMenuGroup = menuGroup;

        public void EnablePlayerControl()
        {
            foreach(Button button in _DisableButtons)
                button.interactable = true;

            _DayProgressor.Resume();
        }
        
        public void DisablePlayerControl()
        {
            foreach(Button button in _DisableButtons)
                button.interactable = false;
                
            _DayProgressor.Stop();
        }
    }
}