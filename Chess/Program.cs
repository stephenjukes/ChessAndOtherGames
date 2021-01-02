using Chess.Games;
using Chess.InputOutput;
using Chess.Players;
using System;

namespace Chess
{
    class Program
    {
        // TODO: 
            // give pieces appearance
            // display board
            // handle en passant

        static void Main(string[] args)
        {
            var io = new ConsoleLogger();
            var game = new ChessGame(io);
            game.Run();
        }
    }
}
