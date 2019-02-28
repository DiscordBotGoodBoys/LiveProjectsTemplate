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
        public static bool gameHosting = false;
        public static bool gameStarted = false;
        public static string gameState = "Null";
        //public static string player1 = null, player2 = null, player3 = null, 
        //    player4 = null, player5 = null, player6 = null;

        
        public static Player[] player = new Player[5]; 




        [Command("Host"), Summary("Summons the bot and hosts the game.")]
        public async Task Host(IUser User = null)
        {
            if (User == null)
            {
                await Context.Channel.SendMessageAsync("You need to specify a user to elect as host (?host @[user]).");
                return;
            }

            if (!gameHosting)
            {
                //SocketGuildUser User1 = Context.User as SocketGuildUser;
                player[0] = new Player(User.Mention, 1, "room");
                gameHosting = true;
                await Context.Channel.SendMessageAsync("Game opened! Use ?addplayer @[user] to add more players to your game.");
            }

            else
            {
                await Context.Channel.SendMessageAsync("Somebody is already hosting! Ask them super nicely to add you to the game.");
            }
            
            


            
        }

        [Command("AddPlayer"), Summary("Adds a player to the game.")]
        public async Task AddPlayer(IUser User = null)
        {
            if (User == null)
            {
                await Context.Channel.SendMessageAsync("You need to specify a user to add (?addplayer @[user].");
                return;
            }
            else
            {
                for (int i = 0; i <= 6; i++)
                {
                    if(player[i] != null)
                    {
                        if (User.Mention == player[i].userID)
                        {
                            await Context.Channel.SendMessageAsync("This player has already been added to your game.");
                            return;
                        }
                    }

                    if (!PlayerExists(i))
                    {
                        player[i] = new Player(User.Mention, i, "room");
                        await Context.Channel.SendMessageAsync("Player " + (i + 1) + " added.");
                        i = 99;
                    }
                }



            }
        }

        [Command("RemovePlayer"), Summary("Removes a player from the game.")]
        public async Task RemovePlayer(IUser User = null)
        {
            if (User == null)
            {
                await Context.Channel.SendMessageAsync("You need to specify a user to remove(?removeplayer @[user]).");
                return;
            }
            else
            {
                for (int i = 0; i <= 6; i++)
                {
                    if (player[i] == User)
                    {
                        player[i] = null;
                    }
                }
            }
        }

        [Command("Start"), Alias("StartGame"), Summary("Starts the game.")]
        public async Task StartGame()
        {
            if (player.GetLength(6) > 1)
            {
                gameStarted = true;
                gameState = "";
            }
        }

        [Command("Close"), Alias("CloseGame"), Summary("Closes current game.")]
        public async Task CloseGame()
        {
            gameHosting = false;
            for (int i = 0; i < 6; i++)
            {
                player[i] = null;
            }
        }

        public bool PlayerExists(int playerNumber)    //Checks if player number has been added.
        {
            if (player[playerNumber] != null)
                return true;
            return false;
            
        }
    }
}
