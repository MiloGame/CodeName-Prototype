

using System;
[Serializable]
public class IfNode : CompositeNode, BehaviorTreeFunInterface
{
    public void OnStart()
    {
        
    }

    public void OnStop()
    {
        
    }

    public AllState OnUpdate()
    {
        var JudgeNode = ChildreNodes[0];
    
        switch (JudgeNode.UpdateState())
        {
            case AllState.Running:
                //ChildreNodes[2].UpdateState();
                return AllState.Running;
                break;
            case AllState.Failure:
                ChildreNodes[2].UpdateState();
                return AllState.Failure;
                break;
            case AllState.Success:
                ChildreNodes[1].UpdateState();
                return AllState.Success;
                break;
            default:
                throw new ArgumentOutOfRangeException("exceed limit");
        }

    }

    public string GetNodeInfoAsString()
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
