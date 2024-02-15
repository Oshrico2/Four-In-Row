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
            while (PlayMoveOffMode(ref i_Game)) ;
        }
        else
        {
            Console.WriteLine("\nStart a one player game Press q whenever you want to quit.");
            while (PlayMoveOnMode(ref i_Game)) ;
        }
        Console.WriteLine("Thank you for playing");
        Environment.Exit(1);
    }

    public static bool PlayMoveOffMode(ref GameController i_Game)
    {
        int playerSign = 1;
        int oppositePlayerSign;
        int input;
        bool playAnotherMove = true;
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
            if (i_Game.IsEmptyBoardMatrix()) //someone quit
            {
                oppositePlayerSign = playerSign == 1 ? 2 : 1;
                Console.WriteLine($"Player {oppositePlayerSign} Win!\nState of record:\nPlayer 1 : {i_Game.Player1.Record}\tPlayer 2 : {i_Game.Player2.Record}");
                if (GameUI.AskForAnotherGame())
                {
                    PlayNewGame(ref i_Game);
                }
                else
                {
                    playAnotherMove = false;
                    break;
                }
            }
            HandleGameOver(ref i_Game, playerSign);
            playerSign++;
        }
        return playAnotherMove;
    }

    public static bool PlayMoveOnMode(ref GameController i_Game)
    {
        int input = 0;
        bool playAnotherMove = true;
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
                playAnotherMove = false;
            }
        }
        if (playAnotherMove)
        {
            i_Game.MakeComputerMove();
            GameUI.DisplayScreen(i_Game.BoardMatrix);
            HandleGameOver(ref i_Game, 2);
        }

        return playAnotherMove;
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
}