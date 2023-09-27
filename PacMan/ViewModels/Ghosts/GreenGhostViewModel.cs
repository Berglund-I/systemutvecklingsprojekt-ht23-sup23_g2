using PacMan.Enums;
using PacMan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PacMan.ViewModels.Ghosts
{
    internal class GreenGhostViewModel : GhostViewModel
    {

        public void Ai(AiDirectionPackage data)
        {

            Point McCurrentPosition = data.McCurrentPosition;
            Point GhostCurrentPosition = data.GhostCurrentPosition;

            
 
                if (Math.Abs(McCurrentPosition.Y - GhostCurrentPosition.Y) > 120)
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
                else
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
                
            
        }
    }
}
