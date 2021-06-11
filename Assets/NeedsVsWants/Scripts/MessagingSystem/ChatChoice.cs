using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using NeedsVsWants.WelfareSystem;

namespace NeedsVsWants.MessagingSystem
{
    [System.Serializable]
    public struct ChatChoice
    {
        public string name;
        public Conversation nextConversation;
        public int moneyOnChoice;
        public WelfareOperator welfareOnChoice;
    }
}