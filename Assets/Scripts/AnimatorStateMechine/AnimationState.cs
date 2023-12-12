using Assets.Scripts;
using UnityEngine;
using UnityEngine.Animations.Rigging;


public abstract class AnimationState
{
    public AniParams StateParams;
    public AnimatorPlayer Ap;
    public AnimationData AniData;
    public AnimationData.AnimationType anitype;
    public GameObject CamGameObject;
    public PlayerControler PcControler;
    public CamerBehavior CamerControler;

  //  public AnimatorRootMotionDeal Ard = null;
    public abstract AnimationState HandleInput();
    public abstract void Enter();
    public abstract void Init();
}


