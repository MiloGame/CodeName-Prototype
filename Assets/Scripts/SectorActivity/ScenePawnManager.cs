using System.Collections;
using System.Collections.Generic;
using System.IO;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;

public class ScenePawnManager : MonoBehaviour
{
    private string prefaboath = "ScenePrefabs/";
    private string jsonpath = "/JsonData/ScenePawnData/";
    public GameObject Zone3;
    public PawnData tmPawnData = new PawnData(0,0,0,"a");
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
        Instantiate(Zone3);
    }
    private void CreateSpecticScene()
    {
        
    }


    private void CreateALLScene()
    {
        DirectoryInfo prefabDirectoryInfo = new DirectoryInfo("Assets/JsonData/ScenePawnData");
        FileInfo[] files = prefabDirectoryInfo.GetFiles("*");
        for (int i = 0; i < files.Length; i++)
        {
            if (files[i].Name.EndsWith(".meta"))
                continue;
            else
            {
                DataSaveManager<PawnData>.Instance.LoadData(jsonpath  +files[i].Name, ref tmPawnData);
                var  gameObject=Resources.Load<GameObject>(prefaboath+tmPawnData.Prefabname);
                if (gameObject!=null)
                {
                    Instantiate(gameObject, new Vector3(tmPawnData.VectorX, tmPawnData.VectorY, tmPawnData.VectorZ),
                    Quaternion.identity);
                }

                
            }
        }
    }


}
