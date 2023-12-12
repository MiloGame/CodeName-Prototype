using System;
using UnityEditor.AI;
using UnityEngine;
using UnityEngine.AI;
[Serializable]
public class GotoPosistionNode : ActionNode,BehaviorTreeFunInterface
{
    //private float miniMoveTheshold;
    public void OnStart()
    {

    }

    public void OnStop()
    {
    }

    public AllState OnUpdate()
    {
        float distance = Vector3.Distance(TreeBlackBoard.NextPatrolPoint, TreeBlackBoard.aiAgent.transform.position);
        //Debug.Log("diatance"+distance+"stop dis"+ TreeBlackBoard.aiAgent.stoppingDistance);
        if (distance > TreeBlackBoard.aiAgent.stoppingDistance)
        {


            var reletiveDir = TreeBlackBoard.NextPatrolPoint -TreeBlackBoard.SelfTransform.position;
            var rotationTarget = Quaternion.LookRotation(reletiveDir, Vector3.up);
            TreeBlackBoard.SelfTransform.rotation = Quaternion.Slerp(TreeBlackBoard.SelfTransform.rotation , rotationTarget, Time.deltaTime * TreeBlackBoard.RotationSpeed);
            if (Quaternion.Angle(rotationTarget,TreeBlackBoard.SelfTransform.rotation) <1f)
            {
                TreeBlackBoard.aiAgent.SetDestination(TreeBlackBoard.NextPatrolPoint);
                TreeBlackBoard.aiAgent.isStopped = false;


            }
            else
            {
                return AllState.Running;
            }
            
            //TreeBlackBoard.AiInfo.SelfTransform.LookAt(targetPosition);
            

            return AllState.Failure;
        }
        else
        {
            TreeBlackBoard.aiAgent.isStopped = true;
            //TreeBlackBoard.AiInfo.SelfTransform.LookAt(TreeBlackBoard.PlayerTransform.position);
            return AllState.Success;
        }
    }

    public  string GetNodeInfoAsString()
    {
        string info = $"Title: {titlename}\nWidth: {width}\nHeight: {height}\nPositionX: {positionx}\nPositionY: {positiony}\n" +
                      $"NodeType: {nodetype}\nIsExcuted:{IsExcuted}\n";
        return info;
    }
}
