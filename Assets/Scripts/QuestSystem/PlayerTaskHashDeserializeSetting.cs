using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class PlayerTaskHashDeserializeSetting : JsonConverter
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
        Debug.Log("PlayerTaskHashDeserializeSetting" + jsonObject);
        return null;
    }

    public override bool CanConvert(Type objectType)
    {
        return typeof(Dictionary<int,TaskInfo>).IsAssignableFrom(objectType);
    }
}
