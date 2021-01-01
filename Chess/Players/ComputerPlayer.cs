using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chess.Players
{
    class ComputerPlayer : Player
    {
        private Board _board;

        public ComputerPlayer(PieceColor color) : base(color)
        {
        }

        public override void  View(Board board)
        {
            _board = board;
        }

        public override Move Move()
        {
            var viableSquares = _board.Squares
                .Where(s =>
                    s.OccupyingPiece != null &&
                    s.OccupyingPiece.Color == this.Color
                    && Moves.ResolveScope(_board, s, s.OccupyingPiece.ScopeFuncs()).Any());

            var square = viableSquares.ToArray().Last();
            var scope = Moves.ResolveScope(_board, square, square.OccupyingPiece.ScopeFuncs());

            return new Move
            {
                Origin = square,
                Destination = scope.Last(),
                Piece = square.OccupyingPiece
            };
        }
    }
}
