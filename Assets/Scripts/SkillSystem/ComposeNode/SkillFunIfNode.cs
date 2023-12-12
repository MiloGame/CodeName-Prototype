using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]

public class SkillFunIfNode : SkillComposeNode, FuncInterface
{

    public bool InParam;
    public bool OutPutStatus;
    public void OnStart()
    {
    }

    public void OnStop()
    {
        
    }

    public SkillNodeAllState OnUpdate()
    {
        if (InParam)
        {
            OutPutStatus = true;
            var childnode = ChildrenNodes[0];
            FuncInterface nodeInterface = childnode as FuncInterface;
            if (nodeInterface!=null)
            {
                nodeInterface.UpdateState();
            }
            return SkillNodeAllState.Success;

        }
        else
        {
            OutPutStatus = false;
            if (ChildrenNodes.Count >=2)
            {
                var childnode = ChildrenNodes[1];
                FuncInterface nodeInterface = childnode as FuncInterface;
                if (nodeInterface != null)
                {
                    nodeInterface.UpdateState();
                }
            }
            

            return SkillNodeAllState.Failure;
        }
        
    }
}
