using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class AttAckPlayerNode : ActionNode, BehaviorTreeFunInterface
{
    public void OnStart()
    {
        
    }

    public void OnStop()
    {
        
    }

    public AllState OnUpdate()
    {
        TreeBlackBoard.EnemyAnimator.SetBool("IsAttack",true);
        return AllState.Success;
    }

    public string GetNodeInfoAsString()
    {
        string info = $"Title: {titlename}\nWidth: {width}\nHeight: {height}\nPositionX: {positionx}\nPositionY: {positiony}\n" +
                      $"NodeType: {nodetype}\nIsExcuted:{IsExcuted}\n";
        return info;
    }
}
