using PacMan.Views.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacMan.ViewModels
{
    internal class PlayerViewModel : BaseViewModel
    {
        /// <summary>
        /// Count of the total amount of possible score in the game
        /// </summary>

        //public ObservableCollection<GoldCoinViewModel> TotalScore { get; private set; } = new ObservableCollection<GoldCoinViewModel>();


        public int PlayerEarnedScore { get; set; }

        public int PlayerLives { get; set; } = 4;

         
    }
}
