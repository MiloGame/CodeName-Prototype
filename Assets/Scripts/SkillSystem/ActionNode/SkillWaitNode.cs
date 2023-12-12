
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SkillWaitNode : SkillComposeNode, FuncInterface
{
    public float WaitTime = 2f;
    public bool CanTrigger;
    public bool CanCountDown;
    public bool ArriveTime ;
    public bool OutPutStatus ;
    public enum SkillTypeVM
    {
        QSkill,
        ESkill,
        HSkill,
        JSkill
    }

    private Dictionary<SkillTypeVM, EventBusManager.EventType> MVVMDic;
    private CustomTEventData<float> mvvmEventData;
    public SkillWaitNode()
    {
        MVVMDic = new Dictionary<SkillTypeVM, EventBusManager.EventType>();
        MVVMDic[SkillTypeVM.QSkill] = EventBusManager.EventType.QCoolen;
        MVVMDic[SkillTypeVM.ESkill] = EventBusManager.EventType.ECoolen;
        MVVMDic[SkillTypeVM.HSkill] = EventBusManager.EventType.HCoolen;
        MVVMDic[SkillTypeVM.JSkill] = EventBusManager.EventType.JCoolen;
        mvvmEventData = new CustomTEventData<float>();
    }
    public SkillTypeVM SkillType;
    [SerializeField] private float _starttime;
    public void OnStart()
    {
        ArriveTime = false;
        _starttime = Time.time;
    }

    public void OnStop()
    {

    }

    public SkillNodeAllState OnUpdate()
    {
        OutPutStatus = !CanCountDown;
        if (CanTrigger)
        {
            var childnode = ChildrenNodes[0];
            FuncInterface nodeInterface = childnode as FuncInterface;
            if (nodeInterface != null)
            {
                nodeInterface.UpdateState();
            }

            if (ArriveTime==false)
            {
                CanCountDown = true;
            }
            
            //Debug.Log("start count "+Time.time);
            return SkillNodeAllState.Running;
        }
        ArriveTime = false;

        if (CanCountDown)
        {
            //float remaintime = 
            mvvmEventData.message = Mathf.Clamp(WaitTime + _starttime - Time.time, 0,WaitTime );
            EventBusManager.Instance.ParamPublish(MVVMDic[SkillType],this,mvvmEventData);

            if (Time.time - _starttime > WaitTime)
            {
                //Debug.Log("time arrive"+Time.time);
                CanCountDown = false;
                ArriveTime = true;
                return SkillBaseNode.SkillNodeAllState.Success;
            }
           
            
            var childnode = ChildrenNodes[0];
            FuncInterface nodeInterface = childnode as FuncInterface;
            if (nodeInterface != null)
            {
                nodeInterface.UpdateState();
            }

            return SkillNodeAllState.Running;
        }
        else
        {
            var childnode = ChildrenNodes[0];
            FuncInterface nodeInterface = childnode as FuncInterface;
            if (nodeInterface != null)
            {
                nodeInterface.UpdateState();
            }

            return SkillNodeAllState.Failure;
        }



    }
}
