using System;
namespace FourInRow
{
	public class GameUI
	{
        static public void GetBoardSize(out int rows, out int cols)
        {
            Console.WriteLine("Enter the number of rows (min 4, max 8): ");
            rows = GetNumberInput(4, 8);

            Console.WriteLine("Enter the number of columns (min 4, max 8): ");
            cols = GetNumberInput(4, 8);
        }

        static private int GetNumberInput(int min, int max)
        {
            int input;
            while (!int.TryParse(Console.ReadLine(), out input) || input < min || input > max)
            {
                Console.WriteLine($"Please enter a number between {min} and {max}: ");
            }
            return input;
        }

        static public bool ChooseOpponent()
        {
            Console.WriteLine("Do you want to play against the computer? (Y/N)");
            string input = Console.ReadLine().ToUpper();
            return input == "Y";
        }
    }
}

