

using System;
using UnityEngine;
[Serializable]
public class WaitNode : ActionNode,BehaviorTreeFunInterface
{
    public float Waittime = 1f;
    private float _starttime;

    public void OnStart()
    {
        _starttime = Time.time;
    }

    public void OnStop()
    {
    }

    public AllState OnUpdate()
    {
        if (Time.time - _starttime > Waittime)
        {
            return AllState.Success;
        }


        return AllState.Running;
    }
    public  string GetNodeInfoAsString()
    {
        string info = $"Title: {titlename}\nWidth: {width}\nHeight: {height}\nPositionX: {positionx}\nPositionY: {positiony}\n" +
                      $"NodeType: {nodetype}\nIsExcuted:{IsExcuted}\nWaittime:{Waittime}\n";
        return info;
    }

}
