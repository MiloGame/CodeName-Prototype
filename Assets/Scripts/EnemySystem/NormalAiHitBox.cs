using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.BehaviorTree;
using UnityEngine;

public class NormalAiHitBox : MonoBehaviour
{
    public BehaviorTreeManger BmManger;
    public void OnHit(float amount)
    {
        Debug.Log("OnHit");
        BmManger.TakeDamage(amount, transform.name);
    }
}
