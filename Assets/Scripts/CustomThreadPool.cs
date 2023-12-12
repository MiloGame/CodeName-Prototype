using System.Collections.Generic;
using Assets.Scripts;
using Unity.VisualScripting;
using UnityEngine;

public class CustomThreadPool
{
    Queue<GameObject> _pooledInstanceQueue = new Queue<GameObject>();

    private int cnt = 0;

    public GameObject Create()
    {
        if (_pooledInstanceQueue.Count > 0)
        {
            GameObject tmp = _pooledInstanceQueue.Dequeue();
            tmp.GetComponent<Renderer>().enabled = true;
            
            return tmp;
        }

        cnt++;
        var res = new GameObject();


        return res;

    }

    public void Destory(GameObject tmp)
    {
        tmp.GetComponent<Renderer>().enabled = false;
        _pooledInstanceQueue.Enqueue(tmp);

    }


}
