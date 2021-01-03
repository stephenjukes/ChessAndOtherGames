using System;
using System.Collections.Generic;
using System.Text;

namespace Chess
{
    class Move
    {
        public Square Origin { get; set; }
        public Square Destination { get; set; }
        public int Number { get; set; }
        public string Notation { get; set; }
    }
}
