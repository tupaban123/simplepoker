using System.Collections.Generic;

namespace Poker.PokerClasses
{
    public class CombinationFinder
    {
        public Combinations CalculateCombination(List<Card> cards)
        {
            var sortedCouples = SortToCouples(cards);//сортуємо карти до кучок

            //перевіряємо комбінацию та повертаємо її
            if (IsRoyalFlush(new List<Card>(cards)))
                return Combinations.RoyalFlush;
            else if (IsStraightFlush(new List<Card>(cards)))
                return Combinations.StraightFlush;
            else if (IsFlush(new List<Card>(cards)))
                return Combinations.Flush;
            else if (IsStraight(new List<Card>(cards)))
                return Combinations.Straight;
            else if (IsFourOfAKind(new List<Couple>(sortedCouples)))
                return Combinations.FourOfAKind;
            else if (IsFullHouse(new List<Couple>(sortedCouples)))
                return Combinations.FullHouse;
            else if (IsThreeOfAKind(new List<Couple>(sortedCouples)))
                return Combinations.ThreeOfAKind;
            else if (IsTwoPairs(new List<Couple>(sortedCouples)))
                return Combinations.TwoPair;
            else if (IsPair(new List<Couple>(sortedCouples)))
                return Combinations.Pair;
            else
                return Combinations.HighCard;
        }

        private List<Couple> SortToCouples(List<Card> cards)
        {
            cards = SortCardsByValue(cards);//сортуємо карти за значенням

            List<Couple> couples = new List<Couple>();//створюємо список с кучками
            var lastAddedCardIndex = -1;//значення останньої доданої карти

            foreach (var card in cards)//перебираємо кожну карту
            {
                var cardIndex = (int)card.Value;//беремо значення карти конвертоване в інт

                if (cardIndex == lastAddedCardIndex)
                {
                    couples[couples.Count - 1].cards.Add(card);
                }//якщо значення поточної карти == значення останньої доданої карти, добавляємо до останньої созданої купи
                else
                {
                    couples.Add(new Couple(new List<Card>
                    {
                        card
                    }));
                }// якщо ні, то створюємо нову купу, додаємо до неї карту
                lastAddedCardIndex = cardIndex;//міняємо зміну яка зберігає значення останньої доданної карти
            }

            List<Couple> couplesToDelete = new List<Couple>();//створюємо список з купами, які ми будемо видаляти

            foreach (var couple in couples)
            {
                if (couple.cards.Count < 2)
                    couplesToDelete.Add(couple);
            }//перебираємо кожну купу, якщо в купі менше двух карт, додаємо її до списку з купами, що треба видалити

            foreach (var couple in couplesToDelete)
                couples.Remove(couple);//видаляємо потрібні купи

            return couples;
        }//повертаємо купи

        private List<Card> SortCardsByValue(List<Card> cards)
        {
            for (int i = 0; i < cards.Count; i++)
            {
                for (int a = 0; a < cards.Count - 1; a++)
                {
                    var cardValueIndex = (int)cards[a].Value;
                    var nextCardValueIndex = (int)cards[a + 1].Value;
                    if (cardValueIndex > nextCardValueIndex)
                    {
                        var temp = cards[a + 1];
                        cards[a + 1] = cards[a];
                        cards[a] = temp;
                    }
                }
            }

            return cards;
        }//перетворити сортовані кучки на один список

        private List<Card> SortCardsBySuit(List<Card> cards)
        {
            for (int i = 0; i < cards.Count; i++)
            {
                for (int a = 0; a < cards.Count - 1; a++)
                {
                    var cardValueIndex = (int)cards[a].Suit;
                    var nextCardValueIndex = (int)cards[a + 1].Suit;
                    if (cardValueIndex > nextCardValueIndex)
                    {
                        var temp = cards[a + 1];
                        cards[a + 1] = cards[a];
                        cards[a] = temp;
                    }
                }
            }

            return cards;
        }//сортування списку з картами у декілька кучок

        public Card GetHighCard(List<Card> hand)
        {
            Card highCard = hand[0];

            for (int i = 1; i < hand.Count; i++)
            {
                var card = hand[i];

                var highCardValue = (int)highCard.Value;
                var cardValue = (int)card.Value;

                if (cardValue > highCardValue)
                    highCard = card;
            }

            return highCard;
        }//повернути старшу карту

        private bool IsPair(List<Couple> couples)// перевірка чи дійсна пара
        {
            var combinationCouple = GetPair(couples);

            return combinationCouple.cards.Count > 0;
        }

