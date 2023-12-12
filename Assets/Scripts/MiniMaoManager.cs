using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class MiniMaoManager 
{
    
    private Transform target;
    LoadManager lm;
    GameObject player;
    GameObject cam;

    float cameraHeight = 10f;
    Vector3 cameraTargetPosition;
    public MiniMaoManager(LoadManager LM)
    {
        lm = LM;
        player = lm.Getplayer;
        cam = lm.Getminicam;
        target = player.transform;
        //lm.minicameraFreshEvent += FreshScreen;
        NotificationCenter.Instance.AddObserver("MiniMapFresh",FreshScreen);
    }

    void DisNotify()
    {
        NotificationCenter.Instance.RemoveObserver("MiniMapFresh", FreshScreen);
    }
    void FreshScreen(object[] data)
    {
        cameraTargetPosition = new Vector3(target.position.x, target.position.y + cameraHeight, target.position.z);
        cam.transform.position = cameraTargetPosition;
        Vector3 cameraTargetDirection = Vector3.down.normalized;
        cam.transform.LookAt(cameraTargetDirection);
        cam.transform.rotation = Quaternion.LookRotation(cameraTargetDirection); // 使相机方向垂直于 XZ 平面
    }

}
