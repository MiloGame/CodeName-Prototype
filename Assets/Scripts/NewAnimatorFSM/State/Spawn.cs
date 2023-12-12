using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class Spawn : FSMBaseState
{

    public Spawn(FSMData fsmData, NewAnimatorPlayer napPlayer, string clipname) : base(fsmData, napPlayer, clipname)
    {
        if (DataSaveManager<FSMClipParam>.Instance.LoadData("JsonData/AniFSM", "Spawn.json",
                ref FsmClipParam) == false)
        {
            FsmClipParam = new FSMClipParam(0, 1, Animator.StringToHash(clipname),
                0.4f, false);
            DataSaveManager<FSMClipParam>.Instance.SaveData("JsonData/AniFSM", "Spawn.json",
                ref FsmClipParam);
        }
    }


    public override void RunUpdate()
    {
        //if (Input.GetKeyDown(KeyCode.J))
        //{
        //    nextState = this.FsmData.CreateState(FSMData.ClipType.Default, "Default");
        //    return;
        //}
        if (this.NapPlayer.IsPlayFinish)
        {
            nextState = FsmData.CreateState(FSMData.ClipType.Idle, "Idle");
            return;
        }
        nextState = null;
    }


    public override FSMBaseState Exit()
    {
        FsmData.PlayerModel.IsPlayerAlive = true;
        return nextState;
    }


}
