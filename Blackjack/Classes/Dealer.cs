using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack.Classes
{
    public class Dealer
    {

        private Hand hand;
        private Deck deck;

        public Dealer(Deck deck)
        {
            this.deck = deck;
            this.hand = new Hand(deck);
        }
        public void Turn()
        {

            while (this.hand.GetHandValue() < 17) // Must stand on 17 (including soft)
            {
                this.Hit();
            }

        }

        public List<Card> GetCards()
        {
            return this.hand.GetCards();
        }

        public int GetHandValue()
        {
            return this.hand.GetHandValue();
        }

        public Hand.HAND_STATUS GetHandStatus()
        {
            return this.hand.GetHandStatus();
        }

        public void Hit()
        {
            this.hand.Hit();
        }
    }
}
