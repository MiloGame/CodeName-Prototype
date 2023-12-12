using Unity.VisualScripting;
using UnityEngine;


public class TestEnySeen : MonoBehaviour
{
    public float viewRadius;
    public float radiusangle;
    public Transform PlayerTransform;
    public TimerManager TmManager;
    public bool CanSearch = true;
    public Collider[] targetInRadius;
    public Transform tmpplayer;
    public float distanceTarget;

    void Update()
    {
        radiusangle = Mathf.Clamp(radiusangle, 0, 360f);
        if (CanSearch)
        {
            CanSearch = false;
            FindPlayer();
        }
    }

    void FindPlayer()
    {
        PlayerTransform = null;
        targetInRadius = Physics.OverlapSphere(transform.position, viewRadius,(1<<8));
        if (targetInRadius.Length!=0)
        {
            tmpplayer = targetInRadius[0].transform;
            Vector3 targetDir = (tmpplayer.position - transform.position).normalized;
            if (Vector3.Angle(targetDir,transform.forward) < radiusangle/2) // angle在两个向量同向的时候返回0~180的值
            {
                distanceTarget = Vector3.Distance(transform.position, tmpplayer.position);
                RaycastHit hitinfo;
                if (!Physics.Raycast(transform.position,targetDir,out hitinfo, distanceTarget, ~(1<<8|1<<10)))
                {
                    PlayerTransform = tmpplayer;
                }
            }
     
        }
        
        TmManager.StartTimer(.2f,OnCoolen,10);
    }

    public Vector3 EdiCalRadius(float angles)
    {
        angles += transform.rotation.eulerAngles.y;

        return new Vector3(Mathf.Sin(angles * Mathf.Deg2Rad),0,Mathf.Cos(angles * Mathf.Deg2Rad));
    }
    private void OnCoolen()
    {
        CanSearch = true;
    }
}
