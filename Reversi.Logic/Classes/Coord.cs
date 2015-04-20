using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reversi.Logic.Classes
{
    public struct Coord
    {
        public int x, y;

        public Coord(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public override string ToString()
        {
            return string.Format("{0}{1}", (char)('A' + x), y + 1);
        }

        public static Coord operator +(Coord c1, Coord c2)
        {
            return new Coord(c1.x + c2.x, c1.y + c2.y);
        }
        public static Coord operator -(Coord c1, Coord c2)
        {
            return new Coord(c1.x - c2.x, c1.y - c2.y);
        }
        public static bool operator ==(Coord c1, Coord c2)
        {
            return c1.x == c2.x && c1.y == c2.y;
        }
        public static bool operator !=(Coord c1, Coord c2)
        {
            return !(c1 == c2);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override bool Equals(object obj)
        {
            return obj is Coord && (Coord)obj == this;
        }

        public static Coord Empty = new Coord(Int32.MinValue, Int32.MinValue);
    }
}