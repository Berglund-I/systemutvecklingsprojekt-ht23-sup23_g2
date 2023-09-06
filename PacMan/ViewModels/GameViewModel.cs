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
