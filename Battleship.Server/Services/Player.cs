using Battleship.Server.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Battleship.Server
{
    class Player
    {
        public Guid PlayerId { get; set; }
        public string Name { get; set; }

        public IPlayerCallback CallBack { get; set; }
    }
}
