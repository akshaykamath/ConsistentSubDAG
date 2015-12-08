using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
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
            //BreadthFirstAlgorithm b = new BreadthFirstAlgorithm();            

            Console.WriteLine("Traverse\n------");
            //var root = b.BuildGraph();
            // b.Traverse(root);    
            //b.CreateSubDags();

            DepthFirstAlgorithm b = new DepthFirstAlgorithm();
            b.CreateSubDags();

            Console.ReadLine();
        }
    }
}
