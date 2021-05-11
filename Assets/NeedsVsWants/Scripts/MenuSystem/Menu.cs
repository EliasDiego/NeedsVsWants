using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

using UnityEditor;

using TMPro;

namespace NeedsVsWants.MenuSystem
{
    public abstract class Menu : MonoBehaviour
    {
        [SerializeField]
        bool _ActiveOnStart = false;
        [SerializeField]
        Selectable _FirstSelected;

        GameObject _CurrentSelected;

        //InputHandler _InputHandler;

        Menu _ReturnMenu;

        Action<InputAction.CallbackContext> _OnReturnEvent, _OnPointEvent, _OnMoveEvent;

        UnityEngine.EventSystems.EventSystem _EventSystem;

        bool _IsActive = false;

        public bool isActive => _IsActive;

        #if UNITY_EDITOR
        [CustomEditor(typeof(Menu), true)]    
        public class MenuCustomEditor : Editor
        {
            bool _IsActiveOnAwake = false;

            public override void OnInspectorGUI()
            {
                Menu menu = target as Menu;
         
                SerializedProperty iterator = serializedObject.GetIterator();

                if(!menu)
                    return;

                EditorGUI.BeginChangeCheck();

                // Script
                iterator.NextVisible(true);

                using (new EditorGUI.DisabledScope("m_Script" == iterator.propertyPath))
                    EditorGUILayout.PropertyField(iterator, true);

                // Enable Disable Buttons
                GUILayout.BeginHorizontal();

                if(GUILayout.Button("Enable Menu"))
                    menu.transform.SetActiveChildren(true);

                if(GUILayout.Button("Disable Menu"))
                    menu.transform.SetActiveChildren(false);

                GUILayout.EndHorizontal();
         
                // Base Fields 
                EditorGUILayout.Space();

                while (iterator.NextVisible(false))
                    EditorGUILayout.PropertyField(iterator, true);

                EditorGUI.EndChangeCheck();
         
                serializedObject.ApplyModifiedProperties();
            }
        }
        #endif

        protected virtual void Awake()
        {
            //_InputHandler = Resources.Load<InputHandler>("YSInputHandler");

            _EventSystem = UnityEngine.EventSystems.EventSystem.current;

            _OnReturnEvent = context =>
            {
                Selectable selectable = _EventSystem.currentSelectedGameObject?.GetComponent<Selectable>();

                if(selectable)
                {
                    if(selectable is TMP_InputField)
                    {
                        TMP_InputField inputField = selectable as TMP_InputField;

                        if(inputField)
                        {
                            if(!inputField.isFocused)
                                Return();

                            else
                                inputField.DeactivateInputField();
                        }
                    }

                    else if(selectable is Toggle)
                    {
                        Toggle toggle = selectable as Toggle;

                        if(toggle)
                        {
                            Transform toggleParent = toggle.transform?.parent?.parent?.parent?.parent;
                            
                            if(!toggleParent)
                                Return();
                        }
                    }

                    else if(selectable is Button)
                    {
                        Button button = selectable as Button;

                        if(button)
                            Return();
                    }

                    else if(selectable is Slider)
                    {
                        Slider slider = selectable as Slider;

                        if(slider)
                            Return();
                    }

                    else
                        Return();
                }

                else
                    Return();
            };



            _OnPointEvent = context =>
            {
                if(_EventSystem.currentSelectedGameObject)
                {
                    _CurrentSelected = _EventSystem.currentSelectedGameObject;

                    _EventSystem.SetSelectedGameObject(null);
                }
            };

            _OnMoveEvent = context =>
            {
                if(!_EventSystem.currentSelectedGameObject)
                    _EventSystem.SetSelectedGameObject(_CurrentSelected);
            };

            if(_ActiveOnStart)
            {
                // _InputHandler.Enable("Menu");
                // _InputHandler.Disable("Game");

                EnableMenu();
            }

            else    
                DisableMenu();

        }

        void OnDisable() 
        {
            if(_IsActive)
                UnhookEvents();
        }

