using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class Idle_to_aim_crouch_RT : FSMBaseState
{

    public Idle_to_aim_crouch_RT(FSMData fsmData, NewAnimatorPlayer napPlayer, string clipname) : base(fsmData, napPlayer, clipname)
    {
        if (DataSaveManager<FSMClipParam>.Instance.LoadData("JsonData/AniFSM", "Idle_to_aim_crouch_RT.json",
                ref FsmClipParam) == false)
        {
            FsmClipParam = new FSMClipParam(0, 1, Animator.StringToHash(clipname),
                0f, true);
            DataSaveManager<FSMClipParam>.Instance.SaveData("JsonData/AniFSM", "Idle_to_aim_crouch_RT.json",
                ref FsmClipParam);
        }
    }


    public override void RunUpdate()
    {
        if (this.NapPlayer.IsPlayFinish || FsmData.PlayerModel.CrouchIsMove)
        {
            nextState = FsmData.CreateState(FSMData.ClipType.CrouchLocomotiom, "CrouchLocomotiom");
            return;
        }

        nextState = null;
    }



}
