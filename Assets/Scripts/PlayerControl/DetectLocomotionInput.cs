using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(PlayerModel))]
public class DetectLocomotionInput : MonoBehaviour
{
    public PlayerModel playerModel;
    public CameraModel CameraModel;
    public event Action OnPressW;
    public event Action OnPressS;
    public event Action OnPressA;
    public event Action OnPressD;

    public event Action OnPressMouseLeft;
    public event Action OnPressV;
    public TimerManager TimeM;
    public int idx = 0;
    public event Action OnPressFire;
    private event Action OnPressDash;

    public void DealLocomotionInput()
    {



        playerModel.ShortPressW = Input.GetKey(KeyCode.W);
        if (Input.GetKeyDown(KeyCode.W))
        {
            OnPressW = OnLongPressW;
            TimeM.StartTimer(0.5f, OnPressW,0);
        }

        playerModel.LongPressW &= playerModel.ShortPressW;

        playerModel.ShortPressS = Input.GetKey(KeyCode.S);
        if (Input.GetKeyDown(KeyCode.S))
        {
            OnPressS = OnLongPressS;
            TimeM.StartTimer(0.5f, OnPressS,1);
        }
        playerModel.LongPressS &= playerModel.ShortPressS;

        playerModel.ShortPressA = Input.GetKey(KeyCode.A);
        if (Input.GetKeyDown(KeyCode.A))
        {
            OnPressA = OnLongPressA;
            TimeM.StartTimer(0.5f, OnPressA,2);
        }
        playerModel.LongPressA &= playerModel.ShortPressA;

        playerModel.ShortPressD = Input.GetKey(KeyCode.D);
        if (Input.GetKeyDown(KeyCode.D))
        {
            OnPressD= OnLongPressD;
            TimeM.StartTimer(0.5f, OnPressD,3);
        }
        playerModel.LongPressD &= playerModel.ShortPressD;

        playerModel.ShortPressDash = Input.GetKey(KeyCode.LeftShift);
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            OnPressDash = OnLongPressDash;
            TimeM.StartTimer(1.25f, OnPressDash, 4);
        }
        playerModel.LongPressDash &= playerModel.ShortPressDash;


        playerModel.ShortPressSpace = Input.GetKeyDown(KeyCode.Space);
        playerModel.ShortPressLeftAlt = Input.GetKeyDown(KeyCode.LeftAlt);

        playerModel.ShortPressLeftCtrl = Input.GetKeyDown(KeyCode.LeftControl);
        playerModel.ShortPressX = Input.GetKeyDown(KeyCode.X);
        playerModel.ShortPressNum1 = Input.GetKeyDown(KeyCode.Alpha1);
        playerModel.ShortPressNum2 = Input.GetKeyDown(KeyCode.Alpha2);

        playerModel.ShortPressFire = Input.GetKey(KeyCode.Mouse0);
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            OnPressFire = OnLongPressFire;
            TimeM.StartTimer(0.5f, OnPressFire, 7);
        }
        playerModel.LongPressFire &= playerModel.ShortPressFire;
        // <summary>
        // camera view input
        // </summary>

        CameraModel.ShortPressMouseLeft = Input.GetKey(KeyCode.Mouse1);
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            OnPressMouseLeft = OnLongPressMouseLeft;
            TimeM.StartTimer(1.5f, OnPressMouseLeft, 5);
        }
        CameraModel.LongPressMouseLeft &= CameraModel.ShortPressMouseLeft;

        CameraModel.ShortPressV = Input.GetKey(KeyCode.V);
        if (Input.GetKeyDown(KeyCode.V))
        {
            OnPressV = OnLongPressV;
            TimeM.StartTimer(1.5f, OnPressV, 6);
        }
        CameraModel.LongPressV &= CameraModel.ShortPressV;

        

    }

    public void OnLongPressFire()
    {
        if (Input.GetKey(KeyCode.Mouse0))
            playerModel.LongPressFire = true;
        else
            playerModel.LongPressFire = false;
    }

    public void OnLongPressV()
    {
        if (Input.GetKey(KeyCode.V))
            CameraModel.LongPressV = true;
        else
            CameraModel.LongPressV = false;
    }

    public void OnLongPressMouseLeft()
    {
        if (Input.GetKey(KeyCode.Mouse1))
            CameraModel.LongPressMouseLeft = true;
        else
            CameraModel.LongPressMouseLeft = false;
    }


    public void OnLongPressW()
    {
        if (Input.GetKey(KeyCode.W))
            playerModel.LongPressW = true;
        else
            playerModel.LongPressW = false;
    } 
    public void OnLongPressS()
    {
        if (Input.GetKey(KeyCode.S))
            playerModel.LongPressS = true;
        else
            playerModel.LongPressS = false;
    }
    public void OnLongPressA()
    {
        if (Input.GetKey(KeyCode.A))
            playerModel.LongPressA = true;
        else
            playerModel.LongPressA = false;
    } 
    public void OnLongPressD()
    {
        if (Input.GetKey(KeyCode.D))
            playerModel.LongPressD = true;
        else
            playerModel.LongPressD = false;
    }
    public void OnLongPressDash()
    {
        if (Input.GetKey(KeyCode.LeftShift))
            playerModel.LongPressDash = true;
        else
            playerModel.LongPressDash = false;
    }

}
    //public void DealDamage(float damage)
    //{
    //    Debug.Log("deal damage from controler"+damage);
    //}

