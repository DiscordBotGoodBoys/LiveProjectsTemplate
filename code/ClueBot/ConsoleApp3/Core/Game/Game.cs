using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;


namespace ClueBot.Core.Commands
{
    public class Game : ModuleBase<SocketCommandContext>
    {
        public static string listOfEverything = "";
        public string gridBuffer = "No grid is active.";

        //game states
        public static bool gameHosting = false;
        public static bool gamePlaying = false;

        public static int playerTurn = 0;
        public static int currentPlayers = 0;
        public static int roll = 0;
        public static string userCoords = "";

        public static string suggestedWeapon = "";
        public static string suggestedUser = "";
        public static string suggestedRoom = "";
        public static bool suggestionInProgress = false;
        public static bool accusationInProgress = false;


        public static string gameState = "Null";

        public static Player[] player = new Player[5];

        public Grid grid = new Grid(25, 24, player);
        

        [Command("Start"), Alias("StartGame"), Summary("Starts the game.")]
        public async Task StartGame()
        {
            //Checks if there are enough players in the game.
            //Note that this will break if player 2 is removed and nobody replaces them.
            /*if (!PlayerExists(1))
            {
                await Context.Channel.SendMessageAsync("There are not enough players to start the game.");
                return;
            }
            */

            //grid.initializeGrid(player);
            grid.drawGrid(ref gridBuffer);
            Console.WriteLine(gridBuffer);

            MurderScenario murderScenario = new MurderScenario(player);

            listOfEverything = "";
            SendListToBuffer(ref listOfEverything, murderScenario);

            //DEBUG BLOCK (displays the three murder cards, and then each player's hand
            foreach (string card in murderScenario.murderList)
                Console.Write(card + ", ");
            Console.WriteLine();
            foreach (Player player in player)
            {
                if (player != null)
                {
                    Console.Write("Player " + (player.playerNumber + 1) + "'s cards: ");
                    foreach (string card in player.cards)
                        Console.Write(card + ", ");
                    Console.Write('\n');
                }
            }
            //DEBUG BLOCK END

            currentPlayers = 0;
            foreach(Player player in player)
            { 
                currentPlayers++;   //Currently broken; also counts null
            }

            await Context.Channel.SendMessageAsync("The game has begun!");

            gamePlaying = true;
            playerTurn = 0;
            gameState = "Roll";
            

            while (gamePlaying)
            {
                string winner = "";
                switch (GameStatus(player)) //checks if game has ended
                {
                    case 1: //if ended due to a player winning
                        for (int i = 0; i < player.Length; i++)
                        if (player[i] != null)
                            if (player[i].gameStatus == 1) //find the winner
                                winner = player[i].userID;
                    Console.WriteLine("GAME WON BY " + winner + " VIA ACCUSATION");
                    gamePlaying = false;
                    break;
                    case -1: //if ended due to all but one players losing
                        for (int i = 0; i < player.Length; i++)
                        if (player[i] != null)
                            if (player[i].gameStatus != -1) //the winner is the one that isn't a loser
                                winner = player[i].userID;
                    Console.WriteLine("GAME WON BY " + winner + " BY LAST MAN STANDING");
                    gamePlaying = false;
                    break;
                    default: //if the game hasn't ended, do the following
                    if (player[playerTurn] != null && player[playerTurn].gameStatus != -1)
                    // the != null check comes into play if there are less than 6 players
                    // gameStatus != -1 checks if the player hasn't lost and can no longer have turns
                    {
                        await Turn(player[playerTurn], player, grid, murderScenario);
                        if (gameState == "Won")
                        //runs the current players turn. if they win, return true. if they don't, return false
                        {
                            gamePlaying = false;
                        }
                        else
                        {
                            grid.drawGrid(ref gridBuffer);
                        }
                    }
                    playerTurn++; //cycle to the next player
                    if (playerTurn >= 6) //if you go out of the array bounds
                        playerTurn = 0; //the next player cycles back around to 0
                    break;
                }
            }
        }

        [Command("Grid"), Summary("Displays the grid.")]
        public async Task PrintGrid()
        {
            EmbedBuilder Embed = new EmbedBuilder();
            Embed.WithColor(55, 0, 255);
            Embed.WithTitle("Grid");

            grid.drawGrid(ref gridBuffer);
            Console.WriteLine(gridBuffer);
            

            Embed.WithDescription(gridBuffer);
            await Context.Channel.SendMessageAsync(gridBuffer);
            await Context.Channel.SendMessageAsync("", false, Embed.Build()); //2048 characters allowed
        }

        //Checks if player playerNumber has been added to the game.
        public bool PlayerExists(int whichPlayer)
        {
            if (player[whichPlayer] != null)
                return true;
            return false;

        }

        ////////////////////////////////////////////////////////////////////////////////////////////////

