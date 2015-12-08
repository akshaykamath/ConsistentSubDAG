using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace SubDags
{
    class GraphBuilder
    {
        private List<Node> _adjacencyList = new List<Node>();
        public List<Node> AdjacencyList
        {
            get { return _adjacencyList; }
        }

        public Node ReadGraph()
        {
            //string[] lines = File.ReadAllLines(@"Graphs\graph.txt");
            string fileName = @"Graphs\graph.txt";
            string line = string.Empty;
            using (StreamReader file = new StreamReader(fileName))
            {
                while ((line = file.ReadLine().Trim()) != null)
                {
                    char[] delimiters = new char[] { '\t' };
                    string[] parts = line.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 0; i < parts.Length; i++)
                    {
                        Console.WriteLine(parts[i]);
                        sepList.Add(parts[i]);
                    }

                }

                file.Close();
            }
        }

        public Node BuildGraph()
        {
            /*Pedja example.
            Node n1 = new Node("1");
            Node n2 = new Node("2");
            Node n3 = new Node("3");
            Node n4 = new Node("4");
            Node n5 = new Node("5");
            Node n6 = new Node("6");
            Node n7 = new Node("7");
            Node n8 = new Node("8");

            n1.AddChild(n2);
            n1.AddChild(n3);

            n2.AddChild(n4);
            n2.AddChild(n5);

            n3.AddChild(n5);
            n3.AddChild(n6);
            n3.AddChild(n7);

            n6.AddChild(n7);
            n6.AddChild(n8);

            n7.AddChild(n8);
            _adjacencyList.Add(n1);
            _adjacencyList.Add(n2);
            _adjacencyList.Add(n3);
            _adjacencyList.Add(n4);
            _adjacencyList.Add(n5);
            _adjacencyList.Add(n6);
            _adjacencyList.Add(n7);
            _adjacencyList.Add(n8);

            return n1;
            */
            /*          Node A = new Node("A");
                      Node B = new Node("B");
                      Node C = new Node("C");
                      Node D = new Node("D");
                      Node E = new Node("E");
                      Node F = new Node("F");
                      Node G = new Node("G");
                      Node H = new Node("H");

                      A.Children.Add(B);
                      A.Children.Add(C);
                      A.Children.Add(D);

                      B.AddChild(E);
                      B.AddChild(F);

                      C.AddChild(E);
                      C.AddChild(G);

                      D.AddChild(F);
                      D.AddChild(G);

                      E.AddChild(H);
                      F.AddChild(H);
                      G.AddChild(H);

                    _adjacencyList.Add(A);
                    _adjacencyList.Add(B);
                    _adjacencyList.Add(C);
                    _adjacencyList.Add(D);
                    _adjacencyList.Add(E);
                    _adjacencyList.Add(F);
                    _adjacencyList.Add(G);
                    _adjacencyList.Add(H);

            return A;*/
            // non rev
            /*   Node n1 = new Node("1");
               Node n2 = new Node("2");
               Node n3 = new Node("3");
               Node n4 = new Node("4");
               Node n5 = new Node("5");                   

               n1.AddChild(n2);
               n1.AddChild(n3);

               n2.AddChild(n5);

               n3.AddChild(n4);
               n4.AddChild(n5);


            _adjacencyList.Add(n1);
            _adjacencyList.Add(n2);
            _adjacencyList.Add(n3);
            _adjacencyList.Add(n4);
            _adjacencyList.Add(n5);

            return n1;
            */
            /*   // arrow reverse
               Node n1 = new Node("1");
               Node n2 = new Node("2");
               Node n3 = new Node("3");
               Node n4 = new Node("4");
               Node n5 = new Node("5");

               n1.AddChild(n2);
               n1.AddChild(n3);

               n2.AddChild(n4);

               n3.AddChild(n5);
               //n4.AddChild(n5);
               n4.AddChild(n5);

               _adjacencyList.Add(n1);
               _adjacencyList.Add(n2);
               _adjacencyList.Add(n3);
               _adjacencyList.Add(n4);
               _adjacencyList.Add(n5);

               return n1;*/

            /* Node n1 = new Node("1");
             Node n2 = new Node("2");
             Node n3 = new Node("3");
             n1.AddChild(n2);
             n1.AddChild(n3);

             _adjacencyList.Add(n1);
             _adjacencyList.Add(n2);
             _adjacencyList.Add(n3);

             return n1;*/
             // Prateek Bhatt example
            Node n1 = new Node("1");
            Node n2 = new Node("2");
            Node n3 = new Node("3");
            Node n4 = new Node("4");
            Node n5 = new Node("5");
            Node n6 = new Node("6");
            Node n7 = new Node("7");
            Node n8 = new Node("8");
            Node n9 = new Node("9");
            Node n10 = new Node("10");
            Node n11 = new Node("11");
            Node n12 = new Node("12");
            Node n13 = new Node("13");

            n1.AddChild(n2);
            n1.AddChild(n3);

            n3.AddChild(n4);

            n2.AddChild(n5);
            n2.AddChild(n6);
            n2.AddChild(n7);
            n2.AddChild(n8);

            n5.AddChild(n9);
            n5.AddChild(n10);

            n6.AddChild(n9);
            n6.AddChild(n11);

            n7.AddChild(n10);
            n7.AddChild(n11);

            n8.AddChild(n13);

            n9.AddChild(n12);
            n9.AddChild(n13);

            n10.AddChild(n12);
            n10.AddChild(n13);

            n11.AddChild(n12);
            n11.AddChild(n13);

            _adjacencyList.Add(n1);
            _adjacencyList.Add(n2);
            _adjacencyList.Add(n3);
            _adjacencyList.Add(n4);
            _adjacencyList.Add(n5);
            _adjacencyList.Add(n6);
            _adjacencyList.Add(n7);
            _adjacencyList.Add(n8);
            _adjacencyList.Add(n9);
            _adjacencyList.Add(n10);
            _adjacencyList.Add(n11);
            _adjacencyList.Add(n12);
            _adjacencyList.Add(n13);
            return n1;

        }
    }
}
