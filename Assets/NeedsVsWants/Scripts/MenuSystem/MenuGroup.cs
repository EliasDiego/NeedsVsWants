using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeedsVsWants.MenuSystem
{
    public class MenuGroup : MonoBehaviour
    {
        Menu _CurrentMenu;

        Menu[] _Menus;

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

        public Menu Return()
        {
            Menu returnMenu = _CurrentMenu.returnMenu;
            
            if(returnMenu)
            {
                _CurrentMenu.Return();

                _CurrentMenu = returnMenu;
            }

            return returnMenu;
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