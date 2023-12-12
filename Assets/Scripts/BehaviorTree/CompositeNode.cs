
using System;
using System.Collections.Generic;
[Serializable]
public  class CompositeNode : Node
{
    public List<Node> ChildreNodes = new List<Node>();
}
