
using System;
using UnityEngine;
using UnityEngine.AI;

[Serializable]
public class DoPatrolNode : ActionNode,BehaviorTreeFunInterface
{
    private int patrolindex=0;
    private NavMeshHit navMeshinfo;
    private bool isChange = false;
    public void OnStart()
    {
    }

    public void OnStop()
    {

    }

    public AllState OnUpdate()
    {
        if (!isChange)
        {
            isChange = true;
            for (int i=0;i< TreeBlackBoard.BTManager.ParaslList.Count;i++)
            {
                var oripos = TreeBlackBoard.BTManager.ParaslList[i];

                if (NavMesh.SamplePosition(oripos, out navMeshinfo, 0.5f, NavMesh.AllAreas))
                {
                    TreeBlackBoard.BTManager.ParaslList[i] = navMeshinfo.position;
                }
            }

            TreeBlackBoard.NextPatrolPoint = TreeBlackBoard.BTManager.ParaslList[0];
        }
        
        

        return AllState.Success;
    }

    public  string GetNodeInfoAsString()
    {
        string info = $"Title: {titlename}\nWidth: {width}\nHeight: {height}\nPositionX: {positionx}\nPositionY: {positiony}\n" +
                      $"NodeType: {nodetype}\nIsExcuted:{IsExcuted}\n";
        return info;
    }
}
