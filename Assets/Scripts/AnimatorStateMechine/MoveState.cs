using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class MoveState : AnimationState
{
    private float value=0f;

    public override AnimationState HandleInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            return AniData.CreateState(AnimationData.AnimationType.StartJump, Ap, PcControler,CamerControler);
        }
        if (!PcControler.ismove && CamerControler.GetCameraMode == 1)
        {
            value = 0f;
            return AniData.CreateState(AnimationData.AnimationType.IdleAnimateState, Ap, PcControler,CamerControler);

        }
        return null;
    }

    public override void Enter()
    {
       // Debug.Log("Move"+"value"+value);
        if (!Input.GetKey(KeyCode.LeftShift))
        {
            value = Mathf.MoveTowards(value, 5, Time.deltaTime*7f);
        }
        else
        {
            value = Mathf.MoveTowards(value, 10, Time.deltaTime*7f);
        }

        Ap.GetAnimator.SetFloat("MoveSpeed", value);
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
                "TPSMove.json",
                ref StateParams) == false)
        {
            StateParams = new AniParams();
            StateParams.FrameName = "TPSMove";
            StateParams.rate = 0.5f;
            StateParams.layer = 1;
            StateParams.TransitionSpeed = 0.4f;
            StateParams.needrootmotion = false;
            DataSaveManager<AniParams>.Instance.SaveData("JsonData/animation",
                "TPSMove.json",
                ref StateParams);
        }
     //   Ard.SetfileName(StateParams.FrameName);
        StateParams.rate = 1.4f;
        StateParams.TransitionSpeed = 0.4f;
    }
}
