using Les1OpdrachtTelefoons.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;

namespace Les1OpdrachtTelefoons
{
    class Program
    {
        static List<Phone> phoneList = InMemoryData.phoneList;

        static void Main(string[] args)
        {
            MainMenu();
        }

        static void MainMenu()
        {
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WindowWidth = 60;
            Console.BufferWidth = 60;

            Console.WriteLine("Beschikbare telefoons. (Kies nummer voor meer informatie...)");
            createBreakLines();

            for (int i = 0; i < phoneList.Count; i++)
            {
                Console.WriteLine($"{i + 1} - {phoneList[i].Brand} {phoneList[i].Type}");
            }

            createBreakLines();

            GetUserInput();
        }



        static void GetUserInput()
        {
            int userInput;
            
            while (!int.TryParse(Console.ReadKey().KeyChar.ToString(), out userInput))
            {
                string message = "Voer een juist nummer in!";
                OutputError(message);
            }

            if (userInput > phoneList.Count || userInput < 1)
            {
                string message = "Telefoon bestaat niet!";
                OutputError(message);
                GetUserInput();
            }

            showPhoneDetails(userInput-1);
        }

        static void showPhoneDetails(int phoneNumber)
        {
            Console.OutputEncoding = Encoding.UTF8;
            CultureInfo culture = new CultureInfo("nl-NL"); 
            Console.Clear();

            Console.WriteLine($"Merk: {phoneList[phoneNumber].Brand} | Type: {phoneList[phoneNumber].Type} | " +
                $"Prijs: {phoneList[phoneNumber].Price:C2}");
            createBreakLines();

            Console.WriteLine($"\nBeschrijving:");
            createBreakLines();

            string description = phoneList[phoneNumber].Description;

            foreach (string substring in SplitSentence(description, 30))
            {
                Console.WriteLine(substring);
            }

            createBreakLines();
            Console.WriteLine("\n\nDruk een willekeurige toets om terug te keren...");

            Console.ReadKey();
            Console.Clear();

            MainMenu();
        }

        static void OutputError(string message)
        {
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(message.PadLeft(message.Length + 5));
            Console.SetCursorPosition(0, Console.CursorTop);
        }

        static List<string> SplitSentence(string sentence, int chunkLength)
        {
            List<string> wordsList = new();

            StringBuilder sb = new();

            string[] wordsArray = sentence.Split(); // Alle losse woorden in Array

            for (int i = 0; i < wordsArray.Length; i++) // loop door alle woorden in array
            {
                if (sb.Length > chunkLength) // als string groter dan chunk, ga naar volgende index en voeg toe aan SB
                {
                    wordsList.Add(sb.ToString()); // voeg sb toe aan list
                    sb.Clear(); // clear sb voor nieuwe ronde
                }

                sb.Append($"{wordsArray[i]} "); // voeg woord toe aan sb

                if (i == wordsArray.Length-1) // laatste woord
                    wordsList.Add(sb.ToString()); // voeg laatste stukje sb toe aan list
            }
            return wordsList;
        }

        static void createBreakLines()
        {
            Console.WriteLine(new string('-', Console.BufferWidth));
        }
    }
}
