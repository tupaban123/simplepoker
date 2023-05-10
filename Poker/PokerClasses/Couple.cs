using System.Collections.Generic;

namespace Poker.PokerClasses
{
    public class Couple //клас для карток з одним і тим же значенням
    {
        public List<Card> cards = new List<Card>();

        public Couple(List<Card> cardsToInit) => cards = cardsToInit;

        public bool CheckCard(CardValue value)
        {
            foreach (var card in cards)
                if (card.Value == value)
                    return true;

            return false;
        }
    }
}
