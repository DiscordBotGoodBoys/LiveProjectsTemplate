using Discord;
using Discord.Commands;
using System;
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
    }
}
