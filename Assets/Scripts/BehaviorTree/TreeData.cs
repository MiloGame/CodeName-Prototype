

using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TreeData 
{
    public List<Node> nodesData = new List<Node>();
    private int index = 0;




    public Node CreateNode(Node.TreeNodeType nodeType)
    {
        Debug.Log("create node succeed"+nodeType);
        index++;
        switch (nodeType)
        {
            case Node.TreeNodeType.DebugLogNode:
                DebugLogNode resDebugLogNode =  new DebugLogNode();
                resDebugLogNode.titlename = "DebugLogNode"+index;
                resDebugLogNode.nodetype = nodeType;
                return resDebugLogNode;
            case Node.TreeNodeType.RepeatedNode:
                RepeatedNode resRepeatedNode = new RepeatedNode();
                resRepeatedNode.titlename = "RepeatedNode" + index;
                resRepeatedNode.nodetype = nodeType;
                return resRepeatedNode;
            case Node.TreeNodeType.Inverter:
                Inverter resInverter = new Inverter();
                resInverter.titlename = "InverterNode" + index;
                resInverter.nodetype = nodeType;
                return resInverter;
            case Node.TreeNodeType.SecquenceNode:
                SecquenceNode resSecquenceNode = new SecquenceNode();
                resSecquenceNode.titlename = "SecquenceNode" + index;
                resSecquenceNode.nodetype = nodeType;
                return resSecquenceNode;
            case Node.TreeNodeType.Selector:
                Selector resSelector = new Selector();
                resSelector.titlename = "SelectorNode" + index;
                resSelector.nodetype = nodeType;
                return resSelector;
            case Node.TreeNodeType.WaitNode:
                WaitNode resWaitNode = new WaitNode();
                resWaitNode.titlename = "WaitNode" + index;
                resWaitNode.nodetype = nodeType;
                return resWaitNode;

            case Node.TreeNodeType.RootNode:
                RootNode resRootNode = new RootNode();
                resRootNode.titlename = "RootNode" + index;
                resRootNode.nodetype = nodeType;
                return resRootNode;

            case Node.TreeNodeType.ChaseNode:
                ChaseNode resChaseNode = new ChaseNode();
                resChaseNode.titlename = "ChaseNode" + index;
                resChaseNode.nodetype = nodeType;
                return resChaseNode;
            case Node.TreeNodeType.EnemyHealthNode:
                EnemyHealthNode resEnemyHealthNode = new EnemyHealthNode();
                resEnemyHealthNode.titlename = "EnemyHealthNode" + index;
                resEnemyHealthNode.nodetype = nodeType;
                return resEnemyHealthNode;
            case Node.TreeNodeType.GotoPosistionNode:
                GotoPosistionNode resGotoPosistionNode = new GotoPosistionNode();
                resGotoPosistionNode.titlename = "GotoPosistionNode" + index;
                resGotoPosistionNode.nodetype = nodeType;
                return resGotoPosistionNode;
            case Node.TreeNodeType.IsCoverAvaliableNode:
                IsCoverAvaliableNode resIsCoverAvaliableNode = new IsCoverAvaliableNode();
                resIsCoverAvaliableNode.titlename = "IsCoverAvaliableNode" + index;
                resIsCoverAvaliableNode.nodetype = nodeType;
                return resIsCoverAvaliableNode;
            case Node.TreeNodeType.IsCoveredNode:
                IsCoveredNode resIsCoveredNode = new IsCoveredNode();
                resIsCoveredNode.titlename = "IsCoveredNode" + index;
                resIsCoveredNode.nodetype = nodeType;
                return resIsCoveredNode;
            case Node.TreeNodeType.ReturnToBornPosNode:
                ReturnToBornPosNode resRangeNode = new ReturnToBornPosNode();
                resRangeNode.titlename = "ReturnToBornPosNode" + index;
                resRangeNode.nodetype = nodeType;
                return resRangeNode;
            case Node.TreeNodeType.SetCoverPositionNode:
                SetCoverPositionNode resSetCoverPositionNode = new SetCoverPositionNode();
                resSetCoverPositionNode.titlename = "SetCoverPositionNode" + index;
                resSetCoverPositionNode.nodetype = nodeType;
                return resSetCoverPositionNode;
            case Node.TreeNodeType.ShootingNode:
                ShootingNode resShootingNode = new ShootingNode();
                resShootingNode.titlename = "ShootingNode" + index;
                resShootingNode.nodetype = nodeType;
                return resShootingNode;
            case Node.TreeNodeType.DoPatrolNode:
                DoPatrolNode resDoPatrolNode = new DoPatrolNode();
                resDoPatrolNode.titlename = "DoPatrolNode" + index;
                resDoPatrolNode.nodetype = nodeType;
                return resDoPatrolNode;
            case Node.TreeNodeType.GetPatrolNode:
                GetPatrolNode resGetPatrolNode = new GetPatrolNode();
                resGetPatrolNode.titlename = "GetPatrolNode" + index;
                resGetPatrolNode.nodetype = nodeType;
                return resGetPatrolNode;
            case Node.TreeNodeType.UnderAttackNode:
                UnderAttackNode resUnderAttackNode = new UnderAttackNode();
                resUnderAttackNode.titlename = "UnderAttackNode" + index;
                resUnderAttackNode.nodetype = nodeType;
                return resUnderAttackNode;
            case Node.TreeNodeType.EnemyViewNode:
                EnemyViewNode resEnemyViewNode = new EnemyViewNode();
                resEnemyViewNode.titlename = "EnemyViewNode" + index;
                resEnemyViewNode.nodetype = nodeType;
                return resEnemyViewNode;
                break;
            case Node.TreeNodeType.IfNode:
                IfNode resIfNode = new IfNode();
                resIfNode.titlename = "IfNode" + index;
                resIfNode.nodetype = nodeType;
                return resIfNode;
                break;
            case Node.TreeNodeType.ForEachNode:
                ForEachNode resForEachNode = new ForEachNode();
                resForEachNode.titlename = "ForEachNode" + index;
                resForEachNode.nodetype = nodeType;
                return resForEachNode;
                break;
            case Node.TreeNodeType.AlwaysTrueNode:
                AlwaysTrueNode resAlwaysTrueNode = new AlwaysTrueNode();
                resAlwaysTrueNode.titlename = "AlwaysTrueNode" + index;
                resAlwaysTrueNode.nodetype = nodeType;
                return resAlwaysTrueNode;
                break;
            case Node.TreeNodeType.DecideToChaseNode:
                DecideToChaseNode resDecideToChaseNode = new DecideToChaseNode();
                resDecideToChaseNode.titlename = "DecideToChaseNode" + index;
                resDecideToChaseNode.nodetype = nodeType;
                return resDecideToChaseNode;
                break;
            case Node.TreeNodeType.LoseViewNode:
                LoseViewNode resLoseViewNode = new LoseViewNode();
                resLoseViewNode.titlename = "LoseViewNode" + index;
                resLoseViewNode.nodetype = nodeType;
                return resLoseViewNode;
                break;
            case Node.TreeNodeType.AttAckPlayerNode:
                AttAckPlayerNode resAttAckPlayerNode = new AttAckPlayerNode();
                resAttAckPlayerNode.titlename = "AttAckPlayerNode" + index;
                resAttAckPlayerNode.nodetype = nodeType;
                return resAttAckPlayerNode;
                break;
            case Node.TreeNodeType.BossHealthUnderLimitNode:
                BossHealthUnderLimitNode resBossHealthUnderLimitNode = new BossHealthUnderLimitNode();
                resBossHealthUnderLimitNode.titlename = "BossHealthUnderLimitNode" + index;
                resBossHealthUnderLimitNode.nodetype = nodeType;
                return resBossHealthUnderLimitNode;
                break;
            case Node.TreeNodeType.BossDistanceUnderLimitNode:
                BossDistanceUnderLimitNode resBossDistanceUnderLimitNode = new BossDistanceUnderLimitNode();
                resBossDistanceUnderLimitNode.titlename = "BossDistanceUnderLimitNode" + index;
                resBossDistanceUnderLimitNode.nodetype = nodeType;
                return resBossDistanceUnderLimitNode;
                break;
            case Node.TreeNodeType.BossTurnNode:
                BossTurnNode resBossTurnNode = new BossTurnNode();
                resBossTurnNode.titlename = "BossTurnNode" + index;
                resBossTurnNode.nodetype = nodeType;
                return resBossTurnNode;
                break;
            case Node.TreeNodeType.BossGrenateNavPathNode:
                BossGrenateNavPathNode resBossGrenateNavPathNode = new BossGrenateNavPathNode();
                resBossGrenateNavPathNode.titlename = "BossGrenateNavPathNode" + index;
                resBossGrenateNavPathNode.nodetype = nodeType;
                return resBossGrenateNavPathNode;
                break;
            case Node.TreeNodeType.BossAttackNode:
                BossAttackNode resBossAttackNode = new BossAttackNode();
                resBossAttackNode.titlename = "BossAttackNode" + index;
                resBossAttackNode.nodetype = nodeType;
                return resBossAttackNode;
                break;
            case Node.TreeNodeType.BossChasePlayerNode:
                BossChasePlayerNode resBossChasePlayerNode = new BossChasePlayerNode();
                resBossChasePlayerNode.titlename = "BossChasePlayerNode" + index;
                resBossChasePlayerNode.nodetype = nodeType;
                return resBossChasePlayerNode;
                break;
            case Node.TreeNodeType.BossJumpNode:
                BossJumpNode resBossJumpNode = new BossJumpNode();
                resBossJumpNode.titlename = "BossJumpNode" + index;
                resBossJumpNode.nodetype = nodeType;
                return resBossJumpNode;
                break;
            case Node.TreeNodeType.BossGenreateTinyEnemyNode:
                BossGenreateTinyEnemyNode resBossGenreateTinyEnemyNode = new BossGenreateTinyEnemyNode();
                resBossGenreateTinyEnemyNode.titlename = "BossGenreateTinyEnemyNode" + index;
                resBossGenreateTinyEnemyNode.nodetype = nodeType;
                return resBossGenreateTinyEnemyNode;
                break;
            case Node.TreeNodeType.WaitCoolenNode:
                WaitCoolenNode resWaitCoolenNode = new WaitCoolenNode();
                resWaitCoolenNode.titlename = "WaitCoolenNode" + index;
                resWaitCoolenNode.nodetype = nodeType;
                return resWaitCoolenNode;
                break;
            case Node.TreeNodeType.BossShouldJumpNode:
                BossShouldJumpNode resBossShouldJumpNode = new BossShouldJumpNode();
                resBossShouldJumpNode.titlename = "BossShouldJumpNode" + index;
                resBossShouldJumpNode.nodetype = nodeType;
                return resBossShouldJumpNode;
                break;
            case Node.TreeNodeType.BossAimNode:
                BossAimNode resBossAimNode = new BossAimNode();
                resBossAimNode.titlename = "BossAimNode" + index;
                resBossAimNode.nodetype = nodeType;
                return resBossAimNode;
                break;
            default:
                Debug.Log("No Node Created");
                index--;
                return null;
        }
    }

    public void DeleteNode(Node delNode)
    {
        Debug.Log("delete node succeed"+delNode.titlename);
        nodesData.Remove(delNode);
    }
}
