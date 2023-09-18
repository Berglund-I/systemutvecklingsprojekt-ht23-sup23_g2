using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacMan.ViewModels
{
    internal class MainCharacterViewModel : BaseViewModel
    {
        public int McSize { get; set; } = 40;
        public int McLives { get; set; }
    }
}
