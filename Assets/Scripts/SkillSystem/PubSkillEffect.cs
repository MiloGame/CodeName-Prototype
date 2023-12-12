using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations.Rigging;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class PubSkillEffect : MonoBehaviour
{
    public PrefabManger PbManger;
    // Start is called before the first frame update
    private EventBusManager busManager;
    private float grenadeRadius;
    int _bezierPointnums = 30;
    public LineRenderer LineRenderer;
    private Vector3[] path ;
    public PlayerHealthManager PcHealthManager;
    Vector3 GrenadeEndPoint;
    Vector3 GrenadeControlPoint;
    private bool CanTrow;
    private CameraModel cameraModel;
    private MVP_GunPresenter gunPresenter;
    private float generateRange = 10f;
    Vector3 wallCubeExplosionOffset = new Vector3(0, 0.6f, 0);
    private float wallDuration = 3f;
    private float updateRate = 0.7f;
    private GameObject Wallmaker;
    private float Healval = 45f;

    Vector3 GetBezierPoint(float t, Vector3 start, Vector3 center, Vector3 end)
    {
        return (1 - t) * (1 - t) * start + 2 * t * (1 - t) * center + t * t * end;
    }
    void Start()
    {
        CanTrow = false; 
        path  = new Vector3[_bezierPointnums];
        busManager = EventBusManager.Instance;
        busManager.ParamScribe(EventBusManager.EventType.GrenadeEffect,GrenadeEffect);
        busManager.NonParamScribe(EventBusManager.EventType.ClearGrenadeTrail,ClearGrenadeTrail);
        busManager.NonParamScribe(EventBusManager.EventType.EarthShatter, StartEarthShatter);
        busManager.NonParamScribe(EventBusManager.EventType.GetHeal,StartHeal);
        busManager.NonParamScribe(EventBusManager.EventType.UpdateWallMaker,OnUpdateWallmaker);
        busManager.NonParamScribe(EventBusManager.EventType.BuildWall,OnBuildWall);
        busManager.NonParamScribe(EventBusManager.EventType.ControlEnemy, OnControlEnemy);
        cameraModel = PbManger.cameramanager.GetComponent<CameraModel>();
        gunPresenter = PbManger.PlayerTransform.GetComponent<MVP_GunPresenter>();
    }

    private void OnControlEnemy(object sender, EventArgs e)
    {
        GameObject shockwave =
            Instantiate(PbManger.ShockwavePrefab,PbManger.PlayerTransform);

    }

    private void OnBuildWall(object sender, EventArgs e)
    {
        GameObject wallcubes = Instantiate(PbManger.wallcubesPrefab, Wallmaker.transform.position, Wallmaker.transform.rotation);
        Destroy(Wallmaker);

        StartCoroutine(DestoryWall(wallcubes));
    }
    IEnumerator DestoryWall(GameObject WallToDestory)
    {
        float duration = wallDuration;
        float cracksAmount = 0;
        List<Material> materials = new List<Material>();
        for (int i = 0; i < WallToDestory.transform.childCount; i++)
        {
            materials.Add(WallToDestory.transform.GetChild(i).GetChild(0).GetComponent<MeshRenderer>().material);
        }
        while (duration > 0)
        {
            duration -= updateRate;

            if (cracksAmount < 1)
            {
                cracksAmount += 1 / ((wallDuration - 0.2f) / updateRate);
                for (int i = 0; i < materials.Count; i++)
                    materials[i].SetFloat("_CracksAmount", cracksAmount);
            }

            yield return new WaitForSeconds(updateRate);

            if (duration <= 0)
            {
                for (int j = 0; j < WallToDestory.transform.childCount; j++)
                {
                    var explosion = Instantiate(PbManger.DestoryExplosion, WallToDestory.transform.GetChild(j).transform.position + wallCubeExplosionOffset, Quaternion.identity) as GameObject;
                    Destroy(explosion, 3);
                }
                Destroy(WallToDestory);
            }
        }

    }
    private void OnUpdateWallmaker(object sender, EventArgs e)
    {
        Ray ray = PbManger.MainCamera.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, generateRange, (1 << 11)))
        {
            Wallmaker = Wallmaker == null ? Instantiate(PbManger.WallmakerPrefab,hit.point,Quaternion.identity) : Wallmaker;

            Wallmaker.transform.position =Vector3.MoveTowards(Wallmaker.transform.position,hit.point,2f);

            Wallmaker.transform.right = PbManger.MainCamera.transform.right;
        }
        else
        {
            Wallmaker = Wallmaker == null ? Instantiate(PbManger.WallmakerPrefab, hit.point, Quaternion.identity) : Wallmaker;

            Wallmaker.transform.position = Vector3.MoveTowards(Wallmaker.transform.position, PbManger.PlayerTransform.position+PbManger.PlayerTransform.forward*3f, 2f);

            Wallmaker.transform.right = PbManger.MainCamera.transform.right;
        }
    }


    private void ClearGrenadeTrail(object sender, EventArgs e)
    {
        if (CanTrow )
        {
            //Debug.Log("ClearGrenadeTrail calles" + sender);

            GameObject res = GameObject.Instantiate(PbManger.GrenadePrefab);
            res.AddComponent<GrenadeMove>();
            res.transform.position = PbManger.PlayerTransform.position + Vector3.up.normalized*0.5f;
            res.GetComponent<GrenadeMove>().Init(path, PbManger.Explodeprefab);
            LineRenderer.positionCount = 0;
        }

        CanTrow = false;
    }

    private void GrenadeEffect(object sender, EventArgs e)
    {
        //Debug.Log("GrenadeEffect calles" + sender);
        LineRenderer.positionCount = _bezierPointnums;
        CanTrow = true;
        var data = e as CustomTEventData<float>;
        if (data!=null)
        {
            grenadeRadius = data.message;
        }

        GrenadeEndPoint = PbManger.PlayerTransform.position+PbManger.PlayerTransform.forward * 0.5f + (PbManger.MainCamera.transform.forward).normalized * grenadeRadius;
        GrenadeControlPoint = (PbManger.PlayerTransform.position + Vector3.up.normalized * 0.5f + GrenadeEndPoint) * 0.5f + (Vector3.up * 3f);
        for (int i = 0; i < _bezierPointnums; i++)
        {
            float t = i / (float)(_bezierPointnums - 1);
            path[i] = GetBezierPoint(t, PbManger.PlayerTransform.position + Vector3.up.normalized * 1f+PbManger.PlayerTransform.forward*0.5f, GrenadeControlPoint, GrenadeEndPoint);
        }
        LineRenderer.SetPositions(path);
    }
    private void StartEarthShatter(object sender, EventArgs e)
    {
        gunPresenter.UnEquip();
        gunPresenter.gunModel.EnableFire = false;
        PbManger.PlayerTransform.GetComponent<PlayerModel>().EnableMove = false;
        PbManger.PlayerAnimator.SetLayerWeight(1,1);
        cameraModel.EnableSkillLook = true;
        PbManger.PlayerAnimator.SetTrigger("Combo");
    }
    public void EarthShatterAniStart()
    {
        cameraModel.EnableFreeLook = true;
        var earthshatter=Instantiate(PbManger.EarthShatter, PbManger.PlayerTransform.position+PbManger.PlayerTransform.forward*3f,
            Quaternion.identity);
        earthshatter.transform.forward = PbManger.PlayerTransform.forward;
        var hitscripts = earthshatter.GetComponentsInChildren<ParticleHitEnemy>();
        foreach (var particleHitEnemy in hitscripts)
        {
            particleHitEnemy.damage = 80f;
        }
        PbManger.PlayerTransform.GetComponent<PlayerModel>().EnableMove = true;
        gunPresenter.gunModel.EnableFire = true;

        Destroy(earthshatter, 3f);

    }
    public void EarthShatterAniFinish()
    {
        PbManger.PlayerAnimator.SetLayerWeight(1, 0);
        gunPresenter.SwitchGun(gunPresenter.gunModel.currentGunType);
    }

    public void HealAniFinish()
    {
        PbManger.PlayerAnimator.SetLayerWeight(1, 0);
        PbManger.PlayerAnimator.SetLayerWeight(0, 1);
        PbManger.FullScreenHeal.SetActive(false);

        gunPresenter.gunModel.EnableFire = true;
        PbManger.PlayerTransform.GetComponent<PlayerModel>().EnableMove = true;

    }
    private void StartHeal(object sender, EventArgs e)
    {
        //Debug.Log("heal trigger");
        gunPresenter.UnEquip();
        gunPresenter.gunModel.EnableFire = false;
        PbManger.PlayerTransform.GetComponent<PlayerModel>().EnableMove = false;
        PcHealthManager.RemainHealth = Mathf.Clamp(PcHealthManager.RemainHealth + Healval, 0, 100);
        PbManger.PlayerAnimator.SetLayerWeight(1, 1);
        PbManger.PlayerAnimator.SetLayerWeight(0, 0);
        PbManger.PlayerAnimator.applyRootMotion = true;
        PbManger.PlayerAnimator.SetTrigger("Heal");
        var healgameobj = Instantiate(PbManger.HealEffectPrefab, PbManger.PlayerTransform, false);
        healgameobj.transform.localPosition = new Vector3(0, 0.8f, 0);
        PbManger.FullScreenHeal.SetActive(true);
        Material _fullScreenEffectFeature = PbManger.FullScreenHeal.GetComponent<Image>().material;
        _fullScreenEffectFeature.SetColor("_EffectColor", Color.green);
        _fullScreenEffectFeature.SetFloat("_VoronoiIntensity", 2f);
        Destroy(healgameobj,3f);
    }
}
