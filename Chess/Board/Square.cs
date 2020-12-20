using System;
using System.Collections.Generic;
using System.Text;

namespace Chess
{
    class Square
    {
        public int Row { get; set; }
        public SquareColumn Column { get; set; }
        public Piece OccupyingPiece { get; set; }

        public Square(int row, SquareColumn column)
        {
            Row = row;
            Column = column;
        }
    }
}
