using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class EffectManager : MonoBehaviour
{
    private CustomThreadPool _playertrailPool;
    public NotificationCenter NCenter;
    public float Hurtactivetime = 2f;
    public float Healactivetime = 2f;
    public float Hurtrate = 0.1f;
    public float Healrate = 0.1f;
    Vector3 heightoffset = new Vector3(0, 1.7f, 0);
    [SerializeField]private GameObject _fullScreenEffect;
    [SerializeField]private Material _fullScreenEffectFeature;
    [SerializeField]private Color _healColor ;
    [SerializeField]private Color _damageColor ;
    public GameObject SheildGameObject;
    public GameObject PlayerGameObject;
    private int _fullScreenColor = Shader.PropertyToID("_EffectColor");
    private Vector3 sheldpos;
    private float maxDistance=1f;
    private GameObject sheldinstance;
    private float SheldDestime=1f;
    private float SheldDestoryrate = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        NCenter = NotificationCenter.Instance;
        NCenter.AddObserver("GetHurt",GetHurt);
        NCenter.AddObserver("GetHeal", GetHeal);
        NCenter.AddObserver("GreateSheld", BuiltSheld);
        NCenter.AddObserver("DestorySheld", DestorySheld);
        ColorUtility.TryParseHtmlString("#B4E33D", out _healColor);
        _damageColor=Color.red;
        _fullScreenEffectFeature = _fullScreenEffect.GetComponent<Image>().material;
        _fullScreenEffect.SetActive(false);
    }
    void BuiltSheld(object[] data)
    {

        Ray ray = new Ray(PlayerGameObject.transform.position + heightoffset, PlayerGameObject.transform.forward);
        RaycastHit hitinfo;
        if (Physics.Raycast(ray,out hitinfo,maxDistance))
        {
            sheldpos = hitinfo.point;
        }
        else
        {
            sheldpos = ray.GetPoint(maxDistance);
        }

        sheldinstance = Instantiate(SheildGameObject, PlayerGameObject.transform);
        sheldinstance.transform.position = sheldpos ;
    }

    void DestorySheld(object[] data)
    {
        StartCoroutine(DestoryCorp(SheldDestime));
    }

     IEnumerator DestoryCorp(float sheldDestime)
    {
        while (sheldDestime > 0)
        {
            sheldDestime -= SheldDestoryrate;
            yield return new WaitForSeconds(SheldDestoryrate);
        }
        Destroy(sheldinstance);
    }

    private void GetHurt(object[] data)
    {
        Debug.Log("effect get hurt is called");
        _fullScreenEffect.SetActive(true);
        _fullScreenEffectFeature.SetColor("_EffectColor", _damageColor);
        _fullScreenEffectFeature.SetFloat("_VoronoiIntensity", 2f);
        StartCoroutine(Hurt(Hurtactivetime));
    }
     private void GetHeal(object[] data)
     {
         _fullScreenEffect.SetActive(true);
         _fullScreenEffectFeature.SetColor("_EffectColor", _healColor);
         _fullScreenEffectFeature.SetFloat("_VoronoiIntensity", 2f);
         StartCoroutine(Heal(Healactivetime));
     }

      IEnumerator Heal(float healtime)
     {
        while (healtime > 0)
        {
            healtime -= Healrate;
            yield return new WaitForSeconds(Healrate);
        }
        _fullScreenEffect.SetActive(false);
    }


     IEnumerator Hurt(float actime)
    {
        while (actime>0)
        {
            actime -= Hurtrate;
            yield return new WaitForSeconds(Hurtrate);
        }
        _fullScreenEffect.SetActive(false);
    }
}
