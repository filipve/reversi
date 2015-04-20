using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reversi.Logic.Classes
{
    public class OffsetCalculator
    {
        private Coord originalCoord;
        private Coord offsetedCoord;

        private Coord direction;

        public int ResultX { get { return offsetedCoord.x; } }
        public int ResultY { get { return offsetedCoord.y; } }

        public OffsetCalculator(int x, int y)
        {
            originalCoord = new Coord()
            {
                x = x,
                y = y
            };
        }

        public void setDirection(int dir)
        {
            direction.x = dir % 3 - 1;
            direction.y = dir / 3 - 1;
        }

        public void MakeOffset(int offset)
        {
            offsetedCoord = new Coord()
            {
                x = originalCoord.x + offset * direction.x,
                y = originalCoord.y + offset * direction.y
            };
        }
    }
}
