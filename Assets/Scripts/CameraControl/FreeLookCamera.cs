using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CameraModel))]
public class FreeLookCamera : MonoBehaviour
{
    public CameraModel CameraModel;
    public PrefabManger PbManger;
    public void RotateFreeLookCameraInput()
    {
        CameraModel.angleX += Input.GetAxis("Mouse X");
        CameraModel.angleY = Mathf.Clamp(CameraModel.angleY -= Input.GetAxis("Mouse Y"), -60, 60);
        CameraModel.radius = Mathf.Clamp(CameraModel.radius -= Input.mouseScrollDelta.y, 0.5f, 1.5f);
        CameraModel.LookAtPos = PbManger.LookAtPosTransform.position;

        CameraModel.LookAtPosRadius = Mathf.Abs(CameraModel.TpsoffsetVector3.x);
        //if (CameraModel.angleX > 360)
        //{
        //    CameraModel.angleX -= 360;
        //}
        //else if (CameraModel.angleX < 0)
        //{
        //    CameraModel.angleX += 360;
        //}

        CameraModel.angleX %= 360;



    }


}
