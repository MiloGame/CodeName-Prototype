using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchieveCarryBoxNum : AchieveRuleBase,AchieveVisitorInterface
{
    public AchieveCarryBoxNum()
    {
        
    }

    public AchieveCarryBoxNum(int targetval,int id)
    {
        FinishRuleID = id;
        TargetVal = targetval;
    }
    public bool Accept(AchieveFunInterface achieveFun)
    {
        return achieveFun.IsSatisify(this);
    }
}
