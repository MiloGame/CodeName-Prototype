using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchieveDiscoverAreaNum : AchieveRuleBase,AchieveVisitorInterface
{
    public AchieveDiscoverAreaNum()
    {
        
    }

    public AchieveDiscoverAreaNum(int targetval,int id)
    {
        FinishRuleID = id;
        TargetVal = targetval;
    }
    public bool Accept(AchieveFunInterface achieveFun)
    {
        return achieveFun.IsSatisify(this);
    }
}
