using System;
using System.Collections.Generic;
using System.Text;

namespace CGT_Project.Data_Structures
{
    public class Vertex : ICloneable
    {
        public List<int> Connections { get; set; } = new List<int>();
        public int Color { get; set; }
        public int Id { get; set; }

        public Vertex() { }

        public Vertex(int id, int color = 0)
        {
            Id = id;
            Color = color;
        }

        public object Clone()
        {
            List<int> newConnections = new List<int>();
            newConnections.AddRange(Connections);
            return new Vertex
            {
                Connections = newConnections,
                Color = this.Color,
                Id = this.Id
            };
        }
    }
}
