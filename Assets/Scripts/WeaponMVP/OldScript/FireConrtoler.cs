
using Assets.Scripts;
using UnityEngine;


    public class FireConrtoler
    {
        private GameObject _pool;
        private BulletPool _bullpool;
        GameObject _player;
        GameObject _mainCamera;
        GameObject _hudcamGameObject;
        GameObject _grenadeprefab;
        GameObject _blodeprefab;
        LoadManager lm;
        float _throwcircle = 10f;
        int _bezierPointnums = 100;
        public LineRenderer Linerender;
        bool _isThrow = false;
        Vector3[] _path;
        private int maxbullets = 35;
        private int _fireMode = 0;
        // Start is called before the first frame update
        public FireConrtoler(LoadManager LM)
        {
            lm = LM;
            _player = lm.Getplayer;
            _mainCamera = lm.Getcam;
            _hudcamGameObject = lm.GetHudCamera;
            _grenadeprefab = Resources.Load<GameObject>("Prefabs/Grenade");
            _blodeprefab = Resources.Load<GameObject>("Prefabs/BigExplosion");//
            _path = new Vector3[_bezierPointnums];
            _pool = new GameObject("BulletPool");
            _bullpool = _pool.GetComponent<BulletPool>();
            //_wp = new Weapon(_mainCamera,_player,_bullpool);
            //lm.fireEvent += chooseFireMode;
            lm.fireEvent += Grenade;

        }

        void Grenade()
        {
            Vector3 beginThrow = _player.transform.Find("Armature/cMainH/Spine0/Spine1/Spine2/Spine3/cShrugger.R/Bicep.R/Elbow.R/4arm.R/Hand.R/weapon.R/RayCastShootOrigin").position;//¹Òµ½½ÇÉ«ÉÏ
            Vector3 endThrow = _player.transform.position + _player.transform.forward * _throwcircle;
            endThrow.y = 0;
            Vector3 ControlPoint = (beginThrow + endThrow) * 0.5f + (Vector3.up * 10f);
        
            if (Input.GetKey(KeyCode.Z))
            {
            
                _isThrow = true;
                for (int i = 0; i < _bezierPointnums; i++)
                {
                    float t = i / (float)(_bezierPointnums-1);
                    _path[i] = GetBezierPoint(t, beginThrow, ControlPoint, endThrow);
                    //Debug.Log(_path[i]+"press shift");
                }
                Linerender.positionCount = _path.Length;
                Linerender.SetPositions(_path);
            }
            else
            {
                if (_isThrow)
                {

                    GameObject res = GameObject.Instantiate(_grenadeprefab);
                    res.AddComponent<GrenadeMove>();
                    res.transform.position = beginThrow;
                    res.GetComponent<GrenadeMove>().Init(_path, _blodeprefab);
                    Linerender.positionCount = 0;
                }
                _isThrow = false;
            }
        
 
        }
        static Vector3 GetBezierPoint(float t, Vector3 start, Vector3 center, Vector3 end)
        {
            return (1 - t) * (1 - t) * start + 2 * t * (1 - t) * center + t * t * end;
        }



    }

