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
using System.Windows.Threading;
using System.Threading;
//using System.Drawing;
using PacMan.Views.Entities;

namespace PacMan.ViewModels
{
    internal class GameViewModel : BaseViewModel
    {
        public int GameViewWidth { get; set; } = 848;
        public int GameViewHeight { get; set; } = 700-40;
        public MainCharacter MainCharacter { get; set; } = new MainCharacter();
        public GhostBlue GhostBlueView { get; set; } = new GhostBlue();
        public GhostViewModel Ghosts { get; set; } = new GhostViewModel();
        public BlueGhostViewModel BlueGhostVM { get; set; } = new BlueGhostViewModel();
        public int GhostSize { get; set; }
        public int McSize { get; set; }
        public static Movement MovementDirection { get; set; }
        public ICommand LeftPressedCommand { get; set; }
        public ICommand RightPressedCommand { get; set; }
        public ICommand UpPressedCommand { get; set; }
        public ICommand DownPressedCommand { get; set; }

        public double BlueGhostX { get; set; } = -100;
        public double BlueGhostY { get; set; }
        
        public ICommand BlueGhostAiCommand { get;}
        public BaseUserControl CurrentUserControl { get; set; }

        public ObservableCollection<Obstacles> Obstacles { get; } = new ObservableCollection<Obstacles>();


        public bool blueGhostCollision = false;
        int movementSpeed = 10;
        private readonly DispatcherTimer timer = new DispatcherTimer();
        int timerSpeed = 100;

        public ObservableCollection<GoldCoin> GoldCoins { get; set; } = new ObservableCollection<GoldCoin>(); //ida

        public GameViewModel()
        {
            BlueGhostVM = new BlueGhostViewModel();
            GhostSize = Ghosts.GhostSize;

            McSize = MainCharacter.Size;
            LeftPressedCommand = new RelayCommand(x => LeftPressed());
            RightPressedCommand = new RelayCommand(x => RightPressed());
            UpPressedCommand = new RelayCommand(x => UpPressed());
            DownPressedCommand = new RelayCommand(x => DownPressed());
            MovementDirection = Movement.Down;

            CreateObstaclesList();

            timer.Interval = TimeSpan.FromMilliseconds(timerSpeed);
            timer.Tick += GhostMovementTimer;
            timer.Tick += MainCharacterMovementTimer;
            timer.Start();

            BlueGhostAiCommand = new RelayCommand(execute: x => BlueGhostVM.Ai((AiDirectionPackage)x));
            
        }

       private void CreateObstaclesList()
        {
            Obstacles.Add(new Obstacles { Height = 20, Width = 578, XPosition = 142, YPosition = 70 });
            Obstacles.Add(new Obstacles { Height = 20, Width = 402, XPosition = 231, YPosition = 159 });
            Obstacles.Add(new Obstacles { Height = 20, Width = 124, XPosition = 142, YPosition = 339 });
            Obstacles.Add(new Obstacles { Height = 20, Width = 124, XPosition = 142, YPosition = 467 });
            Obstacles.Add(new Obstacles { Height = 20, Width = 124, XPosition = 596, YPosition = 467 });
            Obstacles.Add(new Obstacles { Height = 20, Width = 124, XPosition = 596, YPosition = 339 });
            Obstacles.Add(new Obstacles { Height = 187, Width = 20, XPosition = 142, YPosition = 155 });
            Obstacles.Add(new Obstacles { Height = 187, Width = 20, XPosition = 700, YPosition = 155 });
            Obstacles.Add(new Obstacles { Height = 109, Width = 20, XPosition = 613, YPosition = 178 });
            Obstacles.Add(new Obstacles { Height = 109, Width = 20, XPosition = 231, YPosition = 178 });
            Obstacles.Add(new Obstacles { Height = 82, Width = 20, XPosition = 142, YPosition = 485 });
            Obstacles.Add(new Obstacles { Height = 82, Width = 20, XPosition = 700, YPosition = 485 });
            Obstacles.Add(new Obstacles { Height = 82, Width = 76, XPosition = 395, YPosition = 487 });
        }

