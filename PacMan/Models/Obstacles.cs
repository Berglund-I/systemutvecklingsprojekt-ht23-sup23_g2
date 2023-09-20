using PacMan.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacMan.Models
{
    class Obstacles : BaseViewModel
    {
        public int XPosition { get; set; }
        public int YPosition { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
    }
}
