using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridBasedMovment
{
    class Program
    {
        static void Main(string[] args)
        {
            Grid myGrid = new Grid(24, 25);
            int currentPlayer = 0;
            bool gameRunning = true;
            Player playerOne = new Player(1, "Haygarth", 4, 8, "no");
            Player playerTwo = new Player(2, "Sam", 6, 9, "no");
            Player playerThree = new Player(3, "Josh", 2, 2, "no");
            Player[] players = new Player[6];
            players[0] = playerOne;
            players[1] = playerTwo;
            players[2] = playerThree;
            myGrid.initializeGrid(players);
            myGrid.drawGrid();
            while (gameRunning)
            {
                if (players[currentPlayer] != null)
                {
                    if (GameManager.Turn(players[currentPlayer], myGrid))
                        gameRunning = false;
                    myGrid.drawGrid();
                }
                currentPlayer++;
                if (currentPlayer >= 6)
                    currentPlayer = 0;
            }
            Console.ReadKey();
        }
    }
}
