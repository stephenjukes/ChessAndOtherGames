using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Chess.InputOutput
{
    abstract class IO
    {
        public abstract string Read();
        public abstract void Render(string data);
        public abstract void Render(Board board);
        public abstract void Render(Move move);

        // TODO: Check that all pieces have been populated with a rendering string
        // Change to protected when handled correctly in derived class
        public Dictionary<Type, string> PieceTypes = Assembly.GetAssembly(typeof(Piece)).GetTypes()
            .Where(type => type.IsSubclassOf(typeof(Piece)))
            .ToDictionary(x => x, x => x.Name);
    }
}
