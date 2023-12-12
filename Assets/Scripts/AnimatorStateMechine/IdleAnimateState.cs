using System.Security.Cryptography.X509Certificates;
using Assets.Scripts;
using UnityEngine;


public class IdleAnimateState : AnimationState
{
    private float rotateangle;
    private float tpsturnangle;


    public override AnimationState HandleInput()
    {
        // Debug.Log("PcControler.shouldrotate"+ PcControler.shouldrotate+ "PcControler.ismove"+ PcControler.ismove);
        if (PcControler.ismove && !PcControler.shouldrotate && CamerControler.GetCameraMode==1 )
        {

            return AniData.CreateState(AnimationData.AnimationType.MoveState, Ap, PcControler,CamerControler);
        }
        if (PcControler.ismove && !PcControler.shouldrotate && CamerControler.GetCameraMode!=1)
        {

            return AniData.CreateState(AnimationData.AnimationType.ShotMove, Ap, PcControler,CamerControler);
        }
        // Debug.Log("getcameramode"+(PcControler.shouldrotate && (CamerControler.GetCameraMode == 1)));
      //  Debug.Log("PcControler.tpsmouseshouldrotate" + PcControler.tpsmouseshouldrotate);
        if (PcControler.shouldrotate && CamerControler.GetCameraMode == 1)
        {
            if (PcControler.deltaangle > 0 && PcControler.deltaangle<180)
            {
                rotateangle = PcControler.deltaangle;
            }
            else if(PcControler.deltaangle > 180)
            {
                rotateangle = PcControler.deltaangle - 360f;
            }

            if (PcControler.deltaangle < 0 && PcControler.deltaangle > -180)
            {
                rotateangle = PcControler.deltaangle;
            }
            else if (PcControler.deltaangle < -180)
            {
                rotateangle = PcControler.deltaangle + 360f;
            }

            Debug.Log("rotateangle"+rotateangle);
            Ap.GetAnimator.SetFloat("RotateAngle",rotateangle);
            if (rotateangle>0)
            {

                return AniData.CreateState(AnimationData.AnimationType.TurnPlus, Ap,PcControler,CamerControler);
            }else if (rotateangle<0)
            {

                return AniData.CreateState(AnimationData.AnimationType.TurnInv, Ap, PcControler,CamerControler);
            }
        }
        if (PcControler.tpsmouseshouldrotate && CamerControler.GetCameraMode == 2)
        {

            tpsturnangle = PcControler.tpsturnbodyangle;
            Debug.Log("tpsturnangle" + tpsturnangle);
            Ap.GetAnimator.SetFloat("RotateAngle", tpsturnangle);
            if (tpsturnangle > 0)
            {

                // PcControler.RotateTpsCamera();    

                return AniData.CreateState(AnimationData.AnimationType.TurnPlus, Ap, PcControler, CamerControler);
            }
            else if (tpsturnangle < 0)
            {
                //  PcControler.RotateTpsCamera();
                return AniData.CreateState(AnimationData.AnimationType.TurnInv, Ap, PcControler, CamerControler);
            }
        }
        if (Input.GetKey(KeyCode.Space))
        {
            return AniData.CreateState(AnimationData.AnimationType.StartJump, Ap, PcControler,CamerControler);
        }
        
        return null;
    }

    public override void Enter()
    {
      //  Debug.Log("idle");

        if (Ap.GetcurrentAnimationName != StateParams.FrameName)
        {
            // Debug.Log("IdleAnimateState Enter Handler calling change frame");
            Ap.Changeframe(StateParams.FrameName, StateParams.TransitionSpeed);
        }
        else
        {
            //  Debug.Log("IdleAnimateState Enter Handler calling play");
            Ap.Play(StateParams.FrameName, StateParams.rate, StateParams.layer,false);
        }
    }

    public override void Init()
    {
        if (DataSaveManager<AniParams>.Instance.LoadData("JsonData/animation", "Idle-Long.json",
                ref StateParams) == false)
        {
            StateParams = new AniParams();
            StateParams.FrameName = "Idle-Long";
            StateParams.rate = 1;
            StateParams.layer = 1;
            StateParams.TransitionSpeed = 0.4f;
            StateParams.needrootmotion = false;
            DataSaveManager<AniParams>.Instance.SaveData("JsonData/animation", "Idle-Long.json",
                ref StateParams);
        }

        //if (Ard==null)
        //{
        //    Debug.Log("idle ard null");
        //}
        //else
        //{
        //    Ard.SetfileName(StateParams.FrameName);
        //}
        
        StateParams.rate = 1;
        StateParams.TransitionSpeed = 0.8f;
    }
}
