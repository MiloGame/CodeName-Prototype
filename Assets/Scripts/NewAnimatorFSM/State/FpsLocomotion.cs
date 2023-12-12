using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class FpsLocomotion : FSMBaseState
{

    public FpsLocomotion(FSMData fsmData, NewAnimatorPlayer napPlayer, string clipname) : base(fsmData, napPlayer, clipname)
    {
        if (DataSaveManager<FSMClipParam>.Instance.LoadData("JsonData/AniFSM", "FpsLocomotion.json",
                ref FsmClipParam) == false)
        {
            FsmClipParam = new FSMClipParam(0, 1, Animator.StringToHash(clipname),
                0.4f, false);
            DataSaveManager<FSMClipParam>.Instance.SaveData("JsonData/AniFSM", "FpsLocomotion.json",
                ref FsmClipParam);
        }
    }


    public override void RunUpdate()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            nextState = FsmData.CreateState(FSMData.ClipType.FpsLocomotion, "FpsLocomotion");
            return;
        }

        nextState = null;
    }


  
}