        void HookEvents()
        {
            // _InputHandler["Menu", "Return"].started += _OnReturnEvent;
            // _InputHandler["Menu", "Point"].performed += _OnPointEvent;
            // _InputHandler["Menu", "Move"].started += _OnMoveEvent;

        }

        void UnhookEvents()
        {
            // _InputHandler["Menu", "Return"].started -= _OnReturnEvent;
            // _InputHandler["Menu", "Point"].performed -= _OnPointEvent;
            // _InputHandler["Menu", "Move"].started -= _OnMoveEvent;
        }

        protected abstract void OnEnableMenu();
        protected abstract void OnDisableMenu();
        protected abstract void OnReturn();

        public virtual void EnableMenu()
        {
            _IsActive = true;

            _EventSystem.SetSelectedGameObject(null);

            _CurrentSelected = _FirstSelected.gameObject;

            OnEnableMenu();
            
            HookEvents();
        }

        public virtual void DisableMenu()
        {
            _IsActive = false;

            _EventSystem.SetSelectedGameObject(null);
            
            _CurrentSelected = null;

            OnDisableMenu();
            
            UnhookEvents();
        }

        public virtual void Return()
        {
            if(_ReturnMenu)
            {
                _ReturnMenu.EnableMenu();
                
                OnReturn();

                DisableMenu();
            }

            else
                Debug.LogWarning("MenuWarning: ReturnMenu is Null!");
        }

        public void SetReturnMenu(Menu menu) => _ReturnMenu = menu;

        /// <summary>
        /// ##UNTESTED##
        /// Returns back to the intendend Menu through Previos Menus.
        /// </summary>
        public void ReturnToPreviousMenu<T>() where T : Menu
        {
            Menu currentMenu = this;

            do
            {
                if(currentMenu._ReturnMenu)
                    currentMenu = currentMenu._ReturnMenu;

                else
                {
                    Debug.LogWarning("MenuWarning: ReturnToPreviousMenu<T>() stopped at " + currentMenu.ToString() + "!");

                    break;
                }
            }
            while(typeof(T) != currentMenu.GetType());

            currentMenu.EnableMenu();

            DisableMenu();
            
            SetReturnMenu(null);
        }

        /// <summary>
        /// ##UNTESTED##
        /// Returns back to the intendend Menu through Previos Menus.
        /// </summary>
        public void ReturnToPreviousMenu(int numToMenu)
        {
            Menu currentMenu = this;

            for(int i = 0; i < numToMenu; i++)
            {
                if(currentMenu._ReturnMenu)
                    currentMenu = currentMenu._ReturnMenu;

                else
                {
                    Debug.LogWarning("MenuWarning: ReturnToPreviousMenu(int) stopped at " + currentMenu.ToString() + "!");

                    break;
                }
            }

            currentMenu.EnableMenu();

            DisableMenu();
            
            SetReturnMenu(null);
        }

        /// <summary>
        /// ##BROKEN##
        /// Returns back to the intendend Menu through Previos Menus.
        /// </summary>
        public void ReturnToPreviousMenu(Menu menu)
        {
            Menu currentMenu = this;
            Menu tempMenu;

            do
            {
                Debug.Log(currentMenu);
                if(currentMenu._ReturnMenu)
                {
                    tempMenu = currentMenu._ReturnMenu;

                    currentMenu.SetReturnMenu(null);

                    currentMenu = tempMenu;
                }

                else
                {
                    Debug.LogWarning("MenuWarning: ReturnToPreviousMenu<T>() stopped at " + currentMenu.ToString() + "!");

                    break;
                }
            }
            while(menu != currentMenu);

            currentMenu.EnableMenu();
            
            DisableMenu();
        }

        public void SwitchTo(Menu menu, bool disableMenu)
        {
            menu.SetReturnMenu(this);
            menu.EnableMenu();

            SetReturnMenu(null);

            if(disableMenu)
                DisableMenu();
        }

        public void SwitchTo(Menu menu) => SwitchTo(menu, true);
    }
}