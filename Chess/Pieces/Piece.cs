using System;
using System.Collections.Generic;
using System.Text;

namespace Chess
{
    abstract class Piece
    {
        public Piece(PieceColor color, Square homeSquare)
        {
            Color = color;
            HomeSquare = homeSquare;
            Position = homeSquare;
        }

        public abstract int Value { get; }
        public abstract IEnumerable<Func<Board, Square, IEnumerable<Square>>> ScopeFuncs();
        public PieceColor Color { get; set; }
        public Square HomeSquare { get; set; }
        public Square Position { get; set; }
    }
}
