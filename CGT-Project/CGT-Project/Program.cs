using CGT_Project.Data_Structures;
using CGT_Project.Graph_Handlers;
using System;
using System.Collections.Generic;

namespace CGT_Project
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var generatedGraphs = GenerateGraphs(4, 4, 8, 1);
            foreach (Graph graph in generatedGraphs)
            {
                graph.PrintGraph();
            }

            Console.WriteLine("\nBy By World!");
        }

        static List<Graph> GenerateGraphs(int numberOfGeneratedGraphs, int minimumSize, int maximumSize, int connectivityParameter)
        {
            List<Graph> graphs = new List<Graph>();
            Random random = new Random();

            for (int i = 0; i < numberOfGeneratedGraphs; i++)
            {
                graphs.Add(GraphGenerator.GenerateConnectedGraph(random.Next(minimumSize, maximumSize), connectivityParameter));
            }

            return graphs;
        }
    }
}
