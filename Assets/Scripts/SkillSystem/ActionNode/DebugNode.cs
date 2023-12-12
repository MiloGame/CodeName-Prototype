
using System;
using UnityEngine;
[Serializable]
public class DebugNode : SkillActionNode , FuncInterface
{
    public string Message = "";
    public bool isecute;
    public object aaa;
    public void OnStart()
    {
        isecute = true;
        //Debug.Log("debug on start called");
    }

    public void OnStop()
    {

        //Debug.Log("on stop called");
    }

    public SkillNodeAllState OnUpdate()
    {
        //Debug.Log(Message)/*;*/
        return SkillNodeAllState.Success;
    }


}
