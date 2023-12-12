using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEditor.Search;
using UnityEngine;

public class TaskDeserializeSetting : JsonConverter
{
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        JObject jsonObject = JObject.Load(reader);
        //Debug.Log("jsonObject data" + jsonObject);
        //Node.TreeNodeType nodetype = jsonObject["nodetype"].ToObject<Node.TreeNodeType>();
        Debug.Log("jobject"+jsonObject);
        TaskInfo resInfo = new TaskInfo();
        resInfo.TaskID = jsonObject["TaskID"].ToObject<int>();
        resInfo.TaskTitle = jsonObject["TaskTitle"].ToString();
        resInfo.TaskDescription = jsonObject["TaskDescription"].ToString();
        resInfo.UnlockRule = jsonObject["UnlockRule"].ToString();
        resInfo.FinishRule = jsonObject["FinishRule"].ToString();
        if (jsonObject["Award"] == null)
        {
            resInfo.Award = "";
        }
        else
        {
            resInfo.Award = jsonObject["Award"].ToString();
        }

        resInfo.Status = jsonObject["Status"].ToObject<int>();
        resInfo.UnlockRuleList = new List<UnlockRuleBase>();
        resInfo.AchieveRuleList = new List<AchieveRuleBase>();
        if (resInfo.UnlockRule != "-1")
        {
            Debug.Log("unlockrule is not null");
            string[] cells = resInfo.UnlockRule.Split('&');
            foreach (var cell in cells)
            {
                string[] initStrings = cell.Split('%');
                int id = int.Parse(initStrings[0]);
                int initparam = int.Parse(initStrings[1]);
                Debug.Log("init type" + id + "init partam" + initparam);
                //可扩展为自动代码管线，按照完成表的规则进行反序列化
                switch (id)
                {
                    case 20001:
                        resInfo.UnlockRuleList.Add(new UnlockRuleNeedFormerTask(initparam, id));
                        break;
                    case 20002:
                        resInfo.UnlockRuleList.Add(new UnlockRuleChatWithNpc(initparam, id));
                        break;
                    
                }
            }
        }
        else
        {
            Debug.Log("unlockrule is  null");

        }
        string[] FinishRulecells = resInfo.FinishRule.Split('&');
        foreach (var cell in FinishRulecells)
        {
            string[] initStrings = cell.Split('%');
            int id = int.Parse(initStrings[0]);
            int initparam = int.Parse(initStrings[1]);
            Debug.Log("init type" + id + "init partam" + initparam);
            //可扩展为自动代码管线，按照解锁表的规则进行反序列化
            switch (id)
            {
                case 30001:
                    resInfo.AchieveRuleList.Add(new AchieveDefeatNormalEnemyNum(initparam, id));
                    break;
                case 30002:
                    resInfo.AchieveRuleList.Add(new AchieveDefateBoss(initparam, id));
                    break;
                case 30003:
                    resInfo.AchieveRuleList.Add(new AchieveCarryBoxNum(initparam, id));
                    break;
                case 30004:
                    resInfo.AchieveRuleList.Add(new AchieveDiscoverAreaNum(initparam, id));
                    break;
            }
        }
        return resInfo;
    }

    public override bool CanConvert(Type objectType)
    {
        return typeof(TaskInfo).IsAssignableFrom(objectType);
    }
}
