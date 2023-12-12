using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class Locomotiom : FSMBaseState
{
    private float verspeed = 0;
    private float horspeed = 0;
    public Locomotiom(FSMData fsmData, NewAnimatorPlayer napPlayer, string clipname) : base(fsmData, napPlayer, clipname)
    {
        if (DataSaveManager<FSMClipParam>.Instance.LoadData("JsonData/AniFSM", "Locomotiom.json",
                ref FsmClipParam) == false)
        {
            FsmClipParam = new FSMClipParam(0, 1f, Animator.StringToHash(clipname),
                0.05f, false);
            DataSaveManager<FSMClipParam>.Instance.SaveData("JsonData/AniFSM", "Locomotiom.json",
                ref FsmClipParam);
        }
    }

    public override FSMBaseState Exit()
    {
        if (!FsmData.PlayerModel.IsMove && FsmData.PlayerModel.shouldRootMotionrotate)
        {
            FsmData.MoeTurnAngle = FsmData.PlayerModel.Rotatedeltaangle;
        }

        return nextState;
    }
    public override void RunUpdate()
    {
        if (!FsmData.PlayerModel.IsLocomotionInput)
        {
            nextState = FsmData.CreateState(FSMData.ClipType.Suddenstop, "Suddenstop");
            return;
        }
        //if (!FsmData.PlayerModel.IsMove && !FsmData.PlayerModel.shouldRootMotionrotate && !FsmData.PlayerModel.IsLocomotionInput)
        //{
        //    //if (FsmData.PlayerModel.IsSprint)
        //    //{
        //    //    nextState = FsmData.CreateState(FSMData.ClipType.Sprint_To_Stop_RT, "Sprint_To_Stop_RT");
        //    //    return;
        //    //}
        //    //if (FsmData.PlayerModel.IsWalking)
        //    //{
        //    //    nextState = FsmData.CreateState(FSMData.ClipType.Walk_To_Stop_RT, "Walk_To_Stop_RT");
        //    //    return;
        //    //}

        //    //if (FsmData.PlayerModel.IsRunning)
        //    //{
        //    //    nextState = FsmData.CreateState(FSMData.ClipType.Run_To_Stop_RT, "Run_To_Stop_RT");
        //    //    return;
        //    //}
        //}
        //if (!FsmData.PlayerModel.IsMove && FsmData.PlayerModel.shouldRootMotionrotate && FsmData.PlayerModel.IsLocomotionInput)
        //{
        //    if (FsmData.PlayerModel.IsWalking)
        //    {
        //        nextState = FsmData.CreateState(FSMData.ClipType.walk_Turn_RT, "walk_Turn_RT");
        //    }else if (FsmData.PlayerModel.IsRunning)
        //    {
        //        nextState = FsmData.CreateState(FSMData.ClipType.run_Turn_RT, "run_Turn_RT");

        //    }
        //    else if(FsmData.PlayerModel.IsSprint)
        //    {
        //        nextState = FsmData.CreateState(FSMData.ClipType.sprint_Turn_RT, "sprint_Turn_RT");

        //    }
        //    return;
        //}

        // jump
        if (FsmData.PlayerModel.ShortPressSpace)
        {
            nextState = FsmData.CreateState(FSMData.ClipType.Jump, "Jump");
            return;
        }

        nextState = null;
    }
    public override void Enter()
    {
        //Debug.Log("FsmData.PlayerModel.EnableFreeLook" + FsmData.PlayerModel.EnableFreeLook + "verspeed" + verspeed);

        if (FsmData.PlayerModel.EnableFreeLook)
        {
            horspeed = 0;
            NapPlayer.animatorRef.SetFloat(FsmData.m_LocomotionHoritional, horspeed);

            //NapPlayer.animatorRef.SetFloat(FsmData.m_LocomotionHoritional, 0);
            if (FsmData.PlayerModel.IsRunning)
            {
                verspeed = Mathf.Lerp(verspeed, 3f, Time.deltaTime * 3f);
                NapPlayer.animatorRef.SetFloat(FsmData.m_LocomotionVerticle, verspeed);
            }else if (FsmData.PlayerModel.IsWalking)
            {
                verspeed = Mathf.Lerp(verspeed, 1.5f, Time.deltaTime * 3f);
                NapPlayer.animatorRef.SetFloat(FsmData.m_LocomotionVerticle, verspeed);
            }else if (FsmData.PlayerModel.IsSprint)
            {
                verspeed = Mathf.Lerp(verspeed, 5f, Time.deltaTime * 3f);

                NapPlayer.animatorRef.SetFloat(FsmData.m_LocomotionVerticle, verspeed);
            }

        }
        else
        {
            if (FsmData.PlayerModel.IsRunning)
            {
                verspeed = Mathf.Lerp(verspeed, 3f*FsmData.PlayerModel.AimMoveDirection.z, Time.deltaTime * 3f);
                horspeed = Mathf.Lerp(horspeed, 3f * FsmData.PlayerModel.AimMoveDirection.x, Time.deltaTime * 3f);
                NapPlayer.animatorRef.SetFloat(FsmData.m_LocomotionVerticle, verspeed);
                NapPlayer.animatorRef.SetFloat(FsmData.m_LocomotionHoritional, horspeed);
            }
            else if (FsmData.PlayerModel.IsWalking)
            {
                verspeed = Mathf.Lerp(verspeed, 1.5f * FsmData.PlayerModel.AimMoveDirection.z, Time.deltaTime * 3f);
                horspeed = Mathf.Lerp(horspeed, 1.5f * FsmData.PlayerModel.AimMoveDirection.x, Time.deltaTime * 3f);
                NapPlayer.animatorRef.SetFloat(FsmData.m_LocomotionVerticle, verspeed);
                NapPlayer.animatorRef.SetFloat(FsmData.m_LocomotionHoritional, horspeed);
            }
            else if (FsmData.PlayerModel.IsSprint)
            {
                verspeed = Mathf.Lerp(verspeed, 5f * FsmData.PlayerModel.AimMoveDirection.z, Time.deltaTime * 3f);
                horspeed = Mathf.Lerp(horspeed, 5f * FsmData.PlayerModel.AimMoveDirection.x, Time.deltaTime * 3f);
                NapPlayer.animatorRef.SetFloat(FsmData.m_LocomotionVerticle, verspeed);
                NapPlayer.animatorRef.SetFloat(FsmData.m_LocomotionHoritional, horspeed);
            }

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
