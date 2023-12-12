using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;

public class GameUIManager : MonoBehaviour
{
    public enum  UIType
    {
        MainScene,
        Dialog,
        CloseWindow,
        TaskScreen,
        TaskUIButton,
        GameEndScreen,
        PlayerDead
    }
    public UIDocument UiDocument;
    public VisualElement RootVisualElement;
    public Dictionary<GameUIManager.UIType, VisualElement> sceneDictionary;
    public Dictionary<GameUIManager.UIType, ViewModelInterface> sceneCache;
    private CustomTEventData<GameStatus> changEventData;
    public GameManager GameManager;
    [SerializeField]
    private UIType currentUiType;
    private bool SetPause = false;
    private GameStatus preStatus;

    public UIType CurrentUI
    {
        get
        {
            return currentUiType;
        }
    }

    public void MouseInvisible()
    {
        //mouse invisible
        Cursor.visible = false;
        //mouse locked
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void MouseVisible()
    {
        //mouse invisible
        Cursor.visible = true;
        //mouse locked
        Cursor.lockState = CursorLockMode.None;
    }
    // Start is called before the first frame update
    public void FreshAwake()
    {
        RootVisualElement = UiDocument.rootVisualElement;
        sceneDictionary = new Dictionary<GameUIManager.UIType, VisualElement>();
        InitSceneDic();
        sceneCache = new Dictionary<UIType, ViewModelInterface>();
        changEventData = new CustomTEventData<GameStatus>();
    }

    public void FreshStart()
    {
        sceneCache[UIType.MainScene].OnEnter(UIType.MainScene);
        EventBusManager.Instance.NonParamScribe(EventBusManager.EventType.PlayerDead,OnPlayerDead);
        EventBusManager.Instance.NonParamScribe(EventBusManager.EventType.GameEnd,OnGameEnd);
        EventBusManager.Instance.NonParamScribe(EventBusManager.EventType.Restart, OnRestart);


    }

    private void OnRestart(object sender, EventArgs e)
    {
        if (currentUiType != UIType.MainScene)
        {
            sceneCache[currentUiType].OnExit(currentUiType);
            sceneCache[UIType.GameEndScreen].OnEnter(UIType.MainScene);
        }
    }

    private void OnGameEnd(object sender, EventArgs e)
    {
        if (currentUiType!=UIType.GameEndScreen)
        {
            sceneCache[currentUiType].OnExit(currentUiType);
            sceneCache[UIType.GameEndScreen].OnEnter(UIType.GameEndScreen);
        }
        
    }

    private void OnPlayerDead(object sender, EventArgs e)
    {
        if (currentUiType!=UIType.PlayerDead)
        {
            sceneCache[currentUiType].OnExit(currentUiType);
            sceneCache[UIType.PlayerDead].OnEnter(UIType.PlayerDead);
        }
    }

    public void LogToDir(UIType typeui,ViewModelInterface vm)
    {
        sceneCache[typeui] = vm;
    }
    private void InitSceneDic()
    {
        sceneDictionary[UIType.MainScene] = RootVisualElement.Q<VisualElement>("Root");
        sceneDictionary[UIType.Dialog] = RootVisualElement.Q<VisualElement>("DialogBar");
        sceneDictionary[UIType.TaskUIButton] = RootVisualElement.Q<VisualElement>("taskui");
        sceneDictionary[UIType.CloseWindow] = RootVisualElement.Q<VisualElement>("close-confirm");
        sceneDictionary[UIType.TaskScreen] = RootVisualElement.Q<VisualElement>("TaskContainer");
        sceneDictionary[UIType.GameEndScreen] = RootVisualElement.Q<VisualElement>("GameEnd");
        sceneDictionary[UIType.PlayerDead] = RootVisualElement.Q<VisualElement>("PlayerDie");
    }
    public void ActivateScene(GameUIManager.UIType sceneType)
    {

        if (sceneDictionary.ContainsKey(sceneType))
        {
            currentUiType = sceneType;
            sceneDictionary[sceneType].style.display = DisplayStyle.Flex;
        }

    }
    public void DeActivateScene(GameUIManager.UIType sceneType)
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

    public void FreshUpdate()
    {
        if (Input.GetKey(KeyCode.LeftAlt)||currentUiType ==UIType.Dialog||currentUiType==UIType.PlayerDead||currentUiType==UIType.TaskScreen || currentUiType == UIType.GameEndScreen)
        {
            MouseVisible();

        }
        else
        {
            MouseInvisible();
        }
        if (Input.GetKey(KeyCode.LeftAlt) )
        {
            //Debug.Log("pause");
            if (!SetPause)
                preStatus = GameManager.Status;
            changEventData.message = GameStatus.Pause;
            SetPause = true;
            EventBusManager.Instance.ParamPublish(EventBusManager.EventType.ChangeGameState,this,changEventData);
        }
        else
        {
            //Debug.Log("un pause");

            //if (SetPause && currentUiType == UIType.MainScene)
            if (SetPause )
            {
                SetPause = false;
                changEventData.message = preStatus;
                EventBusManager.Instance.ParamPublish(EventBusManager.EventType.ChangeGameState, this, changEventData);

            }
       
        }
    }
}
