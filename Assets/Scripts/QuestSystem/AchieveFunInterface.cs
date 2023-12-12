using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface AchieveFunInterface
{
    public bool IsSatisify(AchieveDefateBoss achieveDefateBoss);
    public bool IsSatisify(AchieveDefeatNormalEnemyNum achieveNum);
    public bool IsSatisify(AchieveDiscoverAreaNum achieveNum);
    public bool IsSatisify(AchieveCarryBoxNum achieveNum);

}
