using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AimLandMove : MonoBehaviour
{
    public PlayerModel PlayerModel;
    public PrefabManger PbManger;
    public void FreshAimMove()
    {
        PlayerModel.IsLocomotionInput = PlayerModel.ShortPressA || PlayerModel.ShortPressD || PlayerModel.ShortPressS ||
                                        PlayerModel.ShortPressW;
        if (!PlayerModel.IsLocomotionInput)
        {
            PlayerModel.AimMoveDirection = Vector3.zero;
            PlayerModel.moveDirection = Vector3.zero;
        }
        else
        {
            PlayerModel.targetRotationYAngle = TPSGetRotateAngle();
            MoveDirectionInit();
            PlayerModel.moveDirection = Quaternion.Euler(0f, PlayerModel.targetRotationYAngle, 0f) * Vector3.forward * PlayerModel.AimMoveDirection.z +
                                        PbManger.PlayerTransform.right * PlayerModel.AimMoveDirection.x;
        }


        Rotate();
        

    }
    void MoveDirectionInit()
    {
        if (PlayerModel.ShortPressD)
        {
            PlayerModel.AimMoveDirection.x = 1;
        }
        else if (PlayerModel.ShortPressA)
        {
            PlayerModel.AimMoveDirection.x = -1;
        }
        else
        {
            PlayerModel.AimMoveDirection.x = 0;
        }

        if (PlayerModel.ShortPressW)
        {
            PlayerModel.AimMoveDirection.z = 1;
        }

        else if (PlayerModel.ShortPressS)
        {
            PlayerModel.AimMoveDirection.z = -1;
        }
        else
        {
            PlayerModel.AimMoveDirection.z = 0;
        }

        PlayerModel.AimMoveDirection.y = 0f;
    }
    private float TPSGetRotateAngle()
    {
        float directionangle = 0;
       
        directionangle += PbManger.MainCamera.transform.eulerAngles.y;
        


        if (directionangle > 360f) directionangle -= 360f;
        else if (directionangle < 360f) directionangle += 360f;
        directionangle %= 360f;
        PlayerModel.targetRotation = Quaternion.Euler(0f, directionangle, 0f);
        return directionangle;
    }
    void Rotate() //player rotate towards
    {

        PbManger.PlayerTransform.rotation = Quaternion.RotateTowards(PbManger.PlayerTransform.transform.rotation, PlayerModel.targetRotation, 8f);

    }
}
