using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack.Classes
{
    public class Player
    {

        private List<Hand> hands;
        private Deck deck;

        private int currentHandIndex = 0;
        private bool inPlay;
        private double bet;
        private double balance;

        public Player(Deck deck, double bet, double balance)
        {
            this.deck = deck;
            this.bet = bet;
            this.balance = balance;

            Hand initialHand = new Hand(deck);
            this.hands = new List<Hand>();
            this.hands.Add(initialHand);

            this.inPlay = true;
        }

        public bool IsInPlay()
        {
            return this.inPlay;
        }

        public List<Hand> GetHands()
        {
            return this.hands;
        }

        public Hand GetCurrentHand()
        {
            return this.hands[this.currentHandIndex];
        }

        public int GetCurrentHandIndex()
        {
            return this.currentHandIndex;
        }

        public int GetNumberOfHandsInPlay()
        {
            int handsInPlay = 0;

            foreach (Hand hand in this.hands) {
                if (hand.IsInPlay())
                {
                    handsInPlay++;
                }    
            }

            return handsInPlay;
        }
        public List<Hand> GetHandsInPlay()
        {
            List<Hand> handsInPlay = new List<Hand>();

            foreach (Hand hand in this.hands)
            {
                if (hand.IsInPlay())
                {
                    handsInPlay.Add(hand);
                }
            }

            return handsInPlay;
        }

        public void UpdateCurrentHand(Hand currentHand)
        {
            if (currentHand.IsInPlay())
            {
                return;
            }

            List<Hand> handsInPlay = this.GetHandsInPlay();

            if (handsInPlay.Count > 0)
            {
                Hand firstHand = handsInPlay[0];
                this.currentHandIndex = this.hands.IndexOf(firstHand);
            } else
            {
                this.inPlay = false;
                this.currentHandIndex = 0;
            }
        }
        public void UpdateCurrentHand()
        {
            Hand currentHand = this.hands[currentHandIndex];
            this.UpdateCurrentHand(currentHand);
        }

        public char[] GetOptions()
        {
            char[] options = new char[6];

            this.UpdateCurrentHand();

            if (this.inPlay)
            {
                options[0] = 'H'; // Hit
                options[1] = 'S'; // Stand

                if (this.bet <= this.balance) // remember, balance is decremented by the bet before this
                {
                    options[2] = 'D'; // Double down

                    if (this.hands[this.currentHandIndex].CanSplit())
                    {
                        options[3] = 'P'; // Split
                    }
                }
            }

            if (this.GetNumberOfHandsInPlay() > 1)
            {
                if (this.currentHandIndex > 0)
                {
                    options[4] = 'Q'; // Previous hand
                }

                if (this.currentHandIndex < this.hands.Count - 1)
                {
                    options[5] = 'E'; // Next hand
                }
            }

            return options;
        }

        public void Turn()
        {

            char[] options = this.GetOptions();

            char playerChoice = Inputs.Char("What will you do? ").ToString().ToUpper()[0];

            Console.WriteLine($"'{playerChoice}'");

            switch (playerChoice)
            {
                case 'H':
                    Console.WriteLine("Hitting!");
                    this.Hit();
                    break;
                case 'S':
                    Console.WriteLine("Standing!");
                    this.Stand();
                    break;
                case 'D':
                    Console.WriteLine("Doubling down!");
                    this.DoubleDown();
                    break;
                case 'P':
                    Console.WriteLine("Splitting!");
                    this.Split();
                    break;
                case 'Q':
                    Console.WriteLine("Previous Hand!");
                    this.PrevHand();
                    break;
                case 'E':
                    Console.WriteLine("Next Hand!");
                    this.NextHand();
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }

            this.UpdateCurrentHand();
        }

        public void Hit()
        {
            Hand hand = this.GetCurrentHand();

            hand.Hit();
        }
        public void Stand() {
            Hand hand = this.GetCurrentHand();

            hand.Stand();
        }
        public void DoubleDown()
        {
            Hand hand = this.GetCurrentHand();

            hand.DoubleDown();
        }
        public void Split()
        {
            Hand hand = this.GetCurrentHand();

            hand.Split();
        }

        public void PrevHand()
        {
            this.currentHandIndex--;
            this.UpdateCurrentHand();
        }
        public void NextHand()
        {
            this.currentHandIndex++;
            this.UpdateCurrentHand();
        }
    }
}
