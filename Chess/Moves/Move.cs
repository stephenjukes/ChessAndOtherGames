using System;
using System.Collections.Generic;
using System.Text;

namespace Chess
{
    class Move
    {
        public Square Origin { get; set; }
        public Square Destination { get; set; }
        public Piece Piece { get; set; }
    }
}
