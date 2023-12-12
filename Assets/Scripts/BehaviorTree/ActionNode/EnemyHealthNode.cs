using System;

[Serializable]
public class EnemyHealthNode : ActionNode,BehaviorTreeFunInterface
{


    public void OnStart()
    {

    }

    public void OnStop()
    {

    }

    public AllState OnUpdate()
    {
        if (TreeBlackBoard.IsDead)
        {
            return AllState.Success;
        }
        if (TreeBlackBoard.CurrentHealth < TreeBlackBoard.PreHealth)
        {
            TreeBlackBoard.Ishit = true;
            TreeBlackBoard.PreHealth = TreeBlackBoard.CurrentHealth;
            TreeBlackBoard.BTManager.UiManger.OnHealthChange(TreeBlackBoard.CurrentHealth);
            return AllState.Success;

        }
        else
        {
            TreeBlackBoard.Ishit = false;

            return AllState.Failure;
        }
    }

    public  string GetNodeInfoAsString()
    {
        string info = $"Title: {titlename}\nWidth: {width}\nHeight: {height}\nPositionX: {positionx}\nPositionY: {positiony}\n" +
                      $"NodeType: {nodetype}\nIsExcuted:{IsExcuted}\n";
        return info;
    }
}
