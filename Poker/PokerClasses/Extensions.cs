namespace Poker.Extensions
{
    public static class Extensions//клас розширення
    {
        public static int Clamp(this int value, int min, int max)//метод який повертає максимальне або мінімальне значення,
                                                                 //якщо вводиме значення вийшло за його кордони
        {
            if (value < min)
                return min;
            else if (value > max)
                return max;
            else
                return value;
        }
    }
}
