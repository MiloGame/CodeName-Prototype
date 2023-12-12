using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FireBaseView : MonoBehaviour
{
    public enum GunType
    {
        RifleGun,
        Pistol
    }
    public Dictionary<GunType, string> AniClipParamName;
    public List<GameObject> GunsPrefabs;
    public List<GameObject> BulletPrefab;
    public Camera HudCamera;
    public GameObject AimPosGameObject;
    public GameObject GunShotPosGameObject;
 
    public ParticleSystem[] MuzzleFlash;
   // public ParticleSystem[] HitEffect;
   public ParticleSystem HitEffit;

}
