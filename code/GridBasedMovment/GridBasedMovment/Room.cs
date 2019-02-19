using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridBasedMovment
{
    class Room
    {
        string name;
        Player[] players = new Player[6];
        int[,] doorPoints;
    }
}
