using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CameraModel))]
public class CameraAvoidCollision : MonoBehaviour
{
    public CameraModel CameraModel;
    public PrefabManger PbManger;

    private int masklayermask;

    // œ‡ª˙±‹’œ
    public void CheckView()
    {
        Ray raydown = new Ray(PbManger.MainCamera.transform.position, CameraModel.LookAtPos - PbManger.MainCamera.transform.position);
        //Debug.DrawRay(PbManger.MainCamera.transform.position,  PbManger.LookAtPosTransform.position - PbManger.MainCamera.transform.position);

        RaycastHit hitdownInfo;
        masklayermask = ~(1 << 8 + 1 << 10);
        if (Physics.Raycast(raydown, 
                out hitdownInfo,
                Vector3.Distance(PbManger.MainCamera.transform.position, CameraModel.LookAtPos),masklayermask))
        {
            //if (hitdownInfo.collider.transform.name != "Player" && hitdownInfo.transform.tag!="Enemy")
            //{
               CameraModel.camhitColliderpos = Vector3.Distance(CameraModel.LookAtPos, hitdownInfo.point);
            //Debug.Log("camera hit other" + hitdownInfo.collider.transform.name);
            CameraModel.Ishitcollider = true; 
                return;
            //}
        }

        CameraModel.Ishitcollider = false;
    }
}
