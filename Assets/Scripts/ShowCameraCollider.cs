using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowCameraCollider : MonoBehaviour
{
    public GameObject cam;
    public GameObject LookAtObj;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Ray raydown = new Ray(cam.transform.position, LookAtObj.transform.position - cam.transform.position);
        RaycastHit hitdownInfo;
        if (Physics.Raycast(raydown, out hitdownInfo))
        {
            Gizmos.DrawSphere(hitdownInfo.point, 0.1f);
        }

    }
}
