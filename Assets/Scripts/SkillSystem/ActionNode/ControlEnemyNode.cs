using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class ControlEnemyNode : SkillActionNode, FuncInterface
{
    public bool OutPutStatus;
    public bool InParam;
    public void OnStart()
    {

    }

    public void OnStop()
    {
    }

    public SkillNodeAllState OnUpdate()
    {
        EventBusManager.Instance.NonParamPublish(EventBusManager.EventType.ControlEnemy);
        return SkillNodeAllState.Success;
    }
}


