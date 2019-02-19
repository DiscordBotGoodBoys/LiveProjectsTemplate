using System;
using System.Collections.Generic;
using System.Text;




namespace ClueBot.Core.Game
{
    class Game
    {
        //Game States
        const int SETUP = 0;
        const int START = 1;
        const int SUGGEST1 = 2;
        const int ROLL = 3;
        const int MOVE = 4;
        const int SUGGEST2 = 5;
        const int END = 6;

        int playerTurn = 0;



        //1)    Determine the order in which players play. Assign numbers (instances of classes) to players. 
        //      Set case cards. Deal cards. 
        
        //2)    Randomise weapon and player positions. 

        //3)    Game loop begins!

        //4)    Players start their turn either by rolling or suggesting a person.

        //4a)   Player suggests a case providing that they are in a room. This ends the player turn.

        //4b)   Player rolls. Player uses MoveTowards to move towards a building.

        //4c)   If the player reaches a building, they suggest a case. During suggestions, 
                



    }
}
