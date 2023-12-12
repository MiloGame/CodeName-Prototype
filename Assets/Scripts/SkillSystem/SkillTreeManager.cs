using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class SkillTreeManager 
{
    public SkillBaseNode SkillRunningNode;
    public SkillBaseNode.SkillNodeAllState SkillTreeRunningState;
    public string name;
    public struct ReceviePortData
    {
        public string paramN;
        public FieldInfo ReFieldInfo;
        public object instance;
        public SkillBaseNode portAttchNode;
    }
    public struct SendPortData
    {
        public object instance;
        public SkillBaseNode portAttchNode;
    }
    public SkillTreeData TreeData;
    public Dictionary<string, SendPortData> sendportMappings = new Dictionary<string, SendPortData>();
    public Dictionary<string, ReceviePortData> receiveportMappings =
        new Dictionary<string, ReceviePortData>();

    public SkillTreeManager(string filename)
    {
        name = filename;
        LoadTreeData(filename);
    }
    // Start is called before the first frame update
    public void Init()
    {
        //var log1Node= new DebugNode();
        //log1Node.message = "print one";
        //var log2Node= new DebugNode();
        //log2Node.message = "print two";
        //var log3Node= new DebugNode();
        //log3Node.message = "print three";
        //var rep = new SkillRepeateNode();
        //var foreachNode = new SkillForEachNode();
        //var inv = new SkillInverterNode();
        //inv.ChildNode = log2Node;
        //rep.ChildNode = foreachNode;
        //foreachNode.ChildrenNodes.Add(log1Node);
        //foreachNode.ChildrenNodes.Add(inv);
        //foreachNode.ChildrenNodes.Add(log3Node);
        //SkillRunningNode = rep;
        SkillBaseNode rootNode = TreeData.NodesData.FirstOrDefault(n => n.Nodetype == SkillBaseNode.TreeNodeType.SkillRootNode);
        SkillRunningNode = rootNode;
    }

    public void LoadTreeData(String filename)
    {
        string Filepath = Path.Combine(Application.dataPath, "JsonData", filename+".json");
        JsonSerializerSettings settings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All
        };
        string Jsonload = File.ReadAllText(Filepath);
        TreeData = JsonConvert.DeserializeObject<SkillTreeData>(Jsonload, settings);

        //skillTreeData = JsonConvert.DeserializeObject<SkillTreeData>(Jsonload);
        foreach (var elem in TreeData.NodesData)
        {
            ////Debug.Log($"read from json elem type{elem.GetType()}");
            ReConnectTreedata(elem);
            //Debug.Log(elem.ToString());
            switch (elem.Nodetype)
            {
                case SkillBaseNode.TreeNodeType.DebugNode:
                    CreateSendPortsMap<DebugNode>(elem as DebugNode);
                    CreateReceivePortsMap<DebugNode>(elem as DebugNode);
                    break;
                case SkillBaseNode.TreeNodeType.SkillSecquenceNode:
                    CreateSendPortsMap<SkillSecquenceNode>(elem as SkillSecquenceNode);
                    CreateReceivePortsMap<SkillSecquenceNode>(elem as SkillSecquenceNode);
                    break;
                case SkillBaseNode.TreeNodeType.SkillForEachNode:
                    CreateSendPortsMap<SkillForEachNode>(elem as SkillForEachNode);
                    CreateReceivePortsMap<SkillForEachNode>(elem as SkillForEachNode);
                    break;
                case SkillBaseNode.TreeNodeType.SkillInverterNode:
                    CreateSendPortsMap<SkillInverterNode>(elem as SkillInverterNode);
                    CreateReceivePortsMap<SkillInverterNode>(elem as SkillInverterNode);
                    break;
                case SkillBaseNode.TreeNodeType.SkillRepeateNode:
                    CreateSendPortsMap<SkillRepeateNode>(elem as SkillRepeateNode);
                    CreateReceivePortsMap<SkillRepeateNode>(elem as SkillRepeateNode);
                    break;
                case SkillBaseNode.TreeNodeType.SkillRootNode:
                    CreateSendPortsMap<SkillRootNode>(elem as SkillRootNode);
                    CreateReceivePortsMap<SkillRootNode>(elem as SkillRootNode);
                    break;
                case SkillBaseNode.TreeNodeType.SkillWaitNode:
                    CreateSendPortsMap<SkillWaitNode>(elem as SkillWaitNode);
                    CreateReceivePortsMap<SkillWaitNode>(elem as SkillWaitNode);
                    break;
                case SkillBaseNode.TreeNodeType.SkillSingeClick:
                    CreateSendPortsMap<SkillSingeClick>(elem as SkillSingeClick);
                    CreateReceivePortsMap<SkillSingeClick>(elem as SkillSingeClick);
                    break;
                case SkillBaseNode.TreeNodeType.GetHurtNode:
                    CreateSendPortsMap<GetHurtNode>(elem as GetHurtNode);
                    CreateReceivePortsMap<GetHurtNode>(elem as GetHurtNode);
                    break;
                case SkillBaseNode.TreeNodeType.GetHealNode:
                    CreateSendPortsMap<GetHealNode>(elem as GetHealNode);
                    CreateReceivePortsMap<GetHealNode>(elem as GetHealNode);
                    break;
                case SkillBaseNode.TreeNodeType.SkillLongPressNode:
                    CreateSendPortsMap<SkillLongPressNode>(elem as SkillLongPressNode);
                    CreateReceivePortsMap<SkillLongPressNode>(elem as SkillLongPressNode);
                    break;
                case SkillBaseNode.TreeNodeType.SheldSkillNode:
                    CreateSendPortsMap<SheldSkillNode>(elem as SheldSkillNode);
                    CreateReceivePortsMap<SheldSkillNode>(elem as SheldSkillNode);
                    break;
                case SkillBaseNode.TreeNodeType.SkillFunIfNode:
                    CreateSendPortsMap<SkillFunIfNode>(elem as SkillFunIfNode);
                    CreateReceivePortsMap<SkillFunIfNode>(elem as SkillFunIfNode);
                    break;
                case SkillBaseNode.TreeNodeType.ArmGrenadeNode:
                    CreateSendPortsMap<ArmGrenadeNode>(elem as ArmGrenadeNode);
                    CreateReceivePortsMap<ArmGrenadeNode>(elem as ArmGrenadeNode);
                    break;
                case SkillBaseNode.TreeNodeType.DisArmGrenadeNode:
                    CreateSendPortsMap<DisArmGrenadeNode>(elem as DisArmGrenadeNode);
                    CreateReceivePortsMap<DisArmGrenadeNode>(elem as DisArmGrenadeNode);
                    break;
                case SkillBaseNode.TreeNodeType.SkillFunDoubleIfNode:
                    CreateSendPortsMap<SkillFunDoubleIfNode>(elem as SkillFunDoubleIfNode);
                    CreateReceivePortsMap<SkillFunDoubleIfNode>(elem as SkillFunDoubleIfNode);
                    break;
                case SkillBaseNode.TreeNodeType.EarthShatterNode:
                    CreateSendPortsMap<EarthShatterNode>(elem as EarthShatterNode);
                    CreateReceivePortsMap<EarthShatterNode>(elem as EarthShatterNode);
                    break;
                case SkillBaseNode.TreeNodeType.AimWallNode:
                    CreateSendPortsMap<AimWallNode>(elem as AimWallNode);
                    CreateReceivePortsMap<AimWallNode>(elem as AimWallNode);
                    break;

                case SkillBaseNode.TreeNodeType.ControlEnemyNode:
                    CreateSendPortsMap<ControlEnemyNode>(elem as ControlEnemyNode);
                    CreateReceivePortsMap<ControlEnemyNode>(elem as ControlEnemyNode);
                    break;

            }
        }
    }

    private void CreateSendPortsMap<T>(T sendnode) where T:SkillBaseNode
    {
        for (int j = 0; j < sendnode.OutportsInfo.Count; j++)
        {
            PortInfo sendPortInfo = sendnode.OutportsInfo[j];
            var sendinstancetextfield = sendnode.GetType().GetField(sendPortInfo.ParamName);
            if (sendinstancetextfield!=null)
            {
                var sendmessageinstance = sendinstancetextfield.GetValue(sendnode);
    
             //   //Debug.Log($"sendinstancetextfield: {sendinstancetextfield}, sendmessageinstance: {(sendmessageinstance != null ? sendmessageinstance.ToString() : "null")}");
                if (sendmessageinstance!=null&&sendportMappings.ContainsKey(sendPortInfo.connectToPortGuid)!=true)
                {
                    var tmp = new SendPortData();
                    tmp.instance = sendmessageinstance;
                    tmp.portAttchNode = sendnode;
                    sendportMappings[sendPortInfo.connectToPortGuid] = tmp;
                    //Debug.Log($"createsendport{sendmessageinstance}");
                }else if (sendportMappings.ContainsKey(sendPortInfo.connectToPortGuid)==true)
                {
                    var changedict = sendportMappings[sendPortInfo.connectToPortGuid];
                    changedict.instance = sendmessageinstance;
                    changedict.portAttchNode = sendnode;
                    sendportMappings[sendPortInfo.connectToPortGuid] = changedict;
                }
                
            }
                
        }
        
    }

    private void CreateReceivePortsMap<T>(T revnode) where T : SkillBaseNode
    {
        for (int j = 0; j < revnode.InportsInfo.Count; j++)
        {
            PortInfo revPortInfo = revnode.InportsInfo[j];
            var revinstancetextfield = revnode.GetType().GetField(revPortInfo.ParamName);
            ////Debug.Log($"revPortInfo.ParamName{revPortInfo.ParamName}revinstancetextfield==null{revinstancetextfield == null}{revinstancetextfield}");
            //var mmmmm = revnode.GetType().GetFields();
            //foreach (var info in mmmmm)
            //{
            //    //Debug.Log(revnode.Titlename + info);
            //}
            if (revinstancetextfield != null)
            {
                var revmessageinstance = revinstancetextfield.GetValue(revnode);
                //Debug.Log($"revinstancetextfield{revinstancetextfield}revmessageinstance==null{revmessageinstance==null}{revmessageinstance.ToString()}");

                if (revmessageinstance != null && receiveportMappings.ContainsKey(revPortInfo.Portguid) != true)
                {
                    var tmp = new ReceviePortData();
                    tmp.ReFieldInfo = revinstancetextfield;
                    tmp.paramN = revPortInfo.ParamName;
                    tmp.instance = revnode;
                    tmp.portAttchNode = revnode;
                    receiveportMappings[revPortInfo.Portguid] =tmp ;
                    //Debug.Log($"createreceport{revinstancetextfield.GetValue(revnode)}");
                }
                else if (receiveportMappings.ContainsKey(revPortInfo.Portguid) == true)
                {
                    var changedict = receiveportMappings[revPortInfo.Portguid];
                    changedict.ReFieldInfo = revinstancetextfield;
                    changedict.paramN = revPortInfo.ParamName;
                    changedict.instance = revnode;
                    changedict.portAttchNode = revnode;
                    receiveportMappings[revPortInfo.Portguid] = changedict;
                }

            }

        }

    }

    private void ReConnectPortdata()
    {
        for (int i = 0; i < sendportMappings.Count; i++)
        {
            string sendkey = sendportMappings.Keys.ElementAt(i);
         //   //Debug.Log("sendkey"+i+":"+ sendkey);
            var sendref = sendportMappings[sendkey];
            for (int j = 0; j < receiveportMappings.Count; j++)
            {
                string revkey = receiveportMappings.Keys.ElementAt(j);
              //  //Debug.Log("revkey" + j + ":" + revkey);
                var revref = receiveportMappings[sendkey];
                if (revkey==sendkey)
                {
                    var revi = revref.ReFieldInfo;
                    revi.SetValue(revref.instance,sendref.instance);
                  //  //Debug.Log($"sendref{sendref}revi{revi}instance{revref.instance}param{revref.paramN}revmessage{revi.GetValue(revref.instance)}");
                }
                //else
                //{
                //    //Debug.Log("ReConnectPortdata fail"+ "revkey"+revkey + "sendkey" + sendkey);
                //}
            }
        }
        //foreach (KeyValuePair< string, object> entry  in receiveportMappings)
        //{
        //    //Debug.Log($"receiveportMappings key {entry.Key}value{entry.Value}");
        //}
        //foreach (KeyValuePair< string, object> entry  in sendportMappings)
        //{
        //    //Debug.Log($"sendportMappings key {entry.Key}value{entry.Value}");
        //}
    }

    private void ReConnectTreedata(SkillBaseNode elem)    
    {
        SkillDecorateNode decoparentNode = elem as SkillDecorateNode;
        if (decoparentNode != null)
        {
            //int childindex = treeData.nodesData.IndexOf(decoparentNode.ChildNode);
            if (decoparentNode.ChildNode != null)
            {
                SkillBaseNode nodeToRecon = TreeData.NodesData.FirstOrDefault(node => node.nodeGuid == decoparentNode.ChildNode.nodeGuid);
                decoparentNode.ChildNode = nodeToRecon;
            }
            else
            {
                decoparentNode.ChildNode = null;
            }
        }
        SkillComposeNode compparentNode = elem as SkillComposeNode;
        if (compparentNode != null)
        {
            for (int i = 0; i < compparentNode.ChildrenNodes.Count; i++)
            {
                if (compparentNode.ChildrenNodes[i] != null)
                {
                    SkillBaseNode nodeToRecon = TreeData.NodesData.FirstOrDefault(node => node.nodeGuid == compparentNode.ChildrenNodes[i].nodeGuid);
                    compparentNode.ChildrenNodes[i] = nodeToRecon;
                }
                else
                {
                    compparentNode.ChildrenNodes[i] = null;
                }

            }
        }
        SkillRootNode rootpareNode = elem as SkillRootNode;
        if (rootpareNode != null)
        {
            if (rootpareNode.ChildNode != null)
            {
                SkillBaseNode nodeToRecon = TreeData.NodesData.FirstOrDefault(node => node.nodeGuid == rootpareNode.ChildNode.nodeGuid);
                rootpareNode.ChildNode = nodeToRecon;
            }
            else
            {
                rootpareNode.ChildNode = null;
            }

        }
    }

    // Update is called once per frame
    public void Fresh()
    {
        UpdateState();
        ReFreshSendPortMap();
        //ReFreshRevPortMap();
        ReConnectPortdata();
        
    }

    private void ReFreshSendPortMap()
    {
        for (int i = 0; i < sendportMappings.Count; i++)
        {
            string sendkey = sendportMappings.Keys.ElementAt(i);
            var sendref = sendportMappings[sendkey];
            switch (sendref.portAttchNode.Nodetype)
            {
                case SkillBaseNode.TreeNodeType.DebugNode:
                    CreateSendPortsMap<DebugNode>(sendref.portAttchNode as DebugNode);
                    break;
                case SkillBaseNode.TreeNodeType.SkillSecquenceNode:
                    CreateSendPortsMap<SkillSecquenceNode>(sendref.portAttchNode as SkillSecquenceNode);
                    break;
                case SkillBaseNode.TreeNodeType.SkillForEachNode:
                    CreateSendPortsMap<SkillForEachNode>(sendref.portAttchNode as SkillForEachNode);
                    break;
                case SkillBaseNode.TreeNodeType.SkillInverterNode:
                    CreateSendPortsMap<SkillInverterNode>(sendref.portAttchNode as SkillInverterNode);
                    break;
                case SkillBaseNode.TreeNodeType.SkillRepeateNode:
                    CreateSendPortsMap<SkillRepeateNode>(sendref.portAttchNode as SkillRepeateNode);
                    break;
                case SkillBaseNode.TreeNodeType.SkillRootNode:
                    CreateSendPortsMap<SkillRootNode>(sendref.portAttchNode as SkillRootNode);
                    break;
                case SkillBaseNode.TreeNodeType.SkillWaitNode:
                    CreateSendPortsMap<SkillWaitNode>(sendref.portAttchNode as SkillWaitNode);
                    break;
                case SkillBaseNode.TreeNodeType.SkillSingeClick:
                    CreateSendPortsMap<SkillSingeClick>(sendref.portAttchNode as SkillSingeClick);
                    break;
                case SkillBaseNode.TreeNodeType.GetHurtNode:
                    CreateSendPortsMap<GetHurtNode>(sendref.portAttchNode as GetHurtNode);
                    break;
                case SkillBaseNode.TreeNodeType.GetHealNode:
                    CreateSendPortsMap<GetHealNode>(sendref.portAttchNode as GetHealNode);
                    break;
                case SkillBaseNode.TreeNodeType.SkillLongPressNode:
                    CreateSendPortsMap<SkillLongPressNode>(sendref.portAttchNode as SkillLongPressNode);
                    break;
                case SkillBaseNode.TreeNodeType.SheldSkillNode:
                    CreateSendPortsMap<SheldSkillNode>(sendref.portAttchNode as SheldSkillNode);
                    break;
                case SkillBaseNode.TreeNodeType.SkillFunIfNode:
                    CreateSendPortsMap<SkillFunIfNode>(sendref.portAttchNode as SkillFunIfNode);

                    break;
                case SkillBaseNode.TreeNodeType.ArmGrenadeNode:
                    CreateSendPortsMap<ArmGrenadeNode>(sendref.portAttchNode as ArmGrenadeNode);
                    break;
                case SkillBaseNode.TreeNodeType.DisArmGrenadeNode:
                    CreateSendPortsMap<DisArmGrenadeNode>(sendref.portAttchNode as DisArmGrenadeNode);
                    break;
                case SkillBaseNode.TreeNodeType.SkillFunDoubleIfNode:
                    CreateSendPortsMap<SkillFunDoubleIfNode>(sendref.portAttchNode as SkillFunDoubleIfNode);

                    break;
                case SkillBaseNode.TreeNodeType.EarthShatterNode:
                    CreateSendPortsMap<EarthShatterNode>(sendref.portAttchNode as EarthShatterNode);

                    break;
                case SkillBaseNode.TreeNodeType.AimWallNode:
                    CreateSendPortsMap<AimWallNode>(sendref.portAttchNode as AimWallNode);
                    break;

                case SkillBaseNode.TreeNodeType.ControlEnemyNode:
                    CreateSendPortsMap<ControlEnemyNode>(sendref.portAttchNode as ControlEnemyNode);
                    break;

            }
        }
    }



    public SkillBaseNode.SkillNodeAllState UpdateState()
    {
        if (SkillTreeRunningState == SkillBaseNode.SkillNodeAllState.Running)
        {
            SkillTreeRunningState = SkillRunningNode.UpdateState();
        }
        return SkillTreeRunningState;
    }
}
