

using System;
using UnityEngine;
using UnityEngine.AI;
[Serializable]
public class ShootingNode : ActionNode,BehaviorTreeFunInterface
{
    [NonSerialized]private NavMeshAgent aiAgent;
    private EnemyInfo aiInfo;

    public void OnStart()
    {
        //aiAgent = TreeBlackBoard.AiInfo.aiAgent;
        //aiInfo = TreeBlackBoard.AiInfo;
    }

    public void OnStop()
    {
    }

    public AllState OnUpdate()
    {
        aiAgent.isStopped = true;
        aiInfo.SetFire();
        return AllState.Running;
    }

    public string GetNodeInfoAsString()
    {
        string info = $"Title: {titlename}\nWidth: {width}\nHeight: {height}\nPositionX: {positionx}\nPositionY: {positiony}\n" +
                      $"NodeType: {nodetype}\nIsExcuted:{IsExcuted}\n";
        return info;
    }
}
