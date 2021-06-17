using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem;

using NeedsVsWants;

namespace NeedsVsWants.MenuSystem
{
    public class PhoneMenu : Menu
    {
        [SerializeField]
        InputActionReference _Point;
        [SerializeField]
        InputActionReference _CameraZoom;
        [SerializeField]
        HomeScreenMenu _HomeScreenMenu;

        MenuGroup _CurrentMenuGroup;

        RectTransform _RectTransform;

        void Awake() 
        {
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

        protected override void OnDisableMenu()
        {

        }

        protected override void OnEnableMenu()
        {
            
        }

        protected override void OnReturn()
        {
            
        }

        protected override void OnSwitchFrom()
        {
            
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
            // if(_CurrentMenuGroup)
            // {
            //     Menu returnMenu = _CurrentMenuGroup.Return();

            //     if(!returnMenu) // If current group return doesn't have a return menu, thus go to home screen
            //     {
            //         _CurrentMenuGroup.DisableGroup();

            //         _CurrentMenuGroup = null;

            //         _HomeScreenMenu.EnableMenu();
            //     }
            // }
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
    }
}