using System.Collections.Generic;
using Assets.Scripts.BehaviorTree;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.PlayerLoop;

#pragma warning disable IDE0052

namespace Assets.Scripts
{
    public class LoadManager : MonoBehaviour
    {
        //MVC
        public GunView gunViewInstance;
        public GunModel gunModelInstance;
        public GunControler GunControlerInstance;

        public Rig[] AimRig;
        public GameObject FpsGameObject;
        public GameObject TpsGameObject;
        public GameObject LookAtGameObject;
        public GameObject CameraBase;
        //public PlayerControler _pc;
        HUDControler _hc;
        public CamerBehavior _cc;
        MiniMaoManager _mc;
        public ShowMessage Sm;
        public List<SkillTreeManager> _skillTreeManagers;
        GameObject _message;
        GameObject _playerObject;
        GameObject _camObject;
        GameObject _minicamObject;
        //状态机
        private AnimationState _downbodystate;

        public AnimatorPlayer _ap;
        private AnimationData _anidData;
        //枪械状态机
        private AnimationState GunState;
        public GameObject Gunobj;
        private AnimatorPlayer gunAnimatorPlayer;

        public Canvas Hud;

        public delegate void MyEventHandler();
        public event MyEventHandler cameraFreshEvent, fireEvent;
        //input status

        private AnimationState state;
        private AnimationState humanweaponstate;
        private AnimationState skillstate;
     
        public GameObject GunPiv;
        // Start is called before the first frame update
        private void Awake()
        {
            Init();
        }
        private void Start()
        {
            //CreatInstance();

            ////_pc.MouseInvisible();
            //foreach (var treeManager in _skillTreeManagers)
            //{
            //    treeManager.Init();
            //}
            //GunControlerInstance.Init();
        }


        private void Update()
        {
            //_ap.GetAnimator.SetLayerWeight(1, 1);
            //_pc.PlayerMoveFresh();
            //cameraFreshEvent();
            //if (_cc.GetCameraMode ==1)
            //{
            //    foreach (var rig in AimRig)
            //    {
            //        rig.weight = 0;
            //    }
            //}
            //else
            //{
            //    foreach (var rig in AimRig)
            //    {
            //        rig.weight = 1;
            //    }
            //}
            ////并发状态机入口
            ////AnimationHandle();
            //GunControlerInstance.FreshEvent();

            //_hc.Init();
            //foreach (var treeManager in _skillTreeManagers)
            //{
            //    treeManager.Fresh();
            //}
        }
        private void LateUpdate()
        {
            //    AnimationHandle();


            //NotificationCenter.Instance.PostNotification("MiniMapFresh");
        }

        //获得private player
        public GameObject Getplayer
        {
            get
            {
                return this._playerObject;
            }
        }

        //获得private cam
        public GameObject Getcam
        {
            get
            {
                return this._camObject;
            }
        }
        public GameObject Getminicam
        {
            get
            {
                return this._minicamObject;
            }
        }

        public GameObject GetHudCamera
        {
            get
            {
                return GameObject.Find("HUDCamera");
            }
        }
        private void Init()
        {
            _playerObject = GameObject.FindGameObjectWithTag("Player");
            _camObject = GameObject.Find("Main Camera");
            _minicamObject = GameObject.Find("MiniMapCamera");
            Hud =GameObject.FindWithTag("HUD").GetComponent<Canvas>();
            _message = GameObject.Find("Message");
        }
        //对组合件制作整体collider
        //void generateCollider(GameObject target)
        //{
        //    Vector3 originalPosition;
        //    Quaternion originalRotation;
        //    // 保存父物体的位置和旋转
        //    originalPosition = target.transform.position;
        //    originalRotation = target.transform.rotation;
        //    // 设置父物体的位置和旋转为零
        //    target.transform.position = Vector3.zero;
        //    target.transform.rotation = Quaternion.identity;
        //    // 获取子物体的 Mesh Filter 组件
        //    MeshFilter[] childMeshFilters = target.GetComponentsInChildren<MeshFilter>();

        //    // 创建合并后的网格
        //    CombineInstance[] combine = new CombineInstance[childMeshFilters.Length];
        //    for (int i = 0; i < childMeshFilters.Length; i++)
        //    {
        //        combine[i].mesh = childMeshFilters[i].sharedMesh;
        //        combine[i].transform = childMeshFilters[i].transform.localToWorldMatrix;
        //    }

        //    // 创建父物体的 Mesh Collider 组件
        //    target.AddComponent<MeshCollider>();
        //    MeshCollider meshCollider = target.GetComponent<MeshCollider>();
        //    // 创建合并后的网格并设置给 Mesh Collider
        //    Mesh combinedMesh = new Mesh();
        //    combinedMesh.CombineMeshes(combine, true, true);
        //    meshCollider.sharedMesh = combinedMesh;
        //    // 恢复父物体的位置和旋转
        //    target.transform.position = originalPosition;
        //    target.transform.rotation = originalRotation;
        //    // 设置 Mesh Collider 的凸性
        //    meshCollider.convex = true;
        //    meshCollider.isTrigger = true;
        //}
        //void FindAll(GameObject root)
        //{
        //    if (root.transform.childCount==0)
        //    {
        //        return;
        //    }

        //    //root.GetComponent<CapsuleCollider>().isTrigger = true;
        //    for (int i=0;i<root.transform.childCount;i++)
        //    {
        //        GameObject child = root.transform.GetChild(i).gameObject;
        //        MeshCollider[] meshColliders = child.GetComponents<MeshCollider>();
        //        if (meshColliders.Length!=0)
        //        {
        //            foreach (var element in meshColliders)
        //            {
        //                element.convex = true;
        //            }
        //        }
        //        child.GetComponent<Collider>().isTrigger = true;

        //        FindAll(child);

        //    }

        //}
 
        //create Instance
        private void CreatInstance()
        {
            _cc = new CamerBehavior(this);
            _hc = new HUDControler(this.Hud);
            _mc = new MiniMaoManager(this);
            _anidData = new AnimationData(this);
           //_ap = _playerObject.GetComponent<AnimatorPlayer>();
            //_pc = new PlayerControler(this);

          
            //if (_ap == null)
            //{
            //    Debug.Log("nullllllll");
            //}
            //else
            //{
            //    Debug.Log("nullllllll"+ _ap.GetType());
            //    _downbodystate = _anidData.CreateState(AnimationData.AnimationType.IdleAnimateState,_ap,_pc,_cc);
            //}



            _skillTreeManagers = new List<SkillTreeManager>();
            _skillTreeManagers.Add(new SkillTreeManager("GetHeal"));
            _skillTreeManagers.Add(new SkillTreeManager("GetHurt"));
            _skillTreeManagers.Add(new SkillTreeManager("SheldEffect"));

            gunModelInstance = new GunModel();
            GunControlerInstance = new GunControler(gunViewInstance, gunModelInstance);
        }
        

        void AnimationHandle()
        {
            //AnimationState state = _downbodystate.HandleInput();
            state = _downbodystate.HandleInput();
          //  Debug.Log("enter animation handler state" + _downbodystate.StateParams.FrameName);

            if (state!=null)
            {
                _downbodystate = state;
            }
            _downbodystate.Enter();
            
        }

    }

}
