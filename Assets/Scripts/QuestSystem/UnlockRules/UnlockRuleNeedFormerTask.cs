using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockRuleNeedFormerTask : UnlockRuleBase, UnLockVisitorInterface
{
    public UnlockRuleNeedFormerTask()
    {
        
    }

    public UnlockRuleNeedFormerTask(int formerid,int id)
    {
        UnlockRuleID = id;
        FormalTaskID = formerid;
    }
    public bool Accept(UnLockFunInterface unLockFun)
    {
        return unLockFun.CanUnlock(this);
    }
}
