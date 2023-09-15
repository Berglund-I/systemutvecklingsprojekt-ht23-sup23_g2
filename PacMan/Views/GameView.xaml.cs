using PacMan.ViewModels;
using PacMan.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using static System.Net.Mime.MediaTypeNames;
using System.Threading;
using PacMan.Views.Components;
using PacMan.ViewModels.Ghosts;
using PacMan.Models;

namespace PacMan.Views
{
    /// <summary>
    /// Interaction logic for GameView.xaml
    /// </summary>
    public partial class GameView : UserControl
    {
        private static readonly Random _random = new();
        Movement RandomMovmentDirection;

        private int movementSpeed = 10;
        Movement MovementDirection;
        bool mcMovement = false;
        int timerSpeed = 100;
        ContentControl currentContentControl;

        bool blueGhostCollision = true;


        public GameView()
        {
            InitializeComponent();
            GameCanvas.Focus();
            DataContext = new GameViewModel();

            timer.Interval = TimeSpan.FromMilliseconds(timerSpeed);
            timer.Tick += GhostMovementTimer;
            timer.Tick += MainCharacterMovementTimer;
            timer.Start();
        }


        #region "Ghost Code"
        private readonly DispatcherTimer timer = new DispatcherTimer();

        /// <summary>
        /// Generates a random movement direction
        /// </summary>
        /// <returns>Movment direction,(up, down, left, right)</returns>
        private static Movement GetRandomDirection()
        {
            int magicNumer = _random.Next(4);
            return (Movement)magicNumer;
        }

        private Movement GhostBlueAi()
        {
            Point McCurrentPosition = new Point(Canvas.GetLeft(TheMainCharacter), Canvas.GetTop(TheMainCharacter));
            Point GhostCurrentPosition = new Point(Canvas.GetLeft(TheBlueGhost), Canvas.GetTop(TheBlueGhost));
            AiDirectionPackage aiDirectionPackage = new AiDirectionPackage(GhostCurrentPosition, McCurrentPosition, blueGhostCollision);

            var model = (GameViewModel)DataContext;
            
            model.BlueGhostAiCommand.Execute(aiDirectionPackage);

            return model.GetBlueGhostMovementDirection();
        }
        /// <summary>
        /// Makes the ghosts move
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GhostMovementTimer(object sender, EventArgs e)
        {
            //RandomMovmentDirection = GetRandomDirection();
            Movement aiTest = GhostBlueAi();

            //blueGhostLastPosition = new Point(Canvas.GetLeft(TheBlueGhost), Canvas.GetTop(TheBlueGhost));
            MoveContentControl(TheBlueGhost, movementSpeed, aiTest); /*Remove comment to move the Ghost :)*/
            
        }
        #endregion

        #region "Main Charachter Code"
        private void GameCanvas_KeyDown(object sender, KeyEventArgs e)
        {
              
            switch (e.Key)
            {
                case Key.Left:
                    MovementDirection = Movement.Left;
                    break;
                case Key.Up:
                    MovementDirection = Movement.Up;
                    break;
                case Key.Right:
                    MovementDirection = Movement.Right;
                    break;
                case Key.Down:
                    MovementDirection = Movement.Down;
                    break;
            }
            mcMovement = true;
        }
        private void MainCharacterMovementTimer(object? sender, EventArgs e)
        {
            if (mcMovement == true)
            {
               MoveContentControl(TheMainCharacter, movementSpeed, MovementDirection);
            }
        }

        #endregion


        /// <summary>
        /// Takes a content control, a speed intager and a movment direction and moves the content control
        /// the specified amount of pixels in the movment direction specified.
        /// </summary>
        /// <param name="contentControl"></param>
        /// <param name="movementSpeed"></param>
        /// <param name="movementDirection"></param>
        private void MoveContentControl(ContentControl contentControl, int movementSpeed, Movement movementDirection)
        {
            //Point ContentControlPosition = contentControl.TransformToAncestor(this).Transform(new Point(0, 0));
            double currentPositionX = Canvas.GetLeft(contentControl);
            double currentPositionY = Canvas.GetTop(contentControl);
            if (contentControl.Name == "TheBlueGhost")
            {
                blueGhostCollision = true;
            }
            //GameViewModel currentGhost = (GhostViewModel)contentControl.Content;
            switch (movementDirection)
            {
                case Movement.Up:
                    if  (BorderColisionUp(currentPositionY, movementSpeed, contentControl)) // If collision is detected with the border of the GameView
                    {
                        Canvas.SetTop(contentControl, 0);
                    }
                    else if (WallCollision(contentControl, movementSpeed, movementDirection)) { }
                    else { Canvas.SetTop(contentControl, currentPositionY - movementSpeed);if (contentControl.Name == "TheBlueGhost") { blueGhostCollision = false; } }
                    break;
                case Movement.Down:
                    if (CollisionDown(currentPositionY + contentControl.Height, movementSpeed, contentControl)) // If collision is detected with the border of the GameView
                    {
                        Canvas.SetTop(contentControl, this.Height - contentControl.Height);
                    }
                    else if (WallCollision(contentControl, movementSpeed, movementDirection)) { }
                    else { Canvas.SetTop(contentControl, currentPositionY + movementSpeed); if (contentControl.Name == "TheBlueGhost") { blueGhostCollision = false; } } 
                    break;
                case Movement.Left:
                    if (CollisionLeft(currentPositionX, movementSpeed, contentControl)) // If collision is detected with the border of the GameView
                    {
                        Canvas.SetLeft(contentControl, 0);
                        
                    }
                    else if (WallCollision(contentControl, movementSpeed, movementDirection)) { }
                    else { Canvas.SetLeft(contentControl, currentPositionX - movementSpeed); if (contentControl.Name == "TheBlueGhost") { blueGhostCollision = false; } }
                    break;

                case Movement.Right:
                    if (CollisionRight(currentPositionX + contentControl.Width, movementSpeed, contentControl)) // If no collision is detected with the border of the GameView
                    {
                        Canvas.SetLeft(contentControl, this.Width - contentControl.Width);
                        
                    }
                    else if (WallCollision(contentControl, movementSpeed, movementDirection)) { }
                    else { Canvas.SetLeft(contentControl, currentPositionX + movementSpeed); if (contentControl.Name == "TheBlueGhost") { blueGhostCollision = false; } }
                    break;


            }
        }


