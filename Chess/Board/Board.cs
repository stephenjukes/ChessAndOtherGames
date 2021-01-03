using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chess
{
    class Board
    {
        public Board()
        {
            ArrangeSquares();
            ArrangePieces();
        }

        public List<Square> Squares { get; } = new List<Square>();

        private void ArrangeSquares()
        {
            for (int row = 1; row <= 8; row++)
            {
                foreach (var column in Enum.GetValues(typeof(SquareColumn)).Cast<SquareColumn>().ToList())
                {
                    Squares.Add(new Square(row, column));
                }
            }
        }

        private void ArrangePieces()
        {
            // WhitePieces
            ArrangePiece((color, square) => new Pawn(color, square), PieceColor.White, Squares.Where(s => s.Row == 2));
            ArrangePiece((color, square) => new Rook(color, square), PieceColor.White, Squares.Where(s => s.Row == 1 && (s.Column == SquareColumn.A || s.Column == SquareColumn.H)));
            ArrangePiece((color, square) => new Knight(color, square), PieceColor.White, Squares.Where(s => s.Row == 1 && (s.Column == SquareColumn.B || s.Column == SquareColumn.G)));
            ArrangePiece((color, square) => new Bishop(color, square), PieceColor.White, Squares.Where(s => s.Row == 1 && (s.Column == SquareColumn.C || s.Column == SquareColumn.F)));
            ArrangePiece((color, square) => new Queen(color, square), PieceColor.White, Squares.Where(s => s.Row == 1 && s.Column == SquareColumn.D));
            ArrangePiece((color, square) => new King(color, square), PieceColor.White, Squares.Where(s => s.Row == 1 && s.Column == SquareColumn.E));

            // BlackPieces
            ArrangePiece((color, square) => new Pawn(color, square), PieceColor.Black, Squares.Where(s => s.Row == 7));
            ArrangePiece((color, square) => new Rook(color, square), PieceColor.Black, Squares.Where(s => s.Row == 8 && (s.Column == SquareColumn.A || s.Column == SquareColumn.H)));
            ArrangePiece((color, square) => new Knight(color, square), PieceColor.Black, Squares.Where(s => s.Row == 8 && (s.Column == SquareColumn.B || s.Column == SquareColumn.G)));
            ArrangePiece((color, square) => new Bishop(color, square), PieceColor.Black, Squares.Where(s => s.Row == 8 && (s.Column == SquareColumn.C || s.Column == SquareColumn.F)));
            ArrangePiece((color, square) => new Queen(color, square), PieceColor.Black, Squares.Where(s => s.Row == 8 && s.Column == SquareColumn.D));
            ArrangePiece((color, square) => new King(color, square), PieceColor.Black, Squares.Where(s => s.Row == 8 && s.Column == SquareColumn.E));
        }

        private void ArrangePiece(Func<PieceColor, Square, Piece> pieceCreator, PieceColor color, IEnumerable<Square> positions)
        {
            foreach (var square in positions)
            {
                var piece = pieceCreator(color, square);
                square.OccupyingPiece = piece;
            }
        }
    }
}
