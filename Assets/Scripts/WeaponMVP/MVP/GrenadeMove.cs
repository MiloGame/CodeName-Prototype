using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GrenadeMove : MonoBehaviour
{
    public float moveSpeed = 5f;
    Vector3[] _path;
    private int t = 0;
    private GameObject explodeGameObject;
    private RaycastHit info;
    private GameObject hitGameObject;
    private Vector3 castori;
    private SphereCollider grenderCollider;

    private bool ishit=false;
    private bool hasexplode=false;
    private float rayradis = 0.5f;

    void Start()
    {
        //grenderCollider = transform.GetComponent<SphereCollider>();
    }
    // Start is called before the first frame update
    public void Init(Vector3[] path,GameObject exploGameObject)
    {
        _path = path;
        t = 0;
        explodeGameObject = exploGameObject;
        
    }

    // Update is called once per frame
    void Update()
    {
        castori = transform.position;
        //Vector3 castforward = transform.forward;
        //Vector3 castdirback = -transform.forward;
        //Vector3 castdirleft = -transform.right;
        //Vector3 castdirright = transform.right;
        //Vector3 castdirup = transform.up;
        //Vector3 castdirdown = -transform.up;
  
        //if (collidedetect(castforward,rayradis,rayDistance,ref hitGameObject) ||
        //    collidedetect(castdirback, rayradis, rayDistance, ref hitGameObject) ||
        //    collidedetect(castdirleft, rayradis, rayDistance, ref hitGameObject) ||
        //    collidedetect(castdirright, rayradis, rayDistance, ref hitGameObject) ||
        //    collidedetect(castdirdown, rayradis, rayDistance, ref hitGameObject) ||
        //    collidedetect(castdirup, rayradis, rayDistance, ref hitGameObject))
        //{
        //    //Debug.Log(hitGameObject.transform.name);
        //    //Debug.Log(~(1 << 8 | 1 << 6 | 1 << 3) + "mask");
        //    //hitGameObject = info.transform.gameObject;
        //    ishit = true;
        //}
        //if (Physics.CheckSphere(grenderCollider.center,grenderCollider.radius+rayradis, out info,~(1 << 8 | 1 << 6 | 1 << 3)))
        //{
        //    ishit = true;

        //}
        var colliders = Physics.OverlapSphere(transform.position,  rayradis,
            ~(1 << 8 | 1 << 6 | 1 << 3));
        if (colliders.Length!=0)
        {
            //Debug.Log("grenade radius" + grenderCollider.radius + rayradis+"grenade center"+ transform.position);
            //Debug.Log("grenade hit objS" + colliders.ToString());
            //foreach (var collider1 in colliders)
            //{
            //    Debug.Log("grenade hit objS" + collider1.name);

            //}
            ishit = true;
        }
        else
        {
            ishit = false;
            hasexplode = false;
            if (t < _path.Length)
            {

                transform.position = _path[t];
                t++;
                
            }
            else
            {
                //Debug.Log("move  to thr end");
                GameObject explode = Instantiate(explodeGameObject, transform.position, transform.rotation);
                var hitdamages = explode.GetComponentsInChildren<ParticleHitEnemy>();
                foreach (var hitdamage in hitdamages)
                {
                    hitdamage.damage = 3f;
                }
                Destroy(gameObject);
                Destroy(explode, 2f);
            }
        }

        if (ishit&&hasexplode==false)
        {
            hasexplode = true;
            Destroy(gameObject);
            StartCoroutine(MyCoroutine());
            //Debug.Log("detect collision");
            GameObject explode = Instantiate(explodeGameObject, transform.position, transform.rotation);
            var hitdamages = explode.GetComponentsInChildren<ParticleHitEnemy>();
            foreach (var hitdamage in hitdamages)
            {
                hitdamage.damage = 3f;
            }
            Destroy(explode, 2f);
        }

    }

    bool collidedetect(Vector3 direction,float detectdistance,float castradius,ref GameObject hitGameObject)
    {
        if (Physics.SphereCast(castori, castradius, direction, out info, detectdistance,~(1<<8 | 1<<6|1<<3)))
        {
            hitGameObject = info.transform.gameObject;
            return true;
        }

        return false;
    }
    IEnumerator MyCoroutine()
    {
        // ÑÓÊ±
        yield return new WaitForSeconds(2f);
        
    }

}
