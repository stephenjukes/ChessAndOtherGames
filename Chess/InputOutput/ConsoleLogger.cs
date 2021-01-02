using System;
using System.Collections.Generic;
using System.Text;

namespace Chess.InputOutput
{
    class ConsoleLogger : IO
    {
        public ConsoleLogger()
        {
            PopulatePieceRenderings();
        }

        private void PopulatePieceRenderings()
        {
            PieceTypes[typeof(Pawn)] = "♙♟";
        }

        public override string Read()
        {
            return Console.ReadLine();
        }

        public override void Render(string data)
        {
            Console.WriteLine(data);
        }
    }
}
