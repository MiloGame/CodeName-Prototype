
using System;
using UnityEngine;
[Serializable]
public class TimeOut : DecoratorNode,BehaviorTreeFunInterface
{
    public float duration = 1f;
    private float startTime;
    private bool isexcute = false;
    public void OnStart()
    {
        if (!isexcute)
            startTime = Time.time;
    }

    public void OnStop()
    {
    }

    public AllState OnUpdate()
    {
        if (isexcute)
        {
            return AllState.Failure;
        }
        if (Time.time - startTime > duration)
        {
            isexcute = true;
            return AllState.Failure;
        }
        else 
        {
            ChildNode.UpdateState();
            return AllState.Running;
        }

        
    }

    public  string GetNodeInfoAsString()
    {
        string childinfo = "";
        if (ChildNode != null)
        {
            BehaviorTreeFunInterface chInterface = ChildNode as BehaviorTreeFunInterface;
            childinfo = chInterface?.GetNodeInfoAsString();
        }
        string info = $"Title: {titlename}\nWidth: {width}\nHeight: {height}\nPositionX: {positionx}\nPositionY: {positiony}\n" +
                      $"NodeType: {nodetype}\nIsExcuted:{IsExcuted}\nChildNode info below:\n{childinfo}\n";
        return info;
    }
}
