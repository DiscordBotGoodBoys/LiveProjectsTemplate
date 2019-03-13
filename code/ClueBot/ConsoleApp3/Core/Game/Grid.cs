﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClueBot.Core.Commands
{
    public class Grid
    {
        int x, y; // the dimensions of the grid
        public int[,] gridID; //the player ID stored in each square, 0 by default
        public string[] emoji; //will hopefully store emoji to be drawn once integrated with the discord bot
        public bool[,] blocked; //stores whether or not a space is a wall. false means no wall, and is the default setting
        public int[,] roomID; //stores the location of a door, that connects to the room ID stored. 0 by default
        public Grid(int x, int y)
        {
            this.x = x;
            this.y = y;
            gridID = new int[x, y];
            roomID = new int[x, y];
            blocked = new bool[x, y];
        }

        public void initializeGrid() // set all grid locations to their default values
        {
            for (int j = 0; j < y; j++)
                for (int i = 0; i < x; i++)
                {
                    gridID[i, j] = 0;
                    roomID[i, j] = 0;
                    blocked[i, j] = false;
                }
        }
        public void initializeGrid(Player[] players) // set all grid locations to their default values
                                                    // ...then put players in their spawn locations
        {
            for (int j = 0; j < y; j++)
                for (int i = 0; i < x; i++)
                {
                    gridID[i, j] = 0;
                    roomID[i, j] = 0;
                    blocked[i, j] = false;
                }
            foreach (Player item in players)
            {
                if (item != null)
                    gridID[item.x, item.y] = item.playerNumber;
            }
        }
        public void drawGrid() //draws the contents of the grid, as well as the borders around it with coordinates
        {
            Console.Write(" X|");
            for (int i = 0; i < x; i++)
                if (i < 10)
                    Console.Write(i + " |");
                else
                    Console.Write(i + "|");
            Console.Write('\n' + "--|");
            for (int i = 0; i < x; i++)
                Console.Write("---");
            Console.Write('\n');
            for (int j = 0; j < y; j++)
            {
                Console.Write((char)(j + 97) + " |");
                for (int i = 0; i < x; i++)
                {
                    if (blocked[i, j])
                    {
                        Console.Write("██|"); //where walls are drawn
                    }
                    else if (roomID[i, j] > 0)
                    {
                        Console.Write(" \\|"); //where doors are drawn
                    }
                    else
                        Console.Write(gridID[i, j] + " |"); //where playerIDs are drawn
                }
                Console.Write('\n' + "--|");
                for (int i = 0; i < x; i++)
                    Console.Write("---");
                Console.Write('\n');
            }
        }
        public void AssignStandardWalls()//applies the walls/blocking from the original cluedo board (rotated 90 degrees anticlockwise)
                                         //can only work fully properly on a grid of size (25, 24) - standard size
        {
            for (int j = 0; j < y; j++)
                for (int i = 0; i < x; i++)
                {
                    if (i == 0 || j == 0 || i == x - 1 || j == y - 1)
                        blocked[i, j] = true; // edge of board
                }
            for (int j = 0; j < 7; j++)
                for (int i = 0; i < 6; i++)
                    blocked[i, j] = true; //krustylu studios

            for (int j = 0; j < 8; j++)
                for (int i = 9; i < 16; i++)
                    blocked[i, j] = true;
            for (int j = 5; j < 8; j++)
                blocked[15, j] = false; //bowlarama

            for (int j = 0; j < 6; j++)
                for (int i = 18; i < 24; i++)   //changed from 25 to 24
                    blocked[i, j] = true; //kwik-e-mart

            for (int j = 9; j < 15; j++)
                for (int i = 0; i < 7; i++)
                    blocked[i, j] = true; //burns manor

            for (int j = 8; j < 16; j++)
                for (int i = 17; i < 23; i++)
                    blocked[i, j] = true; //nuclear power plant
            for (int j = 10; j < 14; j++)
                blocked[23, j] = true;

            for (int j = 17; j < 24; j++)
                for (int i = 0; i < 4; i++)
                    blocked[i, j] = true; //androids dungeon

            for (int j = 17; j < 24; j++)
                for (int i = 6; i < 11; i++)
                    blocked[i, j] = true; //frying dutchman
            blocked[6, 17] = false;
            blocked[10, 17] = false;

            for (int j = 18; j < 24; j++)
                for (int i = 12; i < 17; i++)
                    blocked[i, j] = true; //simpsons house

            for (int j = 18; j < 24; j++)
                for (int i = 19; i < 24; i++)   //25 to 24
                    blocked[i, j] = true; //retirement castle
            blocked[19, 18] = false;

            //SPAWN POINTS
            blocked[7, 0] = false; //col mustard
            blocked[23, 9] = false; // mrs white        24 to 23
            blocked[23, 14] = false; // rev green
            blocked[18, 23] = false; // mrs peacock
            blocked[5, 23] = false; // prof. plum
            blocked[0, 7] = false; //mrs scarlett

            //DOORS
            blocked[5, 6] = false; // krustylu 1
            roomID[5, 6] = 1;

            blocked[9, 6] = false; //bowlarama 1
            roomID[9, 6] = 2;
            blocked[12, 7] = false; //bowlarama 2
            roomID[12, 7] = 2;

            blocked[18, 4] = false; //kwik 1
            roomID[18, 4] = 3;

            blocked[19, 8] = false;
            roomID[19, 8] = 4;
            blocked[17, 9] = false;
            roomID[17, 9] = 4;
            blocked[17, 14] = false; // power plant
            roomID[17, 14] = 4;
            blocked[19, 15] = false;
            roomID[19, 15] = 4;

            blocked[19, 19] = false; // retirement castle
            roomID[19, 19] = 5;

            blocked[15, 18] = false;
            roomID[15, 18] = 6; // homer house
            blocked[12, 22] = false;
            roomID[12, 22] = 6;

            blocked[10, 20] = false;
            roomID[10, 20] = 7;
            blocked[8, 17] = false;
            roomID[8, 17] = 7; // frying dutchman

            blocked[3, 17] = false;
            roomID[3, 17] = 8; // android dungeon

            blocked[4, 14] = false;
            roomID[4, 14] = 9;
            blocked[6, 12] = false; //burns manor
            roomID[6, 12] = 9;
        }
    }
}
