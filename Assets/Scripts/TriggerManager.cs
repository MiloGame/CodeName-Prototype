using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts
{
    public class TriggerEvent : UnityEvent<Collider> { }
    public class TriggerManager : MonoBehaviour
    {
        //GameObject bottle, bottlePrefab;

        //void OnTriggerEnter(Collider other)
        //{
        //    bottlePrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Bottle/Prefabs/Bottle.prefab");
        //    bottle = GameObject.Find("Bottle");
        //    Vector3 bottlepos = bottle.transform.position;
        //    if (other.name == "BottleLOD0")
        //    {
            
        //        bottle.GetComponent<Bottle>().Explode();
        //        bottle = Instantiate(bottlePrefab);
        //        bottle.transform.position = bottlepos;
        //        bottle.name = "Bottle";
      
            
        //    }
        
        //    //Debug.Log("--collider enter"+other.name);

       
        //}


        //private void OnTriggerStay(Collider other)
        //{

        //    // Debug.Log("--collider stary" + other.name);
        //    //Destroy(gameObject);
        //    if (gameObject.name == "BulletPrefabs(Clone)")
        //    {
        //        gameObject.GetComponent<Bullet>().bullpool.Destory(gameObject);
        //    }

        //}
        //private void OnTriggerExit(Collider other)
        //{
        //    //Destroy(gameObject);
        //    //  Debug.Log("--collider exit" + other.name);
    
        //}
    }
}