using System;
using System.Linq;
using System.Net.Mime;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using BehaveTreeManager = Assets.Scripts.BehaviorTree.BehaviorTreeManger;
public class BehaviorTreeEditor : EditorWindow
{
    public BehaviorTreeView treeView;
    public InspectView ViewInspect;
    public IMGUIContainer BlackboarderContainer;
    public SerializedObject blackSerializedObject;
    public SerializedProperty blackProperty;
    private bool isclear = false;
    private bool isPlaying = false;
    private Button loadbutton;
    private Button savebutton;
    private TextField FileNameField;
    public SerializedObject NodeInspectSerializedObject;
    public SerializedProperty SpecialNodeProperty;
    public DebugLogNode DebugEdi;
    public GameObject selectGameObject;
    public WaitNode WaitNodeEdi;
    public RepeatedNode RepeatedNodeEdi;
    public SecquenceNode SecquenceNodeEdi;
    public Selector SelectorEdi;
    public RootNode RootNodeEdi;
    public Inverter InverterEdi;
    public ChaseNode ChaseNodeEdi;

    public GotoPosistionNode GotoPosistionNodeEdi;

    public IfNode IfNodeEdi;
    public EnemyViewNode EnemyViewNodeEdi;
    public BehaveTreeManager bm;
    public EnemyHealthNode EnemyHealthNodeEdi;
    public IsCoverAvaliableNode IsCoverAvaliableNodeEdi;
    public ReturnToBornPosNode RangeNodeEdi;
    public IsCoveredNode IsCoveredNodeEdi;
    public SetCoverPositionNode SetCoverPositionNodeEdi;
    public ShootingNode ShootingNodeEdi;
    public DoPatrolNode DoPatrolNodeEdi;
    public GetPatrolNode GetPatrolNodeEdi;
    public UnderAttackNode UnderAttackNodeEdi;
    public ForEachNode ForEachNodeEdi;
    public AlwaysTrueNode AlwaysTrueNodeEdi;
    public LoseViewNode LoseViewNodeEdi;
    public DecideToChaseNode DecideToChaseNodeEdi;
    public AttAckPlayerNode AttAckPlayerNodeEdi;
    public BossHealthUnderLimitNode BossHealthUnderLimitNodeEdi;
    public BossDistanceUnderLimitNode BossDistanceUnderLimitNodeEdi;
    public BossTurnNode BossTurnNodeEdi;
    public BossGrenateNavPathNode BossGrenateNavPathNodeEdi;
    public BossAttackNode BossAttackNodeEdi;
    public BossChasePlayerNode BossChasePlayerNodeEdi;
    public BossJumpNode BossJumpNodeEdi;
    public BossGenreateTinyEnemyNode BossGenreateTinyEnemyNodeEdi;
    public WaitCoolenNode WaitCoolenNodeEdi;
    public BossShouldJumpNode BossShouldJumpNodeEdi;
    public BossAimNode BossAimNodeEdi;

    [MenuItem("BehaviorTreeEditor/Editor ...")]
    public static void OpenWindow()
    {
        BehaviorTreeEditor wnd = GetWindow<BehaviorTreeEditor>();
        wnd.titleContent = new GUIContent("BehaviorTreeEditor");
    }



    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        // VisualElements objects can contain other VisualElement following a tree hierarchy.
        //VisualElement label = new Label("Hello World! From C#");
        //root.Add(label);

        // Instantiate UXML
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/BehaviorTreeEditorFolder/BehaviorTreeEditor.uxml");
        visualTree.CloneTree(root);

        var stylesheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Editor/BehaviorTreeEditorFolder/BehaviorTreeEditor.uss");
        root.styleSheets.Add(stylesheet);
        treeView = root.Q<BehaviorTreeView>();
        ViewInspect = root.Q<InspectView>();
        BlackboarderContainer = root.Q<IMGUIContainer>();
        loadbutton = root.Q<Button>("Load");
        savebutton = root.Q<Button>("Save");
        FileNameField = root.Q<TextField>("FileName");
        loadbutton.RegisterCallback<ClickEvent>(OnLoad);
        savebutton.RegisterCallback<ClickEvent>(OnSave);
        BlackboarderContainer.onGUIHandler = () =>
        {

            OnSelectChange();

        };
       
