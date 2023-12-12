using System;
[Serializable]
public class Selector : CompositeNode,BehaviorTreeFunInterface
{
    protected int current;

    public void OnStart()
    {
        current = 0;
    }

    public void OnStop()
    {
    }

    public AllState OnUpdate()
    {
        var currnetkid = ChildreNodes[current];
        switch (currnetkid.UpdateState())
        {
            case AllState.Running:
                return AllState.Running;
            case AllState.Failure:
                current++;
                break;
            case AllState.Success:
                return AllState.Success;
                
        }


        return current == ChildreNodes.Count ? AllState.Failure : AllState.Running;
    }

    public  string GetNodeInfoAsString()
    {
        string total = "";
        foreach (var childreNode in ChildreNodes)
        {
            BehaviorTreeFunInterface chInterface = childreNode as BehaviorTreeFunInterface;
            total += chInterface?.GetNodeInfoAsString();
        }

        total += "\n";
        string info = $"Title: {titlename}\nWidth: {width}\nHeight: {height}\nPositionX: {positionx}\nPositionY: {positiony}\n" +
                      $"NodeType: {nodetype} \nIsExcuted:{IsExcuted}\nChildreNodes info below:\n" + total;
        return info;
    }
}