using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class ArmGrenadeNode : SkillActionNode,FuncInterface
{
    public float radius = 0;
    private float maxradius = 5f;
    public bool NeedClear;
    private CustomTEventData<float> radiusData = new CustomTEventData<float>();
    public void OnStart()
    {
       
    }

    public void OnStop()
    {

    }

    public SkillNodeAllState OnUpdate()
    {
        if (NeedClear)
        {
            radius += Time.deltaTime * 3f;
            radius = Mathf.Clamp(radius, 0, maxradius);
            radiusData.message = radius;
            EventBusManager.Instance.ParamPublish(EventBusManager.EventType.GrenadeEffect, this, radiusData);
        }
        else
        {
            radius = 0;
        }
        
        return SkillNodeAllState.Success;
    }
}
