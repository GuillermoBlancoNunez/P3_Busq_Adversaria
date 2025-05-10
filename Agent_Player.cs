// Guillermo Blanco Núñez & Fiz Garrido Escudero




/// <summary>
/// Represents an AI-controlled player that selects moves using the Minimax algorithm.
/// </summary>
public class Agent_Player : Player
{
    private int agentSymbol;
    private int humanSymbol;

    /// <summary>
    /// Initializes a new instance of the <see cref="Agent_Player"/> class.
    /// </summary>
    /// <param name="agentSymbol">
    /// Numeric symbol assigned to the agent (e.g., 1 for 'X' or -1 for 'O').
    /// </param>
    /// <param name="humanSymbol">
    /// Numeric symbol assigned to the human player (the opposite of the agent).
    /// </param>
    public Agent_Player(int agentSymbol, int humanSymbol)
    {
        this.agentSymbol = agentSymbol;
        this.humanSymbol = humanSymbol;
    }

    /// <summary>
    /// Gets the best move for the agent given the current board state.
    /// </summary>
    /// <param name="board">The board instance representing the current game state.</param>
    /// <returns>A <see cref="Move"/> representing the chosen move.</returns>
    public Move GetMove(Board board)
    {
        return MinimaxClass.FindBestMove(board, agentSymbol, humanSymbol);
    }
}
