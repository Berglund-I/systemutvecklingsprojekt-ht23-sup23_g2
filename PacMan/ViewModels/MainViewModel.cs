using PacMan.Commands;
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
        //public BaseViewModel CurrentViewModel { get; set; } = new GameViewModel(); Probably isnt needed
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
                }
            }
        }



        public RelayCommand StartGameCommand { get; private set; }


        private void StartGame(object parameter)
        {
            string playerName = parameter as string;


            // Creating a new instance of GameView
            GameView gameView = new GameView(); 
                                             

            // Changing the whole conntent in MainWindow to GameView
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            if (mainWindow != null)
            {
                mainWindow.Content = gameView; 
                                              
                                              
            }

            MessageBox.Show($"Nu börjar spelet {playerName}");
        }
    }
}
