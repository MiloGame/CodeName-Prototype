
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class LoginScreenViewModel : MonoBehaviour
{
    public UIDocument RootUiDocument;
    private VisualElement rootvisualEle;
    private VisualElement noticebutton;
    private string noticeone = "icon-default-step-one";
    private string noticetwo = "icon-default-step-two";
    private string noticedefault = "icon-default";
    private VisualElement noticeicon;
    private VisualElement exitbutton;

    
  
    [SerializeField]
    public UITypeUnityEvent OnExitLog;
    [SerializeField]
    public UITypeUnityEvent OnStartGame;
    private VisualElement logwindow;
    public bool isstart;

    void Start()
    {

        rootvisualEle = RootUiDocument.rootVisualElement;
        noticebutton = rootvisualEle.Q<VisualElement>("notice-button");
        noticebutton.RegisterCallback<MouseEnterEvent>(OnMouseIn);
        noticeicon=noticebutton.Q<VisualElement>("icon");
        noticebutton.RegisterCallback<MouseLeaveEvent>(OnMouseOut);
        exitbutton = rootvisualEle.Q<VisualElement>("exit-button");
        exitbutton.RegisterCallback<ClickEvent>(OnExitButtonClick);
        logwindow = rootvisualEle.Q<VisualElement>("Logwindow");
        logwindow.RegisterCallback<ClickEvent>(OnStartClick);
    }

    private void OnStartClick(ClickEvent evt)
    {

            OnStartGame.Invoke(UIManager.UIType.StartGameView);
        
        
    }


    private void OnExitButtonClick(ClickEvent evt)
    {
        OnExitLog.Invoke(UIManager.UIType.ExitPopWindow);

    }



    private void OnMouseOut(MouseLeaveEvent evt)
    {
        noticebutton.UnregisterCallback<TransitionEndEvent>(OnAniLoop);
        noticeicon.ClearClassList();
        noticeicon.ToggleInClassList(noticedefault);
    }

    private void OnMouseIn(MouseEnterEvent evt)
    {
        noticebutton.RegisterCallback<TransitionEndEvent>(OnAniLoop);
        Debug.Log("trigger");
        noticeicon.ToggleInClassList(noticetwo);

    }

    private void OnAniLoop(TransitionEndEvent evt)
    {
        Debug.Log("change");
        noticeicon.ToggleInClassList(noticetwo);
        noticeicon.ToggleInClassList(noticeone);
        
    }


}
