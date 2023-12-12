using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class ConvertNodeJson : JsonConverter
{
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        JObject jsonObject = JObject.Load(reader);
        Debug.Log("jsonObject data" + jsonObject);
        Node.TreeNodeType nodetype = jsonObject["nodetype"].ToObject<Node.TreeNodeType>();
        Debug.Log("nodetype" + nodetype);
        switch (nodetype)
        {
            case Node.TreeNodeType.EnemyViewNode:
                return jsonObject.ToObject<EnemyViewNode>();
            case Node.TreeNodeType.RepeatedNode:
                var repeatednode = new RepeatedNode();
                repeatednode.titlename = jsonObject["titlename"].ToString();
                repeatednode.width = jsonObject["width"].ToObject<float>();
                repeatednode.height = jsonObject["height"].ToObject<float>();
                repeatednode.positionx = jsonObject["positionx"].ToObject<float>();
                repeatednode.positiony = jsonObject["positiony"].ToObject<float>();
                repeatednode.nodetype = jsonObject["nodetype"].ToObject<Node.TreeNodeType>(); //nodetype
                //var childNodeJson = jsonObject["ChildNode"];
                repeatednode.ChildNode = dealRepeatedNode(jsonObject);
                //Debug.Log(childNodeJson.Type == JTokenType.Null);
                //if (childNodeJson!=null&&childNodeJson.Type != JTokenType.Null)
                //{
                //    var childNodeType = childNodeJson["nodetype"].ToObject<Node.TreeNodeType>();
                //    switch (childNodeType)
                //    {
                //        case Node.TreeNodeType.DebugLogNode:
                //            repeatednode.ChildNode = jsonObject["ChildNode"].ToObject<DebugLogNode>();
                //            break;
                //        case Node.TreeNodeType.WaitNode:
                //            repeatednode.ChildNode = jsonObject["ChildNode"].ToObject<WaitNode>();
                //            break;
                //        case Node.TreeNodeType.RepeatedNode:
                //            repeatednode.ChildNode = jsonObject["ChildNode"].ToObject<RepeatedNode>();
                //            break;
                //        case Node.TreeNodeType.SecquenceNode:
                //            repeatednode.ChildNode = jsonObject["ChildNode"].ToObject<SecquenceNode>();
                //            break;
                //        default:
                //            throw new ArgumentOutOfRangeException();
                //    }
                //}
                return repeatednode;
            case Node.TreeNodeType.SecquenceNode:
                var secquenceNode = new SecquenceNode();
                secquenceNode.titlename = jsonObject["titlename"].ToObject<string>(); // 获取 titlename 属性值
                secquenceNode.width = jsonObject["width"].ToObject<float>(); // 获取 width 属性值
                secquenceNode.height = jsonObject["height"].ToObject<float>(); // 获取 height 属性值
                secquenceNode.positionx = jsonObject["positionx"].ToObject<float>(); // 获取 positionx 属性值
                secquenceNode.positiony = jsonObject["positiony"].ToObject<float>(); // 获取 positiony 属性值
                                                                                     // secquenceNode.childindex = jsonObject["childindex"].ToObject<int>(); //childindex
                secquenceNode.nodetype = jsonObject["nodetype"].ToObject<Node.TreeNodeType>(); //nodetype
                var childrenNodesArray = jsonObject["ChildreNodes"].ToObject<JArray>();
                secquenceNode.ChildreNodes = dealSecqunenceNode(childrenNodesArray);
                //secquenceNode.ChildreNodes = new List<Node>();
                //foreach (var elem in childrenNodesArray)
                //{
                //    var elemtype = elem["nodetype"].ToObject<Node.TreeNodeType>();
                //    Node elemNode = null;
                //    switch (elemtype)
                //    {
                //        case Node.TreeNodeType.DebugLogNode:
                //            elemNode = elem.ToObject<DebugLogNode>();
                //            break;
                //        case Node.TreeNodeType.WaitNode:
                //            elemNode = elem.ToObject<WaitNode>();
                //            break;
                //        case Node.TreeNodeType.RepeatedNode:
                //            elemNode = elem.ToObject<RepeatedNode>();
                //            break;
                //        case Node.TreeNodeType.SecquenceNode:
                //            elemNode = elem.ToObject<SecquenceNode>();
                //            break;
                //        default:
                //            throw new ArgumentOutOfRangeException();
                //    }
                //    secquenceNode.ChildreNodes.Add(elemNode);
                //}
                return secquenceNode;
            case Node.TreeNodeType.RootNode:
                var rootnode = new RootNode();
                rootnode.titlename = jsonObject["titlename"].ToString();
                rootnode.width = jsonObject["width"].ToObject<float>();
                rootnode.height = jsonObject["height"].ToObject<float>();
                rootnode.positionx = jsonObject["positionx"].ToObject<float>();
                rootnode.positiony = jsonObject["positiony"].ToObject<float>();
                rootnode.nodetype = jsonObject["nodetype"].ToObject<Node.TreeNodeType>(); //nodetype
                rootnode.ChildNode = dealRootNode(jsonObject);
                return rootnode;

            default:
                throw new NotSupportedException("Unsupported node type: " + nodetype);
        }
    }
    Node dealRootNode(JObject jObject)
    {
        Node res = null;
        if (jObject != null && jObject["ChildNode"] != null && jObject["ChildNode"].Type != JTokenType.Null)
        {
            var childNodeType = jObject["ChildNode"]["nodetype"].ToObject<Node.TreeNodeType>();
            var rnj = jObject["ChildNode"] as JObject;
            switch (childNodeType)
            {

                case Node.TreeNodeType.RepeatedNode:
                    res = new RepeatedNode();
                    RepeatedNode tmpNode = res as RepeatedNode;
                    tmpNode.titlename = jObject["ChildNode"]["titlename"].ToString();
                    tmpNode.width = jObject["ChildNode"]["width"].ToObject<float>();
                    tmpNode.height = jObject["ChildNode"]["height"].ToObject<float>();
                    tmpNode.positionx = jObject["ChildNode"]["positionx"].ToObject<float>();
                    tmpNode.positiony = jObject["ChildNode"]["positiony"].ToObject<float>();
                    tmpNode.nodetype = jObject["ChildNode"]["nodetype"].ToObject<Node.TreeNodeType>(); //nodetype
                    var cnj = jObject["ChildNode"] as JObject;
                    tmpNode.ChildNode = dealRepeatedNode(cnj);
                    break;
                //case Node.TreeNodeType.RootNode:
                //    res = new RootNode();
                //    RootNode tmprootNode = res as RootNode;
                //    tmprootNode.titlename = jObject["ChildNode"]["titlename"].ToString();
                //    tmprootNode.width = jObject["ChildNode"]["width"].ToObject<float>();
                //    tmprootNode.height = jObject["ChildNode"]["height"].ToObject<float>();
                //    tmprootNode.positionx = jObject["ChildNode"]["positionx"].ToObject<float>();
                //    tmprootNode.positiony = jObject["ChildNode"]["positiony"].ToObject<float>();
                //    tmprootNode.nodetype = jObject["ChildNode"]["nodetype"].ToObject<Node.TreeNodeType>(); //nodetype
                //    var rnj = jObject["ChildNode"]["ChildNode"] as JObject;
                //    tmprootNode.ChildNode = dealRootNode(rnj);
                //    break;
                case Node.TreeNodeType.SecquenceNode:
                    res = new SecquenceNode();
                    var tmpsc = res as SecquenceNode;
                    tmpsc.titlename = jObject["ChildNode"]["titlename"].ToObject<string>(); // 获取 titlename 属性值
                    tmpsc.width = jObject["ChildNode"]["width"].ToObject<float>(); // 获取 width 属性值
                    tmpsc.height = jObject["ChildNode"]["height"].ToObject<float>(); // 获取 height 属性值
                    tmpsc.positionx = jObject["ChildNode"]["positionx"].ToObject<float>(); // 获取 positionx 属性值
                    tmpsc.positiony = jObject["ChildNode"]["positiony"].ToObject<float>(); // 获取 positiony 属性值
                                                                                           //   tmpsc.childindex = jObject["childindex"].ToObject<int>(); //childindex
                    tmpsc.nodetype = jObject["ChildNode"]["nodetype"].ToObject<Node.TreeNodeType>(); //nodetype
                    var childrenNodesArray = jObject["ChildNode"]["ChildreNodes"].ToObject<JArray>();
                    tmpsc.ChildreNodes = dealSecqunenceNode(childrenNodesArray);
                    //res = jObject["ChildNode"].ToObject<SecquenceNode>();
                    break;
                default:
                    throw new ArgumentOutOfRangeException("Unsupported node type: "+childNodeType);
            }
        }

        return res;
    }
    Node dealRepeatedNode(JObject jObject)
    {
        Node res = null;
        if (jObject != null && jObject["ChildNode"] != null && jObject["ChildNode"].Type != JTokenType.Null)
        {
            var childNodeType = jObject["ChildNode"]["nodetype"].ToObject<Node.TreeNodeType>();
            switch (childNodeType)
            {

                case Node.TreeNodeType.RepeatedNode:
                    res = new RepeatedNode();
                    RepeatedNode tmpNode = res as RepeatedNode;
                    tmpNode.titlename = jObject["ChildNode"]["titlename"].ToString();
                    tmpNode.width = jObject["ChildNode"]["width"].ToObject<float>();
                    tmpNode.height = jObject["ChildNode"]["height"].ToObject<float>();
                    tmpNode.positionx = jObject["ChildNode"]["positionx"].ToObject<float>();
                    tmpNode.positiony = jObject["ChildNode"]["positiony"].ToObject<float>();
                    tmpNode.nodetype = jObject["ChildNode"]["nodetype"].ToObject<Node.TreeNodeType>(); //nodetype
                    var cnj = jObject["ChildNode"] as JObject;
                    tmpNode.ChildNode = dealRepeatedNode(cnj);
                    break;
                case Node.TreeNodeType.SecquenceNode:
                    res = new SecquenceNode();
                    var tmpsc = res as SecquenceNode;
                    tmpsc.titlename = jObject["ChildNode"]["titlename"].ToObject<string>(); // 获取 titlename 属性值
                    tmpsc.width = jObject["ChildNode"]["width"].ToObject<float>(); // 获取 width 属性值
                    tmpsc.height = jObject["ChildNode"]["height"].ToObject<float>(); // 获取 height 属性值
                    tmpsc.positionx = jObject["ChildNode"]["positionx"].ToObject<float>(); // 获取 positionx 属性值
                    tmpsc.positiony = jObject["ChildNode"]["positiony"].ToObject<float>(); // 获取 positiony 属性值
                                                                                           //    tmpsc.childindex = jObject["childindex"].ToObject<int>(); //childindex
                    tmpsc.nodetype = jObject["ChildNode"]["nodetype"].ToObject<Node.TreeNodeType>(); //nodetype
                    var childrenNodesArray = jObject["ChildNode"]["ChildreNodes"].ToObject<JArray>();
                    tmpsc.ChildreNodes = dealSecqunenceNode(childrenNodesArray);
                    //res = jObject["ChildNode"].ToObject<SecquenceNode>();
                    break;
                default:
                    throw new ArgumentOutOfRangeException("Unsupported node type: "+childNodeType);
            }
        }

        return res;
    }
    List<Node> dealSecqunenceNode(JArray jsonload)
    {
        var resNodeList = new List<Node>();
        foreach (var elem in jsonload)
        {
            var elemtype = elem["nodetype"].ToObject<Node.TreeNodeType>();
            Node elemNode;
            switch (elemtype)
            {
                case Node.TreeNodeType.DebugLogNode:
                    elemNode = elem.ToObject<DebugLogNode>();
                    break;
                case Node.TreeNodeType.WaitNode:
                    elemNode = elem.ToObject<WaitNode>();
                    break;
                case Node.TreeNodeType.RepeatedNode:
                    elemNode = new RepeatedNode();
                    var tmpnode = elemNode as RepeatedNode;
                    tmpnode.titlename = elem["titlename"].ToString();
                    tmpnode.width = elem["width"].ToObject<float>();
                    tmpnode.height = elem["height"].ToObject<float>();
                    tmpnode.positionx = elem["positionx"].ToObject<float>();
                    tmpnode.positiony = elem["positiony"].ToObject<float>();
                    tmpnode.nodetype = elem["nodetype"].ToObject<Node.TreeNodeType>(); //nodetype
                    var rpcJObject = elem as JObject;
                    tmpnode.ChildNode = dealRepeatedNode(rpcJObject);
                    break;
                case Node.TreeNodeType.SecquenceNode:
                    elemNode = new SecquenceNode();
                    SecquenceNode secnode = elemNode as SecquenceNode;
                    secnode.titlename = elem["titlename"].ToObject<string>(); // 获取 titlename 属性值
                    secnode.width = elem["width"].ToObject<float>(); // 获取 width 属性值
                    secnode.height = elem["height"].ToObject<float>(); // 获取 height 属性值
                    secnode.positionx = elem["positionx"].ToObject<float>(); // 获取 positionx 属性值
                    secnode.positiony = elem["positiony"].ToObject<float>(); // 获取 positiony 属性值
                    secnode.nodetype = elem["nodetype"].ToObject<Node.TreeNodeType>(); //nodetype
                                                                                       //    secnode.childindex = elem["childindex"].ToObject<int>(); //childindex

                    var cna = elem["ChildreNodes"].ToObject<JArray>();
                    secnode.ChildreNodes = dealSecqunenceNode(cna);
                    break;

                //case Node.TreeNodeType.EnemyViewNode:
                //    elemNode = elem.ToObject<EnemyViewNode>();
                //    break;

                default:
                    throw new ArgumentOutOfRangeException("Unsupported node type: " + elemtype);
            }
            resNodeList.Add(elemNode);
        }
        return resNodeList;
    }
    public override bool CanConvert(Type objectType)
    {
        return typeof(Node).IsAssignableFrom(objectType);
    }
}
