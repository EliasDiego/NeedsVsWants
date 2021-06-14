using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using NeedsVsWants;

namespace NeedsVsWants.MenuSystem
{
    public class PhoneMenu : Menu
    {

        [SerializeField]
        HomeScreenMenu _HomeScreenMenu;
        // [Header("Buttons")]
        // [SerializeField]
        // Button _HideButton;
        // [SerializeField]
        // Button _ShowButton;

        // [Header("Positions")]
        // [SerializeField]
        // float _MoveSpeed;
        // [SerializeField]
        // Vector2 _HidePosition;
        // [SerializeField]
        // Vector2 _ShowPosition;

        // bool _IsShown = false;

        // RectTransform _RectTransform;
        
        // void Awake() 
        // {
        //     _RectTransform = transform as RectTransform;

        //     _RectTransform.anchoredPosition = _HidePosition;
        // }

        // void Update() 
        // {
        //     Vector2 position = _IsShown ? _ShowPosition : _HidePosition;

        //     if(_RectTransform.anchoredPosition != position)
        //         _RectTransform.anchoredPosition = Vector2.Lerp(_RectTransform.anchoredPosition, position, _MoveSpeed * Time.deltaTime);
        // }

        MenuGroup _CurrentMenuGroup;

        protected override void OnDisableMenu()
        {
            // _ShowButton.gameObject.SetActive(true);
            // _HideButton.gameObject.SetActive(false);

            // _IsShown = false;
        }

        protected override void OnEnableMenu()
        {
            // _ShowButton.gameObject.SetActive(false);
            // _HideButton.gameObject.SetActive(true);

            // _IsShown = true;
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
                Menu returnMenu = _CurrentMenuGroup.Return();

                if(!returnMenu) // If current group return doesn't have a return menu, thus go to home screen
                {
                    _CurrentMenuGroup.DisableGroup();

                    _CurrentMenuGroup = null;

                    _HomeScreenMenu.EnableMenu();
                }
            }
            // Menu menu = Menu.current;

            // if(menu)
            // {
            //     if(menu.GetType() != typeof(HomeScreenMenu))
            //     {
            //         if(menu.returnMenu)
            //             menu.Return();
                    
            //         else
            //         {
            //             menu.DisableMenu();

            //             _HomeScreenMenu.EnableMenu();
            //         }
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