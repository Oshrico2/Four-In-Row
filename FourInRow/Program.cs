using System;
using FourInRow.UI;
using FourInRow.Logic;

public class Program
{
    public static void Main()
    {
        RunProgram();
    }

    public static void RunProgram()
    {
        int rows, cols;
        bool playAgainstComputer;
        GameController game;

        Console.WriteLine("Welcome to Four in a Row!");
        GameUI.GetBoardSize(out rows, out cols);

        playAgainstComputer = GameUI.ChooseOpponent();
        game = new GameController(rows, cols, playAgainstComputer);
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
            Console.WriteLine("\nStart a one player game Press q whenever you want to quit.");
            while (true)
            {
                PlayMoveOnMode(ref i_Game);
            }
        }
    }

    public static bool PlayMoveOffMode(ref GameController i_Game)
    {
        int playerSign = 1;
        int input;
        bool playAnotherMove = true;
        bool fullCapacity;

        while (playerSign <= 2)
        {
            input = 0;

            while (!i_Game.IsValidMakeMove(input, playerSign, out fullCapacity))
            {
                if (fullCapacity)
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
            HandleGameOver(ref i_Game, playerSign);
            playerSign++;
        }

        return playAnotherMove;
    }

    public static bool PlayMoveOnMode(ref GameController i_Game)
    {
        int input = 0;
        bool playAnotherMove = true;
        bool fullCapacity;

        while (!i_Game.IsValidMakeMove(input, 1, out fullCapacity))
        {
            if (fullCapacity)
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
        if (playAnotherMove)
        {
            i_Game.MakeComputerMove();
            GameUI.DisplayScreen(i_Game.BoardMatrix);
            HandleGameOver(ref i_Game, 2);
        }

        return playAnotherMove;
    }

    public static void HandleGameOver(ref GameController i_Game, int i_PlayerSign)
    {
        int oppositePlayerSign;
        bool gameFinshed = false;
        bool askForAnotherGame = false;
        int gameOverSign = 0;

        GameUI.DisplayScreen(i_Game.BoardMatrix);

        if (i_Game.IsEmptyBoardMatrix()) //someone quit
        {
            oppositePlayerSign = i_PlayerSign == 1 ? 2 : 1;
            Console.WriteLine($"Player {oppositePlayerSign} Win!\nState of record:\nPlayer 1 : {i_Game.Player1.Record}\tPlayer 2 : {i_Game.Player2.Record}");
            askForAnotherGame = GameUI.AskForAnotherGame();
            gameFinshed = true;
        }

        if (!gameFinshed)
        {
            gameOverSign = i_Game.IsGameOver(i_PlayerSign);
        }

        if (gameOverSign == 1)
        {
            Console.WriteLine($"Player {i_PlayerSign} Win!\nState of record:\nPlayer 1 : {i_Game.Player1.Record}\tPlayer 2 : {i_Game.Player2.Record}");
            askForAnotherGame = GameUI.AskForAnotherGame();
            gameFinshed = true;
        }
        else if (gameOverSign == 2)
        {
            Console.WriteLine($"Nobody Win, State of record:\nPlayer 1 : {i_Game.Player1.Record}\tPlayer 2 : {i_Game.Player2.Record}");
            askForAnotherGame = GameUI.AskForAnotherGame();
            gameFinshed = true;
        }

        if (askForAnotherGame)
        {
            GameUI.DisplayScreen(i_Game.BoardMatrix);
            PlayNewGame(ref i_Game);
        }

        if (gameFinshed)
        {
            Console.WriteLine("Thank you for playing");
            Environment.Exit(1);
        }
    }
}