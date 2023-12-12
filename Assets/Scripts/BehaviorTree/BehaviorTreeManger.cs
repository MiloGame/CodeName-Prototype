using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.Scripts.BehaviorTree
{
    [Serializable]
    public class BehaviorTreeManger : MonoBehaviour
    {
        public string AIJsonFilename;
        public BehaveTree tree;
        public BlackBoard TreeBlackBoard;
        public TreeData treeData;
        public NavMeshAgent AiAgent;
        public GameObject EnemyObj;
        public GameObject PlayerObj;
        //public EnemyHealthManager EnemyHealthManager;
        public RigDollManager RigDollManager;
        public Animator EnemyAnimator;
        public EnemyHealthUIManger UiManger;
        public Vector3 Point1;
        public Vector3 Point2;
        public Vector3 Point3;
        public Vector3 Point4;
        public List<Vector3> ParaslList;
        //[SerializeField]private Transform PlayerTransform;
        [SerializeField]private Transform EnemyTransform;
        public Transform BossLeftFoot;
        public BossLaserManager BossLaserManager;
        public BossMove BooFootManager;
        private BindableTProperty<float> bossHealthBindableTProperty;
        public Transform Root;
        public bool EnableQuestSYSTEM;
        public GameObject BossFellow;
        public bool IsBossFellow=false;
        public Transform FireTransform;
        void Start()
        {
            bossHealthBindableTProperty = new BindableTProperty<float>();
            if (AiAgent!=null)
            {
                AiAgent.updateRotation = false;

            }
            Init();
            ParaslList = new List<Vector3>();
            if (Point1!=Vector3.zero)
            {
                ParaslList.Add(Point1);
            }
            if (Point2!=Vector3.zero)
            {
                ParaslList.Add(Point2);
            }
            if (Point3!=Vector3.zero)
            {
                ParaslList.Add(Point3);
            }
            if (Point4!=Vector3.zero)
            {
                ParaslList.Add(Point4);
            }
        }
        public void Init()
        {
            LoadTreeBlackBoard();

            if (AIJsonFilename!="")
            {
                LoadTreeData(AIJsonFilename);
                tree = new BehaveTree();
                Node rootNode = treeData.nodesData.FirstOrDefault(n => n.nodetype == Node.TreeNodeType.RootNode);
                tree.Treenode = rootNode;
            }
            
        }
        private void LoadTreeBlackBoard()
        {
            //string BlackBoardfilepath = Path.Combine(Application.dataPath, "JsonData", "NormalAiTreeBlackBoardData.json");
            //if (File.Exists(BlackBoardfilepath))
            //{
            //    string json = File.ReadAllText(BlackBoardfilepath);

            //    TreeBlackBoard = JsonConvert.DeserializeObject<BlackBoard>(json);
            //    //TreeBlackBoard.PlayerTransform = PlayerObj.transform;
            //    TreeBlackBoard.AiInfo.SelfTransform = EnemyObj.transform;
            //    TreeBlackBoard.AiInfo.BornPosition = EnemyObj.transform.position;
            //    TreeBlackBoard.AiInfo.aiAgent = AiAgent;
            //}
            //else
            //{
                //PlayerTransform = PlayerObj.transform;
                EnemyTransform = EnemyObj.transform;
                //TreeBlackBoard = new BlackBoard(PlayerTransform, AiAgent,EnemyTransform,EnemyTransform.position);
                TreeBlackBoard = new BlackBoard(this);
                if (PlayerObj!=null)
                {
                    TreeBlackBoard.PlayerTransform = PlayerObj.transform;
                }
            //    SaveTreeBlackBoard();
            //}
        }

        void LoadTreeData(string filename)
        {
            string filepath = Path.Combine(Application.dataPath, "JsonData", filename+".json");
            string json = File.ReadAllText(filepath);
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                Converters = new List<JsonConverter> { new ConvertParentToChild() }
               
            };

            treeData = JsonConvert.DeserializeObject<TreeData>(json, settings);
           // Debug.Log("read from json done");
            foreach (var elem in treeData.nodesData)
            {
                elem.TreeBlackBoard = TreeBlackBoard;
                ReConnectTreedata(elem);
            }

           
            //  Debug.Log("loadsuccess load info:" + treeData);
        }

        void ReConnectTreedata(Node dataNode)
        {
            //int fatherindex = treeData.nodesData.IndexOf(fathernode.nodetoGraph);
            DecoratorNode decoparentNode = dataNode as DecoratorNode;
            if (decoparentNode != null)
            {
                //int childindex = treeData.nodesData.IndexOf(decoparentNode.ChildNode);
                if (decoparentNode.ChildNode != null)
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
                for (int i = 0; i < compparentNode.ChildreNodes.Count; i++)
                {
                    if (compparentNode.ChildreNodes[i] != null)
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
                if (rootpareNode.ChildNode != null)
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
        //void SaveTreeData()
        //{
        //    string filepath = Path.Combine(Application.dataPath, "JsonData", "NormalAi.json");
        //    string json = JsonConvert.SerializeObject(treeData);
        //    if (File.Exists(filepath))
        //    {
        //        File.Delete(filepath);
        //    }
        //    File.WriteAllText(filepath, json);


        //}

        // Update is called once per frame
        public void Fresh()
        {
            tree.UpdateState();
            //TreeBlackBoard.AiInfo.currentHealth-=3;
            //if (TreeBlackBoard.AiInfo.currentHealth <= 0)
            //{
            //    TreeBlackBoard.AiInfo.currentHealth = 100;
            //}
           // TreeBlackBoard.AiInfo.preHealth = TreeBlackBoard.AiInfo.currentHealth;

            //Debug.Log("blackboard movePosition"+TreeBlackBoard.movePosition);
            // Debug.Log("blackboard aiinfo bestCoverPos" + TreeBlackBoard.AiInfo.bestCoverPos);
            //SaveTreeBlackBoard();
        }

        void Update()
        {
            if (AIJsonFilename != "")
            {
                Fresh();
            }

        }

        public void SaveTreeBlackBoard()
        {
            string fileTreedatapath = Path.Combine(Application.dataPath, "JsonData", "NormalAiTreeBlackBoardData.json");
           
            //string json = JsonConvert.SerializeObject(TreeBlackBoard);
            string json = JsonConvert.SerializeObject(TreeBlackBoard, Formatting.Indented,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                }
            );
            if (File.Exists(fileTreedatapath))
            {
                File.Delete(fileTreedatapath);
            }
            File.WriteAllText(fileTreedatapath, json);
        }

        public void TakeDamage(float amount, string transformName)
        {
            Debug.Log("takedamage"+amount+"hitpos"+transformName);
            if (TreeBlackBoard.CurrentHealth < 0)
            {

                TreeBlackBoard.IsDead = true;
                if (IsBossFellow)
                {
                    Destroy(EnemyObj);
                }
                else
                {
                    RigDollManager.ActivateRigDoll();

                    if (EnableQuestSYSTEM)
                    {
                        EnableQuestSYSTEM = false;
                        EventBusManager.Instance.NonParamPublish(EventBusManager.EventType.KillEnemyTask);
                    }
                    Destroy(EnemyObj, 2f);
                }
                
            }
            else
            {
                TreeBlackBoard.CurrentHealth -= amount;
            }
            
           
            
        }

        public void BossTakeDamage(float amount, string transformName)
        {
            Debug.Log("takedamage" + amount + "hitpos" + transformName);
            if (TreeBlackBoard.CurrentHealth < 0)
            {
                TreeBlackBoard.IsDead = true;
                EventBusManager.Instance.NonParamPublish(EventBusManager.EventType.KillBoss);

                Destroy(EnemyObj, 2f);
            }
            else
            {
                TreeBlackBoard.CurrentHealth -= amount;
            }
            bossHealthBindableTProperty.SetValue(TreeBlackBoard.CurrentHealth, EventBusManager.EventType.BossHealthChange);

        }

        public void BossCreateFellowEnemy(Vector3 generateFellowPos)
        {
            var enemy = Instantiate(BossFellow, generateFellowPos,Quaternion.identity);
            var behavior = enemy.GetComponent<BehaviorTreeManger>();
            behavior.PlayerObj = PlayerObj;
            behavior.IsBossFellow = true;
        }
    }
}
