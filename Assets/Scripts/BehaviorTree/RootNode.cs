
using System;

[Serializable]
public class RootNode : Node,BehaviorTreeFunInterface
{
    public Node ChildNode;
    public void OnStart()
    {
       
    }

    public void OnStop()
    {
        
    }

    public AllState OnUpdate()
    {
        return ChildNode.UpdateState();
    }

    public string GetNodeInfoAsString()
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
