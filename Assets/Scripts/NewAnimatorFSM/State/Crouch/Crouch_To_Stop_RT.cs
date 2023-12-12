using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class Crouch_To_Stop_RT : FSMBaseState
{

    public Crouch_To_Stop_RT(FSMData fsmData, NewAnimatorPlayer napPlayer, string clipname) : base(fsmData, napPlayer, clipname)
    {
        if (DataSaveManager<FSMClipParam>.Instance.LoadData("JsonData/AniFSM", "Crouch_To_Stop_RT.json",
                ref FsmClipParam) == false)
        {
            FsmClipParam = new FSMClipParam(0, 1.5f, Animator.StringToHash(clipname),
                0f, true);
            DataSaveManager<FSMClipParam>.Instance.SaveData("JsonData/AniFSM", "Crouch_To_Stop_RT.json",
                ref FsmClipParam);
        }
    }
    
    //public override FSMBaseState Exit()
    //{
    //    if (!FsmData.PlayerModel.CrouchIsMove && FsmData.PlayerModel.CrouchshouldRootMotionrotate)
    //    {
    //        FsmData.CrouchMoeTurnAngle = FsmData.PlayerModel.CrouchRotatedeltaangle;
    //    }
    //    return nextState;
    //}

    public override void RunUpdate()
    {
        if (NapPlayer.IsPlayFinish )
        {
            nextState = FsmData.CreateState(FSMData.ClipType.CrouchIdle, "CrouchIdle");
            return;
        }

        //if (!FsmData.PlayerModel.CrouchIsMove && FsmData.PlayerModel.CrouchshouldRootMotionrotate)
        //{
            
        //    nextState = FsmData.CreateState(FSMData.ClipType.walk_Turn_RT, "walk_Turn_RT");
        //    return;
        //}
        nextState = null;
    }
    public override void Enter()
    {
        if (FsmData.CameraModel.EnableFreeLook)
        {
            NapPlayer.animatorRef.SetFloat(FsmData.m_CrouchLocomotionStopHor, 0);
            NapPlayer.animatorRef.SetFloat(FsmData.m_CrouchLocomotionStopVer, 1);
        }
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
