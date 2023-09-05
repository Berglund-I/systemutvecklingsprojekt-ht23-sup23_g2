using PacMan.Views.Components;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public ObservableCollection<GameMapPiece>? GameMap { get; private set; }

        private const int _mapSize = 20;
        
        public GameViewModel()
        {
            CreateGameMap();
            StartGameCommand = new RelayCommand(StartGame);
        }
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

       


        private void CreateGameMap()
        {
            GameMap = new ObservableCollection<GameMapPiece>();
            for (int x = 0; x < _mapSize; x++)
            {
                for (int y = 0; y < _mapSize; y++)
                {
                    var piece = new GameMapPiece();
                    GameMap.Add(piece);
                }
            }

        }
    }
}
