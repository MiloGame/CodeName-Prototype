using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ParticleHitEnemy : MonoBehaviour
{
    public ParticleSystem part;
    public List<ParticleCollisionEvent> collisionEvents;
    private NavMeshAgent preAgent;
    private NavMeshAgent prebossAgent=null;
    public float damage { get; set; }
    void Start()
    {
        preAgent = null;
        part = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
    }
    
    void OnParticleCollision(GameObject other)
    {
        int numCollisionEvents = part.GetCollisionEvents(other, collisionEvents);
        //Debug.Log("OnParticleCollision trigged" + numCollisionEvents);
        //Rigidbody rb = other.GetComponent<Rigidbody>();
        //var rb = other.GetComponent<HitBox>();
        int i = 0;

        while (i < numCollisionEvents)
        {
            var rb = other.GetComponent<NormalAiHitBox>();
            if (rb!=null)
            {
                var agent = other.GetComponentInParent<NavMeshAgent>();
                if (preAgent==null || preAgent!=agent)
                {
                    preAgent = agent;
                    //Debug.Log(rb.transform.name);
                    rb.OnHit(damage);
                }

                
            }
            var rb1 = other.GetComponent<BossHitBox>();

            if (rb1 != null)
            {
                var agent = other.GetComponentInParent<NavMeshAgent>();
                if (prebossAgent == null || preAgent != agent)
                {
                    prebossAgent = agent;
                    //Debug.Log(rb.transform.name);
                    rb1.OnHit(damage);
                }
                //Debug.Log(rb.transform.name);
            }
            i++;
        }

        prebossAgent = null;
        preAgent = null;
    }
}
