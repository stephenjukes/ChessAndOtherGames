using Chess.InputOutput;
using Chess.Players;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chess
{
    class Game
    {
        protected readonly IO IO;
        protected int moves = 0;
        protected Board board = new Board();
        protected Player[] players;

        public Game(IO io)
        {
            IO = io;
        }
    }
}
