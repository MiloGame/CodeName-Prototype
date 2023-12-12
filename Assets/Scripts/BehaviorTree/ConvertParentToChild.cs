
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class ConvertParentToChild : JsonConverter
{
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        JObject jsonObject = JObject.Load(reader);
        //Debug.Log("jsonObject data" + jsonObject);
        Node.TreeNodeType nodetype = jsonObject["nodetype"].ToObject<Node.TreeNodeType>();
        //Debug.Log("nodetype" + nodetype);
        switch (nodetype)
        {
            case Node.TreeNodeType.DebugLogNode:
                return jsonObject.ToObject<DebugLogNode>();
                break;
            case Node.TreeNodeType.WaitNode:
                return jsonObject.ToObject<WaitNode>();
                break;
            case Node.TreeNodeType.RepeatedNode:
                return jsonObject.ToObject<RepeatedNode>();
                break;
            case Node.TreeNodeType.SecquenceNode:
                return jsonObject.ToObject<SecquenceNode>();
                break;
            case Node.TreeNodeType.RootNode:
                return jsonObject.ToObject<RootNode>();
                break;
            case Node.TreeNodeType.Inverter:
                return jsonObject.ToObject<Inverter>();
                break;
            case Node.TreeNodeType.Selector:
                return jsonObject.ToObject<Selector>();
                break;
            case Node.TreeNodeType.ChaseNode:
                return jsonObject.ToObject<ChaseNode>();
                break;
            case Node.TreeNodeType.EnemyHealthNode:
                return jsonObject.ToObject<EnemyHealthNode>();
                break;
            case Node.TreeNodeType.GotoPosistionNode:
                return jsonObject.ToObject<GotoPosistionNode>();
                break;
            case Node.TreeNodeType.IsCoverAvaliableNode:
                return jsonObject.ToObject<IsCoverAvaliableNode>();
                break;
            case Node.TreeNodeType.IsCoveredNode:
                return jsonObject.ToObject<IsCoveredNode>();
                break;
            case Node.TreeNodeType.ReturnToBornPosNode:
                return jsonObject.ToObject<ReturnToBornPosNode>();
                break;
            case Node.TreeNodeType.SetCoverPositionNode:
                return jsonObject.ToObject<SetCoverPositionNode>();
                break;
            case Node.TreeNodeType.ShootingNode:
                return jsonObject.ToObject<ShootingNode>();
                break;
            case Node.TreeNodeType.DoPatrolNode:
                return jsonObject.ToObject<DoPatrolNode>();
                break;
            case Node.TreeNodeType.GetPatrolNode:
                return jsonObject.ToObject<GetPatrolNode>();
                break;
            case Node.TreeNodeType.UnderAttackNode:
                return jsonObject.ToObject<UnderAttackNode>();
                break;
            case Node.TreeNodeType.EnemyViewNode:
                return jsonObject.ToObject<EnemyViewNode>();
                break;
            case Node.TreeNodeType.IfNode:
                return jsonObject.ToObject<IfNode>();
                break;
            case Node.TreeNodeType.ForEachNode:
                return jsonObject.ToObject<ForEachNode>();
                break;
            case Node.TreeNodeType.AlwaysTrueNode:
                return jsonObject.ToObject<AlwaysTrueNode>();
                break;
            case Node.TreeNodeType.DecideToChaseNode:
                return jsonObject.ToObject<DecideToChaseNode>();
                break;
            case Node.TreeNodeType.LoseViewNode:
                return jsonObject.ToObject<LoseViewNode>();
                break;
            case Node.TreeNodeType.AttAckPlayerNode:
                return jsonObject.ToObject<AttAckPlayerNode>();
                break;
            case Node.TreeNodeType.BossHealthUnderLimitNode:
                return jsonObject.ToObject<BossHealthUnderLimitNode>();
                break;
            case Node.TreeNodeType.BossDistanceUnderLimitNode:
                return jsonObject.ToObject<BossDistanceUnderLimitNode>();
                break;
            case Node.TreeNodeType.BossTurnNode:
                return jsonObject.ToObject<BossTurnNode>();
                break;
            case Node.TreeNodeType.BossGrenateNavPathNode:
                return jsonObject.ToObject<BossGrenateNavPathNode>();
                break;
            case Node.TreeNodeType.BossAttackNode:
                return jsonObject.ToObject<BossAttackNode>();
                break;
            case Node.TreeNodeType.BossChasePlayerNode:
                return jsonObject.ToObject<BossChasePlayerNode>();
                break;
            case Node.TreeNodeType.BossJumpNode:
                return jsonObject.ToObject<BossJumpNode>();
                break;
            case Node.TreeNodeType.BossGenreateTinyEnemyNode:
                return jsonObject.ToObject<BossGenreateTinyEnemyNode>();
                break;
            case Node.TreeNodeType.WaitCoolenNode:
                return jsonObject.ToObject<WaitCoolenNode>();
                break;
            case Node.TreeNodeType.BossShouldJumpNode:
                return jsonObject.ToObject<BossShouldJumpNode>();
                break;
            case Node.TreeNodeType.BossAimNode:
                return jsonObject.ToObject<BossAimNode>();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public override bool CanConvert(Type objectType)
    {
        return typeof(Node).IsAssignableFrom(objectType);
    }
}
