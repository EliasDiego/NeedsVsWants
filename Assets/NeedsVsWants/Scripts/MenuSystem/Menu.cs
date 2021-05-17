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
    [RequireComponent(typeof(AudioSource))]
    public abstract class Menu : MonoBehaviour
    {
        [SerializeField]
        bool _ActiveOnStart = false;

        AudioSource _AudioSource;

        Menu _ReturnMenu;

        bool _IsActive = false;

        public bool isActive => _IsActive;

        protected AudioSource audioSource => _AudioSource;

        public static Menu _CurrentMenu;

        public static Menu current => _CurrentMenu;

        #if UNITY_EDITOR
        [CustomEditor(typeof(Menu), true)]    
        public class MenuCustomEditor : Editor
        {
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

        protected virtual void Start()
        {
            _AudioSource = GetComponent<AudioSource>();

            if(_ActiveOnStart)
            {
                // _InputHandler.Enable("Menu");
                // _InputHandler.Disable("Game");

                if(!_CurrentMenu)
                    EnableMenu();
            }

            else    
                DisableMenu();
        }

        protected abstract void OnEnableMenu();
        protected abstract void OnDisableMenu();
        protected abstract void OnReturn();

        public virtual void EnableMenu()
        {
            _IsActive = true;

            OnEnableMenu();

            _CurrentMenu = this;
        }

        public virtual void DisableMenu()
        {
            _IsActive = false;
            
            OnDisableMenu();
            
            _CurrentMenu = null;
        }

        public virtual void Return() // Needs to be redo for Confirmation Menus and such
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
            SetReturnMenu(null);

            if(disableMenu)
                DisableMenu();
                
            menu.SetReturnMenu(this);
            menu.EnableMenu();
        }

        public void SwitchTo(Menu menu) => SwitchTo(menu, true);
    }
}