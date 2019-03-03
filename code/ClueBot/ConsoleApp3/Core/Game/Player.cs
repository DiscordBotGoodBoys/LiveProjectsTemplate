using Discord;
using Discord.Commands;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord.WebSocket;
using System.Linq;
using Discord.Audio;

namespace ClueBot.Core.Commands
{
    public class Player
    {
        public int x, y;
        public string userID;
        public int playerNumber;

        string room;

        string[] cards;

        public Player(string userID, int playerNumber, string room)
        {
            this.userID = userID;
            this.playerNumber = playerNumber;
            this.room = room;
        }

        //public string userName
        //{
        //    get { return userName; }
        //    set { userName = value; }
        //}

        public bool movePlayer(Grid grid, int x, int y, int diceroll)
        {
            int movement = (Math.Abs(this.x - x) + Math.Abs(this.y - y));
            if (movement > diceroll)
            {
                //Context.Channel.SendMessageAsync("Movement out of bounds, please try again");
                return false;
            }
            else
            {
                grid.gridID[this.x, this.y] = 0;
                grid.gridID[x, y] = playerNumber;
                return true;
            }
        }

        public int CompareTo(Player other)
        {
            return (userID.CompareTo(other.userID));
        }

        public int CompareTo(string other)
        {
            return (userID.CompareTo(other));
        }
    }
}
