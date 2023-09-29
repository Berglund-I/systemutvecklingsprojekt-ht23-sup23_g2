using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace PacMan.ViewModels
{
    class GoldCoinViewModel : BaseViewModel
    {

        public int XPosition { get; set; }
        public int YPosition { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        //public Visibility GoldCoinVisibility { get; set; } = Visibility.Visible;

        public GoldCoinViewModel() 
        {
            
        }
    }
}
