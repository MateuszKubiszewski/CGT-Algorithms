using CGT_Project.Data_Structures;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CGT_Project.Algorithms
{
    public static class GreedyIndependentSetAlgorithm
    {
        public static void ColorGraph(Graph G)
        {
            int color = 1;
            List<Vertex> uncoloredVertices = G.GetUncoloredVertices();
            while (uncoloredVertices.Count > 0)
            {
                // Deep copying remaining uncolored vertices to available vertices
                List<Vertex> availableVertices = uncoloredVertices.Select(vertex => vertex.Clone() as Vertex).ToList();

                while (availableVertices.Count > 0)
                {
                    // Calculating G[available]
                    Graph inducedGraph = Graph.GetGraphInducedBy(availableVertices);

                    // Calculating vertex of minimal degree in G[available]
                    Vertex v = inducedGraph.FindMinimalDegreeVertex();

                    // Coloring the minimal degree vertex
                    G.SetVertexColor(v.Id, color);

                    // Removing neighbors of colored vertex along with colored vertex from available
                    foreach (int connection in v.Connections)
                    {
                        availableVertices.RemoveAll(vertex => vertex.Id == connection);
                    }
                    availableVertices.Remove(v);
                }

                // Updating the list of remaining uncolored vertices before the next iteration
                uncoloredVertices = G.GetUncoloredVertices();
                color++;
            }

            if (G.AllVerticesHaveColor() && G.IsColoringProper())
                Console.WriteLine("success\n");

            //return G.AllVerticesHaveColor() && G.IsColoringProper();
        }
    }
}

/* i = 1
 * while there are uncolored vertices:
 *      available := uncolored vertices
 *      while available != empty
 *          v := vertex of minimal degree in G[available]
 *          color v with i
 *          remove v and its neighbors from available
 *      i = i + 1
 */