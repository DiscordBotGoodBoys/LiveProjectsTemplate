using ClueBot.Resources.Database;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ClueBot.Core.Experience   ///Unnecessary; sacrificial feature.
{
    [Group("experience"), Alias("xp", "exp"), Summary("Group to manage experience")]
    public class ExperiencePoints : ModuleBase<ShardedCommandContext>
    {
        [Command(""), Alias("me", "my"), Summary("Shows your experience")]
        public async Task Me(IUser User = null)
        {
            if (User == null)
                await Context.Channel.SendMessageAsync($"{Context.User.Username} has {Data.Data.GetExp(Context.User.Id)} XP.");
            else
                await Context.Channel.SendMessageAsync($"{Context.User}, you have {Data.Data.GetExp(Context.User.Id)} XP.");
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

            //await Data.Data.SaveExp(User.Id);

            //SocketGuildUser User1 = Context.User as SocketGuildUser;
            //if(!User1.GuildPermissions.Administrator)                     Tests if the user has a specified permission.

            throw new NotImplementedException();    //Command not yet implemented.
        }

        [Command("ResetEXP"), Summary("Resets all XP progress of mentioned player")]
        public async Task ResetEXP(IUser User = null)
        {
            if(User == null)
            {
                await Context.Channel.SendMessageAsync($"You need to specify a user to reset (e.g. ?ResetEXP {Context.User.Mention}");
                return;
            }

            if(User.IsBot)
            {
                await Context.Channel.SendMessageAsync("Bots cannot be reset.");
                return;
            }

            SocketGuildUser User1 = Context.User as SocketGuildUser;
            if(!User1.GuildPermissions.Administrator)
            {
                await Context.Channel.SendMessageAsync($"You don't have administrator permissions in this server.");
                return;
            }
            await Context.Channel.SendMessageAsync($"{User.Mention}'s experience points have been reset.");

            using (var DbContext = new SqliteDbContext())
            {
                DbContext.ExperiencePoints.RemoveRange(DbContext.ExperiencePoints.Where(x => x.UserId == User.Id));
                await DbContext.SaveChangesAsync();
            }
        }

    }
}
