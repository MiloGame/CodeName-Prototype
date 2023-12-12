

using System.Collections.Generic;
using UnityEngine;

public class GunControler : FireBaseControler , GunFunInterface
{
    private float _currenttime;
    private float fireCoolenTimer = 0.05f;
    private bool issingleshoot = true;
    private BulletData bulletData;
    private BulletPool bp;
    private GameObject gb;
    private BulletView gbBulletView;
    private bool isautoaim = false;
    public delegate void OnDestory(GameObject todestruct);

    public OnDestory todestory;
    public GunControler(GunView gunView1 , GunModel gunModel1)
    {
        gunView = gunView1;
        gunModel = gunModel1;
    }
    public void Reload()
    {
        gunModel.CurrentBullets = 0;
    }

    public void UnEquip()
    {

       
            gunView.UnEquip();

            gunView.iseuqip = false;
        
    }

    public void FreshEvent()
    {
        IsAutoCast();
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchGun(FireBaseView.GunType.RifleGun);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchGun(FireBaseView.GunType.Pistol);
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            UnEquip();
        }
        
        if (gunModel.CurrentBullets < gunModel.MaxBullets)
        {
            ChangeShotMode();
            
        }
        else
        {
            gunView.UpdateHud("Reload! Reload!");
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
            gunView.UpdateHud("Remaining Bullets"+(gunModel.MaxBullets-gunModel.CurrentBullets));
        }
        
    }

    public void SwitchGun(FireBaseView.GunType gunType)
    {
        
        gunView.SwitchGun(gunType);
    }

    public void ChangeShotMode()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            issingleshoot = !issingleshoot;
            if (issingleshoot)
                gunView.UpdateHud("Switch to Single Fire");
            else
                gunView.UpdateHud("Switch to Continue Fire");
        }

        if (issingleshoot)
        {
            
            if (Input.GetButtonDown("Fire1"))
            {
                SingleFire();
                gunView.ShowEffect();
            }
        }
        else
        {
           
            if (Input.GetButton("Fire1"))
            {
                ContinueFire();
            }
        }
    }

    public void IsAutoCast()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            isautoaim = !isautoaim;
            if (isautoaim)
                gunView.UpdateHud("Auto tracking bullets Activate");
            else
                gunView.UpdateHud("Normal bullets Activate");
        }
    }
    //实现单发和连射
    public void ContinueFire()
    {

        //if (Input.GetMouseButtonDown(0))
         gunView.UpdateHud("Switch TO Continue Fire");
            _currenttime += Time.deltaTime;
            if (_currenttime > fireCoolenTimer)
            {
                SingleFire();
                gunView.ShowEffect();
                _currenttime = 0f;
                
            }
    }

    public void CreateBullet()
    {
        gb = bp.Create();
        if (gb==null)
        {
            gb = gunView.CreateBulletInstance();
            gb.AddComponent<BulletView>();
            gbBulletView = gb.GetComponent<BulletView>();
            if (!isautoaim)
            {
                gbBulletView.SetBulletProp(bulletData, todestory,
                    gunView.GunShotPosGameObject.transform.position,
                    gunView.AimPosGameObject.transform.position,
                    gunView.onHit);
            }
            else
            {
                gbBulletView.SetBulletProp(bulletData, todestory,
                    gunView.GunShotPosGameObject.transform.position,
                    gunView.AimPosGameObject.transform,
                    gunView.onHit);
            }
            bp.AddtoPool(gb);
        }
        else
        {
          
            gbBulletView = gb.GetComponent<BulletView>();
            if (!isautoaim)
            {
                gbBulletView.SetBulletProp(bulletData, todestory,
                    gunView.GunShotPosGameObject.transform.position,
                    gunView.AimPosGameObject.transform.position,
                    gunView.onHit);
            }
            else
            {
                gbBulletView.SetBulletProp(bulletData, todestory,
                    gunView.GunShotPosGameObject.transform.position,
                    gunView.AimPosGameObject.transform,
                    gunView.onHit);
            }

        }
        
       

       
    }
        

    public void BulletRecollect(GameObject todestruct)
    {

        bp.Destory(todestruct);
        
    }
    public void Init()
    {
        todestory += BulletRecollect;
        gunView.onHit += MakeDamage;
        bp = new BulletPool();
        bulletData = new BulletData();
        gunModel.Init();
        gunModel.SetBulletData(bulletData);
        _currenttime = 0f;
        gunView.Init();
    }

    private void MakeDamage(RaycastHit hitinfohit)
    {
        Debug.Log("HIT NAME"+ hitinfohit.transform.name);
        var hitbox = hitinfohit.collider.GetComponent<HitBox>();
        if (hitbox)
        {
            hitbox.ForceVector3 = -hitinfohit.normal;
            hitbox.ForceVector3.y=1f;
            hitbox.OnHit(gunModel.gundamage);
        }
    }

    public void SingleFire() 
    {
        foreach (var key in gunView.AniClipParamName.Keys)
        {
                if (gunView.WeaponAnimator.GetBool(gunView.AniClipParamName[key]))
                {
                    gunView.iseuqip = true;
                 //   gunView.SwitchGun(key);
                    break;
                }
        }

            if (gunView.iseuqip == false)
            {
                SwitchGun(FireBaseView.GunType.RifleGun);
            }
            gunModel.CurrentBullets++;
            CreateBullet();
    }
}
