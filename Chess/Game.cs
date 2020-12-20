using System;
using System.Collections.Generic;
using System.Text;

namespace Chess
{
    class Game
    {
        private bool checkmate { get; set; } = false;
        public int Move { get; set; } = 0;
        public int players { get; set; }
    }
}
