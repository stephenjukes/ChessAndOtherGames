using System;
using System.Collections.Generic;
using System.Text;

namespace Chess
{
    class Bishop : Piece
    {
        public Bishop(PieceColor color, Square position) : base(color, position)
        {
        }

        public override int Value { get; } = 3;

        public override IEnumerable<Func<Board, Square, IEnumerable<Square>>> ScopeFuncs()
        {
            return new Func<Board, Square, IEnumerable<Square>>[]
                {
                    Moves.ForwardSlashDiagonal(8),
                    Moves.BackSlashDiagonal(8)
                };
        }
    }
}
