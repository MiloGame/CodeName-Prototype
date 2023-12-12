using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class NewState : FSMBaseState
{

    public NewState(FSMData fsmData, NewAnimatorPlayer napPlayer, string clipname) : base(fsmData, napPlayer, clipname)
    {
        if (DataSaveManager<FSMClipParam>.Instance.LoadData("JsonData/AniFSM", "NewState.json",
                ref FsmClipParam) == false)
        {
            FsmClipParam = new FSMClipParam(1, 1, Animator.StringToHash(clipname),
                0f, true);
            DataSaveManager<FSMClipParam>.Instance.SaveData("JsonData/AniFSM", "NewState.json",
                ref FsmClipParam);
        }
    }


    public override void RunUpdate()
    {
        

        nextState = null;
    }


  
}
