using PacMan.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacMan.Models
{
    class GameScore : BaseViewModel
    {
        public GameScore(int score) 
        {
            Score = score;
        }
        public int Score { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        
    }
}
