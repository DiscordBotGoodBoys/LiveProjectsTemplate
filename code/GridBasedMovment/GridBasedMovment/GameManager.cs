using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridBasedMovment
{
    public static class GameManager
    {
        public static bool Turn(Player player, Player[] players, Grid grid, MurderScenario murderScenario)
        {
            Console.Write(player.name + "'s turn. ");
            if (grid.roomID[player.x, player.y] > 0)
            {
                Console.WriteLine("You are currently in the " + murderScenario.roomList[grid.roomID[player.x, player.y] - 1] + ".");
                Console.WriteLine("Would you like to make a suggestion? Enter 0 for no, 1 for yes");
                ConsoleKeyInfo cki = Console.ReadKey();
                int choice = Convert.ToInt32(cki.Key.ToString());
                if (choice == 1)
                {
                    Suggest(player, players, grid.roomID[player.x, player.y], murderScenario);
                    return false;
                }
                else
                {

                }
            }
            Console.WriteLine("Would you like to make an accusation? WARNING: Accusing incorrectly will kick you out of the game, so you should only do this when you're certaion of whodunnit!");
            Console.WriteLine("Enter 0 for no, 1 for yes.");
            int willAccuse = 0;
            willAccuse = Convert.ToInt32(Console.ReadLine());
            if (willAccuse == 1)
            {
                return Accuse(player, murderScenario);
            }
            else
            {
                Console.WriteLine("Press any key to roll your dice!");

                Console.ReadKey();
                int diceRollOne = 0; int diceRollTwo = 0;
                player.rollDice(ref diceRollOne, ref diceRollTwo);
                Console.WriteLine("You rolled {0} + {1} = {2}! Choose a space to move to", diceRollOne, diceRollTwo, (diceRollOne + diceRollTwo));
                bool movementValid = false;
                int x = 0;
                int y = 0;
                while (!movementValid)
                {
                    Console.Write("\nEnter coordinates taking the form of letter then number, i.e. e15 or B4: ");
                    string coords = Console.ReadLine();
                    x = 0;
                    y = 0;
                    if (InterpretCoords(coords, ref x, ref y))
                        movementValid = player.movePlayer(grid, x, y, diceRollOne + diceRollTwo);
                }
                if (grid.roomID[x, y] > 0)
                {
                    Suggest(player, players, grid.roomID[x, y], murderScenario);
                }
            }
            return false;
        }
        private static void Suggest(Player player, Player[] players, int roomID, MurderScenario murderScenario)
        {
            bool suggestionReady = false;
            int weaponChoice = 0;
            int personChoice = 0;
            while (!suggestionReady)
            {
                Console.Write("\nSuggest a weapon from the weapon list: ");
                foreach (string weapon in murderScenario.weaponList)
                    Console.Write(weapon + ", ");
                Console.WriteLine();
                weaponChoice = Convert.ToInt32(Console.ReadLine());
                Console.Write("\nSuggest a person from the person list: ");
                foreach (string person in murderScenario.personList)
                    Console.Write(person + ", ");
                Console.WriteLine();
                personChoice = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("You suggest that " + murderScenario.personList[personChoice - 1] + " comitted murder in the " + murderScenario.roomList[roomID - 1] + " with the " + murderScenario.weaponList[weaponChoice - 1]);
                Console.WriteLine("Are you happy with this suggestion? 1 for yes 0 for no");
                int choice = Convert.ToInt32(Console.ReadLine());
                if (choice == 1)
                    suggestionReady = true;
            }
            Player nearestPlayerWithCards = CheckPlayersForCards(player.id, players, murderScenario.personList[personChoice - 1], murderScenario.roomList[roomID - 1], murderScenario.weaponList[weaponChoice - 1]);
            if (nearestPlayerWithCards == null)
            {
                Console.WriteLine("The cards you suggested are not held by any of your fellow detectives...");
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine(nearestPlayerWithCards.name + " holds one or more of the cards you suggested. They will now decide which card to reveal.");
                string cardToShow = ChooseCardToShow(nearestPlayerWithCards, player, murderScenario.personList[personChoice - 1], murderScenario.roomList[roomID - 1], murderScenario.weaponList[weaponChoice - 1]);
                Console.WriteLine(nearestPlayerWithCards.name + " shows you " + cardToShow + ". Press any key to continue."); //this needs to be sent in a DM to the player
                Console.ReadKey();
            }
        }
        private static bool Accuse(Player player, MurderScenario murderScenario)
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
                return true;
            }
            else
            {
                player.gameStatus = -1;
                Console.WriteLine("False accusations never solve anything. You got " + correctCards + " out of 3 cards correct, and are forced to retire from the case. Press any key to continue.");
                Console.ReadKey();
                return false;
            }
        }
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
                Console.WriteLine("The only card suggested in your hand was " + buffer + "so you need not choose a card. Press any key to send " + suggestingPlayer.name + " this card.");
                Console.ReadKey();
                return cards[0];
            }
            else
            {
                Console.Write("You must choose a card to show to " + suggestingPlayer.name + " out of the following in your hand: ");
                Console.WriteLine(buffer);
                Console.WriteLine("Select using a number between 1 and " + noOfCards + ", corresponding to the cards place in the above list.");
                int choice = Convert.ToInt32(Console.ReadLine());
                return cards[choice - 1];
            }
        }
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
                            break;
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
    }
}
