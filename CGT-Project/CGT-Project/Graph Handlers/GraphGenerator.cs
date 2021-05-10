using CGT_Project.Data_Structures;
using System;
using System.Collections.Generic;
using System.Text;

namespace CGT_Project.Graph_Handlers
{
    public static class GraphGenerator
    {
        public static Graph GenerateConnectedGraph(int size, int connectivityParameter)
        {
            Random random = new Random();
            Graph newGraph = new Graph();
            newGraph.AddVertex();

            for (int i = 1; i < size; i++)
            {
                int numberOfConnections = random.Next(1, (int)Math.Ceiling((double)i / connectivityParameter));
                List<int> newConnections = new List<int>();
                while (newConnections.Count < numberOfConnections)
                {
                    int newConnection = random.Next(0, i - 1);
                    if (!newConnections.Contains(newConnection) && newConnection != i)
                        newConnections.Add(newConnection);
                }
                newGraph.AddVertex();

                foreach (int connection in newConnections)
                {
                    newGraph.AddEdge(connection, i);
                }
            }

            return newGraph;
        }
    }
}
