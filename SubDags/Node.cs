using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubDags
{
    public class Node
    {
        private string _identity { get; set; }
        private List<Node> _childNodes = new List<Node>();

        public Color Color = Color.White;
        public int InitialTime { get; set; }
        public int FinishTime { get; set; }
        public Node EdgeNode { get; set; }

        public string Identity
        {
            get { return _identity; }
            set { _identity = value; }
        }
        /// <summary>
        /// Accepts the identity string of the node.
        /// </summary>
        /// <param name="identity"></param>
        public Node(string identity)
        {
            _identity = identity;
        }

        public List<Node> Children
        {
            get { return _childNodes; }
        }

        public void AddChild(Node p)
        {
            _childNodes.Add(p);
        }

        public override string ToString()
        { return _identity; }
    }
}
