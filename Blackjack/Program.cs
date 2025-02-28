using Blackjack.Classes;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    internal class Program
    {

        static Deck deck = new Deck(3);
        enum GAME_RESULT
        {
            PLAYER_WINS,
            PLAYER_LOSES,
            PLAYER_DRAWS,
            PLAYER_BLACKJACK
        }
        static Dictionary<GAME_RESULT, double> payouts = new Dictionary<GAME_RESULT, double>
        {
            { GAME_RESULT.PLAYER_WINS, 2 },
            { GAME_RESULT.PLAYER_LOSES, 0 },
            { GAME_RESULT.PLAYER_DRAWS, 1 },
            { GAME_RESULT.PLAYER_BLACKJACK, 2.5 } // 3:2
        };


        static void Output(Player player, Dealer dealer, double balance, double bet)
        {
            Console.Clear();

            Console.WriteLine($"Current balance: £{balance}");

            List<Hand> playerHands = player.GetHands();
            if (playerHands.Count > 1)
            {
                Console.WriteLine($"Each hand worth: £{bet}");
                Console.WriteLine($"Total hands worth: £{bet * playerHands.Count}");
            } else
            {
                Console.WriteLine($"Hand worth: £{bet}");
            }

            List<Card> dealerCards = dealer.GetCards();
            if (player.IsInPlay())
            {
                // Hide the dealer's 2nd card
                dealerCards = new List<Card> { dealerCards[0] };
            }

            Console.WriteLine("Dealer is showing:");
            CardDisplay.PrintCards(dealer.GetCards());
            Console.WriteLine($"Hand value: {dealer.GetHandValue()}");
            Console.WriteLine("\n ----- \n");

            Console.WriteLine("You are showing:");
            foreach (Hand hand in playerHands)
            {
                CardDisplay.PrintCards(hand.GetCards());
                Console.WriteLine($"Hand value: {hand.GetHandValue()}");
                Console.WriteLine("-----");
            }
        }

        public static double GetBet(double balance)
        {
            double bet = 0;

            while (bet <= 0 || bet > balance)
            {

                bet = Inputs.Float("Enter bet: ", 0f);

                if (bet > balance)
                {
                    Console.WriteLine("Too high of a bet, you don't have enough balance.");
                }

                if (bet <= 0)
                {
                    Console.WriteLine("Too low of a bet, you can't bet £0.00 or below");
                }
            }

            return bet;
        }

        static List<GAME_RESULT> StartGame(double balance, double bet)
        {
            Player player = new Player(deck, bet, balance);
            Dealer dealer = new Dealer(deck);

            player.Hit();
            dealer.Hit();
            player.Hit();
            dealer.Hit();

            if (dealer.GetHandStatus() == Hand.HAND_STATUS.BLACKJACK)
            {
                // Dealer Blackjack!
                Console.WriteLine("The dealer hit a blackjack, oopsies!");
                return new List<GAME_RESULT> { GAME_RESULT.PLAYER_LOSES };
            }

            if (player.GetCurrentHand().GetHandStatus() == Hand.HAND_STATUS.BLACKJACK)
            {
                // Blackjack!
                Console.WriteLine("You hit a blackjack, congrats!");
                return new List<GAME_RESULT> { GAME_RESULT.PLAYER_BLACKJACK };
            }

            while (player.IsInPlay())
            {
                Output(player, dealer, balance, bet);
                player.Turn();
            }

            Output(player, dealer, balance, bet);

            // Check if all hands are bust
            bool AllBust = true;
            foreach (Hand playerHand in player.GetHands())
            {
                if (playerHand.GetHandStatus() != Hand.HAND_STATUS.BUST)
                {
                    AllBust = false;
                    break;
                }
            }
            if (!AllBust) // If not, the dealer takes a turn
            {
                dealer.Turn();
                Output(player, dealer, balance, bet);
            }

            List<GAME_RESULT> results = new List<GAME_RESULT> { };
            int dealerValue = dealer.GetHandValue();

            foreach (Hand playerHand in player.GetHands())
            {

                if (playerHand.GetHandStatus() == Hand.HAND_STATUS.BUST)
                {
                    results.Add(GAME_RESULT.PLAYER_LOSES);
                    continue;
                }

                if (playerHand.GetHandStatus() == Hand.HAND_STATUS.BLACKJACK)
                {
                    results.Add(GAME_RESULT.PLAYER_BLACKJACK);
                    continue;
                }


                if (dealer.GetHandStatus() == Hand.HAND_STATUS.BUST)
                {
                    results.Add(GAME_RESULT.PLAYER_WINS);
                    continue;
                }

                int playerValue = playerHand.GetHandValue();

                if (playerValue < dealerValue)
                {
                    results.Add(GAME_RESULT.PLAYER_LOSES);
                    continue;
                } else if (playerValue == dealerValue)
                {
                    results.Add(GAME_RESULT.PLAYER_DRAWS);
                    continue;
                } else
                {
                    results.Add(GAME_RESULT.PLAYER_WINS);
                    continue;
                }
            }

            return results;
        }

        static void Main(string[] args)
        {

            Console.Write("\x1b]2;fagjack\x07");
            Console.Write("\x1b]4;264;rgb:ffff/c8c8/e6e6\x1b\\\x1b[2;0;264,|");

            double balance = 100.00d;

            while (true)
            {

                Console.WriteLine($"You have £{balance}");

                if (balance <= 0)
                {
                    char playAgain = Inputs.Char("You busted out! Try again?").ToString().ToUpper()[0];

                    if (playAgain == 'Y')
                    {
                        balance = 100;
                        continue;
                    } else
                    {
                        Environment.Exit(0);
                    }
                }


                double bet = GetBet(balance);
                List<GAME_RESULT> gameResults = StartGame(balance-bet, bet);

                balance -= bet * gameResults.Count; // For splits
                int handNum = 1;
                foreach (GAME_RESULT gameResult in gameResults)
                {
                    double payout = payouts[gameResult];

                    if (gameResult == GAME_RESULT.PLAYER_WINS)
                    {
                        Console.WriteLine($"Hand {handNum} won! Returning £{bet * payout}");
                    }
                    else if (gameResult == GAME_RESULT.PLAYER_LOSES)
                    {
                        Console.WriteLine($"Hand {handNum} lost! Returning £{bet * payout}");
                    } else if (gameResult == GAME_RESULT.PLAYER_DRAWS)
                    {
                        Console.WriteLine($"Hand {handNum} push! Returning £{bet * payout}");
                    } else if (gameResult == GAME_RESULT.PLAYER_BLACKJACK)
                    {
                        Console.WriteLine($"Hand {handNum} blackjack! Returning £{bet * payout}");
                    }

                    balance += bet * payout;
                }
            }
        }
    }
}
