using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using NeedsVsWants.Patterns;

namespace NeedsVsWants.ShoppingSystem
{
    public class GameObjectItemManager : SimpleSingleton<GameObjectItemManager>
    {
        GameObject[] _GameObjects;

        void Start() 
        {
            _GameObjects = GetComponentsInChildren<Transform>(true).Select(transform => transform.gameObject).Except(new GameObject[] { gameObject }).ToArray();
        }

        public GameObject GetGameObject(string name) =>
             _GameObjects.First(gameObject => gameObject.name == name);
    }
}