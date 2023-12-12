using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class CrouchLocomotiom : FSMBaseState
{

    public CrouchLocomotiom(FSMData fsmData, NewAnimatorPlayer napPlayer, string clipname) : base(fsmData, napPlayer, clipname)
    {
        if (DataSaveManager<FSMClipParam>.Instance.LoadData("JsonData/AniFSM", "CrouchLocomotiom.json",
                ref FsmClipParam) == false)
        {
            FsmClipParam = new FSMClipParam(0, 1f, Animator.StringToHash(clipname),
                0.4f, false);
            DataSaveManager<FSMClipParam>.Instance.SaveData("JsonData/AniFSM", "CrouchLocomotiom.json",
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
        if (!FsmData.PlayerModel.CrouchIsMove && !FsmData.PlayerModel.CrouchshouldRootMotionrotate && !FsmData.PlayerModel.CrouchIsLocomotionInput)
        {

            nextState = FsmData.CreateState(FSMData.ClipType.Crouch_To_Stop_RT, "Crouch_To_Stop_RT");
            return;
            
        }
        //if (!FsmData.PlayerModel.IsMove && FsmData.PlayerModel.shouldRootMotionrotate && FsmData.PlayerModel.IsLocomotionInput)
        //{

        //    nextState = FsmData.CreateState(FSMData.ClipType.walk_Turn_RT, "walk_Turn_RT");
        //    return;
        //}

        nextState = null;
    }
    public override void Enter()
    {
        FsmData.PlayerModel.crouchspeed = 1f;
        if (FsmData.CameraModel.EnableFreeLook)
        {
            NapPlayer.animatorRef.SetFloat(FsmData.m_CrouchLocomotionHoritional, 0);
            NapPlayer.animatorRef.SetFloat(FsmData.m_CrouchLocomotionVerticle, FsmData.PlayerModel.crouchspeed);
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
