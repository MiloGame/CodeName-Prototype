using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using Edge = UnityEditor.Experimental.GraphView.Edge;

public class SkillGraphView : GraphView
{
    public SkillTreeData skillTreeData;
    public string Filepath;
    public string FileName;
    public string Jsonload;
    public string Jsonsave;
    public SkillViewNode SelectNode;
    public List<SkillViewNode> RecreateSkillViewNodes;
    public bool RuntimeCanFre;

    public new class UxmlFactory : UxmlFactory<SkillGraphView,GraphView.UxmlTraits> { }
    public SkillGraphView()
    {
        Insert(0,new GridBackground());
        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new ContentZoomer());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new RectangleSelector());
        var stylesheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Editor/SkillEditorFolder/SkillEditor.uss");
        styleSheets.Add(stylesheet);
        skillTreeData = new SkillTreeData();
        RecreateSkillViewNodes = new List<SkillViewNode>();
        RegisterCallback<MouseUpEvent>(OnMouseUp);//实现位置更新
        graphViewChanged += OnGraphViewChanged;//委托增加方法检测连线的变化
        if (FileName!=""&&FileName!=null)
        {
            ReCreateGraph();
        }
    }



    public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
    {
        return ports.ToList().Where(endPort =>
            endPort.direction != startPort.direction &&
            endPort.node != startPort.node).ToList();
    }
    private GraphViewChange OnGraphViewChanged(GraphViewChange graphViewChange)
    {
        if (graphViewChange.edgesToCreate != null)
        {
            graphViewChange.edgesToCreate.ForEach(edge =>
            {
                //Debug.Log(edge.output.portName+edge.output.userData);
                SkillViewNode parenetView = edge.output.node as SkillViewNode;
                SkillViewNode childView = edge.input.node as SkillViewNode;
                int indexParent = skillTreeData.NodesData.IndexOf(parenetView.NodeToView);
                int indexChild = skillTreeData.NodesData.IndexOf(childView.NodeToView);
                var parent = skillTreeData.NodesData[indexParent];
                var kid = skillTreeData.NodesData[indexChild];
                if (edge.output.portName != "LinkOutPort")
                {
                    Debug.Log("OnGraphViewChanged" + edge.output.portName + edge.output.userData);
                    var parentPort = parent.OutportsInfo.Find(elem => elem.ParamName == edge.output.portName);
                    var childPort = kid.InportsInfo.Find(e => e.ParamName == edge.input.portName);
                    parentPort.connectToPortGuid = childPort.Portguid;
                    childPort.connectToPortGuid = parentPort.Portguid;
                    edge.output.ConnectTo(edge.input);
                    edge.userData = edge.output.userData;
                    edge.input.userData = edge.userData;
                    childView.ReFreshData(kid);
                    Debug.Log("output"+edge.output.userData+edge.output.connected+"input"+edge.input.userData+edge.input.connected+"edge"+edge.userData);

                }
                else
                {
                    
                    AddChild(ref parent, ref kid);
                }
               
                //Debug.Log("create edge");
                //SaveTreeData();
            });
        }
        if (graphViewChange.elementsToRemove != null)
        {
            graphViewChange.elementsToRemove.ForEach(elem =>
            {
                if (elem is SkillViewNode)
                {
                    //Debug.Log("del viewnode");
                    DeleteNodeView();
                    //SaveTreeData();
                }
                else if (elem is Edge)
                {
                    //Debug.Log("DEL Edge");
                    Edge edge = elem as Edge;
                    SkillViewNode parenetView = edge.output.node as SkillViewNode;
                    SkillViewNode childView = edge.input.node as SkillViewNode;
                    int indexParent = skillTreeData.NodesData.IndexOf(parenetView.NodeToView);
                    int indexChild = skillTreeData.NodesData.IndexOf(childView.NodeToView);
                    var parent = skillTreeData.NodesData[indexParent];
                    var kid = skillTreeData.NodesData[indexChild];
                    if (edge.output.portName != "LinkOutPort")
                    {
                        var parentPort = parent.OutportsInfo.Find(elem => elem.ParamName == edge.output.portName);
                        var childPort = kid.InportsInfo.Find(e => e.ParamName == edge.input.portName);
                        parentPort.connectToPortGuid = "";
                        childPort.connectToPortGuid = "";
                        //childView.ReFreshData(kid);
                        //parenetView.ReFreshData(parent);

                    }
                    else
                    {
                        RemoveChild(ref parent, ref kid);
                    }
                        //Debug.Log("removekid treedata contains"+treeData.nodesData.Contains(kid));
                        
                    //    DeleteNodeInAllParenet(ref parenetView);
                    //SaveTreeData();
                }

            });

            
        }
        if (graphViewChange.movedElements != null)
        {
            nodes.ForEach((n) =>
            {
                SkillViewNode view = n as SkillViewNode;
                view.SortChildren();
            });
        }
        return graphViewChange;

    }

    private void RemoveChild(ref SkillBaseNode parentNode, ref SkillBaseNode childNode)
    {
        SkillDecorateNode decoparentNode = parentNode as SkillDecorateNode;
        if (decoparentNode != null)
        {
            decoparentNode.ChildNode = null;
        }
        SkillRootNode rootparentNode = parentNode as SkillRootNode;
        if (rootparentNode != null)
        {
            rootparentNode.ChildNode = null;
        }
        SkillComposeNode compparentNode = parentNode as SkillComposeNode;
        if (compparentNode != null)
        {
            compparentNode.ChildrenNodes.Remove(childNode);

        }
    }

    private void AddChild(ref SkillBaseNode parentNode, ref SkillBaseNode childNode)
    {
        SkillDecorateNode decoparentNode = parentNode as SkillDecorateNode;
        if (decoparentNode != null)
        {
            decoparentNode.ChildNode = childNode;
        }
        SkillComposeNode compparentNode = parentNode as SkillComposeNode;
        if (compparentNode != null)
        {
            compparentNode.ChildrenNodes.Add(childNode);
        }
        SkillRootNode rootpareNode = parentNode as SkillRootNode;
        if (rootpareNode != null)
        {
            rootpareNode.ChildNode = childNode;
        }
    }

    private void DeleteNodeView()
    {
        var seletnodes = selection.OfType<SkillViewNode>().ToList();//LinQ
        foreach (var elem in seletnodes)
        {
            skillTreeData.DeleteNode(elem.NodeToView);
            RemoveElement(elem as GraphElement);
        }

        SelectNode = null;
        //SaveTreeData();
    }

    public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
    {
        if (evt.target is GraphView)
        {
            Vector2 mousePosition = evt.mousePosition;
            evt.menu.AppendAction("Create SkillActionNode/Create EarthShatterNode", (action) =>
            {
                CreateNodeView(mousePosition, SkillBaseNode.TreeNodeType.EarthShatterNode);
                evt.StopPropagation();
                // //Debug.Log(mousePosition);
            });
            evt.menu.AppendAction("Create SkillActionNode/Create DebugNode", (action) =>
            {
                CreateNodeView(mousePosition, SkillBaseNode.TreeNodeType.DebugNode);
                evt.StopPropagation();
                // //Debug.Log(mousePosition);
            });
            evt.menu.AppendAction("Create SkillActionNode/Create ControlEnemyNode", (action) =>
            {
                CreateNodeView(mousePosition, SkillBaseNode.TreeNodeType.ControlEnemyNode);
                evt.StopPropagation();
                // //Debug.Log(mousePosition);
            });
            evt.menu.AppendAction("Create SkillActionNode/Create AimWallNode", (action) =>
            {
                CreateNodeView(mousePosition, SkillBaseNode.TreeNodeType.AimWallNode);
                evt.StopPropagation();
                // //Debug.Log(mousePosition);
            });
            evt.menu.AppendAction("Create SkillActionNode/Create SkillWaitNode", (action) =>
            {
                CreateNodeView(mousePosition, SkillBaseNode.TreeNodeType.SkillWaitNode);
                evt.StopPropagation();
                // //Debug.Log(mousePosition);
            });
            evt.menu.AppendAction("Create SkillActionNode/Create SheldSkillNode", (action) =>
            {
                CreateNodeView(mousePosition, SkillBaseNode.TreeNodeType.SheldSkillNode);
                evt.StopPropagation();
                // //Debug.Log(mousePosition);
            });
            evt.menu.AppendAction("Create SkillActionNode/Create SheldSkillNode", (action) =>
            {
                CreateNodeView(mousePosition, SkillBaseNode.TreeNodeType.SheldSkillNode);
                evt.StopPropagation();
                // //Debug.Log(mousePosition);
            });
            evt.menu.AppendAction("Create SkillActionNode/Create ArmGrenadeNode", (action) =>
            {
                CreateNodeView(mousePosition, SkillBaseNode.TreeNodeType.ArmGrenadeNode);
                evt.StopPropagation();
                // //Debug.Log(mousePosition);
            });
            evt.menu.AppendAction("Create SkillActionNode/Create DisArmGrenadeNode", (action) =>
            {
                CreateNodeView(mousePosition, SkillBaseNode.TreeNodeType.DisArmGrenadeNode);
                evt.StopPropagation();
                // //Debug.Log(mousePosition);
            });
            evt.menu.AppendAction("Create SkillActionNode/Create GetHurtNode", (action) =>
            {
                CreateNodeView(mousePosition, SkillBaseNode.TreeNodeType.GetHurtNode);
                evt.StopPropagation();
                // //Debug.Log(mousePosition);
            });
            evt.menu.AppendAction("Create SkillActionNode/Create SkillSingeClick", (action) =>
            {
                CreateNodeView(mousePosition, SkillBaseNode.TreeNodeType.SkillSingeClick);
                evt.StopPropagation();
                // //Debug.Log(mousePosition);
            });
            evt.menu.AppendAction("Create SkillActionNode/Create GetHealNode", (action) =>
            {
                CreateNodeView(mousePosition, SkillBaseNode.TreeNodeType.GetHealNode);
                evt.StopPropagation();
                // //Debug.Log(mousePosition);
            });
            evt.menu.AppendAction("Create SkillActionNode/Create SkillLongPressNode", (action) =>
            {
                CreateNodeView(mousePosition, SkillBaseNode.TreeNodeType.SkillLongPressNode);
                evt.StopPropagation();
                // //Debug.Log(mousePosition);
            });
            evt.menu.AppendAction("Create SkillComposeNode/Create SkillSecquenceNode", (action) =>
            {
                CreateNodeView(mousePosition, SkillBaseNode.TreeNodeType.SkillSecquenceNode);
                evt.StopPropagation();
                // //Debug.Log(mousePosition);
            });
            evt.menu.AppendAction("Create SkillComposeNode/Create SkillForEachNode", (action) =>
            {
                CreateNodeView(mousePosition, SkillBaseNode.TreeNodeType.SkillForEachNode);
                evt.StopPropagation();
                // //Debug.Log(mousePosition);
            });
            evt.menu.AppendAction("Create SkillComposeNode/Create SkillFunIfNode", (action) =>
            {
                CreateNodeView(mousePosition, SkillBaseNode.TreeNodeType.SkillFunIfNode);
                evt.StopPropagation();
                // //Debug.Log(mousePosition);
            });
            evt.menu.AppendAction("Create SkillComposeNode/Create SkillFunDoubleIfNode", (action) =>
            {
                CreateNodeView(mousePosition, SkillBaseNode.TreeNodeType.SkillFunDoubleIfNode);
                evt.StopPropagation();
                // //Debug.Log(mousePosition);
            });
            evt.menu.AppendAction("Create SkillDecorateNode/Create SkillInverterNode", (action) =>
            {
                CreateNodeView(mousePosition, SkillBaseNode.TreeNodeType.SkillInverterNode);
                evt.StopPropagation();
                // //Debug.Log(mousePosition);
            });
            evt.menu.AppendAction("Create SkillDecorateNode/Create SkillRepeateNode", (action) =>
            {
                CreateNodeView(mousePosition, SkillBaseNode.TreeNodeType.SkillRepeateNode);
                evt.StopPropagation();
                // //Debug.Log(mousePosition);
            });
            evt.menu.AppendAction("Create SkillRootNode", (action) =>
            {
                CreateNodeView(mousePosition, SkillBaseNode.TreeNodeType.SkillRootNode);
                evt.StopPropagation();
                // //Debug.Log(mousePosition);
            });

        }
        evt.menu.AppendAction("Delete Node", (action) =>
        {
            DeleteNodeView();
            evt.StopPropagation();
        });
    }

    private void CreateNodeView(Vector2 evtMousePosition, SkillBaseNode.TreeNodeType nodeType)
    {
        Vector2 localMousePosition = contentViewContainer.WorldToLocal(evtMousePosition);
        var createNode = skillTreeData.CreateNode(nodeType);
        skillTreeData.NodesData.Add(createNode);
        skillTreeData.NodesData[skillTreeData.NodesData.Count - 1].Positionx = localMousePosition.x;
        skillTreeData.NodesData[skillTreeData.NodesData.Count - 1].Positiony = localMousePosition.y;
        skillTreeData.NodesData[skillTreeData.NodesData.Count - 1].Nodetype = nodeType;
        SkillViewNode newViewNode = new SkillViewNode(skillTreeData.NodesData[skillTreeData.NodesData.Count - 1]);
        newViewNode.SetPosition(new Rect(newViewNode.NodeToView.Positionx, newViewNode.NodeToView.Positiony,
            newViewNode.NodeToView.Width, newViewNode.NodeToView.Height));
        newViewNode.Skillview = this;
        AddElement(newViewNode);
        //SaveTreeData();
    }

    private void OnMouseUp(MouseUpEvent evt)
    {
        var seletnodes = selection.OfType<SkillViewNode>().ToList();//LinQ
        //Debug.Log("mouse up selectnodes"+seletnodes);
        if (seletnodes.Count==0)
        {
            SelectNode = null;
        }
        foreach (var elementViewNode in seletnodes)
        {
            Debug.Log("selectnodes type" + elementViewNode.NodeToView.Titlename + elementViewNode.NodeToView.Nodetype + elementViewNode.NodeToView.nodeGuid);
            var newRect = elementViewNode.layout;
            int index = skillTreeData.NodesData.IndexOf(elementViewNode.NodeToView);
            skillTreeData.NodesData[index].Positionx = newRect.x;
            skillTreeData.NodesData[index].Positiony = newRect.y;
            skillTreeData.NodesData[index].Width = newRect.width;
            skillTreeData.NodesData[index].Height = newRect.height;
            elementViewNode.SetPosition(newRect);
            //Debug.Log("mouse up change pos nodetype"+treeData.nodesData[index].nodetype+treeData.nodesData[index].GetType());
            ////Debug.Log("changed elements selected");
            SelectNode = elementViewNode;
        }
        //SaveTreeData();
        evt.StopPropagation();
    }
    public void SaveTreeData()
    {
        Filepath = Path.Combine(Application.dataPath, "JsonData", FileName+ ".json");
        JsonSerializerSettings settings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All
        };
        Jsonsave = JsonConvert.SerializeObject(skillTreeData,settings);
        if (File.Exists(Filepath))
        {
            File.Delete(Filepath);
        }
        File.WriteAllText(Filepath, Jsonsave);
    }

    public void LoadTreeData()
    {
        Filepath = Path.Combine(Application.dataPath, "JsonData", FileName+ ".json");
        JsonSerializerSettings settings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All
        };
        Jsonload = File.ReadAllText(Filepath);
        skillTreeData = JsonConvert.DeserializeObject<SkillTreeData>(Jsonload, settings);

        //skillTreeData = JsonConvert.DeserializeObject<SkillTreeData>(Jsonload);
        foreach (var elem in skillTreeData.NodesData)
        {
            Debug.Log($"read from json elem type{elem.GetType()}");
            ReConnectTreedata(elem);
            Debug.Log(elem.ToString());
        }
    }

    private void ReConnectTreedata(SkillBaseNode elem)
    {
        SkillDecorateNode decoparentNode = elem as SkillDecorateNode;
        if (decoparentNode != null)
        {
            //int childindex = treeData.nodesData.IndexOf(decoparentNode.ChildNode);
            if (decoparentNode.ChildNode != null)
            {
                SkillBaseNode nodeToRecon = skillTreeData.NodesData.FirstOrDefault(node => node.nodeGuid == decoparentNode.ChildNode.nodeGuid);
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
                    SkillBaseNode nodeToRecon = skillTreeData.NodesData.FirstOrDefault(node => node.nodeGuid == compparentNode.ChildrenNodes[i].nodeGuid);
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
                SkillBaseNode nodeToRecon = skillTreeData.NodesData.FirstOrDefault(node => node.nodeGuid == rootpareNode.ChildNode.nodeGuid);
                rootpareNode.ChildNode = nodeToRecon;
            }
            else
            {
                rootpareNode.ChildNode = null;
            }

        }
    }

    public void ReCreateGraph()
    {
        RecreateSkillViewNodes.Clear();
        DeleteElements(graphElements.ToList());
        string filepath = Path.Combine(Application.dataPath, "JsonData", FileName+".json");
        //Debug.Log("recreategraph called");
        if (File.Exists(filepath))
        {
            RuntimeCanFre = true;
            LoadTreeData();
            if (skillTreeData != null)
            {
                foreach (var elemNode in skillTreeData.NodesData)
                {
                    CreateNodeViewFromJson(elemNode);
                }

                foreach (var recreateSkillViewNode in RecreateSkillViewNodes)
                {
                    recreateSkillViewNode.CutAllLinks();
                    recreateSkillViewNode.RefreshLinks();
                }
                foreach (var elemNode in skillTreeData.NodesData)
                {
                    //Debug.Log(elemNode.titlename+"create edge node type"+elemNode.GetType());
                    var children = GetChildreNodes(elemNode);
                    children.ForEach(c =>
                    {
                        if (c != null)
                        {
                            SkillViewNode parentView = FindViewNode(elemNode);
                            SkillViewNode childView = FindViewNode(c);
                            //Debug.Log("parenet view"+parentView+"childview"+childView);
                            //   //Debug.Log(childView.nodetoGraph.GetNodeInfoAsString());
                            Edge ed = parentView.LinkOutPort.ConnectTo(childView.LinkInPort);//不会触发edge的change事件
                            AddElement(ed);
                            }

                    });


                }
            }
            else
            {
                RuntimeCanFre = false;
                if (File.Exists(filepath))
                {
                    File.Delete(filepath);
                    //Debug.Log("load null delete exist file");
                }
            }

        }

    }

    public SkillViewNode FindViewNode(SkillBaseNode elemNode)
    {
        return GetNodeByGuid(elemNode.nodeGuid) as SkillViewNode;
    }

    private List<SkillBaseNode> GetChildreNodes(SkillBaseNode parenetNode)
    {
        List<SkillBaseNode> noderes = new List<SkillBaseNode>();
        SkillDecorateNode decoparentNode = parenetNode as SkillDecorateNode;
        if (decoparentNode != null && decoparentNode.ChildNode != null)
        {
            noderes.Add(decoparentNode.ChildNode);
        }
        SkillComposeNode compparentNode = parenetNode as SkillComposeNode;
        if (compparentNode != null)
        {
            noderes = compparentNode.ChildrenNodes;
        }
        SkillRootNode rootparentNode = parenetNode as SkillRootNode;
        if (rootparentNode != null)
        {
            noderes.Add(rootparentNode.ChildNode);
        }
        return noderes;
    }

    private void CreateNodeViewFromJson(SkillBaseNode elemNode)
    {
        SkillViewNode newViewNode = new SkillViewNode(elemNode);
        newViewNode.SetPosition(new Rect(newViewNode.NodeToView.Positionx, newViewNode.NodeToView.Positiony,
            newViewNode.NodeToView.Width, newViewNode.NodeToView.Height));
        RecreateSkillViewNodes.Add(newViewNode);
        newViewNode.Skillview = this;
        AddElement(newViewNode);
    }
}
