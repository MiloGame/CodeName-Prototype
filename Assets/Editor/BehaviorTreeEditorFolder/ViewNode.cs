using System;
using Assets.Scripts;
using NUnit.Framework.Constraints;
using UnityEditor;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

public class ViewNode : UnityEditor.Experimental.GraphView.Node
{
    public Node nodetoGraph;
    public Port InputPort;
    public Port OutputPort;
    //string runnodename ;
    //bool nodeisrun ;
    //Node.AllState runnodestate ;
    //int tmprunstate ;
    public ViewNode(Node nodepro) : base("Assets/Editor/BehaviorTreeEditorFolder/ViewNodeEdge.uxml")
    {
        nodetoGraph = nodepro;
        //Debug.Log("viewnode struct fun called nodetograph info" + nodetoGraph.GetNodeInfoAsString());
        ////Debug.Log("viewnode struct fun called nodetype"+nodetoGraph.nodetype+nodepro.nodetype);
        this.title = nodetoGraph.titlename;
        this.viewDataKey = nodetoGraph.titlename;
        CreateInputPorts();
        CreateOutputPorts();
        SetViewNodeClass();
    }
    public void ReFreshData(Node node)
    {
        nodetoGraph = node;
        //Debug.Log("viewnode struct fun called nodetograph info" + nodetoGraph.GetNodeInfoAsString());
        ////Debug.Log("viewnode struct fun called nodetype"+nodetoGraph.nodetype+nodepro.nodetype);
        this.title = nodetoGraph.titlename;
        this.viewDataKey = nodetoGraph.titlename;

        SetViewNodeClass();


    }
    //自定义节点颜色
    private void SetViewNodeClass()
    {
        if (nodetoGraph is ActionNode)
        {
            AddToClassList("action");
        }
        else if (nodetoGraph is CompositeNode)
        {
            AddToClassList("composite");
        }
        else if (nodetoGraph is DecoratorNode)
        {
            AddToClassList("decorator");
        }
        else if (nodetoGraph is RootNode)
        {
            AddToClassList("root");
        }

    }

    private void CreateInputPorts()
    {
        if (nodetoGraph is ActionNode)
        {
            InputPort = InstantiatePort(Orientation.Vertical, Direction.Input, Port.Capacity.Single, typeof(bool));
        }else if (nodetoGraph is CompositeNode)
        {
            //Debug.Log("enter createinputport is compositenode  info" + (nodetoGraph as CompositeNode).GetNodeInfoAsString());
            InputPort = InstantiatePort(Orientation.Vertical, Direction.Input, Port.Capacity.Single, typeof(bool));
        }
        else if (nodetoGraph is DecoratorNode)
        {
            //Debug.Log("enter createinputport is DecoratorNode  info" + (nodetoGraph as DecoratorNode).GetNodeInfoAsString());

            InputPort = InstantiatePort(Orientation.Vertical, Direction.Input, Port.Capacity.Single, typeof(bool));
        }
        if (InputPort!=null)
        {
            InputPort.portName = "";
            InputPort.style.flexDirection = FlexDirection.Column;
            inputContainer.Add(InputPort);
        }
    }

    private void CreateOutputPorts()
    {
        if (nodetoGraph is CompositeNode)
        {
            //Debug.Log("enter createoutputport is compositenode info" + (nodetoGraph as CompositeNode).GetNodeInfoAsString());

            OutputPort = InstantiatePort(Orientation.Vertical, Direction.Output, Port.Capacity.Multi, typeof(bool));
        }
        else if (nodetoGraph is DecoratorNode)
        {
            //Debug.Log("enter createoutputport is DecoratorNode info" + (nodetoGraph as DecoratorNode).GetNodeInfoAsString());

            OutputPort = InstantiatePort(Orientation.Vertical, Direction.Output, Port.Capacity.Single, typeof(bool));
        }
        else if (nodetoGraph is RootNode)
        {
            //Debug.Log("enter createoutputport is RootNode info" + (nodetoGraph as RootNode).GetNodeInfoAsString());

            OutputPort = InstantiatePort(Orientation.Vertical, Direction.Output, Port.Capacity.Single, typeof(bool));
        }

        if (OutputPort != null)
        {
            OutputPort.portName = "";
            OutputPort.style.flexDirection = FlexDirection.ColumnReverse;//对齐居中
            outputContainer.Add(OutputPort);
        }
    }
    //图形界面按节点从左到右的顺序执行
    public void SortChildren()
    {
        CompositeNode parentnode = nodetoGraph as CompositeNode;
        if (parentnode!=null)
        {
            parentnode.ChildreNodes.Sort(SortByHorizonPos);
        }
    }

    private int SortByHorizonPos(Node left, Node right)
    {
        return left.positionx < right.positionx ? -1 : 1;
    }


    public void Viewnodestatefresh()
    {

        //仅调试使用，发布时删除
        //EditorPrefs.GetString("nodename", runnodename);
        //EditorPrefs.GetBool("nodeisexcuted", nodeisrun);
        //EditorPrefs.GetInt("nodestate", tmprunstate);
       // runnodestate = (Node.AllState)tmprunstate;
        RemoveFromClassList("running");
        RemoveFromClassList("failure");
        RemoveFromClassList("success");
        if (Application.isPlaying)
        {
            switch (nodetoGraph.State)
            {
                case Node.AllState.Running:
                    if (nodetoGraph.IsExcuted)
                    {
                        //Debug.Log(nodetoGraph.titlename + "Viewnodestatefresh running is called");
                        AddToClassList("running");
                    }

                    break;
                case Node.AllState.Failure:
                    //Debug.Log(nodetoGraph.titlename + "Viewnodestatefresh running is called");
                    AddToClassList("failure");
                    break;
                case Node.AllState.Success:
                    //Debug.Log(nodetoGraph.titlename + "Viewnodestatefresh running is called");
                    AddToClassList("success");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
