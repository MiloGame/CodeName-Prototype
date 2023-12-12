using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class AimWallNode : SkillActionNode, FuncInterface
{
    public bool OutPutStatus;
    private bool istrigger = false;
    public void OnStart()
    {
        
    }

    public void OnStop()
    {
        
    }

    public SkillNodeAllState OnUpdate()
    {
        

        if (Input.GetKeyUp(KeyCode.E) && istrigger)
        {
            OutPutStatus = true;
            istrigger = false;
            EventBusManager.Instance.NonParamPublish(EventBusManager.EventType.BuildWall);

        }
        else
        {
            OutPutStatus = false;
        }
        if (Input.GetKey(KeyCode.E))
        {
            EventBusManager.Instance.NonParamPublish(EventBusManager.EventType.UpdateWallMaker);
            istrigger = true;
        }

        return SkillNodeAllState.Success;
    }
}


