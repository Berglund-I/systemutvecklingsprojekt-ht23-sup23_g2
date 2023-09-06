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

namespace PacMan.Views.Entities
{
    /// <summary>
    /// Interaction logic for Coins.xaml
    /// </summary>
    public partial class GoldCoin : Coin
    {
        /// <summary>
        /// Score for normal coins
        /// </summary>
        public GoldCoin()
        {
            InitializeComponent();
            Score = 1;
        }
    }
}
