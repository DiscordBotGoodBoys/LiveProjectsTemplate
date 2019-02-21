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
    public class SetupCommands : ModuleBase<ShardedCommandContext>
    {
        public static bool gameHosting = false;
        //public static string player1 = null, player2 = null, player3 = null, 
        //    player4 = null, player5 = null, player6 = null;
        Player player1 = new Player(1);


        [Command("Host"), Summary("Summons the bot and hosts the game.")]
        public async Task Host(IUser user)
        {
            if(!gameHosting)
            {
                SocketGuildUser User1 = Context.User as SocketGuildUser;
                string player1 = Context.User.Mention;
                gameHosting = true;
            }
            
            


            throw new NotImplementedException();
        }

        [Command("AddPlayer"), Summary("Adds a player to the game.")]
        public async Task AddPlayer(IUser User = null)
        {
            if (User == null)
                await Context.Channel.SendMessageAsync("You need to specify a user to add.");
            else
            {
                for (int i = 2; i <= 6; i++)
                {
                    if (!PlayerExists(i))
                    {
                        player+i = User.Mention
                    }
                }
                /*if (User1.Id == player1.Id)
                {
                    
                }
                */

            }
        }

        [Command("RemovePlayer"), Summary("Removes a player from the game.")]
        public async Task RemovePlayer()
        {
            throw new NotImplementedException();
        }

        [Command("Start"), Alias("StartGame"), Summary("Starts the game.")]
        public async Task StartGame()
        {
            throw new NotImplementedException();
        }

        [Command("Close"), Alias("CloseGame"), Summary("Closes current game.")]
        public async Task CloseGame()
        {
            throw new NotImplementedException();
        }

        public bool PlayerExists(int player)    //Checks if player number has been added.
        {
            throw new NotImplementedException();
            return false;
            
        }
    }
}
