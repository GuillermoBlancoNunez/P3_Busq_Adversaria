// Guillermo Blanco Núñez & Fiz Garrido Escudero




/// <summary>
/// Represents a move on the board by row and column.
/// </summary>
public struct Move
{
    /// <summary>Row index (0–2) of the move.</summary>
    public int Row { get; set; }

    /// <summary>Column index (0–2) of the move.</summary>
    public int Col { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Move"/> struct with the specified row and column.
/// </summary>
/// <param name="row">Row of the move (0–2).</param>
/// <param name="col">Column of the move (0–2).</param>
    public Move(int row, int col)
    {
        Row = row;
        Col = col;
    }
}
