using System;
using System.Collections.Generic;
using System.Text;

namespace Chess
{
    class Rook : Piece
    {
        public Rook(PieceColor color, Square position) : base(color, position)
        {
        }

        public override int Value { get; } = 5;

        public override IEnumerable<Func<Board, Square, IEnumerable<Square>>> ScopeFuncs()
        {
            return new Func<Board, Square, IEnumerable<Square>>[]
                {
                    Moves.Horizontal(8),
                    Moves.Vertical(8)
                };
        }
    }
}
