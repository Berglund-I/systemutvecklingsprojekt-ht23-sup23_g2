﻿using PacMan.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace PacMan.ViewModels.Ghosts
{
    class GhostViewModel : BaseViewModel
    {
        public int GhostSize { get; set; } = 40;
        public Movement MovementDirection { get; set; } = Movement.Left;
        
        public GhostViewModel()
        {
            
        }

    }
}
