using Reversi.Logic.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reversi.Logic.Common
{
    public class Turn
    {
        public Board Board { get; set; }
        public Player PlayerThatPlayed { get; set; }
        public Player PlayerToPlay { get; set; }
        public override string ToString()
        {
            return PlayerThatPlayed.Token.ToString();
        }
    }

    public class OnePartTurn : Turn
    {
        public Coord Coord { get; set; }
        public override string ToString()
        {
            return PlayerThatPlayed.Token.ToString() + " " + Coord.ToString();
        }
    }

    public class TwoPartTurn : Turn
    {
        public Coord Start { get; set; }
        public Coord End { get; set; }

        public string Coord { get { return Start + ":" + End; } }

        public TwoPartTurn()
        {
        }

        public TwoPartTurn(int x1, int y1, int x2, int y2)
        {
            Start = new Coord(x1, y1);
            End = new Coord(x2, y2);
        }
    }
}