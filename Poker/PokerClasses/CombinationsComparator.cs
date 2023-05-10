using System.Collections.Generic;
using System.Linq;

namespace Poker.PokerClasses
{
    public class CombinationsComparator
    {
        public Player CompareCombinations(List<Player> players, List<Card> tableDeck)
        {
            Dictionary<Player, int> playersCombinations = new Dictionary<Player, int>();

            foreach (var player in players)
                playersCombinations.Add(player, (int)player.GetCombination(new List<Card>(tableDeck)));//перетворюємо список гравців
                                                                  //на словник Гравець - комбінація(щоб не брати її декілька разів)

            var highestCombinationsPlayers = GetPlayersWithHighestCombination(playersCombinations);//беремо гравців з найвищчою
                                                                                                   //комбінацією

            if (highestCombinationsPlayers.Count == 1)
                return highestCombinationsPlayers[0];//якщо гравець один, то повевртаємо переможця
            else
            {
                var playerWithHighestCard = highestCombinationsPlayers[0];

                for (int i = 1; i < highestCombinationsPlayers.Count; i++)
                {
                    var card = (int)highestCombinationsPlayers[i].GetHighCardFromHand().Value;

                    if (card >= (int)playerWithHighestCard.GetHighCardFromHand().Value)
                        playerWithHighestCard = highestCombinationsPlayers[i];
                }

                return playerWithHighestCard;
            }
        }//якщо не один, значить у них однакові комбінації, повертаємо гравця з старшою картою

        public List<Player> GetPlayersWithHighestCombination(Dictionary<Player, int> playersWithCombination)//повертання гравців з
                                                                                                    //самими високими комбінаціями
        {
            var sorted = playersWithCombination.OrderBy(player => player.Value);//сортуємо по комбінаціям

            var highestCombination = sorted.ElementAt(sorted.Count() - 1).Value;//беремо найвищу комбінацію

            List<Player> playersWithHighestCombination = new List<Player>();//створюємо список з гравцями з найвищими комбінаціями

            foreach (var player in sorted)
                if (player.Value == highestCombination)
                    playersWithHighestCombination.Add(player.Key);//додаємо гравців з найвижчою комбінацією до списку

            return playersWithHighestCombination;
        }
    }
}
