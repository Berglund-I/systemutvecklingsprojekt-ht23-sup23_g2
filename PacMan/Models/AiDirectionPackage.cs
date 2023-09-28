using System;
using System.Collections.Generic;
using System.Windows;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacMan.Models
{
    internal class AiDirectionPackage
    {
        public Point GhostCurrentPosition { get; set; }
        public Point McCurrentPosition { get; set; }
        public bool GhostCollision { get; set; }
        /// <summary>
        /// An object for all the neccesary info for the ghosts ai to work
        /// </summary>
        /// <param name="ghostCurrentPosition"></param>
        /// <param name="mcCurrentPosition"></param>
        public AiDirectionPackage( Point ghostCurrentPosition, Point mcCurrentPosition, bool ghostCollision)
        {
            
            GhostCurrentPosition = ghostCurrentPosition;
            McCurrentPosition = mcCurrentPosition;
            GhostCollision = ghostCollision;

        }
    }
}
