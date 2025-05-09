public static class MinimaxClass
{
    public static Move FindBestMove(Board board, int agentSymbol, int humanSymbol)
    {
        int bestValue = int.MinValue;
        Move bestMove = new Move(-1, -1);
        foreach (Move move in board.GetAvailableMoves())
        {
            board.ApplyMove(move, agentSymbol);
            int moveValue = Minimax(board, true, int.MinValue, int.MaxValue, 0, agentSymbol, humanSymbol);
            board.ApplyMove(move, 0);
            if (moveValue > bestValue)
            {
                bestValue = moveValue;
                bestMove = move;
            }
        }
        return bestMove;
    }

    private static int Minimax(Board board, bool turnHuman, int alpha, int beta, int depth, int agentSymbol, int humanSymbol)
    {
        if (board.IsWin(agentSymbol)) return 10 - depth;
        if (board.IsWin(humanSymbol)) return -10 + depth;
        if (board.IsTie()) return 0;

        if (turnHuman)
        {
            int worst = int.MaxValue;
            foreach (Move move in board.GetAvailableMoves())
            {
                board.ApplyMove(move, humanSymbol);
                worst = Math.Min(worst, Minimax(board, false, alpha, beta, depth + 1, agentSymbol, humanSymbol));
                board.ApplyMove(move, 0);
                beta = Math.Max(beta, worst);
                if (beta <= alpha) break;
            }
            return worst;
        }
        else
        {
            int best = int.MinValue;
            foreach (Move move in board.GetAvailableMoves())
            {
                board.ApplyMove(move, agentSymbol);
                best = Math.Max(best, Minimax(board, true, alpha, beta, depth + 1, agentSymbol, humanSymbol));
                board.ApplyMove(move, 0);
                alpha = Math.Min(alpha, best);
                if (beta <= alpha) break;
            }
            return best;
        }
    }
}