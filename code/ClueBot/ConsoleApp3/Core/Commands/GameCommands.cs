using Discord;
using Discord.Commands;
using System;
using System.Threading.Tasks;

namespace ClueBot.Core.Commands
{
    public class GameCommands : ModuleBase<SocketCommandContext> //<ShardedCommandContext>
    {
        [Command("Move"), Alias("MoveTo"), Summary("Moves the player towards a place.")]
        public async Task Move(string coords)
        {
            if(Game.gameState.Contains("Move") 
                && Context.User.Id.ToString() == Game.player[Game.playerTurn].userID) //Checks if the correct user is using the command
            {
                Game.userCoords = coords;
            }
        }

        [Command("List"), Summary("Shows all available weapons, players and rooms.")]
        public async Task List()
        {
            if (Game.gamePlaying)
            {
                EmbedBuilder Embed = new EmbedBuilder();
                Embed.WithColor(55, 0, 255);
                Embed.WithTitle("Players, Weapons and Rooms");
                Embed.WithDescription(Game.listOfEverything);
                await Context.Channel.SendMessageAsync("", false, Embed.Build());
            }
        }

        [Command("Suggest"), Alias("Suggestion"), Summary("Suggests [person] with [weapon] in [location].")]
        public async Task Suggest(IUser user = null, string weapon = null,  string room = null)
        {
            if (Game.gameState.Contains("Suggest")
                && Context.User.Id.ToString() == Game.player[Game.playerTurn].userID) //Checks if the correct user is using the command
            {
                Game.suggestionInProgress = true;
                Game.suggestedWeapon = weapon;
                Game.suggestedUser = user.Id.ToString();
                //Game.suggestedRoom = room;    //players can only suggest the room they're in!

                await Context.Channel.SendMessageAsync(Context.User + " suggested that " + user + 
                    " committed the murder with the " + weapon + " in the " + room + ".");
                //Context.User.SendMessageAsync
            }
        }

        [Command("Accuse"), Alias ("Accusation"), Summary("Accuses [person] with [weapon] in [location].")]
        public async Task Accuse(IUser user = null, string weapon = null, string room = null)
        {
            if (Game.gameState.Contains("Suggest")
                && Context.User.Id.ToString() == Game.player[Game.playerTurn].userID) //Checks if the correct user is using the command
            {
                Game.suggestionInProgress = true;
                Game.suggestedWeapon = weapon;
                Game.suggestedUser = user.Id.ToString();
                Game.suggestedRoom = room;

                await Context.Channel.SendMessageAsync(Context.User + " accused " + user +
                    " of committing the murder with the " + weapon + " in the " + room + ".");
            }
        }

        [Command("Roll"), Summary("Rolls the dice")]
        public async Task Roll()
        {
            if(Game.gameState.Contains("Roll")
                && Context.User.Id.ToString() == Game.player[Game.playerTurn].userID) //checks if it is the correct user using the command
            {
                Game.roll = DiceRoll();
                EmbedBuilder Embed = new EmbedBuilder();
                Embed.WithColor(55, 0, 255);
                Embed.WithDescription(":game_die: " + Game.roll + ". Use ?Move [coords] to move! Coords take the form of letter then number (i.e. b5 or E18).");
                await Context.Channel.SendMessageAsync("", false, Embed.Build());
            }           
        }

        public int DiceRoll()
        {
            Random random = new Random();
            int die1 = random.Next(1, 7);
            int die2 = random.Next(1, 7);
            return die1 + die2;

        }
    } 
}
