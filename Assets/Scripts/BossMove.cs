using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics;
using UnityEditor.Analytics;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations.Rigging;

public class BossMove : MonoBehaviour
{
    public BossFootIk LeftFootIk;
    public BossFootIk RightFootIk;
    public LayerMask walkLayerMask;
    public bool legsMove; // true for left false for right 
    public Transform Leftfoot;
    public Transform Rightfoot;
    public float distance;
    public float movespeed = 0.05f;
    public float StepHeight;
    public float stepHeight = 0.4f;

    private float StepLength = 1.6f;
    public Vector3 prepos;
    private float tomoveDistance;
    private RaycastHit hitinfo;
    public Vector3 rayorigin;
    public Vector3 leftrayOffset;
    public Vector3 rightrayOffset;
    public bool LetLeftmove;

    public bool LetRightMove;


    public Vector3 UpOffset = new Vector3(0 , 1, 0);
    public Transform hip;

    public Vector2 prePlane;
    public Vector2 transPlane;
    public bool EnableRotate;
    public Transform AimPos;
    public bool EnableMove;
    public Vector3 JumpStart;
    public Vector3 JumpEnd;

    public Vector3 aimpre;
    public bool EnableJump;
    public Rig BodyRig;
    public int _bezierPointnums = 10;
    public Vector3[] path;

    public Vector3 AimInitPos;
    // Start is called before the first frame update
    void Start()
    {
        AimInitPos = AimPos.position;
        path = new Vector3[_bezierPointnums];
        //hipPlane = new Vector2(hip.position.x, hip.position.z);
        transPlane = new Vector2(transform.position.x, transform.position.z);
        prePlane = new Vector2(transform.position.x, transform.position.z);
        aimpre = AimPos.position;
        legsMove = true;
        rayorigin = transform.position;
        prepos = transform.position;
        leftrayOffset = Leftfoot.localPosition * 0.4f;
        rightrayOffset = Rightfoot.localPosition * 0.4f;


        ////Debug.Log("leftoffset"+leftrayOffset+"calbyself"+(Leftfoot.position - hip.position)+"correct pos"+ Leftfoot.localPosition * 0.4f);
    }
    Vector3 GetBezierPoint(float t, Vector3 start, Vector3 center, Vector3 end)
    {
        return (1 - t) * (1 - t) * start + 2 * t * (1 - t) * center + t * t * end;
    }

