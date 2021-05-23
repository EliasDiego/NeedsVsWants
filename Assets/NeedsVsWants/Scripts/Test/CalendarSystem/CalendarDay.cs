using System;

using UnityEngine;
using UnityEngine.UI;

using TMPro;

namespace NeedsVsWants.Test.CalendarySystem
{
    public class CalendarDay : MonoBehaviour
    {
        TMP_Text _Text;

        Image _Image;

        DateTime _DateTime;

        public DateTime dateTime 
        { 
            get => _DateTime;
            set
            {
                _DateTime = value;

                _Text.text = _DateTime.Day.ToString();
            }
        }

        public Color color { get => _Image.color; set => _Image.color = value; }

        void Awake()
        {
            _Text = GetComponentInChildren<TMP_Text>();

            _Image = GetComponent<Image>();
        }

        public void Clear()
        {
            dateTime = default(DateTime);

            _Text.text = "";
        }
    }
}