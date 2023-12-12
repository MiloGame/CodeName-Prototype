 using System;
 using System.Collections;
using System.Collections.Generic;
 using System.IO;
 using System.Text;
 using Newtonsoft.Json;
 using Unity.VisualScripting;
 using UnityEngine;
 using UnityEngine.Purchasing;

 [Serializable]
 public class PlayerTaskStatus
 {
     public int taskid;
     public int Status;
 }
public class QuestManager : MonoBehaviour,AchieveFunInterface,UnLockFunInterface
{
    public List<TaskInfo> TaskInfolist;
    public bool EnableQuest;
    public GameUIManager GameUiManager;
    public List<AchieveRuleBase> AchieveRuleBaseInitList;
    public List<UnlockRuleBase> UnlockRuleBaseInitList;
    private Dictionary<int, TaskInfo> taskInfosHash;
    public Dictionary<int, AchieveRuleBase> AchieveRuleHash;
    public Dictionary<int, UnlockRuleBase> UnlockRuleHash;
    public List<PlayerTaskStatus> PlayerTaskStatuslist;
    private CustomTEventData<int> taskButtonCreateData;
    private CustomTEventData<int> taskButtonDestoryData;
    [SerializeField]
    private Dictionary<int,TaskInfo> OnProcessTask;
    [SerializeField]
    private bool CurrentBossStatus;
    [SerializeField]
    private int CurrentDefeatEnemyVal;
    [SerializeField]
    private int CurrentDisCoverAreas;
    [SerializeField]
    private int CurrentCarryBox;
    [SerializeField]
    private int CurrentTalkNpcID;

    private CustomTEventData<int> CustomChatNPCIDEventData;
    private BindableTProperty<string> taskprocessBindableTProperty;
    private BindableTProperty<string> taskShortcutBindableTProperty;

    private int CurrentProcessId;

    public bool Alltaskcomplete=false;

    // Start is called before the first frame update
    void Start()
    {
        CurrentDisCoverAreas = 1;
        taskShortcutBindableTProperty = new BindableTProperty<string>();
        taskprocessBindableTProperty = new BindableTProperty<string>();
        CustomChatNPCIDEventData = new CustomTEventData<int>();
        //LoadData("TaskTemplete 1");
        taskInfosHash = new Dictionary<int, TaskInfo>();
        taskButtonCreateData = new CustomTEventData<int>();
        taskButtonDestoryData = new CustomTEventData<int>();
        AchieveRuleBaseInitList = LoadData<AchieveRuleBase>("AchieveRule");
        UnlockRuleBaseInitList = LoadData<UnlockRuleBase>("UnlockRule");
        AchieveRuleHash = new Dictionary<int, AchieveRuleBase>();
        UnlockRuleHash = new Dictionary<int, UnlockRuleBase>();
        InitHashTable();
        LoadInfoData("TaskTemplete");
        ShowUnlockRuleListParam();
        ShowAchieveRuleListParam();
        Debug.Log("taskinfolist length" + TaskInfolist.Count);
        foreach (var taskInfo in TaskInfolist)
        {
            Debug.Log("taskinfo id" + taskInfo.TaskID);
            taskInfosHash[taskInfo.TaskID] = taskInfo;
        }
        Debug.Log("PlayerTaskStatuslist init length"+taskInfosHash.Count);
        PlayerTaskStatuslist = new List<PlayerTaskStatus>();
        for (int i = 0; i < taskInfosHash.Count; i++)
        {
            PlayerTaskStatuslist.Add(new PlayerTaskStatus());
        }
        OnProcessTask = taskInfosHash;

        if (LoadPlayerTaskData("PlayerTaskStatus"))
        {
            foreach (var playertaskstatus in PlayerTaskStatuslist)
            {
                OnProcessTask[playertaskstatus.taskid].Status = playertaskstatus.Status;
            }
        }

        ScribeEvent();
        //taskButtonData.message = TaskInfolist[0].TaskID;
        //Debug.Log("publish id"+ TaskInfolist[0].TaskID);
    }

