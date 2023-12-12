using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(PlayerView))]
public class ShowPlayerView : Editor
{
    void OnSceneGUI()
    {
        PlayerView fow = target as PlayerView;
        Handles.color = Color.white;
        Handles.DrawWireArc(fow.transform.position, Vector3.up, Vector3.forward, 360f, fow.viewRadius);
        Vector3 viewAngleA = fow.EdiCalRadius(fow.radiusangle / 2);
        Vector3 viewAngleB = fow.EdiCalRadius(-fow.radiusangle / 2);
        Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleA * fow.viewRadius);
        Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleB * fow.viewRadius);
        Handles.color = Color.yellow;
        foreach (var fowTargetInRadiu in fow.targetInRadius)
        {
            Handles.DrawLine(fow.transform.position, fowTargetInRadiu.transform.position);
        }
        //if (fow.PlayerTransform != null)
        //{
        //    Handles.DrawLine(fow.transform.position, fow.PlayerTransform.position);
        //}
    }

}
