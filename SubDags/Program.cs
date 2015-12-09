using System;

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
            Console.WriteLine("Traverse\n------");
            GraphBuilder.Instance.ReadGraph();
            
            DepthFirstSearch dfs = new DepthFirstSearch(GraphBuilder.Instance.AdjacencyList);
            dfs.PrintTopologicalSortedNodes();

            ConsistentSubDags subDags = new ConsistentSubDags(GraphBuilder.Instance.AdjacencyList, dfs.TopologicalSortedVertices, dfs.ParentDictionary);
            subDags.CreateSubDags();
            subDags.PrintAllSubDags();
            Console.ReadLine();
        }
    }
}
