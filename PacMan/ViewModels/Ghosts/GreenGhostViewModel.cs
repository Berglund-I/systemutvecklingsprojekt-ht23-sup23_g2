﻿using PacMan.Enums;
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
            double absoluteDifferenceY = Math.Abs(McCurrentPosition.Y - GhostCurrentPosition.Y);
            double absoluteDifferenceX = Math.Abs(McCurrentPosition.X - GhostCurrentPosition.X);

            // Ghost calculates what axis it has the least amount of distance to the ghost and moves that way
                if (absoluteDifferenceX < absoluteDifferenceY)
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
