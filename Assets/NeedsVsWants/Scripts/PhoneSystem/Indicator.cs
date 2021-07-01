using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using TMPro;

namespace NeedsVsWants.PhoneSystem
{
    public class Indicator : MonoBehaviour
    {
        [SerializeField]
        Image _Graphic;
        [SerializeField]
        TMP_Text _Text;
        
        public string text { get => _Text.text; set => _Text.text = value.Length > 3 ? value.Substring(0, 3) : value; }
    }
}