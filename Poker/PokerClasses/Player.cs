using System.Collections.Generic;

namespace Poker.PokerClasses
{
    public class Player // гравець
    {
        public List<Card> hand { get; private set; } = new List<Card>(); //"рука" гравця(карти, що йому видали)

        public Player(List<Card> cards) //конструктор, в якому ініціалізуємо його руку
        {
            hand = cards;
        }

        public Card GetHighCardFromHand() //беремо старшу карту з його руки за допомогою класу CombinationFinder
        {
            var combFinder = new CombinationFinder();
            
            return combFinder.GetHighCard(hand);
        }

        public Combinations GetCombination(List<Card> tableDeck) //метод для отримання комбінації з списком карт на столі 
        {
            var combinationFinder = new CombinationFinder(); //створюємо об'єкт для просчитування комбінації

            foreach (var card in hand)
                tableDeck.Add(card); //об'єднуємо руку та карти зі стола в один список

            return combinationFinder.CalculateCombination(tableDeck); //повертаємо комбінацію
        }
    }
}
