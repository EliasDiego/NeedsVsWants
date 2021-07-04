using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using TMPro;

namespace NeedsVsWants.MessagingSystem
{
    public class MessageBox : MonoBehaviour
    {
        [SerializeField]
        TMP_Text _Sender;
        [SerializeField]
        TMP_Text _Text;
        [SerializeField]
        Image _ProfilePicture;
        [SerializeField]
        LayoutElement _BoxLayout;
        [SerializeField]
        float _CharacterLimit;

        HorizontalLayoutGroup _LayoutGroup;

        void Awake() 
        {
            _LayoutGroup = GetComponent<HorizontalLayoutGroup>();    
        }

        public void AssignMessage(Character sender, string text, bool isAnne)
        {
            _Sender.text = sender.name;
            _Text.text = text;
            _ProfilePicture.sprite = sender.profilePicture;

            if(isAnne)
            {
                _LayoutGroup.childAlignment = TextAnchor.UpperRight;

                _ProfilePicture.transform.SetSiblingIndex(1);

                _Sender.alignment = TextAlignmentOptions.Right;
            }

            else
            {
                _LayoutGroup.childAlignment = TextAnchor.UpperLeft;

                _ProfilePicture.transform.SetSiblingIndex(0);
                
                _Sender.alignment = TextAlignmentOptions.Left;
            }

            _BoxLayout.enabled = (_Sender.text.Length > _Text.text.Length ? _Sender.text.Length : _Text.text.Length) > _CharacterLimit;
        }
    }
}