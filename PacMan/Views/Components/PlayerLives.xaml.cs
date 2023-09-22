using PacMan.ViewModels;
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
    /// Interaction logic for PlayerLives.xaml
    /// </summary>
    public partial class PlayerLives : UserControl
    {
            int numberOfLives;
        public PlayerLives()
        {
            InitializeComponent();

            var myPanel = new StackPanel();
            myPanel.Margin = new Thickness(10);

            var myRectangle = new Rectangle();
            myRectangle.Name = "myRectangle";
            this.RegisterName(myRectangle.Name, myRectangle);
            myRectangle.Width = 100;
            myRectangle.Height = 100;
            myRectangle.Fill = Brushes.Blue;
            myPanel.Children.Add(myRectangle);
            this.Content = myPanel;
            
        }

    }
}
