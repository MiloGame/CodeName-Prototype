using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface UnLockFunInterface 
{
    public bool CanUnlock(UnlockRuleNeedFormerTask formerTask);
    public bool CanUnlock(UnlockRuleChatWithNpc chatWithNpc);

}
