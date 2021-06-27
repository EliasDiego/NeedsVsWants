using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeedsVsWants.MenuSystem
{
    public class MenuGroup : MonoBehaviour
    {
        Menu _CurrentMenu;

        Menu[] _Menus;

        public bool isActive => _CurrentMenu ? _CurrentMenu.isActive : false;

        public Menu currentMenu => _CurrentMenu;
        public Menu[] menus => _Menus;

        void Start() 
        {
            _Menus = GetComponentsInChildren<Menu>();

            if(_Menus.Length >= 1)
                _CurrentMenu = _Menus[0];
        }

        protected virtual void OnEnableGroup() { }

        protected virtual void OnDisableGroup() { }

        public void SwitchTo(Menu menu)
        {
            _CurrentMenu.SwitchTo(menu);

            _CurrentMenu = menu;
        }

        public void Return()
        {
            Menu returnMenu = _CurrentMenu.returnMenu;
            
            if(returnMenu)
            {
                _CurrentMenu.Return();

                _CurrentMenu = returnMenu;
            }
        }

        public void ReturnToPreviousMenu<T>() where T : Menu
        {
            Menu newCurrentMenu = _CurrentMenu;

            do
            {
                if(newCurrentMenu.returnMenu)
                    newCurrentMenu = newCurrentMenu.returnMenu;

                else
                {
                    Debug.LogWarning("MenuWarning: ReturnToPreviousMenu<T>() stopped at " + newCurrentMenu.ToString() + "!");

                    break;
                }
            }
            while(typeof(T) != newCurrentMenu.GetType());

            newCurrentMenu.EnableMenu();

            _CurrentMenu.DisableMenu();
            _CurrentMenu.SetReturnMenu(null);

            _CurrentMenu = newCurrentMenu;
        }

        public void DisableAllMenus()
        {
            while(_CurrentMenu?.returnMenu)
                _CurrentMenu?.Return();

            OnDisableGroup();

            _CurrentMenu?.DisableMenu();
        }

        public void DisableGroup()
        {
            OnDisableGroup();

            // _CurrentMenu?.DisableMenu();
        }

        public void EnableGroup()
        {
            OnEnableGroup();

            // _CurrentMenu?.EnableMenu();
        }
    }
}