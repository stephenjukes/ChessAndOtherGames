using Chess.Players;
using System;

namespace Chess
{
    class Program
    {
        // TODO: 
            // give pieces appearance
            // display board
            // dry up Moves static class
            // handle pawn capture and en passant

        static void Main(string[] args)
        {
            var checkmate = false;
            var moves = 0;
            var board = new Board();
            var players = new Player[]
            {
                new ComputerPlayer(PieceColor.White),
                new ComputerPlayer(PieceColor.Black)
            };

            while(!checkmate)
            {
                var player = players[moves % 2];
                player.View(board);
                var proposedMove = player.Move();

                moves++;
            }
        }

        static Player GetPlayerType(int player)
        {
            // inject I.O. type
            Console.WriteLine($"Player {player}: Human (1) or computer (2)?");
            var playerInput = Console.ReadLine();

            var isValidPlayerType = int.TryParse(playerInput, out int playerType);
            if (!isValidPlayerType) return null;

            switch(playerType)
            {
                case 1:
                    return new HumanPlayer(PieceColor.Black);   // TODO: Handle correctly
                case 2:
                    return new ComputerPlayer(PieceColor.White);
                default:
                    return null;
            }
        }
    }
}
