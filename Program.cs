using System;
using System.IO;
using System.Threading;

namespace Hangman
{
    class Program
    {
        private static Random random = new Random();

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
                string secretWord = string.Empty;
                bool correctAnswer = false;
                int humanOrAI = default;
                int tries = 6;

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
                        Print.Cells(answer);
                        Print.Text(" Enter the letter: ");
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

                        if (tries != 0)
                        {
                            if (tries > 1)
                                Print.Text($" Wrong, try again ({tries} tries left)\n");

                            else if (tries == 1)
                                Print.Text(" Wrong, last try left!\n");

                            Print.HangedMan(tries);
                            Thread.Sleep(3000);
                        }

                        else
                        {
                            Print.Cells(answer);
                            Print.HangedMan(tries);
                            Print.Text($"\n\n You lose :( The secret word was: {secretWord}\n\n", ConsoleColor.DarkRed);
                        }
                    }

                    correctAnswer = new string(answer) == secretWord;
                    if (correctAnswer)
                    {
                        Print.Cells(answer);
                        Print.Text($" You win :) The secret word was: {secretWord}\n\n", ConsoleColor.DarkGreen);
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
