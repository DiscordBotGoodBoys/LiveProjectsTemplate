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
            Console.WriteLine(player.name + "'s turn. Press any key to roll your dice!");
            Console.ReadKey();
            int diceRollOne = player.rollDice(); int diceRollTwo = player.rollDice();
            Console.WriteLine("You rolled {0} + {1} = {2}! Choose a space to move to", diceRollOne, diceRollTwo, (diceRollOne + diceRollTwo));
            bool movementValid = false;
            while (!movementValid) {
                Console.Write("\nEnter an x coordinate: ");
                int x = Convert.ToInt32(Console.ReadLine());
                Console.Write("\nEnter a y coordinate: ");
                int y = Convert.ToInt32(Console.ReadLine());
                movementValid = player.movePlayer(grid, x, y, diceRollOne + diceRollTwo);
            }
            return GameWon(player);
        }
        private static bool GameWon(Player player)
        {
            return false;
        }
    }
}
