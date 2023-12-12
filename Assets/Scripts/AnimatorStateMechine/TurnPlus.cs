using Assets.Scripts;
using UnityEngine;

public class TurnPlus : AnimationState
{

    public override AnimationState HandleInput()
    {

        //  Debug.Log("root transform"+Ap.GetAnimator.rootRotation.eulerAngles.y);
        if (Ap.IsPlayFinish)
        {
            return AniData.CreateState(AnimationData.AnimationType.IdleAnimateState, Ap, PcControler,CamerControler);
        }
        return null;
    }

    public override void Enter()
    {
        Debug.Log("TurnPlus");
        //if (CamerControler.GetCameraMode == 2)
        //{
        //    StateParams.rate = 600f;
        //    StateParams.TransitionSpeed = 0f;
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
                "RotatePlus.json",
                ref StateParams) == false)
        {
            StateParams = new AniParams();
            StateParams.FrameName = "RotatePlus";
            StateParams.rate = 0.5f;
            StateParams.layer = 1;
            StateParams.TransitionSpeed = 0.5f;
            StateParams.needrootmotion = true;
            DataSaveManager<AniParams>.Instance.SaveData("JsonData/animation",
                "RotatePlus.json",
                ref StateParams);
        }
//        Ard.SetfileName(StateParams.FrameName);
        StateParams.rate = 1f;
        StateParams.TransitionSpeed = 0.4f;
    }
}