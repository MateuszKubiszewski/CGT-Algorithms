using CGT_Project.Data_Structures;
using System;
using System.Collections.Generic;
using System.Text;

namespace CGT_Project.Algorithms
{
    class LargestFirstAlgorithm : Algorithm
    {
        public override void Coloring(Graph G)
        {
            Vertex StartingV = getLargestVertex(G);
            colorSingleVertex(StartingV, G);
            if (G.allVerticesHaveColor())
                G.AcceptColoring();
            else
                Console.WriteLine("ERROR LF Algorithm created not valid coloring");
        }
        
        private void colorSingleVertex(Vertex v, Graph G)
        {
            if (v.Color != 0) return;
            int newColor = getSmallestAvaibleColorInNeighbours(v, G);
            G.SetVertexColor(v.Id, newColor);
            foreach(int VertexId in v.Connections)
            {
                colorSingleVertex(G.GetVertexById(VertexId), G);
            }
        }
        private int getSmallestAvaibleColorInNeighbours(Vertex v, Graph G)
        {
            List<int> colors = new List<int>();
            foreach (int VertexId in v.Connections)
            {
                colors.Add(G.GetVertexColor(VertexId));
            }
            for(int i=1;i<G.Size;i++)
            {
                bool cFound = true;
                foreach (int c in colors)
                {
                    if (c == i) cFound = false;
                }
                if (cFound) return i;
            }
            Console.WriteLine("ERROR Number of Colors in Graph exceeded safe boundry");
            return getLargestColorInNeighbours(v,G);
        }
        private int getLargestColorInNeighbours(Vertex v, Graph G)
        {
            int maxColor = 0;
            foreach(int VertexId in v.Connections)
            {
                if (G.GetVertexColor(VertexId) > maxColor) maxColor = G.GetVertexColor(VertexId);
            }
            return maxColor;
        }
        private Vertex getLargestVertex(Graph G)
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
