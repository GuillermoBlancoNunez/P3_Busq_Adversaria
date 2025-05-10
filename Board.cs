// Guillermo Blanco Núñez & Fiz Garrido Escudero




/// <summary>
/// Represents the 3x3 grid for a Tic-Tac-Toe game.
/// </summary>
/// <remarks>
/// Each cell is stored as an integer: 0 = empty, 1 = 'X', -1 = 'O'.
/// </remarks>
public class Board
{
    private int[,] cells = new int[3, 3];

    /// <summary>
    /// Checks if the specified cell is empty.
/// </summary>
/// <param name="row">Row index (0–2).</param>
/// <param name="col">Column index (0–2).</param>
/// <returns><c>true</c> if the cell is empty; otherwise, <c>false</c>.</returns>
    public bool IsEmpty(int row, int col) => cells[row, col] == 0;

    /// <summary>
    /// Applies a move to the board by setting the player's symbol in the cell.
/// </summary>
/// <param name="move">The <see cref="Move"/> specifying row and column.</param>
/// <param name="player">Player symbol (1 or -1).</param>
    public void ApplyMove(Move move, int player)
    {
        cells[move.Row, move.Col] = player;
    }

    /// <summary>
    /// Gets all available moves (empty cells) on the board.
/// </summary>
/// <returns>A list of <see cref="Move"/> instances for each empty cell.</returns>
    public List<Move> GetAvailableMoves()
    {
        var moves = new List<Move>();
        for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++)
                if (cells[i, j] == 0)
                    moves.Add(new Move(i, j));
        return moves;
    }

    /// <summary>
    /// Determines whether the specified player has won the game.
/// </summary>
/// <param name="player">Player symbol to check.</param>
/// <returns><c>true</c> if the player has three in a row; otherwise, <c>false</c>.</returns>
    public bool IsWin(int player)
    {
        // Rows and columns
        for (int i = 0; i < 3; i++)
        {
            if (cells[i, 0] == player && cells[i, 1] == player && cells[i, 2] == player) return true;
            if (cells[0, i] == player && cells[1, i] == player && cells[2, i] == player) return true;
        }
        // Diagonals
        if (cells[0, 0] == player && cells[1, 1] == player && cells[2, 2] == player) return true;
        if (cells[0, 2] == player && cells[1, 1] == player && cells[2, 0] == player) return true;
        return false;
    }

    /// <summary>
    /// Checks if the board is full and there is no winner.
/// </summary>
/// <returns><c>true</c> if all cells are occupied; otherwise, <c>false</c>.</returns>
    public bool IsTie()
    {
        for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++)
                if (cells[i, j] == 0)
                    return false;
        return true;
    }

    /// <summary>
    /// Returns a string representation of the board for console display.
/// </summary>
/// <returns>A formatted grid string with 'X', 'O', or space characters.</returns>
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

    /// <summary>
    /// Gets the internal cell matrix (read-only).
/// </summary>
    public int[,] Cells => cells;
}
