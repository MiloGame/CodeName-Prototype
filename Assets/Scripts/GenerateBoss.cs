using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.BehaviorTree;
using UnityEngine;

public class GenerateBoss: MonoBehaviour
{
    public GameObject Bossprefab;
    public GameObject Boss;
    public GameObject Player;
    private Vector3 BossgenreatePos;
    public Vector3 boxsize = new Vector3(5, 0, 2f);
    public LayerMask layerMask;
    private RaycastHit hitinfo;
    public GameObject BossFellowGameObject;
    void Start()
    {
        BossgenreatePos = new Vector3(-5.21f, 0.08f, -33.5f);
    }

    void FixedUpdate()
    {
        if (Physics.BoxCast(transform.position + Vector3.down, boxsize / 2, Vector3.up,
                out hitinfo, transform.rotation, 10, layerMask))
        {
            if (Boss == null)
            {
                TriggerGenerateBoss();
            }
            
        }
    }
    public void TriggerGenerateBoss()
    {
        Boss = Instantiate(Bossprefab, BossgenreatePos, Quaternion.identity);
        Boss.GetComponent<BehaviorTreeManger>().PlayerObj = Player;
        Boss.GetComponent<BehaviorTreeManger>().BossFellow = BossFellowGameObject;
        EventBusManager.Instance.NonParamPublish(EventBusManager.EventType.ShowBossHealth);
    }
    private void OnDrawGizmos()
    {


        Gizmos.color = Color.red;
        //Collider[] hitColliders = Physics.OverlapSphere(transform.position, 10);
        //foreach (Collider collider in hitColliders)
        //{
        //    Gizmos.DrawSphere(collider.transform.position, 0.1f);
        //}\
        Gizmos.DrawRay(transform.position + Vector3.down, Vector3.up * 10);
        Gizmos.DrawWireCube(transform.position, boxsize);
    }

}
