using System;
using UnityEngine;
using UnityEngine.AI;
[Serializable]
public class SetCoverPositionNode :ActionNode,BehaviorTreeFunInterface
{
    public void OnStart()
    {
    }

    public void OnStop()
    {
    }

    public AllState OnUpdate()
    {
        //if (TreeBlackBoard.AiInfo.bestCoverPos==null)
        //{
        //    return AllState.Failure;
        //}
        //else
        //{
        //    //TreeBlackBoard.movePosition = TreeBlackBoard.AiInfo.bestCoverPos;
        //    return AllState.Success;
        //}
        return AllState.Failure;
    }

    public  string GetNodeInfoAsString()
    {
        string info = $"Title: {titlename}\nWidth: {width}\nHeight: {height}\nPositionX: {positionx}\nPositionY: {positiony}\n" +
                      $"NodeType: {nodetype}\nIsExcuted:{IsExcuted}\n";
        //info += GetBlackBoardData();
        return info;
    }
}
