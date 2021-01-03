using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chess
{
    static class Moves
    {
        private static Dictionary<PieceColor, Direction> colorOrientations = new Dictionary<PieceColor, Direction>()
        {
            { PieceColor.White, Direction.North },
            { PieceColor.Black, Direction.South }
        };

        private static CompassPoint[] compassPoints = new CompassPoint[]
        {
            new CompassPoint(Direction.North, 0, North),
            new CompassPoint(Direction.NorthEast, 45, NorthEast),
            new CompassPoint(Direction.East, 90, East),
            new CompassPoint(Direction.SouthEast, 135, SouthEast),
            new CompassPoint(Direction.South, 180, South),
            new CompassPoint(Direction.SouthWest, 225, SouthWest),
            new CompassPoint(Direction.West, 270, West),
            new CompassPoint(Direction.NorthWest, 315, NorthWest),
        };

        // RELATIVE FUNCS
        // Can a delegate be used here?
        public static Func<Board, Square, IEnumerable<Square>> Forwards(PieceColor pieceColor, int pieceRange)
            => ResolveRelativeScopeFunc(pieceColor, 0)(pieceRange);

        public static Func<Board, Square, IEnumerable<Square>> ForwardsRight(PieceColor pieceColor, int pieceRange)
            => ResolveRelativeScopeFunc(pieceColor, 45)(pieceRange);

        public static Func<Board, Square, IEnumerable<Square>> Right(PieceColor pieceColor, int pieceRange)
            => ResolveRelativeScopeFunc(pieceColor, 90)(pieceRange);

        public static Func<Board, Square, IEnumerable<Square>> BackwardsRight(PieceColor pieceColor, int pieceRange)
            => ResolveRelativeScopeFunc(pieceColor, 135)(pieceRange);

        public static Func<Board, Square, IEnumerable<Square>> Backwards(PieceColor pieceColor, int pieceRange)
            => ResolveRelativeScopeFunc(pieceColor, 180)(pieceRange);

        public static Func<Board, Square, IEnumerable<Square>> BackwardsLeft(PieceColor pieceColor, int pieceRange)
            => ResolveRelativeScopeFunc(pieceColor, 225)(pieceRange);

        public static Func<Board, Square, IEnumerable<Square>> Left(PieceColor pieceColor, int pieceRange)
            => ResolveRelativeScopeFunc(pieceColor, 270)(pieceRange);

        public static Func<Board, Square, IEnumerable<Square>> ForwardsLeft(PieceColor pieceColor, int pieceRange)
            => ResolveRelativeScopeFunc(pieceColor, 315)(pieceRange);

        private static Func<int, Func<Board, Square, IEnumerable<Square>>> ResolveRelativeScopeFunc(PieceColor pieceColor, int degreeRotation)
        {
            var colorOrientation = colorOrientations[pieceColor];
            var compassOrientation = compassPoints.FirstOrDefault(p => p.Direction == colorOrientation);
            var moveOrientation = compassPoints.FirstOrDefault(p => p.Bearing == ((compassOrientation.Bearing + degreeRotation) % 360));

            return moveOrientation.ScopeFunc;
        }

        // ABSOLUTE FUNCS
        public static Func<Board, Square, IEnumerable<Square>> North(int pieceRange) => (Board board, Square position)
           => ResolveScope(pieceRange, board, position, position.OccupyingPiece.Color, position => s => s.Column == position.Column && s.Row == position.Row + 1);

        public static Func<Board, Square, IEnumerable<Square>> South(int pieceRange) => (Board board, Square position)
            =>  ResolveScope(pieceRange, board, position, position.OccupyingPiece.Color, position => s => s.Column == position.Column && s.Row == position.Row - 1);

        public static Func<Board, Square, IEnumerable<Square>> East(int pieceRange) => (Board board, Square position)
            => ResolveScope(pieceRange, board, position, position.OccupyingPiece.Color, position => s => s.Row == position.Row && s.Column == position.Column + 1);

        public static Func<Board, Square, IEnumerable<Square>> West(int pieceRange) => (Board board, Square position)
            => ResolveScope(pieceRange, board, position, position.OccupyingPiece.Color, position => s => s.Row == position.Row && s.Column == position.Column + 1);

        public static Func<Board, Square, IEnumerable<Square>> NorthEast(int pieceRange) => (Board board, Square position)
             => ResolveScope(pieceRange, board, position, position.OccupyingPiece.Color, position => s => s.Column == position.Column + 1 && s.Row == position.Row + 1);

        public static Func<Board, Square, IEnumerable<Square>> SouthEast(int pieceRange) => (Board board, Square position)
            => ResolveScope(pieceRange, board, position, position.OccupyingPiece.Color, position => s => s.Column == position.Column + 1 && s.Row == position.Row - 1);

        public static Func<Board, Square, IEnumerable<Square>> SouthWest(int pieceRange) => (Board board, Square position)
            => ResolveScope(pieceRange, board, position, position.OccupyingPiece.Color, position => s => s.Row == position.Row - 1 && s.Column == position.Column - 1);

        public static Func<Board, Square, IEnumerable<Square>> NorthWest(int pieceRange) => (Board board, Square position)
            => ResolveScope(pieceRange, board, position, position.OccupyingPiece.Color, position => s => s.Row == position.Row + 1 && s.Column == position.Column - 1);

        private static IEnumerable<Square> ResolveScope(int pieceRange, Board board, Square position, PieceColor pieceColor, Func<Square, Func<Square, bool>> nextSquareFunc)
        {
            var nextSquare = board.Squares.FirstOrDefault(nextSquareFunc(position));
            IEnumerable<Square> addToScope;

            if (nextSquare == null || pieceRange == 0) addToScope = new Square[0];
            else if (nextSquare.OccupyingPiece == null) addToScope = new Square[] { nextSquare}.Concat(ResolveScope(pieceRange - 1, board, nextSquare, pieceColor, nextSquareFunc));
            else if (nextSquare.OccupyingPiece.Color == pieceColor) addToScope = new Square[0];
            else addToScope = new Square[] { nextSquare };

            return addToScope;
        }

        public static Func<Board, Square, IEnumerable<Square>> Vertical(int pieceRange)
        {
            return (Board board, Square position) =>
            {
                var northScope = North(pieceRange)(board, position);
                var southScope = South(pieceRange)(board, position);
                return northScope.Concat(southScope);
            };
        }

        public static Func<Board, Square, IEnumerable<Square>> Horizontal (int pieceRange)
        {
            return (Board board, Square position) =>
            {
                var eastScope = East(pieceRange)(board, position);
                var westScope = West(pieceRange)(board, position);
                return eastScope.Concat(westScope);
            };
        }

        public static Func<Board, Square, IEnumerable<Square>> ForwardSlashDiagonal(int pieceRange)
        {
            return (Board board, Square position) =>
            {
                var northeastScope = NorthEast(pieceRange)(board, position);
                var southwestScope = SouthWest(pieceRange)(board, position);
                return northeastScope.Concat(southwestScope);
            };
        }

        public static Func<Board, Square, IEnumerable<Square>> BackSlashDiagonal (int pieceRange)
        {
            return (Board board, Square position) =>
            {
                var northwestScope = NorthWest(pieceRange)(board, position);
                var southeastScope = SouthEast(pieceRange)(board, position);
                return northwestScope.Concat(southeastScope);
            };
        }

        public static Func<Board, Square, IEnumerable<Square>> Knight(int pieceRange)
        {
            return (Board board, Square position) =>
                board.Squares.Where(s =>
                {
                    var horizontalTravel = Math.Abs(s.Column - position.Column);
                    var verticalTravel = Math.Abs(s.Row - position.Row);

                    return horizontalTravel + verticalTravel == pieceRange &&
                        horizontalTravel > 0 && verticalTravel > 0 &&
                        s.OccupyingPiece?.Color != position.OccupyingPiece.Color;
                });
        }

        public static Func<Board, Square, IEnumerable<Square>> Pawn(int pieceRange)
        {
            return (Board board, Square position) =>
            {
                var piece = position.OccupyingPiece;
                var advance = Forwards(piece.Color, !piece.MoveHistory.Any() ? 2 : pieceRange)(board, position).Where(s => s.OccupyingPiece == null);
                var leftCapture = ForwardsLeft(piece.Color, pieceRange)(board, position).Where(s => s.OccupyingPiece != null);
                var rightCapture = ForwardsRight(piece.Color, pieceRange)(board, position).Where(s => s.OccupyingPiece != null);

                return advance
                    .Concat(leftCapture)
                    .Concat(rightCapture)
                    .Concat(EnPassant(board, position));
            };
        }

        private static IEnumerable<Square> EnPassant(Board board, Square position)
        {
            var piece = position.OccupyingPiece;

            return board.Squares
                .Where(s =>
                    piece.GetType() == typeof(Pawn) &&
                    s.Row == position.Row &&
                    Math.Abs(s.Column - position.Column) == 1 &&
                    s.OccupyingPiece.GetType() == typeof(Pawn) &&
                    s.OccupyingPiece.Color != piece.Color)
                .SelectMany(s =>
                    Backwards(s.OccupyingPiece.Color, 1)(board, s)
                );
        }

        public static IEnumerable<Square> ResolveScope(Board board, Square position, IEnumerable<Func<Board, Square, IEnumerable<Square>>> scopeFuncs)
            => scopeFuncs.Aggregate(new Square[0], (scope, scopeFunc) => scope.Concat(scopeFunc(board, position)).ToArray());
    }
}
