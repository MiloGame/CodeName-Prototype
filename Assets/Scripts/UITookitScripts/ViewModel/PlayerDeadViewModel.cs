using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerDeadViewModel : MonoBehaviour, ViewModelInterface
{
    public GameUIManager GameUiManager;
    VisualElement RootVisualElement;
    private VisualElement popupWindow;
    private string initcssstyle = "popwindow-init";
    private VisualElement yesButton;
    private VisualElement noButton;

    void Start()
    {
        GameUiManager.LogToDir(GameUIManager.UIType.PlayerDead, this as ViewModelInterface);

        RootVisualElement = GameUiManager.sceneDictionary[GameUIManager.UIType.PlayerDead];
        popupWindow = RootVisualElement.Q<VisualElement>("Playerdead-pop");
        yesButton = popupWindow.Q<VisualElement>("yes");
        noButton = popupWindow.Q<VisualElement>("no");
        yesButton.RegisterCallback<ClickEvent>(OnRestore);
        noButton.RegisterCallback<ClickEvent>(OnEnd);
    }

    private void OnRestore(ClickEvent evt)
    {
        EventBusManager.Instance.NonParamPublish(EventBusManager.EventType.RestoreTaskStatus);
        EventBusManager.Instance.NonParamPublish(EventBusManager.EventType.Restart);


    }

    private void OnEnd(ClickEvent evt)
    {
        OnExit(GameUIManager.UIType.GameEndScreen);
        GameUiManager.EndGame();
    }


    public void Fresh()
    {
    }

    public void OnEnter(GameUIManager.UIType uiType)
    {
        Debug.Log("enter gameend");
        GameUiManager.ActivateScene(uiType);
        StartCoroutine(GameEndDelay());
    }

    private IEnumerator GameEndDelay()
    {
        yield return new WaitForSeconds(0.25f);
        popupWindow.ToggleInClassList(initcssstyle);

    }

    public void OnExit(GameUIManager.UIType uiType)
    {
        popupWindow.ToggleInClassList(initcssstyle);
        GameUiManager.DeActivateScene(uiType);
    }
}
