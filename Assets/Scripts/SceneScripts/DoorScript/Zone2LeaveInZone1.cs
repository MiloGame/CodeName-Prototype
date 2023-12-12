using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone2LeaveInZone1 : MonoBehaviour
{
    public DynamicLoadSector LoadSector;
    public Door TriggerDoor;
    int layerMask = 1 << 8;
    public Vector3 boxsize =new Vector3(20, 0, 1.5f);
    private RaycastHit hitinfo;

    public bool DetectPlayer;
    public void CallTriggerDoor()
    {
        TriggerDoor.closeDoor();
    }

    void FixedUpdate()
    {
        if (Physics.BoxCast(transform.position+Vector3.down,boxsize/2 ,Vector3.up,
                out hitinfo , transform.rotation, 10,layerMask ))
        {
            DetectPlayer = true;
            if (TriggerDoor.IsOpen)
            {
                LoadSector.UnloadSector(DynamicLoadSector.SectorType.Zone2Prefab);
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