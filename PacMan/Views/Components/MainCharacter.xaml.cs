using PacMan.Commands;
using PacMan.Enums;
using PacMan.ViewModels;
using PacMan.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
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

namespace PacMan.Views.Components
{
    /// <summary>
    /// Interaction logic for MainCharacter.xaml
    /// </summary>
    public partial class MainCharacter : BaseUserControl
    {
        public int Size;
        public static Movement movement;
        public ICommand LeftPressedCommand { get; set; }
        public ICommand RightPressedCommand { get; set; }
        public ICommand UpPressedCommand { get; set; }
        public ICommand DownPressedCommand { get; set; }
        public string firstCurrentImage;
        public string secondCurrentImage;

        public MainCharacter()
        {
            InitializeComponent();
            Size = 40;
            xStartPosition = 0;
            yStartPosition = 0;
            LeftPressedCommand = new RelayCommand(x => LeftPressed());
            RightPressedCommand = new RelayCommand(x => RightPressed());
            UpPressedCommand = new RelayCommand(x => UpPressed());
            DownPressedCommand = new RelayCommand(x => DownPressed());
            firstCurrentImage= "pack://application:,,,/PacMan;component/Views/PacManFirst.png";
            secondCurrentImage = "pack://application:,,,/PacMan;component/Views/PacManSecond.png";

            Occupation = Occupation.Pacman;
        }

        private void DownPressed()
        {
            movement = Movement.Down;
            firstCurrentImage = "pack://application:,,,/PacMan;component/Views/PacManFirstDown.png";
            secondCurrentImage = "pack://application:,,,/PacMan;component/Views/PacManSecondDown.png";

        }

        private void UpPressed()
        {
            movement = Movement.Up;
            firstCurrentImage = "pack://application:,,,/PacMan;component/Views/PacManFirstUp.png";
            secondCurrentImage = "pack://application:,,,/PacMan;component/Views/PacManSecondUp.png";

        }

        private void RightPressed()
        {
            movement = Movement.Right;
            firstCurrentImage = "pack://application:,,,/PacMan;component/Views/PacManFirst.png";
            secondCurrentImage = "pack://application:,,,/PacMan;component/Views/PacManSecond.png";


        }
        private void LeftPressed()
        {
            movement = Movement.Left;
            firstCurrentImage = "pack://application:,,,/PacMan;component/Views/PacManFirstLeft.png";
            secondCurrentImage = "pack://application:,,,/PacMan;component/Views/PacManSecondLeft.png";


        }
    }
}
