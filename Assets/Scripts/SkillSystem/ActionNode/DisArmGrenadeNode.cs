using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class DisArmGrenadeNode : SkillActionNode,FuncInterface
{
    public float resetRadius;

    public void OnStart()
    {
        resetRadius = 0;
    }

    public void OnStop()
    {

    }

    public SkillNodeAllState OnUpdate()
    {
        EventBusManager.Instance.NonParamPublish(EventBusManager.EventType.ClearGrenadeTrail);
        return SkillNodeAllState.Success;
    }
}
