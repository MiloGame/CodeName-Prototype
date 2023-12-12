using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BehaveTree
{
    public Node Treenode;
    public Node.AllState TreeState = Node.AllState.Running;
    public Node.AllState UpdateState()
    {
        if (Treenode.State == Node.AllState.Running)
        {
            TreeState =Treenode.UpdateState();
        }
        return TreeState;
    }

    //public static void Traverse(Node node,Action<Node> visitAction) 
    //{
    //    if (node!=null)
    //    {
    //        visitAction.Invoke(node);
    //        var kids = GetNodeChildren(node);
    //        kids.ForEach((n) =>
    //        {
    //            Traverse(n,visitAction);
    //        });
    //    }
    //}

    private static List<Node> GetNodeChildren(Node node)
    {
        List<Node> resList = new List<Node>();
        DecoratorNode decparent = node as DecoratorNode;
        if (decparent!=null && decparent.ChildNode!=null)
        {
            resList.Add(decparent.ChildNode);
        }
        RootNode rootparent = node as RootNode; 
        if (rootparent != null && rootparent.ChildNode != null)
        {
            resList.Add(rootparent.ChildNode);
        }
        CompositeNode comparent = node as CompositeNode;
        if (comparent != null )
        {
            resList = comparent.ChildreNodes;
        }

        return resList;
    }
}
