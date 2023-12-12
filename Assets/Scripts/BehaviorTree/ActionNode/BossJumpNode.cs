using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class BossJumpNode : ActionNode, BehaviorTreeFunInterface
{
    public float JumpHeight;
    public float JumpSpeed;
    private int index;
    private Vector3 EndPoint;
    private Vector3 CentorPoint;
    private float maxdistance;
    public void OnStart()
    {

        maxdistance = Vector3.Distance(TreeBlackBoard.SelfTransform.position, TreeBlackBoard.PlayerTransform.position) -
                      8f;
            TreeBlackBoard.JumpDistance = Mathf.Clamp(TreeBlackBoard.JumpDistance, 0,
            maxdistance);
        
        index = 0;
        EndPoint = TreeBlackBoard.SelfTransform.position +
                   (TreeBlackBoard.PlayerTransform.position - TreeBlackBoard.SelfTransform.position).normalized *
                   TreeBlackBoard.JumpDistance;
        CentorPoint = (TreeBlackBoard.SelfTransform.position + EndPoint) * 0.5f + Vector3.up * JumpHeight;
        TreeBlackBoard.BTManager.BooFootManager.GenreateJumpPath(TreeBlackBoard.SelfTransform.position,CentorPoint,EndPoint);
        Debug.Log("end point" + EndPoint+"jump distance"+ TreeBlackBoard.JumpDistance);
    }

    public void OnStop()
    {

    }

    public AllState OnUpdate()
    {
        TreeBlackBoard.BTManager.BooFootManager.EnableMove = false;
        TreeBlackBoard.BTManager.BooFootManager.EnableRotate = false;
        TreeBlackBoard.BTManager.BooFootManager.EnableJump = true;

        if (TreeBlackBoard.JumpDistance <0.05f  )
        {
            TreeBlackBoard.Jumping = false;

            return AllState.Failure;
        }
        else
        {

        }
        if (index < TreeBlackBoard.BTManager.BooFootManager._bezierPointnums)
        {
            TreeBlackBoard.Jumping = true;

            TreeBlackBoard.SelfTransform.position =Vector3.MoveTowards(TreeBlackBoard.SelfTransform.position,TreeBlackBoard.BTManager.BooFootManager.path[index], JumpSpeed);
            if (Vector3.Distance(TreeBlackBoard.SelfTransform.position, TreeBlackBoard.BTManager.BooFootManager.path[index]) < 0.05f)
            {

                index++;
            }
            //TreeBlackBoard.SelfTransform.position = TreeBlackBoard.BTManager.BooFootManager.path[index++];
            return AllState.Running;
        }
        else
        {
            //if (!TreeBlackBoard.BTManager.BooFootManager.LetRightMove&&!TreeBlackBoard.BTManager.BooFootManager.LetLeftmove )
            //{
                TreeBlackBoard.Jumping = false;
                return AllState.Success;
            //}
            
            //return AllState.Running;

            
        }

       

    }

    public string GetNodeInfoAsString()
    {
        string info = $"Title: {titlename}\nWidth: {width}\nHeight: {height}\nPositionX: {positionx}\nPositionY: {positiony}\n" +
                      $"NodeType: {nodetype}\nIsExcuted:{IsExcuted}\n";
        return info;
    }
}
