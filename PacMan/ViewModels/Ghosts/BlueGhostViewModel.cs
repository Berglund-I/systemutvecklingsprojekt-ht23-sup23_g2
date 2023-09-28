using PacMan.Enums;
using PacMan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PacMan.ViewModels.Ghosts
{
    internal class BlueGhostViewModel : GhostViewModel
    {
        public Point GhostLastPosition { get; set; } = new Point(0,0);
        bool blueGhostAlternator = false;
        public BlueGhostViewModel()
        {
            
        }
        /// <summary>
        /// Logic for the ghost to follow the main character
        /// </summary>
        /// <param name="data"></param>
        public void Ai(AiDirectionPackage data)
        {

            Point McCurrentPosition = data.McCurrentPosition;
            Point GhostCurrentPosition = data.GhostCurrentPosition;
            bool blueGhostCollision = data.GhostCollision;

            if (GhostCurrentPosition == GhostLastPosition) // If the ghost is stuck it will alternate which axis it will chase the player on to unstuck itself
            {
                if (blueGhostAlternator == true)
                {
                    blueGhostAlternator = false;
                    if (McCurrentPosition.X > GhostCurrentPosition.X)
                    {
                        MovementDirection = Movement.Right;
                    }
                    else
                    {
                        MovementDirection = Movement.Left;
                    }
                }
                else
                {
                    blueGhostAlternator = true;
                    if (McCurrentPosition.Y > GhostCurrentPosition.Y)
                    {
                        MovementDirection = Movement.Down;
                    }
                    else
                    {
                        MovementDirection = Movement.Up;
                    }
                }
            }
            else if (blueGhostCollision == false && Math.Abs(McCurrentPosition.X - GhostCurrentPosition.X) > 25) // Moves left or right if its not colliding with anything or within 25units of the playeer in the X axis
            {
                if (McCurrentPosition.X > GhostCurrentPosition.X)
                {
                    MovementDirection = Movement.Right;
                }
                else
                {
                    MovementDirection = Movement.Left;
                }
            }
            else
            {
                if (McCurrentPosition.Y > GhostCurrentPosition.Y)
                {
                    MovementDirection = Movement.Down;
                }
                else
                {
                    MovementDirection = Movement.Up;
                }
            }
            GhostLastPosition = data.GhostCurrentPosition;

        }
    }
}
