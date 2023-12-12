using System;
using System.Collections.Generic;
using System.Reflection;
using Codice.CM.Common;
using Codice.CM.Common.Serialization;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using UnityEditor;
using UnityEngine;
using Edge = UnityEditor.Experimental.GraphView.Edge;

public class SkillViewNode : UnityEditor.Experimental.GraphView.Node
{
    public SkillBaseNode NodeToView;
    public Dictionary<string, Type> TransTypes=new Dictionary<string, Type>();
    public Port LinkInPort;
    public Port LinkOutPort;
    public SkillGraphView Skillview;
    public SkillViewNode(SkillBaseNode node)
    {
        NodeToView = node;
        this.title = node.Titlename;
        this.viewDataKey = node.nodeGuid;
        InitTransDic();
        CreateInputPorts();
        CreateOutputPorts();
       // CutAllLinks();
       // RefreshLinks();
    }

    public void ReFreshData(SkillBaseNode node)
    {
        NodeToView = node;
        this.title = node.Titlename;
        this.viewDataKey = node.nodeGuid;
        this.SetPosition(new Rect(NodeToView.Positionx, NodeToView.Positiony,
            NodeToView.Width, NodeToView.Height));
        InitTransDic();
        CreateInputPorts();
        CreateOutputPorts();
        CutAllLinks();
        RefreshLinks();

    }
    
    public void CutAllLinks()
    {
        if (NodeToView is SkillComposeNode || NodeToView is SkillDecorateNode || NodeToView is SkillRootNode)
        {
            for (int i = 1; i < outputContainer.childCount; i++)
            {
                Port outputPort = outputContainer[i] as Port;
                outputPort.DisconnectAll();
            }
        }
        else
        {
            for (int i = 0; i < outputContainer.childCount; i++)
            {
                Port outputPort = outputContainer[i] as Port;
                outputPort.DisconnectAll();
            }
        }
    }
    private Port FindConInPort(string elemguid)
    {
        return Skillview.GetPortByGuid(elemguid) as Port;
    }
    
    public void RefreshLinks()
    {
        if (NodeToView is SkillComposeNode || NodeToView is SkillDecorateNode || NodeToView is SkillRootNode)
        {
            for (int i = 1; i < outputContainer.childCount; i++)
            {
                Port outputPort = outputContainer[i] as Port;
                if (outputPort != null)
                {
                    Port inputPort = FindConInPort(NodeToView.OutportsInfo[i - 1].connectToPortGuid);
                    if (inputPort!=null)
                    {
                        Edge edge = outputPort.ConnectTo(inputPort);
                        edge.userData = edge.output.userData;
                        edge.input.userData = edge.userData;
                        //var childView = edge.input.node as SkillViewNode;
                        //childView.ReFreshData(childView.NodeToView);
                        Skillview.AddElement(edge);
                        Debug.Log("links two point"+edge.input.node.title+edge.output.node.title);
                        Debug.Log("RefreshLinks output" + edge.output.userData + edge.output.connected + "input" + edge.input.userData + edge.input.connected + "edge" + edge.userData);
                    }
                }
            }
        }
        else
        {
            for (int i = 0; i < outputContainer.childCount; i++)
            {
                Port outputPort = outputContainer[i] as Port;
                if (outputPort != null)
                {
                    Port inputPort = FindConInPort(NodeToView.OutportsInfo[i].connectToPortGuid);
                    if (inputPort != null)
                    {
                        Edge edge = outputPort.ConnectTo(inputPort);
                        edge.userData = edge.output.userData;
                        edge.input.userData = edge.userData;
                        //var childView = edge.input.node as SkillViewNode;
                        //childView.ReFreshData(childView.NodeToView);
                        Skillview.AddElement(edge);
                        Debug.Log("links two point" + edge.input.node.title + edge.output.node.title);

                        Debug.Log("RefreshLinks output" + edge.output.userData + edge.output.connected + "input" + edge.input.userData + edge.input.connected + "edge" + edge.userData);
                    }
                }
            }
        }
    }

