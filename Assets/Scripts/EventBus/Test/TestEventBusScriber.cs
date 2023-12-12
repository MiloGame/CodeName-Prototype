using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEventBusScriber : MonoBehaviour
{
    public EventBusManager BusManager;
    void Start()
    {
        BusManager = EventBusManager.Instance;
        BusManager.NonParamUnScribe(EventBusManager.EventType.avc,ff3);
        BusManager.NonParamScribe(EventBusManager.EventType.avc, ff1);
        BusManager.NonParamScribe(EventBusManager.EventType.avc, ff2);
        BusManager.ParamScribe(EventBusManager.EventType.bcd,ff4);
    }

    private void ff4(object sender, EventArgs e)
    {
        var mm = e as CustomIntEventData;
        Debug.Log(mm.Theshold+"received1");
    }

    // Update is called once per frame
    void Update()
    {
        BusManager.NonParamScribe(EventBusManager.EventType.avc, ff2);

        if (Input.GetKeyDown(KeyCode.K))
        {
            BusManager.NonParamUnScribe(EventBusManager.EventType.avc,ff1);
        }

    }

    public void ff1(object ob , EventArgs e)
    {
        Debug.Log("1111111111");
    }
    public void ff2(object ob, EventArgs e)
    {
        Debug.Log("2222222222222");
    }
    public void ff3(object ob, EventArgs e)
    {
        Debug.Log("333333333333e");
    }
}
