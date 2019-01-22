using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Discord;
using Discord.Commands;

namespace ClueBot.Core.Commands
{
    public class TextCommands:ModuleBase<SocketCommandContext>
    {
        [Command("About"), Summary("Shows information about the bot and its authors.")]               //This 
        public async Task About()
        {
            EmbedBuilder Embed = new EmbedBuilder();    //new EmbedBuilder
            Embed.WithAuthor("GoodBoiz");               //Author   
            Embed.WithColor(55, 0, 255);                //Embed colour left outline
            //Embed.WithFooter()                        //Footer if needed
            Embed.WithDescription("Play *Cluedo* with your friends!");  //Description
            await Context.Channel.SendMessageAsync("", false, Embed.Build());   //Send embed as message
        }

        [Group("Help"), Alias("commands"), Summary("Help group")]
        public class HelpGroup : ModuleBase<ShardedCommandContext>
        {
            [Command(""), Alias ("me"), Summary("Displays commands and what they do.")]
            public async Task HelpCommand()
            {
                EmbedBuilder Embed = new EmbedBuilder();        // used to start an embedded message
                Embed.WithTitle("What do you need help with?");
                Embed.WithDescription("\n\n **?help game**: Shows game commands. ");

                Embed.WithColor(55, 0, 255);
                await Context.Channel.SendMessageAsync("", false, Embed.Build());
            }

            [Command("Game"), Summary("Shows game commands.")]
            public async Task HelpGame()
            {
                EmbedBuilder Embed = new EmbedBuilder();
                Embed.WithTitle("Setup Commands");

                Embed.WithDescription("/n/n ?**host**: Summons the bot to your voice channel, and makes you the host if there is not one already. (host, hostgame)" +
                    "\n\n?**addplayer** [@player]: Adds mentioned player to your game, provided they are in your voice channel." +
                    "\n\n?**removeplayer** [@player] Removes mentioned player from your game." +
                    "\n\n?**start**: Starts the game. Have fun! (start, startgame)" +
                    "\n\n?**close**: Closes current game and disconnects the bot. (close, closegame)");
                Embed.WithTitle("Game Commands");

                Embed.WithColor(55, 0, 255);
                await Context.Channel.SendMessageAsync("", false, Embed.Build());


            }

        }

    }
}   
