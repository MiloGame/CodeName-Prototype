using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;

public class AimPosUpdate : MonoBehaviour
{
    // Start is called before the first frame update

    private Ray _ray;
    public Camera mainCamera;


    // Update is called once per frame
    void LateUpdate() //否则准星不跟手 必须放在最后更新
    {
        
        _ray.direction = mainCamera.transform.forward;
        _ray.origin = mainCamera.transform.position;
        RaycastHit _hitinfo;
        if (Physics.Raycast(_ray, out _hitinfo,1000,~(1<<8)) )
        {
            //Debug.DrawLine(_ray.origin,_hitinfo.point);
            //if (_hitinfo.transform.name != "Cube" && _hitinfo.transform.name != "Player")
            //{
                transform.position = _hitinfo.point; //世界坐标 
            //}
            

            //HitEffect.transform.forward = _hitinfo.normal;
        }
   
        else
        { transform.position = mainCamera.transform.position+ _ray.direction * 500;
            
           // transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
        }
       
        //Debug.DrawLine(_ray.origin, transform.position, Color.red);
    }
}