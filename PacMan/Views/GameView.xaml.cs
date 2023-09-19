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


        }


        
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
               //MoveContentControl(TheMainCharacter, movementSpeed, MovementDirection);
            }
        }

        #endregion


        

    }
}
