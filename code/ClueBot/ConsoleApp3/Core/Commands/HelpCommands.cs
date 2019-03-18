using Discord;
using Discord.Commands;
using System.Threading.Tasks;

namespace ClueBot.Core.Commands
{
    public class HelpCommands : ModuleBase<SocketCommandContext>
    {
        [Command("help"), Summary("Displays commands and what they do.")]
        public async Task Help(string param)
        {



            if (param == "Game" || param == "game") 
            {
                EmbedBuilder Embed3 = new EmbedBuilder();
                Embed3.WithTitle("Setup Commands");

                Embed3.WithDescription("?**host**: Summons the bot to your voice channel, and makes you the host if there is not one already. (host, hostgame)" +
                    "\n\n?**add** [@player]: Adds mentioned player to your game, provided they are in your voice channel. (add, addplayer)" +
                    "\n\n?**remove** [@player] Removes mentioned player from your game. (remove, removeplayer)" +
                    "\n\n?**start**: Starts the game. Have fun! (start, startgame)" +
                    "\n\n?**close**: Closes the current game. (close, closegame)");
                Embed3.WithColor(55, 0, 255);
                await Context.Channel.SendMessageAsync("", false, Embed3.Build());

                EmbedBuilder Embed2 = new EmbedBuilder();
                Embed2.WithTitle("Game Commands");

                Embed2.WithDescription("?**Roll**: Rolls the dice." +
                    "\n\n?**Cards**: Direct messages your cards." + 
                    "\n\n?**List**: Lists all available cards." +
                    "\n\n?**Move** [coords]: Moves you to the specified coordinates (?Move E18)." +
                    "\n\n?**Suggest** [person] [weapon]: Suggests a case (?Suggest Haygarth Knife). Has no game-ending consequences." +
                    "\n\n?**Accuse** [person] [weapon] [location]: Accuse someone of the crime. If you're correct, you win. If you're wrong, you're out!");
                Embed2.WithColor(55, 0, 255);
                await Context.Channel.SendMessageAsync("", false, Embed2.Build());

                return;

            }

            EmbedBuilder Embed = new EmbedBuilder();        // used to start an embedded message
            Embed.WithTitle("What do you need help with?");
            Embed.WithDescription("\n\n **?help Game**: Shows game commands. " +
                "\n\n Rules of Cluedo: https://www.fgbradleys.com/rules/Clue.pdf");

            Embed.WithColor(55, 0, 255);
            await Context.Channel.SendMessageAsync("", false, Embed.Build());
        }
    }
}   
