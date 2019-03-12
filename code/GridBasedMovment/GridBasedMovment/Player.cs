using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridBasedMovment
{
    public class Player : IComparable<Player>
    {
        public int id;
        public string name;
        public int x, y;
        public List<String> cards;
        public int gameStatus = 0;
        Random rnd;
        public Player(int id, string name, int x, int y)
        {
            this.id = id;
            this.name = name;
            this.x = x;
            this.y = y;
            cards = new List<string>();
            gameStatus = 0;
        }
        public void rollDice(ref int i, ref int j)
        {
            rnd = new Random();
            i = rnd.Next(1, 7); j = rnd.Next(1, 7);
        }
        public bool movePlayer(Grid grid, int x, int y, int diceroll)
        {
            int movement = (Math.Abs(this.x - x) + Math.Abs(this.y - y));
            if (movement > diceroll)
            {
                Console.WriteLine("\nMovement out of bounds, please try again");
                return false;
            }
            if (grid.gridID[x, y] > 0 && grid.gridID[x, y] != id)
            {
                Console.WriteLine("\nCan't move on top of another player, please try again");
                return false;
            }
            if (grid.blocked[x, y] == true)
            {
                Console.WriteLine("\nThis space is blocked, please try again");
                return false;
            }
            for (int j = -1; j < 2; j++)
            {
                for (int i = -1; i < 2; i++)
                {
                    if (grid.roomID[x + i, y + j] > 0 //if the space you're trying to move to is next to a door 
                        && (x + i != x || y + j != y)) //if the space you're trying to move to is not the door itself
                    {
                        Console.WriteLine("\nYou cannot occupy the space in front of a door. Please try again");
                        return false;
                    }
                }
            }

            grid.gridID[this.x, this.y] = 0;
            grid.gridID[x, y] = id;
            this.x = x;
            this.y = y;
            return true;

        }
        public int CompareTo(Player other)
        {
            return (this.id.CompareTo(other.id));
        }
    }
}