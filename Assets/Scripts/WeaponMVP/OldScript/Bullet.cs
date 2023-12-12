using UnityEngine;


    public class Bullet : MonoBehaviour
    {
        private BulletPool _bulletPool;
        private BulletData _bulletData;
        float  _lifestartTime;
        float _verticalSpeed;
        private RaycastHit _hitinfo;
        private Vector3 _shootdirection,_desVector3,_starVector3,_oldpos;
        public void Init(BulletData bulletData,BulletPool bulletPool)
        {
            _bulletPool = bulletPool;
            _bulletData = bulletData;
            _verticalSpeed = _bulletData.VerticalSpeed;
            _lifestartTime = Time.time;
            _shootdirection = _desVector3 - _starVector3;
            transform.position = _starVector3;
//           Debug.LogFormat("{0}init create time{1}",this.name,_lifestartTime);
        }
        private void Update()
        {
            _oldpos = transform.position;
            // 计算发射方向

            transform.position += _shootdirection.normalized * _bulletData.Floatspeed * Time.deltaTime;
            
            Gravity();
            //printall();
            Ray ray = new Ray(transform.position, (transform.position-_oldpos).normalized);
            float rayDistance = 0.05f;

            // 检测射线是否与物体相撞
            if (Physics.Raycast(ray, out _hitinfo, rayDistance))
            {
                // Debug.Log("is ground");
                object[] info = { _hitinfo };
                //NotificationCenter.Instance.PostNotification("bullet hit",info);
                _lifestartTime = -100f;
            }
            if (Time.time - _lifestartTime > _bulletData.LifeTime)
            {
                if (_bulletPool != null)
                {
                   //Debug.Log(this.name + " destory time"+Time.time);
                    _bulletPool.Destory(gameObject);
                }
                else
                {
                    Debug.Log(gameObject.name + "bullpoll is null");
                }

            }

        }
        void Gravity()
        {
            Vector3 worldY = transform.InverseTransformDirection(Vector3.up);
            Vector3 moveDirectionY;
            moveDirectionY = _shootdirection;
            moveDirectionY.x = 0;
            moveDirectionY.z = 0;
            _verticalSpeed -= _bulletData.BulletGravity * Time.deltaTime;

            transform.position += new Vector3(0f, _verticalSpeed * Time.deltaTime, 0f);
        }

    //void hitemit(object[] data)
    //{
    //    RaycastHit intmp = (RaycastHit)data[0];
    //    HitEffect.transform.forward = intmp.normal;
    //    HitEffect.transform.position = intmp.point;
    //    HitEffect.Emit(1);
    //}
}




