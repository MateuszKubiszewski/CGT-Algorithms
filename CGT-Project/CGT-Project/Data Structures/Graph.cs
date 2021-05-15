using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CGT_Project.Data_Structures
{
    public class Graph
    {
        public List<Vertex> Vertices { get; set; } = new List<Vertex>();
        public List<int> UsedColors
        {
            get
            {
                return Vertices.Select(vertex => vertex.Color).ToList();
            }
        }
        public int ChromaticSum
        {
            get
            {
                return UsedColors.Sum();
            }
        }

        public void ClearColors()
        {
            Vertices.ForEach(vertex => vertex.Color = 0);
        }

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
            Vertices.Add(new Vertex(Vertices.Count));
        }
        
        public void SetVertexColor(int v, int color)
        {
            Vertices.Find(vertex => vertex.Id == v).Color = color;
        }

        public int GetVertexColor(int v)
        {
            return Vertices.Find(vertex => vertex.Id == v).Color;
        }
        public Vertex GetVertexById(int v)
        {
            return Vertices.Find(vertex => vertex.Id == v);
        }
        public bool AllVerticesHaveColor()
        {
            return !Vertices.Any(vertex => vertex.Color == 0);
        }
        
        public void PrintGraph()
        {
            Console.WriteLine($"\nGRAPH OF SIZE {Vertices.Count}");
            foreach (Vertex vertex in Vertices)
            {
                Console.WriteLine($"[Id: {vertex.Id}, Color: {vertex.Color}] connections: {String.Join(", ", vertex.Connections)}");
            }
        }

        public void AcceptColoring()
        {
            Console.WriteLine("Coloring is accepted");
        }

        /* ---------------------- */

        public Vertex FindMinimalDegreeVertex()
        {
            Vertex minimalDegreeVertex = Vertices.First();
            foreach (Vertex v in Vertices)
            {
                if (v.Connections.Count < minimalDegreeVertex.Connections.Count)
                {
                    minimalDegreeVertex = v;
                }
            }
            return minimalDegreeVertex;
        }

        public List<Vertex> GetUncoloredVertices()
        {
            return Vertices.Where(vertex => vertex.Color == 0).ToList();
        }

        public bool IsColoringProper()
        {
            foreach (Vertex vertex in Vertices)
            {
                foreach (int connection in vertex.Connections)
                {
                    if (vertex.Color == Vertices.Find(vertex => vertex.Id == connection).Color)
                        return false;
                }
            }
            return true;
        }

        public static Graph GetGraphInducedBy(List<Vertex> vertices)
        {
            foreach (Vertex vertex in vertices)
            {
                vertex.Connections.RemoveAll(connection => !vertices.Any(v => v.Id == connection));
            }
            return new Graph
            {
                Vertices = vertices
            };
        }
    }
}
