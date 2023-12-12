

using System;
using UnityEngine;
[Serializable]
public class IsCoveredNode : ActionNode,BehaviorTreeFunInterface
{
    [NonSerialized]private Transform targeTransform;
    [NonSerialized]private Transform selfTransform;
    public void OnStart()
    {
        //targeTransform = TreeBlackBoard.PlayerTransform;
        selfTransform = TreeBlackBoard.SelfTransform;
    }

    public void OnStop()
    {
    }

    public AllState OnUpdate()
    {
        RaycastHit hitinfo;
        if (Physics.Raycast(selfTransform.position,targeTransform.position-selfTransform.position,out hitinfo))
        {
            if (hitinfo.transform != targeTransform)
            {
                return AllState.Success;
            }
        }

        return AllState.Failure;
    }

    public  string GetNodeInfoAsString()
    {
        string info = $"Title: {titlename}\nWidth: {width}\nHeight: {height}\nPositionX: {positionx}\nPositionY: {positiony}\n" +
                      $"NodeType: {nodetype}\nIsExcuted:{IsExcuted}\n";
        return info;
    }
}
