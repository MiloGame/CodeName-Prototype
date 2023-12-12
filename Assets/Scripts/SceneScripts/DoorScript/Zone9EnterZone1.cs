using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone9EnterZone1 : MonoBehaviour
{
    public DynamicLoadSector LoadSector;
    public bool DetectPlayer;
    public Door TriggerDoor;
    int layerMask = 1 << 8;
    public Vector3 boxsize = new Vector3(5, 0, 2f);
    private RaycastHit hitinfo;
    public void CallTriggerDoor()
    {
        TriggerDoor.openDoor();
    }
    void FixedUpdate()
    {
        if (Physics.BoxCast(transform.position + Vector3.down, boxsize/2, Vector3.up,
                out hitinfo, transform.rotation, 10, layerMask))
        {
            DetectPlayer = true;
            if (TriggerDoor.IsClose)
            {
                LoadSector.LoadSector(DynamicLoadSector.SectorType.Zone1Prefab);
                CallTriggerDoor();
            }


        }
        else
        {
            DetectPlayer = false;
        }
     
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
        Gizmos.DrawWireCube(transform.position , boxsize);
    }
}
