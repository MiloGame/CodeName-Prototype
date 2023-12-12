using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class GameMainSceneViewModel : MonoBehaviour,ViewModelInterface
{
    public GameUIManager GameUiManager;
    VisualElement RootVisualElement;
    private VisualElement WeaponBar;
    private VisualElement MenuBar;
    private VisualElement SkillBar;
    private VisualElement Boss;
    private VisualElement Minimap;
    private VisualElement Ammo;
    private Label SettingButton;
    private Label Total;
    private Label Remainammo;
    private BindableTProperty<int> totalBindableTProperty;
    private BindableTProperty<int> remainBindableTProperty;
    private Label QCoolcen;
    private Label ECoolen;
    private Label HCoolen;
    private Label JCoolen;
    private BindableTProperty<float> QcoolBindableTProperty;
    private BindableTProperty<float> EcoolBindableTProperty;
    private BindableTProperty<float> HcoolBindableTProperty;
    private BindableTProperty<float> JcoolBindableTProperty;
    private BindableTProperty<float> HealthBindableTProperty;
    private BindableTProperty<float> BossHealthBindableTProperty;
    private VisualElement HealthFill;
    private Label HealthVal;
    private Label Taskprogress;
    private BindableTProperty<string> taskprocessBindableTProperty;
    private BindableTProperty<string> taskshortcutBindableTProperty;
    private Label TaskShortCut;
    private VisualElement BossHealthBar;

    void Start()
    {
        GameUiManager.LogToDir(GameUIManager.UIType.MainScene,this as ViewModelInterface);
        RootVisualElement = GameUiManager.sceneDictionary[GameUIManager.UIType.MainScene];
        WeaponBar = RootVisualElement.Q<VisualElement>("WeaponBar");
        MenuBar = RootVisualElement.Q<VisualElement>("MenuBar");
        SkillBar = RootVisualElement.Q<VisualElement>("SkillBar");
        HealthFill = RootVisualElement.Q<VisualElement>("FillAmount");
        HealthVal = RootVisualElement.Q<Label>("LoadPersent");
        Boss = RootVisualElement.Q<VisualElement>("Boss");
        BossHealthBar = Boss.Q<VisualElement>("BossFillAmount");
        QCoolcen = SkillBar.Q<Label>("QCool");
        ECoolen = SkillBar.Q<Label>("ECool");
        HCoolen = SkillBar.Q<Label>("HCool");
        JCoolen = SkillBar.Q<Label>("JCool");
        QcoolBindableTProperty = new BindableTProperty<float>();
        EcoolBindableTProperty = new BindableTProperty<float>();
        HcoolBindableTProperty = new BindableTProperty<float>();
        JcoolBindableTProperty = new BindableTProperty<float>();
        HealthBindableTProperty = new BindableTProperty<float>();
        BossHealthBindableTProperty = new BindableTProperty<float>();
        Minimap = RootVisualElement.Q<VisualElement>("minimapgroup");
        Ammo = RootVisualElement.Q<VisualElement>("Ammo");
        SettingButton = MenuBar.Q<Label>("Settings");
        Remainammo = Ammo.Q<Label>("Remain");
        Total = Ammo.Q<Label>("Total");
        totalBindableTProperty = new BindableTProperty<int>();
        remainBindableTProperty = new BindableTProperty<int>();
        TaskShortCut = RootVisualElement.Q<Label>("TaskShortCut");
        Taskprogress = RootVisualElement.Q<Label>("Taskprogress");
        taskprocessBindableTProperty = new BindableTProperty<string>();
        taskshortcutBindableTProperty = new BindableTProperty<string>();
        taskshortcutBindableTProperty.GetValue(EventBusManager.EventType.TaskShortCutFresh, OnTaskShortCutFresh);
        taskprocessBindableTProperty.GetValue(EventBusManager.EventType.TaskProcessRefresh, OnTaskProgressFresh);
        TaskShortCut.text = "";
        Taskprogress.text = "";
        Boss.style.display = DisplayStyle.None;
    }
    private void OnTaskShortCutFresh(object sender, EventArgs e)
    {
        var data = e as CustomTEventData<string>;
        if (data != null)
        {
            TaskShortCut.text = data.message;
        }
    }

    private void OnTaskProgressFresh(object sender, EventArgs e)
    {
        var data = e as CustomTEventData<string>;
        if (data != null)
        {
            Taskprogress.text = data.message;
        }
    }
    public void Fresh()
    {
        
    }

    public void OnEnter(GameUIManager.UIType uiType)
    {
        GameUiManager.ActivateScene(uiType);
        SettingButton.RegisterCallback<ClickEvent>(OnClickSetting);
        remainBindableTProperty.GetValue(EventBusManager.EventType.ModelChangeRemains,OnRemainChange);
        totalBindableTProperty.GetValue(EventBusManager.EventType.ModelChangeTotal,OnTotalChange);
        QcoolBindableTProperty.GetValue(EventBusManager.EventType.QCoolen,OnQSkillCoolen);
        EcoolBindableTProperty.GetValue(EventBusManager.EventType.ECoolen,OnESkillCoolen);
        HcoolBindableTProperty.GetValue(EventBusManager.EventType.HCoolen,OnHSkillCoolen);
        JcoolBindableTProperty.GetValue(EventBusManager.EventType.JCoolen,OnJSkillCoolen);
        HealthBindableTProperty.GetValue(EventBusManager.EventType.PlayerHealthChange, OnPlayerHealthChange);
        BossHealthBindableTProperty.GetValue(EventBusManager.EventType.BossHealthChange, OnBossHealthChange);
        EventBusManager.Instance.NonParamScribe(EventBusManager.EventType.ShowBossHealth,OnShowBossHealth);
        EventBusManager.Instance.NonParamScribe(EventBusManager.EventType.HideBossHealth,OnHideBossHealth);
    }

    private void OnHideBossHealth(object sender, EventArgs e)
    {
        Boss.style.display = DisplayStyle.None;
    }

    private void OnShowBossHealth(object sender, EventArgs e)
    {
        Boss.style.display = DisplayStyle.Flex;
    }

    private void OnBossHealthChange(object sender, EventArgs e)
    {
        var  data = e as CustomTEventData<float>;
        if (data!=null)
        {
            BossHealthBar.style.translate = new Translate(Length.Percent(data.message - 100), 0, 0);

        }
    }

    private void OnPlayerHealthChange(object sender, EventArgs e)
    {
        var data = e as CustomTEventData<float>;
        if (data != null)
        {
            HealthVal.text = data.message.ToString();
            HealthFill.style.translate =   new Translate(Length.Percent(data.message - 100), 0, 0);
        }
    }

    private void OnJSkillCoolen(object sender, EventArgs e)
    {
        var data = e as CustomTEventData<float>;
        if (data != null)
        {
            
            JCoolen.text = (data.message.ToString("0.0") == "0.0") ? "" : data.message.ToString("0.0");
        }
    }

    private void OnHSkillCoolen(object sender, EventArgs e)
    {
        var data = e as CustomTEventData<float>;
        if (data != null)
        {
            HCoolen.text = (data.message.ToString("0.0") == "0.0") ? "" : data.message.ToString("0.0");
        }
    }

    private void OnESkillCoolen(object sender, EventArgs e)
    {
        var data = e as CustomTEventData<float>;
        if (data != null)
        {
            ECoolen.text = (data.message.ToString("0.0") == "0.0") ? "" : data.message.ToString("0.0");
        }
    }

    private void OnQSkillCoolen(object sender, EventArgs e)
    {
        var data = e as CustomTEventData<float>;
        if (data != null)
        {
            QCoolcen.text = (data.message.ToString("0.0") == "0.0" )? "" : data.message.ToString("0.0");
        }
    }

    private void OnClickSetting(ClickEvent evt)
    {
        OnExit(GameUIManager.UIType.MainScene);
        GameUiManager.sceneCache[GameUIManager.UIType.CloseWindow].OnEnter(GameUIManager.UIType.CloseWindow);
    }

    private void OnTotalChange(object sender, EventArgs e)
    {
        var data = e as CustomTEventData<int>;
        if (data != null)
        {
            Total.text = data.message.ToString();
        }
    }

    private void OnRemainChange(object sender, EventArgs e)
    {
        var data = e as CustomTEventData<int>;
        if (data!=null)
        {
            Remainammo.text = data.message.ToString();
        }
    }

    public void OnExit(GameUIManager.UIType uiType)
    {
        GameUiManager.DeActivateScene(uiType);
        SettingButton.UnregisterCallback<ClickEvent>(OnClickSetting);
        remainBindableTProperty.UnScribe(EventBusManager.EventType.ModelChangeRemains, OnRemainChange);
        totalBindableTProperty.UnScribe(EventBusManager.EventType.ModelChangeTotal, OnTotalChange);

    }
}
