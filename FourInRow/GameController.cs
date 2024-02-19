using System;

namespace FourInRow.Logic
{
    public class GameController
    {
        private int m_RowsOfBoard, m_ColsOfBoard;
        private int[,] m_BoardMatrix;
        private bool m_GameMode; // set true for play against computer or false for two players.
        private Player m_Player1, m_Player2;

        public Player Player1
        {
            get { return m_Player1; }
        }

        public Player Player2
        {
            get { return m_Player2; }
        }

        public int[,] BoardMatrix
        {
            get
            {
                return this.m_BoardMatrix;
            }
        }

        public bool GameMode
        {
            get
            {
                return m_GameMode;
            }
        }

        public GameController(int i_Rows, int i_Cols, bool i_GameMode)
        {
            this.m_RowsOfBoard = i_Rows;
            this.m_ColsOfBoard = i_Cols;
            this.m_GameMode = i_GameMode;

            m_BoardMatrix = new int[i_Rows, i_Cols];

            m_Player1 = new Player(1);
            m_Player2 = new Player(2);
        }

        public bool IsValidMakeMove(int i_Column, int i_NumOfPlayer, out bool fullCapacity)
        {
            fullCapacity = false;
            bool validColumn = false;
            if (i_Column < 0)
            {
                isQuit(i_NumOfPlayer);
                validColumn = true;
            }
            else if (i_Column != 0 && updateMatrix(i_Column, i_NumOfPlayer))
            {
                validColumn = true;
            }
            else
            {
                fullCapacity = i_Column != 0;
            }
            return validColumn;
        }

        private bool updateMatrix(int i_Column, int i_NumOfPlayer)
        {
            bool validColumn = false;
            for (int i = m_RowsOfBoard - 1; i >= 0; i--)
            {
                if (m_BoardMatrix[i, i_Column - 1] == 0)
                {
                    m_BoardMatrix[i, i_Column - 1] = i_NumOfPlayer;
                    validColumn = true;
                    break;
                }
            }
            return validColumn;
        }

        public bool IsEmptyBoardMatrix()
        {
            bool isEmpty = true;
            for (int i = 0; i < m_RowsOfBoard; i++)
            {
                for (int j = 0; j < m_ColsOfBoard; j++)
                {
                    if (m_BoardMatrix[i, j] != 0)
                    {
                        isEmpty = false;
                    }
                }
            }
            return isEmpty;
        }

        public int IsGameOver(int i_NumOfPlayer)
        {
            int isGameOverSign = 0;
            if (isWinner(i_NumOfPlayer))
            {
                isGameOverSign = 1;
            }
            else if (isDraw())
            {
                restartGame();
                isGameOverSign = 2;
            }
            return isGameOverSign;
        }

        private bool isWinner(int i_NumOfPlayer)
        {
            bool isWinner = false;
            // Check horizontal
            for (int row = 0; row < m_RowsOfBoard; row++)
            {
                for (int col = 0; col < m_ColsOfBoard - 3; col++)
                {
                    if (m_BoardMatrix[row, col] == i_NumOfPlayer &&
                        m_BoardMatrix[row, col + 1] == i_NumOfPlayer &&
                        m_BoardMatrix[row, col + 2] == i_NumOfPlayer &&
                        m_BoardMatrix[row, col + 3] == i_NumOfPlayer)
                    {
                        isWinner = true;
                    }
                }
            }

            // Check vertical
            for (int row = 0; row < m_RowsOfBoard - 3; row++)
            {
                for (int col = 0; col < m_ColsOfBoard; col++)
                {
                    if (m_BoardMatrix[row, col] == i_NumOfPlayer &&
                        m_BoardMatrix[row + 1, col] == i_NumOfPlayer &&
                        m_BoardMatrix[row + 2, col] == i_NumOfPlayer &&
                        m_BoardMatrix[row + 3, col] == i_NumOfPlayer)
                    {
                        isWinner = true;
                    }
                }
            }

            // Check diagonals (positive slope)
            for (int row = 0; row < m_RowsOfBoard - 3; row++)
            {
                for (int col = 0; col < m_ColsOfBoard - 3; col++)
                {
                    if (m_BoardMatrix[row, col] == i_NumOfPlayer &&
                        m_BoardMatrix[row + 1, col + 1] == i_NumOfPlayer &&
                        m_BoardMatrix[row + 2, col + 2] == i_NumOfPlayer &&
                        m_BoardMatrix[row + 3, col + 3] == i_NumOfPlayer)
                    {
                        isWinner = true;
                    }
                }
            }

            // Check diagonals (negative slope)
            for (int row = 3; row < m_RowsOfBoard; row++)
            {
                for (int col = 0; col < m_ColsOfBoard - 3; col++)
                {
                    if (m_BoardMatrix[row, col] == i_NumOfPlayer &&
                        m_BoardMatrix[row - 1, col + 1] == i_NumOfPlayer &&
                        m_BoardMatrix[row - 2, col + 2] == i_NumOfPlayer &&
                        m_BoardMatrix[row - 3, col + 3] == i_NumOfPlayer)
                    {
                        isWinner = true;
                    }
                }
            }
            if (isWinner)
            {
                isQuit(i_NumOfPlayer == 1 ? 2 : 1);
            }

            return isWinner;
        }


        private bool isDraw()
        {
            bool isFull = true;

            for (int i = 0; i < m_RowsOfBoard; i++)
            {
                for (int j = 0; j < m_ColsOfBoard; j++)
                {
                    if (m_BoardMatrix[i, j] == 0)
                    {
                        isFull = false;
                    }
                }
            }
            return isFull;
        }

        private void isQuit(int i_NumOfPlayer)
        {
            restartGame();

            if (m_Player1.NumOfPlayer == i_NumOfPlayer)
            {
                m_Player2.Winner();
            }
            else
            {
                m_Player1.Winner();
            }

        }

        private void restartGame()
        {
            Array.Clear(m_BoardMatrix, 0, m_BoardMatrix.Length);
        }

        public void MakeComputerMove()
        {
            bool o_FullCapacity;

            Random rand = new Random();
            int columnRandomed = rand.Next(1, m_ColsOfBoard + 1);

            while (!IsValidMakeMove(columnRandomed, 2, out o_FullCapacity) || o_FullCapacity)
                {
                    columnRandomed = rand.Next(1, m_ColsOfBoard + 1);
                }
        }
    }
 
}