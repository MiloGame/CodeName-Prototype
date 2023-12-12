using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MVP_GunView : MonoBehaviour
{
    public PrefabManger PbManger;
    public Animator WeaponAnimator;
    public GameObject Muzzle;

    public GameObject HitImpactobj;
    public ParticleSystem HitImpact;

    public GameObject PistolBullet;
    public GameObject RifleBullet;
    public ParticleSystem[] Muzzles;
    public GameObject RifleFirePos;
    public GameObject PistolFirePos;
    public GameObject Pistol;
    public GameObject Rifle;
    public void Start()
    {
        Muzzle = Instantiate(PbManger.Muzzle, this.transform.position, Quaternion.identity);
        
        HitImpactobj = Instantiate(PbManger.HitImpact, this.transform.position, Quaternion.identity);
        HitImpact = HitImpactobj.GetComponent<ParticleSystem>();
        Muzzles = Muzzle.GetComponentsInChildren<ParticleSystem>();
        RifleBullet = PbManger.RifleBullet;
        PistolBullet = PbManger.PistolBullet;
    }
}
