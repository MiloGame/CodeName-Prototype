using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks.Triggers;
using UnityEngine;
using UnityEngine.UIElements;

public class TaskScreenViewModel : MonoBehaviour, ViewModelInterface
{
    public GameUIManager GameUiManager;
    public QuestManager QManager;
    public VisualElement RootVisualElement;
    private string _TaskButtonstylesheet = "TaskButton";

    private ScrollView TaskButtoncontainer;

    private Label TaskShowText;
    private Button RUNTasKButton;

    private Dictionary<int, Label> ExistButtonCache;
    public int CurrentClickTaskID;
    private Button EarnAward;
    private Button RestoreTask;


    // Start is called before the first frame update
    void Start()
    {
        ExistButtonCache = new Dictionary<int, Label>();
        GameUiManager.LogToDir(GameUIManager.UIType.TaskScreen, this as ViewModelInterface);
        RootVisualElement = GameUiManager.sceneDictionary[GameUIManager.UIType.TaskScreen];
       
        TaskButtoncontainer = RootVisualElement.Q<ScrollView>("TaskButtonContainer");
        TaskShowText = RootVisualElement.Q<Label>("TaskText");
        RUNTasKButton = RootVisualElement.Q<Button>("RunTask");
        EarnAward = RootVisualElement.Q<Button>("EarnAward");
        RestoreTask = RootVisualElement.Q<Button>("RestoreTask");
       
    }

  


    public void Fresh()
    {
    }

    public void OnEnter(GameUIManager.UIType uiType)
    {
        GameUiManager.ActivateScene(uiType);
        TaskShowText.text = "";

        RUNTasKButton.style.display = DisplayStyle.None;
        EarnAward.style.display = DisplayStyle.None;
        EventBusManager.Instance.ParamScribe(EventBusManager.EventType.TaskCreatTaskButton, OnCreateOptionButton);
        EventBusManager.Instance.ParamScribe(EventBusManager.EventType.TaskDistoryTaskButton, OnDistoryOptionButton);
        RUNTasKButton.RegisterCallback<ClickEvent>(OnClickStart);
        EarnAward.RegisterCallback<ClickEvent>(OnClickAward);
        RestoreTask.RegisterCallback<ClickEvent>(OnRestore);
        EventBusManager.Instance.NonParamScribe(EventBusManager.EventType.RestoreTaskStatus,OnRestoreAllButton);

    }

    private void OnRestoreAllButton(object sender, EventArgs e)
    {
        foreach (var todestorLabel in ExistButtonCache.Values)
        {

            todestorLabel.RemoveFromHierarchy();
        }
        ExistButtonCache.Clear();
    }

    private void OnRestore(ClickEvent evt)
    {
        if (evt.propagationPhase != PropagationPhase.AtTarget)
            return;
        EventBusManager.Instance.NonParamPublish(EventBusManager.EventType.RestoreTaskStatus);
        
        Debug.Log("ExistButtonCache length"+ ExistButtonCache.Count);
    }

    private void OnClickAward(ClickEvent evt)
    {
        QManager.GeTaskInfoDB(CurrentClickTaskID).Status = 3;
        //QManager.SavePlayerStatus();

        EarnAward.style.display = DisplayStyle.None;
    }

    private void OnClickStart(ClickEvent evt)
    {
        QManager.GeTaskInfoDB(CurrentClickTaskID).Status = 1;
        //QManager.SavePlayerStatus();
        RUNTasKButton.style.display = DisplayStyle.None;
    }

    private void OnDistoryOptionButton(object sender, EventArgs e)
    {
        var data = e as CustomTEventData<int>;
        if (data != null && ExistButtonCache.ContainsKey(data.message))
        {
            Label todestorLabel = ExistButtonCache[data.message];
            TaskShowText.text = "";

            ExistButtonCache.Remove(data.message);
            todestorLabel.RemoveFromHierarchy();

        }
    }

    private void OnCreateOptionButton(object sender, EventArgs e)
    {
        var data = e as CustomTEventData<int>;
        
        if (data!=null && !ExistButtonCache.ContainsKey(data.message))
        {
            var info = QManager.GeTaskInfoDB(data.message);

            Label newTaskLabel = new Label(info.TaskTitle);
            newTaskLabel.viewDataKey = data.message.ToString();
            newTaskLabel.RegisterCallback<ClickEvent>(OnClickTaskButton);
            TaskButtoncontainer.Add(newTaskLabel);
            newTaskLabel.AddToClassList(_TaskButtonstylesheet);
            ExistButtonCache[data.message] = newTaskLabel;
        }
    }

    private void OnClickTaskButton(ClickEvent evt)
    {
        if (evt.propagationPhase != PropagationPhase.AtTarget)
            return;
        Label buttonLabel = evt.target as Label;
        CurrentClickTaskID = int.Parse(buttonLabel.viewDataKey);

        var info = QManager.GeTaskInfoDB(CurrentClickTaskID);
        if (info.Status == 2)
        {
            EarnAward.style.display = DisplayStyle.Flex;
            RUNTasKButton.style.display = DisplayStyle.None;

        }
        else if (info.Status == 0)
        {
            EarnAward.style.display = DisplayStyle.None;
            RUNTasKButton.style.display = DisplayStyle.Flex;
        }
        else
        {
            EarnAward.style.display = DisplayStyle.None;
            RUNTasKButton.style.display = DisplayStyle.None;
        }

        TaskShowText.text = info.TaskDescription;
    }


    public void OnExit(GameUIManager.UIType uiType)
    {
        GameUiManager.DeActivateScene(uiType);
        EventBusManager.Instance.ParamUnScribe(EventBusManager.EventType.TaskCreatTaskButton, OnCreateOptionButton);
        EventBusManager.Instance.ParamUnScribe(EventBusManager.EventType.TaskDistoryTaskButton, OnDistoryOptionButton);
        RUNTasKButton.UnregisterCallback<ClickEvent>(OnClickStart);
        EarnAward.UnregisterCallback<ClickEvent>(OnClickAward);
        RestoreTask.UnregisterCallback<ClickEvent>(OnRestore);

    }
}
