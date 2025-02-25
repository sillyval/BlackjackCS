using System;
using System.Collections.Generic;
using System.Linq;

namespace Blackjack.Classes
{
    public class CardDisplay
    {
        private static readonly Dictionary<int, string> SuitMapping = new Dictionary<int, string>
        {
            { 0, "Hearts" },
            { 1, "Diamonds" },
            { 2, "Clubs" },
            { 3, "Spades" }
        };

        private static readonly Dictionary<string, string> SuitEmojis = new Dictionary<string, string>
        {
            { "Hearts", "♥" },
            { "Diamonds", "♦" },
            { "Clubs", "♣" },
            { "Spades", "♠" }
        };

        private static readonly Dictionary<int, string> CardValues = new Dictionary<int, string>
        {
            { 1,  "A" },
            { 10, "10" },
            { 11, "J" },
            { 12, "Q" },
            { 13, "K" }
        };

        private static readonly string CardTemplate =
            "┌────────┐\n" +
            "│ {0}   {1} │\n" +
            "│        │\n" +
            "│        │\n" +
            "│        │\n" +
            "│ {2}   {3} │\n" +
            "└────────┘";

        public static string GetCardDisplay(string suit, int? value)
        {
            if (suit == null || value == null)
            {
                return "┌────────┐\n" +
                       "│╱╲╱╲╱╲╱╲│\n" +
                       "│╲╱╲╱╲╱╲╱│\n" +
                       "│        │\n" +
                       "│╱╲╱╲╱╲╱╲│\n" +
                       "│╲╱╲╱╲╱╲╱│\n" +
                       "└────────┘";
            }

            string valueStr = CardValues.ContainsKey(value.Value) ? CardValues[value.Value] : value.Value.ToString();
            string suitEmoji = SuitEmojis[suit];

            return string.Format(CardTemplate, valueStr.PadRight(2), suitEmoji, suitEmoji, valueStr.PadLeft(2));
        }

        public static void PrintCards(List<Card> cards)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            List<string[]> cardLines = new List<string[]>();
            foreach (var card in cards)
            {
                string suit = SuitMapping[card.GetSuit()];
                cardLines.Add(GetCardDisplay(suit, card.GetValue()).Split('\n'));
            }
            if (cards.Count < 2)
            {
                cardLines.Add(GetCardDisplay(null, null).Split('\n'));
            }

            for (int i = 0; i < cardLines[0].Length; i++)
            {
                foreach (var cardLine in cardLines)
                {
                    Console.Write(cardLine[i] + "  ");
                }
                Console.WriteLine();
            }
            Console.ResetColor();
        }
    }
}
