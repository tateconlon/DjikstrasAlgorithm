using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Edge
{
    public Node start;
    public Node end;
    public int length;

	public Edge(Node start, Node end, int length)
	{
        this.start = start;
        this.end = end;
        this.length = length;
	}
}
