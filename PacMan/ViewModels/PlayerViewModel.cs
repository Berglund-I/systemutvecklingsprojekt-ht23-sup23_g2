using PacMan.Views.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PacMan.ViewModels
{
    internal class PlayerViewModel : BaseViewModel
    {

        public int PlayerLives { get; set; } = 2;
    }

    public class Player 
    {
        public ObservableCollection<Player> PlayerSave { get; set; } = new ObservableCollection<Player>();
        public int PlayerFinalScore { get; set; }
        public string PlayerNameSave { get; set; }
        public Visibility PlayerSaveVisibility { get; set; } = Visibility.Collapsed;
    }
}
