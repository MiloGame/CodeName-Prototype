
using System;

[Serializable]
public class Failure : DecoratorNode,BehaviorTreeFunInterface
{
    public void OnStart()
    {
    }

    public void OnStop()
    {
    }

    public AllState OnUpdate()
    {
        var state = ChildNode.State;
        if (state == AllState.Success)
        {
            return AllState.Failure;
        }

        return state;
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
