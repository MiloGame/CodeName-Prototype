using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Object = UnityEngine.Object;

[Serializable]
public class PrefabManger : MonoBehaviour
{
    [SerializeField]
    public Transform PlayerTransform;
    [SerializeField]
    public Camera MainCamera;
    [SerializeField]
    public Transform LookAtPosTransform;
    [SerializeField]
    public Transform TpsmoonTransform;
    [SerializeField]
    public NewAnimatorPlayer NapPlayer;
    [SerializeField]
    public FSMData FsmData;
    [SerializeField]
    public TpsLandmove TpsLandMove;
    [SerializeField]
    public FSMManager FsmManager;

    public GameObject GrenadePrefab;
    public GameObject cameramanager;

    public GameObject Zone01Light;
    public GameObject Zone02Light;
    public GameObject Zone03Light;
    public GameObject Zone04Light;
    public GameObject Zone05Light;
    public GameObject Zone06Light;
    public GameObject Zone07Light;
    public GameObject Zone08Light;
    public GameObject Zone09Light;

    public GameObject Muzzle;

    public GameObject HitImpact;

    public GameObject PistolBullet;
    public GameObject RifleBullet;


    private string prefaboath = "ScenePrefabs/";
    private string jsonpath = "/JsonData/ScenePawnData/";
    private CustomTEventData<GameStatus> changEventData;
    public PawnData tmPawnData = new PawnData(0, 0, 0, "a");
    public GameObject Explodeprefab;
    public GameObject EarthShatter;
    public Animator PlayerAnimator;
    public GameObject HealEffectPrefab;
    public GameObject FullScreenHeal;
    public GameObject WallmakerPrefab;
    public GameObject DestoryExplosion;
    public GameObject wallcubesPrefab;
    public GameObject ShockwavePrefab;

    public void FreshStart()
    {
        MainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        changEventData = new CustomTEventData<GameStatus>();
        changEventData.message = GameStatus.Running;
        PlayerAnimator = PlayerTransform.GetComponent<Animator>();
    }

    public void FreshUpdate()
    {
        //Debug.Log("maincamera is null");
        //Debug.Log(this.MainCamera is null);
        //Debug.Log("maincamera is camera");
        //Debug.Log(this.MainCamera is Camera);
        if (!cameramanager.activeInHierarchy)
        {
            cameramanager.SetActive(true);
        }
        EventBusManager.Instance.ParamPublish(EventBusManager.EventType.ChangeGameState,this,changEventData);

    }
}
