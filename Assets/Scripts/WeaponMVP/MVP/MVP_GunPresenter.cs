using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using Random = UnityEngine.Random;

public class MVP_GunPresenter : MonoBehaviour
{
    public PlayerModel PlayerModel;
    public MVP_GunView GunView;
    public GameObject AimGameObject;
    public MVP_GunModel gunModel;
    public TimerManager TimeM;
    public event Action OnPressFire;
    public event Action OnContinueFire;
    private GameObjectPool gameObjectPool;
    public Camera MainCamera;
    public void FreshStart()
    {
        gunModel = new MVP_GunModel();
        gameObjectPool = new GameObjectPool();
    }


    void CreateBullet(MVP_GunModel.GunType gunType)
    {
        GameObject newbullet;

        switch (gunType)
        {
            case MVP_GunModel.GunType.Rifle:
                newbullet = gameObjectPool.Create(gunType);
                if (newbullet!=null)
                {
                    newbullet.transform.position = GunView.RifleFirePos.transform.position;
                    //newbullet.transform.forward =
                    //    AimGameObject.transform.position - GunView.RifleFirePos.transform.position;

                    //newbullet.SetActive(true);

                }
                else
                {
                    newbullet = Instantiate(GunView.RifleBullet, GunView.RifleFirePos.transform.position,
                            Quaternion.identity);
                    newbullet.transform.GetComponent<TrailRenderer>().enabled = false;
                    //Debug.Log(newbullet.transform.position);
                    Debug.Log("new rifle");
                    gameObjectPool.AddtoPool(newbullet,gunType);
                    
                }
                newbullet.SetActive(true);
                newbullet.GetComponent<BulletControl>().Init(gunModel.bulletLife,this
                ,GunView.RifleFirePos.transform.position,AimGameObject.transform.position,
                gunModel.speed,gunModel.gravity);

                break;
            case MVP_GunModel.GunType.Pistol:
                newbullet = gameObjectPool.Create(gunType);
                if (newbullet != null)
                {
                    newbullet.transform.position = GunView.PistolFirePos.transform.position;

                }
                else
                {
                    newbullet = Instantiate(GunView.PistolBullet, GunView.PistolFirePos.transform.position,
                        Quaternion.identity);
                    newbullet.transform.GetComponent<TrailRenderer>().enabled = false;
                    Debug.Log("new pistol");
                    gameObjectPool.AddtoPool(newbullet,gunType);

                }
                newbullet.SetActive(true);
                newbullet.GetComponent<BulletControl>().Init(gunModel.bulletLife, this
                    , GunView.PistolFirePos.transform.position, AimGameObject.transform.position,
                    gunModel.speed, gunModel.gravity);
                break;
           
        }
    }

    public void PlaceHitEffect(RaycastHit hitinfoHit)
    {
        GunView.HitImpact.transform.position = hitinfoHit.point;
        GunView.HitImpact.transform.forward = hitinfoHit.normal;
        GunView.HitImpact.Emit(1);
    }
 
    public void DestoryBullet(GameObject gbGameObject,MVP_GunModel.GunType gunType)
    {
        
        gameObjectPool.Destory(gbGameObject,gunType);
    }
    // Update is called once per frame
    public void FreshUpdate()
    {
        if (gunModel.EnableFire)
        {

            if (gunModel.currentGunType==MVP_GunModel.GunType.None)
            {
                gunModel.ChangeGunModel(MVP_GunModel.GunType.Rifle);

            }
            DetectInput();
            if (gunModel.ShortPressNum1 || (gunModel.IsArmed == false && gunModel.ShortPressFire))
            {
                SwitchGun(MVP_GunModel.GunType.Rifle);
            }
            else if (gunModel.ShortPressNum2)
            {
                SwitchGun(MVP_GunModel.GunType.Pistol);
            }
            else if (gunModel.ShortPressX)
            {
                UnEquip();
            }

            if (gunModel.ShortPressFire && gunModel.remains > 0)
            {
                SingleFire();

            }else if (gunModel.LongPressFire && gunModel.remains>0 && gunModel.CanContinuShoot)
            {
            
                if (Input.GetKey(KeyCode.Mouse0))
                {
                    OnContinueFire = ContinueFire;
                    TimeM.StartTimer(gunModel.CoolenTime, OnContinueFire, 9);
                }
            }


            if (gunModel.ShortPressR)
            {
                ReLoad();
            }
            
        }

        PlayerModel.EnableFreeLook = !gunModel.IsArmed;
    }

