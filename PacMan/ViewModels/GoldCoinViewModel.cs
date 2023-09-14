using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Threading;

namespace PacMan.ViewModels
{
    class GoldCoinViewModel : BaseViewModel
    {

        public int CoinSize { get; set; } = 20;
        public GoldCoinViewModel() 
        {
            
        }
    }
}
