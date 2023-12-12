using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPosAttchView : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject TofolloeGameObject;

    public Vector3 offsetPos;
    public Transform AimPosGameObject;

    public void Update()
    {
        transform.position = TofolloeGameObject.transform.position + offsetPos;

        transform.forward = TofolloeGameObject.transform.up; // 手的local正向等于枪的世界forward   forwward正常为z 
    //    transform.LookAt(AimPosGameObject);
        Debug.Log("gunuoooo");
        //transform.rotation = Quaternion.Euler(offsetRos) * RootGameObject.transform.rotation;
    }
}
