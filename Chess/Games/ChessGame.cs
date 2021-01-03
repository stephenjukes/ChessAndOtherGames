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

                var move = player.Move();
                move.Number = moves;

                // TODO: Check move for validity
                var piece = move.Origin.OccupyingPiece;
                piece.MoveHistory.Add(move.Destination);
                move.Destination.OccupyingPiece = piece;
                move.Origin.OccupyingPiece = null;


                this.IO.Render(board);
                this.IO.Render(move);

                moves++;
            }
        }
    }
}
