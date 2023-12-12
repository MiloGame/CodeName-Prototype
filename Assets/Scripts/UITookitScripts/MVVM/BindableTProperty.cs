

using System;
using UnityEngine;

public class BindableTProperty<T>
{
    private T _previousVal;
    private bool isfirsttime = true;
    private CustomTEventData<T> paraCustomTEventData = new CustomTEventData<T>();
    public void SetValue(T value, EventBusManager.EventType eventType)
    {
        //Debug.Log(eventType + " isfirsttime " + isfirsttime + " val " + value + " previous " + _previousVal);
        if (isfirsttime || !value.Equals( _previousVal))
        {
            //Debug.Log(eventType + "publish called");
            isfirsttime = false;
            _previousVal = value;
            paraCustomTEventData.message = value;
            //Debug.Log(paraCustomTEventData.message);
            EventBusManager.Instance.ParamPublish(eventType, this, paraCustomTEventData);
        }
    }
    public void GetValue(EventBusManager.EventType eventType, EventHandler<EventArgs> OnDataChange)
    {
        EventBusManager.Instance.ParamScribe(eventType, OnDataChange);

    }

    public void UnScribe(EventBusManager.EventType eventType, EventHandler<EventArgs> OnDataChange)
    {
        EventBusManager.Instance.ParamUnScribe(eventType, OnDataChange);
    }
}
