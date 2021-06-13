using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using NeedsVsWants.Patterns;

using TMPro;

namespace NeedsVsWants.MessagingSystem
{
    public class ChatChoicesHolder : MonoBehaviour
    {
        [SerializeField]
        Transform _ButtonList;

        TMP_Text _Text;
        
        ChatChoiceButton[] chatChoiceButtons;

        void Awake() 
        {
            _Text = GetComponentInChildren<TMP_Text>();
        }

        public void ShowChoice(string choiceName)
        {
            _Text.text = "You chose " + choiceName + ".";
            
            _ButtonList.gameObject.SetActive(false);
        }

        public void AssignChoices(ChatChoice[] choices, System.Action<int> onClickChoice)
        {
            ChatChoiceButton chatChoiceButton;
            
            _Text.text = "Choices";
            
            _ButtonList.gameObject.SetActive(true);

            //onClickChoice += () => { Debug.Log("test"); };

            for(int i = 0; i < choices.Length; i++)
            {
                chatChoiceButton = ObjectPoolManager.instance.GetObject("Chat Choice Button").GetComponent<ChatChoiceButton>();

                chatChoiceButton.AssignChoice(choices[i].name, i, this, onClickChoice);
                chatChoiceButton.transform.SetParent(_ButtonList, false);
            }
        }
    }
}