    private void InitTransDic()
    {
        TransTypes["int"] = typeof(int);
        TransTypes["bool"] = typeof(bool);
        TransTypes["float"] = typeof(float);
        TransTypes["double"] = typeof(double);
        TransTypes["string"] = typeof(string);
        TransTypes["object"] = typeof(object);

    }
    public void ReFreshOutPortRef<T>(T s) where T : SkillBaseNode
    {
        //  Debug.Log($"refreshinportref inputContainer.childCount{inputContainer.childCount}");\
        if (s is SkillComposeNode || s is SkillDecorateNode || s is SkillRootNode)
        {
            for (int i = 1; i < outputContainer.childCount; i++)
            {
                Port outputPort = outputContainer[i] as Port;
                if (outputPort != null)
                {
                    if (TransTypes.ContainsKey(s.OutportsInfo[i - 1].DataTypeFullName))
                    {
                        outputPort.portType = TransTypes[NodeToView.OutportsInfo[i - 1].DataTypeFullName];
                        outputPort.portName = NodeToView.OutportsInfo[i - 1].ParamName;
                        var x = s.GetType().GetField(s.OutportsInfo[i - 1].ParamName);
                        //var mmmmm = s.GetType().GetFields();
                        //foreach (var info in mmmmm)
                        //{
                        //    Debug.Log(s.Titlename + info);
                        //}
                        if (x != null)
                        {
                            var attchObject = x.GetValue(s);
                            outputPort.userData = attchObject;
                            Debug.Log($"ReFreshOutPortRef{s.Titlename} attchObject {attchObject} updated to {outputPort.userData}");

                        }
                        else
                        {
                            Debug.Log($"ReFreshOutPortRef x ==null");

                        }
                    }
                }
            }
        }
        else
        {
            for (int i = 0; i < outputContainer.childCount; i++)
            {
                Port outputPort = outputContainer[i] as Port;
                if (outputPort != null)
                {
                    if (TransTypes.ContainsKey(s.OutportsInfo[i].DataTypeFullName))
                    {
                        outputPort.portType = TransTypes[NodeToView.OutportsInfo[i].DataTypeFullName];
                        outputPort.portName = NodeToView.OutportsInfo[i].ParamName;
                        //反射获取运行实例
                        var x = s.GetType().GetField(s.OutportsInfo[i].ParamName);
                        if (x != null)
                        {
                            var attchObject = x.GetValue(s);
                            outputPort.userData = attchObject;
                            Debug.Log($"{s.Titlename}attchObject{attchObject}");
                        }
                    }
                }
            }
        }
    }
    public void FindSpecialNodeOutput<T>(T special) where T : SkillBaseNode
    {
        Debug.Log("FindSpecialNodeOutput");
        if (special is SkillComposeNode || special is SkillDecorateNode || special is SkillRootNode)
        {
            if (special.Outputportnums + 1 > outputContainer.childCount)
            {

                for (int i = outputContainer.childCount - 1; i < special.Outputportnums; i++)
                {
                    Port outPort;
                    if (TransTypes.ContainsKey(special.OutportsInfo[i].DataTypeFullName))
                    {
                        outPort = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, TransTypes[NodeToView.OutportsInfo[i].DataTypeFullName]);

                        //反射获取运行实例
                        var x = special.GetType().GetField(special.OutportsInfo[i].ParamName);
                        if (x != null)
                        {
                            var attchObject = x.GetValue(special);
                            outPort.userData = attchObject;
                            Debug.Log($"{special.Titlename}attchObject{attchObject}");
                        }

                    }
                    else
                    {
                        outPort = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(bool));

                    }
                    outPort.portName = special.OutportsInfo[i].ParamName;
                    outPort.style.flexDirection = FlexDirection.RowReverse;
                    outPort.style.flexGrow = 1;
                    outPort.viewDataKey = special.OutportsInfo[i].Portguid;
                    outputContainer.Add(outPort);
                }


            }
            else if (special.Outputportnums < outputContainer.childCount - 1)
            {
                Debug.Log("delete portinfo while called");
                while (special.Outputportnums != (outputContainer.childCount - 1))
                {
                    outputContainer.RemoveAt(outputContainer.childCount - 1);
                }

            }
        }
        else
        {
            if (special.Outputportnums > outputContainer.childCount)
            {

                for (int i = outputContainer.childCount ; i < special.Outputportnums; i++)
                {
                    Port outPort;
                    if (TransTypes.ContainsKey(special.OutportsInfo[i].DataTypeFullName))
                    {
                        outPort = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, TransTypes[NodeToView.OutportsInfo[i].DataTypeFullName]);

                        //反射获取运行实例
                        var x = special.GetType().GetField(special.OutportsInfo[i].ParamName);
                        if (x != null)
                        {
                            var attchObject = x.GetValue(special);
                            outPort.userData = attchObject;
                            Debug.Log($"{special.Titlename}attchObject{attchObject}");
                        }

                    }
                    else
                    {
                        outPort = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(bool));

                    }
                    outPort.portName = special.OutportsInfo[i].ParamName;
                    outPort.style.flexDirection = FlexDirection.RowReverse;
                    outPort.style.flexGrow = 1;
                    outPort.viewDataKey = special.OutportsInfo[i].Portguid;
                    outputContainer.Add(outPort);
                }


            }
            else if (special.Outputportnums < outputContainer.childCount )
            {
                Debug.Log("delete portinfo while called");
                while (special.Outputportnums != outputContainer.childCount)
                {
                    outputContainer.RemoveAt(outputContainer.childCount - 1);
                }

            }
        }


        
        //for (int i = 0; i < special.Outputportnums; i++)
        //{
        //    Port outPort;
        //    if (TransTypes.ContainsKey(special.OutportsInfo[i].DataTypeFullName))
        //    {
        //        outPort = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, TransTypes[NodeToView.OutportsInfo[i].DataTypeFullName]);

        //        //反射获取运行实例
        //        var x = special.GetType().GetField(special.OutportsInfo[i].ParamName);
        //        if (x != null)
        //        {
        //            var attchObject = x.GetValue(special);
        //            outPort.userData = attchObject;
        //            Debug.Log($"{special.Titlename}attchObject{attchObject}");
        //        }

        //    }
        //    else
        //    {
        //        outPort = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(bool));

        //    }

        //    outPort.portName = special.OutportsInfo[i].ParamName;
        //    outPort.style.flexDirection = FlexDirection.RowReverse;
        //    outPort.style.flexGrow = 1;
        //    outputContainer.Add(outPort);
        //}
    }
    public void CreateOutputPorts()
    {
        outputContainer.style.minHeight = 100;
        // outputContainer.Clear();
        if (LinkOutPort != null)
        {
            Debug.Log("Find exist linkport");

        }
        else
        {
            Debug.Log("create link out port");
            if (NodeToView is SkillComposeNode)
            {
                //Debug.Log("enter createoutputport is compositenode info" + (nodetoGraph as CompositeNode).GetNodeInfoAsString());

                LinkOutPort = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(bool));
            }
            else if (NodeToView is SkillDecorateNode)
            {
                //Debug.Log("enter createoutputport is DecoratorNode info" + (nodetoGraph as DecoratorNode).GetNodeInfoAsString());

                LinkOutPort = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(bool));
            }
            else if (NodeToView is SkillRootNode)
            {
                //Debug.Log("enter createoutputport is RootNode info" + (nodetoGraph as RootNode).GetNodeInfoAsString());

                LinkOutPort = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Single, typeof(bool));
            }

            if (LinkOutPort != null)
            {
                LinkOutPort.portName = "LinkOutPort";
                LinkOutPort.style.flexDirection = FlexDirection.RowReverse;
                LinkOutPort.style.flexGrow = 1;
            }
            if (NodeToView is SkillComposeNode)
            {
                outputContainer.Add(LinkOutPort);
            }
            else if (NodeToView is SkillDecorateNode)
            {
                outputContainer.Add(LinkOutPort);
            }
            else if (NodeToView is SkillRootNode)
            {
                outputContainer.Add(LinkOutPort);
            }
        }
       
        switch (NodeToView.Nodetype)
        {
            case SkillBaseNode.TreeNodeType.DebugNode:
                FindSpecialNodeOutput<DebugNode>(NodeToView as DebugNode);
                ReFreshOutPortRef<DebugNode>(NodeToView as DebugNode);
                break;
            case SkillBaseNode.TreeNodeType.SkillSecquenceNode:
                FindSpecialNodeOutput<SkillSecquenceNode>(NodeToView as SkillSecquenceNode);
                ReFreshOutPortRef<SkillSecquenceNode>(NodeToView as SkillSecquenceNode);
                break;
            case SkillBaseNode.TreeNodeType.SkillForEachNode:
                FindSpecialNodeOutput<SkillForEachNode>(NodeToView as SkillForEachNode);
                ReFreshOutPortRef<SkillForEachNode>(NodeToView as SkillForEachNode);
                break;
            case SkillBaseNode.TreeNodeType.SkillInverterNode:
                FindSpecialNodeOutput<SkillInverterNode>(NodeToView as SkillInverterNode);
                ReFreshOutPortRef<SkillInverterNode>(NodeToView as SkillInverterNode);
                break;
            case SkillBaseNode.TreeNodeType.SkillRepeateNode:
                FindSpecialNodeOutput<SkillRepeateNode>(NodeToView as SkillRepeateNode);
                ReFreshOutPortRef<SkillRepeateNode>(NodeToView as SkillRepeateNode);
                break;
            case SkillBaseNode.TreeNodeType.SkillRootNode:
                FindSpecialNodeOutput<SkillRootNode>(NodeToView as SkillRootNode);
                ReFreshOutPortRef<SkillRootNode>(NodeToView as SkillRootNode);
                break;
            case SkillBaseNode.TreeNodeType.SkillWaitNode:
                FindSpecialNodeOutput<SkillWaitNode>(NodeToView as SkillWaitNode);
                ReFreshOutPortRef<SkillWaitNode>(NodeToView as SkillWaitNode);
                break;
            case SkillBaseNode.TreeNodeType.SkillSingeClick:
                FindSpecialNodeOutput<SkillSingeClick>(NodeToView as SkillSingeClick);
                ReFreshOutPortRef<SkillSingeClick>(NodeToView as SkillSingeClick);
                break;
            case SkillBaseNode.TreeNodeType.GetHurtNode:
                FindSpecialNodeOutput<GetHurtNode>(NodeToView as GetHurtNode);
                ReFreshOutPortRef<GetHurtNode>(NodeToView as GetHurtNode);
                break;
            case SkillBaseNode.TreeNodeType.GetHealNode:
                FindSpecialNodeOutput<GetHealNode>(NodeToView as GetHealNode);
                ReFreshOutPortRef<GetHealNode>(NodeToView as GetHealNode);
                break;
            case SkillBaseNode.TreeNodeType.SkillLongPressNode:
                FindSpecialNodeOutput<SkillLongPressNode>(NodeToView as SkillLongPressNode);
                ReFreshOutPortRef<SkillLongPressNode>(NodeToView as SkillLongPressNode);
                break;
            case SkillBaseNode.TreeNodeType.SheldSkillNode:
                FindSpecialNodeOutput<SheldSkillNode>(NodeToView as SheldSkillNode);
                ReFreshOutPortRef<SheldSkillNode>(NodeToView as SheldSkillNode);
                break;
            case SkillBaseNode.TreeNodeType.SkillFunIfNode:
                FindSpecialNodeOutput<SkillFunIfNode>(NodeToView as SkillFunIfNode);
                ReFreshOutPortRef<SkillFunIfNode>(NodeToView as SkillFunIfNode);
                break;
            case SkillBaseNode.TreeNodeType.ArmGrenadeNode:
                FindSpecialNodeOutput<ArmGrenadeNode>(NodeToView as ArmGrenadeNode);
                ReFreshOutPortRef<ArmGrenadeNode>(NodeToView as ArmGrenadeNode);
                break;
            case SkillBaseNode.TreeNodeType.DisArmGrenadeNode:
                FindSpecialNodeOutput<DisArmGrenadeNode>(NodeToView as DisArmGrenadeNode);
                ReFreshOutPortRef<DisArmGrenadeNode>(NodeToView as DisArmGrenadeNode);
                break;
            case SkillBaseNode.TreeNodeType.SkillFunDoubleIfNode:
                FindSpecialNodeOutput<SkillFunDoubleIfNode>(NodeToView as SkillFunDoubleIfNode);
                ReFreshOutPortRef<SkillFunDoubleIfNode>(NodeToView as SkillFunDoubleIfNode);
                break;
            case SkillBaseNode.TreeNodeType.EarthShatterNode:
                FindSpecialNodeOutput<EarthShatterNode>(NodeToView as EarthShatterNode);
                ReFreshOutPortRef<EarthShatterNode>(NodeToView as EarthShatterNode);
                break;
            case SkillBaseNode.TreeNodeType.AimWallNode:
                FindSpecialNodeOutput<AimWallNode>(NodeToView as AimWallNode);
                ReFreshOutPortRef<AimWallNode>(NodeToView as AimWallNode);
                break;

            case SkillBaseNode.TreeNodeType.ControlEnemyNode:
                FindSpecialNodeOutput<ControlEnemyNode>(NodeToView as ControlEnemyNode);
                ReFreshOutPortRef<ControlEnemyNode>(NodeToView as ControlEnemyNode);
                break;

        }
        //for (int i = 0; i < NodeToView.Outputportnums; i++)
        //{
        //    Port outPort;
        //    if (TransTypes.ContainsKey(NodeToView.OutportsInfo[i].DataTypeFullName))
        //    {
        //        outPort = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, TransTypes[NodeToView.OutportsInfo[i].DataTypeFullName]);
        //    }
        //    else
        //    {
        //        outPort = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(bool));

        //    }
        //    outPort.portName = NodeToView.OutportsInfo[i].ParamName + " Type: " + outPort.portType;
        //    outPort.style.flexDirection = FlexDirection.RowReverse;
        //    outPort.style.flexGrow = 1;
        //    outputContainer.Add(outPort);
        //}
       
    }

    public void ReFreshInPortRef<T>(T sp) where T : SkillBaseNode
    {
      //  Debug.Log($"refreshinportref inputContainer.childCount{inputContainer.childCount}");
        for (int i = 1; i < inputContainer.childCount; i++)
        {
            Port inputPort = inputContainer[i] as Port;
            if (inputPort!=null)
            {
                if (TransTypes.ContainsKey(sp.InportsInfo[i - 1].DataTypeFullName))
                {
                    inputPort.portType = TransTypes[NodeToView.InportsInfo[i-1].DataTypeFullName];
                    inputPort.portName = NodeToView.InportsInfo[i-1].ParamName;
                }
                //反射获取运行实例
                var x = sp.GetType().GetField(sp.InportsInfo[i-1].ParamName);
                if (x != null)
                {
                    //var attchObject = x.GetValue(sp);
                    //attchObject = inputPort.userData;
                    var instance = sp;
                    var fieldType = x.FieldType;
                    Debug.Log($"{sp.Titlename} {x.Name}{fieldType} updated to {inputPort.userData}");
                    Debug.Log(inputPort.userData);

                    if (inputPort.userData != null)
                    {
                        Debug.Log(inputPort.userData.GetType());
                        Debug.Log(fieldType == typeof(string));
                        Debug.Log(fieldType);
                         Debug.Log(inputPort.userData.GetType() == typeof(string));
                    }
                   
                    if (inputPort.userData != null && fieldType.IsAssignableFrom(inputPort.userData.GetType()))
                    {
                        x.SetValue(instance, inputPort.userData);
                        Debug.Log($"{sp.Titlename} {x.Name} updated to {inputPort.userData}");

                    }
                    else
                    {
                        
                        Debug.LogWarning($"Type mismatch between {x.Name}{fieldType} and inputPort.userData{inputPort.userData}for {sp.Titlename}");

                    }
                    // x.SetValue(sp, inputPort.userData); // 将特定字段的值设置为 inPort.userData 的值 
                    //   Debug.Log($"{sp.Titlename} attchObject {attchObject} updated to {inputPort.userData}");

                }
                else
                {
                    Debug.Log($"{sp.Titlename}X == NULL{x}");
                }
            }
            else
            {
                Debug.LogWarning("refreshinportref inputport is null");
            }
        }
    }
    public void FindSpecialNodeInput<T>(T specialnode) where T : SkillBaseNode
    {
        Debug.Log("FindSpecialNodeInput");
        
       // Debug.Log("ChangeFindSpecialNodeInput while called");
      // Debug.Log("speciak inptnum"+specialnode.Inputportnums);
    //  Debug.Log("inputcontainer"+inputContainer.childCount);
    //   Debug.Log("inputportsinfo"+ specialnode.InportsInfo.Count);
            if (specialnode.Inputportnums+1 > inputContainer.childCount)
            {
                if (specialnode.InportsInfo!=null)
                {
                    for (int i = inputContainer.childCount-1; i < specialnode.Inputportnums; i++)
                    {
                        Port inPort;
                        //if (TransTypes.ContainsKey(specialnode.InportsInfo[i].DataTypeFullName))
                        //{
                        //    inPort = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, TransTypes[NodeToView.InportsInfo[i].DataTypeFullName]);

                        //    ////反射获取运行实例
                        //    //var x = specialnode.GetType().GetField(specialnode.InportsInfo[i].ParamName);
                        //    //if (x != null)
                        //    //{
                        //    //    var attchObject = x.GetValue(specialnode);
                        //    //    attchObject = inPort.userData;

                        //    //    x.SetValue(specialnode, inPort.userData); // 将特定字段的值设置为 inPort.userData 的值 
                        //    //    Debug.Log($"{specialnode.Titlename} attchObject {attchObject} updated to {inPort.userData}");

                        //    //}
                        //    //else
                        //    //{
                        //    //    Debug.Log($"{specialnode.Titlename}X == NULL{x}");
                        //    //}

                        //}
                        //else
                        //{
                        inPort = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(bool));

                        //}
                        inPort.portName = specialnode.InportsInfo[i].ParamName;
                        inPort.style.flexDirection = FlexDirection.Row;
                        inPort.style.flexGrow = 1;
                        inPort.viewDataKey = specialnode.InportsInfo[i].Portguid;
                        inputContainer.Add(inPort);
                    }
                }
                
                
            }
            else if (specialnode.Inputportnums < inputContainer.childCount - 1)
            {
                Debug.Log("delete portinfo while called");
                while (specialnode.Inputportnums != (inputContainer.childCount - 1))
                {
                    inputContainer.RemoveAt(inputContainer.childCount - 1);
                }

            }

        //for (int i = 0; i < specialnode.Inputportnums; i++)
        //{
        //    Port inPort;
        //    if (TransTypes.ContainsKey(specialnode.InportsInfo[i].DataTypeFullName))
        //    {
        //        inPort = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, TransTypes[NodeToView.InportsInfo[i].DataTypeFullName]);

        //        //反射获取运行实例
        //        var x = specialnode.GetType().GetField(specialnode.InportsInfo[i].ParamName);
        //        if (x != null)
        //        {
        //            var attchObject = x.GetValue(specialnode);
        //            //attchObject = inPort.userData ;

        //            x.SetValue(specialnode, inPort.userData); // 将特定字段的值设置为 inPort.userData 的值 
        //            Debug.Log($"{specialnode.Titlename} attchObject {attchObject} updated to {inPort.userData}");

        //        }
        //        else
        //        {
        //            Debug.Log($"{specialnode.Titlename}X == NULL{x}");
        //        }

        //    }
        //    else
        //    {
        //        inPort = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(bool));

        //    }
        //    inPort.portName = specialnode.InportsInfo[i].ParamName;
        //    inPort.style.flexDirection = FlexDirection.Row;
        //    inPort.style.flexGrow = 1;
        //    inputContainer.Add(inPort);
        //}
    }
    public void CreateInputPorts()
    {
        Debug.Log("create input ports");
        inputContainer.style.minHeight = 100;
        //inputContainer.Clear();
        if (LinkInPort != null)
        {
            Debug.Log("Find exist linkport");

        }
        else
        {
            Debug.Log("create link port");
            LinkInPort = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Single, typeof(bool));
            LinkInPort.portName = "LinkInputPort";
            LinkInPort.style.flexDirection = FlexDirection.Row;
            LinkInPort.style.flexGrow = 1;
            if (NodeToView is not SkillRootNode)
                inputContainer.Add(LinkInPort);
        }
        

        switch (NodeToView.Nodetype)
        {
            case SkillBaseNode.TreeNodeType.DebugNode:
                FindSpecialNodeInput<DebugNode>(NodeToView as DebugNode);
                ReFreshInPortRef<DebugNode>(NodeToView as DebugNode);
                break;
            case SkillBaseNode.TreeNodeType.SkillSecquenceNode:
                FindSpecialNodeInput<SkillSecquenceNode>(NodeToView as SkillSecquenceNode);
                ReFreshInPortRef<SkillSecquenceNode>(NodeToView as SkillSecquenceNode);
                break;
            case SkillBaseNode.TreeNodeType.SkillForEachNode:
                FindSpecialNodeInput<SkillForEachNode>(NodeToView as SkillForEachNode);
                ReFreshInPortRef<SkillForEachNode>(NodeToView as SkillForEachNode);
                break;
            case SkillBaseNode.TreeNodeType.SkillInverterNode:
                FindSpecialNodeInput<SkillInverterNode>(NodeToView as SkillInverterNode);
                ReFreshInPortRef<SkillInverterNode>(NodeToView as SkillInverterNode);
                break;
            case SkillBaseNode.TreeNodeType.SkillRepeateNode:
                FindSpecialNodeInput<SkillRepeateNode>(NodeToView as SkillRepeateNode);
                ReFreshInPortRef<SkillRepeateNode>(NodeToView as SkillRepeateNode);
                break;
            case SkillBaseNode.TreeNodeType.SkillRootNode:
             //   FindSpecialNodeInput<SkillRootNode>(NodeToView as SkillRootNode);
                ReFreshInPortRef<SkillRootNode>(NodeToView as SkillRootNode);
                break;
            case SkillBaseNode.TreeNodeType.SkillWaitNode:
                FindSpecialNodeInput<SkillWaitNode>(NodeToView as SkillWaitNode);
                ReFreshInPortRef<SkillWaitNode>(NodeToView as SkillWaitNode);
                break;
            case SkillBaseNode.TreeNodeType.SkillSingeClick:
                FindSpecialNodeInput<SkillSingeClick>(NodeToView as SkillSingeClick);
                ReFreshInPortRef<SkillSingeClick>(NodeToView as SkillSingeClick);
                break;
            case SkillBaseNode.TreeNodeType.GetHurtNode:
                FindSpecialNodeInput<GetHurtNode>(NodeToView as GetHurtNode);
                ReFreshInPortRef<GetHurtNode>(NodeToView as GetHurtNode);
                break;
            case SkillBaseNode.TreeNodeType.GetHealNode:
                FindSpecialNodeInput<GetHealNode>(NodeToView as GetHealNode);
                ReFreshInPortRef<GetHealNode>(NodeToView as GetHealNode);
                break;
            case SkillBaseNode.TreeNodeType.SkillLongPressNode:
                FindSpecialNodeInput<SkillLongPressNode>(NodeToView as SkillLongPressNode);
                ReFreshInPortRef<SkillLongPressNode>(NodeToView as SkillLongPressNode);
                break;
            case SkillBaseNode.TreeNodeType.SheldSkillNode:
                FindSpecialNodeInput<SheldSkillNode>(NodeToView as SheldSkillNode);
                ReFreshInPortRef<SheldSkillNode>(NodeToView as SheldSkillNode);
                break;
            case SkillBaseNode.TreeNodeType.SkillFunIfNode:
                FindSpecialNodeInput<SkillFunIfNode>(NodeToView as SkillFunIfNode);
                ReFreshInPortRef<SkillFunIfNode>(NodeToView as SkillFunIfNode);
                break;
            case SkillBaseNode.TreeNodeType.ArmGrenadeNode:
                FindSpecialNodeInput<ArmGrenadeNode>(NodeToView as ArmGrenadeNode);
                ReFreshInPortRef<ArmGrenadeNode>(NodeToView as ArmGrenadeNode);
                break;
            case SkillBaseNode.TreeNodeType.DisArmGrenadeNode:
                FindSpecialNodeInput<DisArmGrenadeNode>(NodeToView as DisArmGrenadeNode);
                ReFreshInPortRef<DisArmGrenadeNode>(NodeToView as DisArmGrenadeNode);
                break;
            case SkillBaseNode.TreeNodeType.SkillFunDoubleIfNode:
                FindSpecialNodeInput<SkillFunDoubleIfNode>(NodeToView as SkillFunDoubleIfNode);
                ReFreshInPortRef<SkillFunDoubleIfNode>(NodeToView as SkillFunDoubleIfNode);
                break;
            case SkillBaseNode.TreeNodeType.EarthShatterNode:
                FindSpecialNodeInput<EarthShatterNode>(NodeToView as EarthShatterNode);
                ReFreshInPortRef<EarthShatterNode>(NodeToView as EarthShatterNode);
                break;
            case SkillBaseNode.TreeNodeType.AimWallNode:
                FindSpecialNodeInput<AimWallNode>(NodeToView as AimWallNode);
                ReFreshInPortRef<AimWallNode>(NodeToView as AimWallNode);
                break;

            case SkillBaseNode.TreeNodeType.ControlEnemyNode:
                FindSpecialNodeInput<ControlEnemyNode>(NodeToView as ControlEnemyNode);
                ReFreshInPortRef<ControlEnemyNode>(NodeToView as ControlEnemyNode);
                break;
        }

    }

    public void SortChildren()
    {
        Debug.Log("sortfun called");
        SkillComposeNode parentnode = NodeToView as SkillComposeNode;
        if (parentnode != null)
        {
            parentnode.ChildrenNodes.Sort(SortByVerticalPos);
        }
    }

    private int SortByVerticalPos(SkillBaseNode up, SkillBaseNode down)
    {
        return up.Positiony < down.Positiony ? -1 : 1;
    }

    public void SetBackgroundColor(SkillBaseNode.SkillNodeAllState runtimeState )
    {
        //Debug.Log(Skillview.FileName+ "called SetBackgroundColor");
        if (Application.isPlaying)
        {
            switch (runtimeState)
            {
                case SkillBaseNode.SkillNodeAllState.Running:
                    this.style.backgroundColor = Color.green;
                    break;
                case SkillBaseNode.SkillNodeAllState.Failure:
                    this.style.backgroundColor = Color.red;
                    break;
                case SkillBaseNode.SkillNodeAllState.Success:
                    this.style.backgroundColor = Color.blue;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
    }


}
