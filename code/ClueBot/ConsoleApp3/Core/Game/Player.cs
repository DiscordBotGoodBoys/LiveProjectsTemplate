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
        int posX;
        int posY;
        IUser playerUser;

        public Player(int playerNumber)
        {
            this.playerNumber = playerNumber;
        }

        public string userName
        {
            get { return userName; }
            set { userName = value; }
        }

        public int playerNumber
        {
            get { return playerNumber; }
            set { playerNumber = value; }
        }
    }
}
