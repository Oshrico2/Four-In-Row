﻿using System;
using System.Text;
using Ex02.ConsoleUtils;

namespace FourInRow.UI
{
    public class GameUI
    {
        public static void GetBoardSize(out int i_Rows, out int i_Cols)
        {
            Console.WriteLine("Enter the number of rows (min 4, max 8): ");
            i_Rows = GetNumberInput(4, 8);

            Console.WriteLine("Enter the number of columns (min 4, max 8): ");
            i_Cols = GetNumberInput(4, 8);
        }

        public static int GetNumberInput(int i_Min, int i_Max)
        {
            int input;
            string inputStr;

            while (!int.TryParse(inputStr = Console.ReadLine(), out input) || input < i_Min || input > i_Max)
            {
                if (inputStr.ToLower() == "q")
                {
                    input = -1;
                    break;
                }

                Console.WriteLine($"Please enter a number between {i_Min} and {i_Max}: ");
            }

            return input;
        }

        public static bool ChooseOpponent()
        {
            string input;

            Console.WriteLine("Do you want to play against the computer? (Y/Any other key)");
            input = Console.ReadLine().ToUpper();

            return input == "Y";
        }

        public static void DisplayScreen(int[,] i_Matrix)
        {
            string equalSigns = new string('=', i_Matrix.GetLength(1) * 4 + 1);

            Screen.Clear();
            for (int i = 1; i <= i_Matrix.GetLength(1); i++)
            {
                if (i == 1)
                {
                    Console.Write("  1");
                    continue;
                }

                Console.Write($"   {i.ToString()}");
            }

            Console.WriteLine();
            for (int i = 0; i < i_Matrix.GetLength(0); i++)
            {
                for (int j = 0; j < i_Matrix.GetLength(1); j++)
                {
                    Console.Write("| " + (i_Matrix[i, j] == 0 ? " " : i_Matrix[i, j] == 1 ? "X" : "O") + " ");
                }

                Console.WriteLine("|");
                Console.WriteLine(equalSigns);
            }

            Console.WriteLine();
        }

        public static bool AskForAnotherGame()
        {
            bool askForAnotherGame = false;
            string input;

            Console.WriteLine("Would you like to play another game? (Y/Any other key)");
            input = Console.ReadLine().ToUpper();

            if (input == "Y")
            {
                askForAnotherGame = true;
            }

            Screen.Clear();

            return askForAnotherGame;
        }
    }
}