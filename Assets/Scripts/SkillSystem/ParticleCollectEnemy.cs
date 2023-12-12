using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ParticleCollectEnemy : MonoBehaviour
{
    public ParticleSystem part;
    public List<ParticleCollisionEvent> collisionEvents;
    public List<NavMeshAgent> hitEnemys;
    private float lifttime;
    public int enemylayer = (1 << 10);
    void OnParticleCollision(GameObject other)
    {
        
        int numCollisionEvents = part.GetCollisionEvents(other, collisionEvents);
        //Debug.Log("OnParticleCollision trigged" + numCollisionEvents);
        //Rigidbody rb = other.GetComponent<Rigidbody>();
        int i = 0;
        
        var hb = other;
        while (i < numCollisionEvents)
        {
            if (hb.GetComponentInParent<NavMeshAgent>())
            {

                hitEnemys.Add(hb.GetComponentInParent<NavMeshAgent>());
            }
            i++;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        part = GetComponent<ParticleSystem>();
        var collision = part.collision;
        collision.bounce = 0f;
        collisionEvents = new List<ParticleCollisionEvent>();
        lifttime = part.main.duration;
    }

    // Update is called once per frame
    void Update()
    {
        lifttime -= Time.deltaTime;
        //if (lifttime <=0)
        //{
        //    Debug.Log("particle dead");
        //}
    }
}
