using System;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.Oculus.Input;
using UnityEngine;

public class testtimer : MonoBehaviour
{
    public TimerManager tm;

    public event Action ontimer;
    public void Start()
    {
        ontimer += CountFinish;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            //tm.StartTimer(5f,ontimer);
            //Debug.Log("main called timer");
        }
    }

    public void CountFinish()
    {
        Debug.Log("count finish");
    }
}
