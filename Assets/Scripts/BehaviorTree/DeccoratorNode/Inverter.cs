using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Inverter : DecoratorNode,BehaviorTreeFunInterface
{
    public void OnStart()
    {
        
    }

    public void OnStop()
    {
        
    }

    public AllState OnUpdate()
    {
        switch (ChildNode.UpdateState())
        {
            case AllState.Running:
                return AllState.Running;
            case AllState.Failure:
                return AllState.Success;
            case AllState.Success:
                return AllState.Failure;
        }

        return AllState.Failure;
    }

    public  string GetNodeInfoAsString()
    {
        string childinfo = "";
        if (ChildNode != null)
        {
            BehaviorTreeFunInterface chiInterface = ChildNode as BehaviorTreeFunInterface;  
            childinfo = chiInterface?.GetNodeInfoAsString();
        }
        string info = $"Title: {titlename}\nWidth: {width}\nHeight: {height}\nPositionX: {positionx}\nPositionY: {positiony}\n" +
                      $"NodeType: {nodetype}\nIsExcuted:{IsExcuted}\nChildNode info below:\n{childinfo}\n";
        return info;
    }
}
