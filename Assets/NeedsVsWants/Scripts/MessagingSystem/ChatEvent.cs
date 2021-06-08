using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using NeedsVsWants.CalendarSystem;
using System;

namespace NeedsVsWants.MessagingSystem
{
    [CreateAssetMenu(menuName = "NeedsVsWants/Messages/Chat Event")]
    public class ChatEvent : CalendarEvent
    {
        public Chat chat;

        public override bool showOnCalendar => false;

        public override void Invoke(DateTime dateTime)
        {
            
        }

        public override bool IsWithinDate(DateTime dateTime)
        {
            return false;
        }
    }
}