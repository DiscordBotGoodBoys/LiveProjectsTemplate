using Discord.Commands;
using System;
using System.Threading;
using System.Threading.Tasks;


namespace ClueBot.Core.Commands
{
    public class Game : ModuleBase<SocketCommandContext>
    {
        public static bool gameHosting = false;
        public static bool gameStart = false;
        public static bool gamePlaying = false;

        public static int playerTurn = 0;
        public static int currentPlayers = 0;
        public static string gameState = "Null";

        public static Player[] player = new Player[5];

        [Command("Start"), Alias("StartGame"), Summary("Starts the game.")]
        public async Task StartGame()
        {
            //Checks if there are enough players in the game.
            //Note that this will break if player 2 is removed and nobody replaces them.
            if (PlayerExists(1))
            {
                gameStart = true;
                gameState = "Starting";
            }

            else
            {
                await Context.Channel.SendMessageAsync("There are not enough players to start the game.");
                return;
            }

            Grid grid = new Grid(24, 25);
            

            if (gameStart)
            {
                currentPlayers = 0;
                foreach(Player player in player)
                {
                    currentPlayers++;
                }

                await Context.Channel.SendMessageAsync("Shuffling cards...");
                await Context.Channel.SendMessageAsync("Setting case cards...");

                //Murder Population
                Scenario.murderList.Add(Scenario.weaponList[Scenario.rngWeapon]);
                Scenario.murderList.Add(Scenario.roomList[Scenario.rngRoom]);
                Scenario.murderList.Add(Scenario.personList[Scenario.rngPerson]);

                //Remove from list
                Scenario.weaponList.RemoveAt(Scenario.rngWeapon);
                Scenario.roomList.RemoveAt(Scenario.rngRoom);
                Scenario.personList.RemoveAt(Scenario.rngPerson);

                //Pool Remaining Cards
                foreach (var el in Scenario.weaponList)
                    Scenario.cardsList.Add(el);

                foreach (var el in Scenario.roomList)
                    Scenario.cardsList.Add(el);

                foreach (var el in Scenario.personList)
                    Scenario.cardsList.Add(el);

                //Shuffle Cards
                Scenario.cardsList.Shuffle();

                //Deal Cards 
                int cards = Scenario.cardsList.Count;
                
                int i = 0;
                foreach (var el in Scenario.cardsList)
                {
                    int ID = i % Scenario.players;
                    Scenario.playerID[i] = ID;
                    i++;
                }

                await Context.Channel.SendMessageAsync("Randomising player and weapon positions...");
                //Randomise weapon and player positions.

                await Context.Channel.SendMessageAsync("The game has begun!");

                gamePlaying = true;
                gameStart = false;
                playerTurn = 1;
                gameState = "Roll";
            }

            while (gamePlaying)
            {

                //if (gameState == "Roll")  //Put roll logic in here if necessary.
                //{

                //}
                await Context.Channel.SendMessageAsync("Player " + playerTurn + "'s turn. ?Roll the dice!");
                SpinWait.SpinUntil(() => GameCommands.roll > 0);
                gameState = "Moving";

                Turn(player[playerTurn], grid);


                GameCommands.roll = 0;
                playerTurn++;
                if (playerTurn > currentPlayers)
                    playerTurn = 1;
            }
                
            
        }

        // = tbd
        /// = done

        ///1)    Determine the order in which players play. Assign numbers (instances of classes) to players. 
        ///      Set case cards. Deal cards. 

        //2)    Randomise weapon and player positions. 

        ///3)    Game loop begins!

        //4)    Players start their turn by rolling.

        //4b)   Player rolls. Player uses MoveTowards to move towards a building.

        //4c)   If the player reaches a building, they suggest a case.


        //Checks if player playerNumber has been added to the game.
        public bool PlayerExists(int whichPlayer)
        {
            if (player[whichPlayer] != null)
                return true;
            return false;

        }


        public static bool Turn(Player player, Grid grid)
        {
            bool movementValid = false;
            while (!movementValid)
            {
                Console.Write("\nEnter an x coordinate: ");
                int x = Convert.ToInt32(Console.ReadLine());
                Console.Write("\nEnter a y coordinate: ");
                int y = Convert.ToInt32(Console.ReadLine());
                movementValid = player.movePlayer(grid, x, y, GameCommands.roll);
            }
            return false;
        }

    }
}
