using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class BossShouldJumpNode : ActionNode, BehaviorTreeFunInterface
{
    public float miniDisTHeshold;
    public float miniAngleTHeshold;
    private Vector2 transplane;
    private Vector2 selftrans;

    public void OnStart()
    {
        
    }

    public void OnStop()
    {
    }

    public AllState OnUpdate()
    {
        transplane.x = TreeBlackBoard.SelfTransform.position.x;
        transplane.y = TreeBlackBoard.SelfTransform.position.z;
        selftrans.x = TreeBlackBoard.BTManager.BossLeftFoot.position.x;
        selftrans.y = TreeBlackBoard.BTManager.BossLeftFoot.position.z;
        TreeBlackBoard.DistanceToSupposePos =
            Vector2.Distance(transplane, selftrans);
        TreeBlackBoard.RotatereletiveDir = (TreeBlackBoard.PlayerTransform.position - TreeBlackBoard.BTManager.BooFootManager.Leftfoot.position).normalized;
        ////Debug.Log("boss play dis"+Vector3.Distance(TreeBlackBoard.PlayerTransform.position,TreeBlackBoard.SelfTransform.position)+
        //    "\nboss root to play dis"+Vector3.Distance(TreeBlackBoard.PlayerTransform.position, TreeBlackBoard.BTManager.Root.position)+
        //    "\nboss leftfoot to play dis"+Vector3.Distance(TreeBlackBoard.PlayerTransform.position, TreeBlackBoard.BTManager.BossLeftFoot.position)+
        //    "\nboss leftik to play dis"+Vector3.Distance(TreeBlackBoard.PlayerTransform.position, TreeBlackBoard.BTManager.BooFootManager.Leftfoot.position)+
        //    "\nboss hip to play dis"+Vector3.Distance(TreeBlackBoard.PlayerTransform.position, TreeBlackBoard.BTManager.BooFootManager.hip.position)+
        //    "\nboss leftfoot to boss trans dis"+Vector3.Distance(TreeBlackBoard.SelfTransform.position,TreeBlackBoard.BTManager.BossLeftFoot.position)+
        //    "\nboss ik to self"+Vector3.Distance(TreeBlackBoard.SelfTransform.position,TreeBlackBoard.BTManager.BooFootManager.Leftfoot.position)+
        //    "\nboss hip to self pos"+Vector3.Distance(TreeBlackBoard.SelfTransform.position,TreeBlackBoard.BTManager.BooFootManager.hip.position));
        ////Debug.Log("rotatet angle " + Vector3.Angle(TreeBlackBoard.BTManager.Root.forward.normalized, TreeBlackBoard.RotatereletiveDir));
        if (TreeBlackBoard.DistanceToSupposePos < miniDisTHeshold && Vector3.Angle(TreeBlackBoard.BTManager.Root.forward.normalized, TreeBlackBoard.RotatereletiveDir) > miniAngleTHeshold)
        {
            TreeBlackBoard.ShouldRotate = true;
            //Debug.Log("caleed turn");
            return AllState.Success;
        }
        else
        {
            TreeBlackBoard.ShouldRotate = false;
            //Debug.Log("call move");
            return AllState.Failure;

        }
    }

    public string GetNodeInfoAsString()
    {
        string info = $"Title: {titlename}\nWidth: {width}\nHeight: {height}\nPositionX: {positionx}\nPositionY: {positiony}\n" +
                      $"NodeType: {nodetype}\nIsExcuted:{IsExcuted}\n";
        return info;
    }
}
