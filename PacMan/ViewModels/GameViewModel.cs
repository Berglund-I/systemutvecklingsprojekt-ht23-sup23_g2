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
using System.Windows.Input;
using PacMan.Models;
using PacMan.Enums;

namespace PacMan.ViewModels
{
    internal class GameViewModel : BaseViewModel
    {
        public MainCharacterViewModel MainCharacterViewModel { get; set; } = new MainCharacterViewModel();
        public MainCharacter MainCharacter { get; set; } = new MainCharacter();
        public GhostViewModel Ghosts { get; set; } = new GhostViewModel();
        public BlueGhostViewModel BlueGhost { get; set; } = new BlueGhostViewModel();
        public GhostBlue GhostBlueView { get; set; } = new GhostBlue();
        public int GhostSize { get; set; }
        public int McSize { get; set; }

        public ICommand BlueGhostAiCommand { get;}
        public ObservableCollection<GameMapPiece>? GameMap { get; private set; }

        private const int _mapSize = 20;

        public GameViewModel()
        {
            BlueGhost = new BlueGhostViewModel();
            McSize = MainCharacterViewModel.McSize;
            GhostSize = Ghosts.GhostSize;
            BlueGhostAiCommand = new RelayCommand(execute: x =>  BlueGhost.Ai((AiDirectionPackage)x) );

            CreateGameMap();
        }
        public Movement GetBlueGhostMovementDirection()
        {
            return BlueGhost.MovementDirection;
        }
        /// <summary>
        /// Generates a grid in GameView of _mapSize size
        /// </summary>
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
