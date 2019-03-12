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
            Grid myGrid = new Grid(25, 24);
            int currentPlayer = 0;
            bool gameRunning = true;
            Player playerOne = new Player(1, "Tom", 7, 0);
            Player playerTwo = new Player(2, "Max", 24, 9);
            Player playerThree = new Player(3, "Brian", 24, 14);
            Player[] players = new Player[6];
            players[0] = playerOne;
            players[1] = playerTwo;
            players[2] = playerThree;
            myGrid.initializeGrid(players);
            myGrid.AssignStandardWalls();
            MurderScenario murderScenario = new MurderScenario(players);
            foreach (string card in murderScenario.murderList)
                Console.Write(card + ", ");
            Console.WriteLine();
            foreach (Player player in players)
            {
                if (player != null)
                {
                    Console.Write(player.name + "'s cards: ");
                    foreach (string card in player.cards)
                        Console.Write(card + ", ");
                    Console.Write('\n');
                }
            }
            myGrid.roomID[0, 0] = 1;
            myGrid.drawGrid();
            while (gameRunning)
            {
                string winner = "";
                switch (GameManager.GameStatus(players))
                {
                    case 1:
                        for (int i = 0; i < players.Length; i++)
                            if (players[i] != null)
                                if (players[i].gameStatus == 1)
                                    winner = players[i].name;
                        Console.WriteLine("GAME WON BY " + winner + " VIA ACCUSATION");
                        gameRunning = false;
                        break;
                    case -1:
                        for (int i = 0; i < players.Length; i++)
                            if (players[i] != null)
                                if (players[i].gameStatus != -1)
                                    winner = players[i].name;
                        Console.WriteLine("GAME WON BY " + winner + " BY LAST MAN STANDING");
                        gameRunning = false;
                        break;
                    default:
                        if (players[currentPlayer] != null && players[currentPlayer].gameStatus != -1)
                        {
                            if (GameManager.Turn(players[currentPlayer], players, myGrid, murderScenario))
                            {
                                gameRunning = false;

                            }
                            else
                            {
                                myGrid.drawGrid();
                            }
                        }
                        currentPlayer++;
                        if (currentPlayer >= 6)
                            currentPlayer = 0;
                        break;
                }
            }
            Console.ReadKey();
        }
    }
}
