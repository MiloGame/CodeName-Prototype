using UnityEngine;

public class BulletView : MonoBehaviour 
{
    public BulletData bulletprop;
    public float startlivetime;
    public GunControler.OnDestory OnDestory;
    public GunView.OnHit OnHit;
    public Vector3 _shootdirection, _desVector3, _starVector3, _oldpos;
    private float _verticalSpeed;
    private Transform _desTransform;
    private bool isautoaim;
    private RaycastHit _hitinfo;
    private bool detecthit = true;
    public void SetBulletProp(BulletData bd,GunControler.OnDestory onDestory,Vector3 shotstart,Vector3 shotend,GunView.OnHit onHit)
    {
        detecthit = true;
        OnHit = onHit;
        bulletprop = bd;
        OnDestory = onDestory;
        _starVector3 = shotstart;
        _desVector3 = shotend;
        transform.position = shotstart;
        //gameObject.GetComponent<TrailRenderer>().enabled = true;
        gameObject.SetActive(true);
        _verticalSpeed = 0f;
        startlivetime = Time.time;
        isautoaim = false;
    }
    public void SetBulletProp(BulletData bd,GunControler.OnDestory onDestory,Vector3 shotstart,Transform followTransform,GunView.OnHit onHit)
    {
        detecthit = true;
        OnHit = onHit; 
        bulletprop = bd;
        OnDestory = onDestory;
        _starVector3 = shotstart;
        _desTransform = followTransform;
        transform.position = shotstart;
        _verticalSpeed = 0f;
        startlivetime =Time.time;
        isautoaim = true;
    }
    public void BulletFireTrail()
    {

    }

    public bool IsAlive()
    {
        if (Time.time - startlivetime < bulletprop.LifeTime)
        {
            return true;
        }
        else
        {
            gameObject.SetActive(false);
            transform.position = _starVector3;
            return false;
        }
    }
    public void IsHitSth()
    {
        
        Ray ray = new Ray(transform.position, (transform.position - _oldpos).normalized);
        Debug.DrawRay(transform.position, (transform.position - _oldpos).normalized,Color.black,20f);
        float rayDistance = 2f;
        if (Physics.Raycast(ray, out _hitinfo, rayDistance))
        {
            //Debug.Log("HIT NAME origin" + _hitinfo.transform.name);
            if (_hitinfo.transform.name!="Player")
            {
                detecthit = false;
                // Debug.Log("is ground");
                OnHit?.Invoke(_hitinfo);
                //transform.gameObject.GetComponent<TrailRenderer>().enabled = false;
                
                //NotificationCenter.Instance.PostNotification("bullet hit",info);
                startlivetime = -100f;
            }
            Debug.Log("ishit"+_hitinfo.transform.name);
            
        }
    }

    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        if (IsAlive())
            BulletMove();
    }

    private void BulletMove()
    {
        _shootdirection = _desVector3 - _starVector3;

        if (isautoaim)
        {
            _desVector3 = _desTransform.position;
        }


        transform.position += _shootdirection.normalized * bulletprop.Floatspeed * Time.deltaTime;

        Gravity();
    }

    void FixedUpdate()
    {
        if (detecthit)
        {
            IsHitSth();
        }
        
        if (!IsAlive())
        {
            
            OnDestory?.Invoke(this.gameObject);
        }
        _oldpos = transform.position;
    }
    void Gravity()
    {
        Vector3 moveDirectionY;
        moveDirectionY = _shootdirection;
        moveDirectionY.x = 0;
        moveDirectionY.z = 0;
        _verticalSpeed -= bulletprop.BulletGravity * Time.deltaTime;

        transform.position += new Vector3(0f, _verticalSpeed * Time.deltaTime, 0f);
    }

}
 