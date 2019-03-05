using System;
using System.Collections.Generic;

namespace ClueBot.Core.Commands
{
    public static class Scenario
    {
        public static int players = 3;

        //RNG setup for murder scene & shuffle
        private static Random rnd = new Random();
        public static int rngWeapon = rnd.Next(0, 5);
        public static int rngRoom = rnd.Next(0, 8);
        public static int rngPerson = rnd.Next(0, 5);
        private static Random rng = new Random();



        //List setup
        public static List<string> weaponList = new List<string>() { "Gun", "Bomb", "Scissors", "Knife", "Sword", "Syringe" };

        public static List<string> roomList = new List<string>(){"Bathroom","Kitchen","Living Room","Bedroom","Shed","Attic","Garden","Study","Basement"};

        public static List<string> personList = new List<string>() { "Alex", "Sam", "Asad", "Josh", "Hugo", "Cris" };

        public static List<string> murderList = new List<string>();

        public static List<string> cardsList = new List<string>();

        public static int[] playerID = new int [18];




        public static List<string> assignedCards = new List<string>() { };
        //Shuffle Cards
        public static void Shuffle<T>(this IList<T> cardsList)
        {
            int n = cardsList.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = cardsList[k];
                cardsList[k] = cardsList[n];
                cardsList[n] = value;
            }
        }

        


        //List population

    }
}
