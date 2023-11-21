using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [HideInInspector] public int SelectedSceneIndex;

    private AsyncOperation async;

    public void LoadScene()
    {
        StartCoroutine(LoadSceneCoroutine());
    }

    private IEnumerator LoadSceneCoroutine()
    {
        async = SceneManager.LoadSceneAsync(SelectedSceneIndex, LoadSceneMode.Single);
        async.allowSceneActivation = false;

        while (async.progress < 0.9f)
        {
            print(async.progress);
            yield return null;
        }
        ShowScene();
        print("SceneLoadDone");
    }

    private void ShowScene()
    {
        async.allowSceneActivation = true;
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(SceneLoader))]
class SceneLoaderEditor : Editor
{
    private SceneLoader sceneLoader;

    public override void OnInspectorGUI()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
        List<string> scenes = new List<string>();
        foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
        {
            if (scene.enabled)
            {
                scenes.Add(scene.path);
            }
        }
        DrawDefaultInspector();
        if (GUILayout.Button("Current SceneToLoad Index: " + sceneLoader.SelectedSceneIndex))
        {
            GenericMenu menu = new GenericMenu();
            for (int i = 0; i < scenes.Count; i++)
            {
                int index = i;
                menu.AddItem(new GUIContent(scenes[i]), false, () =>
                {
                    sceneLoader.SelectedSceneIndex = index;
                    Debug.Log(index);
                });
            }
            menu.ShowAsContext();

            EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
        }
    }
}
#endif
