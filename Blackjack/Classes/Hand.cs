using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack.Classes
{
    public class Hand
    {
        public enum HAND_STATUS
        {
            IN_PLAY,
            STANDING,
            BUST,
            BLACKJACK
        }

        private HAND_STATUS status;
        private List<Card> cards;
        private int handValue;
        private Deck deck;

        public Hand(Deck deck)
        {
            this.status = HAND_STATUS.IN_PLAY;
            this.deck = deck;
            this.cards = new List<Card>();
        }

        public void CalculateHandValue()
        {
            this.handValue = 0;
            foreach (Card card in this.cards)
            {
                this.handValue += card.GetGameValue(this.handValue);
            }
        }

        public List<Card> GetCards()
        {
            return this.cards;
        }

        public int GetHandValue()
        {
            this.CalculateHandValue();
            return this.handValue;
        }

        public HAND_STATUS GetHandStatus()
        {
            return this.status;
        }

        public bool IsInPlay()
        {
            return this.status == HAND_STATUS.IN_PLAY;
        }

        public void Hit()
        {
            if (this.status != HAND_STATUS.IN_PLAY)
            {
                return;
            }

            Card draw = this.deck.Pop();

            Console.WriteLine(draw);

            this.cards.Add(draw);

            this.CalculateHandValue();

            if (this.handValue > 21)
            {
                this.status = HAND_STATUS.BUST;
            } else if (this.handValue == 21)
            {
                this.status = HAND_STATUS.STANDING; // Auto stand
            }

        }
        public void Stand()
        {

            if (this.status == HAND_STATUS.BUST)
            {
                return;
            }

            this.status = HAND_STATUS.STANDING;
        }

        public bool CanSplit()
        {
            return (this.cards.Count == 2) && (this.cards[0].GetValue() == this.cards[1].GetValue());
        }

        public Hand Split()
        {
            if (!this.CanSplit())
            {
                return this;
            }

            Hand splitHand = new Hand(this.deck);
            Card splitCard = this.cards[1];
            splitHand.cards.Add(splitCard);
            this.cards.Remove(splitCard);

            this.Hit();
            splitHand.Hit();

            return splitHand;
        }

        public void DoubleDown()
        {
            this.Hit();

            if (this.status == HAND_STATUS.IN_PLAY) // They might have bust
            {
                this.status = HAND_STATUS.STANDING;
            }
        }
    }
}
