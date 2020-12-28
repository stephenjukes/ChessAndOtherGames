using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chess
{
    static class Moves
    {
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

                var limit = potentialScope.FirstOrDefault(s => s.OccupyingPiece != null);
                
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

                var limit = potentialScope.LastOrDefault(s => s.OccupyingPiece != null);
                
                return potentialScope.Where(s => 
                    s.OccupyingPiece.Color == position.OccupyingPiece.Color && s.Row > limit.Row ||
                    s.OccupyingPiece.Color != position.OccupyingPiece.Color && s.Row >= limit.Row);
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

                var limit = potentialScope.FirstOrDefault(s => s.OccupyingPiece != null);
                
                return potentialScope.Where(s => 
                    s.OccupyingPiece.Color == position.OccupyingPiece.Color && s.Column < limit.Column ||
                    s.OccupyingPiece.Color != position.OccupyingPiece.Color && s.Column <= limit.Column);
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

                var limit = potentialScope.LastOrDefault(s => s.OccupyingPiece != null);
                
                return potentialScope.Where(s => 
                    s.OccupyingPiece.Color == position.OccupyingPiece.Color && s.Column > limit.Column ||
                    s.OccupyingPiece.Color != position.OccupyingPiece.Color && s.Column >= limit.Column);
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

                var limit = potentialScope.FirstOrDefault(s => s.OccupyingPiece != null);
                
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

                var limit = potentialScope.LastOrDefault(s => s.OccupyingPiece != null);
                
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

                var limit = potentialScope.FirstOrDefault(s => s.OccupyingPiece != null);
                
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

                var limit = potentialScope.LastOrDefault(s => s.OccupyingPiece != null);
                
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
