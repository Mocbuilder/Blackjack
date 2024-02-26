using System;
using System.Collections.Generic;

namespace Blackjack
{
    public class Game
    {
        static Random random = new Random();
        static bool isBust = false;
        static bool isBlackjack = false;
        static bool dealerBust = false;
        static bool dealerBlackjack = false;
        static bool dealerNeedsCard = true;
        static bool playerWin = false;
        static bool gameTie = false;
        static int playerTotalValue = 0;
        static int dealerTotalValue = 0;
        static List<int> deck = new List<int>();

        public static void StartBlackjack()
        {
            InitializeVariables();
            InitializeDeck();

            int startValue = 0;

            Print("TERMINAL-BLACKJACK", ConsoleColor.Blue);
            while (isBust == false && isBlackjack == false && playerWin == false && gameTie == false && dealerBlackjack == false && dealerBust == false)
            {
                Print("Initial Dealing", ConsoleColor.Blue);
                startValue = DealPlayer(2);
                playerTotalValue = startValue;
                BustOrBlackjack(playerTotalValue);
                PlayerTurn(playerTotalValue);
                DealersTurn();
                EndTurn(playerTotalValue, dealerTotalValue);
            }

            if(isBlackjack == true)
            {
                Print($"YOU WIN! YOU'RE CARDS HAVE A TOTAL VALUE OF {playerTotalValue} WHILE THE DEALER ONLY HAS {dealerTotalValue}!", ConsoleColor.Blue);
                Console.ReadLine();
            }

            if (gameTie == true)
            {
                Print($"TIE! YOU'RE AND THE DEALERS CARDS HAVE A TOTAL VALUE OF {playerTotalValue}!", ConsoleColor.Blue);
                Console.ReadLine();
            }

            if (playerWin == true)
            {
                Print("BLACKJACK! YOU WIN!", ConsoleColor.Blue);
                Console.ReadLine();
            }

            if (dealerBlackjack == true)
            {
                Print("YOU LOSE! THE DEALER HAS A BLACKJACK!", ConsoleColor.Blue);
                Console.ReadLine();
            }

            if (dealerBust == true)
            {
                Print($"YOU WIN! THE DEALER HAS BUST WITH A VALUE OF {dealerTotalValue}!", ConsoleColor.Blue);
                Console.ReadLine();
            }


            if (isBust == true)
            {
                Console.WriteLine($"BUST! YOUR CARDS HAVE A COMBINED VALUE OF {playerTotalValue}\nYOU LOSE!");
                Console.ReadLine();
            }

        }

        static void InitializeDeck()
        {
            //initialize deck with 4 of each card value
            for (int cardValue = 2; cardValue <= 11; cardValue++)
            {
                for (int cardCount = 0; cardCount < 4; cardCount++)
                {
                    deck.Add(cardValue);
                }
            }

            ShuffleDeck();
        }

        //shuffle with fisher-yates alg.
        static void ShuffleDeck()
        {
            for (int i = deck.Count - 1; i > 0; i--)
            {
                int j = random.Next(0, i + 1);
                int temp = deck[i];
                deck[i] = deck[j];
                deck[j] = temp;
            }
        }

        static int DealPlayer(int numberOfCards)
        {
            int playerHandValue = 0;

            for (int i = 0; i < numberOfCards; i++)
            {
                int newCard = deck[0]; // Get the top card from the deck
                deck.RemoveAt(0); // Remove the dealt card from the deck
                Console.WriteLine($"You were dealt a card: {newCard}");
                playerHandValue += newCard;
            }

            playerTotalValue += playerHandValue;
            Console.WriteLine($"Your total card value is now: {playerTotalValue}");
            return playerTotalValue;
        }

        static void PlayerTurn(int dealtValue)
        {
            Print("Your Turn", ConsoleColor.Blue);

            bool wantsCard = true;

            while (wantsCard == true && isBust == false)
            {
                Console.WriteLine("Do you want to hit or stand?");
                string playerChoice = Console.ReadLine().ToLower();

                if (playerChoice == "hit")
                {
                    int newCard = DealPlayer(1);
                    playerTotalValue = newCard;
                }

                if (playerChoice == "stand")
                {
                    wantsCard = false;
                }

                if (playerChoice != "hit" && playerChoice != "stand")
                {
                    Console.WriteLine("Invalid choice. Please try again.");
                }

                BustOrBlackjack(playerTotalValue);
            }

        }

        static void DealersTurn()
        {
            Print("Dealer's Turn", ConsoleColor.Blue);
            DealDealer(2);
            DealerBustOrBlackjack(dealerTotalValue);

            while (dealerBust == false && dealerBlackjack == false && dealerNeedsCard == true)
            {
                Console.WriteLine("Dealer has not yet reached a value of 17 and needs to hit again.");
                DealDealer(2);
                DealerBustOrBlackjack(dealerTotalValue);
            }

            if (!dealerBust)
            {
                Console.WriteLine($"The dealer has bust with a total value of {dealerTotalValue}");
                return;
            }
        }

        static int DealDealer(int numberOfCards)
        {
            int dealerHandValue = 0;

            for (int i = 0; i < numberOfCards; i++)
            {
                int newCard = deck[0]; //get top card from deck
                deck.RemoveAt(0); //remove dealt card
                Console.WriteLine($"The dealer was dealt a card: {newCard}");
                dealerHandValue += newCard;
            }

            dealerTotalValue += dealerHandValue;
            Console.WriteLine($"The dealers total card value is now: {dealerTotalValue}");
            return dealerTotalValue;
        }

        static void EndTurn(int playerValue, int dealerValue)
        {
            Print("Ending Turn", ConsoleColor.Blue);
            if (dealerValue > playerValue)
            {
                isBust = true;
                return;
            }

            if (dealerValue < playerValue)
            {
                playerWin = true;
                return;
            }

            if (dealerValue == playerValue)
            {
                gameTie = true;
                return;
            }

            Console.WriteLine("You are not supposed to see this text. \nIf you DO see it, something broke or has gone wrong. \nYou will no lose automatically.");
            Console.ReadLine();
            return;
        }

        static void BustOrBlackjack(int toCheck)
        {
            if (toCheck >= 22)
            {
                isBust = true;
                return;
            }

            if (toCheck == 21)
            {
                isBlackjack = true;
                return;
            }
        }

        static void DealerBustOrBlackjack(int toCheck)
        {
            if (toCheck >= 22)
            {
                dealerBust = true;
                return;
            }

            if (toCheck == 21)
            {
                dealerBlackjack = true;
                return;
            }

            if (toCheck >= 17)
            {
                dealerNeedsCard = false;
                Console.WriteLine($"The dealer must stand with a card value of {toCheck}");
                return;
            }
        }

        static void Print(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
        }

        static void InitializeVariables()
        {
            isBust = false;
            isBlackjack = false;
            dealerBust = false;
            dealerBlackjack = false;
            playerTotalValue = 0;
            dealerTotalValue = 0;
        }
    }
}
