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
        }

        public abstract int Value { get; }
        public abstract IEnumerable<Func<Board, Square, IEnumerable<Square>>> ScopeFuncs();
        public PieceColor Color { get; set; }
        //public Square HomeSquare { get; set; }  // Consider removing now we have a MoveHistory property
        public List<Square> MoveHistory { get; set; } = new List<Square>();
    }
}
