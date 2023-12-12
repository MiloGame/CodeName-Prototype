using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PlayerHealthManager : MonoBehaviour
{
    private BindableTProperty<float> remainHealthBindableTProperty;
    public Rigidbody[] Rigidbodies;
    public Animator playerAnimator;
    public float RemainHealth;
    private Vector3 BornPos;

    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        Rigidbodies = GetComponentsInChildren<Rigidbody>();

        EventBusManager.Instance.NonParamScribe(EventBusManager.EventType.Restart,OnRestart);
        BornPos = transform.position; 
        DeActivateRigDoll();
        remainHealthBindableTProperty = new BindableTProperty<float>();
    }

    private void OnRestart(object sender, EventArgs e)
    {
        RemainHealth = 100;
        transform.position = BornPos;
    }

    public void ActivateRigDoll()
    {
        //var rigBuilder = transform.GetComponent<RigBuilder>();
        //rigBuilder.layers[0].active = false;
        //Debug.Log("rigbuilder" + rigBuilder+"layer status"+ rigBuilder.layers[0].active);

        playerAnimator.enabled = false;
        foreach (var rigidbody1 in Rigidbodies)
        {
            rigidbody1.isKinematic = false;
        }
    }
    public void DeActivateRigDoll()
    {
        playerAnimator.enabled = true;
        foreach (var rigidbody1 in Rigidbodies)
        {
            rigidbody1.isKinematic = true;
        }
    }

    void Update()
    {
        remainHealthBindableTProperty.SetValue(RemainHealth, EventBusManager.EventType.PlayerHealthChange);
        if (RemainHealth <= 0)
        {
            ActivateRigDoll();
            EventBusManager.Instance.NonParamPublish(EventBusManager.EventType.PlayerDead);
        }
    }
    public void OnHit(float atkDamageval)
    {
        RemainHealth -= atkDamageval;

        
        RemainHealth = Mathf.Clamp(RemainHealth, 0, 100);
    }
}
