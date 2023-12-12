using System;
using System.Collections;
using System.Collections.Generic;
using Homebrew;
using UnityEngine;

public class FSMData : MonoBehaviour
{
    public PrefabManger PbManger;
    public PlayerModel PlayerModel;
    public CameraModel CameraModel;
    public NewAnimatorPlayer NaPlayer;

    /// <summary>
    /// hash ²ÎÊýÖµ
    /// </summary>

    public enum ClipType
    {
        Spawn,
      
        FpsLocomotion,
        Idle,

        Jump,
        Land,
        Locomotiom,
        MovingLand,
        
       
        Stand_to_Crouch,
        
        Suddenstop,
  
        Crouch_to_Stand,
        CrouchIdle,
        Idle_Crouch_Turn_RT,
        CrouchLocomotiom,
        Crouch_To_Stop_RT,
        NewState,
        Idle_to_aim_crouch_RT
    }
    public Dictionary<ClipType, FSMBaseState> FsmStates = new Dictionary<ClipType, FSMBaseState>();
    public int m_LocomotionHoritional = Animator.StringToHash("LocomotionHoritional");
    public int m_LocomotionVerticle = Animator.StringToHash("LocomotionVerticle");


    public float MoeTurnAngle;
    public int m_JumpCurSpeed = Animator.StringToHash("JumpCurSpeed");
    public float IdleTurnAngle;
    public int m_MovingLandCurSpeed = Animator.StringToHash("MovingLandCurSpeed");

    [Foldout("Crouch Param", true)]
    public bool CrouchIsChangeMovingDir;
    public bool CrouchIsInplaceturning;
    public int m_Idle_Crouch_Turn_RT = Animator.StringToHash("Idle_Crouch_Turn_RT");
    public float CrouchIdleTurnAngle;
    public int m_CrouchLocomotionRotationAngle=Animator.StringToHash("CrouchLocomotionRotationAngle");
    public int m_CrouchIdletoaimMoveHorizontal = Animator.StringToHash("CrouchIdletoaimMoveHorizontal");
    public int m_CrouchIdletoaimMoveVerticle = Animator.StringToHash("CrouchIdletoaimMoveVerticle");
    public float CrouchMoeTurnAngle;
    public int m_CrouchLocomotionStopHor = Animator.StringToHash("CrouchLocomotionStopHor");
    public int m_CrouchLocomotionStopVer=Animator.StringToHash("CrouchLocomotionStopVer");
    public int m_CrouchLocomotionHoritional=Animator.StringToHash("CrouchLocomotionHoritional");
    public int m_CrouchLocomotionVerticle=Animator.StringToHash("CrouchLocomotionVerticle");


    public FSMBaseState CreateState(ClipType clip,string clippname)
    {
        FSMBaseState resState = null;

        switch (clip )
        {

            case ClipType.Spawn:
                if (FsmStates.ContainsKey(clip))
                {
                    resState = FsmStates[clip];
                }
                else
                {
                    resState = new Spawn(this, NaPlayer,clippname);
                }
                break;
           case ClipType.FpsLocomotion:
                if (FsmStates.ContainsKey(clip))
                {
                    resState = FsmStates[clip];
                }
                else
                {
                    resState = new FpsLocomotion(this, NaPlayer, clippname);
                }
                break;
            case ClipType.Idle:
                if (FsmStates.ContainsKey(clip))
                {
                    resState = FsmStates[clip];
                }
                else
                {
                    resState = new Idle(this, NaPlayer, clippname);
                }
                break;
        
          
            case ClipType.Jump:
                if (FsmStates.ContainsKey(clip))
                {
                    resState = FsmStates[clip];
                }
                else
                {
                    resState = new Jump(this, NaPlayer, clippname);
                }
                break;
            case ClipType.Land:
                if (FsmStates.ContainsKey(clip))
                {
                    resState = FsmStates[clip];
                }
                else
                {
                    resState = new Land(this, NaPlayer, clippname);
                }
                break;
            case ClipType.Locomotiom:
                if (FsmStates.ContainsKey(clip))
                {
                    resState = FsmStates[clip];
                }
                else
                {
                    resState = new Locomotiom(this, NaPlayer, clippname);
                }
                break;
            case ClipType.MovingLand:
                if (FsmStates.ContainsKey(clip))
                {
                    resState = FsmStates[clip];
                }
                else
                {
                    resState = new MovingLand(this, NaPlayer, clippname);
                }
                break;
           
            
            case ClipType.Suddenstop:
                if (FsmStates.ContainsKey(clip))
                {
                    resState = FsmStates[clip];
                }
                else
                {
                    resState = new Suddenstop(this, NaPlayer, clippname);
                }
                break;
            
            case ClipType.Stand_to_Crouch:
                if (FsmStates.ContainsKey(clip))
                {
                    resState = FsmStates[clip];
                }
                else
                {
                    resState = new Stand_to_Crouch(this, NaPlayer, clippname);
                }
                break;
            
            case ClipType.Crouch_to_Stand:
                if (FsmStates.ContainsKey(clip))
                {
                    resState = FsmStates[clip];
                }
                else
                {
                    resState = new Crouch_to_Stand(this, NaPlayer, clippname);
                }
                break;
            case ClipType.CrouchIdle:
                if (FsmStates.ContainsKey(clip))
                {
                    resState = FsmStates[clip];
                }
                else
                {
                    resState = new CrouchIdle(this, NaPlayer, clippname);
                }
                break;
            case ClipType.Idle_Crouch_Turn_RT:
                if (FsmStates.ContainsKey(clip))
                {
                    resState = FsmStates[clip];
                }
                else
                {
                    resState = new Idle_Crouch_Turn_RT(this, NaPlayer, clippname);
                }
                break;
            case ClipType.CrouchLocomotiom:
                if (FsmStates.ContainsKey(clip))
                {
                    resState = FsmStates[clip];
                }
                else
                {
                    resState = new CrouchLocomotiom(this, NaPlayer, clippname);
                }
                break;
            case ClipType.Crouch_To_Stop_RT:
                if (FsmStates.ContainsKey(clip))
                {
                    resState = FsmStates[clip];
                }
                else
                {
                    resState = new Crouch_To_Stop_RT(this, NaPlayer, clippname);
                }
                break;
            case ClipType.NewState:
                if (FsmStates.ContainsKey(clip))
                {
                    resState = FsmStates[clip];
                }
                else
                {
                    resState = new NewState(this, NaPlayer, clippname);
                }
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(clip), clip, null);
        }

        FsmStates[clip] = resState;
        return resState;
    }
}
