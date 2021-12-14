using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace Hangman
{
    class Program
    {
        private static Random random = new Random();
        public static int tries;

        static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Black;

            //adding a file with words
            string[] libraryOfWords = File.ReadAllLines(@"C:\docs\code\Hangman\libraryOfWords.txt");
            string exitTheGame = string.Empty;

            while (exitTheGame != "n")
            {
                var usedLetters = new Dictionary<char, bool>();
                string secretWord = string.Empty;
                bool correctAnswer = false;
                int humanOrAI = default;
                tries = 6;

                while (humanOrAI != 1 && humanOrAI != 2)
                {
                    Console.Clear();
                    Print.Text(" [1] enter word manually\n" +
                               " [2] generate word automatically\n\n");
                    Print.Text(" make your choice: ");

                    int.TryParse(Console.ReadLine(), out humanOrAI);
                }

                if (humanOrAI == 1)
                {
                    Print.Text("\n you can enter a word in any language\n", ConsoleColor.DarkGreen);

                    while (secretWord.Length <= 1 || secretWord.Length > 25)
                    {
                        Print.Text(" input (no more than 25 symbols): ");
                        secretWord = Console.ReadLine().ToUpper();
                    }
                }

                else secretWord = libraryOfWords[random.Next(0, libraryOfWords.Length - 1)].ToUpper();

                //initializing an array for pasting correct symbols
                char[] answer = new char[secretWord.Length];
                for (int i = 0; i < answer.Length; i++)
                    answer[i] = ' ';

                //game will continue till the player has any tries left
                while (tries != 0 && !correctAnswer)
                {
                    bool letterIsFound = false;
                    bool charOrNot = false;
                    char letter = default;

                    //searching for letter or digit
                    while (!charOrNot)
                    {
                        Print.Cells(answer, usedLetters);
                        Print.Text($"\n Tries left: {tries}\n", ConsoleColor.DarkMagenta);
                        Print.Text("\n Enter the letter: ");
                        charOrNot = char.TryParse(Console.ReadLine().ToUpper(), out letter);
                    }

                    //pasting correct symbols
                    for (int i = 0; i < secretWord.Length; i++)
                    {
                        if (letter == secretWord[i])
                        {
                            letterIsFound = true;
                            answer[i] = secretWord[i];

                            if (!usedLetters.ContainsKey(letter))
                                usedLetters.Add(letter, true);
                        }
                    }

                    if (!letterIsFound && !usedLetters.ContainsKey(letter))
                    {
                        tries--;
                        usedLetters.Add(letter, false);

                        if (tries == 0)
                        {
                            Print.Cells(answer, usedLetters);
                            Print.Text($"\n YOU LOSE :( The secret word was: {secretWord}\n\n", ConsoleColor.DarkRed);
                        }
                    }

                    correctAnswer = new string(answer) == secretWord;
                    if (correctAnswer)
                    {
                        Print.Cells(answer, usedLetters);
                        Print.Text($"\n YOU WIN :)\n\n", ConsoleColor.DarkGreen);
                    }
                }

                do
                {
                    Print.Text(" Play again? [y] / [n]: ");
                    exitTheGame = Console.ReadLine().ToLower();
                }
                while (exitTheGame != "n" && exitTheGame != "y");
            }
        }
    }
}
