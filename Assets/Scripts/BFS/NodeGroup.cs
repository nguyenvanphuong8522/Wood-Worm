using System;
using System.Collections.Generic;

[Serializable]
public class NodeGroup
{
    public List<Node> Nodes;
    public NodeGroup()
    {
        Nodes = new List<Node>();
    }
}
