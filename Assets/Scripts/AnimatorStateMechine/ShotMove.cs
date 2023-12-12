using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class ShotMove : AnimationState
{
    private float Xvalue = 0f;
    private float Zvalue = 0f;
    public override AnimationState HandleInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            return AniData.CreateState(AnimationData.AnimationType.StartJump, Ap, PcControler, CamerControler);
        }
        if (!PcControler.ismove && CamerControler.GetCameraMode !=1)
        {
            Xvalue = 0f;
            Zvalue = 0f;
            return AniData.CreateState(AnimationData.AnimationType.IdleAnimateState, Ap, PcControler, CamerControler);

        }
        return null;
    }

    public override void Enter()
    {

        if (!Input.GetKey(KeyCode.LeftShift))
        {
            Xvalue = Mathf.MoveTowards(Xvalue, 5*PcControler.RotateDirection.x, Time.deltaTime * 25f);
            Zvalue = Mathf.MoveTowards(Zvalue, 5 * PcControler.RotateDirection.z, Time.deltaTime * 25f);
        }
        else
        {
            Xvalue = Mathf.MoveTowards(Xvalue, 10 * PcControler.RotateDirection.x, Time.deltaTime * 25f);
            Zvalue = Mathf.MoveTowards(Zvalue, 10 * PcControler.RotateDirection.z, Time.deltaTime * 25f);
        }

        Ap.GetAnimator.SetFloat("TPSZ", Zvalue);
        Ap.GetAnimator.SetFloat("TPSX", Xvalue);
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
                "ShotMove.json",
                ref StateParams) == false)
        {
            StateParams = new AniParams();
            StateParams.FrameName = "ShotMove";
            StateParams.rate = 1;
            StateParams.layer = 1;
            StateParams.TransitionSpeed = 0.4f;
            StateParams.needrootmotion = false;
            DataSaveManager<AniParams>.Instance.SaveData("JsonData/animation",
                "ShotMove.json",
                ref StateParams);
        }
    //    Ard.SetfileName(StateParams.FrameName);
        StateParams.rate = 2f;
        StateParams.TransitionSpeed = 0.4f;
    }
}
