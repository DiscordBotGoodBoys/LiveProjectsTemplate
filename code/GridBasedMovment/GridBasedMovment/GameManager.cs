using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridBasedMovment
{
    public static class GameManager
    {
        public static bool Turn(Player player, Grid grid)
        {
            Console.Write(player.name + "'s turn. ");
            if (grid.roomID[player.x, player.y] > 0)
            {
                Console.WriteLine("Would you like to make a suggestion? Enter 0 for no, 1 for yes");
                ConsoleKeyInfo cki = Console.ReadKey();
                int choice = Convert.ToInt32(cki.Key.ToString());
                if (choice == 1)
                {
                    Suggest(player, grid.roomID[player.x, player.y]);
                    return GameWon(player);
                }
            }
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
                Suggest(player, grid.roomID[x,y]);
            }

            return GameWon(player);
        }
        private static void Suggest(Player player, int roomID)
        {

        }
        private static bool GameWon(Player player)
        {
            return false;
        }
        private static bool InterpretCoords(string coords, ref int x, ref int y)
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
