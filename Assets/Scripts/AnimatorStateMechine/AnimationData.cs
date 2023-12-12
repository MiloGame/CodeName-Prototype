using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.Animations.Rigging;


public class AnimationData
{
    public LoadManager lm;
    public enum AnimationType
    {
        IdleAnimateState,
        TurnPlus,
        TurnInv,
        MoveState,
        JumpInAir,
        StartJump,
        Landing,
        AimState,
        RifleEquip,
        SheldEquipState,
        SheldDisArm,
        ShotMove,
        RifileReload,
        AimOffset
    }
    public Dictionary<AnimationType, AnimationState> anidata = new Dictionary<AnimationType, AnimationState>();

    public AnimationData(LoadManager load)
    {
        lm = load;
    }
    public AnimationState CreateState(AnimationType aniType,AnimatorPlayer animatorPlayer,
        PlayerControler pc,CamerBehavior cm,AnimatorRootMotionDeal ard=null)
    {
        AnimationState reState;
        switch (aniType)
        {
            case AnimationType.IdleAnimateState:
                if (anidata.ContainsKey(aniType))
                {
                    reState = anidata[aniType];
                }
                else
                {
                    reState = new IdleAnimateState();
                    reState.Ap = animatorPlayer;
                    //reState.Ard = ard;
                    reState.anitype = aniType;
                    reState.AniData = this;
                    reState.Init();
                    anidata.Add(aniType, reState);
                }
                break;

            case AnimationType.TurnPlus:
                if (anidata.ContainsKey(aniType))
                {
                    reState = anidata[aniType];
                }
                else
                {
                    reState = new TurnPlus();
                    reState.Ap = animatorPlayer;
                    //reState.Ard = ard;
                    reState.anitype = aniType;
                    reState.AniData = this;
                    reState.Init();
                    anidata.Add(aniType, reState);
                }
                break;
            case AnimationType.TurnInv:
                if (anidata.ContainsKey(aniType))
                {
                    reState = anidata[aniType];
                }
                else
                {
                    reState = new TurnInv();
                    reState.Ap = animatorPlayer;
                    //reState.Ard = ard;
                    reState.anitype = aniType;
                    reState.AniData = this;
                    reState.Init();
                    anidata.Add(aniType, reState);
                }
                break;
            case AnimationType.MoveState:
                if (anidata.ContainsKey(aniType))
                {
                    reState = anidata[aniType];
                }
                else
                {
                    reState = new MoveState();
                    reState.Ap = animatorPlayer;
                    //reState.Ard = ard;
                    reState.anitype = aniType;
                    reState.AniData = this;
                    reState.Init();
                    anidata.Add(aniType, reState);
                }
                break;
            case AnimationType.StartJump:
                if (anidata.ContainsKey(aniType))
                {
                    reState = anidata[aniType];
                }
                else
                {
                    reState = new StartJump();
                    reState.Ap = animatorPlayer;
                    //reState.Ard = ard;
                    reState.anitype = aniType;
                    reState.AniData = this;
                    reState.Init();
                    anidata.Add(aniType, reState);
                }
                break;
            case AnimationType.JumpInAir:
                if (anidata.ContainsKey(aniType))
                {
                    reState = anidata[aniType];
                }
                else
                {
                    reState = new JumpInAir();
                    reState.Ap = animatorPlayer;
                    //reState.Ard = ard;
                    reState.anitype = aniType;
                    reState.AniData = this;
                    reState.Init();
                    anidata.Add(aniType, reState);
                }
                break;
            case AnimationType.Landing:
                if (anidata.ContainsKey(aniType))
                {
                    reState = anidata[aniType];
                }
                else
                {
                    reState = new Landing();
                    reState.Ap = animatorPlayer;
                    //reState.Ard = ard;
                    reState.anitype = aniType;
                    reState.AniData = this;
                    reState.Init();
                    anidata.Add(aniType, reState);
                }
                break;
            case AnimationType.AimState:
                if (anidata.ContainsKey(aniType))
                {
                    reState = anidata[aniType];
                }
                else
                {
                    reState = new AimState();
                    reState.Ap = animatorPlayer;
                    //reState.Ard = ard;
                    reState.anitype = aniType;
                    reState.AniData = this;
                    reState.Init();
                    anidata.Add(aniType, reState);
                }
                break;
            case AnimationType.ShotMove:
                if (anidata.ContainsKey(aniType))
                {
                    reState = anidata[aniType];
                }
                else
                {
                    reState = new ShotMove();
                    reState.Ap = animatorPlayer;
                    //reState.Ard = ard;
                    reState.anitype = aniType;
                    reState.AniData = this;
                    reState.Init();
                    anidata.Add(aniType, reState);
                }
                break;

            case AnimationType.AimOffset:
                if (anidata.ContainsKey(aniType))
                {
                    reState = anidata[aniType];
                }
                else
                {
                    reState = new AimOffset();
                    reState.Ap = animatorPlayer;
                    //reState.Ard = ard;
                    reState.anitype = aniType;
                    reState.AniData = this;
                    reState.Init();
                    anidata.Add(aniType, reState);
                }
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(aniType), aniType, null);
        }

        reState.Ap = animatorPlayer;
        reState.CamerControler = cm;
        reState.PcControler = pc;
        ////reState.Ard = ard;
        return reState;
    }
}
