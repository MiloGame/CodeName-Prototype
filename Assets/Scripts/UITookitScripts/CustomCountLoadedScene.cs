using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class CustomCountLoadedScene
{
    // adds a custom menu item which displays the number of open Scenes in a Dialogue Box
    [MenuItem("SceneExample/Number Of Scenes")]
    public static void NumberOfScenes()
    {
        EditorUtility.DisplayDialog("Number of loaded Scenes:", SceneManager.loadedSceneCount.ToString(), "Ok");
    }
}