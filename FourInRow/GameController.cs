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

		public GameController(int i_Rows,int i_Cols,bool i_GameMode)
		{
			this.m_RowsOfBoard = i_Rows;
			this.m_ColsOfBoard = i_Cols;
			this.m_GameMode = i_GameMode;

			m_BoardMatrix = new int[i_Rows, i_Cols];
		}

		public bool IsValidMove(int i_Column,int i_NumOfPlayer)
		{
			bool validColumn = false;
			if ( i_Column != 0 && UpdateMatrix(i_Column, i_NumOfPlayer))
			{
				validColumn = true;
            }
			return validColumn;
        }

		private bool UpdateMatrix(int i_Column, int i_NumOfPlayer)
        {
			bool validColumn = false;
			for(int i = m_RowsOfBoard - 1; i >= 0; i--)
			{
				if (m_BoardMatrix[i,i_Column - 1] == 0)
				{
					m_BoardMatrix[i, i_Column - 1] = i_NumOfPlayer;
					validColumn = true;
					break;
                }
			}
			return validColumn;
        }

        public bool MakeComputerMove()
        {
			Random rand = new Random();
			int columnRandomed = rand.Next(m_ColsOfBoard);

			return IsValidMove(columnRandomed, 2) ? true : MakeComputerMove();

        }

        private bool IsValidColumn(int i_Column)
		{
			bool validColumn = false;
            for (int i = m_RowsOfBoard; i >= 0; i--)
            {
                if (m_BoardMatrix[i - 1, i_Column - 1] == 0)
                {
					validColumn = true;
					break;
                }
            }

			return validColumn;

        }
	}

	public class Player
	{
		private int m_Record, m_NumOfPlayer;

		public void Winner()
		{
			m_Record += 1;
		}
	}

}

