using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations.Rigging;

[Serializable]
public class BossFootManager : MonoBehaviour
{
    public BossFootIk LeftFootIk;
    public BossFootIk RightFootIk;
    public LayerMask walkLayerMask;
    public bool legsMove; // true for left false for right 
    public MultiRotationConstraint roots;
    public Transform PlayerTransform;
    public NavMeshAgent AiAgent;
    public Transform Leftfoot;
    public Transform Rightfoot;
    public float distance;
    public float movespeed =0.05f ;
    public float StepHeight = 0.4f;

    private float StepLength = 1.6f;
    public Vector3 prepos;
    private float tomoveDistance;
    private RaycastHit hitinfo;
    public Vector3 rayorigin;
    public Vector3 leftrayOffset;
    public Vector3 rightrayOffset;
    public List<Vector3> enemypath;
    public bool LetLeftmove;

    public bool LetRightMove;
    public NavMeshPath GuidePath;

    private Vector3 reletiveDir;
    private int indexpath;
    private int times = 0;
    private Vector3 BornPlace;
    public bool CancelOnGround;

    public Transform hip;
    public Vector2 transPlane;
    public Vector2 prePlane;

    // Start is called before the first frame update
    void Start()
    {
        prePlane = new Vector2(transform.position.x, transform.position.z);

        transPlane = new Vector2(transform.position.x, transform.position.z);
        indexpath = 0;
        GuidePath = new NavMeshPath();
        BornPlace = transform.position;
     
        legsMove = true;
        rayorigin = hip.position;
        prepos = hip.position;

        leftrayOffset = Leftfoot.position - hip.position;
        rightrayOffset = Rightfoot.position - hip.position;

    }


    // Update is called once per frame
    void Update()
    {
        //Debug.Log("hip right"+hip.right+"world"+hip.localToWorldMatrix * hip.right);
        RightFootIk.HoldPosition();
        LeftFootIk.HoldPosition();
        transPlane.x = transform.position.x;
        transPlane.y = transform.position.z;

        distance = Vector2.Distance(prePlane, transPlane);


                if (legsMove)
                {

                    if (!LetLeftmove)
                    {
                        LetLeftmove = true;
                        tomoveDistance = (distance - StepLength) > 0 ? StepLength : distance;
                        var movevector =(transPlane - prePlane).normalized * tomoveDistance;
                        rayorigin = prepos + new Vector3(movevector.x,0,movevector.y);
                      

                    }
             
                Ray ray = new Ray(rayorigin + hip.right.normalized * Vector3.Dot(hip.right,leftrayOffset) +
                                  hip.up.normalized * Vector3.Dot(hip.up, leftrayOffset) +
                                  hip.forward.normalized  *Vector3.Dot(hip.forward, leftrayOffset), Vector3.down);
                Debug.DrawRay(ray.origin, ray.direction, Color.red);
                    //Debug.Log(ray.origin);
                    if (Physics.Raycast(ray, out hitinfo, 30, walkLayerMask.value))
                    {
                        LeftFootIk.MoveToPosition(hitinfo.point, hitinfo.normal,movespeed,StepHeight);
                    }

                    if (LeftFootIk.FinishMove)
                    {
                        prepos = rayorigin;
                        legsMove = !legsMove;
                        LetLeftmove = false;
                        prePlane.x = rayorigin.x;
                        prePlane.y = rayorigin.z;
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

                    Ray ray = new Ray(rayorigin + hip.right.normalized * Vector3.Dot(hip.right, rightrayOffset) +
                                      hip.up.normalized * Vector3.Dot(hip.up, rightrayOffset) +
                                      hip.forward.normalized * Vector3.Dot(hip.forward, rightrayOffset), Vector3.down);
            Debug.DrawRay(ray.origin, ray.direction, Color.red);

                    if (Physics.Raycast(ray, out hitinfo, 30, walkLayerMask.value))
                    {
                        RightFootIk.MoveToPosition(hitinfo.point, hitinfo.normal,movespeed,StepHeight);
                    }

                    if (RightFootIk.FinishMove)
                    {
                        prepos = rayorigin;
                        legsMove = !legsMove;
                        LetRightMove = false;
                        prePlane.x = rayorigin.x;
                        prePlane.y = rayorigin.z;
                //indexpath = (indexpath + 1) >= AiAgent.path.corners.Length ? (AiAgent.path.corners.Length - 1) : indexpath;
            }
                }
    

        

            




    }

    
}
