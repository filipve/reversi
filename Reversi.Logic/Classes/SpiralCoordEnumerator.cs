using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reversi.Logic.Classes
{
    /// <summary>
    /// Enumerate through 2d coord [size,size] in a spiral way (counterclockwise). Starts at the low right corner of the spiral defined at distanceFromCenter
    /// </summary>
    public class SpiralCoordEnumerator : IEnumerator<Coord>
    {

        private int _x;
        private int _y;
        int _distanceFromCenter;
        int _size;

        public SpiralCoordEnumerator(int size, int distanceFromCenter)
        {
            this._size = size;

            _x = int.MinValue;
            _y = int.MinValue;

            this._distanceFromCenter = distanceFromCenter;
        }


        public Coord Current
        {
            get { return new Coord(_x, _y); }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public bool MoveNext()
        {
            if (_x == int.MinValue)
            {
                _x = (_size / 2) - _distanceFromCenter;
                _y = (_size / 2) - _distanceFromCenter;
            }
            else
            {
                DoTheMove();
            }


            return _x >= 0;
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }

        private void DoTheMove()
        {
            if (_x + _y < _size - 1)
            {
                if (_x < _y)
                {
                    _y--;
                }
                else
                {
                    _x++;
                }
            }
            else
            {
                if (_x > _y)
                {
                    _y++;
                }
                else
                {
                    _x--;
                }
            }
        }

        object System.Collections.IEnumerator.Current
        {
            get { throw new NotImplementedException(); }
        }
    }
}
