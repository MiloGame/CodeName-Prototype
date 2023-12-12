using Assets.Scripts.NavMesh;
using UnityEditor;
using UnityEngine;
using Assets.Scripts.NavMesh;
#if UNITY_EDITOR
[CustomEditor(typeof(Assets.Scripts.NavMesh.RePlaceNevMeshLink))]
[CanEditMultipleObjects]
public class ReplaceLinkEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("ReGenerate"))
        {
            foreach (var targ in targets)
            {
                ((RePlaceNevMeshLink)targ).Generate();
            }
        }

        if (GUILayout.Button("ClearLinks"))
        {
            foreach (var targ in targets)
            {
                ((RePlaceNevMeshLink)targ).ClearLinks();
            }
        }
    }
}
#endif