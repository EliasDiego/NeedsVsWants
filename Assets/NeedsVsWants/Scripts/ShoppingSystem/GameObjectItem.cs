using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using UnityEditor;

using NeedsVsWants.Player;

namespace NeedsVsWants.ShoppingSystem
{
    [CreateAssetMenu(menuName = "NeedsVsWants/Shopping/Game Object Item")]
    public class GameObjectItem : Item
    {
        GameObject _GameObject;

        public bool isAlreadyActive => _GameObject ? _GameObject.activeSelf : false;

        [CustomEditor(typeof(GameObjectItem))]
        class GameObjectItemEditor : Editor
        {
            public override void OnInspectorGUI()
            {
                base.OnInspectorGUI();

                GameObjectItem gameObjectItem = target as GameObjectItem;

                if(!gameObjectItem)
                    return;

                gameObjectItem._GameObject = EditorGUILayout.ObjectField("Game Object", gameObjectItem._GameObject, typeof(GameObject), true) as GameObject;

                serializedObject.ApplyModifiedProperties();
            }
        }

        public override void OnBuy()
        {
            base.OnBuy();

            _GameObject?.SetActive(true);

            PlayerStatManager.instance.RemoveShopItem(this);
        }
    }
}