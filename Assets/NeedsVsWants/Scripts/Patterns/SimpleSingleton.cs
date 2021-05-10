using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeedsVsWants.Patterns
{
    public class SimpleSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        static T _Instance;

        public static T instance => _Instance;

        protected virtual void Awake()
        {
            if(!_Instance)
                _Instance = this as T;
        }
    }
}