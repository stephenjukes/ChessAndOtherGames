using System;
using System.Collections.Generic;
using System.Text;

namespace Chess
{
    class Knight : Piece
    {
        public Knight(PieceColor color, Square position) : base(color, position)
        {
        }

        public override int Value { get; } = 3;

        public override IEnumerable<Func<Board, Square, IEnumerable<Square>>> ScopeFuncs() 
            => new Func<Board, Square, IEnumerable<Square>>[] { Moves.Knight(3) };    // This argument is handled in an interesting way
    }
}
