
using System;
using System.Collections.Generic;
using Assets.Scripts.BehaviorTree;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;


public class EnemyManagerObstale : MonoBehaviour
{
    public List<GameObject> EnemyPrefabs;
    public GameObject player;
    public GameObject generatepos;
    private int cnt = 0;
    public ShowMessage sm;
    public Queue<BehaviorTreeManger> behaviormanager;
    public bool isgameover;
    public int index;

    public TriggerAREA.OnSetTarigger onSetTarigger;
    // Start is called before the first frame update
    void Start()
    {
        behaviormanager = new Queue<BehaviorTreeManger>();
        
    }

    public void CreateEnemyInstance()
    {
        var randompos = Random.insideUnitCircle * Random.Range(9, 20);
        generatepos.transform.position = player.transform.position + new Vector3(randompos.x, 0, randompos.y);
        index =Random.Range(0,2);
     
       
        var enemyPrefab = EnemyPrefabs[index];
        var enemyinstance = Instantiate(enemyPrefab, generatepos.transform.position, Quaternion.identity);
        enemyinstance.tag ="Enemy";
        //enemyinstance.AddComponent<BehaviorTreeManger>();
        var behaviormanagerInstance = enemyinstance.GetComponent<BehaviorTreeManger>();

        //behaviormanagerInstance.AiAgent = gameObject.GetComponent<NavMeshAgent>();
        //behaviormanagerInstance.EnemyObj = gameObject;
        behaviormanagerInstance.PlayerObj = player;
        behaviormanagerInstance.Init();
        behaviormanager.Enqueue(behaviormanagerInstance);
    }

    // Update is called once per frame
    void Update()
    {
        //if (behaviormanager.Count >0 && cnt < 3)
        //{
        //    AIFresh();
        //    isgameover = false;
        //}
        //else
        //{
        //    GameOver();
        //    isgameover = true;
        //}
        if (isgameover)
        {
            GameOver();
        }
        else
        {
            
            if (behaviormanager.Count > 0 && cnt < 3)
            {
                AIFresh();
                isgameover = false;
            }
            
     
        }

        if (cnt==3)
        {
            isgameover = true;
            cnt = 0;
        }
        //RestartGame();
        if (Input.GetKeyDown(KeyCode.B))
        {
          
                CreateEnemyInstance();
            
        }
    }

    public void SetCallBack(TriggerAREA.OnSetTarigger onSet)
    {
        onSetTarigger = onSet;
    }
    public void RestartGame()
    {
        //if (isgameover)
        //{
        //if (Input.GetKeyDown(KeyCode.J))
        //{
            isgameover = false;
            onSetTarigger.Invoke();
        cnt = 0;
                for (int i = 0; i < 8; i++)
                {
                    CreateEnemyInstance();
                }
        //    }
        //}
    }

    private void AIFresh()
    {
        var queueelem = behaviormanager.Dequeue();
        var enemyhealth = queueelem.GetComponent<EnemyHealthManager>();
        if (!enemyhealth.isdead)
        {
            queueelem.Fresh();
            behaviormanager.Enqueue(queueelem);
        }
        else
        {
            KillEnemyInstance(queueelem);
        }
    }

    private void KillEnemyInstance(BehaviorTreeManger queueelem)
    {
        cnt++;

        Destroy(queueelem.transform.gameObject, 3f);
    }

    private void GameOver()
    {
        sm.showMessage("You have killed" + cnt + "  Enemy GameOver. Press J to Restart");
        while (behaviormanager.Count > 0)
        {
            var queueelem = behaviormanager.Dequeue();
            queueelem.GetComponent<EnemyHealthManager>().Die();
            Destroy(queueelem.transform.gameObject, 2f);
        }
    }
}
