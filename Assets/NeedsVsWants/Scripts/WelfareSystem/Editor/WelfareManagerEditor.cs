using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using UnityEditor;

namespace NeedsVsWants.WelfareSystem
{
    [CustomEditor(typeof(WelfareManager))]
    public class WelfareManagerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            WelfareManager welfareManager = target as WelfareManager;

            if(!welfareManager || !Application.isPlaying)
                return;

            EditorGUILayout.LabelField("Welfare Sliders", EditorStyles.boldLabel);

            // foreach(WelfareSlider welfareSlider in welfareManager.welfareSliders)
            // {
            //     EditorGUILayout.LabelField(welfareSlider.id, EditorStyles.boldLabel);

            //     welfareSlider.value = EditorGUILayout.Slider(welfareSlider.value, 0, welfareSlider.maxValue);
                
            //     EditorGUILayout.Space();
            // }
        }
    }
}