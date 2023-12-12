

using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

[Serializable]
public class GetPatrolNode: ActionNode,BehaviorTreeFunInterface
{
    //private float patrolredius;
    private int patrolnums=1;
    private int totalpoints;
    public void OnStart()
    {
        totalpoints = TreeBlackBoard.BTManager.ParaslList.Count;
        //TreeBlackBoard.patrolPos = null;
        //patrolredius = TreeBlackBoard.patrolRadius;
        //patrolnums = TreeBlackBoard.patrolNums;
        //TreeBlackBoard.patrolPos = new Vector3[patrolnums];
    }


    public void OnStop()
    {
    }

    public AllState OnUpdate()
    {
        GetPartrolPos();
        patrolnums++;
        return AllState.Success;
    }

    void GetPartrolPos()
    {
        if (totalpoints!=0)
        {
            patrolnums %= totalpoints;
            TreeBlackBoard.NextPatrolPoint = TreeBlackBoard.BTManager.ParaslList[patrolnums];
        }
        //for (int i = 0; i < patrolnums; i++)
        //{
        //    Vector3 offsetVector3 = Random.insideUnitSphere * patrolredius;
        //    Vector3 randomPos = TreeBlackBoard.BornPosition + offsetVector3;
        //    NavMeshHit navMeshinfo;
        //    if (NavMesh.SamplePosition(randomPos,out navMeshinfo,patrolredius,NavMesh.AllAreas))
        //    {
        //        //TreeBlackBoard.patrolPos[i] = navMeshinfo.position;
        //        Debug.Log("exist TreeBlackBoard.patrolPos"+ navMeshinfo.position);
        //    }
        //    else
        //    {
        //        Debug.Log("exist TreeBlackBoard.patrolPos not find!!!!");
        //    }

        //}
    }
    public  string GetNodeInfoAsString()
    {
        string info = $"Title: {titlename}\nWidth: {width}\nHeight: {height}\nPositionX: {positionx}\nPositionY: {positiony}\n" +
                      $"NodeType: {nodetype}\nIsExcuted:{IsExcuted}\n";
        return info;
    }
}
