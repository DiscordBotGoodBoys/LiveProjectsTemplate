using Discord;
using Discord.Commands;
using System;
using System.Threading.Tasks;
using Discord.WebSocket;

namespace ClueBot.Core.Commands
{
    public class HelpCommands : ModuleBase<SocketCommandContext>
    {
        [Command("help"), Summary("Displays commands and what they do.")]
        public async Task Help()
        {
            EmbedBuilder Embed = new EmbedBuilder();        // used to start an embedded message
            Embed.WithTitle("What do you need help with?");
            Embed.WithDescription("\n\n **?helpGame**: Shows game commands. " +
                "\n\n Rules of Cluedo: https://www.fgbradleys.com/rules/Clue.pdf");

            Embed.WithColor(55, 0, 255);
            await Context.Channel.SendMessageAsync("", false, Embed.Build());
        }

        [Command("helpGame"), Summary("Shows game commands.")]
        public async Task HelpGame()
        {
            EmbedBuilder Embed = new EmbedBuilder();
            Embed.WithTitle("Setup Commands");

            Embed.WithDescription("?**host**: Summons the bot to your voice channel, and makes you the host if there is not one already. (host, hostgame)" +
                "\n\n?**addplayer** [@player]: Adds mentioned player to your game, provided they are in your voice channel." +
                "\n\n?**removeplayer** [@player] Removes mentioned player from your game." +
                "\n\n?**start**: Starts the game. Have fun! (start, startgame)" +
                "\n\n?**close**: Closes the current game. (close, closegame)");
            Embed.WithColor(55, 0, 255);
            await Context.Channel.SendMessageAsync("", false, Embed.Build());

            EmbedBuilder Embed2 = new EmbedBuilder();
            Embed.WithTitle("Game Commands");

            Embed.WithDescription("?**Roll**: Rolls the dice." +
                "?**MoveTowards**: Moves you towards a building." +
                "\n\n?**Suggest** [@person] [weapon] [building]: Suggests a case (?Suggest @Haygarth Knife Kitchen). Has no game-ending consequences." +
                "\n\n?**Accuse** [@person] [weapon] [building]: Accuse someone of the crime. If you're correct, you win. If you're wrong, you're out!");
            Embed.WithColor(55, 0, 255);
            await Context.Channel.SendMessageAsync("", false, Embed.Build());

        }
        

    }
}   
