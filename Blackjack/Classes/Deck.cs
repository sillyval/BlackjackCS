using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack.Classes
{
    public class Deck
    {

        const int NUM_SUITS = 4;
        const int NUM_VALUES = 13;

        private int numDecks;
        private int deckSize;
        private Card[] deck;

        // keeping track of popping
        private int front;
        private int back;

        public Deck(int numDecks)
        {
            this.numDecks = numDecks;
            this.deckSize = 52 * this.numDecks;
            this.deck = new Card[this.deckSize];

            this.front = 0;
            this.back = this.deckSize - 1;

            // Populate
            for (int deckIndex = 0; deckIndex < numDecks; deckIndex++)
            {
                for (int suit = 0; suit < NUM_SUITS; suit++)
                {
                    for (int value = 1; value <= NUM_VALUES; value++)
                    {
                        int index = (deckIndex * 52) + (suit * 13) + (value - 1);
                        this.deck[index] = new Card(suit, value);
                    }
                }
            }

            // Shuffle
            this.Shuffle();
        }

        public void Shuffle()
        {
            Random random = new Random();
            int numShuffles = random.Next(5, 10);

            for (int _ = 0; _ < numShuffles; _++)
            {
                for (int i = this.deckSize - 1; i > 0; i--)
                {
                    int j = random.Next(i + 1);
                    Card temp = this.deck[i];
                    this.deck[i] = this.deck[j];
                    this.deck[j] = temp;
                }
            }
        }
    
        public Card Pop()
        {
            Card card = this.deck[this.front];
            this.front++;

            if (this.front == this.back)
            {
                // Reset the deck
                this.front = 0;
                this.Shuffle();
            }

            return card;
        }

        public Card Peek()
        {
            Card card = this.deck[this.front];
            return card;
        }
    }
}
