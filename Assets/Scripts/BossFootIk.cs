using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
[Serializable]
public class BossFootIk : MonoBehaviour
{
    



   

    [SerializeField] private Vector3 newPos,oldpos;

    private Vector3 newNormal,oldnormal;

    public float lerpparam;

    public bool EnableWalk;

    public bool EnableJump;
    public Vector3 MoveDelta;
    public Vector3 rayOffset;
    public bool IsExuted;
    public bool FinishMove;
    public bool IsCalDis;
    public float dis;
    public Vector2 targetplane =new Vector2();
    public Vector2 footplane =new Vector2();
    public Quaternion preRot;
    public Vector2 transplane;
    public LayerMask walkLayerMask;
    private float lerpjump;
    public float RemainDis;
    public float ThereShold;


    // Start is called before the first frame update
    void Start()
    {
        preRot = transform.rotation;
        rayOffset = transform.localPosition * 0.4f;
        oldnormal = newNormal = transform.up;
        oldpos = newPos = transform.position;
        lerpparam = 0;
        EnableJump = false;
        EnableWalk = false;
    }

    //void Update()
    //{

    //        HoldPosition();

        
    //}
    public void RotateHole(Vector3 parentforward)
    {
        transform.forward = parentforward;
    }

    public void HoldPosition()
    {
        transform.position = oldpos;
        transform.up = oldnormal;
        //transform.rotation = preRot;
        IsExuted = false;
        lerpparam = 0;
    }

    public void MoveToPosition(Vector3 targetPos,Vector3 targetNormal,float movespeed,float StepHeight)
    {
        
        if (!IsCalDis)
        {
            IsCalDis = true;
            targetplane = new Vector2(targetPos.x, targetPos.z);
            transplane = new Vector2(targetPos.x, targetPos.z);
            footplane = new Vector2(transform.position.x, transform.position.z);
            dis = Vector2.Distance(targetplane, footplane);
        }            
        
        newPos = targetPos;
        newNormal = targetNormal;
        RemainDis = Vector3.Distance(transform.position, targetPos);
        //Debug.Log("foot pos" + transform.position + "target pos" + targetPos + "the same" + (transform.position == targetPos) + "distance" + Vector3.Distance(transform.position, targetPos));
        if (RemainDis < ThereShold )
        {
           //Debug.Log("finish move");
            FinishMove = true;
            IsCalDis = false;
        }
        else
        {
           //Debug.Log("not finish move");
            if (dis!=0)
            {
                lerpparam = Vector2.Distance(footplane, targetplane) / dis;
            }
            else
            {
                lerpparam = 0;
            }
             //Debug.Log(lerpparam);
            MoveDelta = transform.position;
            MoveDelta = Vector3.MoveTowards(MoveDelta, newPos, movespeed);
            MoveDelta.y = Mathf.Sin(lerpparam * Mathf.PI) * StepHeight + targetPos.y;
            footplane.x = MoveDelta.x;
            footplane.y = MoveDelta.z;
            transform.position = MoveDelta;
            transform.up = Vector3.MoveTowards(transform.up, newNormal, movespeed);

            oldnormal = transform.up;
            oldpos = transform.position;
            FinishMove = false;
        }

    } 
    public void MoveToPosition(Vector3 targetPos,Vector3 targetNormal,float movespeed)
    {
        
     
        

            transform.position = Vector3.MoveTowards(transform.position, targetPos, movespeed);
            transform.up = Vector3.MoveTowards(transform.up, newNormal, movespeed);
            oldnormal = transform.up;
            oldpos = transform.position;


    }

    
}
