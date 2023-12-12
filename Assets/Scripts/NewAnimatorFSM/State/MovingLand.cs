using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class MovingLand : FSMBaseState
{
    private float curspeed;

    public MovingLand(FSMData fsmData, NewAnimatorPlayer napPlayer, string clipname) : base(fsmData, napPlayer, clipname)
    {
        if (DataSaveManager<FSMClipParam>.Instance.LoadData("JsonData/AniFSM", "MovingLand.json",
                ref FsmClipParam) == false)
        {
            FsmClipParam = new FSMClipParam(0, 1, Animator.StringToHash(clipname),
                0.05f, false);
            DataSaveManager<FSMClipParam>.Instance.SaveData("JsonData/AniFSM", "MovingLand.json",
                ref FsmClipParam);
        }
    }


    public override void RunUpdate()
    {
        FsmData.NaPlayer.animatorRef.SetFloat(FsmData.m_MovingLandCurSpeed,curspeed );

        if (NapPlayer.IsPlayFinish )
        {
            nextState = FsmData.CreateState(FSMData.ClipType.Locomotiom, "Locomotiom");
            return;
        }
        nextState = null;
    }

    public override FSMBaseState Exit()
    {
        FsmData.PlayerModel.EnableJump = false;
        return nextState;
    }
    public override void Enter()
    {
        curspeed = FsmData.PlayerModel.movespeed;
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
