using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class GenerateMesh : MonoBehaviour
{
    public void CombineMeshes()
    {
        MeshFilter[] childMeshFilters = GetComponentsInChildren<MeshFilter>();
        MeshRenderer[] childMeshRenderers = GetComponentsInChildren<MeshRenderer>();

        CombineInstance[] combineInstances = new CombineInstance[childMeshFilters.Length];

        for (int i = 0; i < childMeshFilters.Length; i++)
        {
            combineInstances[i].mesh = childMeshFilters[i].sharedMesh;
            combineInstances[i].transform = childMeshFilters[i].transform.localToWorldMatrix;
        }

        Mesh combinedMesh = new Mesh();
        combinedMesh.CombineMeshes(combineInstances, true);

        GetComponent<MeshFilter>().sharedMesh = combinedMesh;

        // Disable child renderers since their meshes are now combined
        foreach (MeshRenderer renderer in childMeshRenderers)
        {
            renderer.enabled = false;
        }
    }


}

