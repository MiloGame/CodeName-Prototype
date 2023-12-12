using UnityEngine;
using UnityEngine.SceneManagement;

using UnityEditor;
using UnityEditor.SceneManagement;

using System.Collections.Generic;
using System.IO;

// Scene selection

class SceneWindow : EditorWindow
{

    [MenuItem("Window/Scenes")]
    public static void ShowWindow()
    {

        GetWindow(typeof(SceneWindow), false, "Scenes");

    }

    int selected; int changedSelected;
    List<string> scenes = new List<string>();
    string playerPrefName;
    Vector2 scrollPosition;
    void OnGUI()
    {

        EditorGUILayout.Space();

        scenes.Clear();

        string[] dropOptions = new string[EditorBuildSettings.scenes.Length];

        for (int i = 0; i < EditorBuildSettings.scenes.Length; i++)
        {

            EditorBuildSettingsScene scene = EditorBuildSettings.scenes[i];

            string sceneName = Path.GetFileNameWithoutExtension(scene.path);

            scenes.Add(sceneName);

            dropOptions[i] = scenes[i];

            if (scene.path == EditorSceneManager.GetActiveScene().path)
            {

                selected = changedSelected = i;

            }

        }

        // Selection

        changedSelected = EditorGUILayout.Popup(selected, dropOptions);

        if (selected != changedSelected)
        {

            selected = changedSelected;

            EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
            EditorSceneManager.OpenScene(EditorBuildSettings.scenes[changedSelected].path);

        }

        EditorGUILayout.Space();

        if (GUILayout.Button("Play From Start", GUILayout.Height(20)))
        {

            if (EditorApplication.isPlaying)
            {
                EditorApplication.isPlaying = false;
                return;
            }
            EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
            EditorSceneManager.OpenScene(EditorBuildSettings.scenes[0].path);
            EditorApplication.isPlaying = true;

        }

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Scenes", EditorStyles.boldLabel);

        scrollPosition = GUILayout.BeginScrollView(scrollPosition);

        GUIContent playButton = EditorGUIUtility.IconContent("Animation.Play");
        GUIContent addButton = EditorGUIUtility.IconContent("Toolbar Plus");

        for (int i = 0; i < scenes.Count; i++)
        {

            GUILayout.BeginHorizontal("box");
            if (GUILayout.Button(playButton, GUILayout.Width(30)))
            {

                if (EditorApplication.isPlaying)
                {
                    EditorApplication.isPlaying = false;
                    return;
                }
                selected = changedSelected = i;
                EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
                EditorSceneManager.OpenScene(EditorBuildSettings.scenes[i].path);
                EditorApplication.isPlaying = true;

            }
            if (GUILayout.Button(scenes[i], GUILayout.Height(20)))
            {

                selected = changedSelected = i;

                if (!EditorApplication.isPlaying)
                {
                    EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
                    EditorSceneManager.OpenScene(EditorBuildSettings.scenes[i].path);
                }
                else
                {
                    SceneManager.LoadSceneAsync(EditorBuildSettings.scenes[i].path);
                }

            }
            if (GUILayout.Button(addButton, GUILayout.Width(30)))
            {

                selected = changedSelected = i;

                if (!EditorApplication.isPlaying)
                {
                    EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
                    EditorSceneManager.OpenScene(EditorBuildSettings.scenes[i].path, OpenSceneMode.Additive);
                }
                else
                {
                    SceneManager.LoadSceneAsync(EditorBuildSettings.scenes[i].path, LoadSceneMode.Additive);
                }

            }
            GUILayout.EndHorizontal();
            dropOptions[i] = scenes[i];

        }

        GUILayout.EndScrollView();

    }

}