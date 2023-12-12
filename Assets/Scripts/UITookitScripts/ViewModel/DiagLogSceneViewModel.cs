using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements;

public class DiagLogSceneViewModel : MonoBehaviour, ViewModelInterface
{
    public GameUIManager GameUiManager;
    public VisualElement RootVisualElement;
    private BindableTProperty<string> speakerNameBindableTProperty;
    private BindableTProperty<string> dialogTextBindableTProperty;
    private Label NameTextfield;
    private Label DialogTextfield;
    private Label NextLabel;
    private WaitForSeconds WaitTimeDialogChar;
    private int buttonoffset;
    private int buttonWidth;
    private int buttonMargine;
    private Queue<Label> OptionButton;
    private string _optionstylesheet = "DialogButton";
    private Dictionary<string, CustomButtonTEventData> LabelHash;
    public void Fresh()
    {
        
    }

    public void OnEnter(GameUIManager.UIType uiType)
    {
        GameUiManager.ActivateScene(uiType);
        speakerNameBindableTProperty.GetValue(EventBusManager.EventType.DialogRefreshName,OnNameChange);
        dialogTextBindableTProperty.GetValue(EventBusManager.EventType.DialogRefreshText,OnTextChange);
        EventBusManager.Instance.ParamScribe(EventBusManager.EventType.DialogCreateNewButton,OnCreateOptionButton);
        EventBusManager.Instance.NonParamScribe(EventBusManager.EventType.DialogDistoryButton,OnDistoryOptionButton);
    }

    private void OnDistoryOptionButton(object sender, EventArgs e)
    {
        buttonoffset = -buttonWidth;
        while (OptionButton.Count>0)
        {
            Label labelToDestory = OptionButton.Dequeue();
            labelToDestory.RemoveFromHierarchy();
            LabelHash.Remove(labelToDestory.text);
        }
    }

    private void OnCreateOptionButton(object sender, EventArgs e)
    {
        var data = e as CustomButtonTEventData;
        if (data!=null)
        {
            if (!LabelHash.ContainsKey(data.message))
            {
                Debug.Log("receive data" + data.ToJumpIndex + data.message + LabelHash.ContainsKey(data.message));

                Label OptionNewLabel = new Label(data.message);
                LabelHash.Add(data.message,new CustomButtonTEventData(data)); // 必须用自定义的拷贝构造函数，否则会因为C#引用类型导致后面的选项更改前面的值
                OptionNewLabel.RegisterCallback<ClickEvent>(OnClickOption);
                RootVisualElement.Add(OptionNewLabel);
                OptionNewLabel.AddToClassList(_optionstylesheet);
                OptionNewLabel.style.top = buttonoffset;
                buttonoffset -= buttonWidth;
                buttonoffset -= buttonMargine;
                OptionButton.Enqueue(OptionNewLabel);
            }
            
        }
    }

    private void OnClickOption(ClickEvent evt)
    {
        if (evt.propagationPhase != PropagationPhase.AtTarget)
            return;
        Label buttonLabel = evt.target as Label;
        CustomButtonTEventData datacb = LabelHash[buttonLabel.text];
            Debug.Log("click callback receive exist data" + buttonLabel.text + datacb.ToJumpIndex + datacb.message + datacb.GetHashCode());
        datacb.DialogManager.DialogIndex = datacb.ToJumpIndex;
        datacb.DialogManager.ShowDialogRow();
    }

    //private void OnClickOption( )
    //{
    //    CustomButtonTEventData datacb = 
    //    Debug.Log("click callback exist data"+ datacb.ToJumpIndex+ datacb.message + datacb.GetHashCode());
    //    datacb.DialogManager.DialogIndex = datacb.ToJumpIndex;
    //    datacb.DialogManager.ShowDialogRow();
    //}

    private void OnTextChange(object sender, EventArgs e)
    {
        var data = e as CustomTEventData<string>;
        if (data!=null)
        {
            StartCoroutine(DotWeenLikeString(data.message));
        }
    }

    private IEnumerator DotWeenLikeString(string dataMessage)
    {
        DialogTextfield.text = String.Empty;
        NextLabel.style.display = DisplayStyle.None;
        EventBusManager.Instance.NonParamPublish(EventBusManager.EventType.DialogCanNotProceed);
        foreach (var c in dataMessage.ToCharArray())
        {
            yield return WaitTimeDialogChar;
            DialogTextfield.text += c;
        }
        EventBusManager.Instance.NonParamPublish(EventBusManager.EventType.DialogCanProceed);
        NextLabel.style.display = DisplayStyle.Flex;

    }

    private void OnNameChange(object sender, EventArgs e)
    {

        var data = e as CustomTEventData<string>;
        if (data!=null)
        {
            NameTextfield.text = data.message;
        }

    }

    public void OnExit(GameUIManager.UIType uiType)
    {
        GameUiManager.DeActivateScene(uiType);
        speakerNameBindableTProperty.UnScribe(EventBusManager.EventType.DialogRefreshName, OnNameChange);
        dialogTextBindableTProperty.UnScribe(EventBusManager.EventType.DialogRefreshText, OnTextChange);
        EventBusManager.Instance.ParamUnScribe(EventBusManager.EventType.DialogCreateNewButton, OnCreateOptionButton);
        EventBusManager.Instance.NonParamUnScribe(EventBusManager.EventType.DialogDistoryButton, OnDistoryOptionButton);
    }


// Start is called before the first frame update
    void Start()
    {
        LabelHash = new Dictionary<string, CustomButtonTEventData>();
        OptionButton = new Queue<Label>();
        buttonoffset = -56;
        buttonWidth = 56;
        buttonMargine = 4;
        GameUiManager.LogToDir(GameUIManager.UIType.Dialog, this as ViewModelInterface);
        RootVisualElement = GameUiManager.sceneDictionary[GameUIManager.UIType.Dialog];
        speakerNameBindableTProperty = new BindableTProperty<string>();
        dialogTextBindableTProperty = new BindableTProperty<string>();
        NameTextfield = RootVisualElement.Q<Label>("NPCName");
        DialogTextfield = RootVisualElement.Q<Label>("ShowMessage");
        NextLabel = RootVisualElement.Q<Label>("Next");
        WaitTimeDialogChar = new WaitForSeconds(0.02f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
