using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class ReBuildNavMesh : MonoBehaviour
{
    public NavMeshSurface navMeshSurface; // 参考你的 NavMeshSurface 组件
    public GameObject offMeshLinkPrefab; // OffMeshLink 预制体

    private void Start()
    {
        
        // 生成 NavMesh
        navMeshSurface.BuildNavMesh();

        // 获取生成的 Off-Mesh Links
        NavMeshLink[] offMeshLinks = FindObjectsOfType<NavMeshLink>();

        // 遍历 Off-Mesh Links
        foreach (var offMeshLink in offMeshLinks)
        {
            GameObject reverseOffMeshLinkObject = Instantiate(offMeshLinkPrefab, offMeshLink.startPoint, Quaternion.identity);

            var reverseOffMeshLink = reverseOffMeshLinkObject.GetComponent<NavMeshLink>();

            //设置反向 OffMeshLink 的起始和结束点
            reverseOffMeshLink.startPoint = offMeshLink.startPoint;
            reverseOffMeshLink.endPoint = offMeshLink.endPoint;
            reverseOffMeshLink.bidirectional = true;
            Destroy(offMeshLink.gameObject);


        }

        this.navMeshSurface.RemoveData();
        navMeshSurface.BuildNavMesh();
    }
}