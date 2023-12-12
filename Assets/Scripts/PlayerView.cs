using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    public float viewRadius;
    public float radiusangle;
    public TimerManager TmManager;
    public bool CanSearch = true;
    public Collider[] targetInRadius;
    public float distanceTarget;

    void Update()
    {
        radiusangle = Mathf.Clamp(radiusangle, 0, 360f);
        FindEnemy();
        //if (CanSearch)
        //{
        //    CanSearch = false;
        //    FindEnemy();
        //}
    }

    void FindEnemy()
    {
        targetInRadius = Physics.OverlapSphere(transform.position, viewRadius, (1 << 10));
        for (int i = 0; i < targetInRadius.Length; i++)
        {
            Transform tmpenemy = targetInRadius[i].transform;
            EnemyHealthUIManger enemyhealthbar = tmpenemy.GetComponent<EnemyHealthUIManger>();

            Vector3 targetDir = (tmpenemy.position - transform.position).normalized;
            if (Vector3.Angle(targetDir, transform.forward) < radiusangle / 2) // angle在两个向量同向的时候返回0~180的值
            {
                distanceTarget = Vector3.Distance(transform.position, tmpenemy.position);
                RaycastHit hitinfo;

                if (!Physics.Raycast(transform.position, targetDir, out hitinfo, distanceTarget, ~(1 << 8 | 1 << 10)))
                {
                    enemyhealthbar?.EnableHealthbar();
                }
              
            }
            else
            {
                enemyhealthbar?.DisableHealthbar();
                enemyhealthbar?.UnSetChaseBarPos();
                enemyhealthbar?.UnSetQuestionBarPos();
            }
        }

        //TmManager.StartTimer(.2f, OnCoolen, 11);
    }

    public Vector3 EdiCalRadius(float angles)
    {
        angles += transform.rotation.eulerAngles.y;

        return new Vector3(Mathf.Sin(angles * Mathf.Deg2Rad), 0, Mathf.Cos(angles * Mathf.Deg2Rad));
    }
    //private void OnCoolen()
    //{
    //    CanSearch = true;
    //}
}
