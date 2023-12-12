using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
[Serializable]
public class TaskItem
{
    [JsonProperty("MainTaskID")]
    public int MainTaskID;
    [JsonProperty("MainTaskTitle")]
    public string MainTaskTitle;
    [JsonProperty("SubTaskId")]
    public int SubTaskId;
    [JsonProperty("SubTaskTotalNum")]
    public int SubTaskTotalNum;
    [JsonProperty("SubTaskTitle")]
    public string SubTaskTitle;
    [JsonProperty("SubTaskDescription")]
    public string SubTaskDescription;
    [JsonProperty("SubTaskTarget")]
    public string SubTaskTarget;
    [JsonProperty("SubTaskNum")]
    public int SubTaskNum;
    [JsonProperty("Award")]
    public string Award;
    [JsonProperty("OpenAnotherTask")]
    public string OpenAnotherTask;
    [JsonProperty("Status")]
    public int Status;
}
