using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Assets.Scripts;
using JetBrains.Annotations;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UI;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;


public class CamerBehavior 
{
    private int viewswitch=0;
    LoadManager lm;
    GameObject player;
    GameObject cam;
    GameObject LookAtObj;
    private float anglesTherehold = 85;
    private GameObject tpsGameObject;
    private GameObject fpsGameObject;
    private Vector3 oribit;
    private float angleX;
    private float angleY;
    private float radius;
    private Vector3 rotY;
    private Vector3 rotX;
    float localOffsetCollider = 0;
    private Vector3 orbit;
    private float FPSangleX;
    private float FPSangleY;
    private float TPSangleX;
    private float TPSangleY;
    private Vector3 cameraVelocity = Vector3.zero;
    public float tpsrotateangle;
    private float fpsrotateangle;

    public int GetCameraMode
    {
        get
        {
            return viewswitch;
        } 
    }
    public CamerBehavior(LoadManager LM)
    {
        radius = 10f;
        viewswitch = 0;
        lm = LM;
        tpsGameObject = lm.TpsGameObject;
        fpsGameObject = lm.FpsGameObject;
        LookAtObj = lm.LookAtGameObject;
        player = lm.Getplayer;
        cam = lm.Getcam;
        lm.cameraFreshEvent += detectKey;
    }

