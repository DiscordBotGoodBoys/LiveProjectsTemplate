using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Discord;
using Discord.Commands;

namespace ClueBot.Core.Commands
{
    public class HelloWorld:ModuleBase<SocketCommandContext>
    {
        [Command("Hello"), Alias("sup", "hi", "yo"), Summary("Hello World Command")]
        public async Task ExecCommand()
        {
            await Context.Channel.SendMessageAsync("Yo.");
        }

        [Command("About"), Summary("Shows information about the bot and its authors.")]               //This 
        public async Task About()
        {
            EmbedBuilder Embed = new EmbedBuilder();
            Embed.WithAuthor("GoodBoiz");
            Embed.WithColor(55, 0, 255);
            //Embed.WithFooter()
            Embed.WithDescription("Play *Cluedo* with your friends!");
            await Context.Channel.SendMessageAsync("", false, Embed.Build());
        }

        [Command("Help"), Alias("commands"), Summary("I need help! Displays commands and what they do.")]
        public async Task Help()
        {
            EmbedBuilder Embed = new EmbedBuilder();        // used to start an embedded message
            Embed.WithTitle("Commands: ");
            Embed.WithDescription("\n\n?**help**: I need help! Displays commands and what they do. (help, commands)" +
                "\n\n?**host**: Summons the bot to your voice channel, and makes you the host if there is not one already. (host, hostgame)" +
                "\n\n?**addplayer** [@player]: Adds mentioned player to your game, provided they are in your voice channel." +
                "\n\n?**removeplayer** [@player] Removes mentioned player from your game." +
                "\n\n?**start**: Starts the game. Have fun! (start, startgame)" +
                "\n\n?**close**: Closes current game and disconnects the bot. (close, closegame)");
            
            Embed.WithColor(55, 0, 255);
            await Context.Channel.SendMessageAsync("", false, Embed.Build());
        }
    }
}
