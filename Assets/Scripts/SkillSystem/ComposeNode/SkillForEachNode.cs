

using System;
using UnityEngine;
[Serializable]

public class SkillForEachNode : SkillComposeNode , FuncInterface
{
    private int _childindex;
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
        if (nodeInterface != null)
        {
            //switch (nodeInterface.UpdateState())
            //{
            //    case SkillNodeAllState.Running:
            //        return SkillNodeAllState.Running;
            //    case SkillNodeAllState.Failure:
            //        _childindex++;
            //        break;
            //    case SkillNodeAllState.Success:
            //        _childindex++;
            //        break;
            //    default:
            //        throw new ArgumentOutOfRangeException();
            //}
            nodeInterface.UpdateState();
            _childindex++;

        }
        // Debug.Log("for each node index"+_childindex+"child state"+childnode.State);

        return _childindex == ChildrenNodes.Count ? SkillNodeAllState.Success : SkillNodeAllState.Running;
    }
}

