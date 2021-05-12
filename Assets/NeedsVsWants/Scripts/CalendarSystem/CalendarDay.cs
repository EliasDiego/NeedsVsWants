using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using TMPro;

namespace NeedsVsWants.CalendarSystem
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

        public CalendarEvent[] calendarEvents { get; set; }

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

            calendarEvents = null;
        }
    }
}