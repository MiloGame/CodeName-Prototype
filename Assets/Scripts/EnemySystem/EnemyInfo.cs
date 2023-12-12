using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;

[Serializable]
public class EnemyInfo
{
    //public float startHealth;
    //public float currentHealth;
    //public float preHealth;
    //public float lowHealthTheshold;
    //public float healingRate;
    //public float chasingRange;
    //public float shootingRange;
    [NonSerialized] public Vector3 bestCoverPos;
    [NonSerialized] public Vector3 BornPosition;
    [NonSerialized] public NavMeshAgent aiAgent;
    [NonSerialized] public Transform SelfTransform;
    [NonSerialized] public Animator EnemyAnimator;
    //private float CurrentHealth
    //{
    //    get
    //    {
    //        return currentHealth;
    //    }
    //    set
    //    {
    //        currentHealth = Mathf.Clamp(value, 0, startHealth);
    //    }
       
    //}
    // Start is called before the first frame update
    public EnemyInfo(NavMeshAgent agent, Transform selftran,Vector3 bornPosition,Animator enemyAnimator)
    {
        //startHealth = 100f;
        //CurrentHealth = startHealth;
        //preHealth = CurrentHealth;
        //lowHealthTheshold = 10f;
        //healingRate = 2f;
        aiAgent = agent;
        SelfTransform = selftran;
        BornPosition = bornPosition;
        EnemyAnimator=enemyAnimator;
    }

    // Update is called once per frame
    //void Healing()
    //{
    //    if (CurrentHealth < lowHealthTheshold)
    //    {
    //        CurrentHealth += Time.deltaTime * healingRate;
    //    }
    //}

    //public float GetCurrentHealth()
    //{
    //    return CurrentHealth;
    //}


    public void SetHidePosistion(Vector3 coverpoint)
    {
        bestCoverPos = coverpoint;
    }

    public void SetFire()
    {
       Debug.Log("is fired");
    }
}
