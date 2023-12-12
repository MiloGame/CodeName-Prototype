using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class Suddenstop : FSMBaseState
{
    public Suddenstop(FSMData fsmData, NewAnimatorPlayer napPlayer, string clipname) : base(fsmData, napPlayer, clipname)
    {
        if (DataSaveManager<FSMClipParam>.Instance.LoadData("JsonData/AniFSM", "Suddenstop.json",
                ref FsmClipParam) == false)
        {
            FsmClipParam = new FSMClipParam(0, 0.75f, Animator.StringToHash(clipname),
                0.05f, false);
            DataSaveManager<FSMClipParam>.Instance.SaveData("JsonData/AniFSM", "Suddenstop.json",
                ref FsmClipParam);
        }
    }

    public override void RunUpdate()
    {
        if (this.NapPlayer.IsPlayFinish)
        {
            nextState = FsmData.CreateState(FSMData.ClipType.Idle, "Idle");
            return;
        }

        nextState = null;
    }
}
