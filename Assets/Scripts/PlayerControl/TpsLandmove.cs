using UnityEngine;
[RequireComponent(typeof(PlayerModel))]

public class TpsLandmove : MonoBehaviour
{
    public PlayerModel PlayerModel;
    public PrefabManger PbManger;

    
    public void FreshMove()
    {
        

        PlayerModel.IsMove = PlayerModel.LongPressA || PlayerModel.LongPressD || PlayerModel.LongPressS ||
                             PlayerModel.LongPressW;


        PlayerModel.IsLocomotionInput = PlayerModel.ShortPressA || PlayerModel.ShortPressD || PlayerModel.ShortPressS ||
                                        PlayerModel.ShortPressW;

        //  Debug.Log("should rotate"+shouldrotate+player.transform.rotation+targetRotation);
        if (!PlayerModel.IsLocomotionInput)
        {
            PlayerModel.moveDirection = Vector3.zero;
        }
        
        //if (PlayerModel.IsLocomotionInput)
        //{
        //    RotationDirectionInit();
        //    CalRotateAngle(); 
        //    //if (Mathf.Abs( PlayerModel.Rotatedeltaangle)>5f)
        //    //{
        //    //    PlayerModel.shouldRootMotionrotate = true;
        //    //}
        //    //else
        //    //{
        //    //    PlayerModel.shouldRootMotionrotate = false;
        //    //}
                
        //}


        if (PlayerModel.IsLocomotionInput)
        {
            PlayerModel.shouldRootMotionrotate = false;
            RotationDirectionInit();
            PlayerModel.targetRotationYAngle= CalRotateAngle();
        }

        //  Debug.Log("timepress"+timepress+"shouldrotate"+shouldrotate+ "ApPlayer.IsPlayFinish"+ ApPlayer.IsPlayFinish);
        //if (PlayerModel.shouldRootMotionrotate )
        //{

        //    //rootmotion的移动  和移动玩后让shouldRootMotionrotate =false

        //    if (!PbManger.FsmData.IsInplaceturning && !PlayerModel.IsLocomotionInput) // false
        //    {
        //        PlayerModel.shouldRootMotionrotate = false;
        //    }

        //    if (!PbManger.FsmData.IsChangeMovingDir
        //        && !PlayerModel.IsLocomotionInput
        //        && (PbManger.NapPlayer.currentclipid != PbManger.FsmData.m_Idle_Turn_RT)) // true && false -> false
        //    {
        //        PlayerModel.shouldRootMotionrotate = false;

        //    }
        //}
        //else
        //{
            PlayerModel.moveDirection = Quaternion.Euler(0f, PlayerModel.targetRotationYAngle, 0f) * Vector3.forward;
            Rotate();
            //PlayerModel.Rotatedeltaangle = 0;
            PlayerModel.playerFormerAngle = PbManger.PlayerTransform.eulerAngles.y ;
            //if (PlayerModel.playerFormerAngle > 180f) PlayerModel.playerFormerAngle -= 360f;  // 获取和inspector上一样的值
            //Debug.Log("playerforangle" + PlayerModel.playerFormerAngle);
        //}
    }

    public void RotationDirectionInit()
    {
        if (PlayerModel.ShortPressD)
        {
            PlayerModel.RotateDirection.x = 1;
        }
        else if (PlayerModel.ShortPressA)
        {
            PlayerModel.RotateDirection.x = -1;
        }
        else
        {
            PlayerModel.RotateDirection.x = 0;
        }

        if (PlayerModel.ShortPressW)
        {
            PlayerModel.RotateDirection.z = 1;
        }

        else if (PlayerModel.ShortPressS)
        {
            PlayerModel.RotateDirection.z = -1;
        }
        else
        {
            PlayerModel.RotateDirection.z = 0;
        }

        PlayerModel.RotateDirection.y = 0f;
    }

    public float CalRotateAngle()
    {
        var targetRotationYAngle =
            GetRotateAngle(PlayerModel.RotateDirection.x, PlayerModel.RotateDirection.z, true);

        PlayerModel.Rotatedeltaangle = targetRotationYAngle - PlayerModel.playerFormerAngle;
        if (PlayerModel.Rotatedeltaangle < 0)
        {
            PlayerModel.Rotatedeltaangle += 360f;
        }

        PlayerModel.Rotatedeltaangle %= 360f;
        if (PlayerModel.Rotatedeltaangle > 180f)
        {
            PlayerModel.Rotatedeltaangle -= 360f;
        }

        return targetRotationYAngle;
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
        PlayerModel.targetRotation = Quaternion.Euler(0f, directionangle, 0f);
        return directionangle;
    }
    public void Rotate() //player rotate towards
    {
       
        PbManger.PlayerTransform.rotation = Quaternion.RotateTowards(PbManger.PlayerTransform.transform.rotation, PlayerModel.targetRotation, 6f);

    }
}
