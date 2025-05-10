// Guillermo Blanco Núñez & Fiz Garrido Escudero




/// <summary>
/// Implements the Minimax algorithm with Alpha-Beta pruning to evaluate the best move.
/// </summary>
public static class MinimaxClass
{
    /// <summary>
    /// Finds the best possible move for the agent.
/// </summary>
/// <param name="board">Current board state.</param>
/// <param name="agentSymbol">Agent's numeric symbol.</param>
/// <param name="humanSymbol">Human player's numeric symbol.</param>
/// <returns>The optimal <see cref="Move"/>.</returns>
    public static Move FindBestMove(Board board, int agentSymbol, int humanSymbol)
    {
        int bestValue = int.MinValue;
        Move bestMove = new Move(-1, -1);

        foreach (Move move in board.GetAvailableMoves())
        {
            board.ApplyMove(move, agentSymbol);
            int moveValue = Minimax(board, true, int.MinValue, int.MaxValue, 0, agentSymbol, humanSymbol);
            board.ApplyMove(move, 0); // undo
            if (moveValue > bestValue)
            {
                bestValue = moveValue;
                bestMove = move;
            }
        }
        return bestMove;
    }

    /// <summary>
    /// Recursively executes the Minimax algorithm with Alpha-Beta pruning.
/// </summary>
/// <param name="board">Current board state.</param>
/// <param name="turnHuman"><c>true</c> if it's the human's turn; <c>false</c> if it's the agent's turn.</param>
/// <param name="alpha">Alpha value for pruning.</param>
/// <param name="beta">Beta value for pruning.</param>
/// <param name="depth">Current recursion depth.</param>
/// <param name="agentSymbol">Agent's numeric symbol.</param>
/// <param name="humanSymbol">Human player's numeric symbol.</param>
/// <returns>Heuristic value of the current position.</returns>
    private static int Minimax(Board board, bool turnHuman, int alpha, int beta, int depth, int agentSymbol, int humanSymbol)
    {
        if (board.IsWin(agentSymbol)) return 10 - depth;
        if (board.IsWin(humanSymbol)) return depth - 10;
        if (board.IsTie())           return 0;

        if (turnHuman)
        {
            int worst = int.MaxValue;
            foreach (Move move in board.GetAvailableMoves())
            {
                board.ApplyMove(move, humanSymbol);
                worst = Math.Min(worst, Minimax(board, false, alpha, beta, depth + 1, agentSymbol, humanSymbol));
                board.ApplyMove(move, 0);
                beta = Math.Min(beta, worst);
                if (beta <= alpha) break; // prune
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
                alpha = Math.Max(alpha, best);
                if (beta <= alpha) break; // prune
            }
            return best;
        }
    }
}
