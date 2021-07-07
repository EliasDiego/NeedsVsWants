using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

using NeedsVsWants.WelfareSystem;

namespace NeedsVsWants.MessagingSystem
{
    [System.Serializable]
    public struct ChatChoice
    {
        public string name;
        public Conversation nextConversation;
        public bool applyEffects;
        public double moneyOnChoice;
        public WelfareOperator welfareOnChoice;
        public UnityEvent onChoiceEvent;
    }
}