using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAREA : MonoBehaviour
{
    //public EnemyManager em;
    public bool istrigg = false;
    public GameObject PlayreGameObject;
    public delegate void OnSetTarigger();

    public OnSetTarigger onSetTarigger;
    // Start is called before the first frame update
    void Start()
    {
        onSetTarigger += OnSetIsTrigger;
        //em.SetCallBack(onSetTarigger);

    }
    void Update()
    {
        if (istrigg==false)
        {
            Ray ray = new Ray(PlayreGameObject.transform.position, Vector3.down);
            RaycastHit hitinfoHit = new RaycastHit();
            if (Physics.Raycast(ray, out hitinfoHit, 0.05f))
            {

                if (hitinfoHit.transform.tag == "trigarea")
                {
                    //Debug.Log(hitinfoHit.transform.name + "hit me");
                    //for (int i = 0; i < 8; i++)
                    //{
                    //    em.isgameover = false;
                    //    em.CreateEnemyInstance();
                    //    istrigg = true;
                    //}
                    //em.RestartGame();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            istrigg = false;
        }

    }

    public void OnSetIsTrigger()
    {
        istrigg = true;
    }
}
