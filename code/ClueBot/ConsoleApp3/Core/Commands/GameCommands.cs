using Discord;
using Discord.Commands;
using System;
using System.Threading.Tasks;

namespace ClueBot.Core.Commands
{
    public class GameCommands : ModuleBase<SocketCommandContext> //<ShardedCommandContext>
    {

        

        [Command("MoveTowards"), Summary("Moves the player towards a place.")]
        public async Task MoveTowards()
        {
            throw new NotImplementedException();
        }

        //[Command("EndTurn"), Summary("Ends the current turn.")]
        //public async Task EndTurn()
        //{
        //    throw new NotImplementedException();
        //}

        [Command("Suggest"), Summary("Suggests [person] with [weapon] in [location].")]
        public async Task Suspect()
        {
            throw new NotImplementedException();
        }

        [Command("Accuse"), Summary("Accuses [person] with [weapon] in [location].")]
        public async Task Accuse()
        {
            throw new NotImplementedException();
        }

        [Command("Roll"), Summary("Rolls the dice")]
        public async Task Roll()
        {
            if(Game.gameState == "Roll" && Context.User.Id.ToString() == Game.player[Game.playerTurn].userID)
            {
                Game.roll = DiceRoll();
                EmbedBuilder Embed = new EmbedBuilder();
                Embed.WithColor(55, 0, 255);
                Embed.WithDescription(":game_die: " + roll + ". Use ?MoveTowards [coords] to move!");
                await Context.Channel.SendMessageAsync("", false, Embed.Build());
            }           
        }

        public int DiceRoll()
        {
            Random random = new Random();
            int die1 = random.Next(1, 7);
            int die2 = random.Next(1, 7);
            //Console.WriteLine(die1 + " + " + die2);
            return die1 + die2;
        }
    } 
}