    private void ScribeEvent()
    {
        EventBusManager.Instance.NonParamScribe(EventBusManager.EventType.CarryBoxToPosAdd,OnCarryBoxAdd);
        EventBusManager.Instance.NonParamScribe(EventBusManager.EventType.CarryBoxToPosMinus,OnCarryBoxMinus);
        EventBusManager.Instance.NonParamScribe(EventBusManager.EventType.UnlockAreaAdd, OnUnlockAreaAdd);
        EventBusManager.Instance.NonParamScribe(EventBusManager.EventType.RestoreTaskStatus,OnRestore);
        EventBusManager.Instance.NonParamScribe(EventBusManager.EventType.KillEnemyTask, OnKillEnemy);
        EventBusManager.Instance.NonParamScribe(EventBusManager.EventType.KillBoss, OnKillBoss);
        EventBusManager.Instance.NonParamScribe(EventBusManager.EventType.PlayerDead, OnPlayerDead);
        EventBusManager.Instance.ParamScribe(EventBusManager.EventType.DoDialogk,OnDialog);

    }

    private void OnPlayerDead(object sender, EventArgs e)
    {
        SavePlayerStatus();
    }


    private void OnKillBoss(object sender, EventArgs e)
    {
        CurrentBossStatus = true;
    }

    private void OnUnlockAreaAdd(object sender, EventArgs e)
    {
        CurrentDisCoverAreas++;
    }

    private void OnKillEnemy(object sender, EventArgs e)
    {
        CurrentDefeatEnemyVal++;
    }

    private void OnDialog(object sender, EventArgs e)
    {
        var data = e as CustomNPCTEventData;
        if (data!=null)
        {
            CurrentTalkNpcID = data.NPCID;
        }
    }

    private void OnRestore(object sender, EventArgs e)
    {
        Restore();
        taskShortcutBindableTProperty.SetValue("",EventBusManager.EventType.TaskShortCutFresh);
        taskprocessBindableTProperty.SetValue("",EventBusManager.EventType.TaskProcessRefresh);
        EventBusManager.Instance.NonParamPublish(EventBusManager.EventType.HideBossHealth);
    }

    private void OnCarryBoxMinus(object sender, EventArgs e)
    {
        CurrentCarryBox--;
    }

    private void OnCarryBoxAdd(object sender, EventArgs e)
    {
        CurrentCarryBox++;
    }

