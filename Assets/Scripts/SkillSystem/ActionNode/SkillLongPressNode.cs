

using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks.Triggers;
using UnityEngine;
[Serializable]
public class SkillLongPressNode : SkillActionNode,FuncInterface
{
    public string KeyNameToDetect = "";
    public bool OutputStatus = false;
    public Dictionary<string, UnityEngine.KeyCode> keyMapDictionary = new Dictionary<string, UnityEngine.KeyCode>();
    public float DetectTime=2f;
    public float BeginDetectTime=0;
    public bool CanTrigger;

    public SkillLongPressNode()
    {
        if (keyMapDictionary == null || keyMapDictionary.Count == 0)
        {
            keyMapDictionary["mouseleft"] = KeyCode.Mouse0;
            keyMapDictionary["mouseright"] = KeyCode.Mouse1;
            keyMapDictionary["h"] = KeyCode.H;
            keyMapDictionary["e"] = KeyCode.E;
            keyMapDictionary["k"] = KeyCode.K;
            keyMapDictionary["g"] = KeyCode.G;
        }

    }

    public void OnStart()
    {

    }

    public void OnStop()
    {
    }

    public SkillNodeAllState OnUpdate()
    {
        if (Input.GetKey(keyMapDictionary[KeyNameToDetect]))
        {
            while (BeginDetectTime < DetectTime)
            {
                BeginDetectTime += Time.deltaTime;
                return SkillNodeAllState.Running;
            }
            OutputStatus = true;
            CanTrigger = false;
            return SkillNodeAllState.Success;

        }
        else
        {
            BeginDetectTime = 0;
            OutputStatus = false;
            CanTrigger = true;
        }
        return SkillNodeAllState.Failure;
    }
    
   
}
