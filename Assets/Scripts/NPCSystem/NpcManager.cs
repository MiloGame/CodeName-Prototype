using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class NpcManager : MonoBehaviour
{
    public TextAsset Dialog;
    public NPCUImanager NpcuImanager;
    public LayerMask DeMask;
    public LayerMask PlayerMask;
    public int ID;
    public Animator Animator;
  
    public string gethit;
    public string getrescue;
    public string talking;
    public string Turning;

    private CustomNPCTEventData dialogNpctEventData;
    public bool IsTalking;

    private Quaternion originalRot;
    public Transform PlayerTransform ;
    private bool ShouldShowTaskLabel;
    private float detectplayerradius;

    // Start is called before the first frame update
    void Start()
    {
        originalRot = transform.rotation;
        dialogNpctEventData = new CustomNPCTEventData();
        dialogNpctEventData.NPCID = ID;
        dialogNpctEventData.DialogTextAsset = Dialog;
        IsTalking = false;
        EventBusManager.Instance.ParamScribe(EventBusManager.EventType.DialogFinish,OnDialogFinish);
        EventBusManager.Instance.ParamScribe(EventBusManager.EventType.DialogNPCID, OnNeedTalk);
        ShouldShowTaskLabel = false;
        gethit = "GetHit";
        getrescue = "GetHitStop";
        talking = "IsTalking";
        Turning = "Turning";
        detectplayerradius = 10f;
    }

    void OnDestroy()
    {
        EventBusManager.Instance.ParamUnScribe(EventBusManager.EventType.DialogFinish, OnDialogFinish);
        EventBusManager.Instance.ParamUnScribe(EventBusManager.EventType.DialogNPCID, OnNeedTalk);
    }

    private void OnNeedTalk(object sender, EventArgs e)
    {
        var data = e as CustomTEventData<int>;
        if (data != null)
        {
            int targetid = data.message;
            if (targetid == ID)
            {
                ShouldShowTaskLabel = true;
            }
        }
    }

    private void OnDialogFinish(object sender, EventArgs e)
    {
        var data = e as CustomNPCTEventData;
        if (data!=null)
        {
            if (data.NPCID == ID)
            {
                ShouldShowTaskLabel = false;
            }
            IsTalking = false;
            Animator.SetBool(talking, false);
        }
        

    }

    // Update is called once per frame
    void Update()
    {
        Gravity();
        if (IsTalking)
        {
            ShouldShowTaskLabel = false;
            //Debug.Log("do dialog");
            EventBusManager.Instance.ParamPublish(EventBusManager.EventType.DoDialogk,this,dialogNpctEventData);
            var targetRot = Quaternion.LookRotation(PlayerTransform.position - transform.position);
            if (Quaternion.Angle(transform.rotation, targetRot) <= 0.01f)
            {
                Animator.SetBool(Turning,false);
                Animator.SetBool(talking, true);

            }
            else
            {
                Animator.SetBool(Turning,true);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, 5f * Time.deltaTime);

            }

        }
        else
        {
            if (Quaternion.Angle(transform.rotation, originalRot) <= 0.01f)
            {
                Animator.SetBool(Turning, false);
            }
            else
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, originalRot, 5f * Time.deltaTime);

                Animator.SetBool(Turning, true);
            }
        }

        
        if (ShouldShowTaskLabel && Physics.OverlapSphere(transform.position,detectplayerradius, PlayerMask).Length!=0)
        {
            NpcuImanager.SetTaskIconPos();
        }
        else
        {
            NpcuImanager.DisableTaskIcon();
        }
    }

    private void Gravity()
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

    public void ShowPressF()
    {
        NpcuImanager.SetFBarPos();
    }

    public void OnHit(float gunModelBulletdamage)
    {
        Debug.Log("npc hit");
        Animator.SetTrigger(gethit);
        StartCoroutine(ReturnIdle());
    }

    private IEnumerator ReturnIdle()
    {
        yield return new WaitForSeconds(2f);

        Animator.SetTrigger(getrescue);
    }
}
