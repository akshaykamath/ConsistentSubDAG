using System;
using System.Configuration;

namespace  SubDags
{
    public enum Color
    {
        White,
        Gray,
        Black
    }

    class Program
    {
        static void Main(string[] args)
        {
            var graphMode = ConfigurationManager.AppSettings["graphmode"];
            GraphBuilder.Instance.ReadGraph(Convert.ToInt32(graphMode));

            DepthFirstSearch dfs = new DepthFirstSearch(GraphBuilder.Instance.AdjacencyList);
            dfs.PrintTopologicalSortedNodes();

            ConsistentSubDags subDags = new ConsistentSubDags(GraphBuilder.Instance.AdjacencyList, dfs.TopologicalSortedVertices, dfs.ParentDictionary);
            subDags.CreateSubDags();
            // Uncomment this line to print all the subgraphs
            // subDags.PrintAllSubDags();
            Console.ReadLine();
        }
    }
}
