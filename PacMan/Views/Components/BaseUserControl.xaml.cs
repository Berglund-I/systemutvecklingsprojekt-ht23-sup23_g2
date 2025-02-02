﻿using PacMan.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PacMan.Views.Components
{
    /// <summary>
    /// Interaction logic for BaseUserControl.xaml
    /// </summary>
    public partial class BaseUserControl : UserControl
    {
        public double XPosition { get; set; }
        public double YPosition { get; set; }
        public double xStartPosition { get; set; }
        public double yStartPosition { get; set; }
        public bool CollisionCheck { get; set; } = false;
        public bool ShouldCollideWithWalls { get; set; } = true;
        public Occupation Occupation { get; set; } = Occupation.Ghost;
        public BaseUserControl()
        {
            
        }
    }
}
