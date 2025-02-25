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

        static void Output(Player player, Dealer dealer)
        {
            Console.Clear();

            Console.WriteLine("Dealer is showing:");
            CardDisplay.PrintCards(dealer.GetCards());
            Console.WriteLine("\n ----- \n");

            Console.WriteLine("You are showing:");
            foreach (Hand hand in player.GetHands())
            {
                CardDisplay.PrintCards(hand.GetCards());
                Console.WriteLine("-----");
            }
        }

        static void StartGame()
        {
            Player player = new Player(deck, 100.00f); // TODO MONEYYYYY
            Dealer dealer = new Dealer(deck);

            player.Hit();
            dealer.Hit();
            player.Hit();

            while (player.IsInPlay())
            {
                Output(player, dealer);
                player.Turn();
            }

            dealer.Turn();
            Output(player, dealer);
        }

        static void Main(string[] args)
        {
            while (true)
            {
                StartGame();
            }
        }
    }
}
