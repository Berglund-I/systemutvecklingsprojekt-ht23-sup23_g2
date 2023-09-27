using PacMan.Commands;
using PacMan.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Media;

namespace PacMan.ViewModels
{
    class MainViewModel : BaseViewModel
    {
        //public BaseViewModel CurrentViewModel { get; set; } = new GameViewModel(); Probably isnt needed
        private UserControl _currentView;
        SoundPlayer _backGroundMusic = new SoundPlayer(Properties.Resources.BackGroundMusic);
       

        public MainViewModel()
        {
            StartGameCommand = new RelayCommand(StartGame);
            _backGroundMusic.Play();

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

            GameViewModel gameViewModel = new GameViewModel();
            gameViewModel.PlayerName = playerName;
            

            // Creating a new instance of din GameView eller GameControl UserControl
            GameView gameView = new GameView(); // Om du använder GameView
            gameView.DataContext = gameViewModel;                                   // ELLER
                                                // GameControl gameControl = new GameControl(); // Om du använder GameControl

            // Byt hela innehållet i MainWindow till GameView eller GameControl
            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            if (mainWindow != null)
            {
                mainWindow.Content = gameView; // Om du använder GameView
                                               // ELLER
                                               // mainWindow.Content = gameControl; // Om du använder GameControl
            }

            //MessageBox.Show($"Nu börjar spelet {playerName}");
        }
    }
}
