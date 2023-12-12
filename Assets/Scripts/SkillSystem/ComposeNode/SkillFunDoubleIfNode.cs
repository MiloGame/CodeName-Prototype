using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class SkillFunDoubleIfNode : SkillComposeNode, FuncInterface
{
    public bool InParam1;
    public bool InParam2;
    public bool OutputStatus;
    public void OnStart()
    {
        
    }

    public void OnStop()
    {
        
    }

    public SkillNodeAllState OnUpdate()
    {

        if (InParam1 && InParam2 )
        {
            OutputStatus = true;
            if (ChildrenNodes.Count > 0)
            {
                var childnode = ChildrenNodes[0];
                FuncInterface nodeInterface = childnode as FuncInterface;
                if (nodeInterface != null)
                {
                    nodeInterface.UpdateState();
                }
            }

            return SkillNodeAllState.Success;


        }
        else
        {
            OutputStatus = false;
            if (ChildrenNodes.Count >= 2)
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
