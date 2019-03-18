using Discord;
using Discord.Commands;
using System;
using System.Threading;
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

        [Command("Suggest"), Alias("Suggestion"), Summary("Suggests [person] with [weapon].")]
        public async Task Suggest(string person = null, string weapon = null)
        {
            if (Game.gameState.Contains("Suggest")
                && Context.User.Id.ToString() == Game.player[Game.playerTurn].userID) //Checks if the correct user is using the command
            {
                if(person == null || weapon == null)
                {
                    await Context.Channel.SendMessageAsync("You must specify a person and a weapon to suggest (?Suggest [person] [weapon]).");
                    return;
                }
                Game.suggestedWeapon = weapon;
                //Game.suggestedPerson = user.Id.ToString();
                Game.suggestedPerson = person;
                Game.suggestionInProgress = true;
                SpinWait.SpinUntil(() => Game.shownCardsBuffer != "");

                await Context.User.SendMessageAsync(Game.shownCardsBuffer);
                Game.shownCardsBuffer = "";


            }
        }

        [Command("Accuse"), Alias ("Accusation"), Summary("Accuses [person] with [weapon] in [location].")]
        public async Task Accuse(string person = null, string weapon = null, string room = null)
        {
            if (Game.gameState.Contains("Roll")
                && Context.User.Id.ToString() == Game.player[Game.playerTurn].userID) //Checks if the correct user is using the command
            {
                if (person == null || weapon == null || room == null)
                {
                    await Context.Channel.SendMessageAsync("You must specify a person, a weapon and a room to accuse (?Accuse [person] [weapon] [room]).");
                    return;
                }

                Game.accusationInProgress = true;
                Game.suggestedWeapon = weapon;
                Game.suggestedPerson = person;
                Game.suggestedRoom = room;
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

        [Command("Cards"), Summary("Sends the user their cards.")]
        public async Task Cards()
        {
            foreach (Player playerNum in Game.player)
            {
                if (playerNum.username == Context.User)
                {
                    string buffer = "";

                    foreach (string card in playerNum.cards)
                        buffer += card + ", ";

                    buffer = buffer.Remove(buffer.Length - 2);
                    await Context.User.SendMessageAsync("Your cards: " + buffer + ".");
                }
                
            }
        }
    } 
}
