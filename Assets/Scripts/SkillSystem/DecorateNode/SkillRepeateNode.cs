using System;

[Serializable]

public class SkillRepeateNode : SkillDecorateNode , FuncInterface
{
    public void OnStart()
    {
        
    }

    public void OnStop()
    {
    }

    public SkillNodeAllState OnUpdate()
    {
        FuncInterface nodeinterface = ChildNode as FuncInterface;
        if (nodeinterface != null)
        {
            nodeinterface.UpdateState();
            return SkillNodeAllState.Running;
        }
        else
        {
            throw new System.NotImplementedException();
        }
        
    }
}
