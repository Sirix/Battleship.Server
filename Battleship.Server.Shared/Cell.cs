namespace Battleship.Server.Shared
{
    public enum CellState
    {
        Empty = 0,
        Ship = 1,

        MissedAttack = -1,
        Damaged = -2,
        Destroyed = -3,
    }

    public class Cell
    {
        public CellState State;
        public int X { get; private set; }
        public int Y { get; private set; }

        public Cell(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Cell(int x, int y, CellState state)
            : this(x, y)
        {
            State = state;
        }

        public override string ToString()
        {
            const char a = 'A';

            return string.Format("[{0}-{1}]", (char)(a + X), (Y + 1)); //we store x and y in started from 0 arrays, so add 1 to y
            //but don't add to X because 'A' has a zero itself
        }

        public override bool Equals(object obj)
        {
            var p = (Cell)obj;

            return X == p.X && Y == p.Y;// && this.State == p.State;
        }
    }
}