using PacMan.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacMan.Models
{
    class PlayerLifeModel : BaseViewModel
    {
        public string name { get; set; }

        public PlayerLifeModel(string Name)
        {
            name = Name;
        }

    }
}
