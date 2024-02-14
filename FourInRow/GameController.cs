using System;
using System.Data.Common;

namespace FourInRow
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
                IsQuit(i_NumOfPlayer);
                validColumn = true;
            }
            else if (i_Column != 0 && UpdateMatrix(i_Column, i_NumOfPlayer))
            {
                validColumn = true;
            }
            else
            {
                fullCapacity = i_Column == 0 ? false : true;
            }
            return validColumn;
        }

        private bool UpdateMatrix(int i_Column, int i_NumOfPlayer)
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
            if (IsWinner(i_NumOfPlayer))
            {
                isGameOverSign = 1;
            }
            else if (IsDraw())
            {
                RestartGame();
                isGameOverSign = 2;
            }
            return isGameOverSign;
        }

        private bool IsWinner(int i_NumOfPlayer)
        {
            return false;
        }

        private bool IsDraw()
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

        private void IsQuit(int i_NumOfPlayer)
        {
            RestartGame();
            if (m_Player1.NumOfPlayer == i_NumOfPlayer)
            {
                m_Player2.Winner();
            }
            else
            {
                m_Player1.Winner();
            }

        }

        private void RestartGame()
        {
            Array.Clear(m_BoardMatrix, 0, m_BoardMatrix.Length);
        }

        public bool MakeComputerMove()
        {
            bool o_FullCapacity;
            Random rand = new Random();
            int columnRandomed = rand.Next(m_ColsOfBoard);

            return IsValidMakeMove(columnRandomed, 2, out o_FullCapacity) ? true : MakeComputerMove();

        }

        public class Player
        {
            private int m_Record, m_NumOfPlayer;

            public int NumOfPlayer
            {
                get { return m_NumOfPlayer; }
            }

            public int Record
            {
                get { return m_Record; }
            }

            public Player(int i_NumOfPlayer)
            {
                m_NumOfPlayer = i_NumOfPlayer;
                m_Record = 0;
            }

            public void Winner()
            {
                m_Record += 1;
            }

        }

    }
}