using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectBarrier : MonoBehaviour
{
    public PlayerModel PlayerModel;
    public PrefabManger PbManger;
    private RaycastHit hit;
    private Ray rayfront;
    private Ray rayback;
    private Ray rayleft;
    private Ray rayright;
    public bool hitfront;
    public bool hitback;
    public bool hitright;
    public bool hitleft;
    private Vector3 raypointfront;
    private Vector3 raypointback;
    private Vector3 raypointleft;
    private Vector3 raypointright;
    public float thereshold = 0.1f;
    public float offset = 0.5f;
    public void CheckBarrier()
    {
        raypointfront = PbManger.PlayerTransform.position + PbManger.PlayerTransform.up.normalized * 1f + PbManger.PlayerTransform.forward.normalized* offset;
        raypointback = PbManger.PlayerTransform.position + PbManger.PlayerTransform.up.normalized * 1f - PbManger.PlayerTransform.forward.normalized * offset;
        raypointleft = PbManger.PlayerTransform.position + PbManger.PlayerTransform.up.normalized * 1f - PbManger.PlayerTransform.right.normalized * offset;
        raypointright = PbManger.PlayerTransform.position + PbManger.PlayerTransform.up.normalized * 1f + PbManger.PlayerTransform.right.normalized * offset;
        rayfront = new Ray(raypointfront,PbManger.PlayerTransform.forward);
        Debug.DrawRay(rayfront.origin, rayfront.direction);
        rayback = new Ray(raypointback, -PbManger.PlayerTransform.forward);
        Debug.DrawRay(rayback.origin, rayback.direction); 
        rayleft = new Ray(raypointleft, -PbManger.PlayerTransform.right);
        Debug.DrawRay(rayleft.origin, rayleft.direction);
        rayright = new Ray(raypointright, PbManger.PlayerTransform.right);
        Debug.DrawRay(rayright.origin, rayright.direction);
        if (Physics.Raycast(rayfront, out hit, 1000, ~(1 << 8)))
        {
            var distance = Vector3.Distance(hit.point, raypointfront);
            if (distance < thereshold) hitfront = true;
            else hitfront = false;
        }

        if (Physics.Raycast(rayback, out hit, 1000, ~(1 << 8)))
        {
            var distance = Vector3.Distance(hit.point, raypointback);
            if (distance < thereshold ) hitback = true;
            else hitback = false;
        }
        if (Physics.Raycast(rayleft, out hit, 1000, ~(1 << 8)))
        {
            var distance = Vector3.Distance(hit.point, raypointleft);
            if (distance < thereshold) hitleft = true;
            else hitleft = false;
        }
        if (Physics.Raycast(rayright, out hit, 1000, ~(1 << 8)))
        {
            var distance = Vector3.Distance(hit.point, raypointright);
            if (distance < thereshold) hitright = true;
            else hitright = false;
        }

        if (!hitfront && !hitback && !hitright && !hitleft )
        {
            PlayerModel.IsHit = false;
        }
        else
        {
            PlayerModel.IsHit = true;
        }


    }

    public void UnlockHit()
    {
        if (hitfront && (PlayerModel.ShortPressA || PlayerModel.ShortPressD ))
        {


            PlayerModel.IsHit = false;
        }
            

        if (hitback && (PlayerModel.ShortPressA  || PlayerModel.ShortPressD))
            PlayerModel.IsHit = false;

        if (hitright && (PlayerModel.ShortPressW || PlayerModel.ShortPressS ))
            PlayerModel.IsHit = false;

        if (hitleft && ( PlayerModel.ShortPressS || PlayerModel.ShortPressW))
            PlayerModel.IsHit = false;
    }
}
