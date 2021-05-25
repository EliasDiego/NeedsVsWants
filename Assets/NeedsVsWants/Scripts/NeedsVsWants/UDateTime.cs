using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeedsVsWants
{
    // Source: https://forum.unity.com/threads/propertydrawer-for-datetime-class-not-getting-called.490129/
    [Serializable]
    public class UDateTime : ISerializationCallbackReceiver
    {
        public DateTime dateTime;

        [SerializeField]
        private string _dateTime;

        public static implicit operator DateTime( UDateTime udt )
        {
            return (udt.dateTime);
        }

        public static implicit operator UDateTime( DateTime dt )
        {
            return new UDateTime() { dateTime = dt };
        }

        public void OnAfterDeserialize()
        {
            DateTime.TryParse(_dateTime, out dateTime);
        }

        public void OnBeforeSerialize()
        {
            _dateTime = dateTime.ToString();
        }
    }
}