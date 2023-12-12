using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class LoseViewNode : ActionNode, BehaviorTreeFunInterface
{
    private float starttime;
    public float durationTime;

    public void OnStart()
    {

    }

    public void OnStop()
    {

    }

    public AllState OnUpdate()
    {

        if (!TreeBlackBoard.EnemySeePlayerPre)
        {
            return AllState.Failure;
        }
        else if(TreeBlackBoard.EnemySeePlayerPre)
        {
            TreeBlackBoard.aiAgent.isStopped = true;

            if (!TreeBlackBoard.LoseSightstartCount)
            {
                starttime = Time.time;
                TreeBlackBoard.LoseSightstartCount = true;
            }

            if (Time.time - starttime > durationTime)
            {
                TreeBlackBoard.LoseSightstartCount = false;
                TreeBlackBoard.EnemySeePlayerPre = false;
                return AllState.Failure;
            }
        }

        return AllState.Running;
    }

    public string GetNodeInfoAsString()
    {
        string info = $"Title: {titlename}\nWidth: {width}\nHeight: {height}\nPositionX: {positionx}\nPositionY: {positiony}\n" +
                      $"NodeType: {nodetype}\nIsExcuted:{IsExcuted}\n";
        return info;
    }
}
