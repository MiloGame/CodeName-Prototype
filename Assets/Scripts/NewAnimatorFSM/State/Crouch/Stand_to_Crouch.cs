using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class Stand_to_Crouch : FSMBaseState
{
    public Stand_to_Crouch(FSMData fsmData, NewAnimatorPlayer napPlayer, string clipname) : base(fsmData, napPlayer, clipname)
    {
        if (DataSaveManager<FSMClipParam>.Instance.LoadData("JsonData/AniFSM", "Stand_to_Crouch.json",
                ref FsmClipParam) == false)
        {
            FsmClipParam = new FSMClipParam(0, 1, Animator.StringToHash(clipname),
                0.4f, false);
            DataSaveManager<FSMClipParam>.Instance.SaveData("JsonData/AniFSM", "Stand_to_Crouch.json",
                ref FsmClipParam);
        }
    }

    public override void RunUpdate()
    {
        if (NapPlayer.IsPlayFinish)
        {
            nextState = FsmData.CreateState(FSMData.ClipType.CrouchIdle, "CrouchIdle");
            return;
        }
        nextState = null;
    }
    public override void Enter()
    {
        FsmData.PlayerModel.EnableCrouch = true;
        FsmData.PlayerModel.EnableJump = false;
        FsmData.PlayerModel.EnableMove = false;
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

}
