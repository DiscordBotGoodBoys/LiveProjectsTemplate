using Discord.Commands;
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

        public static string gameState = "Null";

        public static Player[] player = new Player[5];

        [Command("Start"), Alias("StartGame"), Summary("Starts the game.")]
        public async Task StartGame()
        {
            gameStart = true;
            gameState = "";
            playerTurn = 1;
            await Context.Channel.SendMessageAsync("Game starting!");

            if (gameStart)
            {
                await Context.Channel.SendMessageAsync("Shuffling cards...");
                await Context.Channel.SendMessageAsync("Setting case cards...");
                //Set case cards, deal cards.
                await Context.Channel.SendMessageAsync("Randomising player and weapon positions...");
                //Randomise weappon and player positions.

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

                
                GameCommands.roll = 0;
            }
                
            
        }

        // = tbd
        /// = done
        
        ///1)    Determine the order in which players play. Assign numbers (instances of classes) to players. 
        //      Set case cards. Deal cards. 

        //2)    Randomise weapon and player positions. 

        //3)    Game loop begins!

        //4)    Players start their turn by rolling.

        //4b)   Player rolls. Player uses MoveTowards to move towards a building.

        //4c)   If the player reaches a building, they suggest a case. During suggestions, 




    }
}
