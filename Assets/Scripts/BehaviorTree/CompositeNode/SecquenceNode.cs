
using System;


[Serializable]
public class SecquenceNode : CompositeNode,BehaviorTreeFunInterface
{
    private int childindex;
    public void OnStart()
    {
        childindex = 0;
    }

    public void OnStop()
    {
    }

    public AllState OnUpdate()
    {
        var childnode = ChildreNodes[childindex];
        switch (childnode.UpdateState())
        {
            case AllState.Running:
                return AllState.Running;
            case AllState.Failure:
                return AllState.Failure;
            case AllState.Success:
                childindex ++;
                break;
        }
        
        return childindex == ChildreNodes.Count ? AllState.Success : AllState.Running;
    }
    public  string GetNodeInfoAsString()
    {
        string total = "";
        foreach (var childreNode in ChildreNodes)
        {
            BehaviorTreeFunInterface chiInterface = childreNode as BehaviorTreeFunInterface;
            total += chiInterface.GetNodeInfoAsString();
        }

        total += "\n";
        string info = $"Title: {titlename}\nWidth: {width}\nHeight: {height}\nPositionX: {positionx}\nPositionY: {positiony}\n" +
                      $"NodeType: {nodetype} \nIsExcuted:{IsExcuted}\nChildreNodes info below:\n" + total;
        return info;
    }
}
