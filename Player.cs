// Guillermo Blanco Núñez & Fiz Garrido Escudero




/// <summary>
/// Interface that all player types must implement.
/// </summary>
public interface Player
{
    /// <summary>
    /// Gets the next move to make given the current board.
/// </summary>
/// <param name="board">Current board state.</param>
/// <returns>The <see cref="Move"/> selected by the player.</returns>
    Move GetMove(Board board);
}
