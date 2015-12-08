using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace SubDags
{
    public class DepthFirstAlgorithm
    {
        private List<Node> _adjacencyList = new List<Node>();
        private List<Node> _toplogicalSortedVertices = new List<Node>();
        private Dictionary<Node, List<Node>> nodeParentDictionary = new Dictionary<Node, List<Node>>();
        private int time = 0;

        public List<Node> AdjacencyList
        {
            get { return _adjacencyList; }
            set { _adjacencyList = value; }
        }

        public DepthFirstAlgorithm()
        {
            var root = BuildGraph();
            AddParentDictionary(null, root);

            PerformDfs(root);
            PrintTopologicalSortedNodes();
        }

        private Node BuildGraph()
        {
            GraphBuilder gp = new GraphBuilder();
            Node root = gp.BuildGraph();
            AdjacencyList = gp.AdjacencyList;
            return root;
        }

        private void PerformDfs(Node root)
        {
            foreach (var adjNode in AdjacencyList)
            {
                if (adjNode.Color == Color.White)
                {
                    DfsVisit(adjNode);
                }
            }
        }

        private void DfsVisit(Node adjNode)
        {
            time = time + 1;
            adjNode.InitialTime = time;
            adjNode.Color = Color.Gray;
            foreach (var vertex in adjNode.Children)
            {
                AddParentDictionary(adjNode, vertex);

                if (vertex.Color == Color.White)
                {
                    vertex.EdgeNode = adjNode;
                    DfsVisit(vertex);
                }
            }

            adjNode.Color = Color.Black;
            time = time + 1;
            adjNode.FinishTime = time;
            _toplogicalSortedVertices.Add(adjNode);
        }

        private void AddParentDictionary(Node parent, Node child)
        {
            if (!nodeParentDictionary.ContainsKey(child))
            {
                if (parent == null)
                {
                    nodeParentDictionary.Add(child, null);
                }
                else
                {
                    nodeParentDictionary.Add(child, new List<Node>() { parent });
                }
            }
            else
            {
                List<Node> parents = new List<Node>();
                nodeParentDictionary.TryGetValue(child, out parents);
                parents.Insert(0, parent);
            }
        }

        private void AddParentDictionaryEntry(Node root)
        {
            Queue<Node> visitedQueue = new Queue<Node>();
            Queue<Node> tempQueue = new Queue<Node>();

            List<string> subdags = new List<string>();

            tempQueue.Enqueue(root);
            nodeParentDictionary.Add(root, null);

            while (tempQueue.Count > 0)
            {
                Node currentNode = tempQueue.Dequeue();
                foreach (Node child in currentNode.Children)
                {
                    if (!nodeParentDictionary.ContainsKey(child))
                    {
                        tempQueue.Enqueue(child);
                        nodeParentDictionary.Add(child, new List<Node>() { currentNode });
                    }
                    else
                    {
                        List<Node> parents = new List<Node>();
                        nodeParentDictionary.TryGetValue(child, out parents);
                        parents.Add(currentNode);
                    }
                }

                visitedQueue.Enqueue(currentNode);
            }
        }

        public void PrintTopologicalSortedNodes()
        {
            _toplogicalSortedVertices.Reverse();
            foreach (var vertice in _toplogicalSortedVertices)
            {
                Console.WriteLine("{0} {1} {2} {3}", vertice.ToString(), vertice.InitialTime, vertice.FinishTime, vertice.Color);
            }
        }

        public void CreateSubDags()
        {
            List<string> subdags = new List<string>();

            // Traverse the visited queue
            //_toplogicalSortedVertices.Reverse();
            foreach (var currentNode in _toplogicalSortedVertices)
            {
                // Root node
                var root = nodeParentDictionary[currentNode];
                if (root == null)
                {
                    subdags.Add(currentNode.ToString());
                }

                // has parents, traverse through all visited nodes from its recently visited parent.
                else
                {
                    // Get parent string, search for it in the list and then append it.                        
                    StringBuilder sb = new StringBuilder();


                    foreach (var parent in nodeParentDictionary[currentNode])
                    {
                        if (parent != null)
                        {
                            sb.Append(".*");
                            sb.Append(parent.ToString());
                            sb.Append(".*");
                        }
                    }

                    var initialCount = subdags.Count;
                    //foreach (var discoveredSubdag in subdags)
                    for (int i = 0; i < initialCount; i++)
                    {
                        var discoveredSubdag = subdags[i];
                        if (Regex.IsMatch(discoveredSubdag, sb.ToString()))
                        {
                            var newSubDag = string.Format("{0}{1}", discoveredSubdag, currentNode.ToString());
                            subdags.Add(newSubDag);
                        }
                    }
                }
            }

            foreach (var subdag in subdags)
            {
                Console.WriteLine(subdag);
            }
            Console.WriteLine("Count of subdags: " + subdags.Count);
        }
    }
}
