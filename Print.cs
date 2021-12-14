using System;
using System.Collections.Generic;

namespace Hangman
{
    public class Print
    {
        public static void Cells(char[] answer, Dictionary<char, bool> usedLetters)
        {
            Console.Clear();

            //array of numbers to numerate cells accoding to answer length
            int[] numbers = new int[answer.Length];
            for (int i = 0; i < numbers.Length; i++)
                numbers[i] = i + 1;

            Text("\n");

            foreach (var number in numbers)
            {
                if (number < 10 && number > 0) Text($"  {number} ");
                else Text($" {number} ");
            }
            Text("\n");

            for (int i = 0; i < answer.Length; i++)
                Text(" ___", ConsoleColor.DarkBlue);
            Text("\n");

            for (int i = 0; i < answer.Length; i++)
                Text("|   ", ConsoleColor.DarkBlue);
            Text("|\n", ConsoleColor.DarkBlue);

            foreach (var letter in answer)
            {
                Text($"|", ConsoleColor.DarkBlue);
                Text($" {letter} ");
            }
            Text("|\n", ConsoleColor.DarkBlue);

            for (int i = 0; i < answer.Length; i++)
                Text("|___", ConsoleColor.DarkBlue);
            Text("|\n\n", ConsoleColor.DarkBlue);

            foreach (var letter in usedLetters)
            {
                if (letter.Value)
                    Text($" {letter.Key}", ConsoleColor.DarkGreen);

                else
                    Text($" {letter.Key}", ConsoleColor.DarkRed);
            }

            Text($"\n{HangedMan(Program.tries)}");
        }

        public static string HangedMan(int tries)
        {
            string[] hangedMan =
            {            
                "    ______   \n" +
                "    |    |   \n" +
                "    |    O   \n" +
                "    |   /|\\ \n" +
                "    |    |   \n" +
                "    |   / \\ \n" +
                "    |        \n" +
                " ___|___     \n",

                "    ______   \n" +
                "    |    |   \n" +
                "    |    O   \n" +
                "    |   /|\\ \n" +
                "    |    |   \n" +
                "    |   /    \n" +
                "    |        \n" +
                " ___|___     \n",

                "    ______   \n" +
                "    |    |   \n" +
                "    |    O   \n" +
                "    |   /|\\ \n" +
                "    |    |   \n" +
                "    |        \n" +
                "    |        \n" +
                " ___|___     \n",

                "    ______   \n" +
                "    |    |   \n" +
                "    |    O   \n" +
                "    |   /|   \n" +
                "    |    |   \n" +
                "    |        \n" +
                "    |        \n" +
                " ___|___     \n",

                "    ______   \n" +
                "    |    |   \n" +
                "    |    O   \n" +
                "    |    |   \n" +
                "    |    |   \n" +
                "    |        \n" +
                "    |        \n" +
                " ___|___     \n",

                "    ______   \n" +
                "    |    |   \n" +
                "    |    O   \n" +
                "    |        \n" +
                "    |        \n" +
                "    |        \n" +
                "    |        \n" +
                " ___|___     \n",

                "    ______   \n" +
                "    |    |   \n" +
                "    |        \n" +
                "    |        \n" +
                "    |        \n" +
                "    |        \n" +
                "    |        \n" +
                " ___|___     \n"
            };

            return hangedMan[tries];
        }

        public static void Text(string text, ConsoleColor color = ConsoleColor.Black)
        {
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ForegroundColor = ConsoleColor.Black;
        }
    }
}
