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
using System.Drawing;
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


        //private const int _mapSize = 20;
        public bool blueGhostCollision = false;
        int movementSpeed = 10;
        private readonly DispatcherTimer timer = new DispatcherTimer();
        int timerSpeed = 100;

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

            timer.Interval = TimeSpan.FromMilliseconds(timerSpeed);
            timer.Tick += GhostMovementTimer;
            timer.Tick += MainCharacterMovementTimer;
            timer.Start();

            BlueGhostAiCommand = new RelayCommand(execute: x => BlueGhostVM.Ai((AiDirectionPackage)x));
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

        public Movement GetBlueGhostMovementDirection()
        {
            return BlueGhostVM.MovementDirection;
        }
        private void GhostMovementTimer(object sender, EventArgs e)
        {
            //Movement aiTest = GhostBlueAi();
            BlueGhostX = GhostBlueView.XPosition;
            BlueGhostY = GhostBlueView.YPosition;

            CurrentUserControl = GhostBlueView;
            MoveContentControl(Movement.Right);
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
                        CurrentUserControl.YPosition = 0;//Canvas.SetTop(contentControl, 0);
                    }
                    else if (WallCollision(  movementDirection)) { }
                    else { CurrentUserControl.YPosition -= movementSpeed;/*Canvas.SetTop(contentControl, currentPositionY - movementSpeed);*/ }
                    break;
                case Movement.Down:
                    if (CollisionDown(currentPositionY + CurrentUserControl.ActualHeight /*+ contentControl.Height*/)) // If collision is detected with the border of the GameView
                    {
                        CurrentUserControl.YPosition = GameViewHeight - CurrentUserControl.ActualHeight;//Canvas.SetTop(contentControl, GameViewHeight - contentControl.Height);
                    }
                    else if (WallCollision( movementDirection)) { }
                    else { CurrentUserControl.YPosition += movementSpeed;/*Canvas.SetTop(contentControl, currentPositionY + movementSpeed);*/ }
                    break;
                case Movement.Left:
                    if (CollisionLeft(currentPositionX)) // If collision is detected with the border of the GameView
                    {
                        CurrentUserControl.XPosition = 0;//Canvas.SetLeft(contentControl, 0);

                    }
                    else if (WallCollision( movementDirection)) { }
                    else { CurrentUserControl.XPosition -= movementSpeed;/*Canvas.SetLeft(contentControl, currentPositionX - movementSpeed);*/ }
                    break;

                case Movement.Right:
                    if (CollisionRight(currentPositionX + CurrentUserControl.ActualWidth/* + contentControl.Width*/)) // If no collision is detected with the border of the GameView
                    {
                        CurrentUserControl.XPosition = GameViewWidth - CurrentUserControl.ActualWidth;//Canvas.SetLeft(contentControl, GameViewWidth - contentControl.Width);

                    }
                    else if (WallCollision( movementDirection)) { }
                    else { CurrentUserControl.XPosition += movementSpeed;/*Canvas.SetLeft(contentControl, currentPositionX + movementSpeed);*/ }
                    break;


            }
        }


        #region collision controls
        private bool WallCollision( Movement movementDirection)
        {
            //switch (movementDirection)
            //{
            //    case Movement.Up:
            //        Rect contentControlRectUp = new Rect(Canvas.GetLeft(contentControl), Canvas.GetTop(contentControl) - movementSpeed, contentControl.Width, contentControl.Height); // A rectangle with positions of the predicted move from the contentControll
            //        foreach (var x in GameCanvas.Children.OfType<Rectangle>())
            //        {
            //            if ((string)x.Tag == "wall")
            //            {
            //                Rect wallRect = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);

            //                if (contentControlRectUp.IntersectsWith(wallRect))
            //                {
            //                    Canvas.SetTop(contentControl, Canvas.GetTop(x) + x.Height + 1);
            //                    return true;
            //                }
            //            }
            //        }
            //        break;
            //    case Movement.Down:
            //        Rect contentControlRectDown = new Rect(Canvas.GetLeft(contentControl), Canvas.GetTop(contentControl) + movementSpeed, contentControl.Width, contentControl.Height);
            //        foreach (var x in GameCanvas.Children.OfType<Rectangle>())
            //        {
            //            if ((string)x.Tag == "wall")
            //            {
            //                Rect wallRect = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);

            //                if (contentControlRectDown.IntersectsWith(wallRect))
            //                {
            //                    Canvas.SetTop(contentControl, Canvas.GetTop(x) - contentControl.Height - 1);
            //                    return true;
            //                }
            //            }
            //        }
            //        break;
            //    case Movement.Left:
            //        Rect contentControlRectLeft = new Rect(Canvas.GetLeft(contentControl) - movementSpeed, Canvas.GetTop(contentControl), contentControl.Width, contentControl.Height);
            //        foreach (var x in GameCanvas.Children.OfType<Rectangle>())
            //        {
            //            if ((string)x.Tag == "wall")
            //            {
            //                Rect wallRect = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);

            //                if (contentControlRectLeft.IntersectsWith(wallRect))
            //                {
            //                    Canvas.SetLeft(contentControl, Canvas.GetLeft(x) + x.Width + 1);
            //                    return true;
            //                }
            //            }
            //        }
            //        break;

            //    case Movement.Right:
            //        Rect contentControlRectRight = new Rect(Canvas.GetLeft(contentControl) + movementSpeed, Canvas.GetTop(contentControl), contentControl.Width, contentControl.Height);
            //        foreach (var x in GameCanvas.Children.OfType<Rectangle>())
            //        {
            //            if ((string)x.Tag == "wall")
            //            {
            //                Rect wallRect = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);

            //                if (contentControlRectRight.IntersectsWith(wallRect))
            //                {
            //                    Canvas.SetLeft(contentControl, Canvas.GetLeft(x) - contentControl.Width - 1);
            //                    return true;
            //                }
            //            }
            //        }
            //        break;

            //}
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
