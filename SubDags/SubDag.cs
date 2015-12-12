using System;
using System.Collections.Generic;
using System.Linq;

namespace SubDags
{
    /// <summary>
    /// This class accepts an adjacency list, topologically sorted vertices and exposes following methods:
    /// * Method to print all Subdags
    /// * Method to create all Consistent SubDags.
    /// </summary>
    public class ConsistentSubDags
    {
        #region Private Members
        private List<Node> _adjacencyList = new List<Node>();
        private List<Node> _toplogicalSortedVertices = new List<Node>();
        private Dictionary<Node, string> _nodeParentDictionary = new Dictionary<Node, string>();
        private List<string> _subDags = new List<string>();
        #endregion Private Members

        #region Public Properties
        /// <summary>
        /// Gets or sets the adjacency list maintained by the program.
        /// </summary>
        public List<Node> AdjacencyList
        {
            get { return _adjacencyList; }
            private set { _adjacencyList = value; }
        }

        /// <summary>
        /// Gets or sets the list of sorted vertices.
        /// </summary>
        public List<Node> TopologicalSortedVertices
        {
            get { return _toplogicalSortedVertices; }
            private set { _toplogicalSortedVertices = value; }
        }

        /// <summary>
        /// Gets or sets the parent dictionary for each vertex in a graph.
        /// </summary>
        public Dictionary<Node, string> ParentDictionary
        {
            get { return _nodeParentDictionary; }
            private set { _nodeParentDictionary = value; }
        }

        public List<string> SubDAGS
        {
            get { return _subDags; }
            private set { _subDags = value; }
        }
        #endregion Public Properties

        #region Ctor

        public ConsistentSubDags(List<Node> adjacencyList, List<Node> _toplogicalSortedVertices, Dictionary<Node, string> _nodeParentDictionary)
        {
            AdjacencyList = adjacencyList;
            ParentDictionary = _nodeParentDictionary;
            TopologicalSortedVertices = _toplogicalSortedVertices;            
        }
        #endregion Ctor

        #region Public Methods

        public int CreateSubDags()
        {
            try
            {
                // Traverse the visited queue            
                foreach (var currentNode in _toplogicalSortedVertices)
                {
                    int subdas = 0;
                    // Root node                 
                    if (string.IsNullOrEmpty(ParentDictionary[currentNode]))
                    {
                        SubDAGS.Add("[" + currentNode.ToString() + "]");
                        subdas++;
                    }
                    else
                    {
                        var initialCount = SubDAGS.Count;
                        for (int i = 0; i < initialCount; i++)
                        {
                            var discoveredSubdag = SubDAGS[i];
                            var parents = ParentDictionary[currentNode].Split(',');
                            bool hasParentsInSubDAG = parents.All(parent => discoveredSubdag.Contains(parent));

                            if (hasParentsInSubDAG)
                            {
                                var newSubDag = string.Format("{0}[{1}]", discoveredSubdag, currentNode.ToString());
                                SubDAGS.Add(newSubDag);
                                subdas++;
                            }
                        }
                    }

                    Console.WriteLine(currentNode + " - >" + subdas);
                }
            }
            catch (OutOfMemoryException ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(SubDAGS.Count.ToString());
            }

            Console.WriteLine("Count of subdags: " + SubDAGS.Count);
            return SubDAGS.Count;
        }

        public void PrintAllSubDags()
        {
            foreach (var subdag in SubDAGS)
            {
                Console.WriteLine(subdag);
            }

            Console.ReadLine();
        }
        #endregion Public Methods
    }
}
