
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
[Serializable]
public class PortInfo
{
    public string Portguid;
    public string ParamName;
    public float PortPositionX;
    public float PortPositionY;
    public string DataTypeFullName; // 存储类型的全名
    public string connectToPortGuid;
}


[Serializable]
public class SkillBaseNode 
{
    public enum SkillNodeAllState
    {
        Running,
        Failure,
        Success
    }
    public enum TreeNodeType
    {
        DebugNode, 
        SkillSecquenceNode,
        SkillForEachNode,
        SkillInverterNode,
        SkillRepeateNode,
        SkillRootNode,
        SkillWaitNode,
        SkillSingeClick,
        GetHurtNode,
        GetHealNode,
        SkillLongPressNode,
        SheldSkillNode,
        SkillFunIfNode,
        ArmGrenadeNode,
        DisArmGrenadeNode,
        SkillFunDoubleIfNode,
        EarthShatterNode,
        AimWallNode,
        ControlEnemyNode
    }
    public SkillNodeAllState State = SkillNodeAllState.Running;
    public bool IsExcuted = false;
    public string Titlename;
    public float Width =700f;
    public float Height = 500f;
    public float Positionx;
    public float Positiony;
    public TreeNodeType Nodetype;
    public int Inputportnums=0;
    public int Outputportnums=0;
    public List<PortInfo> InportsInfo=new List<PortInfo>();
    public List<PortInfo> OutportsInfo=new List<PortInfo>();
    public string nodeGuid;
    ////反射获取属性用于端口绑定
    //public object Reflection(string paramsName)
    //{
    //    Type m = this.GetType();
    //    FieldInfo x = m.GetField(paramsName);
    //    object elem = x.GetValue(m);
    //    Debug.Log(m+"reflect param"+elem);
    //    return elem;
    //}

    public void ChangeInportsInfo()
    {
       // Debug.Log("ChangeInportsInfo called");
        while (Inputportnums!=InportsInfo.Count)
        {
            Debug.Log("ChangeInportsInfo while called");
            if (Inputportnums > InportsInfo.Count)
            {
                Debug.Log("create new portinfo while called");
                PortInfo tmp = new PortInfo();
                tmp.ParamName = "";
                tmp.Portguid = Guid.NewGuid().ToString();
                tmp.DataTypeFullName = "bool";
                InportsInfo.Add(tmp);
            }
            else if (Inputportnums < InportsInfo.Count)
            {
                Debug.Log("delete portinfo while called");

                InportsInfo.RemoveAt(InportsInfo.Count - 1);
            }
        }
    }
    public void ChangeOutportsInfo()
    {
        while (Outputportnums != OutportsInfo.Count)
        {
            if (Outputportnums > OutportsInfo.Count)
            {
                PortInfo tmp = new PortInfo();
                tmp.ParamName = "";
                tmp.Portguid = Guid.NewGuid().ToString();
                tmp.DataTypeFullName = "bool";
                OutportsInfo.Add(tmp);
            }
            else if (Outputportnums < OutportsInfo.Count)
            {
                OutportsInfo.RemoveAt(OutportsInfo.Count - 1);
            }
        }
    }

    public SkillNodeAllState UpdateState()
    {
        FuncInterface nodeInterface = this as FuncInterface;
        if (nodeInterface != null)
        {
            if (!IsExcuted)
            {
                nodeInterface.OnStart();
                IsExcuted = true;
            }

            State = nodeInterface.OnUpdate();

            if (State == SkillBaseNode.SkillNodeAllState.Failure || State == SkillBaseNode.SkillNodeAllState.Success)
            {
                nodeInterface.OnStop();
                IsExcuted = false;
            }
        }

        return State;
    }
}
