﻿using System;
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
                    && Moves.ResolveScope(_board, s, s.OccupyingPiece.ScopeFuncs()).Any())
                .Select(s => new { Square = s, Scope = Moves.ResolveScope(_board, s, s.OccupyingPiece.ScopeFuncs()) });

            var firstViableSquare = viableSquares.First();

            return new Move
            {
                Origin = firstViableSquare.Square,
                Destination = firstViableSquare.Scope.First()
            };
        }
    }
}
