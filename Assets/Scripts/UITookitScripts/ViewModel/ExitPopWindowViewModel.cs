using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks.Triggers;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class ExitPopWindowViewModel : MonoBehaviour
{
    public UIDocument RootUiDocument;
    public VisualElement RootVisualElement;
    private VisualElement popupWindow;
    private string initcssstyle = "popwindow-init";
    private VisualElement yesButton;
    private VisualElement noButton;
    [SerializeField]
    public UITypeUnityEvent OnLeaveExit;

    public UnityEvent GameOver;
    void Start()
    {
        RootVisualElement = RootUiDocument.rootVisualElement.Q<VisualElement>("Closepopup");
        popupWindow = RootVisualElement.Q<VisualElement>("close-popwindow");
        yesButton = popupWindow.Q<VisualElement>("yes");
        noButton = popupWindow.Q<VisualElement>("no");
        yesButton.RegisterCallback<ClickEvent>(OnExist);
        noButton.RegisterCallback<ClickEvent>(OnReturn);
    }

    private void OnReturn(ClickEvent evt)
    {
        OnLeaveExit.Invoke(UIManager.UIType.ExitPopWindow);
        popupWindow.ToggleInClassList(initcssstyle);
    }

    private void OnExist(ClickEvent evt)
    {
        GameOver.Invoke();
    }

    public void EnterScene(UIManager.UIType scType)
    {
        popupWindow.ToggleInClassList(initcssstyle);
    }


}
