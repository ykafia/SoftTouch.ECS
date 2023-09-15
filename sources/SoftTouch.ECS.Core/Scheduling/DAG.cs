using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftTouch.ECS.Scheduling;

public abstract class Node
{

}


public abstract class DAG : Node
{
    public Dictionary<Node, List<Node>> AdjacencyList { get; private set; }

    public DAG()
    {
        AdjacencyList = new();
    }

    public void AddEdge(Node a, Node b)
    {
        if(AdjacencyList.TryGetValue(a, out var list))
        {
            list.Add(b);
            AdjacencyList.Add(b, new());
        }
        else
        {
            AdjacencyList.Add(a, new());
            AdjacencyList[a].Add(b);
        }
    }
}
