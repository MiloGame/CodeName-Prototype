using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class DecideToChaseNode : ActionNode, BehaviorTreeFunInterface
{
    private float starttime;
    private bool startCount;
    public float durationTime;

    public void OnStart()
    {
        startCount = false;

    }

    public void OnStop()
    {
        
    }

    public AllState OnUpdate()
    {
        if ( TreeBlackBoard.EnemySeePlayerPre)
        {
            TreeBlackBoard.BTManager.UiManger.UnSetQuestionBarPos();
            TreeBlackBoard.BTManager.UiManger.SetChaseBarPos();

            return AllState.Success;
        }else if (!TreeBlackBoard.EnemySeePlayerPre)
        {
            TreeBlackBoard.aiAgent.isStopped = true;
            TreeBlackBoard.BTManager.UiManger.SetQuestionBarPos();
            TreeBlackBoard.BTManager.UiManger.UnSetChaseBarPos();

            if (!startCount)
            {
                starttime = Time.time;
                startCount = true;
            }

            if (Time.time - starttime > durationTime)
            {
                startCount = false;
                TreeBlackBoard.EnemySeePlayerPre = true;
                return AllState.Success;
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
