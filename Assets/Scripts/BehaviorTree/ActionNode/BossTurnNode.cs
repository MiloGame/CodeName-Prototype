using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[Serializable]
public class BossTurnNode : ActionNode, BehaviorTreeFunInterface
{
    public float TurretRotateSpeed;
    public float RotationSpeed;

    public void OnStart()
    {


    }

    public void OnStop()
    {

    }

    public AllState OnUpdate()
    {

        //if (TreeBlackBoard.BTManager.BooFootManager.distance  < 0.005f)
        //{
        //    TreeBlackBoard.BTManager.BooFootManager.EnableMove = false;
        //    TreeBlackBoard.BTManager.BooFootManager.EnableJump = false;
        //    TreeBlackBoard.BTManager.BooFootManager.EnableRotate = true;
        //}
        //else
        //{
        //    TreeBlackBoard.BTManager.BooFootManager.EnableMove = true;
        //    TreeBlackBoard.BTManager.BooFootManager.EnableJump = false;
        //    TreeBlackBoard.BTManager.BooFootManager.EnableRotate = false;

        //}
        TreeBlackBoard.BTManager.BooFootManager.EnableMove = false;
        TreeBlackBoard.BTManager.BooFootManager.EnableJump = false;
        TreeBlackBoard.BTManager.BooFootManager.EnableRotate = true;
        //if (Vector3.Distance(TreeBlackBoard.BTManager.BooFootManager.AimPos.position, TreeBlackBoard.BTManager.BooFootManager.AimInitPos) > 0.05f)
        //{
        //    TreeBlackBoard.BTManager.BooFootManager.AimPos.position = Vector3.Lerp(
        //        TreeBlackBoard.BTManager.BooFootManager.AimPos.position,
        //        TreeBlackBoard.BTManager.BooFootManager.AimInitPos , TurretRotateSpeed * Time.deltaTime);

        //}

        //var reletiveDir = (TreeBlackBoard.PlayerTransform.position- TreeBlackBoard.SelfTransform.position).normalized;
        Quaternion rotationTarget = Quaternion.LookRotation(TreeBlackBoard.RotatereletiveDir, Vector3.up);

        //if (Vector3.Angle(TreeBlackBoard.BTManager.BossRoot.forward.normalized,reletiveDir) < 0.05f)//wrong
        //{
        //    Debug.Log("turn not need");
        //    return AllState.Success;
        //}
        //else
        //{

        //    TreeBlackBoard.FinishRotate = true;



        TreeBlackBoard.SelfTransform.rotation = Quaternion.Slerp(TreeBlackBoard.SelfTransform.rotation, rotationTarget,RotationSpeed);
                //TreeBlackBoard.SelfTransform.rotation = rotationTarget;



        return AllState.Running;
            //    return AllState.Running;
            //}
    }

    public string GetNodeInfoAsString()
    {
        string info = $"Title: {titlename}\nWidth: {width}\nHeight: {height}\nPositionX: {positionx}\nPositionY: {positiony}\n" +
                      $"NodeType: {nodetype}\nIsExcuted:{IsExcuted}\n";
        return info;
    }
}
