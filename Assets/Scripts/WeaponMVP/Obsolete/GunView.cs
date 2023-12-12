using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class GunView : FireBaseView , EffectFunInterface 
{
   // [SerializeField]private List<ParticleSystem> _particleinstance;

    [SerializeField]private List<ParticleSystem> _muzzleList;
    [SerializeField]private ParticleSystem _hiteffect;
    public bool iseuqip;
    public Transform[] weaponparent;
    public Animator WeaponAnimator;
    [SerializeField] private Dictionary<GunType, GameObject> WeaGameObject;
    public ShowMessage SmMessage;

    public delegate void OnHit(RaycastHit hitinfoHit);

    public OnHit onHit;
    public void ShowEffect()
    {

            foreach (var fireeffect in _muzzleList)
            {
                fireeffect.transform.forward = GunShotPosGameObject.transform.forward;
                fireeffect.Emit(1);
            }
            //foreach (var par in _particleinstance)
            //{
            //    par.Emit(1);
            //}
        
        
    }

    public GameObject CreateBulletInstance()
    {
        return Instantiate(BulletPrefab[0], GunShotPosGameObject.transform.position,Quaternion.identity);
    }
    public void PlayAnimation()
    {
        
    }

    public void Init()
    {
        onHit += PlaceHitEffect;
        WeaGameObject = new Dictionary<GunType, GameObject>();
        AniClipParamName = new Dictionary<GunType, string>();
        AniClipParamName[GunType.RifleGun] = "rifleequip";
        AniClipParamName[GunType.Pistol] = "pistolequip";
        foreach (var muzzleParticleSystem in MuzzleFlash)
        {
            var muzzle = Instantiate(muzzleParticleSystem, GunShotPosGameObject.transform);
            _muzzleList.Add(muzzle);
        }
        WeaponAnimator.SetBool("rifleequip", false);
        WeaponAnimator.SetBool("unarmed", false);
        //foreach (var particle in HitEffect)
        //{
        //    var particleGameObjb = Instantiate(particle, GunShotPosGameObject.transform.position, Quaternion.identity);


        //    _particleinstance.Add(particleGameObjb);
        //}
        _hiteffect = Instantiate(HitEffit, GunShotPosGameObject.transform.position, Quaternion.identity);


    }

    public void UpdateState()
    {
        
    }

    public void SwitchGun(GunType gunType)
    {


        foreach (var keyval in AniClipParamName.Keys)
        {
            if (keyval!= gunType)
            {
                WeaponAnimator.SetBool(AniClipParamName[keyval], false);
            }
        }

        if (!WeaponAnimator.GetBool(AniClipParamName[gunType]))
        {
            WeaponAnimator.SetTrigger("returnidle");
        }
        
        if (!WeaGameObject.ContainsKey(gunType))
        {
            WeaGameObject[gunType] = Instantiate(GunsPrefabs[(int)gunType], weaponparent[(int)gunType]);
        }

        
       // SwitchGun(GunType.RifleGun);
        WeaponAnimator.SetBool(AniClipParamName[gunType], true);

    }

    public void PlaceHitEffect(RaycastHit hitinfoHit)
    {
        _hiteffect.transform.position = hitinfoHit.point;
        _hiteffect.transform.forward = hitinfoHit.normal;
        _hiteffect.Emit(1);
        //foreach (var par in _particleinstance)
        //{
        //    par.transform.position = hitinfoHit.point;
        //    par.transform.forward = hitinfoHit.normal;
        //    par.Emit(1);
        //}
    }
    public void UnEquip()
    {
        WeaponAnimator.SetBool(AniClipParamName[GunType.RifleGun], false);
        WeaponAnimator.SetBool(AniClipParamName[GunType.Pistol], false);
    }

    public void UpdateHud(string messageinfo)
    {
        SmMessage.showMessage(messageinfo);
    }

    void OnAnimatorMove()
    {

    }
}
