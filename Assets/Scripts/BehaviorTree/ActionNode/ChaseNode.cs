
using System;
using UnityEditor.AI;
using UnityEngine;
using UnityEngine.AI;
[Serializable]
public class ChaseNode : ActionNode,BehaviorTreeFunInterface
{

    public float miniChasingTheshold;
    public float maxChasingTheshold;
    public void OnStart()
    {

    }

    public void OnStop()
    {
    }

    public AllState OnUpdate()
    {
        float distance = Vector3.Distance(TreeBlackBoard.PlayerTransform.position, TreeBlackBoard.aiAgent.transform.position);
        if (distance > miniChasingTheshold && distance < maxChasingTheshold)
        {
            TreeBlackBoard.aiAgent.isStopped = false;
            var reletiveDir = TreeBlackBoard.PlayerTransform.position - TreeBlackBoard.SelfTransform.position;
            var rotationTarget = Quaternion.LookRotation(reletiveDir, Vector3.up);
            TreeBlackBoard.SelfTransform.rotation = Quaternion.Slerp(TreeBlackBoard.SelfTransform.rotation, rotationTarget, Time.deltaTime * TreeBlackBoard.RotationSpeed);

            TreeBlackBoard.aiAgent.SetDestination(TreeBlackBoard.PlayerTransform.position);
            return AllState.Running;
        }
        else 
        {
            TreeBlackBoard.aiAgent.isStopped = true;

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
