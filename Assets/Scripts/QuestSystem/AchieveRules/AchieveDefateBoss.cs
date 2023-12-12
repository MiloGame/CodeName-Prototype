using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchieveDefateBoss : AchieveRuleBase,AchieveVisitorInterface
{
    public AchieveDefateBoss()
    {
        
    }

    public AchieveDefateBoss(int bossnum,int id)
    {
        FinishRuleID = id;
        if (bossnum==0)
        {
            IsBossDefated = false;
        }
        else
        {
            IsBossDefated = true;
        }
    }
    public bool Accept(AchieveFunInterface achieveFun)
    {
        return achieveFun.IsSatisify(this);
    }
}
