using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class Idle_Crouch_Turn_RT : FSMBaseState
{

    public Idle_Crouch_Turn_RT(FSMData fsmData, NewAnimatorPlayer napPlayer, string clipname) : base(fsmData, napPlayer, clipname)
    {
        if (DataSaveManager<FSMClipParam>.Instance.LoadData("JsonData/AniFSM", "Idle_Crouch_Turn_RT.json",
                ref FsmClipParam) == false)
        {
            FsmClipParam = new FSMClipParam(0, 1.5f, Animator.StringToHash(clipname),
                0f, true);
            DataSaveManager<FSMClipParam>.Instance.SaveData("JsonData/AniFSM", "Idle_Crouch_Turn_RT.json",
                ref FsmClipParam);
        }
    }
    public override void Enter()
    {
        FsmData.CrouchIsInplaceturning = true;
        NapPlayer.animatorRef.SetFloat(FsmData.m_CrouchLocomotionRotationAngle, FsmData.CrouchIdleTurnAngle);

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
    public override FSMBaseState Exit()
    {
        if (nextState != null)
        {
            FsmData.CrouchIsInplaceturning = false;
            FsmData.PlayerModel.CrouchshouldRootMotionrotate = false;
        }

        return nextState;
    }

    public override void RunUpdate()
    {
        Debug.Log("enter Crouchidleturn");
        if (this.NapPlayer.IsPlayFinish  )
        {
            nextState = FsmData.CreateState(FSMData.ClipType.CrouchIdle, "CrouchIdle");
            

            return;
        }


        nextState = null;
    }



}
