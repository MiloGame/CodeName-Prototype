using System;
using Assets.Scripts;
[Serializable]
public class GetHealNode : SkillActionNode,FuncInterface
{
    public void OnStart()
    {
        
    }

    public void OnStop()
    {
    }

    public SkillNodeAllState OnUpdate()
    {

            EventBusManager.Instance.NonParamPublish(EventBusManager.EventType.GetHeal);

            return SkillNodeAllState.Success;

    }
}
