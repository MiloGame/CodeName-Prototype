using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class TestUIModel : MonoBehaviour
{
    //public BindableStringProperty bdstring = new BindableStringProperty();
    public BindableTProperty<string> bdstring = new BindableTProperty<string>();
    public string text;

    public int testint = 10;

    public void Start()
    {
        bdstring.GetValue(EventBusManager.EventType.ModelToViewScribe,OnViewChangeModelData); //注册修改自己的事件总线
    }

    private void OnViewChangeModelData(object sender, EventArgs e)
    {
        var data = e as CustomTEventData<string>;
        if (data!=null)
        {
            text = data.message;
        }
        else
        {
            Debug.Log(data +"isnull");
        }
    }

    public void Fresh()
    {
        //Debug.Log("model running"+text);
        bdstring.SetValue(text,EventBusManager.EventType.ViewToModelScribe);
    }
}
