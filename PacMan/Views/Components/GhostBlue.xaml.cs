using PacMan.ViewModels.Ghosts;
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
    /// Interaction logic for GhostBlue.xaml
    /// </summary>
    public partial class GhostBlue : BaseUserControl
    {

        public GhostBlue()
        {
            InitializeComponent();
            XPosition = 100;
            YPosition = 250;
        }
    }
}
