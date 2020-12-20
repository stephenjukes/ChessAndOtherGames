using System;
using System.Collections.Generic;
using System.Text;

namespace Chess
{
    class King : Piece
    {
        public King(PieceColor color, Square position) : base(color, position)
        {
        }

        public override int Value { get; } = 100;

        public override IEnumerable<Func<Board, Square, IEnumerable<Square>>> ScopeFuncs()
        {
            return new Func<Board, Square, IEnumerable<Square>>[]
                {
                    Moves.Horizontal(1),
                    Moves.Vertical(1),
                    Moves.ForwardSlashDiagonal(1),
                    Moves.BackSlashDiagonal(1)
                };
        }
    }
}
