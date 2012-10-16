using System.Runtime.Serialization;

namespace Battleship.Server
{
    [DataContract]
    public class Cell
    {
        [DataMember]
        public int X;

        [DataMember]
        public int Y;

        public Cell(int x, int y)
        {
            this.X = x;
            this.Y = y;
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

            return this.X == p.X && this.Y == p.Y; // && this.State == p.State;
        }
    }
}