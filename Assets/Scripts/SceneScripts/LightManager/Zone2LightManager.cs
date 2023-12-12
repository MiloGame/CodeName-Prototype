using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Zone2LightManager : MonoBehaviour
{


    float intensity2=4;
   

    private Light[] Zone02LLightsChild;
    public GameObject Zone02Light;

    void Awake()
    {

        Zone02LLightsChild = Zone02Light.GetComponentsInChildren<Light>();

        ChangeLightIntensity(ref Zone02LLightsChild, intensity2);
    }

    private void ChangeLightIntensity(ref Light[] fathrtLights, float divideTheshold)
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
