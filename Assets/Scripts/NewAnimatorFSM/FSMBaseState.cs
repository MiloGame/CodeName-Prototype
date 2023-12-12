using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public abstract class FSMBaseState 
{
    public FSMBaseState nextState=null;
    public NewAnimatorPlayer NapPlayer;
    public FSMData FsmData;
    public FSMClipParam FsmClipParam;
    public string clipname;
    public FSMBaseState(FSMData fsmData,NewAnimatorPlayer napPlayer,string clipname)
    {
        FsmData = fsmData;
        NapPlayer = napPlayer;
        this.clipname = clipname;

    }

    public abstract void RunUpdate();
    public virtual void Enter()
    {
   
        FsmClipParam.clipid = Animator.StringToHash(clipname);
        if (NapPlayer.GetCilpId != FsmClipParam.clipid)
        {
            // Debug.Log("IdleAnimateState Enter Handler calling change frame");
            NapPlayer.Changeframe(FsmClipParam.clipid, FsmClipParam.TransitionTime, FsmClipParam.needRootmotion,
                FsmClipParam.layer);
        }
        else
        {
            //  Debug.Log("IdleAnimateState Enter Handler calling play");
            NapPlayer.Play(FsmClipParam.clipid, FsmClipParam.rate, FsmClipParam.layer, FsmClipParam.needRootmotion);
        }
    }


    public virtual FSMBaseState Exit()
    {
        return nextState;
    }

    
}

