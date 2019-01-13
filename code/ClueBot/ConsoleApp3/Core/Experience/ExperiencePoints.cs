using Discord.Commands;
using Discord;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord.WebSocket;
using ClueBot.Core.Data;

namespace ClueBot.Core.Experience
{
    [Group("experience"), Alias("experiencepoints", "xp", "exp"), Summary("Group to manage experience")]
    class ExperiencePoints : ModuleBase<ShardedCommandContext>
    {
        [Command(""), Alias("me", "my"), Summary("Shows your experience")]
        public async Task Me()
        {

        }

        [Command("wager"), Alias("bet"), Summary("Used to bet some points towards the next game")]
        public async Task Wager(int Amount = 0)
        {
            //exp wager 100
            if (Amount <= 0)        //Tests that the user is wagering more than 0.
            {
                await Context.Channel.SendMessageAsync($"You need to specify a valid amount of stones to wager.");
                return;
            }

            //SocketGuildUser User1 = Context.User as SocketGuildUser;
            //if(!User1.GuildPermissions.Administrator)                     Tests if the user has a specified permission.

            throw new NotImplementedException();    //Command not yet implemented.
        }
     
    }
}
