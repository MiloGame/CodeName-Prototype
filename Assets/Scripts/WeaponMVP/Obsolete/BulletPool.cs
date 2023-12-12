using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class BulletPool 
    {
        Queue<GameObject> _pooledInstanceQueue ;
        private GameObject generateGameObject;

        public BulletPool()
        {
            _pooledInstanceQueue = new Queue<GameObject>();
            generateGameObject = null;
        }
        public bool IsPoolEmpty()
        {
            return _pooledInstanceQueue.Count == 0;
        }

        public void AddtoPool(GameObject gb)
        {
           _pooledInstanceQueue.Enqueue(gb);
        }
        public GameObject Create()
        {
        //if (_pooledInstanceQueue.Count > 0 && _pooledInstanceQueue.Peek().GetComponent<BulletView>()._starVector3 == _pooledInstanceQueue.Peek().transform.position)
        if (_pooledInstanceQueue.Count > 0 )
        {
                
                generateGameObject= _pooledInstanceQueue.Dequeue();
            }
            else
            {
                generateGameObject = null;
            }
          //  generateGameObject.SetActive(true);
            //generateGameObject.GetComponent<TrailRenderer>().enabled = true;
            return generateGameObject;

        }
        public void Destory(GameObject tmp)
        {
            //tmp.SetActive(false);

            _pooledInstanceQueue.Enqueue(tmp);

       
        }


    }

