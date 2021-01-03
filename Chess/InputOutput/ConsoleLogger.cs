using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chess.InputOutput
{
    class ConsoleLogger : IO
    {
        private ConsoleColor BackgroundColor = ConsoleColor.DarkGray;
        private ConsoleColor EmptySquareColor = ConsoleColor.Gray;

        public ConsoleLogger()
        {
            PopulatePieceRenderings();
            Console.BackgroundColor = BackgroundColor;
        }

        private void PopulatePieceRenderings()
        {
            PieceTypes[typeof(Pawn)] = "p";
            PieceTypes[typeof(Rook)] = "R";
            PieceTypes[typeof(Knight)] = "N";
            PieceTypes[typeof(Bishop)] = "B";
            PieceTypes[typeof(Queen)] = "Q";
            PieceTypes[typeof(King)] = "K";
        }

        public override string Read()
        {
            return Console.ReadLine();
        }

        public override void Render(string data)
        {
            Console.WriteLine(data);
        }

        public override void Render(Board board)
        {
            var lines = board.Squares
                .OrderByDescending(s => s.Row)
                .ThenBy(s => s.Column)
                .GroupBy(s => s.Row);

            var indentation = Spaces(4);
            var spacer = Spaces(1);

            foreach (var group in lines)
            {
                Console.ForegroundColor = EmptySquareColor;
                Console.Write($"\n{group.Key}{indentation}");

                foreach (var square in group)
                {
                    var piece = square.OccupyingPiece;

                    if (piece == null)
                    {
                        Console.ForegroundColor = EmptySquareColor;
                        Console.Write("." + spacer);

                        continue;
                    }
                    else if (piece.Color == PieceColor.White)
                    {
                        Console.ForegroundColor = ConsoleColor.White;     
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                    }

                    Console.Write(PieceTypes[piece.GetType()] + spacer);
                }   
            }

            Console.WriteLine("\n");
            Console.ForegroundColor = EmptySquareColor;
            Console.WriteLine(indentation + Spaces(1) + string.Join(spacer, "ABCDEFGH".ToCharArray()));
        }

        public override void Render(Move move)
        {
            var origin = move.Origin;
            var destination = move.Destination;

            Console.WriteLine(
                $"{destination.OccupyingPiece.GetType().Name}: " +
                $"{origin.Column}{origin.Row} - " +
                $"{destination.Column}{destination.Row}");
        }

        private string Spaces(int n) => new string(' ', n);
    }
}
