using Battleship.Server.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Battleship.Server
{
    internal class Player
    {
        public Guid PlayerId { get; set; }
        public string Name { get; set; }
        public List<Ship> Ships { get; set; }

        public IPlayerCallback CallBack { get; set; }

        public bool IsReady
        {
            get { return Ships != null && Ships.Count > 0; }
        }

        public bool IsAlive
        {
            get { return IsReady && Ships.Any(s => !s.IsDestroyed); }
        }

        public bool HisTurn { get; set; }

        public ShootResult ProcessShoot(int x, int y)
        {
            var cell = new Cell(x, y);
            var ship = Ships.FirstOrDefault(s => s.Contains(cell));

            ShootResult result;
            if (ship == null)
                result = ShootResult.Miss;
            else
            {
                result = ShootResult.Damaged;
                ship.UpdateToDamaged(cell);

                if (ship.IsDestroyed)
                    result = ShootResult.Destroyed;
            }
            return result;
        }
    }
}