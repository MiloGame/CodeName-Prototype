using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface BehaviorTreeFunInterface 
{
    public  void OnStart();
    public  void OnStop();
    public  Node.AllState OnUpdate();
    public  string GetNodeInfoAsString();
}
