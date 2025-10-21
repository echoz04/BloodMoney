#if UNITY_EDITOR

using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Sources.Editor
{
    public static class GameBootstrapMenu
    {
        private const string BootstrapScenePath = "Assets/_Project/Users/echoz04/Scenes/Bootstrap.unity";

        [MenuItem("Tools/Project/Quick start")]
        public static void OpenBootstrapAndPlay()
        {
            if (EditorApplication.isPlaying == true)
            {
                Debug.LogWarning("Already in Play Mode.");
                return;
            }

            if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo() == true)
            {
                EditorSceneManager.OpenScene(BootstrapScenePath);
                EditorApplication.EnterPlaymode();
                Debug.Log("Loaded Bootstrap scene and entered Play Mode.");
            }
        }
    }
}

#endif