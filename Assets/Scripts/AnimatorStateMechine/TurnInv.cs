using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class TurnInv : AnimationState
{

    public override AnimationState HandleInput()
    {
        //if (PcControler.ismove && !PcControler.shouldrotate)
        //{

        //    return AniData.CreateState(AnimationData.AnimationType.MoveState, Ap, PcControler);
        //}
        if (Ap.IsPlayFinish)
        {
            return AniData.CreateState(AnimationData.AnimationType.IdleAnimateState, Ap, PcControler,CamerControler);
        }

        return null;
    }

    public override void Enter()
    {
        Debug.Log("TurnInv");
        //if (CamerControler.GetCameraMode==2)
        //{
        //    StateParams.TransitionSpeed = 0f;
        //    StateParams.rate = 600f;
        //}
        //else
        //{
        //    StateParams.TransitionSpeed = 0.4f;
        //    StateParams.rate = 1.45f;
        //}
        if (Ap.GetcurrentAnimationName != StateParams.FrameName)
        {
            // Debug.Log("IdleAnimateState Enter Handler calling change frame");
            Ap.Changeframe(StateParams.FrameName, StateParams.TransitionSpeed);
        }
        else
        {
            //  Debug.Log("IdleAnimateState Enter Handler calling play");
            Ap.Play(StateParams.FrameName, StateParams.rate, StateParams.layer,true);
            //Ap.GetAnimator.Play(StateParams.FrameName, StateParams.layer);
        }
    }

    public override void Init()
    {
        if (DataSaveManager<AniParams>.Instance.LoadData("JsonData/animation",
                "RotateInv.json",
                ref StateParams) == false)
        {
            StateParams = new AniParams();
            StateParams.FrameName = "RotateInv";
            StateParams.rate = 0.5f;
            StateParams.layer = 1;
            StateParams.TransitionSpeed = 0.4f;
            StateParams.needrootmotion = true;
            DataSaveManager<AniParams>.Instance.SaveData("JsonData/animation",
                "RotateInv.json",
                ref StateParams);
        }
      //  Ard.SetfileName(StateParams.FrameName);
        StateParams.rate = 1f;
        StateParams.TransitionSpeed = 0.5f;
    }
}
