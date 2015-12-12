using System.Collections.Generic;
using System.IO;
using System;
using System.Linq;
using System.Configuration;

namespace SubDags
{
    /// <summary>
    /// A singleton class that produces the adjacency list of a graph from a file.
    /// </summary>
    internal class GraphBuilder
    {
        #region Private Members
        private static readonly GraphBuilder _instance = new GraphBuilder();
        private List<Node> _adjacencyList = new List<Node>();
        #endregion Private Members

        #region Ctor
        /// <summary>
        /// Constructor.
        /// </summary>
        private GraphBuilder()
        {
        }
        #endregion Ctor

        #region Public Properties

        public static GraphBuilder Instance
        {
            get { return _instance; }
        }

        public List<Node> AdjacencyList
        {
            get { return _adjacencyList; }
            set { _adjacencyList = value; }
        }
        #endregion Public Properties

        #region Public Methods
        /// <summary>
        /// Method reads the adjacency matrix file specified in the app.config and
        /// then updates the adjacency matrix and returns the root node.
        /// </summary>
        /// <returns>Returns the root node.</returns>
        public Node ReadGraph(int option)
        {            
            var fileName = ConfigurationManager.AppSettings["graphfilelocation"];            
            return option == 1 ? ReadAdjacencyMatrix(fileName) : ReadParentChild(fileName);
        }
        #endregion

        #region Private Methods

        private Node ReadAdjacencyMatrix(string fileName)
        {
            string line = string.Empty;
            int lineCount = 0;
            using (StreamReader file = new StreamReader(fileName))
            {
                while ((line = file.ReadLine()) != null)
                {
                    line = line.Trim();
                    char[] delimiters = new char[] { ' ' };
                    string[] nodes = line.Split(delimiters);

                    if (lineCount == 0)
                    {
                        for (int nodeCount = 1; nodeCount <= nodes.Length; nodeCount++)
                        {
                            AdjacencyList.Add(new Node(nodeCount.ToString()));
                        }
                    }

                    for (int nodeCount = 0; nodeCount < nodes.Length; nodeCount++)
                    {
                        if (nodes[nodeCount] == decimal.One.ToString())
                        {
                            _adjacencyList[lineCount].Children.Add(_adjacencyList[nodeCount]);
                        }
                    }

                    lineCount++;
                }

                file.Close();
            }

            // Return the root of the adjacency list.
            return AdjacencyList[0];
        }

        private Node ReadParentChild(string fileName)
        {

            string line = string.Empty;
            int lineCount = 0;

            try
            {
                using (StreamReader file = new StreamReader(fileName))
                {
                    while ((line = file.ReadLine()) != null)
                    {
                        line = line.Trim();
                        char[] delimiters = new char[] { '\t' };
                        string[] nodes = line.Split(delimiters);

                        if (nodes[0] == "child")
                        {
                            lineCount++;
                            continue;
                        }

                        var child = nodes[0].Replace("GO:", "");
                        var parent = nodes[1].Replace("GO:", "");

                        var parentNode = AdjacencyList.SingleOrDefault(nd => nd.ToString() == parent);
                        var childNode = AdjacencyList.SingleOrDefault(nd => nd.ToString() == child);

                        if (parentNode == null)
                        {
                            parentNode = new Node(parent);
                            AdjacencyList.Add(parentNode);
                        }

                        if (childNode == null)
                        {
                            childNode = new Node(child);
                            AdjacencyList.Add(childNode);
                        }

                        parentNode.Children.Add(childNode);

                        lineCount++;
                    }

                    file.Close();
                }
                Dictionary<string, string> dictionaryNodeMap = new Dictionary<string, string>();
                int newNodeId = 0;
                foreach (var node in AdjacencyList)
                {
                    dictionaryNodeMap.Add(newNodeId.ToString(), node.ToString());
                    node.Identity = newNodeId.ToString();
                    newNodeId++;
                }
                Console.WriteLine("count nodes: " + AdjacencyList.Count);
                // Return the root of the adjacency list.            
                return AdjacencyList[0];
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while reading the file." + ex.Message);
                Console.WriteLine("Please check if the file mode and file path are set correctly in the config file.");
            }

            var errorNode = new Node("Error");
            AdjacencyList.Add(errorNode);
            return AdjacencyList[0];
        }
        #endregion Private Methods        
    }
}
