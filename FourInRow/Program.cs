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

        PlayNewGame(ref game);
    }

    public static void PlayNewGame(ref GameController i_Game)
    {
        if (!i_Game.GameMode)
        {
            while (true)
            {
                PlayMoveOffMode(ref i_Game);
            }        
        }
        else
        {
            while (true)
            {
                PlayMoveOnMode(ref i_Game);
            }
        }
        
    }

    public static void PlayMoveOffMode(ref GameController i_Game)
    {
        int playerSign = 1;
        int input;
        while (playerSign <= 2)
        {
            input = 0;
            while (!i_Game.IsValidMove(input, playerSign))
            {
                Console.WriteLine($"Player{playerSign}, enter a column please: ");
                input = GameUI.GetNumberInput(1, i_Game.BoardMatrix.GetLength(1));
            }
            input = 0;
            GameUI.DisplayScreen(i_Game.BoardMatrix);
            playerSign++;
        }
    }

    public static void PlayMoveOnMode(ref GameController i_Game)
    {
        int input = 0;
        while (!i_Game.IsValidMove(input, 1))
        {
            Console.WriteLine("Enter a column please");
            input = GameUI.GetNumberInput(1, i_Game.BoardMatrix.GetLength(1));
        }

        i_Game.MakeComputerMove();
        GameUI.DisplayScreen(i_Game.BoardMatrix);

    }
}