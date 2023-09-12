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
using PacMan.Views.Entities;
using System.Windows.Media.Media3D;
using System.Reflection;
using System.Diagnostics.Metrics;

namespace PacMan.ViewModels
{
    internal class GameViewModel : BaseViewModel
    {
        public ObservableCollection<GameMapPiece>? GameMap { get; private set; }
        private const int _mapSize = 20;

        private ObservableCollection<GoldCoin> _coins = new ObservableCollection<GoldCoin>();
        public ObservableCollection<GoldCoin> Coins
        {
            get { return _coins; }
            set { _coins = value; }
        }

        public ObservableCollection<Block> Obstacle { get; private set; }

        public GameViewModel()
        {
           
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

        private void InitializeObstacles()
        {
            Obstacle.Add(new Block(142, 70, 578, 20));
            Obstacle.Add(new Block(231, 159, 402, 20));
            Obstacle.Add(new Block(142, 339, 124, 20));
            Obstacle.Add(new Block(142, 467, 124, 20));
            Obstacle.Add(new Block(596, 467, 124, 20));
            Obstacle.Add(new Block(596, 339, 124, 20));
            Obstacle.Add(new Block(142, 155, 20, 187));
            Obstacle.Add(new Block(700, 155, 20, 187));
            Obstacle.Add(new Block(613, 178, 20, 109));
            Obstacle.Add(new Block(231, 178, 20, 109));
            Obstacle.Add(new Block(142, 485, 20, 82));
            Obstacle.Add(new Block(700, 485, 20, 82));
            Obstacle.Add(new Block(395, 487, 76, 82));
        }

        private void PlaceCoins()
        {
            foreach (var block in Blocks)
            {
                bool collision = CheckCollision(coin, block);

                if (collision)
                {
                    // Cirkeln kolliderar med ett hinder, så vi hoppar över den här cirkeln
                    // och går vidare till nästa
                    continue;
                }
            }

            // Om vi har kommit hit innebär det att cirkeln inte kolliderar med något hinder
            // och vi kan lägga till den i Coins-listan
            Coins.Add(coin);
        }

        private bool CheckCollision(Coin coin, Block block)
        {
            // Beräkna avståndet mellan cirkelns mitt (coin.X, coin.Y) och rektangelns mitt (block.X, block.Y)
            double deltaX = Math.Abs(coin.X - block.X - block.Width / 2);
            double deltaY = Math.Abs(coin.Y - block.Y - block.Height / 2);

            // Om avståndet är större än halva cirkelns radie plus halva rektangelns storlek, finns det ingen kollision
            if (deltaX > (coin.Radius + block.Width / 2) || deltaY > (coin.Radius + block.Height / 2))
            {
                return false;
            }

            // Om avståndet är mindre än eller lika med halva cirkelns radie plus halva rektangelns storlek, finns det en kollision
            return true;
        }

    }
}
   
        

      
