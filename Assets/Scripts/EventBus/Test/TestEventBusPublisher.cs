using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEventBusPublisher : MonoBehaviour
{
    public EventBusManager BusManager;
    private CustomIntEventData ii;
    private int cnt = 0;
    // Start is called before the first frame update
    void Start()
    {
        BusManager = EventBusManager.Instance;
        ii = new CustomIntEventData();
        ii.Theshold = 11;

    }

    // Update is called once per frame
    void Update()
    {
        BusManager.NonParamPublish(EventBusManager.EventType.avc);
        BusManager.ParamPublish(EventBusManager.EventType.bcd,this,ii);
        cnt++;
    }
}
