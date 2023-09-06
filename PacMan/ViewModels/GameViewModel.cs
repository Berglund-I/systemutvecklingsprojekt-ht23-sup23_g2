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

namespace PacMan.ViewModels
{
    internal class GameViewModel : BaseViewModel
    {
        public GhostViewModel Ghosts { get; set; } = new GhostViewModel();
        public GhostBlue GhostBlue { get; set; } = new GhostBlue();
        public int GhostSize { get; set; }
        public ObservableCollection<GameMapPiece>? GameMap { get; private set; }

        private const int _mapSize = 20;

        public GameViewModel()
        {
            GhostSize = Ghosts.GhostSize;
            CreateGameMap();
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
