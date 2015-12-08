using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SubDags
{
    public class BreadthFirstAlgorithm
    {
        public Node BuildGraph()
        {
            GraphBuilder gp = new GraphBuilder();
            Node root = gp.BuildGraph();
            return root;
        }

        public Node Search(Node root, string identity)
        {
            Queue<Node> Q = new Queue<Node>();
            HashSet<Node> S = new HashSet<Node>();
            Q.Enqueue(root);
            S.Add(root);

            while (Q.Count > 0)
            {
                Node p = Q.Dequeue();
                if (p.ToString() == identity)
                    return p;
                foreach (Node child in p.Children)
                {
                    if (!S.Contains(child))
                    {
                        Q.Enqueue(child);
                        S.Add(child);
                    }
                }
            }
            return null;
        }

        public Dictionary<Node, List<Node>> GetDictionary(Node root)
        {
            Queue<Node> visitedQueue = new Queue<Node>();
            Queue<Node> tempQueue = new Queue<Node>();
            Dictionary<Node, List<Node>> nodeParentDictionary = new Dictionary<Node, List<Node>>();

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

            return nodeParentDictionary;
        }

        public void Traverse(Node root)
        {
            Queue<Node> visitedQueue = new Queue<Node>();
            Queue<Node> tempQueue = new Queue<Node>();
            Dictionary<Node, List<Node>> nodeParentDictionary = new Dictionary<Node, List<Node>>();

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

            // Traverse the visited queue
            while (visitedQueue.Count > 0)
            {
                var currentNode = visitedQueue.Dequeue();
                // Root node
                if (nodeParentDictionary[currentNode] == null)
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
                        sb.Append(".*");
                        sb.Append(parent.ToString());
                        sb.Append(".*");
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
