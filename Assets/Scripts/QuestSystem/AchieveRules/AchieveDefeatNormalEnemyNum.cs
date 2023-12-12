using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchieveDefeatNormalEnemyNum : AchieveRuleBase,AchieveVisitorInterface
{
    public AchieveDefeatNormalEnemyNum()
    {
        
    }

    public AchieveDefeatNormalEnemyNum(int targetval,int id)
    {
        FinishRuleID = id;
        TargetVal = targetval;
    }
    public bool Accept(AchieveFunInterface achieveFun)
    {
        return achieveFun.IsSatisify(this);
    }
}
