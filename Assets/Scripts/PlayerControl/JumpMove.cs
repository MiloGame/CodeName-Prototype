using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(PlayerModel))]
[RequireComponent(typeof(DetectOnGround))]
public class JumpMove : MonoBehaviour
{
    public PlayerModel playerModel;
    public PrefabManger PbManger;
    public void moveFresh()
    {
        if (!playerModel.OnGround)
        {
            if (playerModel.verticlespeed <= (playerModel.JumpInitSpeed / 2))
            {
                playerModel.verticlespeed -= playerModel.Gravity * Time.deltaTime;
            }
            else
            {
                playerModel.verticlespeed -= 2 * playerModel.Gravity * Time.deltaTime;
            }
        }
        
        var ypos = playerModel.verticlespeed * Time.deltaTime;
        PbManger.PlayerTransform.position += new Vector3(0,ypos,0);
    }
}
