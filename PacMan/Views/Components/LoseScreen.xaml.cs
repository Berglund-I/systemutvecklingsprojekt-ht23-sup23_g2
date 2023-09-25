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
        //private readonly Storyboard _deadAnimation;
        private readonly ImageSource[] images;
        //private Image Image;
        public int CurrentImage;
            ObjectAnimationUsingKeyFrames animation = new ObjectAnimationUsingKeyFrames();
        public LoseScreen()
        {
            InitializeComponent();

            images = new ImageSource[10];

            for (int i = 0; i < 10; i++)
            {
                images[i] = new BitmapImage(new Uri($"pack://application:,,,/PacMan;component/Views/Images/{i}.png"));
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
        public void StopAnimation()
        {

        }
    }
}
