public static class MinimaxEngine 
{
        public static Move FindBestMove(Board board, int agentSymbol, int humanSymbol) 
        {
            int bestValue = int.MaxValue;
            Move bestMove = new Move(-1, -1);
            foreach (Move move in board.GetAvailableMoves()) 
            {
                board.ApplyMove(move, agentSymbol);
                int moveValue = Minimax(board, true, int.MinValue, int.MaxValue, 0, agentSymbol, humanSymbol);
                board.ApplyMove(move, 0);
                if (moveValue < bestValue) 
                {
                    bestValue = moveValue;
                    bestMove = move;
                }
            }
            return bestMove;
        }

        private static int Minimax(Board board, bool turnHuman, int alpha, int beta, int depth, int agentSymbol, int humanSymbol) 
        {
            if (board.IsWin(humanSymbol)) return 10 - depth;
            if (board.IsWin(agentSymbol)) return -10 + depth;
            if (board.IsTie()) return 0;

            if (turnHuman) 
            {
                int best = int.MinValue;
                foreach (Move move in board.GetAvailableMoves()) 
                {
                    board.ApplyMove(move, humanSymbol);
                    best = Math.Max(best, Minimax(board, false, alpha, beta, depth + 1, agentSymbol, humanSymbol));
                    board.ApplyMove(move, 0);
                    alpha = Math.Max(alpha, best);
                    if (beta <= alpha) break;
                }
                return best;
            } 
            else 
            {
                int worst = int.MaxValue;
                foreach (Move move in board.GetAvailableMoves()) 
                {
                    board.ApplyMove(move, agentSymbol);
                    worst = Math.Min(worst, Minimax(board, true, alpha, beta, depth + 1, agentSymbol, humanSymbol));
                    board.ApplyMove(move, 0);
                    beta = Math.Min(beta, worst);
                    if (beta <= alpha) break;
                }
                return worst;
            }
        }
    }