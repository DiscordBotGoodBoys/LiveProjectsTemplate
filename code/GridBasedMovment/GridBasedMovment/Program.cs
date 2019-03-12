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
            Grid myGrid = new Grid(25, 24); // 25 x 24 is the original size, rotated
                                            // rotated so that the y is 24 - due to discord character limits we may
                                            // need to draw a grid using multiple messages. we can only do this by sending
                                            // as rows rather than columns, and 24 has more versatile factors than 25
                                            // which should give us more options when attempting this
                                            // (1, 2, 3, 4, 6, 8, 12, 24 as opposed to 1, 5, 25)
            int currentPlayer = 0;
            bool gameRunning = true;
            Player playerOne = new Player(1, "Tom", 7, 0); //one bug I didnt get around to fixing: two players can't have
            Player playerTwo = new Player(2, "Max", 24, 9);//the same name. I know why this happens and it's pretty much
            Player playerThree = new Player(3, "Brian", 24, 14);//better in every way to fix by preventing people from sharing names
            Player[] players = new Player[6];
            players[0] = playerOne;
            players[1] = playerTwo;
            players[2] = playerThree;
            myGrid.initializeGrid(players);
            myGrid.AssignStandardWalls();
            MurderScenario murderScenario = new MurderScenario(players);
            //the above line of code does all the card stuff haygarth implemented, i.e. setting
            //up the murder and dealing out cards to players

            //DEBUG BLOCK (displays the three murder cards, and then each player's hand
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
            //DEBUG BLOCK END


            myGrid.drawGrid();
            while (gameRunning)
            {
                string winner = "";
                switch (GameManager.GameStatus(players)) //checks if game has ended
                {
                    case 1: //if ended due to a player winning
                        for (int i = 0; i < players.Length; i++)
                            if (players[i] != null)
                                if (players[i].gameStatus == 1) //find the winner
                                    winner = players[i].name;
                        Console.WriteLine("GAME WON BY " + winner + " VIA ACCUSATION");
                        gameRunning = false;
                        break;
                    case -1: //if ended due to all but one players losing
                        for (int i = 0; i < players.Length; i++)
                            if (players[i] != null)
                                if (players[i].gameStatus != -1) //the winner is the one that isn't a loser
                                    winner = players[i].name; 
                        Console.WriteLine("GAME WON BY " + winner + " BY LAST MAN STANDING");
                        gameRunning = false;
                        break;
                    default: //if the game hasn't ended, do the following
                        if (players[currentPlayer] != null && players[currentPlayer].gameStatus != -1)
                            // the != null check comes into play if there are less than 6 players
                            // gameStatus != -1 checks if the player hasn't lost and can no longer have turns
                        {
                            if (GameManager.Turn(players[currentPlayer], players, myGrid, murderScenario))
                                //runs the current players turn. if they win, return true. if they don't, return false
                            {
                                gameRunning = false;
                            }
                            else
                            {
                                myGrid.drawGrid();
                            }
                        }
                        currentPlayer++; //cycle to the next player
                        if (currentPlayer >= 6) //if you go out of the array bounds
                            currentPlayer = 0; //the next player cycles back around to 0
                        break;
                }
            }
            Console.ReadKey();
        }
    }
}
