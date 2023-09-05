﻿using PacMan.Commands;
using PacMan.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace PacMan.ViewModels
{
    class MainViewModel : BaseViewModel
    {
        public BaseViewModel CurrentViewModel { get; set; } = new GameViewModel();
        private UserControl _currentView;

        public MainViewModel()
        {
            StartGameCommand = new RelayCommand(StartGame);
        }

        public UserControl CurrentView
        {
            get { return _currentView; }
            set
            {
                if (_currentView != value)
                {
                    _currentView = value;
                    OnPropertyChanged(nameof(CurrentView));
                }
            }
        }



        public RelayCommand StartGameCommand { get; private set; }


        private void StartGame(object parameter)
        {
            string playerName = parameter as string;


            // Creating a new instance of din GameView eller GameControl UserControl
            GameView gameView = new GameView(); // Om du använder GameView
                                                // ELLER
                                                // GameControl gameControl = new GameControl(); // Om du använder GameControl

            // Byt hela innehållet i MainWindow till GameView eller GameControl
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            if (mainWindow != null)
            {
                mainWindow.Content = gameView; // Om du använder GameView
                                               // ELLER
                                               // mainWindow.Content = gameControl; // Om du använder GameControl
            }

            MessageBox.Show($"Nu börjar spelet {playerName}");
        }
    }
}
