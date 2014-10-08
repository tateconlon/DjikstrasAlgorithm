using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Node
{
    public String Name;
    public int shortestPath;
    public List<Edge> Edges;

    public Node(String name)
    {
        this.Name = name;
        shortestPath = Int32.MaxValue;
        Edges = new List<Edge>();
        Edges.Add(new Edge(this, this, 0));
    }

    public void AddEdge(Node endNode, int length)
    {
        Edges.Add(new Edge(this, endNode, length));
    }
}
