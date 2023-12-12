
using System;
[Serializable]

public class SkillInverterNode : SkillDecorateNode , FuncInterface
{
    public bool InParam;
    public bool OutParam;
    public void OnStart()
    {
    }

    public void OnStop()
    {
    }

    public SkillNodeAllState OnUpdate()
    {
        OutParam = !InParam;
        FuncInterface nodeInterface = ChildNode as FuncInterface;
        if (nodeInterface!=null)
        {
            switch (nodeInterface.UpdateState())
            {
                case SkillNodeAllState.Running:
                    return SkillNodeAllState.Failure;
                case SkillNodeAllState.Failure:
                    return SkillNodeAllState.Success;
                case SkillNodeAllState.Success:
                    return SkillNodeAllState.Failure;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        return SkillNodeAllState.Failure;
    }
}
