using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class BossAttackNode : ActionNode, BehaviorTreeFunInterface
{
    private float _starttime;
    public float AttackLastTime;
    public void OnStart()
    {
        _starttime = Time.time;
    }

    public void OnStop()
    {
        //TreeBlackBoard.BTManager.BossLaserManager.EnableLazer = false;
        //TreeBlackBoard.BTManager.BossLaserManager.EnableEmit = false;
    }

    public AllState OnUpdate()
    {
        ////TreeBlackBoard.BTManager.BooFootManager.AimPos.position = Vector3.Lerp(
        ////    TreeBlackBoard.BTManager.BooFootManager.AimPos.position,
        ////    TreeBlackBoard.PlayerTransform.position, TurretRotateSpeed * Time.deltaTime);
        //if (Vector3.Distance(TreeBlackBoard.BTManager.BooFootManager.AimPos.position,
        //        TreeBlackBoard.PlayerTransform.position) < 0.05f)
        //    return AllState.Success;
        //else
        //    return AllState.Running;
        if ((Time.time - _starttime) > AttackLastTime)
        {
            TreeBlackBoard.BTManager.BossLaserManager.EnableEmit = false;
            TreeBlackBoard.BTManager.BossLaserManager.EnableLazer = false;

            return AllState.Success;
        }
        else
        {
            TreeBlackBoard.BTManager.BossLaserManager.EnableEmit = true;

            TreeBlackBoard.BTManager.BossLaserManager.EnableLazer = true;
            return AllState.Running;
        }
    }

    public string GetNodeInfoAsString()
    {
        string info = $"Title: {titlename}\nWidth: {width}\nHeight: {height}\nPositionX: {positionx}\nPositionY: {positiony}\n" +
                      $"NodeType: {nodetype}\nIsExcuted:{IsExcuted}\n";
        return info;
    }
}
