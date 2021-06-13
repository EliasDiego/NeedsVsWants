using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using UnityEditor;

[CreateAssetMenu(menuName = "test/testSO")]
public class testSO : ScriptableObject
{
    [SerializeField]
    testSO _testSO;

    [CustomEditor(typeof(testSO))]
    class testSOEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if(GUILayout.Button("Create"))
            {
                testSO newTestSO = ScriptableObject.CreateInstance<testSO>();

                testSO testSO = target as testSO;

                testSO._testSO = newTestSO;

                AssetDatabase.AddObjectToAsset(newTestSO, target);

                AssetDatabase.SaveAssets();
                AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(newTestSO));
            }
        }
    }
}
