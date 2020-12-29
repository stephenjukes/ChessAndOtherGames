using System;
using System.Collections.Generic;
using System.Text;

namespace Chess
{
    class CompassPoint
    {
        public CompassPoint(Direction direction, int bearing, Func<int, Func<Board, Square, IEnumerable<Square>>> scopeFunc)
        {
            Direction = direction;
            Bearing = bearing;
            ScopeFunc = scopeFunc;
        }

        public Direction Direction { get; set; }
        public int Bearing { get; set; }
        public Func<int, Func<Board, Square, IEnumerable<Square>>> ScopeFunc { get; set; }
    }
}
