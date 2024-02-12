using System;
using FourInRow;

public class Program
{
    public static void Main()
    {
        RunProgram();
        
    }

    public static void RunProgram()
    {
        Console.WriteLine("Welcome to Four in a Row!");
        int rows, cols;
        GameUI.GetBoardSize(out rows, out cols);
        bool playAgainstComputer = GameUI.ChooseOpponent();
    }
}