using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack
{
    public class Game
    {
        static Random random = new Random();
        static bool isBust = false;
        static bool isBlackjack = false;
        static int liveValue = 0;
        public static void StartBlackjack()
        {
            int startValue = 0;

            Print("TERMINAL-BLACKJACK", ConsoleColor.Blue);
            while (isBust == false && isBlackjack == false)
            {
                Print("Initial Dealing", ConsoleColor.Blue);
                startValue = DealPlayer(2);
                liveValue = startValue;
                BustOrBlackjack(liveValue);
                PlayerTurn(liveValue);
            }

            Console.WriteLine($"Bust! Your cards have a combined value of {startValue}\nYou lose!");
            Console.ReadLine();
        }

        static int DealPlayer(int amount)
        {
            int allCards = 0;

            for (int i = 0; i < amount; i++)
            {
                int newCard = random.Next(2, 10);
                Console.WriteLine($"You were dealt a card: {newCard}");
                allCards = allCards + newCard;
            }

            Console.WriteLine($"Your total card value is now: {allCards + liveValue}");
            liveValue = allCards + liveValue;
            return liveValue;
        }

        static void PlayerTurn(int dealtValue)
        {
            Print("Your Turn", ConsoleColor.Blue);

            bool wantsCard = true;

            while(wantsCard == true && isBust == false)
            {
                Console.WriteLine("Dou you want to hit or stand ?");
                string playerChoice = Console.ReadLine().ToLower();

                if (playerChoice == "hit")
                {
                    int newCard = DealPlayer(1);
                    liveValue = newCard;
                }

                if (playerChoice == "stand")
                {
                    wantsCard = false;
                }

                if (playerChoice != "hit" &&  playerChoice != "stand")
                {
                    Console.WriteLine("Invalid choice. Please try again.");
                }

                BustOrBlackjack(liveValue);
            }
            
        }

        static void BustOrBlackjack(int toCkeck)
        {
            if(toCkeck >= 22)
            {
                isBust = true;
                return;
            }

            if (toCkeck == 21)
            {
                isBlackjack = true;
                return;
            }
        }

        static void Print(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
