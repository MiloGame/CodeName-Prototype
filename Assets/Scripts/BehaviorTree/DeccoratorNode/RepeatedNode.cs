
using System;

[Serializable]
public class RepeatedNode : DecoratorNode,BehaviorTreeFunInterface
{
    public void OnStart()
    {
    }

    public void OnStop()
    {
    }

    public AllState OnUpdate()
    {
        ChildNode.UpdateState();
        return AllState.Running;
    }
    public string GetNodeInfoAsString()
    {
        string childinfo = "";
        if (ChildNode!=null)
        {
            BehaviorTreeFunInterface chInterface = ChildNode as BehaviorTreeFunInterface;
            childinfo = chInterface?.GetNodeInfoAsString();
        }
        string info = $"Title: {titlename}\nWidth: {width}\nHeight: {height}\nPositionX: {positionx}\nPositionY: {positiony}\n" +
                      $"NodeType: {nodetype}\nIsExcuted:{IsExcuted}\nChildNode info below:\n{childinfo}\n";
        return info;
    }
}
