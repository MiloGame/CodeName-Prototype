

using System;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.AI;
[Serializable]
public class IsCoverAvaliableNode : ActionNode,BehaviorTreeFunInterface
{
    private List<Collider> avaliableCovers;
    [NonSerialized]private Transform targetTransform;
    private EnemyInfo aiInfo;
    [NonSerialized]private Transform selfTransform;
    private Vector3 coverpoint;
    private float castRadius;
    private float offset;
    private float defaultdepth;
    private float defaultheight;
    public void OnStart()
    {
        //avaliableCovers = TreeBlackBoard.avaliableCovers;
        avaliableCovers.Clear();
        //targetTransform = TreeBlackBoard.PlayerTransform;
        //aiInfo = TreeBlackBoard.AiInfo;
        //selfTransform = TreeBlackBoard.AiInfo.SelfTransform;
        //castRadius = TreeBlackBoard.FindCoverRadius;
        //offset = TreeBlackBoard.FindCoverOffset;
        //defaultheight = TreeBlackBoard.FindObjDefaultHeight;
        //defaultdepth= TreeBlackBoard.FindObjDefaultDepth;
    }

    public void OnStop()
    {
    }

    public AllState OnUpdate()
    {
        var colliders = Physics.OverlapSphere(selfTransform.position, 10, LayerMask.GetMask("CoverLayer"));

        foreach (var collider in colliders)
        {
            if (avaliableCovers.Contains(collider)!=true)
            {
                avaliableCovers.Add(collider);
            }
            
        }
        Collider bestCover = FindBestCover();
        if (bestCover==null)
        {
            return AllState.Failure;
        }
        else
        {
            Vector3 raydirection = bestCover.transform.position - targetTransform.position;
            raydirection.y = 0;
            raydirection.y -= defaultdepth;
            Ray castRay = new Ray(bestCover.transform.position+defaultheight*Vector3.up, raydirection);
            Debug.DrawRay(bestCover.transform.position+defaultheight * Vector3.up, raydirection,Color.blue,15f);
            RaycastHit hitinfoHit;
            if (Physics.Raycast(castRay, out hitinfoHit, 100, LayerMask.NameToLayer("NavMeshLayer")))
            {
                NavMeshHit navMeshHit;
                if (NavMesh.SamplePosition(hitinfoHit.point, out navMeshHit, 10, NavMesh.AllAreas))
                {
                    coverpoint = navMeshHit.position;
                }
            }
            aiInfo.SetHidePosistion(coverpoint);
            return AllState.Success;
        }
    }

    private Collider FindBestCover()
    {
        float minDistance = 100000;
        Collider res=null;
        if (avaliableCovers.Count==0)
        {
            return res;
        }
        foreach (var avaliableCover in avaliableCovers)
        {
            float distance = Vector3.Distance(selfTransform.position, avaliableCover.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                res = avaliableCover;
            }
        }

        return res;
    }

    public  string GetNodeInfoAsString()
    {
        string info = $"Title: {titlename}\nWidth: {width}\nHeight: {height}\nPositionX: {positionx}\nPositionY: {positiony}\n" +
                      $"NodeType: {nodetype}\nIsExcuted:{IsExcuted}\n";
        return info;
    }
}
