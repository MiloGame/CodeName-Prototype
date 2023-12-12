using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using Cysharp.Threading.Tasks.Triggers;
using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(TestEnySeen))]
public class ShowFieldView : Editor
{
    void OnSceneGUI()
    {
        TestEnySeen fow = target as TestEnySeen;
        Handles.color = Color.white;
        Handles.DrawWireArc(fow.transform.position,Vector3.up, Vector3.forward, 360f,fow.viewRadius);
        Vector3 viewAngleA = fow.EdiCalRadius(fow.radiusangle / 2);
        Vector3 viewAngleB = fow.EdiCalRadius(-fow.radiusangle / 2);
        Handles.DrawLine(fow.transform.position,fow.transform.position + viewAngleA*fow.viewRadius);
        Handles.DrawLine(fow.transform.position,fow.transform.position + viewAngleB*fow.viewRadius);
        Handles.color = Color.yellow;
        if (fow.PlayerTransform!=null)
        {
            Handles.DrawLine(fow.transform.position,fow.PlayerTransform.position );
        }
    }
    
}
