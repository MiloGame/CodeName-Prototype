using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CanPickUpObject : MonoBehaviour
{
    public UIDocument UiDocument;
    private VisualElement rootElement;
    public LayerMask DeMask;
    [SerializeField]private Transform followTransform;
    private Camera maincamera;
    public MeshRenderer[] MeshRenderers;
    public float intensity;
    public float horizonOffset;
    public float Verticleoffset;
    private bool IsFirstPick;
    void Start()
    {
        IsFirstPick = true;
        maincamera =Camera.main;
        rootElement = UiDocument.rootVisualElement;
        rootElement.style.display = DisplayStyle.None;
        MeshRenderers = GetComponentsInChildren<MeshRenderer>();
    }



    public void Drop()
    {
        followTransform = null;
        IsFirstPick = true;
    }
    public void SetFollowTarget(Transform target)
    {
        followTransform = target;
    }
    public void SetBarPos()
    {
        //Debug.Log("SetBarPos");
        rootElement.style.display = DisplayStyle.Flex;
        Vector2 newPos = RuntimePanelUtils.CameraTransformWorldToPanel(rootElement.panel,
            transform.position+Vector3.up*Verticleoffset+Vector3.right*horizonOffset , maincamera);
        rootElement.transform.position = new Vector2(newPos.x , newPos.y );
    } 
    public void SetBarInvisible()
    {
        //Debug.Log("SetBarPos");
        rootElement.style.display = DisplayStyle.None;

    }
    // Update is called once per frame
    void Update()
    {
        
        //SetBarPos();
        if (followTransform!=null)
        {
            transform.forward = followTransform.forward;
            if (IsFirstPick)
            {
                transform.position = Vector3.Lerp(transform.position, followTransform.position, 5f * Time.deltaTime);
                IsFirstPick = false;
            }
            else
            {
                transform.position = followTransform.position;
            }
        }
        else
        {
            RaycastHit hit;
            Ray ray = new Ray(transform.position, Vector3.down);
            //Debug.DrawRay(ray.origin, ray.direction);


            if (Physics.Raycast(ray, out hit, 1000, DeMask))
            {
                //Debug.Log("hit transform" + hit.point);

                transform.position = Vector3.Lerp(transform.position, hit.point, 8f * Time.deltaTime);


            }
        }
        //SetBarInvisible();

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        SetBarInvisible();

    }
}
