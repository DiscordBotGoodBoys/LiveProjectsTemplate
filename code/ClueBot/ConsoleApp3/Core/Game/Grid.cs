using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClueBot.Core.Commands
{
    public class Grid
    {
        int x, y;
        public int[,] gridID;
        public string[] emoji;
        bool[,] blocked;
        public int[,] roomID;
        public Grid(int x, int y)
        {
            this.x = x;
            this.y = y;
            gridID = new int[x, y];
        }
        public void initializeGrid()
        {
            for (int j = 0; j < y; j++)
                for (int i = 0; i < x; i++)
                    gridID[i, j] = 0;
        }
        public void initializeGrid(Player[] players)
        {
            for (int j = 0; j < y; j++)
                for (int i = 0; i < x; i++)
                    gridID[i, j] = 0;
            foreach(Player item in players)
            {
                if(item != null)
                    gridID[item.x, item.y] = item.playerNumber;
            }
        }
        public void drawGrid()
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
                if (j < 10)
                    Console.Write(j + " |");
                else
                    Console.Write(j + "|");
                for (int i = 0; i < x; i++)
                {
                    Console.Write(gridID[i, j] + " |");
                }
                Console.Write('\n' + "--|");
                for (int i = 0; i < x; i++)
                    Console.Write("---");
                Console.Write('\n');
            }
        }
    }
}
