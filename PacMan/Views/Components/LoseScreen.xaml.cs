using PacMan.ViewModels;
using System;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PacMan.Views.Components
{
    /// <summary>
    /// Interaction logic for LoseScreen.xaml
    /// </summary>
    public partial class LoseScreen : UserControl
    {
        private readonly ImageSource[] images;
        public int CurrentImage;
        ObjectAnimationUsingKeyFrames animation = new ObjectAnimationUsingKeyFrames();
        public LoseScreen()
        {
            InitializeComponent();
            //DataContext = new GameViewModel();
            images = new ImageSource[11];
            int imageCounter = 0;
            images[0] = new BitmapImage(new Uri("pack://application:,,,/PacMan;component/Views/Images/PacManFirstUp.png"));
            for (int i = 1; i < 11; i++)
            {
                images[i] = new BitmapImage(new Uri($"pack://application:,,,/PacMan;component/Views/Images/{imageCounter}.png"));
                imageCounter++;
            }
            //_deadAnimation = new Storyboard();
            animation.Duration = TimeSpan.FromSeconds(1.5);
            animation.RepeatBehavior = RepeatBehavior.Forever;

            foreach (var image in images)
            {
                //var keyframe = new DiscreteObjectKeyFrame(image);
                animation.KeyFrames.Add(new DiscreteObjectKeyFrame(image));
            }
        }

        public void StartAnimation()
        {
            CurrentImage = 0;
            deadImage.BeginAnimation(Image.SourceProperty, animation);
        }
    }
}
