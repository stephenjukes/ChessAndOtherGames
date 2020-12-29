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
         
        // Can a delegate be used here?
        public static Func<Board, Square, IEnumerable<Square>> Forwards(PieceColor pieceColor, int pieceRange) 
            => ResolveScopeFunc(pieceColor, 0)(pieceRange);

        public static Func<Board, Square, IEnumerable<Square>> ForwardsRight(PieceColor pieceColor, int pieceRange) 
            => ResolveScopeFunc(pieceColor, 45)(pieceRange);

        public static Func<Board, Square, IEnumerable<Square>> Right(PieceColor pieceColor, int pieceRange) 
            => ResolveScopeFunc(pieceColor, 90)(pieceRange);

        public static Func<Board, Square, IEnumerable<Square>> BackwardsRight(PieceColor pieceColor, int pieceRange) 
            => ResolveScopeFunc(pieceColor, 135)(pieceRange);

        public static Func<Board, Square, IEnumerable<Square>> Backwards(PieceColor pieceColor, int pieceRange) 
            => ResolveScopeFunc(pieceColor, 180)(pieceRange);

        public static Func<Board, Square, IEnumerable<Square>> BackwardsLeft(PieceColor pieceColor, int pieceRange) 
            => ResolveScopeFunc(pieceColor, 225)(pieceRange);

        public static Func<Board, Square, IEnumerable<Square>> Left(PieceColor pieceColor, int pieceRange) 
            => ResolveScopeFunc(pieceColor, 270)(pieceRange);

        public static Func<Board, Square, IEnumerable<Square>> ForwardsLeft(PieceColor pieceColor, int pieceRange) 
            => ResolveScopeFunc(pieceColor, 315)(pieceRange);

        private static Func<int, Func<Board, Square, IEnumerable<Square>>> ResolveScopeFunc(PieceColor pieceColor, int degreeRotation)
        {
            var colorOrientation = colorOrientations[pieceColor];
            var compassOrientation = compassPoints.FirstOrDefault(p => p.Direction == colorOrientation);
            var moveOrientation = compassPoints.FirstOrDefault(p => p.Bearing == ((compassOrientation.Bearing + degreeRotation) % 360));

            return moveOrientation.ScopeFunc;
        }

        public static Func<Board, Square, IEnumerable<Square>> North (int pieceRange)
        {
            return (Board board, Square position) =>
            {
                var potentialScope = board.Squares
                    .Where(s =>
                        s.Column == position.Column &&
                        s.Row > position.Row &&
                        Math.Abs(s.Row - position.Row) <= pieceRange)
                    .OrderBy(s => s.Row);

                var limit = potentialScope.FirstOrDefault(s => s.OccupyingPiece != null) ?? potentialScope.LastOrDefault();
                if (limit == null || limit.OccupyingPiece == null) return potentialScope;

                return potentialScope.Where(s => 
                    limit.OccupyingPiece.Color == position.OccupyingPiece.Color && s.Row < limit.Row ||
                    limit.OccupyingPiece.Color != position.OccupyingPiece.Color && s.Row <= limit.Row);
            };
        }

        public static Func<Board, Square, IEnumerable<Square>> South(int pieceRange)
        {
            return (Board board, Square position) =>
            {
                var potentialScope = board.Squares
                    .Where(s =>
                        s.Column == position.Column &&
                        s.Row < position.Row &&
                        Math.Abs(s.Row - position.Row) <= pieceRange)
                    .OrderBy(s => s.Row);

                var limit = potentialScope.LastOrDefault(s => s.OccupyingPiece != null) ?? potentialScope.LastOrDefault();
                if (limit == null || limit.OccupyingPiece == null) return potentialScope;

                return potentialScope.Where(s => 
                    limit.OccupyingPiece.Color == position.OccupyingPiece.Color && s.Row > limit.Row ||
                    limit.OccupyingPiece.Color != position.OccupyingPiece.Color && s.Row >= limit.Row);
            };
        }

        public static Func<Board, Square, IEnumerable<Square>> East(int pieceRange)
        {
            return (Board board, Square position) =>
            {
                var potentialScope = board.Squares
                            .Where(s =>
                                s.Row == position.Row &&
                                s.Column > position.Column &&
                                Math.Abs(s.Column - position.Column) <= pieceRange)
                            .OrderBy(s => s.Column);

                var limit = potentialScope.FirstOrDefault(s => s.OccupyingPiece != null) ?? potentialScope.LastOrDefault();
                if (limit == null || limit.OccupyingPiece == null) return potentialScope;

                return potentialScope.Where(s => 
                    limit.OccupyingPiece.Color == position.OccupyingPiece.Color && s.Column < limit.Column ||
                    limit.OccupyingPiece.Color != position.OccupyingPiece.Color && s.Column <= limit.Column);
            };
        }

        public static Func<Board, Square, IEnumerable<Square>> West(int pieceRange)
        {
            return (Board board, Square position) =>
            {
                var potentialScope = board.Squares
                            .Where(s =>
                                s.Row == position.Row &&
                                s.Column < position.Column &&
                                Math.Abs(s.Column - position.Column) <= pieceRange)
                            .OrderBy(s => s.Column);

                var limit = potentialScope.LastOrDefault(s => s.OccupyingPiece != null) ?? potentialScope.LastOrDefault();
                if (limit == null || limit.OccupyingPiece == null) return potentialScope;
                
                return potentialScope.Where(s => 
                    limit.OccupyingPiece.Color == position.OccupyingPiece.Color && s.Column > limit.Column ||
                    limit.OccupyingPiece.Color != position.OccupyingPiece.Color && s.Column >= limit.Column);
            };
        }

        public static Func<Board, Square, IEnumerable<Square>> NorthEast(int pieceRange)
        {
            return (Board board, Square position) =>
            {
                var potentialScope = board.Squares
                            .Where(s =>
                                s.Column > position.Column &&
                                (s.Column - s.Row) == (position.Column - position.Row) &&
                                Math.Abs(s.Column - position.Column) <= pieceRange)
                            .OrderBy(s => s.Column);

                var limit = potentialScope.FirstOrDefault(s => s.OccupyingPiece != null) ?? potentialScope.LastOrDefault();
                if (limit == null || limit.OccupyingPiece == null) return potentialScope;

                return potentialScope.Where(s => 
                    limit.OccupyingPiece.Color == position.OccupyingPiece.Color && s.Column < limit.Column ||
                    limit.OccupyingPiece.Color != position.OccupyingPiece.Color && s.Column <= limit.Column);
            };
        }

        public static Func<Board, Square, IEnumerable<Square>> SouthWest(int pieceRange)
        {
            return (Board board, Square position) =>
            {
                var potentialScope = board.Squares
                            .Where(s =>
                                s.Column < position.Column &&
                                (s.Column - s.Row) == (position.Column - position.Row) &&
                                Math.Abs(s.Column - position.Column) <= pieceRange)
                            .OrderBy(s => s.Column);

                var limit = potentialScope.LastOrDefault(s => s.OccupyingPiece != null) ?? potentialScope.LastOrDefault();
                if (limit == null || limit.OccupyingPiece == null) return potentialScope;

                return potentialScope.Where(s => 
                    limit.OccupyingPiece.Color == position.OccupyingPiece.Color && s.Column > limit.Column ||
                    limit.OccupyingPiece.Color != position.OccupyingPiece.Color && s.Column >= limit.Column);
            };
        }
            
        public static Func<Board, Square, IEnumerable<Square>> SouthEast(int pieceRange)
        {
            return (Board board, Square position) =>
            {
                var potentialScope = board.Squares
                            .Where(s =>
                                s.Column > position.Column &&
                                (s.Column + s.Row) == (position.Column + position.Row) &&
                                Math.Abs(s.Column - position.Column) <= pieceRange)
                            .OrderBy(s => s.Column);

                var limit = potentialScope.FirstOrDefault(s => s.OccupyingPiece != null) ?? potentialScope.LastOrDefault();
                if (limit == null || limit.OccupyingPiece == null) return potentialScope;

                return potentialScope.Where(s => 
                    limit.OccupyingPiece.Color == position.OccupyingPiece.Color && s.Column < limit.Column ||
                    limit.OccupyingPiece.Color != position.OccupyingPiece.Color && s.Column <= limit.Column);
            };
        }

        public static Func<Board, Square, IEnumerable<Square>> NorthWest(int pieceRange)
        {
            return (Board board, Square position) =>
            {
                var potentialScope = board.Squares
                            .Where(s =>
                                s.Column < position.Column &&
                                (s.Column + s.Row) == (position.Column + position.Row) &&
                                Math.Abs(s.Column - position.Column) <= pieceRange)
                            .OrderBy(s => s.Column);

                var limit = potentialScope.LastOrDefault(s => s.OccupyingPiece != null) ?? potentialScope.LastOrDefault();
                if (limit == null || limit.OccupyingPiece == null) return potentialScope;

                return potentialScope.Where(s => 
                    limit.OccupyingPiece.Color == position.OccupyingPiece.Color && s.Column > limit.Column ||
                    limit.OccupyingPiece.Color != position.OccupyingPiece.Color && s.Column >= limit.Column);
            };
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

        public static IEnumerable<Square> ResolveScope(Board board, Square position, IEnumerable<Func<Board, Square, IEnumerable<Square>>> scopeFuncs)
            => scopeFuncs.Aggregate(new Square[0], (scope, scopeFunc) => scope.Concat(scopeFunc(board, position)).ToArray());
    }
}
