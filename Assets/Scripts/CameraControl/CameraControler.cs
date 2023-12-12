using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CameraModel))]
public class CameraControler : MonoBehaviour
{
    public UnityEvent OnFreeLookCam;

    public UnityEvent OnAimDownSight;
    public UnityEvent OnOverShouder;
    public CameraModel CameraModel;

    public PrefabManger PbManger;
    public Vector3 cameraVelocity;
    //public void MouseInvisible()
    //{
    //    //mouse invisible
    //    Cursor.visible = false;
    //    //mouse locked
    //    Cursor.lockState = CursorLockMode.Locked;
    //}

    public void ChangeView()
    {
        //if (CameraModel.EnableFreeLook && CameraModel.CurrMode!=CameraModel.mode.Freelook)
        //{
        //    CameraModel.EnableSkillLook = false;
        //    //PbManger.MainCamera.transform.position = Vector3.SmoothDamp(PbManger.MainCamera.transform.position, 
        //    //    (CameraModel.LookAtPos + CameraModel.orbit), ref cameraVelocity, 1f);
        //    //FreeLookCam(1);
        //    //if (PbManger.MainCamera.transform.position == CameraModel.LookAtPos + CameraModel.orbit)
        //    //{
        //    //    CameraModel.CurrMode = CameraModel.mode.Freelook;
                
        //    //    return true;
        //    //}
        //    //else
        //    //{
        //    //    return false;
        //    //}
        //}

        //if (CameraModel.EnableSkillLook && CameraModel.CurrMode != CameraModel.mode.Skill)
        //{
        //    CameraModel.CurrMode = CameraModel.mode.Skill;
        //    CameraModel.EnableFreeLook = false;
        //}
        if (CameraModel.EnableSkillLook & CameraModel.EnableFreeLook)
        {
            if (CameraModel.EnableFreeLook && CameraModel.CurrMode!= CameraModel.mode.Freelook)
            {
                CameraModel.EnableSkillLook = false;
                CameraModel.CurrMode = CameraModel.mode.Freelook;

            }
            else if (CameraModel.EnableSkillLook && CameraModel.CurrMode != CameraModel.mode.Skill )
            {
                CameraModel.EnableFreeLook = false;
                CameraModel.CurrMode = CameraModel.mode.Skill;
            }
        }

    }
    public void FreshUpdate()
    {
        //MouseInvisible();
        ChangeView();
        switch (CameraModel.CurrMode)
        {
            case CameraModel.mode.Freelook:
                FreeLookCam(0.1f);
                break;
            case CameraModel.mode.Skill:
                SkillCam();
                break;


        }
        //if (ChangeView())
        //{
        //    if (CameraModel.EnableFreeLook)
        //    {
        //        FreeLookCam(0.1f);
        //        //transform.LookAt(PbManger.LookAtPosTransform.position);
        //    }

        //    if (CameraModel.EnableSkillLook)
        //    {
        //        SkillCam();
        //    }
        //}

        
    }

    private void SkillCam()
    {
        CameraModel.SkillLookAtPos =
            PbManger.PlayerTransform.position + PbManger.PlayerTransform.up.normalized * 1.2f;


        CameraModel.Skillorbit = PbManger.PlayerTransform.forward * CameraModel.Skillradius;
        CameraModel.Skillorbit =
            Quaternion.Euler(-CameraModel.SkillangleY, CameraModel.SkillangleX, 0) * CameraModel.Skillorbit;


        PbManger.MainCamera.transform.position = Vector3.SmoothDamp(PbManger.MainCamera.transform.position,
            (CameraModel.SkillLookAtPos + CameraModel.Skillorbit), ref cameraVelocity, 1f);


        PbManger.MainCamera.transform.LookAt(CameraModel.SkillLookAtPos);
    }

    private void FreeLookCam(float smoothtime)
    {
        OnFreeLookCam.Invoke();


        if (CameraModel.Ishitcollider)
        {
            //  radius -= 0.2f;
            CameraModel.radius = CameraModel.camhitColliderpos * 0.85f;
            CameraModel.orbit = Vector3.forward * CameraModel.radius;
            CameraModel.orbit = Quaternion.Euler(-CameraModel.angleY, CameraModel.angleX, 0) * CameraModel.orbit;
            //      cam.transform.position = LookAtObj.transform.position + orbit;
            CameraModel.LookPosorbit = Vector3.forward * CameraModel.LookAtPosRadius;
            CameraModel.LookPosorbit = Quaternion.Euler(0, PbManger.MainCamera.transform.eulerAngles.x, 0) *
                                       CameraModel.LookPosorbit;
        }
        else
        {
            CameraModel.orbit = Vector3.forward * CameraModel.radius;
            CameraModel.orbit = Quaternion.Euler(-CameraModel.angleY, CameraModel.angleX, 0) * CameraModel.orbit;
            // cam.transform.position = LookAtObj.transform.position + orbit;
            //PbManger.MainCamera.transform.position = Vector3.Lerp(PbManger.MainCamera.transform.position, CameraModel.LookAtPos + CameraModel.orbit, Time.deltaTime * 7f);
            CameraModel.LookPosorbit = Vector3.forward * CameraModel.LookAtPosRadius;
            CameraModel.LookPosorbit = Quaternion.Euler(0, PbManger.MainCamera.transform.eulerAngles.x, 0) *
                                       CameraModel.LookPosorbit;
        }
        //Debug.Log("maimcamera is same as prefab ?" + PbManger.MainCamera is Camera);

        PbManger.MainCamera.transform.position = Vector3.SmoothDamp(PbManger.MainCamera.transform.position,
            (CameraModel.LookAtPos + CameraModel.orbit), ref cameraVelocity, smoothtime);


        var rotrotation = PbManger.MainCamera.transform.rotation;
        rotrotation.x = PbManger.TpsmoonTransform.rotation.x;
        rotrotation.z = PbManger.TpsmoonTransform.rotation.z;
        PbManger.TpsmoonTransform.rotation = rotrotation;

        PbManger.MainCamera.transform.LookAt(PbManger.LookAtPosTransform.position);
    }
}
