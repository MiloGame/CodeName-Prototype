using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.BehaviorTree;
using UnityEngine;

public class RigDollManager : MonoBehaviour
{
    public Rigidbody[] Rigidbodies;
    public BehaviorTreeManger BehaviorTreeManger;
    public Animator EnemyAnimator;
    public void ActivateRigDoll()
    {
        EnemyAnimator.enabled = false;
        foreach (var rigidbody1 in Rigidbodies)
        {
            rigidbody1.isKinematic = false;
        }
    }
    public void DeActivateRigDoll()
    {
        EnemyAnimator.enabled = true;
        foreach (var rigidbody1 in Rigidbodies)
        {
            rigidbody1.isKinematic = true;
        }
    }
    public void addForce(Vector3 forceVector3)
    {
        var rigi = EnemyAnimator.GetBoneTransform(HumanBodyBones.Hips).GetComponent<Rigidbody>();
        rigi.AddForce(forceVector3, ForceMode.VelocityChange);//不考虑角色质量
    }


    // Start is called before the first frame update
    void Start()
    {
        EnemyAnimator = GetComponent<Animator>();
        BehaviorTreeManger = GetComponent<BehaviorTreeManger>();
        Rigidbodies = GetComponentsInChildren<Rigidbody>();
        foreach (var rigidbody1 in Rigidbodies)
        {
            var hitBox = rigidbody1.gameObject.AddComponent<NormalAiHitBox>();
            hitBox.BmManger = BehaviorTreeManger;
        }

        DeActivateRigDoll();
    }

   
}
