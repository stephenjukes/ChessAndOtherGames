using System;
using System.Collections.Generic;
using System.Text;

namespace Chess.Players
{
    abstract class Player
    {
        public Player(PieceColor color)
        {
            Color = color;
        }

        public PieceColor Color { get; }
        public abstract void View(Board board);
        public abstract Move Move();
    }
}
