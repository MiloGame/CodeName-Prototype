using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Apple;

public class TestBuildWall : MonoBehaviour
{
    public PrefabManger PbManger;
    public GameObject WallmakerPrefab;
    public GameObject wallcubesPrefab;
    public GameObject DestoryExplosion;
    Vector3 wallcubeOffset = new Vector3(0,0.6f,0);
    Vector3 wallCubeExplosionOffset = new Vector3(0,0.6f,0);

    private Vector3 destination;
    private GameObject Wallmaker;

    private float generateRange = 10f;

    private float wallDuration = 3f;
    private float updateRate = 0.7f;

    // Start is called before the first frame updatevoid Start()
    void Start()
    {

        Wallmaker = Instantiate(WallmakerPrefab);

        
    }
    void BuildWall()
    {
        GameObject wallcubes = Instantiate(wallcubesPrefab, Wallmaker.transform.position, Wallmaker.transform.rotation);

        StartCoroutine(DestoryWall(wallcubes));
    }

    void Aim()
    {

        Ray ray = PbManger.MainCamera.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit,generateRange,(1<<11)))
        {
            destination = hit.point;
            Wallmaker.transform.position = destination;

            Wallmaker.transform.right = PbManger.MainCamera.transform.right;
        }
            

    }
    IEnumerator  DestoryWall(GameObject WallToDestory)
    {
        float duration = wallDuration;
        float cracksAmount = 0; 
        List<Material> materials = new List<Material>();
        for (int i = 0; i < WallToDestory.transform.childCount; i++)
        {
            materials.Add(WallToDestory.transform.GetChild(i).GetChild(0).GetComponent<MeshRenderer>().material);
        }
        while (duration > 0)
        {
            duration -= updateRate;

            if (cracksAmount < 1)
            {
                cracksAmount += 1 / ((wallDuration - 0.2f) / updateRate);
                for (int i = 0; i < materials.Count; i++)
                    materials[i].SetFloat("_CracksAmount", cracksAmount);
            }

            yield return new WaitForSeconds(updateRate);

            if (duration <= 0)
            {
                for (int j = 0; j < WallToDestory.transform.childCount; j++)
                {
                    var explosion = Instantiate(DestoryExplosion, WallToDestory.transform.GetChild(j).transform.position + wallCubeExplosionOffset, Quaternion.identity) as GameObject;
                    Destroy(explosion, 3);
                }
                Destroy(WallToDestory);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        Aim();

        if (Input.GetKeyDown(KeyCode.E))
        {
            BuildWall();
        }
    }
}
