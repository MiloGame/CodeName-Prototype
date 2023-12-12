using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerModel))]
public class DetectOnGround : MonoBehaviour
{
    public PlayerModel PlayerModel;
    public PrefabManger PbManger;

    public void CheckGround()
    {
        RaycastHit hit;
        Vector3 raypoint = PbManger.PlayerTransform.position + PbManger.PlayerTransform.up.normalized * 0.5f;
        Ray ray = new Ray(raypoint, -PbManger.PlayerTransform.up);
        Debug.DrawRay(ray.origin,ray.direction);
        //RaycastHit hitinfo;
        //Physics.Raycast(raypoint, -playertrans.up,out hitinfo, 100, ~(1 << 8));
        
        if (Physics.Raycast(ray,out hit,1000,~(1<<8 | 1<<6 )))
        {

            //Debug.Log(hit.transform.name+hit.collider.name+hit.collider.transform.position+ hit.collider.ClosestPoint(playertrans.position)+hit.point);
            PlayerModel.GroundhitPos = hit.point + PbManger.PlayerTransform.up.normalized * PlayerModel.GroundCheckDistance;
            //Debug.Log("hit name" + hit.transform.name + "pos"+PlayerModel.GroundhitPos.y+"laymask"+ ~(1 << 8 + 1 << 6));

        }

    }

    
}


