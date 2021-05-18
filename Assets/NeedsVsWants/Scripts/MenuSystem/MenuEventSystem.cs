using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem.UI;
using UnityEngine.EventSystems;

using TMPro;

namespace NeedsVsWants.MenuSystem
{
    public class MenuEventSystem : MonoBehaviour
    {
        InputSystemUIInputModule _InputModule;

        EventSystem _EventSystem;

        class DropdownItemChecker : TMP_Dropdown 
        {
            public static bool ContainsComponent(GameObject gameObject) => gameObject.TryGetComponent<DropdownItem>(out DropdownItem item);
        }

        void Awake()
        {
            _InputModule = GetComponent<InputSystemUIInputModule>();
            _EventSystem = GetComponent<EventSystem>();

            _InputModule.cancel.action.started += context => 
            {
                if(Menu.current)
                {
                    if(!_EventSystem.currentSelectedGameObject || !DropdownItemChecker.ContainsComponent(_EventSystem.currentSelectedGameObject))
                        Menu.current.Return();
                }
            };
        }
    }
}