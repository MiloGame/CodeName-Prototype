using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.BehaviorTree;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavAni : MonoBehaviour
{
    public Animator EnemyAnimator;
    public BehaviorTreeManger BmManger;
    public NavMeshAgent navMeshAgent;
    private RaycastHit infoHit;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (BmManger.TreeBlackBoard.Ishit )
        {
            EnemyAnimator.SetBool("IsMove", false);
            EnemyAnimator.SetBool("IsAttack", false);
            EnemyAnimator.SetBool("IsHit", true);
        }
        else
        {
            EnemyAnimator.SetBool("IsHit", false);

            if (navMeshAgent.velocity != Vector3.zero)
            {
                EnemyAnimator.SetBool("IsMove", true);
                EnemyAnimator.SetBool("IsAttack", false);
            }
            else
            {
                EnemyAnimator.SetBool("IsMove", false);
            }
        }
       


    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawWireSphere(transform.position, 10);

    //    Gizmos.color = Color.red;
    //    Collider[] hitColliders = Physics.OverlapSphere(transform.position, 10);
    //    foreach (Collider collider in hitColliders)
    //    {
    //        Gizmos.DrawSphere(collider.transform.position, 0.1f);
    //    }
    //}
}
