using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarFollowMOve : MonoBehaviour
{
    public Transform target;

    public Vector3 OffVector3;
    public Image imageui;
    void LateUpdate()
    {
        Vector3 direction = (target.transform.position - Camera.main.transform.position).normalized;
        bool isbehind = Vector3.Dot(direction, Camera.main.transform.forward) <= 0.0f; //垂直于这两个向量
        imageui.enabled = !isbehind;
        transform.position = Camera.main.WorldToScreenPoint(target.position+OffVector3);
    }
}
