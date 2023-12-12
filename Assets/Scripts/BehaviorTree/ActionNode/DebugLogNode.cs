
using System;
using UnityEngine;
[Serializable]
public class DebugLogNode : ActionNode,BehaviorTreeFunInterface
{
    public string message ;
    public void OnStart()
    {
       // Debug.LogFormat("OnStart debugnode {0}",message);
    }

    public void OnStop()
    {
       // Debug.LogFormat("OnStop debugnode {0}", message);
    }

    public AllState OnUpdate()
    {
        Debug.Log(message);
        return AllState.Success;
    }
    public string GetNodeInfoAsString()
    {
        string info = $"Title: {titlename}\nWidth: {width}\nHeight: {height}\nPositionX: {positionx}\nPositionY: {positiony}\n" +
                      $"NodeType: {nodetype}\nIsExcuted:{IsExcuted}\nmessage:{message}\n";
        return info;
    }
}
