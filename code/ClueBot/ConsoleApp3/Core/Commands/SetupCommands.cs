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
            if (!Game.gameHosting)  //if game is currently not being hosted
            {
                Game.player[0] = new Player(Context.User.Id.ToString(), 1, "room"); //sets command user to player 1
                Game.gameHosting = true;
                await Context.Channel.SendMessageAsync("Game open! Use ?addplayer @[user] to add more players to your game.");
            }

            else    //if game is indeed being hosted
            {
                await Context.Channel.SendMessageAsync("Somebody is already hosting! Ask them super nicely to add you to the game.");
            }

        }

        [Command("AddPlayer"), Alias("Add"), Summary("Adds a player to the game.")]
        public async Task AddPlayer(IUser User = null)  //command arguments are put in the parenthesis
        { 
            //          -broken code-
            //if (!CorrectPlayer(Context.Message.Author, 0))  //if user is not host
            //{
            //    await Context.Channel.SendMessageAsync("Only the host can add players.");
            //    return;
            //}
            

            if (User == null)   //if nobody is mentioned
            {
                await Context.Channel.SendMessageAsync("You need to specify a user to add (?addplayer @[user].");
            }

            else
            {
                for (int i = 0; i <= 6; i++)
                {
                    if (Game.player[i] != null)
                    {
                        if (User.Id.ToString() == Game.player[i].userID)
                        //if specified slot is not null and is the same as the mentioned user
                        {
                            await Context.Channel.SendMessageAsync("This player has already been added to your game.");
                            return;
                        }
                    }

                    //          -broken code-
                    //else if (PlayerExists(5)) //if slot 5 is full then the max number of players has been reached.
                    //{
                    //    await Context.Channel.SendMessageAsync("The maximum number of players has been reached; sorry!");
                    //    return;
                    //}

                    else if (!PlayerExists(i))   //fills the lowest available slot with mentioned player.
                    {
                        Game.player[i] = new Player(User.Id.ToString(), i, "room");
                        await Context.Channel.SendMessageAsync("Player " + (i + 1) + " added.");
                        i = 99;
                    }   //endif  
                }   //endfor
            }   //endelse
        }   //endcommand

        //Checks through the player list to see if user IDs match, then removes user with that ID.
        [Command("RemovePlayer"), Alias("Remove"), Summary("Removes a player from the game.")]
        public async Task RemovePlayer(IUser User = null)
        {
            if (User == null)   //if nobody is mentioned
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
        
        [Command("Close"), Alias("CloseGame"), Summary("Closes current game.")]
        public async Task CloseGame()
        {
            Game.gameHosting = false;
            Game.gamePlaying = false;
            for (int i = 0; i < 6; i++)
            {
                Game.player[i] = null;
            }
            
            await Context.Channel.SendMessageAsync("Game successfully closed.");
        }

        //Checks if player playerNumber has been added to the game.
        public bool PlayerExists(int whichPlayer)    
        {
            if (Game.player[whichPlayer] != null)
                return true;
            return false;
        }

        //Checks that the user is the specified player
        public bool CorrectPlayer(IUser user, int whichPlayer)  
        {
            if (user.Id.CompareTo(Game.player[whichPlayer].userID) == 0)
            {
                return true;
            }

            Context.Channel.SendMessageAsync("Invalid command user");
            return false;
        }

        //Mentioned users have their IDs encapsulated in symbols (eg. "<@!userid>).
        //This function removes those symbols to truely match the user IDs.
        //This function might be obsolete, it only happens on User.Mention.
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
