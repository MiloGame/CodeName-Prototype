using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks.Triggers;
using UnityEngine;
[Serializable]
public class BossChasePlayerNode : ActionNode, BehaviorTreeFunInterface
{
    private Vector2 targetpos;
    private Vector2 Bosspos;
    private Vector3 reletiveDir;
    private Quaternion rotationTarget;
    public float ChasingTheshold;


    public void OnStart()
    {
        
    }

    public void OnStop()
    {


    }

    public AllState OnUpdate()
    {
        TreeBlackBoard.BTManager.BooFootManager.EnableJump = false;
        TreeBlackBoard.BTManager.BooFootManager.EnableRotate = false;
        TreeBlackBoard.BTManager.BooFootManager.EnableMove = true;
        if (TreeBlackBoard.PlayerTransform != null && TreeBlackBoard.BTManager.BooFootManager.distance == 0 )
        {
            TreeBlackBoard.SelfTransform.position = TreeBlackBoard.PlayerTransform.position + ChasingTheshold*(TreeBlackBoard.SelfTransform.position - TreeBlackBoard.PlayerTransform.position).normalized;
            return AllState.Success;
        }

        return AllState.Failure;

    }

    public string GetNodeInfoAsString()
    {
        string info = $"Title: {titlename}\nWidth: {width}\nHeight: {height}\nPositionX: {positionx}\nPositionY: {positiony}\n" +
                      $"NodeType: {nodetype}\nIsExcuted:{IsExcuted}\n";
        return info;
    }
}
