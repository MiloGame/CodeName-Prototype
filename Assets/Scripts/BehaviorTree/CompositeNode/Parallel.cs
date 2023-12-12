

using System;
using System.Collections.Generic;
[Serializable]
public class Parallel : CompositeNode,BehaviorTreeFunInterface
{
    private List<AllState> childrenLeftToExecute = new List<AllState>();
    public void OnStart()
    {
        childrenLeftToExecute.Clear();
        ChildreNodes.ForEach(a =>
        {
            childrenLeftToExecute.Add(AllState.Running);
        });
    }

    public void OnStop()
    {
    }

    public AllState OnUpdate()
    {
        bool stillrunning = false;
        for (int i = 0; i < childrenLeftToExecute.Count; i++)
        {
            if (childrenLeftToExecute[i] == AllState.Running)
            {
                var state = ChildreNodes[i].UpdateState();
                if (state == AllState.Failure)
                {
                    AbortRunningChildren();
                    return AllState.Failure;
                }

                if (state == AllState.Running)
                {
                    stillrunning = true;
                }

                childrenLeftToExecute[i] = state;
            }
        }
        return stillrunning ? AllState.Running : AllState.Success;
    }

    private void AbortRunningChildren()
    {
        //for (int i = 0; i < childrenLeftToExecute.Count; i++)
        //{
        //    if (childrenLeftToExecute[i] == AllState.Running)
        //    {
        //        ChildreNodes[i].Abort();
        //    }
        //}
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
