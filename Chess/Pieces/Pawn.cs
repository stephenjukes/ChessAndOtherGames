using System;
using System.Collections.Generic;
using System.Text;

namespace Chess
{
    class Pawn : Piece
    {
        public Pawn(PieceColor color, Square position) : base(color, position)
        {
        }

        public override int Value { get; } = 1;

        // handle diagonal capture and en passant
        public override IEnumerable<Func<Board, Square, IEnumerable<Square>>> ScopeFuncs()
            => new Func<Board, Square, IEnumerable<Square>>[] { Moves.Forwards(this.Color, 1) };
    }
}
