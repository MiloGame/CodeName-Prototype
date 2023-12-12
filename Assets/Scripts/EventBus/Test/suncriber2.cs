using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class suncriber2 : MonoBehaviour
{
    public EventBusManager eb;
    // Start is called before the first frame update
    void Start()
    {
        eb = EventBusManager.Instance;
        eb.ParamScribe(EventBusManager.EventType.bcd,ff);
    }

    private void ff(object sender, EventArgs e)
    {
 
        Debug.Log((e as CustomIntEventData).Theshold+"scribe2"+sender.GetType());
    }

    // Update is called once per frame
    void OnDisable()
    {
        eb.ParamUnScribe(EventBusManager.EventType.bcd, ff);
    }
}
