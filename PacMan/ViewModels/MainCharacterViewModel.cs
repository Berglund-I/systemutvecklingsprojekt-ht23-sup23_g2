using PacMan.Commands;
using PacMan.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PacMan.ViewModels
{
    internal class MainCharacterViewModel : BaseViewModel
    {
        public int McSize { get; set; } = 40;
        public int McLives { get; set; }
        public static Movement MovementDirection { get; set; }
        public ICommand LeftPressedCommand { get; set; }
        public ICommand RightPressedCommand { get; set; }
        public ICommand UpPressedCommand { get; set; }
        public ICommand DownPressedCommand { get; set; }

        public MainCharacterViewModel()
        {
            LeftPressedCommand = new RelayCommand(x => LeftPressed());
            RightPressedCommand = new RelayCommand(x => RightPressed());
            UpPressedCommand = new RelayCommand(x => UpPressed());
            DownPressedCommand = new RelayCommand(x => DownPressed());
            MovementDirection = Movement.Down;
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
