using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class TestNotify : MonoBehaviour
{
    
    // Update is called once per frame
    void Update()
    {
        object[] testinfo = { 1, "TestNotify Calling,playerdied" };
        NotificationCenter.Instance.PostNotification("do not need param");
        NotificationCenter.Instance.PostNotification("playerdied",testinfo);
    }
}
