using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class BossGenreateTinyEnemyNode : ActionNode, BehaviorTreeFunInterface
{
    private Vector3 GenerateFellowPos;

    public void OnStart()
    {
        GenerateFellowPos = TreeBlackBoard.PlayerTransform.position + TreeBlackBoard.PlayerTransform.forward * 3f;
    }

    public void OnStop()
    {
    }

    public AllState OnUpdate()
    {
        for (int i = 0; i < 2; i++)
        {
            var randompos = Random.insideUnitCircle * Random.Range(0, 3);

            TreeBlackBoard.BTManager.BossCreateFellowEnemy(GenerateFellowPos+new Vector3(randompos.x,1f,randompos.y));
        }
        return AllState.Success;
    }

    public string GetNodeInfoAsString()
    {
        string info = $"Title: {titlename}\nWidth: {width}\nHeight: {height}\nPositionX: {positionx}\nPositionY: {positiony}\n" +
                      $"NodeType: {nodetype}\nIsExcuted:{IsExcuted}\n";
        return info;
    }
}
