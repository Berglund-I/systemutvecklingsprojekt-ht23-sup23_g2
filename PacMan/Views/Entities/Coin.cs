using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PacMan.Views.Entities
{
    public class Coin : UserControl
    {


        public int Score
        {
            get { return (int)GetValue(ScoreProperty); }
            set { SetValue(ScoreProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Score.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ScoreProperty =
            DependencyProperty.Register("Score", typeof(int), typeof(Coin), new PropertyMetadata(0,
                new PropertyChangedCallback(SetScore)));

        private static void SetScore(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var coin = (Coin)d;
            coin.Score = (int)e.NewValue;
        }
    }
}
