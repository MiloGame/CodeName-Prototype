
using System;
using Assets.Scripts;
using UnityEngine;

[Serializable]
public class SheldSkillNode : SkillActionNode,FuncInterface
{
    public bool isCreate;
    public bool hasCreate=false;
    public void OnStart()
    {
        
    }

    public void OnStop()
    {
        
    }

    public SkillNodeAllState OnUpdate()
    {
        if (isCreate&&!hasCreate)
        {
            hasCreate = true;
            NotificationCenter.Instance.PostNotification("GreateSheld");
            return SkillNodeAllState.Running;
        }
        if (hasCreate&&!isCreate)
        {
            Debug.Log("destory sheld");
            NotificationCenter.Instance.PostNotification("DestorySheld");
            hasCreate = false;
            return SkillNodeAllState.Success;
        }
        return SkillNodeAllState.Failure;
    }
}
