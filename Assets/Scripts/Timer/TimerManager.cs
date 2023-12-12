using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using System;
using System.Linq;
using System.Threading;
using System.Xml;
using Cysharp.Threading.Tasks;
public class TimerManager : MonoBehaviour
{


    public struct CtsInfo
    {
        public int id;
        public bool IsRunning;
        public CancellationTokenSource cts;
        public float currenttime;

        public float durtiontime;
    }
    List<CtsInfo> ctsInfos = new List<CtsInfo>();

    public CtsInfo CreatCts(int taskidx,float starttime,float lasting)
    {
        var cts = new CancellationTokenSource();
        var ci = new CtsInfo { cts = cts, id = taskidx ,IsRunning = true,currenttime = starttime,durtiontime = lasting};

        ctsInfos.Add(ci);
        return ci;
    }
    public void CancelAllTask()
    {
        ctsInfos.ForEach(ci =>
        {
            ci.cts?.Cancel();
            ci.cts?.Dispose();
        });
        ctsInfos.Clear();
    }



    public void CancelTask(int id)
    {
       // Debug.Log(ctsInfos.Count +"before cancel");

        var toremoveitem = ctsInfos.Find(x => x.id == id);
        toremoveitem.cts?.Cancel();
        toremoveitem.cts?.Dispose();
        ctsInfos.Remove(toremoveitem);
       // Debug.Log(ctsInfos.Count + "after cancel");
    }


    public void StartTimer(float lasttime,Action onAction,int taskid)
    {
        foreach (var i in ctsInfos)
        {
            if(i.id == taskid)
                return;
        }

        var taskinfo = CreatCts(taskid,0f,lasttime);
        //Debug.Log(taskinfo+"start time "+Time.time);
        TimerCount(taskinfo,onAction);
    }
    async UniTask TimerCount(CtsInfo info,Action callbackAction)
    {
        while (info.currenttime < info.durtiontime && info.IsRunning)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(0.1), ignoreTimeScale: false,cancellationToken:info.cts.Token);
            info.currenttime += 0.1f;
        }
        //Debug.Log(info+"count finish lasting "+Time.time);
        info.IsRunning = false;
        CancelTask(info.id);
        callbackAction?.Invoke();
    }
   
    public void OnDisable()
    {
        CancelAllTask();
    }

    public void OnDestory()
    {
        CancelAllTask();
    }
}