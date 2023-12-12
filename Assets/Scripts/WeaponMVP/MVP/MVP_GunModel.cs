using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MVP_GunModel :GunBaseData, GunDataFreshInterface
{
    public enum GunType
    {
        Rifle,
        Pistol,
        None
    }
    private Dictionary<GunType, GunBaseData> gunsDatas;
    public int m_armrifle = Animator.StringToHash("armrifle");
    public int m_armpistol = Animator.StringToHash("armpistol");
    public bool ShortPressFire;
    public bool LongPressFire;
    public bool ShortPressNum1;
    public bool ShortPressNum2;
    public bool ShortPressX;
    public bool IsArmed;
    public bool ShortPressR;
    private BindableTProperty<int> remainBindableTProperty;
    private BindableTProperty<int> totalBindableTProperty;
    public GunType currentGunType;
    public bool EnableFire;
    public float bulletdamage;

    public MVP_GunModel()
    {
        EnableFire = true;
        remains = -10;
        totalammo = -10;
        currentGunType = GunType.None;
        gunsDatas = new Dictionary<GunType, GunBaseData>();
        gunsDatas[GunType.Rifle] = new RifleModel();
        gunsDatas[GunType.Pistol] = new PistolModel();
        remainBindableTProperty = new BindableTProperty<int>();
        totalBindableTProperty = new BindableTProperty<int>();
        EventBusManager.Instance.ParamScribe(EventBusManager.EventType.DoDialogk, OnDialogStart);
        EventBusManager.Instance.ParamScribe(EventBusManager.EventType.DialogFinish, OnDialogFinish);

    }

    private void OnDialogFinish(object sender, EventArgs e)
    {
        EnableFire = true;
    }

    private void OnDialogStart(object sender, EventArgs e)
    {
        EnableFire = false;
    }

    public void ChangeGunModel(GunType gunType)
    {
        if (remains != -10)
        {
            gunsDatas[currentGunType].remains = remains;
        }
        
        currentGunType = gunType;
        this.bulletdamage = gunsDatas[gunType].bulletdamage;
        this.CanContinuShoot = gunsDatas[gunType].CanContinuShoot;
        this.CoolenTime = gunsDatas[gunType].CoolenTime;
        this.gravity = gunsDatas[gunType].gravity;
        this.totalammo = gunsDatas[gunType].totalammo;
        this.bulletLife = gunsDatas[gunType].bulletLife;
        this.speed = gunsDatas[gunType].speed;
        this.remains = gunsDatas[gunType].remains;
        Fresh();
    }

    public void Fresh()
    {
        remainBindableTProperty.SetValue(remains, EventBusManager.EventType.ModelChangeRemains);
        totalBindableTProperty.SetValue(totalammo, EventBusManager.EventType.ModelChangeTotal);
    }

    public void UnScribe()
    {
        
    }

    public void ChangeRemainBullet(int t)
    {
        if (remains > 0)
        {
            remains += t;
        }
    }

    public void ChangeTotalBullets(int t)
    {
        if (t > 0)
        {
            totalammo = t;
        }
    }

    public void Reload()
    {
        remains = totalammo;
    }
}
