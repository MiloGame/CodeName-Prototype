using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
[Serializable]
public class TaskInfo
{
    public int TaskID;
    public string TaskTitle;
    public string TaskDescription;
    public string UnlockRule;
    public string FinishRule;
    [CanBeNull] public string Award;
    public int Status;
    public List<UnlockRuleBase> UnlockRuleList;
    public List<AchieveRuleBase> AchieveRuleList;
}
