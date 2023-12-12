using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Zone3LightManager : MonoBehaviour
{
    //public PrefabManger PbManger;
    
    float intensity3=1.5f;
  

    private Light[] Zone03LLightsChild;
    public GameObject Zone03Light;

    void Awake()
    {

        Zone03LLightsChild = Zone03Light.GetComponentsInChildren<Light>();

        ChangeLightIntensity(ref Zone03LLightsChild, intensity3);
    }

    private void ChangeLightIntensity(ref Light[] fathrtLights, float divideTheshold)
    {

        foreach (var childLight in fathrtLights)
        {
            //Debug.Log("childlight name"+childLight.name+"before"+childLight.color);
            //childLight.color = Color.red;
            //Debug.Log("childlight name"+childLight.name+"after"+childLight.color);
            if (childLight.intensity > 1.5f)
            {
                childLight.intensity /= divideTheshold;
            }

        }
    }
}
