using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour
{
    private float lifetime;
    private MVP_GunPresenter mvpGunPresenter;
    public bool IsAlive;
    private float gravity;
    private Vector3 _shootdirection;
    private float _verticalSpeed;
    private Vector3 _desVector3;
    private Vector3 _starVector3;
    private float movespeed;
    private RaycastHit _hitinfo;
    private bool detecthit;
    private float thereshold=0.5f;
    private Vector3 _oldpos;
    private Vector3 _oldhitpoint;
    public void Init(float life,MVP_GunPresenter presenter, 
        Vector3 shotstart, Vector3 shotend,float speed,float gravityval)
    {
        
        lifetime = life;
        mvpGunPresenter = presenter;
        _starVector3 = shotstart;
        _desVector3 = shotend;
        movespeed = speed;
        gravity = gravityval;
        _oldpos = _starVector3;
        transform.position = _starVector3;
        if (transform.position!=_starVector3)
        {
            IsAlive = false;
        }
        else
        {
            IsAlive = true;
            transform.GetComponent<TrailRenderer>().enabled = true;
        }

    }


    // Update is called once per frame
    public void Update()
    {
        if (IsAlive)
        {
            lifetime -= Time.deltaTime;
            BulletMove();
            IsHitSth();
            //transform.GetComponent<TrailRenderer>().enabled = true;

            if (lifetime <= 0 || detecthit)
            {
                IsAlive = false;
                //transform.GetComponent<TrailRenderer>().enabled = false;
                mvpGunPresenter.DestoryBullet(this.gameObject,mvpGunPresenter.gunModel.currentGunType);
            }
            _oldpos = transform.position;
        }
 
           
        
        
        
        
    }
    public void IsHitSth()
    {
        
        Ray ray = new Ray(_oldpos, (transform.position - _oldpos).normalized);
        Debug.DrawLine(ray.origin,transform.position,Color.white,5f);
        if (Physics.Raycast(ray, out _hitinfo, Vector3.Distance(_oldpos,transform.position), ~(1 << 8 & 1 << 6)))
        {
            //Debug.Log("HIT NAME origin" + _hitinfo.transform.name);

            //var distance = Vector3.Distance(_hitinfo.point, transform.position);
            //if (distance < thereshold) detecthit = true;
            //else detecthit = false;

            var HitScript = _hitinfo.transform.GetComponent<HitBox>();
            HitScript?.OnHit(mvpGunPresenter.gunModel.bulletdamage);
            var HitScript1 = _hitinfo.transform.GetComponent<BossHitBox>();
            HitScript1?.OnHit(mvpGunPresenter.gunModel.bulletdamage);
            var HitScript3 = _hitinfo.transform.GetComponent<NormalAiHitBox>();
            HitScript3?.OnHit(mvpGunPresenter.gunModel.bulletdamage);
            var HitScript2 = _hitinfo.transform.GetComponent<NpcManager>();
            HitScript2?.OnHit(mvpGunPresenter.gunModel.bulletdamage);
            detecthit = true;
            mvpGunPresenter.PlaceHitEffect(_hitinfo);
        }
        else
        {
            detecthit = false;
        }
        //Vector3 moveDir = _oldhitpoint - transform.position;

        //if (Vector3.Dot(moveDir.normalized, ray.direction.normalized) <= 0)
        //{
        //    detecthit = true;
        //}
        //else
        //{
        //    detecthit = false;
        //}
        //Debug.DrawRay(transform.position, moveDir, Color.red, 40f);
        //Debug.Log(Vector3.Dot(moveDir, ray.direction) + "moveDir" + moveDir + "raydir" + ray.direction);

    }
    private void BulletMove()
    {
        _shootdirection = _desVector3 - _starVector3;

        transform.position += _shootdirection.normalized * movespeed * Time.deltaTime;

        Gravity();
    }
    void Gravity()
    {
        Vector3 moveDirectionY;
        moveDirectionY = _shootdirection;
        moveDirectionY.x = 0;
        moveDirectionY.z = 0;
        _verticalSpeed -= gravity * Time.deltaTime;

        transform.position += new Vector3(0f, _verticalSpeed * Time.deltaTime, 0f);
    }
}
