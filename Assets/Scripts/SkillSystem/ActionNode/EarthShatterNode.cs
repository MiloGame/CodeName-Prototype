using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class EarthShatterNode : SkillActionNode, FuncInterface
{

    public void OnStart()
    {

    }

    public void OnStop()
    {

    }

    public SkillNodeAllState OnUpdate()
    {
        
            EventBusManager.Instance.NonParamPublish(EventBusManager.EventType.EarthShatter);
            //Debug.Log("EarthShatter trigger");
       

        return SkillNodeAllState.Success;
    }
}
