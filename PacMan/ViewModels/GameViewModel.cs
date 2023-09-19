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
using System.Windows.Threading;
using PacMan.Views.Entities;

namespace PacMan.ViewModels
{
    internal class GameViewModel : BaseViewModel
    {
        public MainCharacter MainCharacter { get; set; } = new MainCharacter();
        public GhostViewModel Ghosts { get; set; } = new GhostViewModel();
        public GhostBlue GhostBlue { get; set; } = new GhostBlue();
        public GhostBlue GhostBlue2 { get; set; } = new GhostBlue();
        public int GhostSize { get; set; }
        public int McSize { get; set; }
        public static Movement MovementDirection { get; set; }
        public ICommand LeftPressedCommand { get; set; }
        public ICommand RightPressedCommand { get; set; }
        public ICommand UpPressedCommand { get; set; }
        public ICommand DownPressedCommand { get; set; }
        int timerSpeed = 100;
        private int movementSpeed = 10;
        //public ObservableCollection<GameMapPiece>? GameMap { get; private set; }
        private readonly DispatcherTimer timer = new();


        //private const int _mapSize = 20;

        public GameViewModel()
        {
            GhostSize = Ghosts.GhostSize;
            McSize = MainCharacter.Size;
            LeftPressedCommand = new RelayCommand(x => LeftPressed());
            RightPressedCommand = new RelayCommand(x => RightPressed());
            UpPressedCommand = new RelayCommand(x => UpPressed());
            DownPressedCommand = new RelayCommand(x => DownPressed());
            MovementDirection = Movement.Down;

            timer.Interval = TimeSpan.FromMilliseconds(timerSpeed);
            
            timer.Tick += MainCharacterMovementTimer;
            timer.Start();

        }

        private void MainCharacterMovementTimer(object? sender, EventArgs e)
        {
            switch (MovementDirection)
            {
                case Movement.Up:
                    MainCharacter.YCoordinate -= movementSpeed;
                    break;
                case Movement.Down:
                    MainCharacter.YCoordinate += movementSpeed;
                    break;
                case Movement.Left:
                    MainCharacter.XCoordinate -= movementSpeed;
                    break;
                case Movement.Right:
                    MainCharacter.XCoordinate += movementSpeed;
                    break;
                default:
                    break;
            }
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
    }
}