        private void MainCharacterMovementTimer(object? sender, EventArgs e)
        {
            switch (MovementDirection)
            {
                case Movement.Up:
                    MainCharacter.YPosition -= movementSpeed;
                    break;
                case Movement.Down:
                    MainCharacter.YPosition += movementSpeed;
                    break;
                case Movement.Left:
                    MainCharacter.XPosition -= movementSpeed;
                    break;
                case Movement.Right:
                    MainCharacter.XPosition += movementSpeed;
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

        public Movement GetBlueGhostMovementDirection()
        {
            return BlueGhostVM.MovementDirection;
        }
        private void GhostMovementTimer(object sender, EventArgs e)
        {
            BlueGhostX = GhostBlueView.XPosition;
            BlueGhostY = GhostBlueView.YPosition;
            AiDirectionPackage AiPackage = new AiDirectionPackage(new Point(BlueGhostX, BlueGhostY), new Point(MainCharacter.XPosition, MainCharacter.XPosition), blueGhostCollision);
            BlueGhostVM.Ai(AiPackage);
            

            CurrentUserControl = GhostBlueView;
            MoveContentControl(BlueGhostVM.MovementDirection);
        }

        private void MoveContentControl(Movement movementDirection)
        {
            //Point ContentControlPosition = contentControl.TransformToAncestor(this).Transform(new Point(0, 0));
            double currentPositionX = CurrentUserControl.XPosition;
            double currentPositionY = CurrentUserControl.YPosition;
            if (CurrentUserControl == GhostBlueView)
            {
                blueGhostCollision = true;
            }
            //GameViewModel currentGhost = (GhostViewModel)contentControl.Content;
            switch (movementDirection)
            {
                case Movement.Up:
                    if (BorderColisionUp(currentPositionY)) // If collision is detected with the border of the GameView
                    {
                        CurrentUserControl.YPosition = 0;
                    }
                    else if (WallCollision(  movementDirection)) { }
                    else { CurrentUserControl.YPosition -= movementSpeed; }
                    break;
                case Movement.Down:
                    if (CollisionDown(currentPositionY + CurrentUserControl.ActualHeight )) // If collision is detected with the border of the GameView
                    {
                        CurrentUserControl.YPosition = GameViewHeight - CurrentUserControl.ActualHeight;
                    }
                    else if (WallCollision( movementDirection)) { }
                    else { CurrentUserControl.YPosition += movementSpeed; }
                    break;
                case Movement.Left:
                    if (CollisionLeft(currentPositionX)) // If collision is detected with the border of the GameView
                    {
                        CurrentUserControl.XPosition = 0;

                    }
                    else if (WallCollision( movementDirection)) { }
                    else { CurrentUserControl.XPosition -= movementSpeed; }
                    break;

                case Movement.Right:
                    if (CollisionRight(currentPositionX + CurrentUserControl.ActualWidth)) // If no collision is detected with the border of the GameView
                    {
                        CurrentUserControl.XPosition = GameViewWidth - CurrentUserControl.ActualWidth;

                    }
                    else if (WallCollision( movementDirection)) { }
                    else { CurrentUserControl.XPosition += movementSpeed; }
                    break;


            }
        }


        #region collision controls
        private bool WallCollision( Movement movementDirection)
        {
            double nextX = CurrentUserControl.XPosition;
            double nextY = CurrentUserControl.YPosition;
            switch (movementDirection)  // Predicts the next step of the selected UserControl
            {
                case Movement.Left:
                    nextX = CurrentUserControl.XPosition - movementSpeed;
                    break;
                case Movement.Right:
                    nextX = CurrentUserControl.XPosition + movementSpeed;
                    break;
                case Movement.Up:
                    nextY = CurrentUserControl.YPosition - movementSpeed;
                    break;
                case Movement.Down:
                    nextY = CurrentUserControl.YPosition + movementSpeed;
                    break;
            }
            
            foreach (var obstacle in Obstacles) //Checks if the UserControl colides with any of the obstacles and moves it accordingly 
            {

                if (nextX < obstacle.XPosition + obstacle.Width &&
                    nextX + CurrentUserControl.ActualWidth > obstacle.XPosition &&
                    nextY < obstacle.YPosition + obstacle.Height &&
                    nextY + CurrentUserControl.ActualHeight > obstacle.YPosition)
                {
                    switch (movementDirection)
                    {
                        case Movement.Left:
                            CurrentUserControl.XPosition = obstacle.XPosition + obstacle.Width;
                            break;
                        case Movement.Right:
                            CurrentUserControl.XPosition = obstacle.XPosition - CurrentUserControl.ActualWidth; 
                            break;
                        case Movement.Up:
                            CurrentUserControl.YPosition = obstacle.YPosition + obstacle.Height; 
                            break;
                        case Movement.Down:
                            CurrentUserControl.YPosition = obstacle.YPosition + CurrentUserControl.ActualHeight;
                            break;

                    }
                    return true; // Collision detected
                }
            }
            return false;
        }

        public bool BorderColisionUp(double currentPositionY)
        {
            double newPosition = currentPositionY - movementSpeed;
            if (newPosition < 0) // Checks if the new position would be outside of the GameWindow
            {
                return true;
            }
            return false; // If no collision is detected
        }
        public bool CollisionDown(double currentPositionY)
        {
            double newPosition = currentPositionY + movementSpeed;
            if (newPosition > GameViewHeight) // Checks if the new position would be outside of the GameWindow
            {
                return true;
            }
            return false; // If no collision is detected
        }
        public bool CollisionLeft(double currentPositionX)
        {
            double newPosition = currentPositionX - movementSpeed;
            if (newPosition < 0) // Checks if the new position would be outside of the GameWindow
            {
                return true;
            }
            return false; // If no collision is detected
        }
        public bool CollisionRight(double currentPositionX)
        {
            double newPosition = currentPositionX + movementSpeed;
            if (newPosition > GameViewWidth) // Checks if the new position would be outside of the GameWindow
            {
                return true;
            }
            return false; // If no collision is detected
        }
        #endregion

        private void LeftPressed()
        {
            MovementDirection = Movement.Left;

        }
    }
}