        public async Task Turn(Player player, Player[] players, Grid grid, MurderScenario murderScenario)
        {
            if (grid.roomID[player.x, player.y] > 0)
            {
                await PrintGrid();
                await Context.Channel.SendMessageAsync("Player " + (playerTurn+1) + "'s turn. You are currently in the " + murderScenario.roomList[grid.roomID[player.x, player.y] - 1] + ". " +
                    "Make a ?suggestion, ?accuse someone, or ?roll the dice.");
                gameState = "RollSuggest";
                
                SpinWait.SpinUntil(() => suggestionInProgress || roll > 0 || accusationInProgress);

                if (suggestionInProgress)
                {
                    await Suggest(player, players, grid.roomID[player.x, player.y], murderScenario);
                    return /*false*/;
                }

                if (accusationInProgress)
                {
                    await Accuse(player, murderScenario);
                    return /*false*/;
                }
            }

            if (roll == 0) 
                await Context.Channel.SendMessageAsync("Player " + (playerTurn + 1) + "'s turn. ?Roll the dice!");
            SpinWait.SpinUntil(() => roll > 0);

            gameState = "Move";
            bool movementValid = false;
            int x = 0;
            int y = 0;
            while (!movementValid)
            {
                await Context.Channel.SendMessageAsync("Use ?move [coords] to move. Coordinates take the form of letter then number, i.e. e15 or B4. ");
                x = 0;
                y = 0;
                userCoords = "";
                SpinWait.SpinUntil(() => userCoords != "");
                if (InterpretCoords(userCoords, ref x, ref y))
                {
                    movementValid = player.MovePlayer(grid, x, y, roll);
                }
            }

            if (grid.roomID[x, y] > 0)
            {
                await Context.Channel.SendMessageAsync("You can now ?suggest a case. (?Suggest [@person] [weapon] [location])");
                SpinWait.SpinUntil(() => suggestionInProgress);
                if (suggestionInProgress)
                {
                    await Suggest(player, players, grid.roomID[player.x, player.y], murderScenario);
                    return /*false*/;
                }
            }
            roll = 0;
            return/* false*/;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////

        private async Task Suggest(Player player, Player[] players, int roomID, MurderScenario murderScenario)
        {
            int weaponChoice = 0;
            int personChoice = 0;
            Player nearestPlayerWithCards = CheckPlayersForCards(player.playerNumber, players, murderScenario.personList[personChoice - 1], murderScenario.roomList[roomID - 1], murderScenario.weaponList[weaponChoice - 1]);
            if (nearestPlayerWithCards == null)
            {
                await Context.Channel.SendMessageAsync("The cards you suggested are not held by any of your fellow detectives...");
            }
            else
            {
                await Context.Channel.SendMessageAsync(nearestPlayerWithCards.userID + " holds one or more of the cards you suggested. They will now decide which card to reveal.");
                string cardToShow = ChooseCardToShow(nearestPlayerWithCards, player, murderScenario.personList[personChoice - 1], murderScenario.roomList[roomID - 1], murderScenario.weaponList[weaponChoice - 1]);
                //Context.User.SendMessageAsync(nearestPlayerWithCards.userID + " shows you " + cardToShow + ".");
                //Must be DM'ed to the player.

            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////

        private /*static bool */ async Task Accuse(Player player, MurderScenario murderScenario)
        //currently there's no way to escape an accusation after initiating one
        {
            bool accusationReady = false;
            int weaponChoice = 0;
            int roomChoice = 0;
            int personChoice = 0;
            while (!accusationReady)
            {
                Console.Write("\nChoose a weapon from the weapon list: ");
                foreach (string weapon in murderScenario.weaponList)
                    Console.Write(weapon + ", ");
                Console.WriteLine();
                weaponChoice = Convert.ToInt32(Console.ReadLine());
                Console.Write("\nChoose a room from the room list: ");
                foreach (string room in murderScenario.roomList)
                    Console.Write(room + ", ");
                Console.WriteLine();
                roomChoice = Convert.ToInt32(Console.ReadLine());
                Console.Write("\nChoose a person from the person list: ");
                foreach (string person in murderScenario.personList)
                    Console.Write(person + ", ");
                Console.WriteLine();
                personChoice = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("You accuse " + murderScenario.personList[personChoice - 1] + " of comitting murder in the " + murderScenario.roomList[roomChoice - 1] + " with the " + murderScenario.weaponList[weaponChoice - 1]);
                Console.WriteLine("Are you happy with this accusation? 1 for yes 0 for no");
                int choice = Convert.ToInt32(Console.ReadLine());
                if (choice == 1)
                    accusationReady = true;
            }
            int correctCards = 0;
            foreach (string murderCard in murderScenario.murderList)
            {
                if (murderCard.Equals(murderScenario.personList[personChoice - 1]) || murderCard.Equals(murderScenario.roomList[roomChoice - 1]) || murderCard.Equals(murderScenario.weaponList[weaponChoice - 1]))
                    correctCards++;
            }
            if (correctCards == 3)
            {
                player.gameStatus = 1;
                Console.WriteLine("CONGRATULATIONS! You have cracked the case! " + murderScenario.personList[personChoice - 1] + " goes behind bars and you win the game! Press any key to continue.");
                Console.ReadKey();
                return /*true*/;
            }
            else
            {
                player.gameStatus = -1;
                Console.WriteLine("False accusations never solve anything. You got " + correctCards + " out of 3 cards correct, and are forced to retire from the case. Press any key to continue.");
                Console.ReadKey();
                return /*false*/;
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////
        
        private static string ChooseCardToShow(Player player, Player suggestingPlayer, string personChoice, string roomChoice, string weaponChoice)
        {
            //in the real life board game, this function represents a player who has a suggested card
            //deciding which card to show the suggesting player
            string buffer = "";
            List<string> cards = new List<string>();
            int noOfCards = 0;
            foreach (string card in player.cards)
            {
                if (card.Equals(personChoice) || card.Equals(roomChoice) || card.Equals(weaponChoice))
                {
                    buffer += card + ", ";
                    cards.Add(card);
                    noOfCards++;
                }
            }
            if (cards.Count == 1) //if you only have one of the suggested cards, there is no choice to make
            {
                Console.WriteLine("The only card suggested in your hand was " + buffer + "so you need not choose a card. Press any key to send " + suggestingPlayer.userID + " this card.");
                Console.ReadKey();
                return cards[0];
            }
            else
            {
                Console.Write("You must choose a card to show to " + suggestingPlayer.userID + " out of the following in your hand: ");
                Console.WriteLine(buffer);
                Console.WriteLine("Select using a number between 1 and " + noOfCards + ", corresponding to the cards place in the above list.");
                int choice = Convert.ToInt32(Console.ReadLine());
                return cards[choice - 1];
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////

        private static Player CheckPlayersForCards(int currentPlayer, Player[] players, string personChoice, string roomChoice, string weaponChoice)
        //this function is akin to asking clockwise around the table if anyone has the cards you suggested
        //the first person clockwise to you who has a card you suggested in their hand is returned in this function
        {
            int playerID = currentPlayer + 1; //currentPlayer is the person performing the check
            if (playerID > 6)
                playerID = 1;
            while (playerID != currentPlayer)
            {
                if (playerID > 6)
                    playerID = 1;//a players ID is always equal to it's index in the main player array plus one. or at least, it should be...
                if (players[playerID - 1] != null)
                {
                    if (players[playerID - 1].cards.Contains(personChoice)
                        || players[playerID - 1].cards.Contains(roomChoice)
                        || players[playerID - 1].cards.Contains(weaponChoice))
                    {
                        return players[playerID - 1]; //we don't tell the checkee what cards the checked player has in here, just that they have at least one
                    }
                }
                playerID++;
                if (playerID > 6) //I wrote this check down three times because I'm very paranoid
                    playerID = 1;
            }
            return null;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////

        public static int GameStatus(Player[] players) //runs at the start of every turn to check whether the game is over
        {
            int activePlayersCount = 0;
            for (int i = 0; i < players.Length; i++)
            {
                if (players[i] != null)
                {
                    switch (players[i].gameStatus)
                    {
                        case 1:
                            return 1; //if a players state is 1 they have won
                        case 0:
                            activePlayersCount++; //if a players state is 0 they are still playing
                            break;
                        default:
                            break;
                    }
                }
            }
            if (activePlayersCount <= 1) //if there's only one player who is still playing, the game is over
                return -1;
            return 0;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////

        private static bool InterpretCoords(string coords, ref int x, ref int y)
        //this is for ease of use for the end user. they enter coords as letter-number like in battleships
        //validates whether a coordinate is in the correct format, but not if it's possible (i.e. if you're trying to walk into a wall)
        {
            x = 0;
            y = 0;
            if (coords.Length == 2 || coords.Length == 3)
            {
                int letter = (int)coords[0];
                string number = "";
                for (int i = 1; i < coords.Length; i++)
                    number = number + coords[i];
                if (letter >= 65 && letter <= 90)
                {
                    y = (int)(letter - 65);
                    x = Convert.ToInt32(number);
                    return true;
                }
                else if (letter >= 97 && letter <= 122)
                {
                    y = (int)(letter - 97);
                    x = Convert.ToInt32(number);
                    return true;
                }
            }
            Console.WriteLine("Invalid coordinates, please re-enter as letter then number, i.e. e15 or B4.");
            return false;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////

        void SendListToBuffer(ref string buffer, MurderScenario murderScenario)
        {

            buffer = "Weapons: ";
            foreach (string weapon in murderScenario.weaponList)
                buffer += weapon + ", ";

            buffer += ".\nPlayers: ";
            foreach (string person in murderScenario.personList)
                buffer += person + ", ";

            buffer += ".\nRooms: ";
            foreach (string person in murderScenario.roomList)
                buffer += person + ", ";
        }



    }
}
