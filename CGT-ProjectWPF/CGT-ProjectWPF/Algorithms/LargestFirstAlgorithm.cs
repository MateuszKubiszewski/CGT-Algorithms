using CGT_Project.Data_Structures;
using System;
using System.Collections.Generic;

namespace CGT_Project.Algorithms
{
    public static class LargestFirstAlgorithm
    {
        public static void Coloring(Graph G)
        {
            Vertex StartingV = GetLargestVertex(G);
            ColorSingleVertex(StartingV, G);
            if (G.AllVerticesHaveColor() && G.IsColoringProper())
                G.AcceptColoring();
            else
                Console.WriteLine("ERROR LF Algorithm created not valid coloring");
        }
        
        private static void ColorSingleVertex(Vertex v, Graph G)
        {
            if (v.Color != 0) return;
            int newColor = GetSmallestAvaibleColorInNeighbours(v, G);
            G.SetVertexColor(v.Id, newColor);
            foreach(int VertexId in v.Connections)
            {
                ColorSingleVertex(G.GetVertexById(VertexId), G);
            }
        }

        private static int GetSmallestAvaibleColorInNeighbours(Vertex v, Graph G)
        {
            List<int> colors = new List<int>();
            foreach (int VertexId in v.Connections)
            {
                colors.Add(G.GetVertexColor(VertexId));
            }
            for(int i = 1; i < G.Vertices.Count; i++)
            {
                bool cFound = true;
                foreach (int c in colors)
                {
                    if (c == i) cFound = false;
                }
                if (cFound) return i;
            }
            Console.WriteLine("ERROR Number of Colors in Graph exceeded safe boundry");
            return GetLargestColorInNeighbours(v, G);
        }

        private static int GetLargestColorInNeighbours(Vertex v, Graph G)
        {
            int maxColor = 0;
            foreach(int VertexId in v.Connections)
            {
                if (G.GetVertexColor(VertexId) > maxColor) maxColor = G.GetVertexColor(VertexId);
            }
            return maxColor;
        }

        private static Vertex GetLargestVertex(Graph G)
        {
            Vertex maxV = G.Vertices[0];
            foreach(Vertex v in G.Vertices)
            {
                if (v.Connections.Count> maxV.Connections.Count)
                {
                    maxV = v;
                }    
            }
            return maxV;
        }
    }
}
