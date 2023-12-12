using System;
using Homebrew;
using Unity.VisualScripting;
using UnityEngine;



[SerializeField]
public class PlayerModel : MonoBehaviour
{
    //params
        [Foldout("Global Param", true)]
    public float WalkMaxSpeed = 1.5f;
        public float JumpInitSpeed = 7f;
        public float RunMaxSpeed = 3f;
        public float SprintMaxSpeed = 5f;
        public float GroundCheckDistance = 0.03f;
        public float Gravity=9.8f;
        public float currentHorizonalSpeed = 0f;
        public float currentVerticalSpeed = 0f;
        public float BarrierDetectTherehold;
        public float BarrierAngle;
        public float verticlespeed=0f;
        public bool IsMove = false;
        public bool IsJump=false;
        public bool OnGround;
        public bool IsPlayerAlive=false;
        public float DropHeight = 1.5f;

    /// <summary>
    /// enable fun block
    /// </summary>
    /// move params
    [Foldout("enable Param", true)]
    public bool EnableLocomootionInput = true;
        public bool EnableMove = true;
        public bool EnableJump = false;
    /// <summary>
    /// ket detect params
    /// </summary>
    ///

    [Foldout("key detect Param", true)]
    public bool ShortPressW;
        public bool LongPressW;
        public bool ShortPressS;
        public bool LongPressS;
        public bool ShortPressA;
        public bool LongPressA;
        public bool ShortPressD;
        public bool LongPressD;
        public bool ShortPressSpace;
        public bool ShortPressDash;
        public bool LongPressDash;
    /// <summary>
    /// move params
    /// </summary>
    [Foldout("TPS move Param", true)]
        public Vector3 movedirection;
        public Vector3 GroundhitPos;
        public Quaternion targetRotation;
        public float targetRotationYAngle;
        public Vector3 RotateDirection;
        public bool shouldRootMotionrotate;
        public Vector3 moveDirection;
        public float playerFormerAngle;
        public float Rotatedeltaangle;
        public bool ShortPressLeftCtrl;
        public bool ShortPressX;
        public bool ShortPressNum1;
        public bool ShortPressNum2;
        public bool ShortPressFire;
        public bool LongPressFire;
        public bool ShortPressLeftAlt;
        public bool IsSprint;
        public bool IsRunning;
        public bool IsWalking;
        public float movespeed = 0f;
        public bool IsLocomotionInput;


    /// <summary>
    /// Crouch Param
    /// </summary>
        [Foldout("Crouch Param", true)]
        public bool EnableCrouch;
        public bool IsCrouchMove;
        public float crouchspeed=0.7f;
        public Vector3 CrouchRotateDirection;
        public bool CrouchIsMove;
        public bool CrouchIsLocomotionInput;
        public Vector3 CrouchmoveDirection;
        public float CrouchRotatedeltaangle;
        public bool CrouchshouldRootMotionrotate;
        public float CrouchtargetRotationYAngle;
        public float CrouchplayerFormerAngle;
        public Quaternion CrouchtargetRotation;
        public bool IsHit;
        public bool EnableFreeLook;
        public Vector3 AimMoveDirection =new Vector3();

        void Start()
        {
            EventBusManager.Instance.NonParamScribe(EventBusManager.EventType.DisAblePlayerInput,OnDisableInput);
            EventBusManager.Instance.NonParamScribe(EventBusManager.EventType.EnablePlayerInput,OnEnableInput);
        }

        private void OnEnableInput(object sender, EventArgs e)
        {
            EnableLocomootionInput = true;
            EnableMove = true;
            EnableJump = true;
    }

        private void OnDisableInput(object sender, EventArgs e)
        {
            EnableLocomootionInput = false;
            EnableMove = false;
            EnableJump = false;
        ShortPressW=false; 
        LongPressW = false;
        ShortPressS = false;
        LongPressS = false;
        ShortPressA = false;
        LongPressA = false;
        ShortPressD = false;
        LongPressD = false;
        ShortPressSpace = false;
        ShortPressDash = false;
        LongPressDash = false;
        movespeed = 0;
        }
}
