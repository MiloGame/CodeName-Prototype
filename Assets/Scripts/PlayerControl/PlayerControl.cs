using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Analytics;
using UnityEngine;
using UnityEngine.Events;


//[Serializable]
//public class FloatEvent : UnityEvent<float> { }

[RequireComponent(typeof(PlayerModel))]
[RequireComponent(typeof(DetectLocomotionInput))]
[RequireComponent(typeof(JumpMove))]
public class PlayerControl : MonoBehaviour
{

    public UnityEvent onLocomotionInput;
    public UnityEvent OnJumpMove;
    public UnityEvent OnLandMove;
    public UnityEvent OnAimLandMove;
    public UnityEvent OnUnLockHit;
    public UnityEvent OnSlopMove;
    public UnityEvent OnCrouchMove;
    public PlayerModel playerModel;
    public PrefabManger PbManger;
    public NewAnimatorPlayer NapPlayer;
    public bool runorwalk;
    [SerializeField]
    private bool changestate=false;

    public bool isneedupdate=false;
    public Vector3 perpos;
    public Quaternion perrotpos;

    //public FloatEvent onDamage;
    public void FreshLocomotionInput()
    {
        if (playerModel.EnableLocomootionInput)
        {
            onLocomotionInput.Invoke();
        }
    }

    public void Jump()
    {
          
        

            if (playerModel.ShortPressSpace && playerModel.OnGround)
            {
                playerModel.verticlespeed = playerModel.JumpInitSpeed;
                playerModel.OnGround = false;
                playerModel.IsJump = true;
            }
           
        if (playerModel.EnableJump)
        {
            OnJumpMove.Invoke();
            //Debug.Log("player trans"+ PbManger.PlayerTransform.position.y +"groundhit" + playerModel.GroundhitPos.y);
            if (PbManger.PlayerTransform.position.y < playerModel.GroundhitPos.y)
            {
                playerModel.OnGround = true;
                playerModel.IsJump = false;
                playerModel.verticlespeed = 0;
                PbManger.PlayerTransform.position = playerModel.GroundhitPos;
            }

        }
        
     
    }

    public void LandMove()
    {
        if (playerModel.EnableMove)
        {
            playerModel.EnableCrouch = false;
            if (playerModel.EnableFreeLook)
            {
                playerModel.moveDirection = Vector3.zero;

                OnLandMove.Invoke();
            }
                
            else
            {
                playerModel.moveDirection = Vector3.zero;
                OnAimLandMove.Invoke();
            }
            OnUnLockHit.Invoke();
            if (!playerModel.IsHit)
            
            {
                if (playerModel.IsLocomotionInput && !NapPlayer.IsneedRootmotion)
                {
                    //Debug.Log("root pos ros" + perpos+perrotpos +"now" + PbManger.PlayerTransform.position + PbManger.PlayerTransform.rotation);
                    //if (isneedupdate)
                    //{
                    //    PbManger.PlayerTransform.position = perpos;
                    //    PbManger.PlayerTransform.rotation = perrotpos;
                    //    isneedupdate = false;
                    //}
                    PbManger.PlayerTransform.position += playerModel.moveDirection.normalized * playerModel.movespeed * Time.deltaTime;
                }
                //else if (playerModel.IsMove && NapPlayer.IsneedRootmotion)
                //{
                //    isneedupdate = true;
                //    perpos = PbManger.PlayerTransform.position;
                //    perrotpos = PbManger.PlayerTransform.rotation;
                //}
            }
            

        }
    }
    public void CrouchMove()
    {
        if (playerModel.EnableCrouch)
        {

            OnCrouchMove.Invoke();
            if (playerModel.CrouchIsMove && !NapPlayer.IsneedRootmotion)
            {
                PbManger.PlayerTransform.position += playerModel.CrouchmoveDirection.normalized * playerModel.crouchspeed * Time.deltaTime;
            }

        }

    }

    public void SlopMove()
    {
        float distance=0f;
        if (!playerModel.ShortPressSpace && playerModel.OnGround )
        {
            OnSlopMove.Invoke();
            //if (PbManger.PlayerTransform.position.y > (playerModel.GroundhitPos.y + playerModel.GroundCheckDistance))
            //{
            //    distance = PbManger.PlayerTransform.position.y - playerModel.GroundhitPos.y;
            //}


            //if ((distance > playerModel.DropHeight) && playerModel.IsPlayerAlive)
            //    playerModel.OnGround = false;
            //else
            //    playerModel.OnGround = true;
            
            PbManger.PlayerTransform.position = playerModel.GroundhitPos;
            
        }

        //Debug.Log(Vector3.Dot(PbManger.PlayerTransform.forward.normalized, (playerModel.BarrierForward - playerModel.ForBarrraypoint).normalized));
    }
    public void ChangeMoveSpeed()
    {
        
        if (playerModel.ShortPressLeftAlt)
        {
            runorwalk = !runorwalk;
        }

    
        //playerModel.movespeed = runorwalk ? playerModel.RunMaxSpeed : playerModel.WalkMaxSpeed;
        playerModel.IsRunning = runorwalk;
        playerModel.IsWalking = !runorwalk;
        if (!playerModel.IsLocomotionInput)
        {
            //playerModel.IsSprint = false;
            //playerModel.IsRunning = false;
            //playerModel.IsWalking = false;
            playerModel.movespeed = Mathf.MoveTowards(playerModel.movespeed, 0, Time.deltaTime * 7f);
        }
        if (playerModel.LongPressDash)
        {
            playerModel.IsSprint = true;
            playerModel.IsRunning = false;
            playerModel.IsWalking = false;
        }
        else
        {
            playerModel.IsSprint = false;
        }
        if (playerModel.IsRunning)
        {
            playerModel.movespeed = Mathf.MoveTowards(playerModel.movespeed, playerModel.RunMaxSpeed, Time.deltaTime * 7f);
        }
        else if (playerModel.IsWalking)
        {
            playerModel.movespeed = Mathf.MoveTowards(playerModel.movespeed, playerModel.WalkMaxSpeed, Time.deltaTime * 7f);
        }
        else if (playerModel.IsSprint)
        {
            playerModel.movespeed = Mathf.MoveTowards(playerModel.movespeed, playerModel.SprintMaxSpeed, Time.deltaTime * 7f);
        }

        if (playerModel.EnableCrouch) playerModel.movespeed = 1f;

    }
    public void FreshUpdate()
    {
        FreshLocomotionInput();
        Jump();
        LandMove();
        CrouchMove();
        SlopMove();
        //onDamage.Invoke(0.01f);
     
    }
}
