using System;
using System.Collections.Generic;
using System.Linq;

namespace Battleship.Server.Shared
{
    public class Ship : IEnumerable<Cell>
    {
        private readonly List<Cell> _cells;

        public bool IsDestroyed
        {
            get { return _cells.All(c => c.State == CellState.Damaged); }
        }

        public int Size { get { return _cells.Count; } }

        public Ship(IEnumerable<Cell> cells)
        {
            _cells = cells.ToList();
        }

        public bool LocatedIn(Cell cell)
        {
            return _cells.Any(c => c.Equals(cell));
        }
        public void UpdateToDamaged(Cell cell)
        {
            var requiredCell = _cells.FirstOrDefault(p => p.Equals(cell));
            if (requiredCell == null)
                throw new ArgumentException("Unable to find a requested cell");

            requiredCell.State = CellState.Damaged;
        }

        public IEnumerator<Cell> GetEnumerator()
        {
            return _cells.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _cells.GetEnumerator();
        }
    }
}