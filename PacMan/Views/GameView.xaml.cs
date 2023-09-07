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


namespace PacMan.Views
{
    /// <summary>
    /// Interaction logic for GameView.xaml
    /// </summary>
    public partial class GameView : UserControl
    {
        public double CurrentPositionX { get; set; } = 0; // Initial position (left)
        public double CurrentPositionY { get; set; } = 0; // Initial position (Top)
        private static readonly Random _random = new();
        private int movementSpeed = 10;
        Movement test;

        public GameView()
        {
            InitializeComponent();
            DataContext = new GameViewModel();

            timer.Interval = TimeSpan.FromMilliseconds(50);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        #region "Ghost Code"
        private readonly DispatcherTimer timer = new DispatcherTimer();

        private static Movement GetRandomDirection()
        {
            int magicNumer = _random.Next(4);
            return (Movement)magicNumer;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            test = GetRandomDirection();
            switch (test)
            {
                case Movement.Left:
                    CurrentPositionX -= 1 * movementSpeed; // Movment speed
                    if (CurrentPositionX < 0) //Makes sure the ghost does not move out of bounds
                    {
                        CurrentPositionX = 0;
                    }
                    Canvas.SetLeft(TheBlueGhost, CurrentPositionX);
                    break;

                case Movement.Right:
                    CurrentPositionX += 1 * movementSpeed; // Movment speed
                    if (CurrentPositionX > this.Width) //Makes sure the ghost does not move out of bounds
                    {
                        CurrentPositionX = -TheBlueGhost.Width;
                    }
                    Canvas.SetLeft(TheBlueGhost, CurrentPositionX);
                    break;

                case Movement.Up:
                    CurrentPositionY -= 1 * movementSpeed; // Movment speed
                    if (CurrentPositionY < 0) //Makes sure the ghost does not move out of bounds
                    {
                        CurrentPositionY = 0;
                    }
                    Canvas.SetTop(TheBlueGhost, CurrentPositionY);
                    break;

                case Movement.Down:
                    CurrentPositionY += 1 * movementSpeed; // Movment speed
                    if (CurrentPositionX > this.Height) //Makes sure the ghost does not move out of bounds
                    {
                        CurrentPositionX = -TheBlueGhost.Height;
                    }
                    Canvas.SetTop(TheBlueGhost, CurrentPositionY);
                    break;
            }

            // Moves the ghost




            // Reset the position when it reaches the right edge of the window
            //if (currentposition > this.width)
            //{
            //    currentposition = -theblueghost.width;
            //}
        }
        #endregion
    }
}