        #region collision controls
        private bool WallCollision(ContentControl contentControl, int movementSpeed, Movement movementDirection)
        {
            switch (movementDirection)
            {
                case Movement.Up:
                    Rect contentControlRectUp = new Rect(Canvas.GetLeft(contentControl), Canvas.GetTop(contentControl) - movementSpeed, contentControl.Width, contentControl.Height); // A rectangle with positions of the predicted move from the contentControll
                    foreach (var x in GameCanvas.Children.OfType<Rectangle>())
                    {
                        if ((string)x.Tag == "wall")
                        {
                            Rect wallRect = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);

                            if (contentControlRectUp.IntersectsWith(wallRect))
                            {
                                Canvas.SetTop(contentControl, Canvas.GetTop(x) + x.Height + 1);
                                return true;
                            }
                        }
                    }
                    break;
                case Movement.Down:
                    Rect contentControlRectDown = new Rect(Canvas.GetLeft(contentControl), Canvas.GetTop(contentControl) + movementSpeed, contentControl.Width, contentControl.Height);
                    foreach (var x in GameCanvas.Children.OfType<Rectangle>())
                    {
                        if ((string)x.Tag == "wall")
                        {
                            Rect wallRect = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);

                            if (contentControlRectDown.IntersectsWith(wallRect))
                            {
                                Canvas.SetTop(contentControl, Canvas.GetTop(x) - contentControl.Height - 1);
                                return true;
                            }
                        }
                    }
                    break;
                case Movement.Left:
                    Rect contentControlRectLeft = new Rect(Canvas.GetLeft(contentControl) - movementSpeed, Canvas.GetTop(contentControl) , contentControl.Width, contentControl.Height);
                    foreach (var x in GameCanvas.Children.OfType<Rectangle>())
                    {
                        if ((string)x.Tag == "wall")
                        {
                            Rect wallRect = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);

                            if (contentControlRectLeft.IntersectsWith(wallRect))
                            {
                                Canvas.SetLeft(contentControl, Canvas.GetLeft(x) + x.Width + 1);
                                return true;
                            }
                        }
                    }
                    break;

                case Movement.Right:
                    Rect contentControlRectRight = new Rect(Canvas.GetLeft(contentControl) + movementSpeed, Canvas.GetTop(contentControl), contentControl.Width, contentControl.Height);
                    foreach (var x in GameCanvas.Children.OfType<Rectangle>())
                    {
                        if ((string)x.Tag == "wall")
                        {
                            Rect wallRect = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);

                            if (contentControlRectRight.IntersectsWith(wallRect))
                            {
                                Canvas.SetLeft(contentControl, Canvas.GetLeft(x) - contentControl.Width - 1);
                                return true;
                            }
                        }
                    }
                    break;

            }
            return false;
        }

        public bool BorderColisionUp(double currentPositionY, int movementSpeed, ContentControl contentControl)
        {
            double newPosition = currentPositionY - movementSpeed;
            if (newPosition < 0) // Checks if the new position would be outside of the GameWindow
            {
                return true; 
            }
            return false; // If no collision is detected
        }
        public bool CollisionDown(double currentPositionY, int movementSpeed, ContentControl contentControl)
        {
            double newPosition = currentPositionY + (1 * movementSpeed);
            if (newPosition > this.Height) // Checks if the new position would be outside of the GameWindow
            {
                return true;
            }
            return false; // If no collision is detected
        }
        public bool CollisionLeft(double currentPositionX, int movementSpeed, ContentControl contentControl)
        {
            double newPosition = currentPositionX - movementSpeed;
            if (newPosition < 0) // Checks if the new position would be outside of the GameWindow
            {
                return true;
            }
            return false; // If no collision is detected
        }
        public bool CollisionRight(double currentPositionX, int movementSpeed, ContentControl contentControl)
        {
            double newPosition = currentPositionX + movementSpeed;
            if (newPosition > this.Width) // Checks if the new position would be outside of the GameWindow
            {
                return true;
            }
            return false; // If no collision is detected
        }
        #endregion

    }
}
