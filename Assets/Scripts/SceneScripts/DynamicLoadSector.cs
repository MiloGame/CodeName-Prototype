using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicLoadSector : MonoBehaviour
{
    public string prefaboath = "ScenePrefabs/";
    public enum SectorType
    {
        Zone1Prefab,
        Zone2Prefab, 
        Zone3Prefab,
        Zone9Prefab
    }
    private GameObject Zone1Prefab;

    private GameObject Zone2Prefab;

    private GameObject Zone3Prefab;

    private GameObject Zone9Prefab;

    private SectorType currenType;
    public Dictionary<SectorType, GameObject> SectorCache;

    private Dictionary<SectorType, GameObject> InstanceCache;
    // Start is called before the first frame update
    //void Start()
    //{
    //    if (Directory.Exists("Assets/JsonData/ScenePawnData"))
    //    {
    //        CreateALLScene();
    //    }


    //        //DirectoryInfo prefabDirectoryInfo = new DirectoryInfo("Assets/Resources/ScenePrefabs");


    //}
    void Awake()
    {
        Zone1Prefab = Resources.Load<GameObject>(prefaboath+ "Zone1Scene");
        Zone2Prefab = Resources.Load<GameObject>(prefaboath+ "Zone2Scene");
        Zone3Prefab = Resources.Load<GameObject>(prefaboath+ "Zone3Scene");
        Zone9Prefab = Resources.Load<GameObject>(prefaboath+ "Zone9Scene");
        SectorCache = new Dictionary<SectorType, GameObject>();
        SectorCache[SectorType.Zone1Prefab] = Zone1Prefab;
        SectorCache[SectorType.Zone2Prefab] = Zone2Prefab;
        SectorCache[SectorType.Zone3Prefab] = Zone3Prefab;
        SectorCache[SectorType.Zone9Prefab] = Zone9Prefab;
        InstanceCache = new Dictionary<SectorType, GameObject>();
        InstanceCache[SectorType.Zone1Prefab] = null;
        InstanceCache[SectorType.Zone2Prefab] = null;
        InstanceCache[SectorType.Zone3Prefab] = null;
        InstanceCache[SectorType.Zone9Prefab] = null;
        //LoadSector(SectorType.Zone9Prefab);
        LoadSector(SectorType.Zone3Prefab);
        EventBusManager.Instance.NonParamScribe(EventBusManager.EventType.Restart, OnRestart);

    }

    private void OnRestart(object sender, EventArgs e)
    {
        UnloadSector(currenType);
        LoadSector(SectorType.Zone1Prefab);

    }

    public void LoadSector(SectorType typesSectorType)
    {
        currenType = typesSectorType;
        if (InstanceCache[typesSectorType]!=null)
        {
            return;
        }
        else
        {
            InstanceCache[typesSectorType] = Instantiate(SectorCache[typesSectorType]);
        }
    }

    public void UnloadSector(SectorType typesecType)
    {
        if (InstanceCache[typesecType] != null)
        {
            Destroy(InstanceCache[typesecType]);
            InstanceCache[typesecType] = null;
        }
        else
        {
            return;
        }
    }

}
