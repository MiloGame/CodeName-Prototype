using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractManager : MonoBehaviour
{
    public Transform PickupTransform;
    Camera MainCamera;
    public LayerMask DetectPickupMask;
    public LayerMask DetectNPCMask;
    private Ray castray;
    public float detectDistance;
    private RaycastHit hitinfo;

    // Start is called before the first frame update
    void Start()
    {
        MainCamera = Camera.main;
        castray = new Ray(MainCamera.transform.position, MainCamera.transform.forward);
    }

    // Update is called once per frame
    void Update()
    {
        castray.direction = MainCamera.transform.forward;
        castray.origin = MainCamera.transform.position;
        Debug.DrawLine(castray.origin,castray.origin+100* castray.direction, Color.blue);
        if (Physics.Raycast(castray,out hitinfo,detectDistance,DetectPickupMask))
        {
            //Debug.Log("hit transform"+hitinfo.transform.name);
            var pickupobj = hitinfo.transform.GetComponent<CanPickUpObject>();
            if (Input.GetKey(KeyCode.F))
            {
                pickupobj?.SetFollowTarget(PickupTransform);
                pickupobj?.SetBarInvisible();
            }
            else
            {
                pickupobj?.SetBarPos();
                pickupobj?.Drop();
            }
        }
        if (Physics.Raycast(castray,out hitinfo,detectDistance,DetectNPCMask))
        {
            //Debug.Log("npc detect hit transform" + hitinfo.transform.name);
            var npcmanager = hitinfo.transform.GetComponent<NpcManager>();
            if (!npcmanager.IsTalking)
            {
                npcmanager.ShowPressF();
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                npcmanager.IsTalking = true;
                npcmanager.PlayerTransform = transform;
            }
        }
       
    }
}
