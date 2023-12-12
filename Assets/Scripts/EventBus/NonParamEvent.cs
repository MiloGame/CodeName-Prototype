using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NonParamEvent
{
    private EventBusManager.EventType _type;
    public NonParamEvent(EventBusManager.EventType type)
    {
        _type = type;
    }
    private event EventHandler EventList;
    public void Add(EventHandler memberHandler)
    {
        EventList -= memberHandler; // ∑¿÷π÷ÿ∏¥ÃÌº”
        EventList += memberHandler;
    }

    public void Del(EventHandler memHandler)
    {
        EventList -= memHandler;
    }

    public void Publish()
    {
        //Debug.Log(_type+" length event" + EventList.GetInvocationList().Length);
        EventList?.Invoke(this,EventArgs.Empty);
    }
}
