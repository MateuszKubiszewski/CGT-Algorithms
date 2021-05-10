using System;
using System.Collections.Generic;
using System.Text;

namespace CGT_Project.Data_Structures
{
    public class Vertex
    {
        public List<int> Connections { get; set; } = new List<int>();
        public int Color { get; set; }
        public int Id { get; set; }

        public Vertex(int id, int color = 0)
        {
            Id = id;
            Color = color;
        }
    }
}
