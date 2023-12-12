using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectPool 
{
    List<storePrefab> _pooledInstanceList;

    struct storePrefab
    {
        public GameObject obj;
        public MVP_GunModel.GunType gunType;

        public storePrefab(GameObject ob , MVP_GunModel.GunType gunType)
        {
            obj = ob;
            this.gunType = gunType;
        }
    }
    public GameObjectPool()
    {
        _pooledInstanceList = new List<storePrefab>();
    }

    public void AddtoPool(GameObject gb,MVP_GunModel.GunType gunType)
    {
        storePrefab sp = new storePrefab(gb, gunType);
        _pooledInstanceList.Add(sp);
    }

    public GameObject Create( MVP_GunModel.GunType gunType)
    {
        GameObject generateGameObject = null;
        switch (gunType)
        {
            case MVP_GunModel.GunType.Rifle:
                for (int i = 0; i < _pooledInstanceList.Count; i++){

                    if (_pooledInstanceList[i].gunType!=gunType)
                    {
                        continue;
                    }
                    else
                    {
                        if (!_pooledInstanceList[i].obj.GetComponent<BulletControl>().IsAlive)
                        {
                            //Debug.Log("dead rifle");
                            generateGameObject = _pooledInstanceList[i].obj;
                            _pooledInstanceList.RemoveAt(i);
                            break;
                        }
                    }
                    
                }
                break;
            case MVP_GunModel.GunType.Pistol:
                for (int i = 0; i < _pooledInstanceList.Count; i++)
                {

                    if (_pooledInstanceList[i].gunType != gunType)
                    {
                        continue;
                    }
                    else
                    {
                        if (!_pooledInstanceList[i].obj.GetComponent<BulletControl>().IsAlive)
                        {
                            Debug.Log("dead pistol");
                            generateGameObject = _pooledInstanceList[i].obj;
                            _pooledInstanceList.RemoveAt(i);
                            break;
                        }
                    }

                }

                break;
            
        }
        

        return generateGameObject;

    }


    public void Destory(GameObject gb, MVP_GunModel.GunType gunType)
    {
        gb.transform.GetComponent<TrailRenderer>().enabled = false;
        gb.SetActive(false);
        storePrefab sp = new storePrefab(gb, gunType);
        _pooledInstanceList.Add(sp);
    }
}
