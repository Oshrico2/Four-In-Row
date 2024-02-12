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
        int rows, cols;
        Console.WriteLine("Welcome to Four in a Row!");
        GameUI.GetBoardSize(out rows, out cols);
        bool playAgainstComputer = GameUI.ChooseOpponent();

        GameController game = new GameController(rows, cols, playAgainstComputer);

        GameUI.DisplayScreen(game.BoardMatrix);

    }
}