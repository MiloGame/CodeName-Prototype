using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;
[Serializable]
public class GetHurtNode : SkillActionNode,FuncInterface
{
    public bool IsHurt = false;

    public void OnStart()
    {
    }

    public void OnStop()
    {
    }

    public SkillNodeAllState OnUpdate()
    {
        if (IsHurt)
        {
            Debug.Log("gethurt post success");
            NotificationCenter.Instance.PostNotification("GetHurt");
            return SkillNodeAllState.Success;
        }
        return SkillNodeAllState.Failure;

    }
}
