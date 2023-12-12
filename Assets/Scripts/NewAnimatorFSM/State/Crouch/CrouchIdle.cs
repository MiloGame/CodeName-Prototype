using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class CrouchIdle : FSMBaseState
{

    public CrouchIdle(FSMData fsmData, NewAnimatorPlayer napPlayer, string clipname) : base(fsmData, napPlayer, clipname)
    {
        if (DataSaveManager<FSMClipParam>.Instance.LoadData("JsonData/AniFSM", "CrouchIdle.json",
                ref FsmClipParam) == false)
        {
            FsmClipParam = new FSMClipParam(0, 1, Animator.StringToHash(clipname),
                1f, false);
            DataSaveManager<FSMClipParam>.Instance.SaveData("JsonData/AniFSM", "CrouchIdle.json",
                ref FsmClipParam);
        }
    }


    public override void RunUpdate()
    {
        FsmData.PlayerModel.crouchspeed = 0f;
        if (FsmData.PlayerModel.CrouchshouldRootMotionrotate)
        {
            if (!FsmData.PlayerModel.CrouchIsMove && FsmData.PlayerModel.CrouchIsLocomotionInput)
            {
                nextState = FsmData.CreateState(FSMData.ClipType.Idle_Crouch_Turn_RT, "Idle_Crouch_Turn_RT");
                return;
            }



        }
        else
        {

            if (FsmData.PlayerModel.CrouchIsLocomotionInput)
            {
                if (FsmData.CameraModel.EnableFreeLook)
                {
                    NapPlayer.animatorRef.SetFloat(FsmData.m_CrouchIdletoaimMoveHorizontal, 0);
                    NapPlayer.animatorRef.SetFloat(FsmData.m_CrouchIdletoaimMoveVerticle, 1);
                }
                nextState = FsmData.CreateState(FSMData.ClipType.Idle_to_aim_crouch_RT, "Idle_to_aim_crouch_RT");
                return;
            }
        }

        if (FsmData.PlayerModel.ShortPressLeftCtrl)
        {
            nextState = FsmData.CreateState(FSMData.ClipType.Crouch_to_Stand, "Crouch_to_Stand");
            return;
        }


        nextState = null;
    }
    public override FSMBaseState Exit()
    {
        if (FsmData.PlayerModel.CrouchshouldRootMotionrotate && !FsmData.PlayerModel.CrouchIsMove && FsmData.PlayerModel.CrouchIsLocomotionInput)
            FsmData.CrouchIdleTurnAngle = FsmData.PlayerModel.CrouchRotatedeltaangle;

        return nextState;
    }


}