    public void GenreateJumpPath(Vector3 Startpos, Vector3 Centerpos, Vector3 EndPos)
    {
        for (int i = 0; i < _bezierPointnums; i++)
        {
            float t = i / (float)(_bezierPointnums - 1);
            path[i] = GetBezierPoint(t, Startpos , Centerpos, EndPos);
        }
    }
    // Update is called once per frame
    void Update()
    {

        
        if (EnableRotate)
        {
            //StepHeight = 0;
            BodyRig.weight = 0;

            prepos = transform.position;
            rayorigin = transform.position;
            prePlane.x = rayorigin.x;
            prePlane.y = rayorigin.z;


            Ray ray = new Ray(UpOffset + rayorigin + transform.right.normalized * rightrayOffset.x +
                              transform.up.normalized * rightrayOffset.y +
                              transform.forward.normalized * rightrayOffset.z, Vector3.down);
            Debug.DrawRay(ray.origin, ray.direction, Color.white);

            if (Physics.Raycast(ray, out hitinfo, 10000, walkLayerMask.value))
            {
                RightFootIk.MoveToPosition(hitinfo.point, hitinfo.normal, movespeed);
            }
            Ray ray1 = new Ray(UpOffset + rayorigin + transform.right.normalized * leftrayOffset.x +
                               transform.up.normalized * leftrayOffset.y +
                               transform.forward.normalized * leftrayOffset.z, Vector3.down);
            Debug.DrawRay(ray1.origin, ray1.direction, Color.white);

            if (Physics.Raycast(ray1, out hitinfo, 10000, walkLayerMask.value))
            {
                LeftFootIk.MoveToPosition(hitinfo.point, hitinfo.normal, movespeed);
            }
        }
        else
        {
            BodyRig.weight = 1;

            StepHeight = stepHeight;
        }



        
        if (EnableMove)
        {
            RightFootIk.HoldPosition();
            LeftFootIk.HoldPosition();
            transPlane.x = transform.position.x;
            transPlane.y = transform.position.z;
            distance = Vector2.Distance(prePlane, transPlane);
            if (legsMove)
            {
                if (!LetLeftmove )
                {
                    LetLeftmove = true;
                    tomoveDistance = (distance - StepLength) > 0 ? StepLength : distance;
                    var movevector = (transPlane - prePlane).normalized * tomoveDistance;
                    rayorigin = prepos + new Vector3(movevector.x, 0, movevector.y);


                }

                Ray ray = new Ray(UpOffset + rayorigin + transform.right.normalized * leftrayOffset.x +
                    transform.up.normalized * leftrayOffset.y +
                    transform.forward.normalized * leftrayOffset.z, Vector3.down);
                Debug.DrawRay(ray.origin, ray.direction, Color.red);
                ////Debug.Log(ray.origin);
                if (Physics.Raycast(ray, out hitinfo, 10000, walkLayerMask.value))
                {
                    //Debug.Log("left hit " + hitinfo.point);

                    LeftFootIk.MoveToPosition(hitinfo.point, hitinfo.normal, movespeed, StepHeight);
                }
                else
                {
                    //Debug.Log("leftnot hit");
                }

                if (LeftFootIk.FinishMove)
                {
                    prepos = rayorigin;
                    prePlane.x = rayorigin.x;
                    prePlane.y = rayorigin.z;
                    legsMove = !legsMove;
                    LetLeftmove = false;
                    //indexpath = (indexpath+1) >= AiAgent.path.corners.Length ? (AiAgent.path.corners.Length -1): indexpath;
                }

            }
            else
            {
                if (!LetRightMove)
                {
                    LetRightMove = true;

                    tomoveDistance = (distance - StepLength) > 0 ? StepLength : distance;
                    var movevector = (transPlane - prePlane).normalized * tomoveDistance;
                    rayorigin = prepos + new Vector3(movevector.x, 0, movevector.y);

                }

                Ray ray = new Ray(UpOffset + rayorigin + transform.right.normalized * rightrayOffset.x +
                                  transform.up.normalized * rightrayOffset.y +
                                  transform.forward.normalized * rightrayOffset.z, Vector3.down);
                Debug.DrawRay(ray.origin, ray.direction, Color.red);

                if (Physics.Raycast(ray, out hitinfo, 10000, walkLayerMask.value))
                {
                    //Debug.Log("right hit " + hitinfo.point);
                    RightFootIk.MoveToPosition(hitinfo.point, hitinfo.normal, movespeed, StepHeight);
                }
                else
                {
                    //Debug.Log("right not hit");
                }


                if (RightFootIk.FinishMove)
                {
                    prepos = rayorigin;

                    prePlane.x = rayorigin.x;
                    prePlane.y = rayorigin.z;
                    legsMove = !legsMove;
                    LetRightMove = false;
                    //indexpath = (indexpath + 1) >= AiAgent.path.corners.Length ? (AiAgent.path.corners.Length - 1) : indexpath;
                }
            }
        }



        if (EnableJump)
        {
            BodyRig.weight = 0;

            prepos = transform.position;
            rayorigin = transform.position;
            prePlane.x = rayorigin.x;
            prePlane.y = rayorigin.z;


            Ray ray = new Ray(UpOffset + rayorigin + transform.right.normalized * rightrayOffset.x +
                              transform.up.normalized * rightrayOffset.y +
                              transform.forward.normalized * rightrayOffset.z, Vector3.down);
            Debug.DrawRay(ray.origin, ray.direction, Color.white);

            if (Physics.Raycast(ray, out hitinfo, 10000, walkLayerMask.value))
            {
                RightFootIk.MoveToPosition(hitinfo.point, hitinfo.normal, movespeed);
            }
            Ray ray1 = new Ray(UpOffset + rayorigin + transform.right.normalized * leftrayOffset.x +
                              transform.up.normalized * leftrayOffset.y +
                              transform.forward.normalized * leftrayOffset.z, Vector3.down);
            Debug.DrawRay(ray1.origin, ray1.direction, Color.white);

            if (Physics.Raycast(ray1, out hitinfo, 10000, walkLayerMask.value))
            {
                LeftFootIk.MoveToPosition(hitinfo.point, hitinfo.normal, movespeed);
            }
           
        }
        else
        {
            BodyRig.weight = 1;
        }






    }
}
