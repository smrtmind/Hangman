using System;
using System.IO;
using System.Linq;
using System.Threading;

namespace Hangman
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Black;

            Random random = new Random();

            //adding a file with words
            string[] libraryOfWords = File.ReadAllLines(@"D:\Hangman\libraryOfWords.txt"); 
            string exitTheGame = string.Empty;
            int tries = 6;

            while (exitTheGame != "n")
            {
                string secretWord = string.Empty;
                exitTheGame = string.Empty;
                int humanOrAI = 0;

                while (humanOrAI != 1 && humanOrAI != 2)
                {
                    Console.Clear();
                    Print("[1] enter the word manually\n[2] generate word automatically\n\n");
                    Print("make your choice: ");
                    int.TryParse(Console.ReadLine(), out humanOrAI);

                    if (humanOrAI == 1)
                    {
                        Print("\nyou can enter a word in any language\n", ConsoleColor.DarkGreen);

                        while (secretWord.Length <= 1 || secretWord.Length > 25)
                        {
                            Print("input (no more than 25 symbols): ");
                            secretWord = Console.ReadLine().ToUpper();
                        }
                    }

                    else if (humanOrAI == 2)
                        secretWord = libraryOfWords[random.Next(0, libraryOfWords.Length - 1)].ToUpper();

                    Console.Clear();
                }

                //initializing an array for pasting correct symbols
                char[] answer = new char[secretWord.Length];
                for (int i = 0; i < answer.Length; i++)
                    answer[i] = ' ';

                //game will continue till the player has any tries left
                while (tries != 0)
                {
                    bool charOrNot = false;
                    bool letterIsFound = false;
                    char letter = ' ';

                    //searching for letter or digit
                    while (!charOrNot)
                    {
                        PrintCells(answer);
                        Print("Enter the letter: ");
                        charOrNot = char.TryParse(Console.ReadLine().ToUpper(), out letter);
                    }

                    //pasting correct symbols
                    for (int i = 0; i < secretWord.Length; i++)
                    {
                        if (letter == secretWord[i])
                        {
                            letterIsFound = true;
                            answer[i] = secretWord[i];
                        }
                    }

                    if (!letterIsFound)
                    {
                        tries--;

                        if (tries == 5)
                        {
                            Print($"Wrong, try again ({tries} tries left)\n");

                            Print("    ______   \n" +
                                  "    |    |   \n" +
                                  "    |    O   \n" +
                                  "    |        \n" +
                                  "    |        \n" +
                                  "    |        \n" +
                                  "    |        \n" +
                                  " ___|___");

                            Thread.Sleep(4000);
                        }

                        if (tries == 4)
                        {
                            Print($"Wrong, try again ({tries} tries left)\n");

                            Print("    ______   \n" +
                                  "    |    |   \n" +
                                  "    |    O   \n" +
                                  "    |    |   \n" +
                                  "    |    |   \n" +
                                  "    |        \n" +
                                  "    |        \n" +
                                  " ___|___");

                            Thread.Sleep(4000);
                        }

                        if (tries == 3)
                        {
                            Print($"Wrong, try again ({tries} tries left)\n");

                            Print("    ______   \n" +
                                  "    |    |   \n" +
                                  "    |    O   \n" +
                                  "    |   /|   \n" +
                                  "    |    |   \n" +
                                  "    |        \n" +
                                  "    |        \n" +
                                  " ___|___");

                            Thread.Sleep(4000);
                        }

                        if (tries == 2)
                        {
                            Print($"Wrong, try again ({tries} tries left)\n");

                            Print("    ______   \n" +
                                  "    |    |   \n" +
                                  "    |    O   \n" +
                                  "    |   /|\\ \n" +
                                  "    |    |   \n" +
                                  "    |        \n" +
                                  "    |        \n" +
                                  " ___|___");

                            Thread.Sleep(4000);
                        }

                        if (tries == 1)
                        {
                            Print("Wrong, last try left!\n");

                            Print("    ______   \n" +
                                  "    |    |   \n" +
                                  "    |    O   \n" +
                                  "    |   /|\\ \n" +
                                  "    |    |   \n" +
                                  "    |   /    \n" +
                                  "    |        \n" +
                                  " ___|___");

                            Thread.Sleep(4000);
                        }

                        if (tries == 0)
                        {
                            PrintCells(answer);

                            Print("    ______   \n" +
                                  "    |    |   \n" +
                                  "    |    O   \n" +
                                  "    |   /|\\ \n" +
                                  "    |    |   \n" +
                                  "    |   / \\ \n" +
                                  "    |        \n" +
                                  " ___|___");

                            Print($"\n\nYou lose :( The secret word was: {secretWord}\n", ConsoleColor.Red);
                            break;
                        }
                    }

                    string result = new string(answer);

                    if (result == secretWord)
                    {
                        PrintCells(answer);
                        Print($"You win :) The secret word was: {secretWord}\n", ConsoleColor.DarkGreen);
                        break;
                    }
                }

                Print("\n");
                while (exitTheGame != "n" && exitTheGame != "y")
                {
                    Print("Play again? [y] / [n]: ");
                    exitTheGame = Console.ReadLine();
                }
            }
        }
        public static void PrintCells(char[] answer)
        {
            //array of numbers to numerate cells accoding to answer length
            int[] numbers = new int[answer.Length];
            for (int i = 0; i < numbers.Length; i++)
                numbers[i] = i + 1;

            Console.Clear();
            Print("\n");

            foreach (var number in numbers)
            {
                if (number < 10 && number > 0) Print($"  {number} ");
                else Print($" {number} ");
            }
            Print("\n");

            for (int i = 0; i < answer.Length; i++) 
                Print(" ___", ConsoleColor.DarkBlue);
            Print("\n");

            for (int i = 0; i < answer.Length; i++) 
                Print("|   ", ConsoleColor.DarkBlue);
            Print("|\n", ConsoleColor.DarkBlue);

            foreach (var letter in answer)
            {
                Print($"|", ConsoleColor.DarkBlue);
                Print($" {letter} ");
            }
            Print("|\n", ConsoleColor.DarkBlue);

            for (int i = 0; i < answer.Length; i++) 
                Print("|___", ConsoleColor.DarkBlue);
            Print("|\n\n", ConsoleColor.DarkBlue);
        }

        public static void Print(string text, ConsoleColor color = ConsoleColor.Black)
        {
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ForegroundColor = ConsoleColor.Black;
        }
    }
}
