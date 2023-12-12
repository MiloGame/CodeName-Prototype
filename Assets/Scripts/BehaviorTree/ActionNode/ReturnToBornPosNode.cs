

using System;
using UnityEngine;
[Serializable]
public class ReturnToBornPosNode   : ActionNode,BehaviorTreeFunInterface
{
    public float stoppingDistance;

    public void OnStart()
    {
        //rangeTheshiold = TreeBlackBoard.rangeTheshold;
        //targeTransform = TreeBlackBoard.PlayerTransform;
        //selfTransform = TreeBlackBoard.AiInfo.SelfTransform;
    }

    public void OnStop()
    {

    }

    public AllState OnUpdate()
    {
        float distance = Vector3.Distance(TreeBlackBoard.SelfTransform.position, TreeBlackBoard.BornPosition);
        if (distance >stoppingDistance)
        {
            var reletiveDir = TreeBlackBoard.BornPosition - TreeBlackBoard.SelfTransform.position;
            var rotationTarget = Quaternion.LookRotation(reletiveDir, Vector3.up);
            TreeBlackBoard.SelfTransform.rotation = Quaternion.Slerp(TreeBlackBoard.SelfTransform.rotation, rotationTarget, Time.deltaTime * TreeBlackBoard.RotationSpeed);
            if (Quaternion.Angle(rotationTarget, TreeBlackBoard.SelfTransform.rotation) < 1f)
            {
                TreeBlackBoard.aiAgent.SetDestination(TreeBlackBoard.BornPosition);
                TreeBlackBoard.aiAgent.isStopped = false;
                
            }
            else
            {
                return AllState.Running;
            }
            
            return AllState.Failure;
        }
        else
        {
            TreeBlackBoard.aiAgent.isStopped = true;
            return AllState.Success;
        }
    }

    public string GetNodeInfoAsString()
    {
        string info = $"Title: {titlename}\nWidth: {width}\nHeight: {height}\nPositionX: {positionx}\nPositionY: {positiony}\n" +
                      $"NodeType: {nodetype}\nIsExcuted:{IsExcuted}\n";
        return info;
    }
}
