using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ClueBot.Core.Commands
{
    class GameCommands
    {
        [Command("MoveTowards"), Summary("Moves the player towards a place.")]
        public async Task MoveTowards()
        {

        }

        [Command("EndTurn"), Summary("Ends the current turn.")]
        public async Task EndTurn()
        {

        }

        [Command("Suspect"), Summary("Suspects [person] with [weapon] in [location].")]
        public async Task Suspect()
        {

        }

        [Command("Accuse"), Summary("Accuses [person] with [weapon] in [location].")]
        public async Task Accuse()
        {

        }
    }
}
