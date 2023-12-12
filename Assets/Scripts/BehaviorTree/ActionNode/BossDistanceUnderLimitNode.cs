using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
[Serializable]
public class BossDistanceUnderLimitNode : ActionNode, BehaviorTreeFunInterface
{
    public float DistanceLit;
    private Vector2 transplane =new Vector2();
    private Vector2 playplane = new Vector2();
    public void OnStart()
    {

    }

    public void OnStop()
    {
    }

    public AllState OnUpdate()
    {
        transplane.x = TreeBlackBoard.BTManager.BooFootManager.Leftfoot.position.x;
        transplane.y = TreeBlackBoard.BTManager.BooFootManager.Leftfoot.position.z;
        playplane.x = TreeBlackBoard.PlayerTransform.position.x;
        playplane.y = TreeBlackBoard.PlayerTransform.position.z;
        TreeBlackBoard.remainDis =
            Vector2.Distance(transplane,playplane);
        if (TreeBlackBoard.remainDis < DistanceLit)
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
