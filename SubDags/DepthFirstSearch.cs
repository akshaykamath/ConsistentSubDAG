using System;
using System.Collections.Generic;

namespace SubDags
{
    /// <summary>
    /// This class accepts an adjacency list and then performs a discovery of nodes in a topologically sorted order.
    /// The public features of class are :
    /// * AdjacencyList - Returns the adjacency list.
    /// * TopologicalSortedVertices - Returns the list of topologically sorted vertices.
    /// * ParentDictionary - Returns a dictionary containing the parents of each node.
    /// * PrintTopologicalSortedNodes() - Prints a list of topologically sorted vertices.
    /// </summary>
    internal class DepthFirstSearch
    {
        #region Private Members
        private List<Node> _adjacencyList = new List<Node>();
        private List<Node> _toplogicalSortedVertices = new List<Node>();
        private Dictionary<Node, string> _nodeParentDictionary = new Dictionary<Node, string>();
        private int _visitTime = 0;
        private Node _rootNode;
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
            set { _toplogicalSortedVertices = value; }
        }

        /// <summary>
        /// Gets or sets the parent dictionary for each vertex in a graph.
        /// </summary>
        public Dictionary<Node, string> ParentDictionary
        {
            get { return _nodeParentDictionary; }
            set { _nodeParentDictionary = value; }
        }
        #endregion Public Properties

        #region Ctor
        /// <summary>
        /// Entry point of the class. 
        /// Accepts an adjacency list and performs a topological sort.
        /// </summary>
        /// <param name="adjacencyList"></param>
        public DepthFirstSearch(List<Node> adjacencyList)
        {            
            AdjacencyList = adjacencyList;
            _rootNode = AdjacencyList[0];
            PerformTopologicalSort();           
        }
        #endregion Ctor

        #region Public Methods
        /// <summary>
        /// Prints out a list of vertices that are topologically sorted.
        /// </summary>
        public void PrintTopologicalSortedNodes()
        {
            foreach (var vertice in _toplogicalSortedVertices)
            {
                Console.WriteLine("{0} {1} {2} {3}", vertice.ToString(), vertice.InitialTime, vertice.FinishTime, vertice.Color);
            }
        }
        #endregion Public Methods

        #region Private Methods
        /// <summary>
        /// Algorithm based on CLRS textbook.
        /// Uses DFS.
        /// </summary>
        private void PerformTopologicalSort()
        {            
            AddParentDictionary(null, _rootNode);
            PerformDfs();

            TopologicalSortedVertices.Reverse();
        }

        /// <summary>
        /// Performs DFS based on algorithm in CLRS textbook.
        /// </summary>        
        private void PerformDfs()
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
            _visitTime = _visitTime + 1;
            adjNode.InitialTime = _visitTime;
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
            _visitTime = _visitTime + 1;
            adjNode.FinishTime = _visitTime;
            _toplogicalSortedVertices.Add(adjNode);
        }

        private void AddParentDictionary(Node parent, Node child)
        {
            if (!_nodeParentDictionary.ContainsKey(child))
            {
                if (parent == null)
                {
                    _nodeParentDictionary.Add(child, null);
                }
                else
                {
                    _nodeParentDictionary.Add(child, string.Format(".*\\[{0}\\].*", parent.ToString()));
                }
            }
            else
            {
                string oldParent = string.Empty;
                _nodeParentDictionary.TryGetValue(child, out oldParent);
                _nodeParentDictionary[child] = string.Format(".*\\[{0}\\]{1}", parent.ToString(), oldParent);
            }
        }
        #endregion Private Methods
    }
}
