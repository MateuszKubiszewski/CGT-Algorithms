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

        private void addColor(int newColor)
        {
            foreach(int color in UsedColors)
            {
                if (color == newColor) return;
            }
            UsedColors.Add(newColor);
        }  
        
        public void SetVertexColor(int v, int color)
        {
            foreach (Vertex vertex in Vertices)
            {
                if (vertex.Id == v)
                {
                    addColor(color);
                    vertex.Color = color;
                    return;
                }
            }
            Console.WriteLine("ERROR, Trygin to access non existing vertex");
        }
        public int GetVertexColor(int v)
        {
            foreach (Vertex vertex in Vertices)
            {
                if (vertex.Id == v) return vertex.Color;
            }
            Console.WriteLine("ERROR, Trygin to access non existing vertex");
            return 0;
        }
        public Vertex GetVertexById(int v)
        {
            foreach (Vertex vertex in Vertices)
            {
                if (vertex.Id == v) return vertex;
            }
            Console.WriteLine("ERROR, Trygin to access non existing vertex");
            return Vertices[0];
        }
        public bool allVerticesHaveColor()
        {
            foreach(Vertex v in Vertices)
            {
                if (v.Color == 0) return false;
            }
            return true;
        }
        
        public void PrintGraph()
        {
            Console.WriteLine($"\nGRAPH OF SIZE {Size}");
            foreach (Vertex vertex in Vertices)
            {
                Console.WriteLine($"[Id: {vertex.Id},color: {vertex.Color}] connections: {String.Join(", ", vertex.Connections)}");
            }
        }

        public void AcceptColoring()
        {
            Console.WriteLine("Coloring is accepted");
        }
    }
}
