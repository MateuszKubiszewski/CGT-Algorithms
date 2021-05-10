using System;
using System.Collections.Generic;
using System.Text;

namespace CGT_Project.Data_Structures
{
    public class Graph
    {
        public List<Vertex> Vertices { get; set; } = new List<Vertex>();
        public int Size { get; set; } = 0;
        public List<int> UsedColors { get; set; } = new List<int>();

        public void AddEdge(int idA, int idB)
        {
            foreach (Vertex vertex in Vertices)
            {
                if (vertex.Id == idA)
                {
                    vertex.Connections.Add(idB);
                }
                if (vertex.Id == idB)
                {
                    vertex.Connections.Add(idA);
                }
            }
        }

        public void AddVertex()
        {
            Vertices.Add(new Vertex(Size++));
        }

        public void ColorVertex(int vertex, int color)
        {

        }

        public void PrintGraph()
        {
            Console.WriteLine($"\nGRAPH OF SIZE {Size}");
            foreach (Vertex vertex in Vertices)
            {
                Console.WriteLine($"{vertex.Id}: {String.Join(", ", vertex.Connections)}");
            }
        }
    }
}
