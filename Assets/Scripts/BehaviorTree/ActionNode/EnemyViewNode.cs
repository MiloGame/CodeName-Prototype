using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class EnemyViewNode : ActionNode,BehaviorTreeFunInterface
{

     Collider[] targetInRadius;
     Transform tmpplayer;
     float distanceTarget;
     public void OnStart()
     {

         TreeBlackBoard.PlayerTransform = null;

    }

    public void OnStop()
    {
        
    }
    bool FindPlayer()
    {
        DebugDrawView();
        targetInRadius = Physics.OverlapSphere(TreeBlackBoard.SelfTransform.position, TreeBlackBoard.viewRadius, (1 << 8));
        if (targetInRadius.Length != 0)
        {
            tmpplayer = targetInRadius[0].transform;
            Vector3 targetDir = (tmpplayer.position - TreeBlackBoard.SelfTransform.position).normalized;
            if (Vector3.Angle(targetDir, TreeBlackBoard.SelfTransform.forward) < TreeBlackBoard.radiusangle / 2) // angle在两个向量同向的时候返回0~180的值
            {
                distanceTarget = Vector3.Distance(TreeBlackBoard.SelfTransform.position, tmpplayer.position);
                RaycastHit hitinfo;
                if (!Physics.Raycast(TreeBlackBoard.SelfTransform.position, targetDir, out hitinfo, distanceTarget, ~(1 << 8 | 1 << 10)))
                {
                    TreeBlackBoard.PlayerTransform = tmpplayer;
                    Debug.DrawLine(TreeBlackBoard.SelfTransform.position, TreeBlackBoard.PlayerTransform.position);
                    var reletiveDir = TreeBlackBoard.PlayerTransform.position - TreeBlackBoard.SelfTransform.position;
                    var rotationTarget = Quaternion.LookRotation(reletiveDir, Vector3.up);
                    TreeBlackBoard.SelfTransform.rotation = Quaternion.Slerp(TreeBlackBoard.SelfTransform.rotation, rotationTarget, Time.deltaTime * TreeBlackBoard.RotationSpeed);
                    TreeBlackBoard.LoseSightstartCount = false;
                    return true;
                }
            }
            else
            {
                return false;
            }

        }
        return false;
    }
    Vector3 EdiCalRadius(float angles)
    {
        angles += TreeBlackBoard.SelfTransform.rotation.eulerAngles.y;

        return new Vector3(Mathf.Sin(angles * Mathf.Deg2Rad), 0, Mathf.Cos(angles * Mathf.Deg2Rad));
    }
    void DebugDrawView()
    {
        Debug.DrawLine(TreeBlackBoard.SelfTransform.position, TreeBlackBoard.SelfTransform.position+TreeBlackBoard.viewRadius*EdiCalRadius(TreeBlackBoard.radiusangle/2));
        Debug.DrawLine(TreeBlackBoard.SelfTransform.position, TreeBlackBoard.SelfTransform.position + TreeBlackBoard.viewRadius * EdiCalRadius(-TreeBlackBoard.radiusangle/2));
    }
    public AllState OnUpdate()
    {
        TreeBlackBoard.EnemySeePlayerNow = FindPlayer();
        return TreeBlackBoard.EnemySeePlayerNow ? AllState.Success: AllState.Failure;
    }

    public string GetNodeInfoAsString()
    {
        string info = $"Title: {titlename}\nWidth: {width}\nHeight: {height}\nPositionX: {positionx}\nPositionY: {positiony}\n" +
                      $"NodeType: {nodetype}\nIsExcuted:{IsExcuted}\n";
        return info;
    }
}
