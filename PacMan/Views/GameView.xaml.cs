﻿using PacMan.ViewModels;
using PacMan.Enums;
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
using System.Windows.Threading;
using static System.Net.Mime.MediaTypeNames;
using System.Threading;
using PacMan.Views.Components;
using PacMan.ViewModels.Ghosts;
using PacMan.Models;

namespace PacMan.Views
{
    /// <summary>
    /// Interaction logic for GameView.xaml
    /// </summary>
    public partial class GameView : UserControl
    {

        
        public GameView()
        {
            InitializeComponent();
            GameCanvas.Focus();
            DataContext = new GameViewModel();
        }

        private void btnMute_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
