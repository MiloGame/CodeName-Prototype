

using System;
using UnityEngine;
[Serializable]
public class BreakPoint : ActionNode,BehaviorTreeFunInterface
{
    public  void OnStart()
    {
        Debug.Log("Trigging Breakpoint");
        Debug.Break();
    }

    public  void OnStop()
    {
    }

    public  AllState OnUpdate()
    {
        return AllState.Success;
    }

    public  string GetNodeInfoAsString()
    {
        string info = $"Title: {titlename}\nWidth: {width}\nHeight: {height}\nPositionX: {positionx}\nPositionY: {positiony}\n" +
                      $"NodeType: {nodetype}\nIsExcuted:{IsExcuted}\n";
        return info;
    }
}
