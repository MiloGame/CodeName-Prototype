using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class UIManager : MonoBehaviour
{
    public enum UIType
    {
        LoginScreen,
        ExitPopWindow,
        StartGameView,
        LoadingScreen
    }

    private string s = "00FFC7";
    public UIDocument UiDocument;
    public VisualElement RootVisualElement;
    public Dictionary<UIType, VisualElement> sceneDictionary;

    private BindableTProperty<float> loadBindableTProperty;
    // Start is called before the first frame update
    void Awake()
    {
        LoadSceneAsy(1,LoadSceneMode.Additive);
    }

    public void ChangeScene(int oldnum,int newnum)
    {
        sceneDictionary[UIType.LoadingScreen].style.display = DisplayStyle.Flex;
        StartCoroutine(LoadScene(newnum));
        //LoadSceneAsy(newnum,LoadSceneMode.Single);
        //UnLoadSceneAsy(oldnum);
    }

    private IEnumerator LoadScene(int newnum)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(newnum);

        while (!operation.isDone)
        {
            float loadpersent = operation.progress  * 100;
            loadBindableTProperty.SetValue(loadpersent,EventBusManager.EventType.LoadingScreenPersent);
            yield return null;
        }
    }

    void LoadSceneAsy(int id,LoadSceneMode ldMode)
    {
        SceneManager.LoadSceneAsync(id, ldMode);
    }
    void UnLoadSceneAsy(int id)
    {
        SceneManager.UnloadSceneAsync(id);
    }
    void Start()
    {
        RootVisualElement = UiDocument.rootVisualElement;
        sceneDictionary = new Dictionary<UIType, VisualElement>();
        loadBindableTProperty = new BindableTProperty<float>();
        InitSceneDic();
    }

    private void InitSceneDic()
    {
        sceneDictionary[UIType.LoginScreen] = RootVisualElement.Q<VisualElement>("Logwindow");
        sceneDictionary[UIType.ExitPopWindow] = RootVisualElement.Q<VisualElement>("Closepopup");
        sceneDictionary[UIType.StartGameView] = RootVisualElement.Q<VisualElement>("ChosseLevel");
        sceneDictionary[UIType.LoadingScreen] = RootVisualElement.Q<VisualElement>("LoadingScreen");
    }

    public void ActivateScene(UIType sceneType)
    {
        if (sceneDictionary.ContainsKey(sceneType))
        {
            sceneDictionary[sceneType].style.display = DisplayStyle.Flex;
        }
        
    }
    public void DeActivateScene(UIType sceneType)
    {
        if (sceneDictionary.ContainsKey(sceneType))
        {
            sceneDictionary[sceneType].style.display = DisplayStyle.None;
        }
        
    }
    public void EndGame()
    {
#if  UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    // Update is called once per frame
    void Update()
    {
         
    }
}
