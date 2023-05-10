using Poker.Extensions;
using Poker.PokerClasses;
using System;
using System.Collections.Generic;
    
namespace Poker
{
    internal class Program
    {
        private readonly static int maxPlayersCount = 10;
        private readonly static int minPlayersCount = 1;

        static void Main(string[] args)
        {
            //генерування колоди

            List<Card> deck = new List<Card>();

            foreach(CardValue value in Enum.GetValues(typeof(CardValue)))
            {
                foreach(Suits suit in Enum.GetValues(typeof(Suits)))
                {
                    deck.Add(new Card(suit, value));
                }
            }

            //генерування гравців та їх "рук"
            int playersCount = 0;

            //вводимо у консоль кількість гравців   
            while (true)
            {
                Console.WriteLine("Введiть кiлькiсть гравцiв: ");

                if (int.TryParse(Console.ReadLine(), out playersCount))
                {
                    playersCount = playersCount.Clamp(minPlayersCount, maxPlayersCount);
                    break;
                }
                else
                    Console.WriteLine("Ви ввели неправильну кiлькiсть");
            }

            List<Player> players = new List<Player>();

            var handGeneratorRandom = new Random();
            for (int i = 0; i < playersCount; i++)
            {
                List<Card> playerHand = new List<Card>();

                for (int a = 0; a < 2; a++)
                {
                    int cardIndex = handGeneratorRandom.Next(0, deck.Count);

                    playerHand.Add(deck[cardIndex]);
                    deck.RemoveAt(cardIndex);
                }

                players.Add(new Player(playerHand));
            }

            //генерування колоди на столі та її вивід
            List<Card> tableDeck = new List<Card>();
            var tableDeckGeneratorRandom = new Random();

            for (int a = 0; a < 5; a++)
            {
                int cardIndex = tableDeckGeneratorRandom.Next(0, deck.Count);

                tableDeck.Add(deck[cardIndex]);
                deck.RemoveAt(cardIndex);
            }

            string tableDeckInStr = "";

            foreach (var card in tableDeck)
            {
                tableDeckInStr += card.GetCardStr() + ", ";
            }

            Console.WriteLine($"Table deck: {tableDeckInStr}\n");

            /* вивід всіх гравців та їх карт і комбінацій*/
            foreach(var player in players)
            {
                string playerHandInStr = $"Player {players.IndexOf(player)+1}'s hand: ";

                foreach(var card in player.hand)
                {
                    playerHandInStr += card.GetCardStr() + ", ";
                }

                Console.WriteLine(playerHandInStr);
                Console.WriteLine($"Combination: {player.GetCombination(new List<Card>(tableDeck))}\n");
            }

            //порівняння комбінацій та визначення переможця
            var combinationsComparator = new CombinationsComparator();
            var winner = combinationsComparator.CompareCombinations(new List<Player>(players), new List<Card>(tableDeck));

            Console.WriteLine($"Winner: Player {players.IndexOf(winner) + 1}");
        }
    }
}
