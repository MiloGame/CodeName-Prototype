using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using UnityEngine;
[Serializable]
public class ConvertJsonToTask 
{
    public List<TaskItem> tasklist;
    public TaskItem item1;
    public TaskItem item2;
    public Dictionary<int, Dictionary<int,TaskItem>> TaskCache; // 主任务id 子任务id 任务内容
    private static ConvertJsonToTask _convertJsontoTask;
    public static ConvertJsonToTask Instance
    {
        get
        {
            if (_convertJsontoTask == null)
            {
                _convertJsontoTask = new ConvertJsonToTask();
            }

            return _convertJsontoTask;
        }
        
    }

    public ConvertJsonToTask()
    {
        tasklist = new List<TaskItem>();

        TaskCache = new Dictionary<int, Dictionary<int, TaskItem>>();
        //LoadData("TaskTemplete");
        //item1 = GeTaskItem(1, 2);
        //item2 = GeTaskItem(2, 2);
        //Debug.Log("item2 == null"+ (GeTaskItem(2, 2) == null));
    }
    public void LoadData(string Filename)
    {
        string filepath = Path.Combine(Application.dataPath, "JsonData","Task", Filename + ".json");

        string json = File.ReadAllText(filepath,Encoding.UTF8);
        JsonSerializerSettings settings = new JsonSerializerSettings
        {
            Converters = new List<JsonConverter> { new TaskDeserializeSetting() }
        };
        tasklist  = JsonConvert.DeserializeObject<List<TaskItem>>(json);

        for (int i = 0; i < tasklist.Count; i++)
        {
            if (!TaskCache.ContainsKey(tasklist[i].MainTaskID))
            {
                TaskCache[tasklist[i].MainTaskID] = new Dictionary<int, TaskItem>();
            }
            TaskCache[tasklist[i].MainTaskID].Add(tasklist[i].SubTaskId, tasklist[i]);
        }

    }
    public void SaveData(string Filename , List<TaskItem> saveTaskItems)
    {
        string filepath = Path.Combine(Application.dataPath, "JsonData", "Task", Filename + ".json");
        string json = JsonConvert.SerializeObject(saveTaskItems);
        if (File.Exists(filepath))
        {
            File.Delete(filepath);
            Debug.Log("delete " + filepath);
        }
        File.WriteAllText(filepath, json);


    }
    public TaskItem GeTaskItem(int maintaskid,int subtaskid)
    {
        if (TaskCache.ContainsKey(maintaskid) && TaskCache[maintaskid].ContainsKey(subtaskid))
        {
            Debug.Log("exist");
            return TaskCache[maintaskid][subtaskid];
        }

            Debug.Log("not exist");
            return null;
        
    }

}
