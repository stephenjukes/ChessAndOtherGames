using Chess.InputOutput;
using Chess.Players;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chess.Games
{
    class ChessGame : Game
    {
        private bool checkmate = false;

        public ChessGame(IO io) : base(io)
        {
            players = new Player[]
                {
                    new ComputerPlayer(PieceColor.White),
                    new ComputerPlayer(PieceColor.Black)
                };
        }

        public void Run()
        {
            while (!checkmate)
            {
                var player = players[moves % 2];
                player.View(board);
                var proposedMove = player.Move();


                Console.OutputEncoding = System.Text.Encoding.Unicode;
                this.IO.Render(this.IO.PieceTypes[typeof(Pawn)]);
                moves++;
            }
        }
    }
}