    void detectKey()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            viewswitch = (viewswitch+1)%3;
            switch (viewswitch)
            {
                case 0:
                    lm.Sm.showMessage("change to FPS view");
                    lm.cameraFreshEvent -= RotateTPSCamera;
                    lm.cameraFreshEvent -= RotateFreeLookCamera;
                    lm.cameraFreshEvent += RotateFPSCamera;
                    
                    break;
                case 1:
                    lm.Sm.showMessage("change to FreeLook view");
                    lm.cameraFreshEvent -= RotateFPSCamera;
                    lm.cameraFreshEvent -= RotateTPSCamera;
                    lm.cameraFreshEvent += RotateFreeLookCamera;

                    break;
                case 2:
                    lm.Sm.showMessage("change to TPS view");

                    lm.cameraFreshEvent -= RotateFreeLookCamera;
                    lm.cameraFreshEvent -= RotateFPSCamera;
                    lm.cameraFreshEvent += RotateTPSCamera;
                    break;
                default:
                    break;
            }
            
        }
    }

    private void RotateTPSCamera()
    {
        //TPSangleX = Mathf.Clamp(TPSangleX += Input.GetAxis("Mouse X"),-120,120);
        TPSangleX += Input.GetAxis("Mouse X");
        TPSangleY = Mathf.Clamp(TPSangleY -= Input.GetAxis("Mouse Y"), -89, 89);
        radius = Mathf.Clamp(radius -= Input.mouseScrollDelta.y, 0.8f, 10);
        //orbit = -Vector3.forward * Vector3.Distance(tpsGameObject.transform.position, cam.transform.position);

        if (CheckView(tpsGameObject.transform.position, "TPSpos"))
        {
            radius = localOffsetCollider * 0.8f;
            orbit = -player.transform.forward * radius;
            orbit = Quaternion.Euler(TPSangleY, TPSangleX, 0) * orbit;
           // cam.transform.position = LookAtObj.transform.position + offsetVector3 + orbit;
            cam.transform.position = Vector3.SmoothDamp(cam.transform.position, 
                (tpsGameObject.transform.position+ orbit), ref cameraVelocity, 0.1f);
            cam.transform.LookAt(tpsGameObject.transform.position);
        }

        else
        {
            orbit = -Vector3.forward * radius;
            orbit = Quaternion.Euler(TPSangleY, TPSangleX, 0) * orbit;
            //cam.transform.position = Vector3.Lerp(cam.transform.position,
            //    tpsGameObject.transform.position+ orbit, Time.deltaTime * 7f);
            cam.transform.position = Vector3.SmoothDamp(cam.transform.position,
                (tpsGameObject.transform.position + orbit), ref cameraVelocity, 0.1f);
            cam.transform.LookAt(tpsGameObject.transform.position);
        }
        tpsrotateangle = cam.transform.eulerAngles.y - tpsGameObject.transform.eulerAngles.y;
        
    }

    private void RotateFPSCamera()
    {
        FPSangleX += Input.GetAxis("Mouse X");
        FPSangleY = Mathf.Clamp(FPSangleY -= Input.GetAxis("Mouse Y"), -80, 80);
        if (FPSangleX > 360)
        {
            FPSangleX -= 360;
        }
        else if (FPSangleX < 0)
        {
            FPSangleX += 360;
        }
        
        cam.transform.eulerAngles =new Vector3(FPSangleY, FPSangleX, 0) ;
        player.transform.eulerAngles =new Vector3(0, FPSangleX, 0) ;

        cam.transform.position = fpsGameObject.transform.position;
        fpsrotateangle = cam.transform.eulerAngles.y;
    }

    private bool CheckView(Vector3 focusVector3,string focusname)
    {
        Ray raydown = new Ray(cam.transform.position, focusVector3 - cam.transform.position);
        Debug.DrawRay(cam.transform.position, focusVector3 - cam.transform.position);
        
        RaycastHit hitdownInfo;
        if (Physics.Raycast(raydown, out hitdownInfo,Vector3.Distance(cam.transform.position, focusVector3)))
        {
            if (hitdownInfo.collider.transform.name!=focusname && hitdownInfo.collider.transform.name != "Player" && hitdownInfo.transform.name!= "Pointer")
            {
                localOffsetCollider = Vector3.Distance(focusVector3, hitdownInfo.point);
                Debug.Log("camera hit other" + hitdownInfo.collider.transform.name);
                return true;
            }
        }

        return false;
    }
    void RotateFreeLookCamera()
    {
        angleX += Input.GetAxis("Mouse X");
        angleY = Mathf.Clamp(angleY -= Input.GetAxis("Mouse Y"), -89, 89);
        radius = Mathf.Clamp(radius -= Input.mouseScrollDelta.y, 0, 10);
        if (angleX > 360)
        {
            angleX -= 360;
        }
        else if (angleX < 0)
        {
            angleX += 360;
        }

        if (CheckView(LookAtObj.transform.position, "Player"))
        {
          //  radius -= 0.2f;
             radius = localOffsetCollider*0.85f;
            orbit = Vector3.forward * radius;
            orbit = Quaternion.Euler(-angleY, angleX, 0) * orbit;
      //      cam.transform.position = LookAtObj.transform.position + orbit;
            cam.transform.position = Vector3.SmoothDamp(cam.transform.position, (LookAtObj.transform.position + orbit),ref cameraVelocity,0.1f);

            cam.transform.LookAt(LookAtObj.transform.position);
        }
        else
        {
            orbit = Vector3.forward * radius;
            orbit = Quaternion.Euler(-angleY, angleX, 0) * orbit;
           // cam.transform.position = LookAtObj.transform.position + orbit;
            cam.transform.position = Vector3.Lerp(cam.transform.position, LookAtObj.transform.position + orbit, Time.deltaTime * 7f);
            cam.transform.LookAt(LookAtObj.transform.position);
        }


        
        
           
        
   
       
        //cam.transform.position = LookAtObj.transform.position - (cam.transform.forward * radius);
        //cam.transform.RotateAround(LookAtObj.transform.position, Vector3.up, angleX);
        //cam.transform.RotateAround(LookAtObj.transform.position, cam.transform.right, angleY);

        //  cam.transform.LookAt(LookAtObj.transform.position);
        //transfer to Quanternion
        // Quaternion mouseQX = Quaternion.Euler(0, mouseX, 0);
        //rotate 
        //cam.transform.rotation = mouseQX * cam.transform.rotation;
    }



}
