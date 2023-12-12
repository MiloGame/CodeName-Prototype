using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEmmit : MonoBehaviour
{
    public List<ParticleSystem> pcParticleSystem;

    public MVP_GunPresenter Presenter;
    private int cnt = 0; 

    void OnEnable()
    {
        string message = "OnEnable:" + cnt;
        Presenter.OnRecall(message);
    }
    void OnDisable()
    {
        string message = "Ondisable:" + cnt;
        Presenter.OnRecall(message);
    }


    // Update is called once per frame
    void Update()
    {
        cnt++;
        string message = "update:" + cnt;
        Presenter.OnRecall(message);
        //if (Input.GetKeyDown(KeyCode.Mouse0))
        //{
        //    foreach (var system in pcParticleSystem)
        //    {
        //        system.Emit(1);
        //    }
        //}

    }
}
