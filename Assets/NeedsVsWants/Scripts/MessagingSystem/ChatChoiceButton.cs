using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using TMPro;

namespace NeedsVsWants.MessagingSystem
{
    public class ChatChoiceButton : Button
    {
        TMP_Text _Text;

        protected override void Awake() 
        {
            _Text = GetComponentInChildren<TMP_Text>();    
        }

        public void AssignChoice(string choiceName, int choiceIndex, ChatChoicesHolder chatChoicesHolder, System.Action<int> onClickChoice)
        {
            _Text.text = choiceName;

            onClick.RemoveAllListeners();

            onClick.AddListener(() => 
            {
                onClickChoice?.Invoke(choiceIndex);
                
                chatChoicesHolder.ShowChoice(choiceName);
            });
        }   
    }
}