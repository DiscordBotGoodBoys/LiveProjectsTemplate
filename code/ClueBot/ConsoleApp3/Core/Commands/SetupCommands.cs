using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord.WebSocket;
using System.Linq;
using Discord.Audio;

namespace ClueBot.Core.Commands
{
    public class SetupCommands : ModuleBase<SocketCommandContext>
    {
        




        [Command("Host"), Summary("Summons the bot and hosts the game.")]
        public async Task Host()
        {
            if (!Game.gameHosting)
            {
                Game.player[0] = new Player(Context.User.Id.ToString(), 1, "room");
                Game.gameHosting = true;
                await Context.Channel.SendMessageAsync("Game open! Use ?addplayer @[user] to add more players to your game.");
            }

            else
            {
                await Context.Channel.SendMessageAsync("Somebody is already hosting! Ask them super nicely to add you to the game.");
            }

        }

        [Command("AddPlayer"), Summary("Adds a player to the game.")]
        public async Task AddPlayer(IUser User = null)
        { 

            

            if (!CorrectPlayer(Context.Message.Author, 1))
            {
                  await Context.Channel.SendMessageAsync("Only the host can add players."); 
            }

            if (User == null)
            {
                await Context.Channel.SendMessageAsync("You need to specify a user to add (?addplayer @[user].");
                return;
            }
            else
            {
                for (int i = 0; i <= 6; i++)
                {
                    if(Game.player[i] != null)
                    {
                        if (User.Id.ToString() == Game.player[i].userID)
                        {
                            await Context.Channel.SendMessageAsync("This player has already been added to your game.");
                            return;
                        }
                    }

                    if (!PlayerExists(i))
                    {
                        Game.player[i] = new Player(User.Id.ToString(), i, "room");
                        await Context.Channel.SendMessageAsync("Player " + (i + 1) + " added.");
                        i = 99;
                    }
                }
            }
        }

        //Checks through the player list to see if user IDs match, then removes user with that ID.
        [Command("RemovePlayer"), Summary("Removes a player from the game.")]
        public async Task RemovePlayer(IUser User = null)
        {
            if (User == null)
            {
                await Context.Channel.SendMessageAsync("You need to specify a user to remove (?removeplayer @[user]).");
                return;
            }
            else
            {
                for (int i = 0; i <= 6; i++)
                {
                    if (Game.player[i].userID == User.Id.ToString())
                    {
                        Game.player[i] = null;
                    }
                }
            }
        }

        [Command("Start"), Alias("StartGame"), Summary("Starts the game.")]
        public async Task StartGame()
        {
            if (Game.player.GetLength(6) > 1)
            {
                Game.gameStart = true;
                Game.gameState = "";
            }
        }

        
        [Command("Close"), Alias("CloseGame"), Summary("Closes current game.")]
        public async Task CloseGame()
        {
            Game.gameHosting = false;
            Game.gamePlaying = false;
            for (int i = 0; i < 6; i++)
            {
                Game.player[i] = null;
            }
        }

        //Checks if player playerNumber has been added to the game.
        public bool PlayerExists(int playerNumber)    
        {
            if (Game.player[playerNumber] != null)
                return true;
            return false;
            
        }

        //Checks that the user is the specified player
        public bool CorrectPlayer(IUser user, int whichPlayer)  
        {
            if (user.Id.CompareTo(Game.player[whichPlayer].userID) > 0)
            {
                return true;
            }

            Context.Channel.SendMessageAsync("Invalid command user");
            return false;
        }

        //Mentioned users have their IDs encapsulated in symbols (eg. "<@!userid>).
        //This function removes those symbols to truely match the user IDs.
        public string Unmentionify(ref string mentionedUser)
        {
            mentionedUser = mentionedUser.Replace("<@!", "");
            mentionedUser = mentionedUser.Replace(">", "");
            return mentionedUser;
        }

        //string mentionedUser = User.Mention;
        //Unmentionify(ref mentionedUser);        <------ That's how it's used :^)


    }
}
