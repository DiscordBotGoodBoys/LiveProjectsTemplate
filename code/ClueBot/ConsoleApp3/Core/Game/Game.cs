using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ClueBot.Core.Commands
{
    public class Game
    {
        public static bool gameHosting = false;
        public static bool gamePlaying = false;

        public static string gameState = "Null";

        public static Player[] player = new Player[5];

        // = tbd
        /// = done
        private async Task Gameplay()
        {
            while (gamePlaying)
            {

            }
        }
        ///1)    Determine the order in which players play. Assign numbers (instances of classes) to players. 
        //      Set case cards. Deal cards. 
        
        //2)    Randomise weapon and player positions. 

        //3)    Game loop begins!

        //4)    Players start their turn either by rolling or suggesting a person.

        //4a)   Player suggests a case providing that they are in a room. This ends the player turn.

        //4b)   Player rolls. Player uses MoveTowards to move towards a building.

        //4c)   If the player reaches a building, they suggest a case. During suggestions, 
                



    }
}
