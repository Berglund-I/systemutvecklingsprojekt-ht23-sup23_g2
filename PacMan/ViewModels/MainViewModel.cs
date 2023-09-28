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
using System.Windows.Media;

namespace PacMan.ViewModels
{
    class MainViewModel : BaseViewModel
    {
        //private UserControl _currentView;
        SoundPlayer _backGroundMusic = new SoundPlayer(Properties.Resources.BackGroundMusic);
        public RelayCommand StartGameCommand { get; private set; }
        public RelayCommand MuteMusicCommand { get; private set; }
        private bool _isPlaying = true;

        public MainViewModel()
        {
            StartGameCommand = new RelayCommand(StartGame);
            MuteMusicCommand = new RelayCommand(MuteMusic);
            _backGroundMusic.Play();
            
            //MuteImage = "pack://application:,,,/PacMan;component/Views/Images/Unmuted.png";
        }

        private void MuteMusic(object obj)
        {
            
            if (_isPlaying)
            {
                _isPlaying = false;
                _backGroundMusic.Stop();
            }
            else 
            { 
                _isPlaying = true;
                _backGroundMusic.Play();
            }
        }

        //public UserControl CurrentView
        //{
        //    get { return _currentView; }
        //    set
        //    {
        //        if (_currentView != value)
        //        {
        //            _currentView = value;
        //        }
        //    }
        //}

        private void StartGame(object parameter)
        {

            string playerName = parameter as string;
            GameViewModel gameViewModel = new GameViewModel();
            gameViewModel.PlayerName = playerName;

            _backGroundMusic.Stop();


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
