using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Blackjack
{
    public class Menu
    {
        public static Dictionary<string, Action> menuFunctions = new Dictionary<string, Action>()
        {
            {"Play", PlayBlackJack},
            {"Rules", Rules},
            {"Credits", Credits},
            {"Exit", Exit}
        };


        public static void Show()
        {
            string[] menuItems = { "Play", "Rules", "Credits", "Exit" };
            int pointerIndex = 0;

            while (true)
            {
                PrintMenu(menuItems, pointerIndex);

                ConsoleKeyInfo keyInfo = Console.ReadKey();

                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        pointerIndex = (pointerIndex - 1 + menuItems.Length) % menuItems.Length;
                        break;

                    case ConsoleKey.DownArrow:
                        pointerIndex = (pointerIndex + 1) % menuItems.Length;
                        break;

                    case ConsoleKey.Enter:
                        HandleMenuSelection(menuItems[pointerIndex]);
                        break;
                }
            }
        }

        static void PrintMenu(string[] menuItems, int pointerIndex)
        {
            Console.Clear();

            for (int i = 0; i < menuItems.Length; i++)
            {
                if (i == pointerIndex)
                {
                    Console.WriteLine("-> " + menuItems[i]);
                }
                else
                {
                    Console.WriteLine("   " + menuItems[i]);
                }
            }
        }

        static void HandleMenuSelection(string selectedOption)
        {
            Console.Clear();
            if (menuFunctions.ContainsKey(selectedOption))
            {
                Action selectedAction = menuFunctions[selectedOption];
                selectedAction.Invoke();
            }
            else
            {
                Console.WriteLine("Invalid option selected.");
            }
        }
        static void Rules()
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "https://en.wikipedia.org/wiki/Blackjack#Rules_of_play_at_casinos",
                UseShellExecute = true
            });
        }

        static void Credits()
        {
            Console.WriteLine("Made by Mocbuilder (Mocbuilder Coding Creations)\nMy Links: https://linktr.ee/mocbuildercodingcreations");
            Console.ReadLine();
        }

        static void Exit()
        {
            Environment.Exit(0);
        }

        static void PlayBlackJack()
        {
            Game.StartBlackjack();
        }
    }
}
