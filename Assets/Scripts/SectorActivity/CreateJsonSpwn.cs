using System.Collections;
using System.Collections.Generic;
using System.IO;
using Assets.Scripts;
using UnityEngine;

public class CreateJsonSpwn : MonoBehaviour
{
    public List<GameObject> LightSpwnObjects;
    public List<GameObject> SectorSpwnObjects;
    public List<GameObject> PropSpwnObjects;
    private PawnData tmPawnData;
    void Start()
    {
        
        if (Directory.Exists("Assets/JsonData/ScenePawnData"))
        {
            Directory.Delete("Assets/JsonData/ScenePawnData", true);
        }
        
        foreach (var objSpwnObject in LightSpwnObjects)
        {
            var trans = objSpwnObject.transform.position;
            tmPawnData = new PawnData(trans.x, trans.y, trans.z, objSpwnObject.name);
            CreateJson("JsonData/ScenePawnData", tmPawnData.Prefabname+".json", ref tmPawnData);
            
        }
        foreach (var objSpwnObject in SectorSpwnObjects)
        {
            var trans = objSpwnObject.transform.position;
            tmPawnData = new PawnData(trans.x, trans.y, trans.z,  objSpwnObject.name);
            CreateJson("JsonData/ScenePawnData", tmPawnData.Prefabname+".json", ref tmPawnData);
        }
        foreach (var objSpwnObject in PropSpwnObjects)
        {
            var trans = objSpwnObject.transform.position;
            tmPawnData = new PawnData(trans.x, trans.y, trans.z, objSpwnObject.name);
            CreateJson("JsonData/ScenePawnData", tmPawnData.Prefabname + ".json", ref tmPawnData);
        }
    }

    void CreateJson(string path,string filename, ref PawnData pawn)
    {

        DataSaveManager<PawnData>.Instance.SaveData(path, filename,
            ref pawn);

    }
}
