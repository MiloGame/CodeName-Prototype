using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UIElements;
using DataMisalignedException = System.DataMisalignedException;

public class TestUIViewModel : MonoBehaviour
{
    public UIDocument RootUiDocument;
    private VisualElement rootvisualEle;
    private VisualElement custbutton;
    private string clickcss = "hvr-boarder-outlineout--click";
    private TextField custtextfield;
    public TestUIModel Model;

    //public BindableStringProperty TextBindableStringProperty;
    public BindableTProperty<string> TextBindableTProperty;
    private CustomTEventData<string> TextBindEventData;
    private void OnTextFieldChange(object sender, EventArgs e)
    {
        var data = e as CustomStringEventData;
        custtextfield.value = data.message;
        custtextfield.label = data.message;
    }


    void OnEnable()
    {
        rootvisualEle = RootUiDocument.rootVisualElement;
        custbutton=rootvisualEle.Q<VisualElement>("VisualButton");
        custtextfield = rootvisualEle.Q<TextField>("dataupdate");
        custbutton.RegisterCallback<ClickEvent>(OnClick);
        TextBindableTProperty = new BindableTProperty<string>();
        TextBindEventData = new CustomTEventData<string>();
        TextBindableTProperty.GetValue(EventBusManager.EventType.ViewToModelScribe, OnDataChangeText);
        TextBindableTProperty.GetValue(EventBusManager.EventType.ViewToModelScribe, OnDataChangeLabel);

    }

    private void OnDataChangeLabel(object sender, EventArgs e)
    {
        var data = e as CustomTEventData<string>;
        if (data!=null)
        {
            Debug.Log(data.message +" type"+data.message is string);
            custtextfield.label = data.message;
        }
        else
        {
            Debug.Log("data is null label");
        }
    }

    private void OnDataChangeText(object sender, EventArgs e)
    {
        var data = e as CustomTEventData<string>;
        if (data != null)
        {
            custtextfield.value = data.message;
        }
        else
        {
            Debug.Log("data is null text");
        }
    }

    private void OnChangeTextField(ChangeEvent<string> evt)
    {
        var cur = evt.currentTarget as TextField;
        Debug.Log("changed inputtext"+cur.value);
    }

    private void OnClick(ClickEvent evt)
    {
        custbutton.ToggleInClassList("hvr-boarder-outlineout");
        custbutton.ToggleInClassList(clickcss);
        var triggerele = evt.currentTarget as VisualElement;
        Debug.Log(triggerele.name+"click");
    }

    void OnDisable()
    {

    }

    public void Fresh()
    {
        //Debug.Log("view running");
        TextBindableTProperty.SetValue(custtextfield.value,EventBusManager.EventType.ModelToViewScribe);
    }

}
