using System.Collections.Generic;
using System.Linq;
using Unity.AI.Navigation;
#if UNITY_EDITOR
using UnityEditor.SceneManagement;
#endif
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.NavMesh
{
    public class RePlaceNevMeshLink : MonoBehaviour
    {
        public NavMeshData PrefabData;
        public string newNavMeshDataPath = "Assets/Scenes/SampleScene/NewNavMeshData.asset"; // New file path for saving the data

        public void Generate()
        {
            


            Debug.Log("regeneraste completed");
        }

        public void ClearLinks()
        {
            List<UnityEngine.AI.NavMeshLinkInstance> navMeshLinkList = GetComponentsInChildren<UnityEngine.AI.NavMeshLinkInstance>().ToList();
            foreach (var aLink in navMeshLinkList)
            {
                Debug.Log(aLink.owner);
                Debug.Log(aLink.valid);
                aLink.Remove();
            }
            
        }

    }
}

