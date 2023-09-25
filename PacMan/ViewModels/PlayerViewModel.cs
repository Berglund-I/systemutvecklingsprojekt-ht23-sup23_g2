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
        public int PlayerEarnedScore { get; set; }
        public int PlayerLives { get; set; } = 0;
    }
}
