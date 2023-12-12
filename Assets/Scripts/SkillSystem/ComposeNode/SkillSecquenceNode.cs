
using System;

[Serializable]

public class SkillSecquenceNode : SkillComposeNode,FuncInterface
{
    private int _childindex;
    //public string m1 = "";
    //public string m2 = "";
    //public string m3 = "";
    public void OnStart()
    {
        _childindex = 0;
    }

    public void OnStop()
    {
    }

    public SkillNodeAllState OnUpdate()
    {
       
        var childnode = ChildrenNodes[_childindex];
        FuncInterface nodeInterface = childnode as FuncInterface;
        if (nodeInterface!=null)
        {
            switch (nodeInterface.UpdateState())
            {
                case SkillNodeAllState.Running:
                    return SkillNodeAllState.Running;
                case SkillNodeAllState.Failure:
                    return SkillNodeAllState.Failure;
                case SkillNodeAllState.Success:
                    _childindex++;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        return _childindex == ChildrenNodes.Count ? SkillNodeAllState.Success : SkillNodeAllState.Running;
    }
}
