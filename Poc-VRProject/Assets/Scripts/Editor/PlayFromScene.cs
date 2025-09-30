using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

[InitializeOnLoad]
public static class PlayFromStartSceneHook
{
    // Build index of the scene to always start from
    private const int startSceneIndex = 0;
    private const string SceneSetupKey = "PlayFromStartScene_SavedSetup";

    private static bool restoring = false;

    static PlayFromStartSceneHook()
    {
        EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
    }

    [MenuItem("PlayMode/Enable play mode switch", true)]
    private static bool EnableSwitch_Validate()
    {
        // Only show "Enable" if it is currently disabled
        return !EditorPrefs.GetBool("enabledSceneSwitcher", true);
    }

    [MenuItem("PlayMode/Disable play mode switch", true)]
    private static bool DisableSwitch_Validate()
    {
        // Only show "Disable" if it is currently enabled
        return EditorPrefs.GetBool("enabledSceneSwitcher", true);
    }

    [MenuItem("PlayMode/Enable play mode switch")]
    public static void EnableSwitch()
    {
        EditorPrefs.SetBool("enabledSceneSwitcher", true);
    }

    [MenuItem("PlayMode/Disable play mode switch")]
    public static void DisableSwitch()
    {
        EditorPrefs.SetBool("enabledSceneSwitcher", false);
    }

    private static void OnPlayModeStateChanged(PlayModeStateChange state)
    {
        if (EditorPrefs.HasKey("enabledSceneSwitcher")) {
            if (!EditorPrefs.GetBool("enabledSceneSwitcher")) return;
        }
        else
        {
            EditorPrefs.SetBool("enabledSceneSwitcher", true);
        }

        if (state == PlayModeStateChange.ExitingEditMode && !restoring)
        {
            // Save current scenes before switching
            if (!EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
            {
                // Cancel play mode if user declined save
                EditorApplication.isPlaying = false;
                return;
            }

            // Save current open scene setup into EditorPrefs (JSON string)
            var setup = EditorSceneManager.GetSceneManagerSetup();
            EditorPrefs.SetString(SceneSetupKey, JsonUtility.ToJson(new SceneSetupWrapper(setup)));

            // Switch to start scene
            string path = SceneUtility.GetScenePathByBuildIndex(startSceneIndex);
            if (string.IsNullOrEmpty(path))
            {
                Debug.LogError($"No scene found at build index {startSceneIndex}. Check Build Settings!");
                return;
            }

            EditorSceneManager.OpenScene(path);
        }
        else if (state == PlayModeStateChange.EnteredEditMode && !restoring)
        {
            // After play mode ends, restore scene setup
            if (EditorPrefs.HasKey(SceneSetupKey))
            {
                restoring = true;

                var json = EditorPrefs.GetString(SceneSetupKey, "");
                var wrapper = JsonUtility.FromJson<SceneSetupWrapper>(json);
                if (wrapper != null && wrapper.scenes != null && wrapper.scenes.Length > 0)
                {
                    EditorSceneManager.RestoreSceneManagerSetup(wrapper.scenes);
                }

                EditorPrefs.DeleteKey(SceneSetupKey);
                restoring = false;
            }
        }
    }

    // Helper class because SceneSetup[] isn’t serializable by default
    [System.Serializable]
    private class SceneSetupWrapper
    {
        public SceneSetup[] scenes;
        public SceneSetupWrapper(SceneSetup[] scenes) => this.scenes = scenes;
    }
}
