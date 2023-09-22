﻿using PacMan.Views.Components;
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
using static System.Net.Mime.MediaTypeNames;

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
        public PlayerViewModel PlayerVM { get; set; } = new PlayerViewModel();
        public int GhostSize { get; set; }
        public int McSize { get; set; }

        public int PlayerEarnedScore { get; set; } = 3;

        public static Movement MovementDirection { get; set; }
        public ICommand LeftPressedCommand { get; set; }
        public ICommand RightPressedCommand { get; set; }
        public ICommand UpPressedCommand { get; set; }
        public ICommand DownPressedCommand { get; set; }
        public double MainCharacterX { get; set; }
        public double MainCharacterY { get; set; }
        public double BlueGhostX { get; set; } = -100;
        public double BlueGhostY { get; set; }
        
        public ICommand BlueGhostAiCommand { get;}
        public BaseUserControl CurrentUserControl { get; set; }

        public ObservableCollection<Obstacles> Obstacles { get; } = new ObservableCollection<Obstacles>();
        public ObservableCollection<GoldCoinViewModel> GoldCoins { get; } = new ObservableCollection<GoldCoinViewModel>();


        public bool blueGhostCollision = false;
        int movementSpeed = 10;
        private readonly DispatcherTimer timer = new DispatcherTimer();
        int timerSpeed = 100;


        //private ObservableCollection<GoldCoin> _goldcoins = new ObservableCollection<GoldCoin>();
        //public ObservableCollection<GoldCoin> Goldcoins
        //{
        //    get { return _goldcoins; }
        //    set { _goldcoins = value; }
        //}


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
            CreateCoinsList();

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

        private void CreateCoinsList()
        {
 
            int ypos = 25;
            for (int i = 0; i < 7; i++)
            {
                int xpos = 10;
                
                GoldCoins.Add(new GoldCoinViewModel { XPosition = xpos, YPosition = ypos });
                xpos = 100;

                for (int k = 0; k < 10; k++)
                {
                    if(!(xpos == 430 && ypos == 525))
                    {
                        GoldCoins.Add(new GoldCoinViewModel { XPosition = xpos, YPosition = ypos });

                    }
                    xpos += 110;

                }

                ypos += 100;


            }
        }

        private void MainCharacterMovementTimer(object? sender, EventArgs e)
        {
            CurrentUserControl = MainCharacter;
            MainCharacterX = MainCharacter.XPosition;
            MainCharacterY = MainCharacter.YPosition;
            MoveContentControl(MovementDirection);

            foreach(var goldcoin in GoldCoins)
            {
                if(IsCollision(MainCharacter, goldcoin))
                {
                    goldcoin.GoldCoinVisibility = Visibility.Collapsed;
                }
            }
        }

        private bool IsCollision(MainCharacter mainCharacter, GoldCoinViewModel goldCoin)
        {
            
            double mainCharacterLeft = mainCharacter.XPosition;
            double mainCharacterTop = mainCharacter.YPosition;
            double mainCharacterRight = mainCharacterLeft + mainCharacter.ActualWidth;
            double mainCharacterBottom = mainCharacterTop + mainCharacter.ActualHeight;

            double goldCoinLeft = goldCoin.XPosition;
            double goldCoinTop = goldCoin.YPosition;
            double goldCoinRight = goldCoinLeft + goldCoin.Width;
            double goldCoinBottom = goldCoinTop + goldCoin.Height;

            // Look if there is a collision between mainCharacter and goldCoin.
            bool collisionDetected = !(mainCharacterRight < goldCoinLeft || mainCharacterLeft > goldCoinRight ||
                                       mainCharacterBottom < goldCoinTop || mainCharacterTop > goldCoinBottom);

            return collisionDetected;
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

        public Movement GetBlueGhostMovementDirection()
        {
            return BlueGhostVM.MovementDirection;
        }
        private void GhostMovementTimer(object sender, EventArgs e)
        {
            BlueGhostX = GhostBlueView.XPosition;
            BlueGhostY = GhostBlueView.YPosition;
            AiDirectionPackage AiPackage = new AiDirectionPackage(new Point(BlueGhostX, BlueGhostY), new Point(MainCharacterX, MainCharacterY), blueGhostCollision);
            BlueGhostVM.Ai(AiPackage);
            GhostAndMcCollision();

            CurrentUserControl = GhostBlueView;
            MoveContentControl(BlueGhostVM.MovementDirection);
        }

        private void MoveContentControl(Movement movementDirection)
        {
            //Point ContentControlPosition = contentControl.TransformToAncestor(this).Transform(new Point(0, 0));
            double currentPositionX = CurrentUserControl.XPosition;
            double currentPositionY = CurrentUserControl.YPosition;
            if (CurrentUserControl == GhostBlueView){ blueGhostCollision = true; }
            //GameViewModel currentGhost = (GhostViewModel)contentControl.Content;
            switch (movementDirection)
            {
                case Movement.Up:
                    if (BorderColisionUp(currentPositionY)) // If collision is detected with the border of the GameView
                    {
                        CurrentUserControl.YPosition = 0;
                    }
                    else if (WallCollision(  movementDirection)) { }
                    else { CurrentUserControl.YPosition -= movementSpeed; if (CurrentUserControl == GhostBlueView) { blueGhostCollision = false; } }
                    break;
                case Movement.Down:
                    if (CollisionDown(currentPositionY + CurrentUserControl.ActualHeight )) // If collision is detected with the border of the GameView
                    {
                        CurrentUserControl.YPosition = GameViewHeight - CurrentUserControl.ActualHeight;
                    }
                    else if (WallCollision( movementDirection)) { }
                    else { CurrentUserControl.YPosition += movementSpeed; if (CurrentUserControl == GhostBlueView) { blueGhostCollision = false; } }
                    break;
                case Movement.Left:
                    if (CollisionLeft(currentPositionX)) // If collision is detected with the border of the GameView
                    {
                        CurrentUserControl.XPosition = 0;

                    }
                    else if (WallCollision( movementDirection)) { }
                    else { CurrentUserControl.XPosition -= movementSpeed; if (CurrentUserControl == GhostBlueView) { blueGhostCollision = false; } }
                    break;

                case Movement.Right:
                    if (CollisionRight(currentPositionX + CurrentUserControl.ActualWidth)) // If no collision is detected with the border of the GameView
                    {
                        CurrentUserControl.XPosition = GameViewWidth - CurrentUserControl.ActualWidth;

                    }
                    else if (WallCollision( movementDirection)) { }
                    else { CurrentUserControl.XPosition += movementSpeed; if (CurrentUserControl == GhostBlueView) { blueGhostCollision = false; } }
                    break;


            }
        }

        private void GhostAndMcCollision()
        {
            if (BlueGhostX < MainCharacterX + MainCharacter.ActualWidth &&
                    BlueGhostX + GhostBlueView.ActualWidth > MainCharacterX &&
                    BlueGhostY < MainCharacterY + MainCharacter.ActualHeight &&
                    BlueGhostY + GhostBlueView.ActualHeight > MainCharacterY)
            {
                // Put the function for pacman losing life here
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
                            CurrentUserControl.YPosition = obstacle.YPosition - CurrentUserControl.ActualHeight;
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

    }
}
