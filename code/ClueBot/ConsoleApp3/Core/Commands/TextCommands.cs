using Discord;
using Discord.Commands;
using System.Threading.Tasks;

namespace ClueBot.Core.Commands
{
    public class TextCommands:ModuleBase<SocketCommandContext>
    {
        [Command("About"), Summary("Shows information about the bot and its authors.")]
        public async Task About()
        {
            EmbedBuilder Embed = new EmbedBuilder();    //new EmbedBuilder
            Embed.WithAuthor("GoodBoiz");               //Author   
            Embed.WithColor(55, 0, 255);                //Embed colour left outline
            //Embed.WithFooter()                        //Footer if needed
            Embed.WithDescription("Play *Cluedo* with your friends!");  //Description
            await Context.Channel.SendMessageAsync("", false, Embed.Build());   //Send embed as message
        }
            
        [Command("EmbedTest"), Summary("Test")]
        public async Task EmbedTest()
        {
            EmbedBuilder Embed = new EmbedBuilder();    //new EmbedBuilder
            Embed.WithAuthor("Test");               //Author   
            Embed.WithColor(55, 0, 255);                //Embed colour left outline
            //Embed.WithFooter()                        //Footer if needed
            Embed.WithDescription("");  //Description
            await Context.Channel.SendMessageAsync("", false, Embed.Build());   //Send embed as message
        }



    }
}   
