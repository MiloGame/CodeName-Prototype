using System;
using System.Collections.Generic;
using System.Diagnostics.PerformanceData;
using System.IO;
using System.Linq;
using Assets.Scripts;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class SkillEditor : EditorWindow
{
    public IMGUIContainer NodeInspect;
    public SkillGraphView NodeView;
    public SkillBaseNode NodetoShow;
    public TextField FileName;
    public Button SaveButton;
    public Button LoadButton;
    //节点的node集合
    public DebugNode DebugEdi;
    public SerializedObject NodeInspectSerializedObject;
    public SerializedProperty SpecialNodeProperty;
   [SerializeField]
    private VisualTreeAsset m_VisualTreeAsset = default;

    private bool foldinput = false;
    private bool foldoutput = false;
    public SkillSecquenceNode SecquenceEdi;
    public SkillForEachNode ForEachNodeEdi;
    public SkillInverterNode InverterEdi;
    public SkillRepeateNode RepeateEdi;
    public SkillRootNode RootEdi;
    private SerializedObject NodeRunTimeSerializedObject;
    private SerializedProperty NodeInspectRunTimeProperty;
    public SkillBaseNode Runtimeselectnode;
    public SkillManager skm;
    public SkillWaitNode WaitEdi;
    public SkillSingeClick SingeClickEdi;
    public GetHurtNode GetHurtEdi;
    public GetHealNode GetHealEdi;
    public SkillLongPressNode LongPressEdi;
    public SheldSkillNode SheldEdi;
    private List<SkillBaseNode> runtimetreedata;
    private List<SkillTreeManager> skilltreemanager;
    private GameObject selectGameObject;
    public SkillFunIfNode FunIfNodeEdi;
    public ArmGrenadeNode ArmGrenadeNodeEdi;
    public DisArmGrenadeNode DisArmGrenadeNodeEdi;
    public SkillFunDoubleIfNode SkillFunDoubleIfNodeEdi;
    public EarthShatterNode EarthShatterNodeEdi;
    public AimWallNode AimWallNodeEdi;
    public ControlEnemyNode ControlEnemyNodeEdi;

    [MenuItem("SkillEditor/Editor ...")]
    public static void ShowExample()
    {
        SkillEditor wnd = GetWindow<SkillEditor>();
        wnd.titleContent = new GUIContent("SkillEditor");
    }

    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        // VisualElements objects can contain other VisualElement following a tree hierarchy.
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/SkillEditorFolder/SkillEditor.uxml");
        visualTree.CloneTree(root);

        var stylesheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Editor/SkillEditorFolder/SkillEditor.uss");
        root.styleSheets.Add(stylesheet);
        NodeView = root.Q<SkillGraphView>();
        NodeInspect = root.Q<IMGUIContainer>();
        SaveButton = root.Q<Button>("SaveButton");
        LoadButton = root.Q<Button>("LoadButton");
        FileName = root.Q<TextField>();
        
        SaveButton.RegisterCallback<ClickEvent>(ClickFun);
        LoadButton.RegisterCallback<ClickEvent>(LoadClickFun);
        NodeInspect.onGUIHandler = () =>
        {
            OnSelectChange();
            
        };

    }

    private void LoadClickFun(ClickEvent evt)
    {
        Debug.Log("Load Button Clicked!!!!!!");
        if (FileName.value != null && FileName.value != null)
        {

            Debug.Log(FileName.value);
            NodeView.FileName = FileName.value;
            NodeView.ReCreateGraph();

                selectGameObject = Selection.activeGameObject;
                if (selectGameObject != null)
                {
                    skm = selectGameObject.GetComponent<SkillManager>();

                    if (skm != null)
                    {
                        skilltreemanager = skm.skillTreeManagers;
                        //foreach (var treeManager in skilltreemanager)
                        //{
                        //    if (treeManager.name == FileName.value)
                        //    {
                        //        runtimetreedata = treeManager.TreeData.NodesData;
                        //        Debug.Log("OnInspectorUpdate runtimetreedata" + treeManager.name + "FileName.vale" + FileName.value + "runtimetreedata.length" + runtimetreedata.Count);

                        //    }
                        //}
                    }
                }
                

            
        }

        
    }

    private void ClickFun(ClickEvent evt)
    {
        Debug.Log("Save Button Clicked!!!!!!");
        NodeView.SaveTreeData();
    }

    private void OnSelectChange()
    {
        if (Application.isPlaying)
        {
            
            if (selectGameObject != null&& NodeView.RuntimeCanFre)
            {
                
               
                    //foreach (var treeManager in skilltreemanager)
                    //{

                    //    if (treeManager.name == FileName.value)
                    //    {
                    //        runtimetreedata = treeManager.TreeData.NodesData;
                    //        Debug.Log("OnSelectChange runtimetreedata" + treeManager.name + "FileName.vale" + FileName.value);

                    //    }
                    //}
                    // runtimetreedata = skm.TreeData.NodesData;
                    //for (int i = 0; i < runtimetreedata.Count; i++)
                    //{
                    //    if (runtimetreedata[i].IsExcuted)
                    //    {
                    //        NodeView.AddToSelection(NodeView.RecreateSkillViewNodes[i]);
                    //    }
                    //    else
                    //    {
                    //        NodeView.ClearSelection();
                    //    }
                    //}
                    if (NodeView.SelectNode!=null)
                    {
                        Runtimeselectnode =
                            runtimetreedata.FirstOrDefault(n => n.nodeGuid == NodeView.SelectNode.NodeToView.nodeGuid);
                        
                        if (Runtimeselectnode != null)
                        {
                            NodeRunTimeSerializedObject = new SerializedObject(this);
                            switch (Runtimeselectnode.Nodetype)
                            {
                                case SkillBaseNode.TreeNodeType.DebugNode:
                                    DebugEdi = Runtimeselectnode as DebugNode;
                                    //  Debug.Log($"DEBUGEDI==NULL{DebugEdi==null}");
                                    //  Debug.Log($"debugedi titlename{DebugEdi.Titlename}debugedi.guid{DebugEdi.nodeGuid}");
                                    NodeInspectSerializedObject = new SerializedObject(this);
                                    SpecialNodeProperty = NodeInspectSerializedObject.FindProperty("DebugEdi");
                                    EditorGUILayout.PropertyField(SpecialNodeProperty);
                                    break;
                                case SkillBaseNode.TreeNodeType.SkillSecquenceNode:
                                    SecquenceEdi = Runtimeselectnode as SkillSecquenceNode;
                                    NodeInspectSerializedObject = new SerializedObject(this);
                                    SpecialNodeProperty = NodeInspectSerializedObject.FindProperty("SecquenceEdi");
                                    EditorGUILayout.PropertyField(SpecialNodeProperty);
                                    break;
                                case SkillBaseNode.TreeNodeType.SkillForEachNode:
                                    ForEachNodeEdi = Runtimeselectnode as SkillForEachNode;
                                    NodeInspectSerializedObject = new SerializedObject(this);
                                    SpecialNodeProperty = NodeInspectSerializedObject.FindProperty("ForEachNodeEdi");
                                    EditorGUILayout.PropertyField(SpecialNodeProperty);
                                    break;
                                case SkillBaseNode.TreeNodeType.SkillInverterNode:
                                    InverterEdi = Runtimeselectnode as SkillInverterNode;
                                    NodeInspectSerializedObject = new SerializedObject(this);
                                    SpecialNodeProperty = NodeInspectSerializedObject.FindProperty("InverterEdi");
                                    EditorGUILayout.PropertyField(SpecialNodeProperty);
                                    break;
                                case SkillBaseNode.TreeNodeType.SkillRepeateNode:
                                    RepeateEdi = Runtimeselectnode as SkillRepeateNode;
                                    NodeInspectSerializedObject = new SerializedObject(this);
                                    SpecialNodeProperty = NodeInspectSerializedObject.FindProperty("RepeateEdi");
                                    EditorGUILayout.PropertyField(SpecialNodeProperty);
                                    break;
                                case SkillBaseNode.TreeNodeType.SkillRootNode:
                                    RootEdi = Runtimeselectnode as SkillRootNode;
                                    NodeInspectSerializedObject = new SerializedObject(this);
                                    SpecialNodeProperty = NodeInspectSerializedObject.FindProperty("RootEdi");
                                    EditorGUILayout.PropertyField(SpecialNodeProperty);
                                    break;
                                case SkillBaseNode.TreeNodeType.SkillWaitNode:
                                    WaitEdi = Runtimeselectnode as SkillWaitNode;
                                    NodeInspectSerializedObject = new SerializedObject(this);
                                    SpecialNodeProperty = NodeInspectSerializedObject.FindProperty("WaitEdi");
                                    EditorGUILayout.PropertyField(SpecialNodeProperty);
                                    break;
                                case SkillBaseNode.TreeNodeType.SkillSingeClick:
                                    SingeClickEdi = Runtimeselectnode as SkillSingeClick;
                                    NodeInspectSerializedObject = new SerializedObject(this);
                                    SpecialNodeProperty = NodeInspectSerializedObject.FindProperty("SingeClickEdi");
                                    EditorGUILayout.PropertyField(SpecialNodeProperty);
                                    break;
                                case SkillBaseNode.TreeNodeType.GetHurtNode:
                                    GetHurtEdi = Runtimeselectnode as GetHurtNode;
                                    NodeInspectSerializedObject = new SerializedObject(this);
                                    SpecialNodeProperty = NodeInspectSerializedObject.FindProperty("GetHurtEdi");
                                    EditorGUILayout.PropertyField(SpecialNodeProperty);
                                    break;
                                case SkillBaseNode.TreeNodeType.GetHealNode:
                                    GetHealEdi = Runtimeselectnode as GetHealNode;
                                    NodeInspectSerializedObject = new SerializedObject(this);
                                    SpecialNodeProperty = NodeInspectSerializedObject.FindProperty("GetHealEdi");
                                    EditorGUILayout.PropertyField(SpecialNodeProperty);
                                    break;
                                case SkillBaseNode.TreeNodeType.SkillLongPressNode:
                                    LongPressEdi = Runtimeselectnode as SkillLongPressNode;
                                    NodeInspectSerializedObject = new SerializedObject(this);
                                    SpecialNodeProperty = NodeInspectSerializedObject.FindProperty("LongPressEdi");
                                    EditorGUILayout.PropertyField(SpecialNodeProperty);
                                    break;
                                case SkillBaseNode.TreeNodeType.SheldSkillNode:
                                    SheldEdi = Runtimeselectnode as SheldSkillNode;
                                    NodeInspectSerializedObject = new SerializedObject(this);
                                    SpecialNodeProperty = NodeInspectSerializedObject.FindProperty("SheldEdi");
                                    EditorGUILayout.PropertyField(SpecialNodeProperty);
                                    break;
                                case SkillBaseNode.TreeNodeType.SkillFunIfNode:
                                    FunIfNodeEdi = Runtimeselectnode as SkillFunIfNode;
                                    NodeInspectSerializedObject = new SerializedObject(this);
                                    SpecialNodeProperty = NodeInspectSerializedObject.FindProperty("FunIfNodeEdi");
                                    EditorGUILayout.PropertyField(SpecialNodeProperty);
                                    break;
                                case SkillBaseNode.TreeNodeType.ArmGrenadeNode:
                                    ArmGrenadeNodeEdi = Runtimeselectnode as ArmGrenadeNode;
                                    NodeInspectSerializedObject = new SerializedObject(this);
                                    SpecialNodeProperty = NodeInspectSerializedObject.FindProperty("ArmGrenadeNodeEdi");
                                    EditorGUILayout.PropertyField(SpecialNodeProperty);
                                    break;
                                case SkillBaseNode.TreeNodeType.DisArmGrenadeNode:
                                    DisArmGrenadeNodeEdi = Runtimeselectnode as DisArmGrenadeNode;
                                    NodeInspectSerializedObject = new SerializedObject(this);
                                    SpecialNodeProperty = NodeInspectSerializedObject.FindProperty("DisArmGrenadeNodeEdi");
                                    EditorGUILayout.PropertyField(SpecialNodeProperty);
                                    break;
                                case SkillBaseNode.TreeNodeType.SkillFunDoubleIfNode:
                                    SkillFunDoubleIfNodeEdi = Runtimeselectnode as SkillFunDoubleIfNode;
                                    NodeInspectSerializedObject = new SerializedObject(this);
                                    SpecialNodeProperty = NodeInspectSerializedObject.FindProperty("SkillFunDoubleIfNodeEdi");
                                    EditorGUILayout.PropertyField(SpecialNodeProperty);
                                    break;
                                case SkillBaseNode.TreeNodeType.EarthShatterNode:
                                    EarthShatterNodeEdi = Runtimeselectnode as EarthShatterNode;
                                    NodeInspectSerializedObject = new SerializedObject(this);
                                    SpecialNodeProperty = NodeInspectSerializedObject.FindProperty("EarthShatterNodeEdi");
                                    EditorGUILayout.PropertyField(SpecialNodeProperty);
                                    break;
                                case SkillBaseNode.TreeNodeType.AimWallNode:
                                    AimWallNodeEdi = Runtimeselectnode as AimWallNode;
                                    NodeInspectSerializedObject = new SerializedObject(this);
                                    SpecialNodeProperty = NodeInspectSerializedObject.FindProperty("AimWallNodeEdi");
                                    EditorGUILayout.PropertyField(SpecialNodeProperty);
                                    break;
                                case SkillBaseNode.TreeNodeType.ControlEnemyNode:
                                    ControlEnemyNodeEdi = Runtimeselectnode as ControlEnemyNode;
                                    NodeInspectSerializedObject = new SerializedObject(this);
                                    SpecialNodeProperty = NodeInspectSerializedObject.FindProperty("ControlEnemyNodeEdi");
                                    EditorGUILayout.PropertyField(SpecialNodeProperty);
                                break;
                                default:
                                    throw new ArgumentOutOfRangeException();
                            }
                          //  NodeInspectRunTimeProperty = NodeRunTimeSerializedObject.FindProperty("Runtimeselectnode");
                          //  EditorGUILayout.PropertyField(NodeInspectRunTimeProperty);
                        }
                    }

                    
                
            }
        }
        else
        {
            if (NodeView.SelectNode!=null)
            {
                NodetoShow = NodeView.SelectNode.NodeToView;
               // Debug.Log($"NodetoShow titlename{NodetoShow.Titlename}NodetoShow.guid{NodetoShow.nodeGuid}nodetoshow nodetype{NodetoShow.Nodetype}");
                Debug.Log($"NodetoShow type: {NodetoShow.GetType()}NodetoShow.Nodetype == SkillBaseNode.TreeNodeType.DebugNode{NodetoShow.Nodetype == SkillBaseNode.TreeNodeType.DebugNode}");
                EditorGUILayout.LabelField("Edit Select Node Data", EditorStyles.boldLabel);
                EditorGUI.BeginChangeCheck();
                switch (NodetoShow.Nodetype)
                {
                    case SkillBaseNode.TreeNodeType.DebugNode:
                        DebugEdi = NodetoShow as DebugNode;
                      //  Debug.Log($"DEBUGEDI==NULL{DebugEdi==null}");
                      //  Debug.Log($"debugedi titlename{DebugEdi.Titlename}debugedi.guid{DebugEdi.nodeGuid}");
                        NodeInspectSerializedObject = new SerializedObject(this);
                        SpecialNodeProperty = NodeInspectSerializedObject.FindProperty("DebugEdi");
                        EditorGUILayout.PropertyField(SpecialNodeProperty);
                        break;
                    case SkillBaseNode.TreeNodeType.SkillSecquenceNode:
                        SecquenceEdi = NodetoShow as SkillSecquenceNode;
                        NodeInspectSerializedObject = new SerializedObject(this);
                        SpecialNodeProperty = NodeInspectSerializedObject.FindProperty("SecquenceEdi");
                        EditorGUILayout.PropertyField(SpecialNodeProperty);
                        break;
                    case SkillBaseNode.TreeNodeType.SkillForEachNode:
                        ForEachNodeEdi = NodetoShow as SkillForEachNode;
                        NodeInspectSerializedObject = new SerializedObject(this);
                        SpecialNodeProperty = NodeInspectSerializedObject.FindProperty("ForEachNodeEdi");
                        EditorGUILayout.PropertyField(SpecialNodeProperty);
                        break;
                    case SkillBaseNode.TreeNodeType.SkillInverterNode:
                        InverterEdi = NodetoShow as SkillInverterNode;
                        NodeInspectSerializedObject = new SerializedObject(this);
                        SpecialNodeProperty = NodeInspectSerializedObject.FindProperty("InverterEdi");
                        EditorGUILayout.PropertyField(SpecialNodeProperty);
                        break;
                    case SkillBaseNode.TreeNodeType.SkillRepeateNode:
                        RepeateEdi = NodetoShow as SkillRepeateNode;
                        NodeInspectSerializedObject = new SerializedObject(this);
                        SpecialNodeProperty = NodeInspectSerializedObject.FindProperty("RepeateEdi");
                        EditorGUILayout.PropertyField(SpecialNodeProperty);
                        break;
                    case SkillBaseNode.TreeNodeType.SkillRootNode:
                        RootEdi = NodetoShow as SkillRootNode;
                        NodeInspectSerializedObject = new SerializedObject(this);
                        SpecialNodeProperty = NodeInspectSerializedObject.FindProperty("RootEdi");
                        EditorGUILayout.PropertyField(SpecialNodeProperty);
                        break;
                    case SkillBaseNode.TreeNodeType.SkillWaitNode:
                        WaitEdi = NodetoShow as SkillWaitNode;
                        NodeInspectSerializedObject = new SerializedObject(this);
                        SpecialNodeProperty = NodeInspectSerializedObject.FindProperty("WaitEdi");
                        EditorGUILayout.PropertyField(SpecialNodeProperty);
                        break;
                    case SkillBaseNode.TreeNodeType.SkillSingeClick:
                        SingeClickEdi = NodetoShow as SkillSingeClick;
                        NodeInspectSerializedObject = new SerializedObject(this);
                        SpecialNodeProperty = NodeInspectSerializedObject.FindProperty("SingeClickEdi");
                        EditorGUILayout.PropertyField(SpecialNodeProperty);
                        break;
                    case SkillBaseNode.TreeNodeType.GetHurtNode:
                        GetHurtEdi = NodetoShow as GetHurtNode;
                        NodeInspectSerializedObject = new SerializedObject(this);
                        SpecialNodeProperty = NodeInspectSerializedObject.FindProperty("GetHurtEdi");
                        EditorGUILayout.PropertyField(SpecialNodeProperty);
                        break;
                    case SkillBaseNode.TreeNodeType.GetHealNode:
                        GetHealEdi = NodetoShow as GetHealNode;
                        NodeInspectSerializedObject = new SerializedObject(this);
                        SpecialNodeProperty = NodeInspectSerializedObject.FindProperty("GetHealEdi");
                        EditorGUILayout.PropertyField(SpecialNodeProperty);
                        break;
                    case SkillBaseNode.TreeNodeType.SkillLongPressNode:
                        LongPressEdi = NodetoShow as SkillLongPressNode;
                        NodeInspectSerializedObject = new SerializedObject(this);
                        SpecialNodeProperty = NodeInspectSerializedObject.FindProperty("LongPressEdi");
                        EditorGUILayout.PropertyField(SpecialNodeProperty);
                        break;
                    case SkillBaseNode.TreeNodeType.SheldSkillNode:
                        SheldEdi = NodetoShow as SheldSkillNode;
                        NodeInspectSerializedObject = new SerializedObject(this);
                        SpecialNodeProperty = NodeInspectSerializedObject.FindProperty("SheldEdi");
                        EditorGUILayout.PropertyField(SpecialNodeProperty);
                        break;
                    case SkillBaseNode.TreeNodeType.SkillFunIfNode:
                        FunIfNodeEdi = NodetoShow as SkillFunIfNode;
                        NodeInspectSerializedObject = new SerializedObject(this);
                        SpecialNodeProperty = NodeInspectSerializedObject.FindProperty("FunIfNodeEdi");
                        Debug.Log($"SpecialNodeProperty is null{SpecialNodeProperty==null }NodeInspectSerializedObject is null{NodeInspectSerializedObject == null}FunIfNodeEdi is null{FunIfNodeEdi == null}");
                        EditorGUILayout.PropertyField(SpecialNodeProperty);
                        break;
                    case SkillBaseNode.TreeNodeType.ArmGrenadeNode:
                        ArmGrenadeNodeEdi = NodetoShow as ArmGrenadeNode;
                        NodeInspectSerializedObject = new SerializedObject(this);
                        SpecialNodeProperty = NodeInspectSerializedObject.FindProperty("ArmGrenadeNodeEdi");
                        Debug.Log($"SpecialNodeProperty is null{SpecialNodeProperty == null }NodeInspectSerializedObject is null{NodeInspectSerializedObject == null}FunIfNodeEdi is null{FunIfNodeEdi == null}");
                        EditorGUILayout.PropertyField(SpecialNodeProperty);
                        break;
                    case SkillBaseNode.TreeNodeType.DisArmGrenadeNode:
                        DisArmGrenadeNodeEdi = NodetoShow as DisArmGrenadeNode;
                        NodeInspectSerializedObject = new SerializedObject(this);
                        SpecialNodeProperty = NodeInspectSerializedObject.FindProperty("DisArmGrenadeNodeEdi");
                        Debug.Log($"SpecialNodeProperty is null{SpecialNodeProperty == null }NodeInspectSerializedObject is null{NodeInspectSerializedObject == null}FunIfNodeEdi is null{FunIfNodeEdi == null}");
                        EditorGUILayout.PropertyField(SpecialNodeProperty);
                        break;
                    case SkillBaseNode.TreeNodeType.SkillFunDoubleIfNode:
                        SkillFunDoubleIfNodeEdi = NodetoShow as SkillFunDoubleIfNode;
                        NodeInspectSerializedObject = new SerializedObject(this);
                        SpecialNodeProperty = NodeInspectSerializedObject.FindProperty("SkillFunDoubleIfNodeEdi");
                        Debug.Log($"SpecialNodeProperty is null{SpecialNodeProperty == null }NodeInspectSerializedObject is null{NodeInspectSerializedObject == null}FunIfNodeEdi is null{FunIfNodeEdi == null}");
                        EditorGUILayout.PropertyField(SpecialNodeProperty);
                        break;
                    case SkillBaseNode.TreeNodeType.EarthShatterNode:
                        EarthShatterNodeEdi = NodetoShow as EarthShatterNode;
                        NodeInspectSerializedObject = new SerializedObject(this);
                        SpecialNodeProperty = NodeInspectSerializedObject.FindProperty("EarthShatterNodeEdi");
                        Debug.Log($"SpecialNodeProperty is null{SpecialNodeProperty == null }NodeInspectSerializedObject is null{NodeInspectSerializedObject == null}FunIfNodeEdi is null{FunIfNodeEdi == null}");
                        EditorGUILayout.PropertyField(SpecialNodeProperty);
                        break;
                    case SkillBaseNode.TreeNodeType.AimWallNode:
                        AimWallNodeEdi = NodetoShow as AimWallNode;
                        NodeInspectSerializedObject = new SerializedObject(this);
                        SpecialNodeProperty = NodeInspectSerializedObject.FindProperty("AimWallNodeEdi");
                        Debug.Log($"SpecialNodeProperty is null{SpecialNodeProperty == null }NodeInspectSerializedObject is null{NodeInspectSerializedObject == null}FunIfNodeEdi is null{FunIfNodeEdi == null}");
                        EditorGUILayout.PropertyField(SpecialNodeProperty);
                        break;
                    case SkillBaseNode.TreeNodeType.ControlEnemyNode:
                        ControlEnemyNodeEdi = NodetoShow as ControlEnemyNode;
                        NodeInspectSerializedObject = new SerializedObject(this);
                        SpecialNodeProperty = NodeInspectSerializedObject.FindProperty("ControlEnemyNodeEdi");
                        Debug.Log($"SpecialNodeProperty is null{SpecialNodeProperty == null }NodeInspectSerializedObject is null{NodeInspectSerializedObject == null}FunIfNodeEdi is null{FunIfNodeEdi == null}");
                        EditorGUILayout.PropertyField(SpecialNodeProperty);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                ////SerializedProperty property = NodeInspectSerializedObject.GetIterator();
                //while (SpecialNodeProperty.Next(true))
                //{
                //    Debug.Log($"Property name: {SpecialNodeProperty.name}, Property type: {SpecialNodeProperty.propertyType}");
                //}

                foldinput = EditorGUILayout.Foldout(foldinput, "Edite Input Ports Here");
                if (foldinput)
                {
                    for (int i = 0; i < NodetoShow.InportsInfo.Count; i++)
                    {

                        EditorGUILayout.LabelField("NodetoShow.InportsInfo Node " + i + " Edit Prop", EditorStyles.boldLabel);
                        PortInfo portInfo = NodetoShow.InportsInfo[i];

                        EditorGUILayout.BeginVertical(GUI.skin.box);

                        // Display and edit PortInfo properties
                        portInfo.Portguid = EditorGUILayout.TextField("Port GUID", portInfo.Portguid);
                        portInfo.ParamName = EditorGUILayout.TextField("Param Name", portInfo.ParamName);
                        portInfo.PortPositionX = EditorGUILayout.FloatField("Port Position X", portInfo.PortPositionX);
                        portInfo.PortPositionY = EditorGUILayout.FloatField("Port Position Y", portInfo.PortPositionY);
                        portInfo.DataTypeFullName = EditorGUILayout.TextField("Data Type Full Name", portInfo.DataTypeFullName);
                        portInfo.connectToPortGuid = EditorGUILayout.TextField("ConnectTo PortGuid", portInfo.connectToPortGuid);

                        EditorGUILayout.EndVertical();

                        NodetoShow.InportsInfo[i] = portInfo;
                    }
                }
                foldoutput = EditorGUILayout.Foldout(foldoutput, "Edite Output Ports Here");
                if (foldoutput)
                {
                    for (int i = 0; i < NodetoShow.OutportsInfo.Count; i++)
                    {

                        EditorGUILayout.LabelField("NodetoShow.OutportsInfo Node " + i + " Edit Prop", EditorStyles.boldLabel);
                        PortInfo portInfo = NodetoShow.OutportsInfo[i];

                        EditorGUILayout.BeginVertical(GUI.skin.box);

                        // Display and edit PortInfo properties
                        portInfo.Portguid = EditorGUILayout.TextField("Port GUID", portInfo.Portguid);
                        portInfo.ParamName = EditorGUILayout.TextField("Param Name", portInfo.ParamName);
                        portInfo.PortPositionX = EditorGUILayout.FloatField("Port Position X", portInfo.PortPositionX);
                        portInfo.PortPositionY = EditorGUILayout.FloatField("Port Position Y", portInfo.PortPositionY);
                        portInfo.DataTypeFullName = EditorGUILayout.TextField("Data Type Full Name", portInfo.DataTypeFullName);
                        portInfo.connectToPortGuid = EditorGUILayout.TextField("ConnectTo PortGuid", portInfo.connectToPortGuid);
                        EditorGUILayout.EndVertical();

                        NodetoShow.OutportsInfo[i] = portInfo;
                    }
                }
                if (EditorGUI.EndChangeCheck())
                {
                    NodeInspectSerializedObject.ApplyModifiedProperties();

                    NodetoShow.ChangeInportsInfo();
                    NodetoShow.ChangeOutportsInfo();
                    NodeView.SelectNode.ReFreshData(NodetoShow);
                    //Debug.Log("apply modify");
                    Debug.Log("is same object "+NodetoShow.Equals(NodeView.SelectNode.NodeToView));
                    //NodeView.SelectNode.NodeToView.ChangeInportsInfo();
                    //NodeView.SelectNode.NodeToView.ChangeOutportsInfo();
                    //NodeView.SelectNode.CreateInputPorts();
                    //NodeView.SelectNode.CreateOutputPorts();
                }
            }
           

        }
    }

    private void OnEnable()
    {
        EditorApplication.update += OnInspectorUpdate;
    }

    private void OnInspectorUpdate()
    {

        //if (Application.isPlaying==false&&NodeView!=null)
        //{
        //    if (NodeView.SelectNode==null)
        //    {
        //        NodeInspect.Clear();
        //    }
        //}
        //else if (Application.isPlaying && NodeView != null)
        //{
        //    if (NodeView.SelectNode == null)
        //    {
        //        NodeInspect.Clear();
        //    }
        //}
        if (NodeView != null && NodeView.SelectNode == null)
        {

                NodeInspect.Clear();
            
        }

        if (Application.isPlaying &&skilltreemanager!=null)
        {
            foreach (var treeManager in skilltreemanager)
            {
                if (treeManager.name == FileName.value)
                {
                    runtimetreedata = treeManager.TreeData.NodesData;
                    //Debug.Log("OnInspectorUpdate runtimetreedata" + treeManager.name + "FileName.vale" + FileName.value + "runtimetreedata.length" + runtimetreedata.Count);

                }
            }
            // var runtimetreedata = skm.TreeData.NodesData;
            for (int i = 0; i < runtimetreedata.Count; i++)
            {
                //Debug.Log("runtimetreedata[i].State"+ runtimetreedata[i].State);
                NodeView.RecreateSkillViewNodes[i].SetBackgroundColor(runtimetreedata[i].State);
            }
        }
        
    }

    private void OnDisable()
    {

        EditorApplication.update -= OnInspectorUpdate;
    }

}
