using System;
namespace FourInRow
{
	public class GameController
	{
		private int m_RowsOfBoard, m_ColsOfBoard;
		private int [,] m_BoardMatrix;
		private bool m_GameMode; // set true for play against computer or false for two players.

		public int[,] BoardMatrix
		{
			get
			{
				return this.m_BoardMatrix;
			}
		}

		public GameController(int i_Rows,int i_Cols,bool i_GameMode)
		{
			this.m_RowsOfBoard = i_Rows;
			this.m_ColsOfBoard = i_Cols;
			this.m_GameMode = i_GameMode;

			m_BoardMatrix = new int[i_Rows, i_Cols];
		}
	}
}

