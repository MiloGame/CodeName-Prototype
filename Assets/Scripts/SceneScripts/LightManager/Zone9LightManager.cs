using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Zone9LightManager : MonoBehaviour
{
   
    float intensity9=5;

  
    private Light[] Zone09LLightsChild;
    public GameObject Zone09Light;

    void Awake()
    {

        Zone09LLightsChild =Zone09Light.GetComponentsInChildren<Light>();

        ChangeLightIntensity(ref Zone09LLightsChild, intensity9);
    }

    private void ChangeLightIntensity(ref Light[] fathrtLights , float divideTheshold)
    {
       
        foreach (var childLight in fathrtLights)
        {
            if (childLight.intensity > 1.5f)
            {
                childLight.intensity /= divideTheshold;
            }
            
        }
    }
}
