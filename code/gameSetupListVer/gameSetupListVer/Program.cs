using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gameSetupListVer
{
   public class Program
    {
        static void Main(string[] args)
        {
            //Display lists
            Console.WriteLine("Available weapons: ");
            foreach (var el in Scenario.weaponList)
                Console.WriteLine(el);
            Console.WriteLine(Environment.NewLine);

            Console.WriteLine("Available rooms: ");
            foreach (var el in Scenario.roomList)
                Console.WriteLine(el);
            Console.WriteLine(Environment.NewLine);

            Console.WriteLine("Available people: ");
            foreach (var el in Scenario.personList)
                Console.WriteLine(el);
            Console.WriteLine(Environment.NewLine);

            //Murder Population
            Scenario.murderList.Add(Scenario.weaponList[Scenario.rngWeapon]);
            Scenario.murderList.Add(Scenario.roomList[Scenario.rngRoom]);
            Scenario.murderList.Add(Scenario.personList[Scenario.rngPerson]);

            //Remove from list
            Scenario.weaponList.RemoveAt(Scenario.rngWeapon);
            Scenario.roomList.RemoveAt(Scenario.rngRoom);
            Scenario.personList.RemoveAt(Scenario.rngPerson);

            //Display New Lists & Murder scenario
            Console.WriteLine("Murder scenario: ");
            foreach (var el in Scenario.murderList)
                Console.WriteLine(el);
            Console.WriteLine(Environment.NewLine);


            Console.WriteLine("Available weapons: ");
            foreach (var el in Scenario.weaponList)
                Console.WriteLine(el);
            Console.WriteLine(Environment.NewLine);

            Console.WriteLine("Available rooms: ");
            foreach (var el in Scenario.roomList)
                Console.WriteLine(el);
            Console.WriteLine(Environment.NewLine);

            Console.WriteLine("Available people: ");
            foreach (var el in Scenario.personList)
                Console.WriteLine(el);
            Console.WriteLine(Environment.NewLine);

            //Pool Remaining Cards
            foreach (var el in Scenario.weaponList)
                Scenario.cardsList.Add(el);

            foreach (var el in Scenario.roomList)
                Scenario.cardsList.Add(el);

            foreach (var el in Scenario.personList)
                Scenario.cardsList.Add(el);

            //Display cards
            Console.WriteLine("Available cards: ");
            foreach (var el in Scenario.cardsList)
                Console.WriteLine(el);
            Console.WriteLine(Environment.NewLine);

            //Shuffle Cards

            Scenario.cardsList.Shuffle();

            // Display shuffled cards
            Console.WriteLine("Shuffled cards: ");
            foreach (var el in Scenario.cardsList)
                Console.WriteLine(el);
            Console.WriteLine(Environment.NewLine);


            //Deal Cards 
           int cards = Scenario.cardsList.Count;

            Console.WriteLine("Available cards: " + cards);

            int i=0;
            Console.WriteLine("Assigned cards: ");
            foreach (var el in Scenario.cardsList)
            {
                int ID = i % Scenario.players;
                Scenario.playerID[i] = ID;
                Console.WriteLine( el +" "+ Scenario.playerID[i]);
                i++;
            }

            
              
            
        






            Console.ReadKey();
        }
    }
}
