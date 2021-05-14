using UnityEngine;

using UnityEditor;

using NeedsVsWants;

namespace NeedsVsWants
{
    //[CustomPropertyDrawer(typeof(FloatMinMax))]
    public class FloatMinMaxDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            float width = position.width / 4;

            Rect minRect = new Rect(position.x + width * 2, position.y, width, position.height);
            Rect maxRect = new Rect(position.x + width * 2 + width, position.y, width, position.height);

            EditorGUI.LabelField(position, property.name);

            property.FindPropertyRelative("min").floatValue = EditorGUI.FloatField(minRect, "Min", property.FindPropertyRelative("min").floatValue);
            property.FindPropertyRelative("max").floatValue = EditorGUI.FloatField(maxRect, "Max", property.FindPropertyRelative("max").floatValue);

            EditorGUI.EndProperty();
        }

        string FormatName(string name)
        {
            string[] seperatedStrings = name.Split('_');

            return "";
        }
    }
}