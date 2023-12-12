using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class ReBuildNavMesh : MonoBehaviour
{
    public NavMeshSurface navMeshSurface; // �ο���� NavMeshSurface ���
    public GameObject offMeshLinkPrefab; // OffMeshLink Ԥ����

    private void Start()
    {
        
        // ���� NavMesh
        navMeshSurface.BuildNavMesh();

        // ��ȡ���ɵ� Off-Mesh Links
        NavMeshLink[] offMeshLinks = FindObjectsOfType<NavMeshLink>();

        // ���� Off-Mesh Links
        foreach (var offMeshLink in offMeshLinks)
        {
            GameObject reverseOffMeshLinkObject = Instantiate(offMeshLinkPrefab, offMeshLink.startPoint, Quaternion.identity);

            var reverseOffMeshLink = reverseOffMeshLinkObject.GetComponent<NavMeshLink>();

            //���÷��� OffMeshLink ����ʼ�ͽ�����
            reverseOffMeshLink.startPoint = offMeshLink.startPoint;
            reverseOffMeshLink.endPoint = offMeshLink.endPoint;
            reverseOffMeshLink.bidirectional = true;
            Destroy(offMeshLink.gameObject);


        }

        this.navMeshSurface.RemoveData();
        navMeshSurface.BuildNavMesh();
    }
}