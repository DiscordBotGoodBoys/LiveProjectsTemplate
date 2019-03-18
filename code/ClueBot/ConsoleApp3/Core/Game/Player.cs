using Discord.Commands;
using Discord;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ClueBot.Core.Commands
{
    public class Player : IComparable<Player> //the player class holds any and all information required from a player
    {
        public int x, y;
        public string userID;
        public int playerNumber;
        public IUser username;

        public List<string> cards;
        public int gameStatus = 0;

        public Player(IUser username, string userID, int playerNumber, int x, int y)
        {
            this.userID = userID;
            this.playerNumber = playerNumber;
            this.x = x;
            this.y = y;
            this.username = username;
            cards = new List<string>();
            gameStatus = 0;
        }

        public bool MovePlayer(Grid grid, int x, int y, int diceroll) //returns bool to tell gamemanager whether a move occured or not
        {
            int movement = Math.Abs(this.x - x) + Math.Abs(this.y - y); // very simple movement, straight line distance
                                                                          //could be improved but not enough time and
                                                                          //will very rarely produce incorrect results
                                                                          //due to the wide open game board
            if (movement > diceroll)
            {
                Console.WriteLine("\nMovement out of bounds, please try again");
                return false;
            }

            if (grid.gridID[x, y] > 0 && grid.gridID[x, y] != playerNumber
                ) //if there is a player and it's not you
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
            grid.gridID[x, y] = playerNumber;
            this.x = x;
            this.y = y;
            return true;

        }

        public int CompareTo(Player other)
        {
            return playerNumber.CompareTo(other.playerNumber);
        }

        /*
        public int CompareTo(string other)
        {
            return userID.CompareTo(other);
        }
        */
    }
}
