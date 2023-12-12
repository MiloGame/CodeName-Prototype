using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEditor;
using UnityEngine;

[Serializable]
public class Node 
{

    public enum AllState
    {
        Running,
        Failure,
        Success
    }

    public enum TreeNodeType
    {
        DebugLogNode,
        WaitNode,
        RepeatedNode,
        SecquenceNode,
        RootNode,
        Inverter,
        Selector,
        ChaseNode,
        EnemyHealthNode,
        GotoPosistionNode,
        IsCoverAvaliableNode,
        IsCoveredNode,
        ReturnToBornPosNode,
        SetCoverPositionNode,
        ShootingNode,
        DoPatrolNode,
        GetPatrolNode,
        UnderAttackNode,
        EnemyViewNode,
        IfNode,
        ForEachNode,
        AlwaysTrueNode,
        DecideToChaseNode,
        LoseViewNode,
        AttAckPlayerNode,
        BossHealthUnderLimitNode,
        BossDistanceUnderLimitNode,
        BossTurnNode,
        BossGrenateNavPathNode,
        BossAttackNode,
        BossChasePlayerNode,
        BossJumpNode,
        BossGenreateTinyEnemyNode,
        WaitCoolenNode,
        BossShouldJumpNode,
        BossAimNode
    }

    public AllState State = AllState.Running;
    public bool IsExcuted = false;
    public string titlename;
    public float width = 600f;
    public float height = 100f;
    public float positionx;
    public float positiony;
    public TreeNodeType nodetype;
    [NonSerialized] public BlackBoard TreeBlackBoard;

    public AllState UpdateState()
    {

        BehaviorTreeFunInterface interfaceFunInterface = this  as BehaviorTreeFunInterface;
        if (!IsExcuted)
        {
            interfaceFunInterface.OnStart();
            IsExcuted = true;
        }

        State = interfaceFunInterface.OnUpdate();

        if (State == AllState.Failure || State == AllState.Success)
        {
            interfaceFunInterface.OnStop();
            IsExcuted = false;
        }

        //object[] info1={ State};
        //object[] info2={ IsExcuted};
        //NotificationCenter.Instance.PostNotification(titlename + "statechanged", info1);
        //NotificationCenter.Instance.PostNotification(titlename + "runchanged", info2);
        return State;
    }
    //public void Abort()
    //{
    //    BehaveTree.Traverse(this, (node) =>
    //    {
    //        node.State = AllState.Running;
    //        node.IsExcuted = false;
    //        node.OnStop();
    //    });
    //}

}
