using System;
using System.Collections.Generic;
using System.Text;

namespace Chess
{
    class Queen : Piece
    {
        public Queen(PieceColor color, Square position) : base(color, position)
        {
        }

        public override int Value { get; } = 9;

        public override IEnumerable<Func<Board, Square, IEnumerable<Square>>> ScopeFuncs()
        {
            return new Func<Board, Square, IEnumerable<Square>>[]
                {
                    Moves.Horizontal(8),
                    Moves.Vertical(8),
                    Moves.ForwardSlashDiagonal(8),
                    Moves.BackSlashDiagonal(8)
                };
        }

    }
}
