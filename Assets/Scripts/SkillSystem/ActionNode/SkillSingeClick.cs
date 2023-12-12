

using System;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class SkillSingeClick : SkillActionNode,FuncInterface
{
    public string KeyNameToDetect = "";
    public bool OutputStatus = false;
    public string OutputMessage = "";
    public int index = 0;
    public Dictionary<string, UnityEngine.KeyCode> keyMapDictionary=new Dictionary<string, UnityEngine.KeyCode>();

    public SkillSingeClick()
    {
        if (keyMapDictionary == null || keyMapDictionary.Count == 0)
        {
            keyMapDictionary["mouseleft"] = KeyCode.Mouse0;
            keyMapDictionary["mouseright"] = KeyCode.Mouse1;
            keyMapDictionary["h"] = KeyCode.H;
            keyMapDictionary["k"] = KeyCode.K;
            keyMapDictionary["q"] = KeyCode.Q;
            keyMapDictionary["e"] = KeyCode.E;
            keyMapDictionary["j"] = KeyCode.J;
        }

    }

    public void OnStart()
    {
        OutputStatus = false;
    }

    public void OnStop()
    {
       
    }

    public SkillNodeAllState OnUpdate()
    {
        if (Input.GetKeyDown(keyMapDictionary[KeyNameToDetect]))
        {
            OutputStatus = true;
            OutputMessage = "detect" + index;
            index++;
            return SkillNodeAllState.Success;

        }
        else
        {
            OutputStatus = false;
            OutputMessage = "not detect" + index;
            index++;
            return SkillNodeAllState.Failure;

        }

        return SkillNodeAllState.Success;
    }
}
