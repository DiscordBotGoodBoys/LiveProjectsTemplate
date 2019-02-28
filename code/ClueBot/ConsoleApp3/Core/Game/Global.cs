using System;
using System.Collections.Generic;
using System.Text;

namespace ClueBot.Core.Commands
{
    public static class Global
    {
        //Game States
        public const int SETUP = 0;
        public const int START = 1;
        public const int SUGGEST1 = 2;
        public const int ROLL = 3;
        public const int MOVE = 4;
        public const int SUGGEST2 = 5;
        public const int END = 6;

        public static bool gamePlaying = false;
        public static int playerTurn = 0;
    }
}
