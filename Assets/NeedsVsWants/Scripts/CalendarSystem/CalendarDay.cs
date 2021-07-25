using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using NeedsVsWants.TooltipSystems;

using TMPro;
using UnityEngine.EventSystems;

namespace NeedsVsWants.CalendarSystem
{
    public class CalendarDay : TooltipTrigger
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

        public bool isWithinMonth { get; set; } = false;

        public Color color { get => _Image.color; set => _Image.color = value; }

        void Awake()
        {
            _Text = GetComponentInChildren<TMP_Text>();

            _Image = GetComponent<Image>();
        }

        public override void OnPointerEnter(PointerEventData eventData)
        {
            header = "Calendar Events";

            if(isWithinMonth)
            {
                if(calendarEvents != null)
                {
                    if(calendarEvents.Length > 0)
                    {
                        content = "";

                        foreach(CalendarEvent calendarEvent in calendarEvents)
                            content += calendarEvent.name + '\n';
                    }

                    else
                        content = "No Events";
                }

                else
                    content = "No Events";
                    
                base.OnPointerEnter(eventData);
            }
        }

        public void Clear()
        {
            dateTime = default(DateTime);

            _Text.text = "";
        }
    }
}