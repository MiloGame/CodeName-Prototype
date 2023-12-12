using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class JumpInAir : AnimationState
{

    public override AnimationState HandleInput()
    {
        Debug.Log("JumpInAir" + "value"+ Ap.IsPlayFinish);
        if (PcControler.isGrounded)
        {
            return AniData.CreateState(AnimationData.AnimationType.Landing, Ap, PcControler,CamerControler);
        }

        return null;
    }

    public override void Enter()
    {
       

        if (Ap.GetcurrentAnimationName != StateParams.FrameName)
        {
            // Debug.Log("IdleAnimateState Enter Handler calling change frame");
            Ap.Changeframe(StateParams.FrameName, StateParams.TransitionSpeed);
        }
        else
        {
            //  Debug.Log("IdleAnimateState Enter Handler calling play");
            Ap.Play(StateParams.FrameName, StateParams.rate, StateParams.layer, false);
        }
    }

    public override void Init()
    {
        if (DataSaveManager<AniParams>.Instance.LoadData("JsonData/animation",
                "Jmp_Base_A_Keep.json",
                ref StateParams) == false)
        {
            StateParams = new AniParams();
            StateParams.FrameName = "Jmp_Base_A_Keep";
            StateParams.rate = 0.5f;
            StateParams.layer = 1;
            StateParams.TransitionSpeed = 0.4f;
            StateParams.needrootmotion = false;
            DataSaveManager<AniParams>.Instance.SaveData("JsonData/animation",
                "Jmp_Base_A_Keep.json",
                ref StateParams);
        }
     //   Ard.SetfileName(StateParams.FrameName);
        StateParams.rate = 1f;
        StateParams.TransitionSpeed = 0f;
    }
}
