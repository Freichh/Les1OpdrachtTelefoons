using Les1OpdrachtTelefoons.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Les1OpdrachtTelefoons
{
    class Program
    {
        readonly static List<Phone> phoneList = InMemoryData.GetList();

        static void Main(string[] args)
        {
            _ = args;
            MainMenu();
        }

        static void MainMenu()
        {
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WindowWidth = 60;
            Console.BufferWidth = 60;

            Console.WriteLine("Beschikbare telefoons. (Kies nummer voor meer informatie...)");
            CreateBreakLines();

            for (int i = 0; i < phoneList.Count; i++)
            {
                Console.WriteLine($"{i + 1} - {phoneList[i].Brand} {phoneList[i].Type}");
            }

            CreateBreakLines();
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
            // TODO: check op id of bestaat
            //if (phoneList.Where(x => phoneList.Contains(userInput)))
            //{

            //}

            if (userInput > phoneList.Count || userInput < 1)
            {
                string message = "Telefoon bestaat niet!";
                OutputError(message);
                GetUserInput();
            }

            ShowPhoneDetails(userInput);
        }

        static void ShowPhoneDetails(int phoneNumber)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.Clear();

            Phone phone = phoneList.FirstOrDefault(x => x.Id == phoneNumber);
            
            Console.WriteLine($"Merk: {phone.Brand} | Type: {phone.Type} | Prijs: {phone.Price:C2}");
            CreateBreakLines();

            Console.WriteLine($"\nBeschrijving:");
            CreateBreakLines();

            foreach (string substring in SplitSentence(phone.Description, 30))
                Console.WriteLine(substring);

            CreateBreakLines();
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
            
            string[] wordsArray = sentence.Split(); // alle losse woorden in Array

            for (int i = 0; i < wordsArray.Length; i++) // loop door alle woorden in array
            {
                if (sb.Length > chunkLength) // als StringBuilder groter dan chunk, voeg SB toe aan List en leeg SB
                {
                    wordsList.Add(sb.ToString()); // voeg sb toe aan list
                    sb.Clear(); // clear sb voor nieuwe ronde
                }

                sb.Append($"{wordsArray[i]} "); // voeg woord toe aan SB

                if (i == wordsArray.Length-1) // als laatste woord
                    wordsList.Add(sb.ToString()); // voeg laatste stukje sb toe aan list
            }
            return wordsList;
        }

        static void CreateBreakLines()
        {
            Console.WriteLine(new string('-', Console.BufferWidth));
        }
    }
}
