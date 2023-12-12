using System;

[Serializable]
public class UnderAttackNode : ActionNode,BehaviorTreeFunInterface
{
    public EnemyInfo AiInfo;
    public void OnStart()
    {
        //AiInfo = TreeBlackBoard.AiInfo;
    }

    public void OnStop()
    {
        //TreeBlackBoard.AiInfo.preHealth = TreeBlackBoard.AiInfo.currentHealth;

    }

    public AllState OnUpdate()
    {
        //if (AiInfo.currentHealth < AiInfo.preHealth)
        //{

        //    return AllState.Success;
        //}

        return AllState.Failure;
    }

    public  string GetNodeInfoAsString()
    {
        string info = $"Title: {titlename}\nWidth: {width}\nHeight: {height}\nPositionX: {positionx}\nPositionY: {positiony}\n" +
                      $"NodeType: {nodetype}\nIsExcuted:{IsExcuted}\n";
        return info;
    }
}
