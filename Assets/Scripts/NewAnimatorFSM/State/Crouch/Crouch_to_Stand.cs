using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class Crouch_to_Stand : FSMBaseState
{


    public Crouch_to_Stand(FSMData fsmData, NewAnimatorPlayer napPlayer, string clipname) : base(fsmData, napPlayer, clipname)
    {
        if (DataSaveManager<FSMClipParam>.Instance.LoadData("JsonData/AniFSM", "Crouch_to_Stand.json",
                ref FsmClipParam) == false)
        {
            FsmClipParam = new FSMClipParam(0, 1, Animator.StringToHash(clipname),
                1f, false);
            DataSaveManager<FSMClipParam>.Instance.SaveData("JsonData/AniFSM", "Crouch_to_Stand.json",
                ref FsmClipParam);
        }
    }

    public override FSMBaseState Exit()
    {
        FsmData.PlayerModel.EnableCrouch = false;
        FsmData.PlayerModel.verticlespeed = 0;
        FsmData.PlayerModel.EnableJump = true;
        FsmData.PlayerModel.EnableMove = true;
        return nextState;
    }

    public override void RunUpdate()
    {
        if (NapPlayer.IsPlayFinish)
        {
            nextState = FsmData.CreateState(FSMData.ClipType.Idle, "Idle");
            return;
        }

        nextState = null;
    }
}
