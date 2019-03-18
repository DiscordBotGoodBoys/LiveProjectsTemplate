using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClueBot.Core.Commands
{
    public class MurderScenario
    {
        private Random rnd;

        public List<string> weaponList = new List<string>() { "Gun", "Bomb", "Scissors", "Knife", "Sword", "Syringe" };
        public List<string> roomList = new List<string>() { "Bathroom", "Kitchen", "LivingRoom", "Bedroom", "Shed", "Attic", "Garden", "Study", "Basement" };
        public List<string> personList = new List<string>() { "Alex", "Sam", "Asad", "Josh", "Hugo", "Cris" };

        public List<string> murderList = new List<string>() { };
        public List<string> cardsToDealList = new List<string>() { };

        public MurderScenario(Player[] player)
        {
            rnd = new Random();
            for (int i = 0; i < player.Length; i++)
            {
                if (player[i] != null)
                {
                    personList[i] = player[i].usernameNoID;
                }
            }
            CreateMurderScenario();
            Shuffle(ref cardsToDealList);
            DealCards(player);
        }

        private void DealCards(Player[] players)
        {
            int playerID = 0;
            int playerCount = 0;
            foreach (Player player in players)
                if (player != null)
                    playerCount++;
            for (int i = 0; i < cardsToDealList.Count; i++)
            {
                if (playerID >= playerCount)
                    playerID = 0;
                players[playerID].cards.Add(cardsToDealList[i]);
                playerID++;
            }
        }

        private void CreateMurderScenario()
        {
            int nextCard;

            nextCard = rnd.Next(6);
            murderList.Add(weaponList[nextCard]);

            nextCard = rnd.Next(9);
            murderList.Add(roomList[nextCard]);

            nextCard = rnd.Next(6);
            murderList.Add(personList[nextCard]);

            foreach (string item in weaponList)
                if (!murderList.Contains(item))
                    cardsToDealList.Add(item);
            foreach (string item in roomList)
                if (!murderList.Contains(item))
                    cardsToDealList.Add(item);
            foreach (string item in personList)
                if (!murderList.Contains(item))
                    cardsToDealList.Add(item);
        }

        private void Shuffle(ref List<String> cardsList)
        {
            int n = cardsList.Count;
            while (n > 1)
            {
                n--;
                int k = rnd.Next(n + 1);
                string value = cardsList[k];
                cardsList[k] = cardsList[n];
                cardsList[n] = value;
            }
        }
    }
}
