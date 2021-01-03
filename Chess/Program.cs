using Chess.Games;
using Chess.InputOutput;
using Chess.Players;
using System;

namespace Chess
{
    class Program
    {
        // TODO: 
            // move validity
            // handle captures (especially for en passant)
            // figure out the best way to validate that en passant is undertaken immediately after a pawn is advanced by 2 squares
            // handle en passant, castling, pawn promotion, check, checkmate

        static void Main(string[] args)
        {
            var io = new ConsoleLogger();
            var game = new ChessGame(io);
            game.Run();
        }
    }
}
