using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.BehaviorTree;
using UnityEngine;

public class BossHitBox : MonoBehaviour,TakeDamageInterface
{
    public BehaviorTreeManger BmManger;
    public void OnHit(float amount)
    {
        Debug.Log("OnHit");
        BmManger.BossTakeDamage(amount,transform.name);
    }

}
