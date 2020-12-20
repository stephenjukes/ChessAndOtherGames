using System;
using System.Collections.Generic;
using System.Text;

namespace Chess
{
    abstract class Piece
    {
        public Piece(PieceColor color, Square position)
        {
            Color = color;
            Position = position;
        }

        public abstract int Value { get; }
        public abstract IEnumerable<Func<Board, Square, IEnumerable<Square>>> ScopeFuncs();
        public PieceColor Color { get; set; }
        public Square Position { get; set; }
    }
}