    public void SavePlayerStatus()
    {
        int index = 0;
        Debug.Log(PlayerTaskStatuslist.Count+ "PlayerTaskStatuslist");
        foreach (var taskInfo in OnProcessTask)
        {
            PlayerTaskStatuslist[index].taskid = taskInfo.Key;
            PlayerTaskStatuslist[index].Status = taskInfo.Value.Status;
            index++;
        }
        SaveData<List<PlayerTaskStatus>>("PlayerTaskStatus", PlayerTaskStatuslist);
        Debug.Log("save playerOnpROCESSTASK and destory");
    }
    void Update()
    {
        Alltaskcomplete = true;
        foreach (var taskinfoVal in OnProcessTask.Values)
        {
            if (taskinfoVal.Status == 0)
            {
                Alltaskcomplete = false;
                bool canUnlock = true;
                foreach (var unlockRuleBase in taskinfoVal.UnlockRuleList)
                {
                    var visitorinterface = unlockRuleBase as UnLockVisitorInterface;
                    if (visitorinterface.Accept(this) == false)
                    {
                        canUnlock = false;
                        if (unlockRuleBase is UnlockRuleChatWithNpc)
                        {
                            CustomChatNPCIDEventData.message = (unlockRuleBase as UnlockRuleChatWithNpc).NPCID;
                            EventBusManager.Instance.ParamPublish(EventBusManager.EventType.DialogNPCID, this, CustomChatNPCIDEventData);
                        }
                        break;
                    }
                }
                if (canUnlock)
                {
                    taskButtonCreateData.message = taskinfoVal.TaskID;
                    EventBusManager.Instance.ParamPublish(EventBusManager.EventType.TaskCreatTaskButton, this, taskButtonCreateData);
                }

            }
            else if (taskinfoVal.Status == 1)
            {
                Alltaskcomplete = false;
                taskButtonCreateData.message = taskinfoVal.TaskID;
                EventBusManager.Instance.ParamPublish(EventBusManager.EventType.TaskCreatTaskButton, this, taskButtonCreateData);
                taskShortcutBindableTProperty.SetValue("<color=yellow>" + OnProcessTask[taskinfoVal.TaskID].TaskTitle+ "</color>", EventBusManager.EventType.TaskShortCutFresh);
                bool isfinish = true;
                foreach (var achieveRuleBase in taskinfoVal.AchieveRuleList)
                {
                    CurrentProcessId = achieveRuleBase.FinishRuleID;

                    var visitorinterface = achieveRuleBase as AchieveVisitorInterface;
                    if (visitorinterface.Accept(this) == false)
                    {
                        isfinish = false;
                        break;
                    }
                }
                if (isfinish)
                {
                    taskinfoVal.Status = 2;
                }
            }
            else if (taskinfoVal.Status == 2)
            {
                taskButtonCreateData.message = taskinfoVal.TaskID;
                EventBusManager.Instance.ParamPublish(EventBusManager.EventType.TaskCreatTaskButton, this, taskButtonCreateData);
            }
            else
            {
                taskButtonDestoryData.message = taskinfoVal.TaskID;
                EventBusManager.Instance.ParamPublish(EventBusManager.EventType.TaskDistoryTaskButton, this, taskButtonDestoryData);
            }


        }

        if (Alltaskcomplete)
        {
            EventBusManager.Instance.NonParamPublish(EventBusManager.EventType.GameEnd);
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                EnableQuest = !EnableQuest;
            }
            if (EnableQuest)
            {
                if (GameUiManager.CurrentUI != GameUIManager.UIType.TaskScreen)
                {
                    GameUiManager.sceneCache[GameUIManager.UIType.MainScene].OnExit(GameUIManager.UIType.MainScene);

                    GameUiManager.sceneCache[GameUIManager.UIType.TaskScreen].OnEnter(GameUIManager.UIType.TaskScreen);
                }

            }
            else
            {
                if (GameUiManager.CurrentUI == GameUIManager.UIType.TaskScreen)
                {
                    SavePlayerStatus();
                    GameUiManager.sceneCache[GameUIManager.UIType.TaskScreen].OnExit(GameUIManager.UIType.TaskScreen);
                    GameUiManager.sceneCache[GameUIManager.UIType.MainScene].OnEnter(GameUIManager.UIType.MainScene);

                }
            }
        }


        
    }



    private void Restore()
    {
        foreach (var value in OnProcessTask.Values)
        {
            value.Status = 0;
        }

        foreach (var val in PlayerTaskStatuslist)
        {
            val.Status = 0;
        }

        CurrentTalkNpcID = 0;
        CurrentDefeatEnemyVal = 0;
        CurrentDisCoverAreas = 1;
        CurrentCarryBox = 0;
        CurrentBossStatus = false;
    }

    public TaskInfo GeTaskInfoDB(int taskid)
    {
        //Debug.Log("receive id" + taskid);

        return OnProcessTask[taskid];
    }
    private void InitHashTable()
    {
        foreach (var achieveRuleBase in AchieveRuleBaseInitList)
        {
            AchieveRuleHash[achieveRuleBase.FinishRuleID] = achieveRuleBase;
        } 
        foreach (var unlockRuleBase in UnlockRuleBaseInitList)
        {
            UnlockRuleHash[unlockRuleBase.UnlockRuleID] = unlockRuleBase;
        }
        
    }

    void ShowUnlockRuleListParam()
    {
        foreach (var taskInfo in TaskInfolist)
        {
            if (taskInfo.UnlockRuleList.Count!=0)
            {
                foreach (var unlockRuleBase in taskInfo.UnlockRuleList)
                {
                    unlockRuleBase.RuleDiscribe = UnlockRuleHash[unlockRuleBase.UnlockRuleID].RuleDiscribe;
                    Debug.Log("targetval"+unlockRuleBase.TargetVal);
                    Debug.Log("formalid"+unlockRuleBase.FormalTaskID);
                    Debug.Log("npcid"+unlockRuleBase.NPCID);
                }
            }
        }
    }
    void ShowAchieveRuleListParam()
    {
        foreach (var taskInfo in TaskInfolist)
        {
            if (taskInfo.AchieveRuleList.Count!=0)
            {
                foreach (var achieveRuleBase in taskInfo.AchieveRuleList)
                {
                    achieveRuleBase.RuleDiscribe = AchieveRuleHash[achieveRuleBase.FinishRuleID].RuleDiscribe;

                    Debug.Log("targetval"+ achieveRuleBase.TargetVal);
                    Debug.Log("formalid"+ achieveRuleBase.IsBossDefated);
                }
            }
        }
    }
    public List<T> LoadData<T>(string Filename)
    {
        string filepath = Path.Combine(Application.dataPath, "JsonData", "Task", Filename + ".json");

        string json = File.ReadAllText(filepath, Encoding.UTF8);
        //Debug.Log("json"+json);

        return JsonConvert.DeserializeObject<List<T>>(json, new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore
        });
        
    }
    public void LoadInfoData(string Filename)
    {
        string filepath = Path.Combine(Application.dataPath, "JsonData", "Task", Filename + ".json");

        string json = File.ReadAllText(filepath, Encoding.UTF8);
        JsonSerializerSettings set = new JsonSerializerSettings
        {
            Converters = new List<JsonConverter> { new TaskDeserializeSetting() },
            NullValueHandling = NullValueHandling.Ignore
        };
        TaskInfolist = JsonConvert.DeserializeObject<List<TaskInfo>>(json,set);

    }
    public void SaveData<T>(string Filename,T saveItems)
    {
        string filepath = Path.Combine(Application.dataPath, "JsonData", "Task", Filename + ".json");
        string json = JsonConvert.SerializeObject(saveItems);
        if (File.Exists(filepath))
        {
            File.Delete(filepath);
            Debug.Log("delete " + filepath);
        }
        File.WriteAllText(filepath, json);


    }

    public bool LoadPlayerTaskData(string Filename)
    {
        string filepath = Path.Combine(Application.dataPath, "JsonData", "Task", Filename + ".json");
        if (File.Exists(filepath))
        {
            string json = File.ReadAllText(filepath, Encoding.UTF8);
            JsonSerializerSettings set = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };
            PlayerTaskStatuslist = JsonConvert.DeserializeObject<List<PlayerTaskStatus>>(json,set );
            Debug.Log("load player Onprocess Task hash success");
            return true;
        }
        else
        {
            Debug.Log(" player Onprocess Task hash not exist");

            return false;
        }
    }

    public bool IsSatisify(AchieveDefateBoss achieveDefateBoss)
    {
        var process = AchieveRuleHash[CurrentProcessId].RuleDiscribe + "当前boss状态<color=red>" + CurrentBossStatus + "</color>";
        taskprocessBindableTProperty.SetValue(process,EventBusManager.EventType.TaskProcessRefresh);
        return achieveDefateBoss.IsBossDefated == CurrentBossStatus;
    }

    public bool IsSatisify(AchieveDefeatNormalEnemyNum achieveNum)
    {
        var process = AchieveRuleHash[CurrentProcessId].RuleDiscribe.Split("%d");
        var text = process[0] + "<color=yellow>" + CurrentDefeatEnemyVal + "</color>/<color=red>" + achieveNum.TargetVal+ "</color>" + process[1];
        taskprocessBindableTProperty.SetValue(text, EventBusManager.EventType.TaskProcessRefresh);
        return CurrentDefeatEnemyVal >= achieveNum.TargetVal;
    }

    public bool IsSatisify(AchieveDiscoverAreaNum achieveNum)
    {
        var process = AchieveRuleHash[CurrentProcessId].RuleDiscribe.Split("%d");
        var text = process[0] + "<color=yellow>" + CurrentDisCoverAreas + "</color>/<color=red>" + achieveNum.TargetVal + "</color>" + process[1];
        taskprocessBindableTProperty.SetValue(text, EventBusManager.EventType.TaskProcessRefresh);
        return CurrentDisCoverAreas == achieveNum.TargetVal;
    }

    public bool IsSatisify(AchieveCarryBoxNum achieveNum)
    {
        var process = AchieveRuleHash[CurrentProcessId].RuleDiscribe.Split("%d");
        var text = process[0] + "<color=yellow>" + CurrentCarryBox + "</color>/<color=red>" + achieveNum.TargetVal + "</color>" + process[1];
        taskprocessBindableTProperty.SetValue(text, EventBusManager.EventType.TaskProcessRefresh);
        return CurrentCarryBox == achieveNum.TargetVal;
    }


    public bool CanUnlock(UnlockRuleNeedFormerTask formerTask)
    {
        //备注：任务执行状态 0 代表未执行 1 代表执行中  2代表已执行但是未领取奖励  3代表已执行已领取奖励
        return  (OnProcessTask[formerTask.FormalTaskID].Status == 2) || (OnProcessTask[formerTask.FormalTaskID].Status==3);
    }

    public bool CanUnlock(UnlockRuleChatWithNpc chatWithNpc)
    {
        return CurrentTalkNpcID == chatWithNpc.NPCID;
    }


}
