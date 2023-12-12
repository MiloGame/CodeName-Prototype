using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[Serializable]
public class BossGrenateNavPathNode : ActionNode, BehaviorTreeFunInterface
{
    public void OnStart()
    {
        
    }

    public void OnStop()
    {
    }

    public AllState OnUpdate()
    {
        if (TreeBlackBoard.NeedRegrenate||TreeBlackBoard.BossNavPath==null)
        {
            if (TreeBlackBoard.BossNavPath==null)
            {
                TreeBlackBoard.BossNavPath = new NavMeshPath();
            }
            Debug.Log("recreated path");
            TreeBlackBoard.NeedRegrenate = false;
            if (TreeBlackBoard.aiAgent.CalculatePath( TreeBlackBoard.PlayerTransform.position, TreeBlackBoard.BossNavPath))
            {
                if (TreeBlackBoard.BossNavPath.corners.Length > 0)
                {
                    TreeBlackBoard.aiAgent.SetPath(TreeBlackBoard.BossNavPath);
                    Debug.Log("set path"+TreeBlackBoard.BossNavPath.corners.Length+"agentleng"+TreeBlackBoard.aiAgent.path.corners.Length);
                }
                else
                {
                    Debug.Log("navmesh guide genreate fail");

                    return AllState.Failure;
                }

                return AllState.Success;
            }
            else
            {
                Debug.Log("navmesh guide genreate fail");
                return AllState.Failure;
            }
        }
        else
        {
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
