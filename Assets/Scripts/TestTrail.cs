using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTrail : MonoBehaviour
{
    [SerializeField]private bool isTrailActive;
    [SerializeField] private float activeTzime =2f;
    [SerializeField] private float meshRefreshRate =0.05f;
    [SerializeField] private SkinnedMeshRenderer[] childMeshRenderers;
    [SerializeField] private GameObject tocopyGameObject;
    [SerializeField] private CustomThreadPool tpThreadPool;
    public Transform positiontoSpwn;
    private float deathtime=3f;
    public Material Mat1;
    public Material Mat2;

    void Start()
    {
        tpThreadPool = new CustomThreadPool();

    }
    // Update is called once per frame
    void Update()
    {
        //Debug.Log("timelast"+activeTzime+"istrailactive"+isTrailActive);
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isTrailActive)
        {
            isTrailActive = true;
            StartCoroutine(ActivateTrail(activeTzime));
        }       
    }

    IEnumerator ActivateTrail(float timelast)
    {
        Debug.Log("timelast" + timelast );

        while (timelast >0)
        {
            //Debug.Log("timelast" + timelast + (childMeshRenderers == null));

            timelast -= meshRefreshRate;
         
            //拷贝当前模型
            
                //获取子物体网格
                SkinnedMeshRenderer oldMeshRenderer = tocopyGameObject.GetComponent<SkinnedMeshRenderer>();
                    Debug.Log("oldMeshRenderer"+(oldMeshRenderer==null));

                //获取当前的网格数据
                Mesh newMesh = new Mesh();
                newMesh = oldMeshRenderer.sharedMesh;
                oldMeshRenderer.BakeMesh(newMesh);
                //创建物体
                GameObject newModel = new GameObject("NewModel");
            //newModel.transform.SetPositionAndRotation(positiontoSpwn.position, positiontoSpwn.rotation);
            //位置旋转缩放
            newModel.transform.localPosition = positiontoSpwn.localPosition;
            newModel.transform.forward = -positiontoSpwn.forward;

            newModel.transform.localScale = positiontoSpwn.localScale;

            //网格赋值到新物体
            MeshFilter newMeshFilter = newModel.AddComponent<MeshFilter>();
                newMeshFilter.mesh = newMesh;

                MeshRenderer newMeshRenderer = newModel.AddComponent<MeshRenderer>();
                Destroy(newModel,deathtime);
            //复制材质球
            List<Material> materialList = new List<Material>();
            oldMeshRenderer.GetSharedMaterials(materialList);
            if (materialList != null && materialList.Count > 0)
            {
                newMeshRenderer.materials = new Material[materialList.Count];
                //newMeshRenderer.materials[0].CopyPropertiesFromMaterial(materialList[0]);
                //newMeshRenderer.materials[1].CopyPropertiesFromMaterial(materialList[1]);
                //newMeshRenderer.materials[0].shader = Mat1.shader;
                //newMeshRenderer.materials[1].shader = Mat2.shader;
                newMeshRenderer.materials[0] = Mat1;
                newMeshRenderer.materials[1] = Mat2; //需要apply
                
                //for (int j = 0; j < materialList.Count; j++)
                //{
                //    //复制材质球
                //    newMeshRenderer.materials[j].CopyPropertiesFromMaterial(materialList[j]);
                //    //设置材质球Shader
                //    newMeshRenderer.materials[j].shader = materialList[j].shader;
                //}
            }
            

            yield return new WaitForSeconds(meshRefreshRate);
        }

        isTrailActive = false;
    }
}
