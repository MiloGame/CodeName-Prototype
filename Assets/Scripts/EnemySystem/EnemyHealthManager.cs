using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks.Triggers;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class EnemyHealthManager : MonoBehaviour
{
    public Rigidbody[] Rigidbodies;

    public Animator EnemyAnimator;
    //public Image healthbar;
    public EnemyHealthUIManger HealthUiManger;
    public float currentHealth;
    public float MAXHealth;
    public float blinkIntensity;
    public float blinkDuration;
    public float blinkTimer;
    public float healthbarspeed;
    public SkinnedMeshRenderer skinnedMesh;
    public bool isdead =false;
    public float persent;

    public void ActivateRigDoll()
    {
        EnemyAnimator.enabled = false;
        foreach (var rigidbody1 in Rigidbodies)
        {
            rigidbody1.isKinematic = false;
        }
    }

    public void addForce(Vector3 forceVector3)
    {
        var rigi = EnemyAnimator.GetBoneTransform(HumanBodyBones.Hips).GetComponent<Rigidbody>();
        rigi.AddForce(forceVector3,ForceMode.VelocityChange);//不考虑角色质量
    }


    public void TakeDamage(float amount)
    {
        //HealthUiManger.GetHit = true;
        if (!isdead)
        {
             currentHealth -= amount;
        }
       
        if (currentHealth <= 0.0f)
        {
            Die();
        }

        blinkTimer = blinkDuration;

    }
    public void Die()
    {
        isdead = true;
        ActivateRigDoll();
    }
    public void DeActivateRigDoll()
    {
        EnemyAnimator.enabled = true;
        foreach (var rigidbody1 in Rigidbodies)
        {
            rigidbody1.isKinematic = true;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        EnemyAnimator.applyRootMotion = false;
        skinnedMesh = GetComponentInChildren<SkinnedMeshRenderer>();
        MAXHealth = 100;
        currentHealth = MAXHealth;
        Rigidbodies = GetComponentsInChildren<Rigidbody>();
        foreach (var rigidbody1 in Rigidbodies)
        {
            var hitBox = rigidbody1.gameObject.AddComponent<HitBox>();
            hitBox.Health = this;
        }
    }

    // Update is called once per frame
    //void Update()
    //{
    //    if (blinkTimer >=0)
    //    {
    //          blinkTimer -= Time.deltaTime;
    //    }
      
    //    float lerp = Mathf.Clamp01(blinkTimer / blinkDuration);
    //    float intensity = (lerp * blinkIntensity)+1.0f;
    //    foreach (var skinnedMeshMaterial in skinnedMesh.materials)
    //    {
    //        skinnedMeshMaterial.color = Color.white * intensity;
    //    }

    //    persent =Mathf.Lerp(persent,-(1 - currentHealth / MAXHealth) * 100,Time.deltaTime* healthbarspeed);

    //    if (persent > -100)
    //    {
    //        //VisualElement healthbar = HealthUiManger.GetHealthbar();

    //        healthbar.style.translate = new Translate(Length.Percent(persent), 0, 0);
    //        healthbar.style.backgroundColor = Color.Lerp(Color.red, Color.green, (currentHealth / MAXHealth));

    //    }
    //    else
    //    {
    //        HealthUiManger.DisableHealthbar();
    //    }
    //}

    private void ColorChange()
    {
        Color healthcolor = Color.Lerp(Color.red, Color.green, (currentHealth / MAXHealth));
       
    }
}
