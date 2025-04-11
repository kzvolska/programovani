using System;

/*
 * Made by Jan Borecky for PRG seminar at Gymnazium Voderadska, year 2024-2025.
 * Extended by students.
 */

namespace GraphPlayground
{
    internal class Program
    {
        public static void DFS(Node currentNode)
        {
            /*
             * Zde naprogramuj prohledavani do hloubky.
             */

            Console.WriteLine("Current node: " +  currentNode.index);
            currentNode.visited = true;
            foreach (Node neighbor in currentNode.neighbors)
            {
                if (!neighbor.visited)
                    DFS(neighbor);
            }
        }

        public static void BFS(Graph graph, Node startNode, Node targetNode = null)
        {
            /*
             * Zde naprogramuj prohledavani do sirky.
             */

            Console.WriteLine("Starting node: " + startNode.index);
            Node currentNode = null;
            List <Node> queue = new List<Node> (startNode);
            while (queue.Count > 0)
            {
                currentNode = queue[0];
                currentNode.visited = true;
                Console.WriteLine("Current node: " + currentNode.index);
                queue.RemoveAt(0);
                foreach (Node neighbor in currentNode.neighbors)
                {
                    if (!neighbor.visited && !queue.Contains(neighbor))
                    {
                        queue.Add(neighbor);
                        Console.WriteLine("Ading node " + neighbor.index + " to queue.");
                    }
                    Console.WriteLine("Finished at node "+ currentNode.index);
                }
            }
        }

        static void Main(string[] args)
        {
            //Create and print the graph
            Graph graph = new Graph();
            graph.PrintGraph();
            graph.PrintGraphForVisualization();

            //Call both algorithms with a random starting node
            Random rng = new Random();
            DFS(graph, graph.nodes[rng.Next(0, graph.nodes.Count)]);
            BFS(graph, graph.nodes[rng.Next(0, graph.nodes.Count)]);

            Console.ReadKey();
        }
    }
}