        treeView.setInspectView(ViewInspect);
    }

    private void OnLoad(ClickEvent evt)
    {
        if (FileNameField.value!="")
        {
            treeView.Filename = FileNameField.value;
            treeView.ClearAll();

            treeView.ReCreateGraph();
        }
        else
        {
            treeView.ClearAll();
        }
    }

    private void OnSave(ClickEvent evt)
    {
        if (FileNameField.value != "")
        {

            treeView.SaveTreeData();
        }
    }

    private void OnSelectChange()
    {
        Node showNode =null;
        //if (Application.isPlaying)
        //{
        //    if (selectGameObject != null)
        //    {
        //        if (bm != null)
        //        {
        //            var runtimetreedata = bm.treeData.nodesData;
        //            if (runtimetreedata!=null)
        //            {
          
        //                if (treeView.SelectNode!=null)
        //                    showNode = runtimetreedata.FirstOrDefault(n => n.titlename == treeView.SelectNode.nodetoGraph.titlename);

        //            }
        //            //blackSerializedObject = new SerializedObject(bm);
        //            //blackProperty = blackSerializedObject.FindProperty("TreeBlackBoard");
        //            //EditorGUILayout.PropertyField(blackProperty);
        //        }
        //    }
        //}
        //else
        if (!Application.isPlaying)
        {
            if (treeView.SelectNode!=null)
            {
                showNode = treeView.SelectNode.nodetoGraph;
            }
            EditorGUILayout.LabelField("Edit Select Node Data", EditorStyles.boldLabel);
            EditorGUI.BeginChangeCheck();

        }


        if (showNode != null)
        {
            switch (showNode.nodetype)
            {
                case Node.TreeNodeType.DebugLogNode:
                    DebugEdi = showNode as DebugLogNode;
                    NodeInspectSerializedObject = new SerializedObject(this);
                    SpecialNodeProperty = NodeInspectSerializedObject.FindProperty("DebugEdi");
                    EditorGUILayout.PropertyField(SpecialNodeProperty);
                    break;
                case Node.TreeNodeType.WaitNode:
                    WaitNodeEdi = showNode as WaitNode;
                    NodeInspectSerializedObject = new SerializedObject(this);
                    SpecialNodeProperty = NodeInspectSerializedObject.FindProperty("WaitNodeEdi");
                    EditorGUILayout.PropertyField(SpecialNodeProperty);
                    break;
                case Node.TreeNodeType.RepeatedNode:
                    RepeatedNodeEdi = showNode as RepeatedNode;
                    NodeInspectSerializedObject = new SerializedObject(this);
                    SpecialNodeProperty = NodeInspectSerializedObject.FindProperty("RepeatedNodeEdi");
                    EditorGUILayout.PropertyField(SpecialNodeProperty);
                    break;
                case Node.TreeNodeType.SecquenceNode:
                    SecquenceNodeEdi = showNode as SecquenceNode;
                    NodeInspectSerializedObject = new SerializedObject(this);
                    SpecialNodeProperty = NodeInspectSerializedObject.FindProperty("SecquenceNodeEdi");
                    EditorGUILayout.PropertyField(SpecialNodeProperty);
                    break;
                case Node.TreeNodeType.RootNode:
                    RootNodeEdi = showNode as RootNode;
                    NodeInspectSerializedObject = new SerializedObject(this);
                    SpecialNodeProperty = NodeInspectSerializedObject.FindProperty("RootNodeEdi");
                    EditorGUILayout.PropertyField(SpecialNodeProperty);
                    break;
                case Node.TreeNodeType.Inverter:
                    InverterEdi = showNode as Inverter;
                    NodeInspectSerializedObject = new SerializedObject(this);
                    SpecialNodeProperty = NodeInspectSerializedObject.FindProperty("InverterEdi");
                    EditorGUILayout.PropertyField(SpecialNodeProperty);
                    break;
                case Node.TreeNodeType.Selector:
                    SelectorEdi = showNode as Selector;
                    NodeInspectSerializedObject = new SerializedObject(this);
                    SpecialNodeProperty = NodeInspectSerializedObject.FindProperty("SelectorEdi");
                    EditorGUILayout.PropertyField(SpecialNodeProperty);
                    break;
                case Node.TreeNodeType.ChaseNode:
                    ChaseNodeEdi = showNode as ChaseNode;
                    NodeInspectSerializedObject = new SerializedObject(this);
                    SpecialNodeProperty = NodeInspectSerializedObject.FindProperty("ChaseNodeEdi");
                    EditorGUILayout.PropertyField(SpecialNodeProperty);
                    break;
                case Node.TreeNodeType.EnemyHealthNode:
                    EnemyHealthNodeEdi = showNode as EnemyHealthNode;
                    NodeInspectSerializedObject = new SerializedObject(this);
                    SpecialNodeProperty = NodeInspectSerializedObject.FindProperty("EnemyHealthNodeEdi");
                    EditorGUILayout.PropertyField(SpecialNodeProperty);
                    break;
                case Node.TreeNodeType.GotoPosistionNode:
                    GotoPosistionNodeEdi = showNode as GotoPosistionNode;
                    NodeInspectSerializedObject = new SerializedObject(this);
                    SpecialNodeProperty = NodeInspectSerializedObject.FindProperty("GotoPosistionNodeEdi");
                    EditorGUILayout.PropertyField(SpecialNodeProperty);
                    break;
                case Node.TreeNodeType.IsCoverAvaliableNode:
                    IsCoverAvaliableNodeEdi = showNode as IsCoverAvaliableNode;
                    NodeInspectSerializedObject = new SerializedObject(this);
                    SpecialNodeProperty = NodeInspectSerializedObject.FindProperty("IsCoverAvaliableNodeEdi");
                    EditorGUILayout.PropertyField(SpecialNodeProperty);
                    break;
                case Node.TreeNodeType.IsCoveredNode:
                    IsCoveredNodeEdi = showNode as IsCoveredNode;
                    NodeInspectSerializedObject = new SerializedObject(this);
                    SpecialNodeProperty = NodeInspectSerializedObject.FindProperty("IsCoveredNodeEdi");
                    EditorGUILayout.PropertyField(SpecialNodeProperty);
                    break;
                case Node.TreeNodeType.ReturnToBornPosNode:
                    RangeNodeEdi = showNode as ReturnToBornPosNode;
                    NodeInspectSerializedObject = new SerializedObject(this);
                    SpecialNodeProperty = NodeInspectSerializedObject.FindProperty("RangeNodeEdi");
                    EditorGUILayout.PropertyField(SpecialNodeProperty);
                    break;
                case Node.TreeNodeType.SetCoverPositionNode:
                    SetCoverPositionNodeEdi = showNode as SetCoverPositionNode;
                    NodeInspectSerializedObject = new SerializedObject(this);
                    SpecialNodeProperty = NodeInspectSerializedObject.FindProperty("SetCoverPositionNodeEdi");
                    EditorGUILayout.PropertyField(SpecialNodeProperty);
                    break;
                case Node.TreeNodeType.ShootingNode:
                    ShootingNodeEdi = showNode as ShootingNode;
                    NodeInspectSerializedObject = new SerializedObject(this);
                    SpecialNodeProperty = NodeInspectSerializedObject.FindProperty("ShootingNodeEdi");
                    EditorGUILayout.PropertyField(SpecialNodeProperty);
                    break;
                case Node.TreeNodeType.DoPatrolNode:
                    DoPatrolNodeEdi = showNode as DoPatrolNode;
                    NodeInspectSerializedObject = new SerializedObject(this);
                    SpecialNodeProperty = NodeInspectSerializedObject.FindProperty("DoPatrolNodeEdi");
                    EditorGUILayout.PropertyField(SpecialNodeProperty);
                    break;
                case Node.TreeNodeType.GetPatrolNode:
                    GetPatrolNodeEdi = showNode as GetPatrolNode;
                    NodeInspectSerializedObject = new SerializedObject(this);
                    SpecialNodeProperty = NodeInspectSerializedObject.FindProperty("GetPatrolNodeEdi");
                    EditorGUILayout.PropertyField(SpecialNodeProperty);
                    break;
                case Node.TreeNodeType.UnderAttackNode:
                    UnderAttackNodeEdi = showNode as UnderAttackNode;
                    NodeInspectSerializedObject = new SerializedObject(this);
                    SpecialNodeProperty = NodeInspectSerializedObject.FindProperty("UnderAttackNodeEdi");
                    EditorGUILayout.PropertyField(SpecialNodeProperty);
                    break;
                case Node.TreeNodeType.EnemyViewNode:
                    EnemyViewNodeEdi = showNode as EnemyViewNode;
                    NodeInspectSerializedObject = new SerializedObject(this);
                    SpecialNodeProperty = NodeInspectSerializedObject.FindProperty("EnemyViewNodeEdi");
                    EditorGUILayout.PropertyField(SpecialNodeProperty);
                    break;
                case Node.TreeNodeType.IfNode:
                    IfNodeEdi = showNode as IfNode;
                    NodeInspectSerializedObject = new SerializedObject(this);
                    SpecialNodeProperty = NodeInspectSerializedObject.FindProperty("IfNodeEdi");
                    EditorGUILayout.PropertyField(SpecialNodeProperty);
                    break;
                case Node.TreeNodeType.ForEachNode:
                    ForEachNodeEdi = showNode as ForEachNode;
                    NodeInspectSerializedObject = new SerializedObject(this);
                    SpecialNodeProperty = NodeInspectSerializedObject.FindProperty("ForEachNodeEdi");
                    EditorGUILayout.PropertyField(SpecialNodeProperty);
                    break;
                case Node.TreeNodeType.AlwaysTrueNode:
                    AlwaysTrueNodeEdi = showNode as AlwaysTrueNode;
                    NodeInspectSerializedObject = new SerializedObject(this);
                    SpecialNodeProperty = NodeInspectSerializedObject.FindProperty("AlwaysTrueNodeEdi");
                    EditorGUILayout.PropertyField(SpecialNodeProperty);
                    break;
                case Node.TreeNodeType.DecideToChaseNode:
                    DecideToChaseNodeEdi = showNode as DecideToChaseNode;
                    NodeInspectSerializedObject = new SerializedObject(this);
                    SpecialNodeProperty = NodeInspectSerializedObject.FindProperty("DecideToChaseNodeEdi");
                    EditorGUILayout.PropertyField(SpecialNodeProperty);
                    break;
                case Node.TreeNodeType.LoseViewNode:
                    LoseViewNodeEdi = showNode as LoseViewNode;
                    NodeInspectSerializedObject = new SerializedObject(this);
                    SpecialNodeProperty = NodeInspectSerializedObject.FindProperty("LoseViewNodeEdi");
                    EditorGUILayout.PropertyField(SpecialNodeProperty);
                    break;
                case Node.TreeNodeType.AttAckPlayerNode:
                    AttAckPlayerNodeEdi = showNode as AttAckPlayerNode;
                    NodeInspectSerializedObject = new SerializedObject(this);
                    SpecialNodeProperty = NodeInspectSerializedObject.FindProperty("AttAckPlayerNodeEdi");
                    EditorGUILayout.PropertyField(SpecialNodeProperty);
                    break;
                case Node.TreeNodeType.BossHealthUnderLimitNode:
                    BossHealthUnderLimitNodeEdi = showNode as BossHealthUnderLimitNode;
                    NodeInspectSerializedObject = new SerializedObject(this);
                    SpecialNodeProperty = NodeInspectSerializedObject.FindProperty("BossHealthUnderLimitNodeEdi");
                    EditorGUILayout.PropertyField(SpecialNodeProperty);
                    break;
                case Node.TreeNodeType.BossDistanceUnderLimitNode:
                    BossDistanceUnderLimitNodeEdi = showNode as BossDistanceUnderLimitNode;
                    NodeInspectSerializedObject = new SerializedObject(this);
                    SpecialNodeProperty = NodeInspectSerializedObject.FindProperty("BossDistanceUnderLimitNodeEdi");
                    EditorGUILayout.PropertyField(SpecialNodeProperty);
                    break;
                case Node.TreeNodeType.BossTurnNode:
                    BossTurnNodeEdi = showNode as BossTurnNode;
                    NodeInspectSerializedObject = new SerializedObject(this);
                    SpecialNodeProperty = NodeInspectSerializedObject.FindProperty("BossTurnNodeEdi");
                    EditorGUILayout.PropertyField(SpecialNodeProperty);
                    break;
                case Node.TreeNodeType.BossGrenateNavPathNode:
                    BossGrenateNavPathNodeEdi = showNode as BossGrenateNavPathNode;
                    NodeInspectSerializedObject = new SerializedObject(this);
                    SpecialNodeProperty = NodeInspectSerializedObject.FindProperty("BossGrenateNavPathNodeEdi");
                    EditorGUILayout.PropertyField(SpecialNodeProperty);
                    break;
                case Node.TreeNodeType.BossAttackNode:
                    BossAttackNodeEdi = showNode as BossAttackNode;
                    NodeInspectSerializedObject = new SerializedObject(this);
                    SpecialNodeProperty = NodeInspectSerializedObject.FindProperty("BossAttackNodeEdi");
                    EditorGUILayout.PropertyField(SpecialNodeProperty);
                    break;
                case Node.TreeNodeType.BossChasePlayerNode:
                    BossChasePlayerNodeEdi = showNode as BossChasePlayerNode;
                    NodeInspectSerializedObject = new SerializedObject(this);
                    SpecialNodeProperty = NodeInspectSerializedObject.FindProperty("BossChasePlayerNodeEdi");
                    EditorGUILayout.PropertyField(SpecialNodeProperty);
                    break;
                case Node.TreeNodeType.BossJumpNode:
                    BossJumpNodeEdi = showNode as BossJumpNode;
                    NodeInspectSerializedObject = new SerializedObject(this);
                    SpecialNodeProperty = NodeInspectSerializedObject.FindProperty("BossJumpNodeEdi");
                    EditorGUILayout.PropertyField(SpecialNodeProperty);
                    break;
                case Node.TreeNodeType.BossGenreateTinyEnemyNode:
                    BossGenreateTinyEnemyNodeEdi = showNode as BossGenreateTinyEnemyNode;
                    NodeInspectSerializedObject = new SerializedObject(this);
                    SpecialNodeProperty = NodeInspectSerializedObject.FindProperty("BossGenreateTinyEnemyNodeEdi");
                    EditorGUILayout.PropertyField(SpecialNodeProperty);
                    break;
                case Node.TreeNodeType.WaitCoolenNode:
                    WaitCoolenNodeEdi = showNode as WaitCoolenNode;
                    NodeInspectSerializedObject = new SerializedObject(this);
                    SpecialNodeProperty = NodeInspectSerializedObject.FindProperty("WaitCoolenNodeEdi");
                    EditorGUILayout.PropertyField(SpecialNodeProperty);
                    break;
                case Node.TreeNodeType.BossShouldJumpNode:
                    BossShouldJumpNodeEdi = showNode as BossShouldJumpNode;
                    NodeInspectSerializedObject = new SerializedObject(this);
                    SpecialNodeProperty = NodeInspectSerializedObject.FindProperty("BossShouldJumpNodeEdi");
                    EditorGUILayout.PropertyField(SpecialNodeProperty);
                    break;
                case Node.TreeNodeType.BossAimNode:
                    BossAimNodeEdi = showNode as BossAimNode;
                    NodeInspectSerializedObject = new SerializedObject(this);
                    SpecialNodeProperty = NodeInspectSerializedObject.FindProperty("BossAimNodeEdi");
                    EditorGUILayout.PropertyField(SpecialNodeProperty);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (!Application.isPlaying)
            {
                if (EditorGUI.EndChangeCheck())
                {
                    NodeInspectSerializedObject.ApplyModifiedProperties();
                    treeView.SelectNode.ReFreshData(showNode);
                }
            }
           
        }
        //GameObject selectGameObject = Selection.activeGameObject;
        //if (selectGameObject != null && Application.isPlaying)
        //{
        //    BehaveTreeManager bm = selectGameObject.GetComponent<BehaveTreeManager>();
        //    if (bm != null)
        //    {
        //        blackSerializedObject = new SerializedObject(bm);
        //        blackProperty = blackSerializedObject.FindProperty("TreeBlackBoard");
        //        EditorGUILayout.PropertyField(blackProperty);
        //    }
        //}


    }

    //覆盖inspecview的刷新函数用于显示node的运行状态
    private void OnInspectorUpdate()
    {
       // Debug.Log("OnInspectorUpdate is called");
        selectGameObject = Selection.activeGameObject;
        if (selectGameObject!=null && Application.isPlaying)
        {
            bm = selectGameObject.GetComponent<BehaveTreeManager>();
            if (bm!=null)
            {
                //int waitindex = bm.treeData.nodesData.FindIndex(node => node.titlename == "WaitNode2");
               //Debug.Log("OnInspectorUpdate treemanager data waitnode2"+bm.treeData.nodesData[waitindex].State+bm.treeData.nodesData[waitindex].IsExcuted);

                treeView?.ReFreshGraph(bm.treeData);//可空操作符
                treeView?.ShowWhichNodeIsRun();
               
              
            }
        }
        // treeView?.ShowWhichNodeIsRun();
    }

    private void OnEnable()
    {
        EditorApplication.update += OnInspectorUpdate;
    }
    private void OnDisable()
    {
    
        EditorApplication.update -= OnInspectorUpdate;
    }
}