    void Recoil()
    {
        var currentPosition = MainCamera.transform.position;
        Vector3 rocoilPosition= new Vector3(0,  Random.Range(-0.05f, 0),0);
        var TargetPosition = currentPosition + rocoilPosition;
        Vector3 Vec = Vector3.zero;
        //MainCamera.transform.position = Vector3.SmoothDamp(currentPosition,
        //    TargetPosition, ref Vec, 1f,0.001f);
        //MainCamera.transform.position = Vector3.SmoothDamp(TargetPosition,
        //    currentPosition, ref Vec, 1f,0.001f);
        MainCamera.transform.position = Vector3.Lerp(currentPosition, TargetPosition, Time.deltaTime/10000);
        MainCamera.transform.position = Vector3.Lerp(TargetPosition, currentPosition, Time.deltaTime/10000);

    }

    private void ContinueFire()
    {
        SingleFire();
    }

    private void ReLoad()
    {
        var guninter = gunModel as GunDataFreshInterface;

        guninter?.Reload();
        guninter?.Fresh();
    }

    private void SingleFire()
    {
        var guninter = gunModel as GunDataFreshInterface;

       
            CreateBullet(gunModel.currentGunType);
            guninter?.ChangeRemainBullet(-1);
            foreach (var gunViewMuzzle in GunView.Muzzles)
            {
                gunViewMuzzle.Emit(1);
            }

            guninter?.Fresh();

        Recoil();
    }

    public void UnEquip()
    {
        gunModel.IsArmed = false;
        GunView.WeaponAnimator.SetBool(gunModel.m_armpistol,false);
        GunView.WeaponAnimator.SetBool(gunModel.m_armrifle,false);
    }
    public void SwitchGun(MVP_GunModel.GunType gunType)
    {
        UnEquip();
        gunModel.IsArmed = true;
        gunModel.ChangeGunModel(gunType);
        switch (gunType)
        {
            case MVP_GunModel.GunType.Rifle:
                GunView.WeaponAnimator.SetBool(gunModel.m_armrifle, true);
                break;
            case MVP_GunModel.GunType.Pistol:
                GunView.WeaponAnimator.SetBool(gunModel.m_armpistol, true);

                break;

        }
        PlaceMuzzle(gunType);

    }
    public void PlaceMuzzle(MVP_GunModel.GunType gunType)
    {
        switch(gunType)
        {
            case MVP_GunModel.GunType.Rifle:
                GunView.Muzzle.transform.SetParent(GunView.Rifle.transform);
                GunView.Muzzle.transform.position = GunView.RifleFirePos.transform.position;
                GunView.Muzzle.transform.forward = GunView.Rifle.transform.forward;
                break;
            case MVP_GunModel.GunType.Pistol:
                GunView.Muzzle.transform.SetParent(GunView.Pistol.transform);
                GunView.Muzzle.transform.position = GunView.PistolFirePos.transform.position;
                GunView.Muzzle.transform.forward = -GunView.Pistol.transform.right;
                break;
        }
    }
    void DetectInput()
    {
        gunModel.ShortPressFire = Input.GetKeyDown(KeyCode.Mouse0);
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            OnPressFire = OnLongPressFire;
            TimeM.StartTimer(0.5f, OnPressFire, 8);
        }
        gunModel.LongPressFire &= Input.GetKey(KeyCode.Mouse0);
        gunModel.ShortPressNum1 = Input.GetKeyDown(KeyCode.Alpha1);
        gunModel.ShortPressNum2 = Input.GetKeyDown(KeyCode.Alpha2);
        gunModel.ShortPressX = Input.GetKeyDown(KeyCode.X);
        gunModel.ShortPressR = Input.GetKeyDown(KeyCode.R);

    }
    public void OnLongPressFire()
    {
        if (Input.GetKey(KeyCode.Mouse0))
            gunModel.LongPressFire = true;
        else
            gunModel.LongPressFire = false;
    }

    public void OnRecall(string m)
    {
        Debug.Log(m);
    }
}
