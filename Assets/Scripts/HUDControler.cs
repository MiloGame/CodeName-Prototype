using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Reflection;
using Assets.Scripts;
using TMPro;

public class HUDControler 
{
    public TextMeshProUGUI healthText,messageBar;
    public Image sheldBar,finalBar,Aim;
    public Image[] healthBar;
    public float health, sheld,final,maxFinal =100,maxHealth = 100,maxShield=100;
    public float lerpsppeed = 3f;
    Canvas HUD;
    Camera HUDCamera;
    //Camera mainCamera;
    // Start is called before the first frame update
    public HUDControler(Canvas hud)
    {
        HUD = hud;
        Init();
        //Debug.Log("iNIT Success");
    }
   

    //public void BarFiller(Image im,float value,float total)
    //{
    //    im.fillAmount = Mathf.Lerp(im.fillAmount,value/total,lerpsppeed);
    //    //Debug.Log(MethodBase.GetCurrentMethod().Name);
    //}

    //public void colorChanger()
    //{
    //    Color changeColor = Color.Lerp(Color.red,Color.green,(health/maxHealth));
    //    sheldBar.color = changeColor;
    //    finalBar.color = changeColor;
    //    //Debug.Log(MethodBase.GetCurrentMethod().Name);
    //}
    //public void HealthBarFiller()
    //{
    //    healthText = HUD.transform.GetChild(0).GetComponentInChildren<TextMeshProUGUI>();
    //    healthText.text = health + "|" + maxHealth;
    //    for (int i=0;i<healthBar.Length;i++)
    //    {
    //        healthBar[i].enabled = !displayHealthPoint(health,i);
    //    }
    //    //Debug.Log(MethodBase.GetCurrentMethod().Name);
    //}
    
    //public void AimFresh()
    //{

    //    // 将屏幕坐标 Input.mousePosition 转换为 HUD 摄像机下的世界坐标
    //    Vector3 hudCameraWorldPos = HUDCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, HUD.planeDistance));
    //    Vector2 localPos;
    //    RectTransformUtility.ScreenPointToLocalPointInRectangle(Aim.GetComponentInParent<Canvas>().GetComponent<RectTransform>(), hudCameraWorldPos, null, out localPos);
    //    Aim.GetComponent<RectTransform>().anchoredPosition = localPos;
    //}

    public void Init()
    {
        //Debug.Log(MethodBase.GetCurrentMethod().Name);
        Aim = HUD.transform.GetChild(3).GetComponent<Image>();
        HUDCamera = GameObject.Find("HUDCamera").GetComponent<Camera>();
        //mainCamera = Camera.main;
        //object[] info = { transferMousPos() };
        //NotificationCenter.Instance.PostNotification("crossfire",info);
    }
    //public Vector3 transferMousPos()//修改
    //{
    //    // 将鼠标屏幕位置转换为 MainCamera 视口中的坐标
    //    Vector3 mouseViewportPosition = mainCamera.ScreenToViewportPoint(Aim.transform.position);

    //    // 将视口坐标转换为世界坐标
    //    Vector3 mouseWorldPosition = mainCamera.ViewportToWorldPoint(new Vector3(mouseViewportPosition.x, mouseViewportPosition.y, mainCamera.nearClipPlane));
    //    return mouseWorldPosition;
    //}
}
