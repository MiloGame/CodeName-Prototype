using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParamEvent
{
    private EventBusManager.EventType _type;
    public ParamEvent(EventBusManager.EventType type)
    {
        _type = type;
    }
    event EventHandler<EventArgs> EventList;

    public int eventlistcount
    {
        get
        {
            if (EventList!=null)
            {
                return EventList.GetInvocationList().Length;
            }
            else
            {
                return -1;
            }
            
        }
    }

    public void Add(EventHandler<EventArgs> memberHandler)
    {
        EventList -= memberHandler; // ∑¿÷π÷ÿ∏¥ÃÌº”
        EventList += memberHandler;
    }

    public void Del(EventHandler<EventArgs> memHandler)
    {
        EventList -= memHandler;
    }

    public void Publish(object ob, EventArgs param)
    {
        //Debug.Log(_type+" length event" + EventList.GetInvocationList().Length);
        EventList?.Invoke(ob, param);
    }
}
