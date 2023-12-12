using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour,TakeDamageInterface
{
    public EnemyHealthManager Health;

    public Vector3 ForceVector3;
    // Start is called before the first frame update
    public void OnHit(float amount)
    {
        Health.TakeDamage(amount);
        Health.addForce(ForceVector3);
    } 

}
