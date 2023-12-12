using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[Serializable]
public class ForEachNode : CompositeNode, BehaviorTreeFunInterface
{
    private int childindex;

    public void OnStart()
    {
        childindex = 0;
    }

    public void OnStop()
    {
        
    }

    public AllState OnUpdate()
    {
        var childnode = ChildreNodes[childindex];
        childnode.UpdateState();
        childindex++;
        
        return childindex == ChildreNodes.Count ? AllState.Success : AllState.Running;
    }

    public string GetNodeInfoAsString()
    {
        string total = "";
        foreach (var childreNode in ChildreNodes)
        {
            BehaviorTreeFunInterface chiInterface = childreNode as BehaviorTreeFunInterface;
            total += chiInterface.GetNodeInfoAsString();
        }

        total += "\n";
        string info = $"Title: {titlename}\nWidth: {width}\nHeight: {height}\nPositionX: {positionx}\nPositionY: {positiony}\n" +
                      $"NodeType: {nodetype} \nIsExcuted:{IsExcuted}\nChildreNodes info below:\n" + total;
        return info;
    }
}
