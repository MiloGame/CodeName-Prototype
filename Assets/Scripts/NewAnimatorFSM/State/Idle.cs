using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class Idle : FSMBaseState
{

    public Idle(FSMData fsmData, NewAnimatorPlayer napPlayer, string clipname) : base(fsmData, napPlayer, clipname)
    {
        if (DataSaveManager<FSMClipParam>.Instance.LoadData("JsonData/AniFSM", "Idle.json",
                ref FsmClipParam) == false)
        {
            FsmClipParam = new FSMClipParam(0, 1, Animator.StringToHash(clipname),
                0.05f, false);
            DataSaveManager<FSMClipParam>.Instance.SaveData("JsonData/AniFSM", "Idle.json",
                ref FsmClipParam);
        }
    }


    public override void RunUpdate()
    {
        FsmData.PlayerModel.movespeed = 0f;
        //if (FsmData.PlayerModel.shouldRootMotionrotate )
        //{
        //    if (!FsmData.PlayerModel.IsMove && FsmData.PlayerModel.IsLocomotionInput)
        //    {

        //        nextState = FsmData.CreateState(FSMData.ClipType.Idle_Turn_RT, "Idle_Turn_RT");
        //        return;
        //    }
        //}
        //else
        //{
        //if (FsmData.PlayerModel.IsRunning && FsmData.PlayerModel.IsLocomotionInput)
        //{
        //    nextState = FsmData.CreateState(FSMData.ClipType.Idle_to_aim_run_RT, "Idle_to_aim_run_RT");
        //    return;
        //}else if (FsmData.PlayerModel.IsSprint && FsmData.PlayerModel.IsLocomotionInput)
        //{

        //    nextState = FsmData.CreateState(FSMData.ClipType.Idle_to_aim_sprint_RT, "Idle_to_aim_sprint_RT");
        //    return;
        //}else if (FsmData.PlayerModel.IsWalking && FsmData.PlayerModel.IsLocomotionInput)
        //{

        //    nextState = FsmData.CreateState(FSMData.ClipType.Idle_to_aim_walk_RT, "Idle_to_aim_walk_RT");
        //    return;
        //}
        //}
        if (FsmData.PlayerModel.IsLocomotionInput)
        {
            nextState = FsmData.CreateState(FSMData.ClipType.Locomotiom, "Locomotiom");
            return;
        }
        // jump
        if (FsmData.PlayerModel.ShortPressSpace)
        {
            
            nextState = FsmData.CreateState(FSMData.ClipType.Jump, "Jump");
            return;
        }
        //crouch
        if (FsmData.PlayerModel.ShortPressLeftCtrl)
        {
            nextState = FsmData.CreateState(FSMData.ClipType.Stand_to_Crouch, "Stand_to_Crouch");
            return;
        }

        nextState = null;
    }


    public override FSMBaseState Exit()
    {
        if (FsmData.PlayerModel.shouldRootMotionrotate && !FsmData.PlayerModel.IsMove && FsmData.PlayerModel.IsLocomotionInput)
            FsmData.IdleTurnAngle = FsmData.PlayerModel.Rotatedeltaangle;
        if (!FsmData.PlayerModel.ShortPressSpace)
        {
            FsmData.PlayerModel.EnableJump = false;
        }
        return nextState;
    }
    


}
