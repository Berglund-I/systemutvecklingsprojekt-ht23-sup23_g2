using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacMan.Views.Entities
{
    internal class Block
    {
        public int X { get; set; } // X-koordinat för hinder
        public int Y { get; set; } // Y-koordinat för hinder
        public int Width { get; set; } // Bredd på hinder
        public int Height { get; set; } // Höjd på hinder

        public Block(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }
    }
}
