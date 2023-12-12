using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class BossAimNode : ActionNode, BehaviorTreeFunInterface
{
    public float TurretRotateSpeed;
    private RaycastHit hitinfo;

    public void OnStart()
    {
        
    }

    public void OnStop()
    {
    }

    public AllState OnUpdate()
    {
        var targetdir = TreeBlackBoard.PlayerTransform.position - TreeBlackBoard.BTManager.FireTransform.position;
        Ray ray = new Ray(TreeBlackBoard.BTManager.FireTransform.position, targetdir);
        if (Physics.Raycast(ray,out hitinfo))
        {
            TreeBlackBoard.BTManager.BooFootManager.AimPos.position = Vector3.Lerp(
                TreeBlackBoard.BTManager.BooFootManager.AimPos.position, hitinfo.transform.position
                , TurretRotateSpeed * Time.deltaTime);
        }
                        
        
        return AllState.Success;
    }

    public string GetNodeInfoAsString()
    {
        string info = $"Title: {titlename}\nWidth: {width}\nHeight: {height}\nPositionX: {positionx}\nPositionY: {positiony}\n" +
                      $"NodeType: {nodetype}\nIsExcuted:{IsExcuted}\n";
        return info;
    }
}
