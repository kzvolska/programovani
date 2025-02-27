using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graf
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Node node1 = new Node(1);
            Node node2 = new Node(2);
            Node node3 = new Node(3);
            Node node4 = new Node(4);
            Node node5 = new Node(5);
            Node node6 = new Node(6);
            Node node7 = new Node(7);

            node1.AddNeighbours(new List<Node>() { node3, node7} );
            node2.AddNeighbours(new List<Node>() { node3, node4, node5 });
            node3.AddNeighbours(new List<Node>() { node1, node2, node6 });
            node4.AddNeighbours(new List<Node>() { node2, node5, node7 });
            node5.AddNeighbours(new List<Node>() { node2, node4, node6 });
            node6.AddNeighbours(new List<Node>() { node3, node5, node7 });
            node7.AddNeighbours(new List<Node>() { node1, node4, node6 });

            Node currentNode = node1;
            while (true)
            {
                currentNode.PrintNeighbours ();
                Console.WriteLine("Next node: ");
                int nextIndex = int.Parse(Console.ReadLine());
                if (nextIndex < 1) return;
                currentNode = currentNode.FindNeighbour(nextIndex);
            }
        }
    }
}
