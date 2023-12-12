using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    public TextAsset Dialog;

    public GameUIManager GameUiManager;
    private CustomTEventData<string> nameEventData;
    private CustomTEventData<string> dialogEventData;
    private CustomNPCTEventData OnProcessDialogData;
    private CustomButtonTEventData dialogButtonEventData;
    private int dialogIndex;
    private string[] rows;
    private string[] dialogcells;
    private bool canProceed;

    public int DialogIndex
    {
        get
        {
            return dialogIndex;
        }
        set
        {
            dialogIndex = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        dialogButtonEventData = new CustomButtonTEventData();
        dialogIndex = 0;
        canProceed = false;
        nameEventData = new CustomTEventData<string>();
        dialogEventData = new CustomTEventData<string>();
        EventBusManager.Instance.ParamScribe(EventBusManager.EventType.DoDialogk,OnDialog );
    }

    private void OnDialog(object sender, EventArgs e)
    {
        var data = e as CustomNPCTEventData;
        if (data!=null)
        {
            OnProcessDialogData = new CustomNPCTEventData(data);

            if (GameUiManager.CurrentUI != GameUIManager.UIType.Dialog)
            {
                canProceed = true;
                dialogIndex = 0;
                Dialog = data.DialogTextAsset;
                EventBusManager.Instance.NonParamScribe(EventBusManager.EventType.DialogCanProceed, OnCanProceed);
                EventBusManager.Instance.NonParamScribe(EventBusManager.EventType.DialogCanNotProceed, OnCanNotProceed);
                EventBusManager.Instance.NonParamPublish(EventBusManager.EventType.DisAblePlayerInput);
                GameUiManager.sceneCache[GameUIManager.UIType.MainScene].OnExit(GameUIManager.UIType.MainScene);
                ReadText();

                GameUiManager.sceneCache[GameUIManager.UIType.Dialog].OnEnter(GameUIManager.UIType.Dialog);
                ShowDialogRow();
            }

        }
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && canProceed)
        {
            ShowDialogRow();
        }
    }
    // Update is called once per frame
    //void Update()
    //{
    //    if (enablbinput)
    //    {
    //        if (GameUiManager.CurrentUI == GameUIManager.UIType.Dialog)
    //        {
    //            EventBusManager.Instance.NonParamUnScribe(EventBusManager.EventType.DialogCanProceed,OnCanProceed);
    //            EventBusManager.Instance.NonParamUnScribe(EventBusManager.EventType.DialogCanNotProceed,OnCanNotProceed);
    //            EventBusManager.Instance.NonParamPublish(EventBusManager.EventType.EnablePlayerInput);
    //            GameUiManager.sceneCache[GameUIManager.UIType.Dialog].OnExit(GameUIManager.UIType.Dialog);
    //            GameUiManager.sceneCache[GameUIManager.UIType.MainScene].OnEnter(GameUIManager.UIType.MainScene);
    //        }

    //    }
    //    else
    //    {

    //        if (GameUiManager.CurrentUI != GameUIManager.UIType.Dialog)
    //        {
    //            EventBusManager.Instance.NonParamScribe(EventBusManager.EventType.DialogCanProceed, OnCanProceed);
    //            EventBusManager.Instance.NonParamScribe(EventBusManager.EventType.DialogCanNotProceed, OnCanNotProceed);
    //            EventBusManager.Instance.NonParamPublish(EventBusManager.EventType.DisAblePlayerInput);
    //            GameUiManager.sceneCache[GameUIManager.UIType.MainScene].OnExit(GameUIManager.UIType.MainScene);

    //            GameUiManager.sceneCache[GameUIManager.UIType.Dialog].OnEnter(GameUIManager.UIType.Dialog);
    //            ShowDialogRow();
    //        }
    //        if (Input.GetKeyDown(KeyCode.M) && canProceed)
    //        {
    //            ShowDialogRow();
    //            //CreateOptionButton("ceshishishsihsishishsihsihs不啊大苏打大大");
    //            //UpdateUIText("我i我", "awdasdasasdas阿萨大大大撒旦撒大苏打实打实大大");
    //        }
    //    }

        

    //    //if (Input.GetKeyDown(KeyCode.Delete))
    //    //{
    //    //    DestoryAllOptionButton();
    //    //}
    //}

    private void OnCanNotProceed(object sender, EventArgs e)
    {
        canProceed = false;
    }

    private void OnCanProceed(object sender, EventArgs e)
    {
        canProceed = true;
    }

    void CreateOptionButton(int ButtonRow)
    {
        string[] cells = rows[ButtonRow].Split(',');
        //foreach (var cell in cells)
        //{
        //    Debug.Log("rows"+ ButtonRow+"cell"+cell);
        //}
        if (cells[0] == "&")
        {
            //Debug.Log("rows" + ButtonRow + "cell[4]" + int.Parse(cells[4]));

            dialogButtonEventData.message = cells[3];
            dialogButtonEventData.ToJumpIndex = int.Parse(cells[4]);
            dialogButtonEventData.DialogManager = this;
            Debug.Log("publish param"+ dialogButtonEventData.message+ dialogButtonEventData.ToJumpIndex+ ButtonRow);
            EventBusManager.Instance.ParamPublish(EventBusManager.EventType.DialogCreateNewButton, this, dialogButtonEventData);
            CreateOptionButton(ButtonRow+1);
        }
    }
    void DestoryAllOptionButton()
    {
        EventBusManager.Instance.NonParamPublish(EventBusManager.EventType.DialogDistoryButton);
    }
    void ReadText()
    {
        rows = Dialog.text.Split('\n');

    }

    public void ShowDialogRow()
    {
        for (int i = 0; i < rows.Length; i++)
        {
            dialogcells = rows[i].Split(',');
            //Debug.Log("0: "+dialogcells[0]+" 1:"+dialogcells[1]);
            if (dialogcells[0]=="#"&&int.Parse(dialogcells[1]) == dialogIndex)
            {
                DestoryAllOptionButton();
                UpdateUIText(dialogcells[2],dialogcells[3]);
                //Debug.Log("4:"+dialogcells[4]);
                dialogIndex = int.Parse(dialogcells[4]);
                break;
            }else if (dialogcells[0]== "&" && int.Parse(dialogcells[1]) == dialogIndex)
            {
                CreateOptionButton(i);
            }else if (dialogcells[0]== "END" && int.Parse(dialogcells[1]) == dialogIndex)
            {
                canProceed = false;
                EventBusManager.Instance.ParamPublish(EventBusManager.EventType.DialogFinish,this, OnProcessDialogData);
                if (GameUiManager.CurrentUI == GameUIManager.UIType.Dialog)
                {
                    EventBusManager.Instance.NonParamUnScribe(EventBusManager.EventType.DialogCanProceed, OnCanProceed);
                    EventBusManager.Instance.NonParamUnScribe(EventBusManager.EventType.DialogCanNotProceed, OnCanNotProceed);
                    EventBusManager.Instance.NonParamPublish(EventBusManager.EventType.EnablePlayerInput);
                    GameUiManager.sceneCache[GameUIManager.UIType.Dialog].OnExit(GameUIManager.UIType.Dialog);
                    GameUiManager.sceneCache[GameUIManager.UIType.MainScene].OnEnter(GameUIManager.UIType.MainScene);
                }
            }
        }
    }

    void UpdateUIText(string name,string text)
    {
        nameEventData.message = name;
        dialogEventData.message =text;
        EventBusManager.Instance.ParamPublish(EventBusManager.EventType.DialogRefreshName,this,nameEventData);
        EventBusManager.Instance.ParamPublish(EventBusManager.EventType.DialogRefreshText,this,dialogEventData);
    }
}
