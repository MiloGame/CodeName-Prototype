using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class Jump : FSMBaseState
{

    public Jump(FSMData fsmData, NewAnimatorPlayer napPlayer, string clipname) : base(fsmData, napPlayer, clipname)
    {
        if (DataSaveManager<FSMClipParam>.Instance.LoadData("JsonData/AniFSM", "Jump.json",
                ref FsmClipParam) == false)
        {
            FsmClipParam = new FSMClipParam(0, 1, Animator.StringToHash(clipname),
                0.05f, false);
            DataSaveManager<FSMClipParam>.Instance.SaveData("JsonData/AniFSM", "Jump.json",
                ref FsmClipParam);
        }
    }


    public override void RunUpdate()
    {
        FsmData.NaPlayer.animatorRef.SetFloat(FsmData.m_JumpCurSpeed, FsmData.PlayerModel.verticlespeed);

        if (FsmData.PlayerModel.OnGround && Mathf.Abs(FsmData.PlayerModel.movespeed) < 0.7f)
        {
            nextState = FsmData.CreateState(FSMData.ClipType.Land, "Land");
            return;
        }
        if (FsmData.PlayerModel.OnGround && Mathf.Abs(FsmData.PlayerModel.movespeed) > 0.7f)
        {
            nextState = FsmData.CreateState(FSMData.ClipType.MovingLand, "MovingLand");
            return;
        }

        nextState = null;
    }

    public override void Enter()
    {
        FsmData.PlayerModel.EnableJump = true;
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
