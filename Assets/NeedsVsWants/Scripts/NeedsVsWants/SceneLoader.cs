using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

namespace NeedsVsWants
{
    [CreateAssetMenu(menuName = "NeedsVsWants/Scene Loader", fileName = "Scene Loader")]
    public class SceneLoader : ScriptableObject
    {
        public void LoadScene(int sceneBuildIndex)
        {
            Debug.Log("LoadScene...");
            SceneManager.LoadScene(sceneBuildIndex);
        }
    }
}