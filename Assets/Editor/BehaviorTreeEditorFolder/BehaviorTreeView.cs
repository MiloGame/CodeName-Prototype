using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Assets.Scripts;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using Newtonsoft.Json;
using Edge = UnityEditor.Experimental.GraphView.Edge;

public class BehaviorTreeView : GraphView
{
    private TreeData treeData;
    private InspectView inspectView;
    string newName = "";
    string newDebugMessage = "";
    public string Filename;
    public ViewNode SelectNode;

    public new class UxmlFactory : UxmlFactory<BehaviorTreeView, GraphView.UxmlTraits>
    {
    }

    public BehaviorTreeView()
    {
        Insert(0, new GridBackground());
        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new ContentZoomer());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new RectangleSelector());
        var stylesheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Editor/BehaviorTreeEditorFolder/BehaviorTreeEditor.uss");
        styleSheets.Add(stylesheet);
        treeData = new TreeData();
        //ReCreateGraph();
        //回调函数检测界面变化
        RegisterCallback<MouseUpEvent>(OnMouseUp);//实现位置更新
        graphViewChanged += OnGraphViewChanged;//委托增加方法检测连线的变化
    }
    //检测界面变化，实现增加和del键删除
    private GraphViewChange OnGraphViewChanged(GraphViewChange graphViewChange)
    {
        if (graphViewChange.edgesToCreate!=null)
        {
            graphViewChange.edgesToCreate.ForEach(edge =>
            {
                ViewNode parenetView = edge.output.node as ViewNode;
                ViewNode childView = edge.input.node as ViewNode;
                int indexParent = treeData.nodesData.IndexOf(parenetView.nodetoGraph);
                int indexChild = treeData.nodesData.IndexOf(childView.nodetoGraph);
                var parent = treeData.nodesData[indexParent];
                var kid = treeData.nodesData[indexChild];
                AddChild(ref parent, ref kid);
                //Debug.Log("create edge");
                //SaveTreeData();
            });
        }

        if (graphViewChange.elementsToRemove!=null)
        {
            graphViewChange.elementsToRemove.ForEach(elem =>
            {
                if (elem is Edge)
                {
                    //Debug.Log("DEL Edge");
                    Edge edge = elem as Edge;
                    ViewNode parenetView = edge.output.node as ViewNode;
                    ViewNode childView = edge.input.node as ViewNode;
                    int indexParent = treeData.nodesData.IndexOf(parenetView.nodetoGraph);
                    int indexChild = treeData.nodesData.IndexOf(childView.nodetoGraph);
                    var parent = treeData.nodesData[indexParent];
                    var kid = treeData.nodesData[indexChild];
                    //Debug.Log("removekid treedata contains"+treeData.nodesData.Contains(kid));
                    RemoveChild(ref parent,ref kid);
                    //    DeleteNodeInAllParenet(ref parenetView);
                    //SaveTreeData();
                }
                else if (elem is ViewNode)
                {
                    //Debug.Log("del viewnode");
                    DeleteNodeView();
                    //SaveTreeData();
                }
              
                
            });
            this.inspectView.UpdateInfo("");

        }

        if (graphViewChange.movedElements !=null)
        {
            nodes.ForEach((n) =>
            {
                ViewNode view = n as ViewNode;
                view.SortChildren();
            });
        }
        return graphViewChange;
    }

    public void setInspectView(InspectView v)
    {
        this.inspectView = v;
    }
    private void OnMouseUp(MouseUpEvent evt)
    {
        var seletnodes = selection.OfType<ViewNode>().ToList();//LinQ
        //Debug.Log("mouse up selectnodes"+seletnodes);
        foreach (var elementViewNode in seletnodes)
        {
            //Debug.Log("selectnodes type" + elementViewNode.nodetoGraph.titlename + elementViewNode.nodetoGraph.nodetype + elementViewNode.nodetoGraph.GetType());
            var newRect = elementViewNode.layout;
            int index = treeData.nodesData.IndexOf(elementViewNode.nodetoGraph);
            //Debug.Log(index+"find index in treedata index type"+treeData.nodesData[index].titlename+treeData.nodesData[index].nodetype+ treeData.nodesData[index].GetType());
            treeData.nodesData[index].positionx = newRect.x;
            treeData.nodesData[index].positiony = newRect.y;
            treeData.nodesData[index].width = newRect.width;
            treeData.nodesData[index].height = newRect.height;
            elementViewNode.SetPosition(newRect);
            //Debug.Log("mouse up change pos nodetype"+treeData.nodesData[index].nodetype+treeData.nodesData[index].GetType());
            ////Debug.Log("changed elements selected");
            ///
            ///
            BehaviorTreeFunInterface nodeInterface = elementViewNode.nodetoGraph as BehaviorTreeFunInterface;
            string info = nodeInterface.GetNodeInfoAsString();
            this.inspectView.UpdateInfo(info);
            SelectNode = elementViewNode;
        }
        //SaveTreeData();
        evt.StopPropagation();
    }
    



    //菜单回调函数
    public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
    {
        if (evt.target is GraphView)
        {
            Vector2 mousePosition = evt.mousePosition;
            evt.menu.AppendAction("Create ActionNode/Create DecideToChaseNode", (action) =>
            {
                CreateNodeView(mousePosition, Node.TreeNodeType.DecideToChaseNode);
                evt.StopPropagation();
                // //Debug.Log(mousePosition);
            });
            evt.menu.AppendAction("Create ActionNode/Create BossAimNode", (action) =>
            {
                CreateNodeView(mousePosition, Node.TreeNodeType.BossAimNode);
                evt.StopPropagation();
                // //Debug.Log(mousePosition);
            });
            evt.menu.AppendAction("Create ActionNode/Create BossShouldJumpNode", (action) =>
            {
                CreateNodeView(mousePosition, Node.TreeNodeType.BossShouldJumpNode);
                evt.StopPropagation();
                // //Debug.Log(mousePosition);
            });
            evt.menu.AppendAction("Create ActionNode/Create BossGenreateTinyEnemyNode", (action) =>
            {
                CreateNodeView(mousePosition, Node.TreeNodeType.BossGenreateTinyEnemyNode);
                evt.StopPropagation();
                // //Debug.Log(mousePosition);
            });
            evt.menu.AppendAction("Create ActionNode/Create BossJumpNode", (action) =>
            {
                CreateNodeView(mousePosition, Node.TreeNodeType.BossJumpNode);
                evt.StopPropagation();
                // //Debug.Log(mousePosition);
            }); 
            evt.menu.AppendAction("Create ActionNode/Create BossHealthUnderLimitNode", (action) =>
            {
                CreateNodeView(mousePosition, Node.TreeNodeType.BossHealthUnderLimitNode);
                evt.StopPropagation();
                // //Debug.Log(mousePosition);
            });
            evt.menu.AppendAction("Create ActionNode/Create BossDistanceUnderLimitNode", (action) =>
            {
                CreateNodeView(mousePosition, Node.TreeNodeType.BossDistanceUnderLimitNode);
                evt.StopPropagation();
                // //Debug.Log(mousePosition);
            });
            evt.menu.AppendAction("Create ActionNode/Create BossTurnNode", (action) =>
            {
                CreateNodeView(mousePosition, Node.TreeNodeType.BossTurnNode);
                evt.StopPropagation();
                // //Debug.Log(mousePosition);
            });
            evt.menu.AppendAction("Create ActionNode/Create BossGrenateNavPathNode", (action) =>
            {
                CreateNodeView(mousePosition, Node.TreeNodeType.BossGrenateNavPathNode);
                evt.StopPropagation();
                // //Debug.Log(mousePosition);
            });
            evt.menu.AppendAction("Create ActionNode/Create BossAttackNode", (action) =>
            {
                CreateNodeView(mousePosition, Node.TreeNodeType.BossAttackNode);
                evt.StopPropagation();
                // //Debug.Log(mousePosition);
            });
            evt.menu.AppendAction("Create ActionNode/Create BossChasePlayerNode", (action) =>
            {
                CreateNodeView(mousePosition, Node.TreeNodeType.BossChasePlayerNode);
                evt.StopPropagation();
                // //Debug.Log(mousePosition);
            });
            evt.menu.AppendAction("Create ActionNode/Create LoseViewNode", (action) =>
            {
                CreateNodeView(mousePosition, Node.TreeNodeType.LoseViewNode);
                evt.StopPropagation();
                // //Debug.Log(mousePosition);
            });
            evt.menu.AppendAction("Create ActionNode/Create DecideToChaseNode", (action) =>
            {
                CreateNodeView(mousePosition, Node.TreeNodeType.DecideToChaseNode);
                evt.StopPropagation();
                // //Debug.Log(mousePosition);
            });
            evt.menu.AppendAction("Create ActionNode/Create AttAckPlayerNode", (action) =>
            {
                CreateNodeView(mousePosition, Node.TreeNodeType.AttAckPlayerNode);
                evt.StopPropagation();
                // //Debug.Log(mousePosition);
            });
            evt.menu.AppendAction("Create ActionNode/Create DebugLogNode", (action) =>
            {
                CreateNodeView(mousePosition, Node.TreeNodeType.DebugLogNode);
                evt.StopPropagation();
                // //Debug.Log(mousePosition);
            });
            evt.menu.AppendAction("Create ActionNode/Create AlwaysTrueNode", (action) =>
            {
                CreateNodeView(mousePosition, Node.TreeNodeType.AlwaysTrueNode);
                evt.StopPropagation();
                // //Debug.Log(mousePosition);
            });
            evt.menu.AppendAction("Create ActionNode/Create EnemyViewNode", (action) =>
            {
                CreateNodeView(mousePosition, Node.TreeNodeType.EnemyViewNode);
                evt.StopPropagation();
                // //Debug.Log(mousePosition);
            });
            evt.menu.AppendAction("Create ActionNode/Create DoPatrolNode", (action) =>
            {
                CreateNodeView(mousePosition, Node.TreeNodeType.DoPatrolNode);
                evt.StopPropagation();
                // //Debug.Log(mousePosition);
            });
            evt.menu.AppendAction("Create ActionNode/Create GetPatrolNode", (action) =>
            {
                CreateNodeView(mousePosition, Node.TreeNodeType.GetPatrolNode);
                evt.StopPropagation();
                // //Debug.Log(mousePosition);
            });
            evt.menu.AppendAction("Create ActionNode/Create UnderAttackNode", (action) =>
            {
                CreateNodeView(mousePosition, Node.TreeNodeType.UnderAttackNode);
                evt.StopPropagation();
                // //Debug.Log(mousePosition);
            });
            evt.menu.AppendAction("Create ActionNode/Create ChaseNode", (action) =>
            {
                CreateNodeView(mousePosition, Node.TreeNodeType.ChaseNode);
                evt.StopPropagation();
                // //Debug.Log(mousePosition);
            });
            evt.menu.AppendAction("Create ActionNode/Create EnemyHealthNode", (action) =>
            {
                CreateNodeView(mousePosition, Node.TreeNodeType.EnemyHealthNode);
                evt.StopPropagation();
                // //Debug.Log(mousePosition);
            });
            evt.menu.AppendAction("Create ActionNode/Create GotoPosistionNode", (action) =>
            {
                CreateNodeView(mousePosition, Node.TreeNodeType.GotoPosistionNode);
                evt.StopPropagation();
                // //Debug.Log(mousePosition);
            });
            evt.menu.AppendAction("Create ActionNode/Create IsCoverAvaliableNode", (action) =>
            {
                CreateNodeView(mousePosition, Node.TreeNodeType.IsCoverAvaliableNode);
                evt.StopPropagation();
                // //Debug.Log(mousePosition);
            });
            evt.menu.AppendAction("Create ActionNode/Create IsCoveredNode", (action) =>
            {
                CreateNodeView(mousePosition, Node.TreeNodeType.IsCoveredNode);
                evt.StopPropagation();
                // //Debug.Log(mousePosition);
            });
            evt.menu.AppendAction("Create ActionNode/Create ReturnToBornPosNode", (action) =>
            {
                CreateNodeView(mousePosition, Node.TreeNodeType.ReturnToBornPosNode);
                evt.StopPropagation();
                // //Debug.Log(mousePosition);
            });
            evt.menu.AppendAction("Create ActionNode/Create SetCoverPositionNode", (action) =>
            {
                CreateNodeView(mousePosition, Node.TreeNodeType.SetCoverPositionNode);
                evt.StopPropagation();
                // //Debug.Log(mousePosition);
            });
            evt.menu.AppendAction("Create ActionNode/Create ShootingNode", (action) =>
            {
                CreateNodeView(mousePosition, Node.TreeNodeType.ShootingNode);
                evt.StopPropagation();
                // //Debug.Log(mousePosition);
            });
            evt.menu.AppendAction("Create RootNode", (action) =>
            {
                CreateNodeView(mousePosition, Node.TreeNodeType.RootNode);
                evt.StopPropagation();
                // //Debug.Log(mousePosition);
            });
            evt.menu.AppendAction("Create DecoratorNode/Create RepeatedNode", (action) =>
            {
                CreateNodeView(mousePosition, Node.TreeNodeType.RepeatedNode);
                evt.StopPropagation();
                // //Debug.Log(mousePosition);
            });
            evt.menu.AppendAction("Create ComPositeNode/Create SecquenceNode", (action) =>
            {
                CreateNodeView(mousePosition, Node.TreeNodeType.SecquenceNode);
                evt.StopPropagation();
                //  //Debug.Log(mousePosition);
            });
            evt.menu.AppendAction("Create ComPositeNode/Create IfNode", (action) =>
            {
                CreateNodeView(mousePosition, Node.TreeNodeType.IfNode);
                evt.StopPropagation();
                //  //Debug.Log(mousePosition);
            });
            evt.menu.AppendAction("Create ActionNode/Create WaitNode", (action) =>
            {
                CreateNodeView(mousePosition, Node.TreeNodeType.WaitNode);
                evt.StopPropagation();
                //   //Debug.Log(mousePosition);
            });
            evt.menu.AppendAction("Create DecoratorNode/Create Inverter", (action) =>
            {
                CreateNodeView(mousePosition, Node.TreeNodeType.Inverter);
                evt.StopPropagation();
                //   //Debug.Log(mousePosition);
            });
            evt.menu.AppendAction("Create DecoratorNode/Create WaitCoolenNode", (action) =>
            {
                CreateNodeView(mousePosition, Node.TreeNodeType.WaitCoolenNode);
                evt.StopPropagation();
                //   //Debug.Log(mousePosition);
            });
            evt.menu.AppendAction("Create ComPositeNode/Create Selector", (action) =>
            {
                CreateNodeView(mousePosition, Node.TreeNodeType.Selector);
                evt.StopPropagation();
                //   //Debug.Log(mousePosition);
            });
            evt.menu.AppendAction("Create ComPositeNode/Create ForEachNode", (action) =>
            {
                CreateNodeView(mousePosition, Node.TreeNodeType.ForEachNode);
                evt.StopPropagation();
                //   //Debug.Log(mousePosition);
            });
        }

        evt.menu.AppendAction("Delete Node", (action) =>
        {
            DeleteNodeView();
            evt.StopPropagation();
        });
        evt.menu.AppendAction("Rename Node", (action) =>
        {
            RenameNode.OpenDialog();
            newName = EditorPrefs.GetString("NewName", "");
            //Debug.Log(newName);
            RenameNodeView(newName);
            evt.StopPropagation();
        });
        evt.menu.AppendAction("Change debug message", (action) =>
        {
            ChangeStringValue.OpenDialog();
            newDebugMessage = EditorPrefs.GetString("NewMessage", "");
            var elem = selection.OfType<ViewNode>().ToList();
            foreach (var elementViewNode in elem)
            {
                if ((elementViewNode.nodetoGraph as DebugLogNode)!=null)
                {
                    int index = treeData.nodesData.IndexOf(elementViewNode.nodetoGraph);
                    var targetnode = treeData.nodesData[index] as DebugLogNode;
                    targetnode.message = newDebugMessage;
                    //SaveTreeData();
                    evt.StopPropagation();
                }

            }
                
        });

    }



    void CreateNodeView(Vector2 evtMousePosition, Node.TreeNodeType nodeType)
    {
        Vector2 localMousePosition = contentViewContainer.WorldToLocal(evtMousePosition);
        var createNode =  treeData.CreateNode(nodeType);
        Debug.Log(createNode.GetType());

        treeData.nodesData.Add(createNode);
        Debug.Log(createNode.GetType());
        Debug.Log(treeData.nodesData[treeData.nodesData.Count-1].GetType());
        treeData.nodesData[treeData.nodesData.Count - 1].positionx = localMousePosition.x;
        treeData.nodesData[treeData.nodesData.Count - 1].positiony = localMousePosition.y;
        treeData.nodesData[treeData.nodesData.Count - 1].nodetype = nodeType;
        ViewNode newViewNode = new ViewNode(treeData.nodesData[treeData.nodesData.Count - 1]);
        newViewNode.SetPosition(new Rect(newViewNode.nodetoGraph.positionx, newViewNode.nodetoGraph.positiony,
            newViewNode.nodetoGraph.width, newViewNode.nodetoGraph.height));
        AddElement(newViewNode);
        //SaveTreeData();
    }

    void CreateNodeViewFromJson(Node nodetoprocess)
    {
        //Debug.Log(nodetoprocess.titlename+"create node from load type is " + nodetoprocess.GetType()+nodetoprocess.nodetype);
        //Debug.Log("CreateNodeViewFromJson  fun called node info" + nodetoprocess.GetNodeInfoAsString());

        ViewNode newViewNode = new ViewNode(nodetoprocess);
        newViewNode.SetPosition(new Rect(newViewNode.nodetoGraph.positionx, newViewNode.nodetoGraph.positiony,
            newViewNode.nodetoGraph.width, newViewNode.nodetoGraph.height));
        AddElement(newViewNode);
       // //Debug.Log(newViewNode.OutputPort + "inputport" + newViewNode.InputPort + "viewdatakey" + newViewNode.viewDataKey);
    }

    void DeleteNodeView()
    {
        var seletnodes = selection.OfType<ViewNode>().ToList();//LinQ
        foreach (var elem in seletnodes)
        {
            treeData.DeleteNode(elem.nodetoGraph);
            RemoveElement(elem as GraphElement);
        }

        //SaveTreeData();
    }

    public void DeleteNodeInAllParenet(ref ViewNode viewnodetofresh)
    {
        
        //获取edge连接线的父节点
        ViewNode fatherviewnode = viewnodetofresh.InputPort.connections.FirstOrDefault()?.output.node as ViewNode;
        if (fatherviewnode != null)
        {
            int index = treeData.nodesData.IndexOf(fatherviewnode.nodetoGraph);
            CompositeNode m = treeData.nodesData[index] as CompositeNode;
            if (m != null)
            {
                m.ChildreNodes.Remove(viewnodetofresh.nodetoGraph);
                m.ChildreNodes.Add(viewnodetofresh.nodetoGraph);
            }

            DecoratorNode d = treeData.nodesData[index] as DecoratorNode;
            if (d != null)
            {
                d.ChildNode = viewnodetofresh.nodetoGraph;
            }
            DeleteNodeInAllParenet(ref fatherviewnode);
        }
        
    }
    public void RenameNodeView(string newName)
    {
        var seletnodes = selection.OfType<ViewNode>().ToList();//LinQ
        foreach (var elementViewNode in seletnodes)
        {
            //Debug.Log("elementViewNode is null newname"+ (elementViewNode is null) + newName);
            if (!string.IsNullOrEmpty(newName))
            {
                if (elementViewNode!=null)
                {
                     int index = treeData.nodesData.IndexOf(elementViewNode.nodetoGraph);
                     string oldname = treeData.nodesData[index].titlename;
                    treeData.nodesData[index].titlename =newName; 
                    elementViewNode.title = newName;
                    ////获取edge连接线的父节点？？
                    //ViewNode fathernode =elementViewNode.InputPort.connections.FirstOrDefault()?.output.node as ViewNode;
                    //if (fathernode!=null)
                    //{
                    //    int fatherindex = treeData.nodesData.IndexOf(fathernode.nodetoGraph);
                    //    CompositeNode m = treeData.nodesData[fatherindex] as CompositeNode;
                    //    if (m!=null)
                    //    {
                    //        foreach (var mChildreNode in m.ChildreNodes)
                    //        {
                    //            if (mChildreNode.titlename == oldname)
                    //            {
                    //                mChildreNode.titlename = newName;
                    //            }
                    //        }
                    //    }

                    //    DecoratorNode d = treeData.nodesData[fatherindex] as DecoratorNode;
                    //    if (d!=null)
                    //    {
                    //        d.ChildNode.titlename = newName;
                    //    }

                    //}
                    //SaveTreeData();
                }
            }
        }


        
    }
       
    public void SaveTreeData()
    {
        string filepath = Path.Combine(Application.dataPath, "JsonData", Filename+".json");
        string json = JsonConvert.SerializeObject(treeData);
        if (File.Exists(filepath))
        {
            File.Delete(filepath);
            Debug.Log("delete "+filepath);
        }
        File.WriteAllText(filepath, json);
        

    }

    void LoadTreeData()
    {
        string filepath = Path.Combine(Application.dataPath, "JsonData", Filename+ ".json");
        string json = File.ReadAllText(filepath);
        //JsonSerializerSettings settings = new JsonSerializerSettings
        //{
        //    Converters = new List<JsonConverter> { new ConvertNodeJson() },
        //    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
        //};
        //JsonSerializerSettings settings = new JsonSerializerSettings
        //{
        //    TypeNameHandling = TypeNameHandling.All
        //};
        JsonSerializerSettings settings = new JsonSerializerSettings
        {
            Converters = new List<JsonConverter> { new ConvertParentToChild() }
        };
        treeData = JsonConvert.DeserializeObject<TreeData>(json, settings);
        //Debug.Log("read from json done");
        foreach (var elem in treeData.nodesData)
        {
            Debug.Log(elem.GetType());
            ReConnectTreedata(elem);
        }
        Debug.Log("loadsuccess load info:" + treeData.nodesData.Count);

    }

    void ReConnectTreedata(Node dataNode)
    {
        Debug.Log(dataNode.positionx+""+dataNode.nodetype);
        //int fatherindex = treeData.nodesData.IndexOf(fathernode.nodetoGraph);
        DecoratorNode decoparentNode = dataNode as DecoratorNode;
        if (decoparentNode != null)
        {
            dataNode = decoparentNode;
            //int childindex = treeData.nodesData.IndexOf(decoparentNode.ChildNode);
            if (decoparentNode.ChildNode!=null)
            {
                Node nodeToRecon = treeData.nodesData.FirstOrDefault(node => node.titlename == decoparentNode.ChildNode.titlename);
                decoparentNode.ChildNode = nodeToRecon;
            }
            else
            {
                 decoparentNode.ChildNode = null;
            }
        }
        CompositeNode compparentNode = dataNode as CompositeNode;
        if (compparentNode != null)
        {
            dataNode = compparentNode;
            for (int i = 0; i < compparentNode.ChildreNodes.Count; i++)
            {
                if (compparentNode.ChildreNodes[i]!=null)
                {
                    Node nodeToRecon = treeData.nodesData.FirstOrDefault(node => node.titlename == compparentNode.ChildreNodes[i].titlename);
                    compparentNode.ChildreNodes[i] = nodeToRecon;
                }
                else
                {
                    compparentNode.ChildreNodes[i] = null;
                }
                
            }
        }
        RootNode rootpareNode = dataNode as RootNode;
        if (rootpareNode != null)
        {
            dataNode = rootpareNode;
            if (rootpareNode.ChildNode!=null)
            {
                Node nodeToRecon = treeData.nodesData.FirstOrDefault(node => node.titlename == rootpareNode.ChildNode.titlename);
                rootpareNode.ChildNode = nodeToRecon;
            }
            else
            {
                rootpareNode.ChildNode = null;
            }
            
        }
    }
    ViewNode FindViewNode(Node node)
    {
        return GetNodeByGuid(node.titlename) as ViewNode;
    }
    public void ReCreateGraph()
    {
        string filepath = Path.Combine(Application.dataPath, "JsonData", Filename + ".json");
        //Debug.Log("recreategraph called");
        if (File.Exists(filepath))
        {
             LoadTreeData();
            // string adad = "";
            // foreach (var ttt in treeData.nodesData)
            // {
            //     BehaviorTreeFunInterface nodeInterface = ttt as BehaviorTreeFunInterface;
            //     adad += nodeInterface.GetNodeInfoAsString();
            // }
            //Debug.Log("loadtreedata finished result" + adad);
            if (treeData != null)
             {
                 foreach (var elemNode in treeData.nodesData)
                 {
                     CreateNodeViewFromJson(elemNode);
                 }
                 foreach (var elemNode in treeData.nodesData)
                 {
                    Debug.Log(elemNode.titlename + "create edge node type" + elemNode.GetType());
                    var children = GetChildreNodes(elemNode);
                     children.ForEach(c =>
                     {
                         if (c!=null)
                         {
                             ViewNode parentView = FindViewNode(elemNode);
                             ViewNode childView = FindViewNode(c);
                             Debug.Log("parenet view" + parentView + "childview" + childView);
                             //Debug.Log(childView.nodetoGraph.GetNodeInfoAsString());
                             Edge ed = parentView.OutputPort.ConnectTo(childView.InputPort);//不会触发edge的change事件
                             AddElement(ed);
                         }
                         else
                         {
                             Debug.Log(c.titlename+"isnull");
                         }
                         
                     });
                     
                     
                 }
             }
             else
             {
                if (File.Exists(filepath))
                {
                    File.Delete(filepath);
                    //Debug.Log("load null delete exist file");
                }
             }

        }else
        {
            //Debug.Log("no previousfile");
        }
    }

    public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
    {
        return ports.ToList().Where(endPort =>
            endPort.direction != startPort.direction &&
            endPort.node != startPort.node).ToList();
    }

    public void AddChild(ref Node parentNode,ref Node childNode)
    {
        //Debug.Log(parentNode.titlename+"AddChild parent node type" + parentNode.GetType()+childNode.titlename+"child node type"+childNode.GetType());
        DecoratorNode decoparentNode = parentNode as DecoratorNode;
        if (decoparentNode!=null)
        {
            decoparentNode.ChildNode = childNode;
        }
        CompositeNode compparentNode = parentNode as CompositeNode;
        if (compparentNode != null)
        {
            compparentNode.ChildreNodes.Add(childNode);
        }
        RootNode rootpareNode = parentNode as RootNode;
        if (rootpareNode!=null)
        {
            rootpareNode.ChildNode = childNode;
        }

    }

    public void RemoveChild(ref Node parentNode,ref Node childNode)
    {
        //Debug.Log(parentNode.titlename+"RemoveChild parent node type" + parentNode.GetType() + childNode.titlename+"child node type" + childNode.GetType());

        DecoratorNode decoparentNode = parentNode as DecoratorNode;
        if (decoparentNode != null)
        {
            decoparentNode.ChildNode = null;
        }
        RootNode rootparentNode = parentNode as RootNode;
        if (rootparentNode != null)
        {
            rootparentNode.ChildNode = null;
        }
        CompositeNode compparentNode = parentNode as CompositeNode;
        if (compparentNode != null)
        {
            ////Debug.Log(compparentNode.ChildreNodes.FirstOrDefault(node => node.titlename == childNode.titlename).titlename);

           // Node nodeToRemove =
           //      compparentNode.ChildreNodes.FirstOrDefault(node => node.titlename == childNode.titlename);

          //  compparentNode.ChildreNodes.Remove(nodeToRemove);
          //Debug.Log($"{parentNode.Equals(compparentNode)}{compparentNode.titlename}contain?{childNode.titlename}{compparentNode.ChildreNodes.Contains(childNode)}");//当没有重新加载的时候为true，但是反序列化会false
            compparentNode.ChildreNodes.Remove(childNode);
            foreach (var tmp in compparentNode.ChildreNodes)
            {
                //Debug.Log(tmp);
            }
        }
    }

    public List<Node> GetChildreNodes(Node parenetNode)
    {
        //Debug.Log(parenetNode.titlename+"GetChildreNodes parent node type" + parenetNode.GetType());

        List<Node> noderes = new List<Node>();
        DecoratorNode decoparentNode = parenetNode as DecoratorNode;
        if (decoparentNode != null&&decoparentNode.ChildNode!=null)
        {
            noderes.Add(decoparentNode.ChildNode);
        }
        CompositeNode compparentNode = parenetNode as CompositeNode;
        if (compparentNode != null)
        {
            noderes = compparentNode.ChildreNodes;
        }
        RootNode rootparentNode = parenetNode as RootNode;
        if (rootparentNode != null)
        {
            noderes.Add(rootparentNode.ChildNode);
        }
        return noderes;
    }

    public void ShowWhichNodeIsRun()
    {
        ////Debug.Log("ShowWhichNodeIsRun is called");
        //LoadTreeData();
        nodes.ForEach(n =>
        {
            ViewNode view = n as ViewNode;
            view.Viewnodestatefresh();
        });
        //ReCreateGraph();
    }
    public void ReFreshGraph(TreeData treeData)
    {
        ClearAll();
        string adad = "";
        if (treeData != null)
        {
            foreach (var elemNode in treeData.nodesData)
            {
                CreateNodeViewFromJson(elemNode);
            }
            foreach (var elemNode in treeData.nodesData)
            {
                //Debug.Log(elemNode.titlename + "create edge node type" + elemNode.GetType());
                var children = GetChildreNodes(elemNode);
                children.ForEach(c =>
                {
                    if (c != null)
                    {
                        ViewNode parentView = FindViewNode(elemNode);
                        ViewNode childView = FindViewNode(c);
                        //Debug.Log("parenet view" + parentView + "childview" + childView);
                        //   //Debug.Log(childView.nodetoGraph.GetNodeInfoAsString());
                        Edge ed = parentView.OutputPort.ConnectTo(childView.InputPort);//不会触发edge的change事件
                        AddElement(ed);
                    }

                });
            }
        }
        
    }

    public void ClearAll()
    {
        // 获取GraphView中的所有节点
        List<UnityEditor.Experimental.GraphView.Node> nodes = this.nodes.ToList();

        // 遍历并删除所有节点
        foreach (var node in nodes)
        {
            this.RemoveElement(node);
        }
        // 获取GraphView中的所有边缘
        List<Edge> edges = this.edges.ToList();

        // 遍历并删除所有边缘
        foreach (var edge in edges)
        {
            this.RemoveElement(edge);
        }


    }
}
