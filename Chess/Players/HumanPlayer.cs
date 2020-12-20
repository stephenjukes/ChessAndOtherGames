using System;
using System.Collections.Generic;
using System.Text;

namespace Chess.Players
{
    class HumanPlayer : Player
    {
        public HumanPlayer(PieceColor color) : base(color)
        {
        }

        public override void View(Board board)
        {
            Console.WriteLine(board);
        }

        public override Move Move()
        {
            return new Move();
        }
    }
}
