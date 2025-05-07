public class Board 
{
        private int[,] cells = new int[3,3];

        public bool IsEmpty(int row, int col) => cells[row,col] == 0;

        public void ApplyMove(Move move, int player) 
        {
            cells[move.Row, move.Col] = player;
        }

        public List<Move> GetAvailableMoves() 
        {
            List<Move> moves = new List<Move>();
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    if (cells[i, j] == 0)
                        moves.Add(new Move(i, j));
            return moves;
        }

        public bool IsWin(int player) 
        {
            for (int i = 0; i < 3; i++) 
            {
                if (cells[i, 0] == player && cells[i, 1] == player && cells[i, 2] == player) return true;
                if (cells[0, i] == player && cells[1, i] == player && cells[2, i] == player) return true;
            }
            if (cells[0, 0] == player && cells[1, 1] == player && cells[2, 2] == player) return true;
            if (cells[0, 2] == player && cells[1, 1] == player && cells[2, 0] == player) return true;
            return false;
        }

        public bool IsTie() 
        {
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    if (cells[i, j] == 0)
                        return false;
            return true;
        }

        public override string ToString() 
        {
            string result = "\n";
            for (int i = 0; i < 3; i++) 
            {
                result += "|";
                for (int j = 0; j < 3; j++) 
                {
                    char c = cells[i, j] == 1 ? 'X' : cells[i, j] == -1 ? 'O' : ' ';
                    result += c + "|";
                }
                result += "\n";
            }
            return result;
        }
        

        public int[,] Cells => cells;
    }