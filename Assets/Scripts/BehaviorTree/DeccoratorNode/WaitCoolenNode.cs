using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class WaitCoolenNode : DecoratorNode, BehaviorTreeFunInterface
{
    public float CoolenTime = 1f;
    private float _starttime;
    private bool StartCollen = false;
    public void OnStart()
    {
    }

    public void OnStop()
    {
    }

    public AllState OnUpdate()
    {
        if (!StartCollen)
        {
            _starttime = Time.time;
        }

        if (Time.time - _starttime > CoolenTime)
            StartCollen = false;
        if (Time.time - _starttime > CoolenTime || !StartCollen)
        {
            switch (ChildNode.UpdateState())
            {
                case AllState.Running:
                    StartCollen = false;
                    return AllState.Running;
                    break;
                case AllState.Failure:
                    StartCollen = false;
                    return AllState.Failure;
                    break;
                case AllState.Success:
                    StartCollen = true;
                    _starttime = Time.time;
                    return AllState.Success;
                    break;

            }
        }

        if (StartCollen)
        {
            TreeBlackBoard.waitingcoolen = true;
        }
        else
        {
            TreeBlackBoard.waitingcoolen = false;
        }
            return AllState.Running;




    }

    public string GetNodeInfoAsString()
    {
        string childinfo = "";
        if (ChildNode != null)
        {
            BehaviorTreeFunInterface chiInterface = ChildNode as BehaviorTreeFunInterface;
            childinfo = chiInterface?.GetNodeInfoAsString();
        }
        string info = $"Title: {titlename}\nWidth: {width}\nHeight: {height}\nPositionX: {positionx}\nPositionY: {positiony}\n" +
                      $"NodeType: {nodetype}\nIsExcuted:{IsExcuted}\nChildNode info below:\n{childinfo}\n";
        return info;
    }
}
