using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GridBasedMovment
{
    public class Player : IComparable<Player> //the player class holds any and all information required from a player
    {
        public int id; // is the ID of the player as understood on the game board
        public string name; // is the name the players will see on discord
        public int x, y; // are the x and y coordinates on the game board
        public List<String> cards; //is the list of cards in this players hand that other players can check
        public int gameStatus = 0; //shows the player's state:
                                // 0 is the default state
                                // -1 means the player has failed an accusation and is no longer able to play
                                // +1 means the player has succeeded with their accusation and won. hooray!
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
        public void rollDice(ref int i, ref int j) //passing by reference so we can get and display both individual numbers
        {
            rnd = new Random();
            i = rnd.Next(1, 7); j = rnd.Next(1, 7);
        }
        public bool movePlayer(Grid grid, int x, int y, int diceroll) //returns bool to tell gamemanager whether a move occured or not
        {
            int movement = (Math.Abs(this.x - x) + Math.Abs(this.y - y)); // very simple movement, straight line distance
                                                                        //could be improved but not enough time and
                                                                        //will very rarely produce incorrect results
                                                                        //due to the wide open game board
            if (movement > diceroll)
            {
                Console.WriteLine("\nMovement out of bounds, please try again");
                return false;
            }

            if (grid.gridID[x, y] > 0 && grid.gridID[x, y] != id) //if there is a player and it's not you
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
                        //this is to hopefully stop players blocking eachother from entering doors. hopefully
                    }
                }
            }

            grid.gridID[this.x, this.y] = 0;
            grid.gridID[x, y] = id;
            this.x = x;
            this.y = y;
            return true;

        }
        public int CompareTo(Player other) //may be superfluous, think I forgot I'd put it in lol
        {
            return (this.id.CompareTo(other.id));
        }
    }
}