using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridBasedMovment
{
    public class Player : IComparable <Player>
    {
        public int id;
        public string name;
        public int x, y;
        string room;
        string[] cards;
        Random rnd = new Random();
        public Player(int id, string name, int x, int y, string room)
        {
            this.id = id;
            this.name = name;
            this.x = x;
            this.y = y;
            this.room = room;
        }
        public int rollDice()
        {
            return (rnd.Next(1, 7));
        }
        public bool movePlayer(Grid grid, int x, int y, int diceroll)
        {
            int movement = (Math.Abs(this.x - x) + Math.Abs(this.y - y));
            if (movement > diceroll)
            {
                Console.WriteLine("\nMovement out of bounds, please try again");
                return false;
            }
            else
            {
                grid.gridID[this.x, this.y] = 0;
                grid.gridID[x, y] = id;
                return true;
            }
        }
        public int CompareTo(Player other)
        {
            return (this.id.CompareTo(other.id));
        }
    }
}
