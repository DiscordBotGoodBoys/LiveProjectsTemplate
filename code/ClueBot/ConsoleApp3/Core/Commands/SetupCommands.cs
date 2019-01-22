﻿using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord.WebSocket;
using System.Linq;

namespace ClueBot.Core.Commands
{
    public class SetupCommands : ModuleBase<ShardedCommandContext>
    {
        [Command("Host"), Summary("Host a game, and summons the bot.")]
        public async Task Host()
        {

        }

        [Command("AddPlayer"), Summary("Adds a player to the game.")]
        public async Task AddPlayer(IUser User = null)
        {
            if (User == null)
                await Context.Channel.SendMessageAsync("You need to specify a user to add.");
            else
            {

            }
        }

        [Command("RemovePlayer"), Summary("Removes a player from the game.")]
        public async Task RemovePlayer()
        {

        }

        [Command("Start"), Alias("StartGame"), Summary("Starts the game.")]
        public async Task StartGame()
        {

        }

        [Command("Close"), Alias("CloseGame"), Summary("Closes current game.")]
        public async Task CloseGame()
        {

        }

    }
}