        private Couple GetPair(List<Couple> couples)//взяти найближчу пару з відсортованих кучок
        {
            foreach (var couple in couples)
            {
                if (couple.cards.Count == 2)
                    return couple;
            }

            return new Couple(new List<Card>());
        }

        private bool IsTwoPairs(List<Couple> couples)//перевірка чи дійсні дві пари
        {
            var combinationCouples = GetTwoPairs(couples);

            foreach (var couple in combinationCouples)
                if (couple.cards.Count < 2)
                    return false;

            return true;
        }

        private List<Couple> GetTwoPairs(List<Couple> couples)//взяти дві найближчі пари з відсортованих кучок
        {
            var firstPair = GetPair(couples);

            if (firstPair.cards.Count > 0)
                couples.Remove(firstPair);
            
            var secondPair = GetPair(couples);

            return new List<Couple> { firstPair, secondPair };
        }

        private bool IsThreeOfAKind(List<Couple> couples)// перевірка чи дійсні три найближчі однакові карти
        {
            return GetThreeOfAKind(couples).cards.Count > 0;
        }

        private Couple GetThreeOfAKind(List<Couple> couples) //взяти три однакові карти с відсортованих кучок
        {
            foreach(var couple in couples)
            {
                if (couple.cards.Count == 3)
                    return couple;
            }

            return new Couple(new List<Card>());
        }

        private bool IsFourOfAKind(List<Couple> couples)// перевірка чи дійсні три найближчі однакові карти
        {
            return GetFourOfAKind(couples).cards.Count > 0;
        }

        private Couple GetFourOfAKind(List<Couple> couples) //взяти три однакові карти с відсортованих кучок
        {
            foreach (var couple in couples)
            {
                if (couple.cards.Count == 4)
                    return couple;
            }

            return new Couple(new List<Card>());
        }

        private bool IsFullHouse(List<Couple> couples) //перевірка чи дійсний фулл хаус
        {
            var combinationCouples = GetFullHouse(couples);

            foreach (var couple in combinationCouples)
                if (couple.cards.Count < 2)
                    return false;

            return true;
        }

        private List<Couple> GetFullHouse(List<Couple> couples) //взяти фулл хаус з відсортованих кучок
        {
            var pair = GetPair(couples);

            if (pair.cards.Count > 0)
                couples.Remove(pair);

            var threeOfAKind = GetThreeOfAKind(couples);

            return new List<Couple> { pair, threeOfAKind };
        }

        private bool IsStraight(List<Card> cards)
        {
            var sortedCardsByValue = SortCardsByValue(new List<Card>(cards));

            var lastCardIndex = -1;
            var iterationsCount = 5;

            for(int i = 0; i < iterationsCount; i++)
            {
                var card = sortedCardsByValue[i];
                var cardValue = (int)card.Value;

                if (lastCardIndex == cardValue)
                {
                    iterationsCount++;
                    continue;
                } 
                else if (lastCardIndex == cardValue - 1 || lastCardIndex == -1)
                {
                    lastCardIndex = cardValue;
                    continue;
                }
                else
                    return false;
            }

            return true;
        }//перевірити карти на стріт
        
        private bool IsFlush(List<Card> cards)
        {
            var sortedCardsBySuits = SortCardsBySuit(new List<Card>(cards));

            var lastCardSuitIndex = -1;
            var cardInRowCount = 0;

            foreach (var card in sortedCardsBySuits)
            {
                var cardSuitIndex = (int)card.Suit;

                if (cardSuitIndex == lastCardSuitIndex || lastCardSuitIndex == -1)
                {
                    cardInRowCount++;
                    lastCardSuitIndex = cardSuitIndex;

                    if (cardInRowCount == 5)
                        return true;
                }
                else
                {
                    lastCardSuitIndex = cardSuitIndex;
                    cardInRowCount = 1;
                }
            }

            return false;
        }//перевірити карти на флеш

        public bool IsStraightFlush(List<Card> cards)
        {
            return IsStraight(cards) && IsFlush(cards);
        }//перевірити карти на стріт флеш

        public bool IsRoyal(List<Card> cards)
        {
            var couple = new Couple(cards);

            return couple.CheckCard(CardValue.Ace) && couple.CheckCard(CardValue.King) && couple.CheckCard(CardValue.Queen)
                && couple.CheckCard(CardValue.Jack) && couple.CheckCard(CardValue.Ten);
        }//перевірити карти на "роял"

        public bool IsRoyalFlush(List<Card> cards)
        {
            return IsRoyal(cards) && IsFlush(cards);
        }//перевірити карти на флеш роял
    }
}
