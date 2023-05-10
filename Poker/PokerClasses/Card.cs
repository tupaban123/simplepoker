namespace Poker.PokerClasses
{
    public class Card //картка
    {
        public Suits Suit { get; private set; } //властивість з мастью
        public CardValue Value { get; private set; } //властивість з значенням

        public Card(Suits suit, CardValue value) //конструктор для побудови картки з мастью та значенням
        {
            this.Suit = suit;
            this.Value = value;
        }

        public string GetCardStr() => $"{Value} {Suit}";
    }
}
