using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks.Triggers;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class GameExitPopWindowViewModel : MonoBehaviour,ViewModelInterface
{
    public GameUIManager GameUiManager;
    VisualElement RootVisualElement;
    private VisualElement popupWindow;
    private string initcssstyle = "popwindow-init";
    private VisualElement yesButton;
    private VisualElement noButton;

    void Start()
    {
        GameUiManager.LogToDir(GameUIManager.UIType.CloseWindow, this as ViewModelInterface);

        RootVisualElement = GameUiManager.sceneDictionary[GameUIManager.UIType.CloseWindow];
        popupWindow = RootVisualElement.Q<VisualElement>("close-popwindow");
        yesButton = popupWindow.Q<VisualElement>("yes");
        noButton = popupWindow.Q<VisualElement>("no");
        yesButton.RegisterCallback<ClickEvent>(OnExist);
        noButton.RegisterCallback<ClickEvent>(OnReturn);
    }

    private void OnReturn(ClickEvent evt)
    {
        OnExit(GameUIManager.UIType.CloseWindow);
        GameUiManager.sceneCache[GameUIManager.UIType.MainScene].OnEnter(GameUIManager.UIType.MainScene);
    }

    private void OnExist(ClickEvent evt)
    {
        OnExit(GameUIManager.UIType.CloseWindow);
        GameUiManager.EndGame();
    }

    //private void OnReturn(ClickEvent evt)
    //{
    //    OnLeaveExit.Invoke(UIManager.UIType.ExitPopWindow);
    //    popupWindow.ToggleInClassList(initcssstyle);
    //}

    //private void OnExist(ClickEvent evt)
    //{
    //    GameOver.Invoke();
    //}

    //public void EnterScene(UIManager.UIType scType)
    //{
    //    popupWindow.ToggleInClassList(initcssstyle);
    //}


    public void Fresh()
    {
        
    }

    public void OnEnter(GameUIManager.UIType uiType)
    {
        GameUiManager.ActivateScene(uiType);
        popupWindow.ToggleInClassList(initcssstyle);
    }

    public void OnExit(GameUIManager.UIType uiType)
    {
        popupWindow.ToggleInClassList(initcssstyle);
        GameUiManager.DeActivateScene(uiType);

    }


}
