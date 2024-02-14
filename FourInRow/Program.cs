using System;
using System.Runtime.CompilerServices;
using FourInRow;
using static FourInRow.GameController;

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
        GameUI.DisplayScreen(i_Game.BoardMatrix);
        if (!i_Game.GameMode)
        {
            Console.WriteLine("\nStart a two player game Press q whenever you want to quit.");
            while (true)
            {
                PlayMoveOffMode(ref i_Game);
            }
        }
        else
        {
            while (true)
            {
                Console.WriteLine("\nStart a one player game Press q whenever you want to quit.");
                PlayMoveOnMode(ref i_Game);
            }
        }

    }

    public static void PlayMoveOffMode(ref GameController i_Game)
    {
        int playerSign = 1;
        int oppositePlayerSign;
        int input;
        bool o_FullCapacity;
        while (playerSign <= 2)
        {
            input = 0;
            while (!i_Game.IsValidMakeMove(input, playerSign, out o_FullCapacity))
            {
                if (o_FullCapacity)
                {
                    Console.WriteLine("The column is full, choose another column.");
                }
                else
                {
                    Console.WriteLine($"Player {playerSign}, enter a column please: ");
                }
                input = GameUI.GetNumberInput(1, i_Game.BoardMatrix.GetLength(1));
            }
            input = 0;
            GameUI.DisplayScreen(i_Game.BoardMatrix);
            if (i_Game.IsEmptyBoardMatrix())
            {
                oppositePlayerSign = playerSign == 1 ? 2 : 1;
                Console.WriteLine($"Player {oppositePlayerSign} Win!\nState of record:\nPlayer 1 : {i_Game.Player1.Record}\tPlayer 2 : {i_Game.Player2.Record}");
                if (GameUI.AskForAnotherGame())
                {
                    PlayNewGame(ref i_Game);
                }
                else
                {
                    break;
                }
            }
            HandleGameOver(ref i_Game, playerSign);
            playerSign++;
        }
    }

    public static void HandleGameOver(ref GameController i_Game, int playerSign)
    {
        int gameOverSign = i_Game.IsGameOver(playerSign);
        bool askForAnotherGame = false;
        if (gameOverSign == 1)
        {
            Console.WriteLine($"Player {playerSign} Win!\nState of record:\nPlayer 1 : {i_Game.Player1.Record}\tPlayer 2 : {i_Game.Player2.Record}");
            askForAnotherGame = GameUI.AskForAnotherGame();
        }
        else if (gameOverSign == 2)
        {
            Console.WriteLine($"Nobody Win, State of record:\nPlayer 1 : {i_Game.Player1.Record}\tPlayer 2 : {i_Game.Player2.Record}");
            askForAnotherGame = GameUI.AskForAnotherGame();
        }
        if (askForAnotherGame)
        {
            GameUI.DisplayScreen(i_Game.BoardMatrix);
            PlayNewGame(ref i_Game);
        }
    }

    public static void PlayMoveOnMode(ref GameController i_Game)
    {
        int input = 0;
        bool o_FullCapacity;
        while (!i_Game.IsValidMakeMove(input, 1, out o_FullCapacity))
        {
            if (o_FullCapacity)
            {
                Console.WriteLine("The column is full, choose another column.");
            }
            else
            {
                Console.WriteLine("Enter a column please");
            }
            input = GameUI.GetNumberInput(1, i_Game.BoardMatrix.GetLength(1));
        }
        HandleGameOver(ref i_Game, 1);
        if (i_Game.IsEmptyBoardMatrix())
        {
            Console.WriteLine($"Computer Win!\nState of record:\nPlayer : {i_Game.Player1.Record}\tComputer : {i_Game.Player2.Record}");
            if (GameUI.AskForAnotherGame())
            {
                PlayNewGame(ref i_Game);
            }
            else
            {
                return;
            }
        }
        i_Game.MakeComputerMove();
        HandleGameOver(ref i_Game, 2);
        GameUI.DisplayScreen(i_Game.BoardMatrix);

    }
}