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

namespace PacMan.Views.Components
{
    /// <summary>
    /// Interaction logic for GameMapPiece.xaml
    /// </summary>
    public partial class GameMapPiece : UserControl
    {



        public SolidColorBrush MapColor
        {
            get { return (SolidColorBrush)GetValue(MapColorProperty); }
            set { SetValue(MapColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MapColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MapColorProperty =
            DependencyProperty.Register("MapColor", typeof(SolidColorBrush), typeof(GameMapPiece), new PropertyMetadata(Brushes.White));


        public GameMapPiece()
        {
            InitializeComponent();
        }
    }
}
