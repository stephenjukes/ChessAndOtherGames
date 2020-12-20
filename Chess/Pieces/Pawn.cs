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

        public override IEnumerable<Func<Board, Square, IEnumerable<Square>>> ScopeFuncs()
        {
            return new Func<Board, Square, IEnumerable<Square>>[0];

            //return position.OccupyingPiece.Color == PieceColor.White
            //        ? new Func<Board, Square, IEnumerable<Square>>[] { Moves.North(1) }
            //        : new Func<Board, Square, IEnumerable<Square>>[] { Moves.South(1) };
        }
    }
}
