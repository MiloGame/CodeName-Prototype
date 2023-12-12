using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class LightManager : MonoBehaviour
{
    //public PrefabManger PbManger;
    public GameObject ZoneLight;
    float intensity1=8;
    float intensity2=4;
    float intensity3=1.5f;
    float intensity4=4;
    float intensity5=5;
    float intensity6=5;
    float intensity7=5;
    float intensity8=3;
    float intensity9=5;

    private Light[] Zone03LLightsChild;
    private Light[] Zone01LLightsChild;
    private Light[] Zone02LLightsChild;
    private Light[] Zone04LLightsChild;
    private Light[] Zone05LLightsChild;
    private Light[] Zone06LLightsChild;
    private Light[] Zone07LLightsChild;
    private Light[] Zone08LLightsChild;
    private Light[] Zone09LLightsChild;

    void Awake()
    {
        Zone03LLightsChild = ZoneLight.GetComponentsInChildren<Light>();
        ////Zone03LLightsChild = PbManger.Zone03Light.GetComponentsInChildren<Light>();
        //Zone01LLightsChild = PbManger.Zone01Light.GetComponentsInChildren<Light>();
        //Zone02LLightsChild = PbManger.Zone02Light.GetComponentsInChildren<Light>();
        //Zone04LLightsChild = PbManger.Zone04Light.GetComponentsInChildren<Light>();
        //Zone05LLightsChild = PbManger.Zone05Light.GetComponentsInChildren<Light>();
        //Zone06LLightsChild = PbManger.Zone06Light.GetComponentsInChildren<Light>();
        //Zone07LLightsChild = PbManger.Zone07Light.GetComponentsInChildren<Light>();
        //Zone08LLightsChild = PbManger.Zone08Light.GetComponentsInChildren<Light>();
        //Zone09LLightsChild = PbManger.Zone09Light.GetComponentsInChildren<Light>();

        ChangeLightIntensity(ref Zone03LLightsChild,intensity3);
        //ChangeLightIntensity(ref Zone01LLightsChild,intensity1);
        //ChangeLightIntensity(ref Zone02LLightsChild, intensity2);
        //ChangeLightIntensity(ref Zone04LLightsChild, intensity4);
        //ChangeLightIntensity(ref Zone05LLightsChild, intensity5);
        //ChangeLightIntensity(ref Zone06LLightsChild, intensity6);
        //ChangeLightIntensity(ref Zone07LLightsChild, intensity7);
        //ChangeLightIntensity(ref Zone08LLightsChild, intensity8);
        //ChangeLightIntensity(ref Zone09LLightsChild, intensity9);
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
