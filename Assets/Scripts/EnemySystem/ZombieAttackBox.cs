using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAttackBox : MonoBehaviour
{
    public Vector3 boxsize = new Vector3(5, 0, 2f);
    public LayerMask layerMask;
    public bool CanDamage;

    public float atkDamageval;
    // Start is called before the first frame update
    void Start()
    {
        CanDamage = false;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hitinfo;
        if (Physics.BoxCast(transform.position , boxsize / 2, transform.forward,
                out hitinfo, transform.rotation, 2, layerMask))
        {
            PlayerHealthManager pcHealthManager = hitinfo.transform.GetComponent<PlayerHealthManager>();
            if (pcHealthManager!=null&&!CanDamage)
            {
                CanDamage = true;
                StartCoroutine(CoolenAttack());
                pcHealthManager.OnHit(atkDamageval);
                Debug.Log("hit player");
            }
        }
    }

    private IEnumerator CoolenAttack()
    {
        yield return new WaitForSeconds(2);
        CanDamage = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawRay(transform.position , transform.forward * 3);
        Gizmos.DrawWireCube(transform.position, boxsize);
    }
}
