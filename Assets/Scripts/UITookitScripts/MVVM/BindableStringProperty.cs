using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BindableStringProperty
{
    private string _previousVal;
    private bool isfirsttime = true;
    private CustomTEventData<string> paraCustomTEventData = new CustomTEventData<string>();
    public void SetValue(string value,EventBusManager.EventType eventType)
    {
        //Debug.Log(eventType + " isfirsttime " + isfirsttime + " val " + value + " previous " + _previousVal);
        if (isfirsttime||value!=_previousVal)
        {
            //Debug.Log(eventType + "publish called");
            isfirsttime = false;
            _previousVal = value;
            paraCustomTEventData.message = value;
            EventBusManager.Instance.ParamPublish(eventType,this, paraCustomTEventData);
        }
    }
    public void GetValue(EventBusManager.EventType eventType,EventHandler<EventArgs> OnDataChange)
    {
        EventBusManager.Instance.ParamScribe(eventType,OnDataChange);

    }

    public void UnScribe(EventBusManager.EventType eventType, EventHandler<EventArgs> OnDataChange)
    {
        EventBusManager.Instance.ParamUnScribe(eventType,OnDataChange);
    }
}
