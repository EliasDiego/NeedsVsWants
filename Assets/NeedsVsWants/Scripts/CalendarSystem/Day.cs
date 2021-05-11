using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using TMPro;

namespace NeedsVsWants.CalendarSystem
{
    public class Day : MonoBehaviour
    {
        int _Day;

        TMP_Text _Text;

        public System.DateTime dateTime { get; set; }

        public TMP_Text text => _Text;

        public CalendarEvent[] calendarEvents { get; set; }

        public int day 
        { 
            get => _Day; 
            set
            {
                _Day = value;

                _Text.text = _Day.ToString();
            }
        }

        void Awake()
        {
            _Text = GetComponentInChildren<TMP_Text>();
        }

        public void Clear()
        {
            _Day = -1;

            text.text = "";

            calendarEvents = null;
        }
    }
}