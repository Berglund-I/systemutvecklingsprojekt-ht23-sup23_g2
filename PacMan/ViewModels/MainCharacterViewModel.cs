using PacMan.Commands;
using PacMan.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PacMan.ViewModels
{
    internal class MainCharacterViewModel : BaseViewModel
    {
        public int McSize { get; set; } = 40;
        public int McLives { get; set; }
        

        public MainCharacterViewModel()
        {
            
        }

       

       
    }
}
