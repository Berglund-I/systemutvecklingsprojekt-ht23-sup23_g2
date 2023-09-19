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
using PacMan.ViewModels.Ghosts;
using PacMan.ViewModels;
using PacMan.Enums;
using System.Windows.Input;

namespace PacMan.ViewModels
{
    internal class GameViewModel : BaseViewModel
    {
        public MainCharacterViewModel MainCharacterViewModel { get; set; } = new MainCharacterViewModel();
        public MainCharacter MainCharacter { get; set; } = new MainCharacter();
        public GhostViewModel Ghosts { get; set; } = new GhostViewModel();
        public GhostBlue GhostBlue { get; set; } = new GhostBlue();
        public GhostBlue GhostBlue2 { get; set; } = new GhostBlue();
        public int GhostSize { get; set; }
        public int McSize { get; set; }
        //public ObservableCollection<GameMapPiece>? GameMap { get; private set; }
        public Movement MovementDirection { get; set; }
        public ICommand LeftPressedCommand { get; set; }
        public ICommand RightPressedCommand { get; set; }
        public ICommand UpPressedCommand { get; set; }
        public ICommand DownPressedCommand { get; set; }
        

        //private const int _mapSize = 20;

        public GameViewModel()
        {
            McSize = MainCharacterViewModel.McSize;
            GhostSize = Ghosts.GhostSize;
            LeftPressedCommand = new RelayCommand(arg => LeftPressed());
            RightPressedCommand = new RelayCommand(arg => RightPressed());
            UpPressedCommand = new RelayCommand(arg => UpPressed());
            DownPressedCommand = new RelayCommand(arg => DownPressed());
            MovementDirection = Movement.Down;
        }
        private void DownPressed()
        {
            
            MovementDirection = Movement.Down;
            
        }

        private void UpPressed()
        {
            MovementDirection = Movement.Up;
        }

        private void RightPressed()
        {
            MovementDirection = Movement.Right;
        }

        private void LeftPressed()
        {
            MovementDirection = Movement.Left;

        }
        //public Movement McMovement()
        //{
        //    return MovementDirection;
        //}

        /// <summary>
        /// Generates a grid in GameView of _mapSize size
        /// </summary>
        //private void CreateGameMap()
        //{
        //    GameMap = new ObservableCollection<GameMapPiece>();
        //    for (int x = 0; x < _mapSize; x++)
        //    {
        //        for (int y = 0; y < _mapSize; y++)
        //        {
        //            var piece = new GameMapPiece();
        //            GameMap.Add(piece);
        //        }
        //    }

        //}
    }
}
