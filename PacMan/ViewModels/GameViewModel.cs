using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using PacMan.Commands;  // Importing namespace for RelayCommand
using PacMan.Views;

namespace PacMan.ViewModels
{
    internal class GameViewModel : BaseViewModel
    {
        private UserControl _currentView;

        public UserControl CurrentView
        {
            get { return _currentView; }
            set 
            {
                if(_currentView != value)
                {
                    _currentView = value;
                    OnPropertyChanged(nameof(CurrentView));
                }
            }
        }

     

        public RelayCommand StartGameCommand { get; private set; }

        public GameViewModel()
        {
            StartGameCommand = new RelayCommand(StartGame);
        }

        private void StartGame(object parameter)
        {
            string playerName = parameter as string;


            // Skapa en ny instans av din GameView eller GameControl UserControl
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
