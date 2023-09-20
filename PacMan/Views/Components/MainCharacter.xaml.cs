using PacMan.ViewModels;
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

namespace PacMan.Views.Components
{
    /// <summary>
    /// Interaction logic for MainCharacter.xaml
    /// </summary>
    public partial class MainCharacter : UserControl
    {
        public double YPosition = 260;
        public double XPosition = 300;
        public int Size;
        public MainCharacter()
        {
            InitializeComponent();
            Size = 40;
        }
    }
}
