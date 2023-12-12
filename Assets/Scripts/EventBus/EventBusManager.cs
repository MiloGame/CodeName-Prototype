
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EventBusManager 
{
    public enum EventType
    {
        avc,
        bcd,
        ViewToModelScribe,
        ModelToViewScribe,
        ChangeGameState,
        ModelChangeRemains,
        ModelChangeTotal,
        CanFire,
        GrenadeEffect,
        ClearGrenadeTrail,
        EarthShatter,
        GetHeal,
        ECoolen,
        QCoolen,
        HCoolen,
        JCoolen,
        UpdateWallMaker,
        BuildWall,
        ControlEnemy,
        DialogRefreshName,
        DialogRefreshText,
        DisAblePlayerInput,
        EnablePlayerInput,
        DialogCreateNewButton,
        DialogDistoryButton,
        DialogCanProceed,
        DialogCanNotProceed,
        TaskCreatTaskButton,
        TaskDistoryTaskButton,

        CarryBoxToPosAdd,
        CarryBoxToPosMinus,
        RestoreTaskStatus,
        DoDialogk,
        DialogFinish,
        DialogNPCID,
        PlayerHealthChange,
        KillEnemyTask,
        LoadingScreenPersent,
        TaskProcessRefresh,
        TaskShortCutFresh,
        UnlockAreaAdd,
        KillBoss,
        BossHealthChange,
        ShowBossHealth,
        HideBossHealth,
        GameEnd,
        PlayerDead,
        Restart
    }

    public EventBusManager()
    {
        
        nonParamEvents = new Dictionary<EventType, NonParamEvent>();
        ParamEvents = new Dictionary<EventType, ParamEvent>();

    }

    private static EventBusManager _evnetBusManager;
    private Dictionary<EventType, NonParamEvent> nonParamEvents;
    //private Queue<EventType> nonParamEventsPublishQueue;
    private Dictionary<EventType, ParamEvent> ParamEvents ;
    //private Dictionary<EventType,EmitData> ParamEventsPublishDir;
    public static EventBusManager Instance
    {
        get
        {
            if (_evnetBusManager == null)
            {
                _evnetBusManager = new EventBusManager();
            }

            return _evnetBusManager;
        }
    }

    //private EventBusManager()
    //{
    //    nonParamEvents = new Dictionary<EventType, NonParamEvent>();
    //    ParamEvents = new Dictionary<EventType, ParamEvent>();
    //}
    public void NonParamScribe(EventType eventType, EventHandler eventHandler)
    {
        if (nonParamEvents.ContainsKey(eventType))
        {
            nonParamEvents[eventType].Add(eventHandler);
        }
        else
        {
            nonParamEvents[eventType] = new NonParamEvent(eventType);
            nonParamEvents[eventType].Add(eventHandler);
        }
    }

    public void NonParamUnScribe(EventType eventType, EventHandler eventHandler)
    {
        if (nonParamEvents.ContainsKey(eventType))
        {
            nonParamEvents[eventType].Del(eventHandler);
        }
        else
        {
            Debug.Log("dict not exist");
        }
    }

    public void NonParamPublish(EventType eventType)
    {
        if (nonParamEvents.ContainsKey(eventType))
        {
            nonParamEvents[eventType].Publish();
            //nonParamEventsPublishQueue.Enqueue(eventType);
        }
        else
        {
            Debug.Log("not exist publisher");
        }
    }

    public void ParamScribe(EventType eventType, EventHandler<EventArgs> eventHandler) 
    {
        if (ParamEvents.ContainsKey(eventType))
        {
            ParamEvents[eventType].Add(eventHandler);
        }
        else
        {
            ParamEvents[eventType] = new ParamEvent(eventType);
            ParamEvents[eventType].Add(eventHandler);
        }
        //foreach (var data in ParamEvents)
        //{
        //    Debug.Log(data.Key + "value" + data.Value.eventlistcount);
        //}
    }

    public void ParamUnScribe(EventType eventType, EventHandler<EventArgs> eventHandler)
    {
        if (ParamEvents.ContainsKey(eventType))
        {
            ParamEvents[eventType].Del(eventHandler);
        }
        else
        {
            Debug.Log("dict not exist");
        }
        //foreach (var data in ParamEvents)
        //{
        //    Debug.Log(data.Key + "value" + data.Value.eventlistcount);
        //}
    }

    public void ParamPublish(EventType eventType,object ob,EventArgs param) 
    {
        //var emitdata = new EmitData();
        //emitdata.ob = ob;
        //emitdata.param = param;
        //emitdata.eventType = eventType;
        //ParamEventsPublishDir[eventType] = emitdata;
        //foreach (var data in ParamEventsPublishDir)
        //{
        //    Debug.Log(data.Key + "value" + data.Value.ob);
        //}
        if (ParamEvents.ContainsKey(eventType))
        {
            ParamEvents[eventType].Publish(ob, param);




        }
        else
        {
            Debug.Log("not exist publisher");
        }
    }

    //public void LateUpdate()
    //{
    //    //while(nonParamEventsPublishQueue.Count!=0)
    //    //{
    //    //    //Debug.Log("nonParamEventsPublishQueue.Count"+ nonParamEventsPublishQueue.Count);
    //    //    var queuehead = nonParamEventsPublishQueue.Dequeue();
    //    //    nonParamEvents[queuehead].Publish();
    //    //}

    //    foreach (var data in ParamEventsPublishDir)
    //    {
    //        var queuehead = data.Value;

    //        ParamEvents[queuehead.eventType].Publish(queuehead.ob, queuehead.param);
    //    }
    //    ParamEventsPublishDir.Clear();

    //}
}
