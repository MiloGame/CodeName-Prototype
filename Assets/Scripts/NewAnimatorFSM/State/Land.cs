using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;

using UnityEngine;

public class Land : FSMBaseState
{

    public Land(FSMData fsmData, NewAnimatorPlayer napPlayer, string clipname) : base(fsmData, napPlayer, clipname)
    {
        if (DataSaveManager<FSMClipParam>.Instance.LoadData("JsonData/AniFSM", "Land.json",
                ref FsmClipParam) == false)
        {
            FsmClipParam = new FSMClipParam(0, 1, Animator.StringToHash(clipname),
                0.05f, false);
            DataSaveManager<FSMClipParam>.Instance.SaveData("JsonData/AniFSM", "Land.json",
                ref FsmClipParam);
        }
    }


    public override void RunUpdate()
    {
        if (NapPlayer.IsPlayFinish && Mathf.Abs(FsmData.PlayerModel.movespeed)<0.7f)
        {
            nextState = FsmData.CreateState(FSMData.ClipType.Idle, "Idle");
            return;
        }

        nextState = null;
    }

    public override FSMBaseState Exit()
    {
        FsmData.PlayerModel.EnableJump = false;
        return nextState;
    }


}
