
using System;
using System.Collections.Generic;
using System.IO;
using Assets.Scripts.BehaviorTree;
using JetBrains.Annotations;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.AI;

//storing behaviortree runtime data / configure data
[Serializable]
public class BlackBoard 
{
   //[NonSerialized]public Transform PlayerTransform;
    public float AiHealthTheshold;
    [NonSerialized] public Vector3 bestCoverPos;
    [NonSerialized] public Vector3 BornPosition;
    [NonSerialized] public NavMeshAgent aiAgent;
    [NonSerialized] public Transform SelfTransform;
    [NonSerialized] public Animator EnemyAnimator;
    [NonSerialized]public BehaviorTreeManger BTManager;

    [NonSerialized]public Vector3 NextPatrolPoint;
    public Transform PlayerTransform;


    public bool EnemySeePlayerNow;

    public bool EnemySeePlayerPre;

    public bool LoseSightstartCount;

    public float RotationSpeed;

    public float radiusangle;

    public float viewRadius;

    public float PreHealth;

    public float CurrentHealth;

    public NavMeshPath BossNavPath;

    public bool NeedRegrenate;

    public Vector3 BossTargetPos;
    public float JumpDistance;
    public float remainDis;
    public bool Jumping;
    public bool waitingcoolen;
    public float DistanceToSupposePos;
    public bool ShouldRotate;
    public Vector3 RotatereletiveDir;
    public bool IsDead;
    public bool Ishit;

    //[NonSerialized]public Vector3[] patrolPos;
    //public float miniMoveTheshold;
    //public List<Collider> avaliableCovers;
    //public float FindCoverRadius;
    //public float FindCoverOffset;
    //public float FindObjDefaultHeight;
    //public float FindObjDefaultDepth;
    //public float rangeTheshold;
    //public float maxChasingTheshold;
    //public float patrolRadius;
    //public int patrolNums;


    public BlackBoard(BehaviorTreeManger btManager)
    {
        IsDead = false;
        JumpDistance = 10f;
        BossNavPath = null;
        CurrentHealth = 100f;
        BTManager = btManager;
        //PlayerTransform = playerTransform;
        BornPosition = btManager.EnemyObj.transform.position;
        SelfTransform = btManager.EnemyObj.transform;
        BossTargetPos = SelfTransform.position;

        aiAgent = btManager.AiAgent;
        EnemyAnimator = btManager.EnemyAnimator;

        AiHealthTheshold = 30f;
        viewRadius = 6f;
        radiusangle = 120f;
        RotationSpeed = 15f;

        PreHealth = CurrentHealth;

        

        //  movePosition=null;
        //miniMoveTheshold = 1f;
        //avaliableCovers = new List<Collider>();
        //FindCoverRadius = 5f;
        //FindCoverOffset = 2f;
        //rangeTheshold = 5f;
        //patrolNums = 5;
        //patrolRadius = 4f;
        //FindObjDefaultDepth = 5f;
        //FindObjDefaultHeight = 0.5f;
    }
}
