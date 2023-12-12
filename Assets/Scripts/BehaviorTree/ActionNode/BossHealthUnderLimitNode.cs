using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
[Serializable]
public class BossHealthUnderLimitNode : ActionNode, BehaviorTreeFunInterface
{
    public float HealthLimit;

    public void OnStart()
    {
    }

    public void OnStop()
    {
    }

    public AllState OnUpdate()
    {
        if (TreeBlackBoard.CurrentHealth < HealthLimit)
        {
            return AllState.Success;
        }
        else
        {
            return AllState.Failure;
        }
    }

    public string GetNodeInfoAsString()
    {
        string info = $"Title: {titlename}\nWidth: {width}\nHeight: {height}\nPositionX: {positionx}\nPositionY: {positiony}\n" +
                      $"NodeType: {nodetype}\nIsExcuted:{IsExcuted}\n";
        return info;
    }
}
