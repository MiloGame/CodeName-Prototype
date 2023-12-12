using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockRuleChatWithNpc : UnlockRuleBase, UnLockVisitorInterface
{
    public UnlockRuleChatWithNpc()
    {
            
    }

    public UnlockRuleChatWithNpc(int npcid,int id)
    {
        UnlockRuleID = id;
        NPCID = npcid;
    }
    public bool Accept(UnLockFunInterface unLockFun)
    {
        return unLockFun.CanUnlock(this);
    }
}
