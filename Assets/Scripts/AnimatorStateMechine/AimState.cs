using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class AimState : AnimationState
{
    private float upangle;
    private float lrangle;
    private float rotateangle;
    private bool isaim = false;
    private float weight=0;

    public override AnimationState HandleInput()
    {
        return null;
    }

    public override void Enter()
    {

        if (Input.GetKeyDown(KeyCode.P)) isaim = !isaim;
        //if (CamerControler.GetCameraMode==2)
        //{
        //    isaim = true;
        //}
        if (isaim && !PcControler.tpsmouseshouldrotate && !PcControler.ismove && PcControler.isGrounded)
        {
            weight = Mathf.MoveTowards(weight, 1, Time.deltaTime *3f);
            Ap.GetAnimator.SetLayerWeight(StateParams.layer,weight);
        }
        else
        {
            weight = Mathf.MoveTowards(weight, 0, Time.deltaTime *3f);
            Ap.GetAnimator.SetLayerWeight(StateParams.layer, weight);
        }
        upangle = -CamGameObject.transform.eulerAngles.x;
        //lrangle = CamGameObject.transform.eulerAngles.y;
        if (CamerControler.GetCameraMode==2)
        {
            
            lrangle = CamerControler.tpsrotateangle;
        }else if (CamerControler.GetCameraMode==1)
        {
            lrangle = CamGameObject.transform.eulerAngles.y;
        }
        if (upangle < -170)
        {
            upangle += 360f;
        }

        if (lrangle > 0 && lrangle < 180)
        {
            rotateangle = lrangle;
        }
        else if (lrangle > 180)
        {
            rotateangle = lrangle - 360f;
        }

        if (lrangle < 0 && lrangle > -180)
        {
            rotateangle = lrangle;
        }
        else if (lrangle < -180)
        {
            rotateangle = lrangle + 360f;
        }
     //   Debug.Log("updown angle"+upangle+"leftright angle"+lrangle+"rotateangle"+rotateangle);
        Ap.GetAnimator.SetFloat("LeftRightVal", rotateangle);
        Ap.GetAnimator.SetFloat("UpDownVal", upangle);
        if (Ap.GetcurrentAnimationName != StateParams.FrameName)
        {
            // Debug.Log("IdleAnimateState Enter Handler calling change frame");
            Ap.Changeframe(StateParams.FrameName, StateParams.TransitionSpeed);
        }
        else
        {
            //  Debug.Log("IdleAnimateState Enter Handler calling play");
            Ap.Play(StateParams.FrameName, StateParams.rate, StateParams.layer, true);
        }
    }

    public override void Init()
    {
        if (DataSaveManager<AniParams>.Instance.LoadData("JsonData/animation",
                "AimState.json",
                ref StateParams) == false)
        {
            StateParams = new AniParams();
            StateParams.FrameName = "AimOffsetVer";
            StateParams.rate = 1;
            StateParams.layer = 3;
            StateParams.TransitionSpeed = 0.4f;
            DataSaveManager<AniParams>.Instance.SaveData("JsonData/animation",
                "AimState.json",
                ref StateParams);
        }
        StateParams.FrameName = "AimOffsetHor";
        StateParams.rate = 1;
        StateParams.TransitionSpeed = 0f;
    }
}
