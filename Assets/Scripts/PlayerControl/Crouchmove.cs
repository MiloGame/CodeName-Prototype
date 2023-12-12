using UnityEngine;
[RequireComponent(typeof(PlayerModel))]

public class Crouchmove : MonoBehaviour
{
    public PlayerModel PlayerModel;
    public PrefabManger PbManger;

    
    public void FreshMove()
    {
        


        PlayerModel.CrouchIsMove = PlayerModel.LongPressA || PlayerModel.LongPressD || PlayerModel.LongPressS ||
                                   PlayerModel.LongPressW;
        //PlayerModel.CrouchIsMove = PlayerModel.CrouchIsMove & !PbManger.FsmData.CrouchIsChangeMovingDir;
        PlayerModel.CrouchIsMove = PlayerModel.CrouchIsMove & !PbManger.FsmData.CrouchIsInplaceturning;

        PlayerModel.CrouchIsLocomotionInput = PlayerModel.ShortPressA || PlayerModel.ShortPressD || PlayerModel.ShortPressS ||
                                        PlayerModel.ShortPressW;

        //  Debug.Log("should rotate"+shouldrotate+player.transform.rotation+targetRotation);
        if (!PlayerModel.CrouchIsLocomotionInput)
        {
            PlayerModel.CrouchmoveDirection = Vector3.zero;
        }
        
        if (PlayerModel.CrouchIsLocomotionInput && !PbManger.FsmData.CrouchIsInplaceturning)
        {
            RotationDirectionInit();
            CalRotateAngle(); 
            if (Mathf.Abs( PlayerModel.CrouchRotatedeltaangle)>5f)
            {
                PlayerModel.CrouchshouldRootMotionrotate = true;
            }
            else
            {
                PlayerModel.CrouchshouldRootMotionrotate = false;
            }
                
        }


        if (PlayerModel.CrouchIsMove )
        {
            PlayerModel.CrouchshouldRootMotionrotate = false;
            RotationDirectionInit();
            PlayerModel.CrouchtargetRotationYAngle = CalRotateAngle();
        }

        //  Debug.Log("timepress"+timepress+"shouldrotate"+shouldrotate+ "ApPlayer.IsPlayFinish"+ ApPlayer.IsPlayFinish);
        if (PlayerModel.CrouchshouldRootMotionrotate )
        {

            //还差rootmotion的移动  和移动玩后让CrouchshouldRootMotionrotate =false

            if (!PbManger.FsmData.CrouchIsInplaceturning && !PlayerModel.CrouchIsLocomotionInput)
            {
                PlayerModel.CrouchshouldRootMotionrotate = false;
            }

            //if (!PbManger.FsmData.CrouchIsChangeMovingDir
            //    && !PlayerModel.CrouchIsLocomotionInput
            //    && (PbManger.NapPlayer.currentclipid != PbManger.FsmData.m_Idle_Crouch_Turn_RT))
            //{
            //    PlayerModel.CrouchshouldRootMotionrotate = false;

            //}
        }
        else
        {
            PlayerModel.CrouchmoveDirection = Quaternion.Euler(0f, PlayerModel.CrouchtargetRotationYAngle, 0f) * Vector3.forward;
            Rotate();
            PlayerModel.CrouchplayerFormerAngle = PbManger.PlayerTransform.eulerAngles.y ;

        }
    }

    public void RotationDirectionInit()
    {
        if (PlayerModel.ShortPressD)
        {
            PlayerModel.CrouchRotateDirection.x = 1;
        }
        else if (PlayerModel.ShortPressA)
        {
            PlayerModel.CrouchRotateDirection.x = -1;
        }
        else
        {
            PlayerModel.CrouchRotateDirection.x = 0;
        }

        if (PlayerModel.ShortPressW)
        {
            PlayerModel.CrouchRotateDirection.z = 1;
        }

        else if (PlayerModel.ShortPressS)
        {
            PlayerModel.CrouchRotateDirection.z = -1;
        }
        else
        {
            PlayerModel.CrouchRotateDirection.z = 0;
        }

        PlayerModel.CrouchRotateDirection.y = 0f;
    }

    public float CalRotateAngle()
    {
         var CrouchtargetRotationYAngle =
            GetRotateAngle(PlayerModel.CrouchRotateDirection.x, PlayerModel.CrouchRotateDirection.z, true);

        PlayerModel.CrouchRotatedeltaangle = CrouchtargetRotationYAngle - PlayerModel.CrouchplayerFormerAngle;
        if (PlayerModel.CrouchRotatedeltaangle < 0)
        {
            PlayerModel.CrouchRotatedeltaangle += 360f;
        }

        PlayerModel.CrouchRotatedeltaangle %= 360f;
        if (PlayerModel.CrouchRotatedeltaangle > 180f)
        {
            PlayerModel.CrouchRotatedeltaangle -= 360f;
        }

        return CrouchtargetRotationYAngle;
    }

    private float GetRotateAngle(float xlocation, float zlocation, bool isconsidercamera)
    {
        float directionangle = Mathf.Atan2(xlocation, zlocation) * Mathf.Rad2Deg;
        if (directionangle < 0)
        {
            directionangle += 360f;
        }

        if (isconsidercamera)
        {
            directionangle += PbManger.MainCamera.transform.eulerAngles.y;
        }
        
        if (directionangle > 360f) directionangle -= 360f; 
        else if (directionangle < 360f) directionangle += 360f;
        directionangle %= 360f;
        PlayerModel.CrouchtargetRotation = Quaternion.Euler(0f, directionangle, 0f);
        return directionangle;
    }
    public void Rotate() //player rotate towards
    {
       
        PbManger.PlayerTransform.rotation = Quaternion.RotateTowards(PbManger.PlayerTransform.transform.rotation, PlayerModel.CrouchtargetRotation, 2f);

    }
}
