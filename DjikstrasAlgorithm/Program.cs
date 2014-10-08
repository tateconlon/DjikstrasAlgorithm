using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DjikstrasAlg
{
    class Program
    {
        static List<Node> nodesNotDone;
        static List<Node> nodesDone;
        static List<Edge> edges;
        static Node startingNode = null;

        static void Main(string[] args)
        {
            nodesNotDone = new List<Node>();
            nodesDone = new List<Node>();
            edges = new List<Edge>();
            bool doneAddNodes = false;
            bool doneAddEdges = false;

            /*********** Add Nodes *************/
            System.Console.Out.WriteLine("Enter Names of all Nodes:");
            String name = String.Empty;
            while (!doneAddNodes)
            {
                System.Console.Out.WriteLine("Node Name:");
                name = Console.ReadLine();
                Node tempNode = new Node(name);
                nodesNotDone.Add(tempNode);

                doneAddNodes = !YesOrNoOption("Add another node? [y/n]");
            }

            /********** Add edges **********/
            doneAddEdges = !YesOrNoOption("Would you like to add edges between nodes? [y/n]");

            while (!doneAddEdges)
            {
                Node startNode = null;
                Node endNode = null;
                int lengthInt = 0;
                bool edgeExists = false;
                bool validInt = false;

                while (startNode == null)
                {
                    ListNodes();
                    System.Console.Out.WriteLine("Start Node Name:");
                    String startNodeName = Console.ReadLine().Trim();
                    startNode = FindNode(startNodeName);
                    if (startNode == null)
                    {
                        System.Console.Out.WriteLine("{0} is not an existing Node. Please Try Again.", startNodeName);
                    }

                }

                while (endNode == null)
                {
                    ListNodes();
                    System.Console.Out.WriteLine("End Node Name:");
                    String endNodeName = Console.ReadLine().Trim();
                    endNode = FindNode(endNodeName);
                    if (endNode == null)
                    {
                        System.Console.Out.WriteLine("{0} is not an existing Node. Please Try Again.", endNodeName);
                    }

                }

                foreach (Edge ed in startNode.Edges)
                {
                    if (ed.start == startNode && ed.end == endNode)
                    {
                        System.Console.Out.WriteLine("There already exists an edge from node {0} to {1} with length {3:d}", startNode, endNode, ed.length);
                        edgeExists = true;
                    }
                }

                if (!edgeExists)
                {
                    String length = String.Empty;
                    while (!validInt)
                    {
                        try
                        {
                            System.Console.Out.WriteLine("Enter {0} to {1} edge length (positive integer):", startNode.Name, endNode.Name);
                            length = Console.ReadLine().Trim();
                            lengthInt = Int32.Parse(length);
                            if (lengthInt >= 1)
                                validInt = true;
                        }
                        catch (Exception e)
                        {
                            System.Console.Out.WriteLine("{0:d} is not a positive integer", length);
                        }
                    }

                }

                startNode.AddEdge(endNode, lengthInt);
                endNode.AddEdge(startNode, lengthInt);
                doneAddEdges = !YesOrNoOption("Add another edge? [y/n]");

            }
                /********** Picking Starting Node **************/

                Console.Out.WriteLine("Pick starting node:");
                while (startingNode == null)
                {
                    ListNodes();
                    System.Console.Out.WriteLine("Starting Name:");
                    String startingNodeName = Console.ReadLine().Trim();
                    startingNode = FindNode(startingNodeName);
                    if (startingNode == null)
                    {
                        System.Console.Out.WriteLine("{0:d} is not an existing Node. Please Try Again.", startingNodeName);
                    }

                }

                /********* Djikstra's Algorithm *************/
                startingNode.shortestPath = 0;
                Djikstra(startingNode);
                DisplayPaths();
                Console.ReadLine();
        }

        static bool YesOrNoOption(String message)
        {
            while (true)
            {
                Console.Out.WriteLine(message);
                String answer = Console.ReadLine();
                if (answer.Trim().Equals("y") || answer.Trim().Equals("Y"))
                {
                    return true;
                }
                else if (answer.Trim().Equals("n") || answer.Trim().Equals("N"))
                {
                    return false;
                }
                Console.Out.WriteLine("{0} is not a valid option. Please enter another option.", answer);
            }
        }

        static void ListNodes()
        {
            Console.Out.WriteLine("List of Nodes in Graph: ");
            foreach (Node node in nodesNotDone)
            {
                Console.Out.Write(node.Name + " ");
            }
            Console.Out.WriteLine();
        }

        static void DisplayPaths()
        {
            Console.Out.WriteLine("Shortest Paths:");
            foreach (Node nod in nodesDone)
            {
                Console.Out.WriteLine("{0} -> {1}: {2:d}", startingNode.Name, nod.Name, nod.shortestPath);
            }
        }

        static Node FindNode(string name)
        {
            foreach (Node nod in nodesNotDone)
            {
                if (nod.Name.Trim().Equals(name))
                {
                    return nod;
                }
            }
            return null;
        }


        static void Djikstra(Node nod) 
        {
            if (nod == null)
                return;

            foreach (Edge ed in nod.Edges)
            {
                int newPath = nod.shortestPath + ed.length;
                if (ed.end.shortestPath > newPath)
                {
                    ed.end.shortestPath = newPath;
                }
            }

            Node smallestPathNode = null;
            int shortestPath = Int32.MaxValue;
            foreach (Node notDone in nodesNotDone)
            {
                if (notDone.shortestPath <= shortestPath)
                {
                    smallestPathNode = notDone;
                    shortestPath = notDone.shortestPath;
                }
            }


            bool removed = nodesNotDone.Remove(nod);

            if(removed)
                nodesDone.Add(nod);

            Djikstra(smallestPathNode);
        }
    }


}

