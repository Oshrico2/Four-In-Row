namespace FourInRow.Logic
{
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