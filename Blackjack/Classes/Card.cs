using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack.Classes
{
    public class Card
    {
        private const int ACE = 1;

        private int suit;
        private int value;
        private int gameValue;

        public Card(int suit, int value)
        {
            this.suit = suit;
            this.value = value;

            this.gameValue = value < 11 ? value : (value == ACE ? 11 : 10); // Blackjack
        }

        public int GetValue()
        {
            return this.value;
        }
        public int GetGameValue(int currentHand)
        {
            return (this.value == ACE) && (currentHand + 11 > 21) ? 1 : this.gameValue;
        }
        public int GetSuit()
        {
            return this.suit;
        }

    }
}
