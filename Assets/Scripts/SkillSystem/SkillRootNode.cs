using System;

[Serializable]

public class SkillRootNode : SkillBaseNode , FuncInterface
{
    public SkillBaseNode ChildNode;
    public void OnStart()
    {
        
    }

    public void OnStop()
    {
    }

    public SkillNodeAllState OnUpdate()
    {
        return ChildNode.UpdateState();
    }
}
