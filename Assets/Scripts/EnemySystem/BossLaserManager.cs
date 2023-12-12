using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks.Triggers;
using UnityEngine;

public class BossLaserManager : MonoBehaviour
{
    public GameObject HitEffect;
    public GameObject FlashEffect;
    public GameObject AimGameObject;
    public bool useLaserRotation = false;

    public float MaxLength;
    private LineRenderer Laser;

    public float MainTextureLength = 1f;
    public float NoiseTextureLength = 1f;
    private Vector4 Length = new Vector4(1, 1, 1, 1);

    //private bool LaserSaver = false;
    //private bool UpdateSaver = false;
    public bool EnableEmit;
    public bool EnableLazer;

    private ParticleSystem[] Effects;
    private ParticleSystem[] Hit;
    private ParticleSystem[] Flash;
    private RaycastHit hitinfo;
    private bool CanDamage=true;

    void Start()
    {
        //Get LineRender and ParticleSystem components from current prefab;  
        Laser = GetComponent<LineRenderer>();
        //Effects = GetComponentsInChildren<ParticleSystem>();
        Hit = HitEffect.GetComponentsInChildren<ParticleSystem>();
        Flash = FlashEffect.GetComponentsInChildren<ParticleSystem>();
        DisableHit();
        DisableEmiting();
    }

    void DisableHit()
    {
        foreach (var particle in Hit)
        {
            if (particle.isPlaying)
                particle.Stop();
        }
    }    
    void DisableEmiting()
    {
        foreach (var particle in Flash)
        {
            if (particle.isPlaying)
                particle.Stop();
        }
    }     
    void EnableHit()
    {
        foreach (var particle in Hit)
        {
            if (!particle.isPlaying)
                particle.Play();
        }
    }    
    void EnableEmiting()
    {
        Laser.enabled = true;
        foreach (var particle in Flash)
        {
            if (!particle.isPlaying)
                particle.Play();
        }
    }    

    void UpdateEmit()
    {
        Laser.material.SetTextureScale("_MainTex", new Vector2(Length[0], Length[1]));
        Laser.material.SetTextureScale("_Noise", new Vector2(Length[2], Length[3]));
        Laser.SetPosition(0, transform.position);
        Laser.SetPosition(1, AimGameObject.transform.position);
        HitEffect.transform.position = AimGameObject.transform.position ;
        //Texture tiling
        Length[0] = MainTextureLength * (Vector3.Distance(transform.position, AimGameObject.transform.position));
        Length[2] = NoiseTextureLength * (Vector3.Distance(transform.position, AimGameObject.transform.position));
    }
    void Update()
    {
        if (EnableEmit)
        {
            EnableEmiting();
        }
        else
        {
            DisableEmiting();
        }

        if (EnableLazer)
        {
            Ray ray = new Ray(transform.position, AimGameObject.transform.position - transform.position);
            Debug.DrawRay(ray.origin,ray.direction);
            if (Physics.Raycast(ray,out hitinfo,100,(1<<8)))
            {
                PlayerHealthManager pcHealthManager = hitinfo.transform.GetComponent<PlayerHealthManager>();
                if (pcHealthManager != null && CanDamage)
                {
                    CanDamage = false;
                    StartCoroutine(CoolenAttack());
                    pcHealthManager.OnHit(5f);
                    
                }
            }
            EnableHit();
            UpdateEmit();
        }
        else
        {
            Laser.enabled = false;
            DisableHit();
        }

        ////To set LineRender position
        //if (Laser != null && UpdateSaver == false)
        //{
        //    Laser.SetPosition(0, transform.position);
        //    RaycastHit hit; 
        //    if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, MaxLength))
        //    {
        //        Laser.SetPosition(1, hit.point);

        //        HitEffect.transform.position = hit.point + hit.normal * HitOffset;
        //        if (useLaserRotation)
        //            HitEffect.transform.rotation = transform.rotation;
        //        else
        //            HitEffect.transform.LookAt(hit.point + hit.normal);

        //        foreach (var AllPs in Effects)
        //        {
        //            if (!AllPs.isPlaying) AllPs.Play();
        //        }
        //        //Texture tiling
        //        Length[0] = MainTextureLength * (Vector3.Distance(transform.position, hit.point));
        //        Length[2] = NoiseTextureLength * (Vector3.Distance(transform.position, hit.point));

        //    }
        //    else
        //    {
        //        //End laser position if doesn't collide with object
        //        var EndPos = transform.position + transform.forward * MaxLength;
        //        Laser.SetPosition(1, EndPos);
        //        HitEffect.transform.position = EndPos;
        //        foreach (var AllPs in Hit)
        //        {
        //            if (AllPs.isPlaying) AllPs.Stop();
        //        }
        //        //Texture tiling
        //        Length[0] = MainTextureLength * (Vector3.Distance(transform.position, EndPos));
        //        Length[2] = NoiseTextureLength * (Vector3.Distance(transform.position, EndPos));
        //        //LaserSpeed[0] = (LaserStartSpeed[0] * 4) / (Vector3.Distance(transform.position, EndPos)); {DISABLED AFTER UPDATE}
        //        //LaserSpeed[2] = (LaserStartSpeed[2] * 4) / (Vector3.Distance(transform.position, EndPos)); {DISABLED AFTER UPDATE}
        //    }
        //    //Insurance against the appearance of a laser in the center of coordinates!
        //    if (Laser.enabled == false && LaserSaver == false)
        //    {
        //        LaserSaver = true;
        //        Laser.enabled = true;
        //    }
        //}
    }

    private IEnumerator CoolenAttack()
    {
        yield return new WaitForSeconds(2);
        CanDamage = true;
    }

    //public void DisablePrepare()
    //{
    //    if (Laser != null)
    //    {
    //        Laser.enabled = false;
    //    }
    //    UpdateSaver = true;
    //    //Effects can = null in multiply shooting
    //    if (Effects != null)
    //    {
    //        foreach (var AllPs in Effects)
    //        {
    //            if (AllPs.isPlaying) AllPs.Stop();
    //        }
    //    }
    //}
}
