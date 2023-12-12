using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Zone1LightManager : MonoBehaviour
{

    float intensity1=8;
    

    private Light[] Zone01LLightsChild;
    public GameObject Zone01Light;

    void Awake()
    {

        Zone01LLightsChild = Zone01Light.GetComponentsInChildren<Light>();

        ChangeLightIntensity(ref Zone01LLightsChild, intensity1);
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
